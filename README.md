# Universidad Tecnológica de Panamá
## Facultad de Ingeniería de Sistemas Computacionales

### Parcial #3 | Parte Práctica

**Asignatura:** Herramientas de la Programación Aplicada III (.NET)  
**Docente:** Ing. Irina Fong  
**Grupo:** 1IL133  
## Información del Estudiante
**Nombre:** Maria Townshend
**Grupo:** 1IL133
**Cédula:** 8-1027-1450

## Fecha de Ejecución: 08/11/26


## Introducción

La parte práctica del Parcial #3 consta de tres actividades. En la primera se implementa una jerarquía de figuras geométricas. En la segunda se convierte un objeto con los datos de una persona a formato JSON. Ambas actividades se ejecutan en un proyecto de consola y comparten el archivo `Program.cs`.

La tercera actividad se desarrolla en Windows Forms. La aplicación permite registrar usuarios, verificar sus contraseñas con BCrypt y guardar en SQL Server el resultado de cada intento de inicio de sesión.

## Requisitos Previos

- Visual Studio Community con la carga de trabajo **Desarrollo de escritorio de .NET**.
- SDK de .NET 10 para el proyecto de consola.
- .NET Framework en la versión de destino indicada por el proyecto Windows Forms.
- SQL Server Express y SQL Server Management Studio (SSMS) para la Actividad 3.
- Restaurar los paquetes NuGet `Newtonsoft.Json` y `BCrypt.Net-Next` en sus respectivos proyectos.

## Tecnologías utilizadas

| Tecnología | Uso |
|---|---|
| C# y .NET 10 | Proyecto de consola para los problemas 1 y 2 |
| `Newtonsoft.Json` | Serialización del objeto del problema 2 |
| C# 7.3 y Windows Forms (.NET Framework) | Aplicación de registro e inicio de sesión del problema 3 |
| `BCrypt.Net-Next` | Generación y verificación de hashes de contraseñas |
| `System.Data.SqlClient` | Conexión y ejecución de consultas parametrizadas en SQL Server |
| SQL Server Express y SSMS | Almacenamiento, consulta y respaldo de los datos del login |


## Instrucciones de Ejecución

### Problemas 1 y 2

1. Abrir en Visual Studio Community la solución del proyecto de consola.
2. Restaurar el paquete NuGet `Newtonsoft.Json`.
3. Compilar el proyecto y ejecutarlo con **Ctrl + F5**.
4. Revisar la salida de la consola. Primero aparecen las figuras con sus áreas y perímetros. Después aparece el objeto serializado a JSON.

### Problema 3

1. Conectarse a SQL Server desde SSMS.
2. Restaurar el archivo `HPA_Parcial3_BCrypt.bak` con el nombre `HPA_Parcial3_BCrypt`. Para empezar con las tablas vacías, ejecutar `01_CrearBase.sql`.
3. Abrir en Visual Studio Community la solución del proyecto Windows Forms y restaurar `BCrypt.Net-Next`.
4. En `AccesoDatos.cs`, ajustar el valor de `Server` en `CadenaConexion` al nombre de la instancia usada en SSMS. El proyecto utiliza autenticación de Windows.
5. Compilar el proyecto y presionar **F5**.
6. Si la base de datos está vacía, registrar una cuenta ficticia. Para reproducir las pruebas, usar `ana` como usuario, `Ana Pérez` como nombre completo y `PruebaHPA2026!` como contraseña de prueba.

## Problema #1. Clases abstractas y polimorfismo

### Descripción

Diseñar una jerarquía de clases para calcular el área y el perímetro de un rectángulo, un cuadrado y un triángulo.

### Funcionamiento

La clase abstracta `Figura` declara los métodos `CalcularArea()` y `CalcularPerimetro()`. Las clases `Rectangulo`, `Cuadrado` y `Triangulo` heredan de `Figura` e implementan ambos métodos con `override`. El triángulo calcula su área mediante la fórmula de Herón a partir de sus tres lados.

En `Main`, las tres instancias se guardan en un arreglo unidimensional de tipo `Figura[]`. Un `foreach` recorre el arreglo, muestra el nombre de la clase con `GetType().Name` y llama a los métodos de cálculo. En cada iteración se ejecuta la implementación de la figura que ocupa esa posición del arreglo.

