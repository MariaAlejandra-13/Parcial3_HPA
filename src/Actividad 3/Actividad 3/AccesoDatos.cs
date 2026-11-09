using System;
using System.Data;
using System.Data.SqlClient;

namespace Actividad_3
{
    internal class AccesoDatos
    {
        private const string CadenaConexion =
            @"Server=DESKTOP-V6IHOS5\SQLEXPRESS;Database=HPA_Parcial3_BCrypt;" +
            "Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        public void RegistrarUsuario(string usuario, string nombre, string hash)
        {
            const string sql = @"
                INSERT INTO dbo.Usuarios
                    (NombreUsuario, NombreCompleto, HashContrasena)
                VALUES (@Usuario, @Nombre, @Hash);";

            using (var conexion = new SqlConnection(CadenaConexion))
            using (var comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.Add("@Usuario", SqlDbType.NVarChar, 50).Value = usuario;
                comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = nombre;
                comando.Parameters.Add("@Hash", SqlDbType.VarChar, 60).Value = hash;

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public (int Id, string Nombre, string Hash)? BuscarUsuario(string usuario)
        {
            const string sql = @"
                SELECT IdUsuario, NombreCompleto, HashContrasena
                FROM dbo.Usuarios
                WHERE NombreUsuario = @Usuario;";

            using (var conexion = new SqlConnection(CadenaConexion))
            using (var comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.Add("@Usuario", SqlDbType.NVarChar, 50).Value = usuario;

                conexion.Open();

                using (var lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        return (lector.GetInt32(0), lector.GetString(1), lector.GetString(2));
                    }

                    return null;
                }
            }
        }

        public void GuardarIntento(int? idUsuario, string usuario, bool exitoso)
        {
            const string sql = @"
                INSERT INTO dbo.IntentosAcceso
                    (IdUsuario, UsuarioIngresado, Exitoso)
                VALUES (@IdUsuario, @Usuario, @Exitoso);";

            using (var conexion = new SqlConnection(CadenaConexion))
            using (var comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.Add("@IdUsuario", SqlDbType.Int).Value =
                    (object)idUsuario ?? DBNull.Value;

                comando.Parameters.Add("@Usuario", SqlDbType.NVarChar, 50).Value = usuario;
                comando.Parameters.Add("@Exitoso", SqlDbType.Bit).Value = exitoso;

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }
    }
}
