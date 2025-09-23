### CotoReserva

Proyecto de sistema de reservas con API en .NET y frontend en React.

-----

### Contenido

  - Descripción
  - Tecnologías
  - Requisitos
  - **Ejecución de la app con Docker Compose (Recomendado)**
  - Ejecución de la app manualmente
  - Notas

-----

### Descripción

Este proyecto permite gestionar reservas de salones mediante una API REST construida con .NET y una interfaz web hecha en React.

-----

### Tecnologías

  - .NET 9 / C\#
  - React 19
  - Bootstrap 5
  - xUnit para pruebas unitarias
  - **Docker Compose**

-----

### Requisitos

  - **Docker y Docker Compose** instalado.

-----

### Ejecución de la app con Docker Compose (Recomendado)

La forma más sencilla de ejecutar la aplicación completa es usando Docker Compose, que levantará el backend y el frontend simultáneamente.

1.  Abre la terminal en la **raíz del proyecto** (donde se encuentra el archivo `docker-compose.yml`).

2.  Ejecuta el siguiente comando para construir las imágenes e iniciar los contenedores en segundo plano:

    ```bash
    docker compose up -d
    ```

3.  Una vez que los contenedores estén levantados, la API estará disponible en `http://localhost:7029` y la aplicación React en `http://localhost:3000`.

-----

### Ejecución de la app manualmente

Esta sección es útil si no deseas usar Docker y prefieres ejecutar los proyectos individualmente.

#### Backend (.NET)

1.  Abre la carpeta del backend en la terminal.
2.  Restaura los paquetes:
    `dotnet restore`
3.  Ejecuta el backend:
    `dotnet run`
      * Por defecto, la API corre en `https://localhost:7029`.
4.  Para correr las pruebas unitarias:
    `dotnet test`

#### Frontend (React)

1.  Abre la carpeta del frontend en la terminal.
2.  Instala las dependencias:
    `npm install`
3.  Ejecuta la app React:
    `npm start`
      * La app estará disponible en `http://localhost:3000`.
4.  Asegúrate de que la variable `apiBaseUrl` en el código apunte a la URL correcta del backend.

-----

### Notas

  - **Variables de Entorno**: Las variables de entorno para la API y el frontend se gestionan en el archivo `docker-compose.yml`. Si necesitas cambiar la cadena de conexión o cualquier otra configuración, edita ese archivo antes de ejecutar `docker compose up`.
  - **Swagger**: Usa Swagger en el backend para probar los endpoints de la API: `https://localhost:7029/swagger`.
  - Si tienes dudas o problemas, abre un *issue* o contacta al equipo de desarrollo.
 