## Problema #2. Serialización de un objeto a JSON

### Descripción

Ejecutar el ejemplo de serialización de los datos de una persona utilizando `Newtonsoft.Json`.

### Funcionamiento

En el mismo `Program.cs`, se crea un objeto con las propiedades `Nombre`, `Edad` y `Carrera`. El método `JsonConvert.SerializeObject(persona)` convierte el objeto en una cadena JSON. Luego, `Console.WriteLine(json)` muestra esa cadena en la consola.

### Resultado de los problemas 1 y 2:

<img width="1080" height="485" alt="image" src="https://github.com/user-attachments/assets/5619f375-befe-4b4c-b81e-b19bb4a4ca30" />

## Problema #3. Generación y verificación de contraseñas con BCrypt

### Descripción

Desarrollar un formulario de registro e inicio de sesión que almacene los hashes de las contraseñas y conserve el historial de accesos en SQL Server.

### Funcionamiento

1. **Generar y almacenar el hash.** Al presionar **Registrar usuario**, el formulario llama a `BCrypt.HashPassword()` y guarda el hash en `dbo.Usuarios` junto con el usuario y el nombre completo.
2. **Verificar la contraseña.** Al presionar **Iniciar sesión**, el programa busca la cuenta y, si la encuentra y la entrada pasa las validaciones, comprueba la contraseña con `BCrypt.Verify()`. Después registra en `dbo.IntentosAcceso` el nombre de usuario ingresado y el resultado. SQL Server asigna la fecha al insertar la fila.
3. **Mostrar el nombre al ingresar.** Si la contraseña es correcta y el intento se guarda, el formulario muestra el nombre completo recuperado de `dbo.Usuarios`. Cuando la cuenta no existe, se registra el intento como fallido y su `IdUsuario` queda en `NULL`.

### Resultado

Con la cuenta ficticia `Maria13`, el formulario muestra **«Bienvenido al sistema, Maria Alejandra»** al ingresar la contraseña correcta. El historial permite revisar los accesos exitosos y fallidos.

**pantalla inicial**
<img width="783" height="470" alt="image" src="https://github.com/user-attachments/assets/fa0105c6-a040-4195-b452-366f52470fc3" />

**Hash de usuarios almacenado en SQL Server**
<img width="1144" height="591" alt="image" src="https://github.com/user-attachments/assets/4d285526-2523-43bb-a323-1dbe90c0c653" />

**Historial de intentos de acceso**
<img width="948" height="574" alt="image" src="https://github.com/user-attachments/assets/dc99e97d-e9b2-4787-9779-8e63d669bed9" />

**Intenta registrar misma cuenta de nuevo**
<img width="786" height="468" alt="image" src="https://github.com/user-attachments/assets/f7c14ff5-9d49-4800-8afd-a3b0510a2486" />

**Inicio de sesión exitoso**
<img width="786" height="467" alt="image" src="https://github.com/user-attachments/assets/458d1921-3927-456a-9fd0-94726b2fabe2" />

**datos de login incorrectos**
<img width="784" height="465" alt="image" src="https://github.com/user-attachments/assets/f9873e5a-9fc2-4ad2-9be6-f5287c757cf0" />

**Creación de cuenta nueva**
<img width="782" height="460" alt="image" src="https://github.com/user-attachments/assets/4f55b3a4-4ffb-4d18-a5ab-7d78fa822462" />

**tabla de usuarios actualizada**
<img width="946" height="573" alt="image" src="https://github.com/user-attachments/assets/917ae31e-17f9-4d2c-ae13-90bf05f57bde" />


## Dificultades y Soluciones

| Dificultad | Solución |
|---|---|
| El proyecto Windows Forms compilaba con C# 7.3 y generaba errores con `using var` y `object?` | Se utilizaron bloques `using (...)` y se ajustaron las declaraciones a la sintaxis de C# 7.3 |
| El proyecto Windows Forms no reconocía `Microsoft.Data.SqlClient` | Se adaptó la clase `AccesoDatos` para utilizar `System.Data.SqlClient` |
| Visual Studio no reconocía `BCrypt` al compilar el formulario | Se agregó el paquete `BCrypt.Net-Next` al proyecto Windows Forms |
