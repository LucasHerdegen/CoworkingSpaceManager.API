# Coworking Space Manager API

Una API RESTful robusta y segura diseñada para la gestión integral de espacios de coworking, permitiendo administrar oficinas, usuarios y reservas de manera eficiente.

Este proyecto fue desarrollado siguiendo las mejores prácticas de la industria, implementando una **Arquitectura Limpia (Clean Architecture)**, patrones de diseño sólidos y un fuerte enfoque en la seguridad.

## Tecnologías y Herramientas

* **Core:** .NET 9 (C#)
* **ORM:** Entity Framework Core (Code-First Approach)
* **Base de Datos:** SQL Server
* **Seguridad:** ASP.NET Core Identity + JWT (JSON Web Tokens)
* **Mapeo:** AutoMapper
* **Validación:** FluentValidation
* **Documentación:** Swagger UI (OpenAPI)
* **Control de Versiones:** Git & GitHub

## Arquitectura y Patrones de Diseño

El sistema está construido sobre una arquitectura de **N-Capas** para asegurar la separación de responsabilidades y la escalabilidad:

1.  **Controllers:** Maneja las peticiones HTTP y respuestas.
2.  **Services:** Contiene la lógica de negocio, validaciones complejas y orquestación.
3.  **Repository:** Abstrae el acceso a datos. Se implementa un **Repositorio Genérico** para operaciones estándar y **Repositorios Específicos** para consultas complejas.
4.  **Models/DTOs:** Entidades de dominio y objetos de transferencia de datos.

**Patrones clave implementados:**
* **Repository Pattern (Generic & Specific):** Desacopla la lógica de negocio del acceso a datos y evita la repetición de código CRUD.
* **Dependency Injection (DI):** Promueve la testabilidad y desacoplamiento entre clases.
* **DTOs (Data Transfer Objects):** Protege las entidades de dominio y optimiza la transferencia de datos al cliente.
* **Asynchronous Programming:** Uso extensivo de `async/await` para operaciones no bloqueantes.

## Funcionalidades Principales

* **Autenticación y Autorización:**
    * Registro y Login de usuarios.
    * Generación de **Tokens JWT** seguros.
    * Gestión de **Roles** (Admin vs User) para proteger endpoints críticos.
* **Gestión de Espacios (Workspaces):**
    * CRUD completo de oficinas y salas.
    * Validación de disponibilidad y capacidad.
* **Sistema de Reservas:**
    * Creación de reservas con validación de conflictos (evita doble reserva en la misma fecha).
    * Historial de "Mis Reservas" para usuarios.
    * Cancelación de reservas con validación de propiedad.
* **Validaciones Avanzadas:** Reglas de negocio (ej. fechas futuras, capacidad positiva) aplicadas con FluentValidation.

## Cómo ejecutar el proyecto localmente

### Prerrequisitos
* [.NET 9 SDK](https://dotnet.microsoft.com/download)
* SQL Server (Express, Developer o LocalDB)

### Pasos
1.  **Clonar el repositorio:**
    ```bash
    git clone [https://github.com/tu-usuario/CoworkingSpaceManager.API.git](https://github.com/tu-usuario/CoworkingSpaceManager.API.git)
    cd CoworkingSpaceManager.API
    ```

2.  **Configurar `appsettings.json`:**
    Asegurate de configurar la cadena de conexión y la clave secreta para JWT en `appsettings.Development.json`:
    ```json
    "ConnectionStrings": {
      "ContextConnection": "Server=localhost; Database=CoworkingDB; Trusted_Connection=True; TrustServerCertificate=True;"
    },
    "jwt": {
      "key": "TuClaveSuperSecretaDebeSerMuyLargaYSeguraaaaaaaaaaaaaaaaaaaaaa"
    }
    ```

3.  **Aplicar Migraciones:**
    Abrí una terminal en el directorio del proyecto y ejecuta:
    ```bash
    dotnet ef database update
    ```
    *Nota: Al iniciar la app, se ejecutará un **Seeder** automático que creará los roles 'Admin' y 'User' si no existen.*

4.  **Ejecutar la API:**
    ```bash
    dotnet run
    ```

5.  **Explorar:**
    Navegá a `http://localhost:5284/swagger` para ver la documentación interactiva y probar los endpoints (recordá usar el botón "Authorize" con tu token).

---
**Desarrollado por [Lucas Herdegen](https://github.com/LucasHerdegen)** - *Ingeniería en Sistemas de Información UTN-FRBA*
---
## Esquema de Base de Datos (Entity-Relationship)

```mermaid
erDiagram
    USER ||--o{ BOOKING : "realiza"
    SPACE ||--o{ BOOKING : "tiene"
    
    USER {
        string Id PK
        string Email
        string FirstName
        string LastName
    }

    SPACE {
        int Id PK
        string Name
        int Capacity
        bool Available
    }

    BOOKING {
        int Id PK
        string UserId FK
        int SpaceId FK
        datetime ReservationDate
    }
