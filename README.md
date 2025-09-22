CotoReserva

Proyecto de sistema de reservas con API en .NET y frontend en React.

Contenido

- Descripción
- Tecnologías
- Requisitos
- Ejecución del backend (.NET)
- Ejecución del frontend (React)
- Docker
- Notas

Descripción

Este proyecto permite gestionar reservas de salones mediante una API REST construida con .NET y una interfaz web hecha en React.

Tecnologías

- .NET 9 / C#
- React 19
- Bootstrap 5
- xUnit para pruebas unitarias
- Docker (opcional)

Requisitos

- .NET 9 SDK instalado
- Node.js y npm instalados
- Docker instalado (opcional)

Ejecución del backend (.NET)

1. Abrir la carpeta del backend en terminal.
2. Restaurar paquetes:
   dotnet restore
3. Ejecutar el backend:
   dotnet run
4. Por defecto corre en https://localhost:7029.
5. Para correr pruebas unitarias:
   dotnet test

Ejecución del frontend (React)

1. Abrir la carpeta del frontend en terminal.
2. Instalar dependencias:
   npm install
3. Ejecutar la app React:
   npm start
4. La app está disponible en http://localhost:3000.
5. Asegurarse que la variable apiBaseUrl en el código apunte a la URL correcta del backend.

Docker

Dockerizar React

Construir la imagen:
docker build -t cotoreserva-react .
Ejecutar el contenedor:
docker run -p 3000:80 cotoreserva-react
Luego entrar a http://localhost:3000 para ver la app.

Notas

- Si usas Docker para backend, ajustar URLs en frontend para conectarse correctamente.
- Recuerda correr backend antes que frontend para evitar errores de conexión.
- Usa Swagger en backend para probar endpoints: https://localhost:7029/swagger
- Si tienes dudas o problemas, abrir un issue o contactar al equipo de desarrollo.

