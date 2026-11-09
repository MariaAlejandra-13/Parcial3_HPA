using System;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace Actividad_3
{
    public partial class Form1 : Form
    {
        private readonly AccesoDatos datos = new AccesoDatos();

        public Form1()
        {
            InitializeComponent();

            btnRegistrar.Click += btnRegistrar_Click;
            btnIngresar.Click += btnIngresar_Click;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string contrasena = txtContrasena.Text;

            if (usuario.Length == 0 || nombre.Length == 0 ||
                string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show("Completa el usuario, el nombre y la contraseña.");
                return;
            }

            if (Encoding.UTF8.GetByteCount(contrasena) > 72)
            {
                MessageBox.Show("Usa una contraseña de hasta 72 bytes.");
                return;
            }

            try
            {
                string hash = BCrypt.Net.BCrypt.HashPassword(contrasena);

                datos.RegistrarUsuario(usuario, nombre, hash);

                txtContrasena.Clear();
                MessageBox.Show("Usuario registrado. El hash quedó guardado.");
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                MessageBox.Show("Ese usuario ya está registrado.");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al registrar el usuario.\n" + ex.Message);
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text;

            try
            {
                var cuenta = datos.BuscarUsuario(usuario);

                bool accesoExitoso = cuenta.HasValue &&
                    !string.IsNullOrWhiteSpace(contrasena) &&
                    Encoding.UTF8.GetByteCount(contrasena) <= 72 &&
                    BCrypt.Net.BCrypt.Verify(contrasena, cuenta.Value.Hash);

                datos.GuardarIntento(cuenta?.Id, usuario, accesoExitoso);

                txtContrasena.Clear();

                if (accesoExitoso)
                {
                    MessageBox.Show("Bienvenido al sistema, " + cuenta.Value.Nombre + ".");
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al procesar el acceso.\n" + ex.Message);
            }
        }
    }
}
