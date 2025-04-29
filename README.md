# Pigeon Post API

An ASP .NET Core Web API for managing a pigeon-based postal network. Track roosts, pigeons, messages, and deliveries with PostgreSQL and Entity Framework Core.

## 📦 Features

- **Roosts**: Create, read, update, delete pigeon roosts
- **Pigeons**: Manage pigeon profiles, assign to roosts
- **Messages**: Compose and timestamp pigeon messages
- **Deliveries**: Dispatch and deliver messages via pigeons (with status & timestamps)
- **Swagger/OpenAPI**: Interactive API documentation at `/openapi/ui`

## 🛠 Tech Stack

- [.NET 9.0](https://dotnet.microsoft.com/) (ASP .NET Core Web API)
- [Entity Framework Core 9.0](https://docs.microsoft.com/ef/core/) with Npgsql provider
- [PostgreSQL](https://www.postgresql.org/)
- C# 11, Nullable Reference Types

## 🚀 Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download)
- `dotnet-ef` CLI tool:  `dotnet tool install --global dotnet-ef`
- Git

### Installation

1. **Clone the repo**
   ```bash
   git clone https://github.com/elias6969/PigeonPostApi.git
   cd PigeonPostApi
   ```

2. **Configure the database**
   - Create the database:
     ```bash
     createdb pigeon_post_db
     ```
   - In `appsettings.json`, update `DefaultConnection`:
     ```jsonc
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=pigeon_post_db;Username=<user>;Password=<password>"
     }
     ```

3. **Restore & Migrate**
   ```bash
   dotnet restore
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. **Run the API**
   ```bash
   dotnet run
   ```

   - Swagger UI: [http://localhost:5292/openapi/ui](http://localhost:5292/openapi/ui)
   - Weather test: [http://localhost:5292/weatherforecast](http://localhost:5292/weatherforecast)

## 📚 API Endpoints

### Roosts

| Method | Route                 | Description                |
|--------|-----------------------|----------------------------|
| GET    | `/api/roosts`         | List all roosts            |
| GET    | `/api/roosts/{id}`    | Get a specific roost       |
| POST   | `/api/roosts`         | Create a new roost         |
| PUT    | `/api/roosts/{id}`    | Update a roost             |
| DELETE | `/api/roosts/{id}`    | Delete a roost             |

### Pigeons

| Method | Route                                           | Description                          |
|--------|-------------------------------------------------|--------------------------------------|
| GET    | `/api/pigeons`                                  | List all pigeons                     |
| GET    | `/api/pigeons/{id}`                             | Get a specific pigeon                |
| POST   | `/api/pigeons`                                  | Create a new pigeon                  |
| PUT    | `/api/pigeons/{id}`                             | Update pigeon details                |
| PUT    | `/api/pigeons/{id}/assign-roost/{roostId}`      | Assign pigeon to a different roost   |
| DELETE | `/api/pigeons/{id}`                             | Remove a pigeon                      |

### Messages

| Method | Route                    | Description                 |
|--------|--------------------------|-----------------------------|
| GET    | `/api/messages`          | List all messages           |
| GET    | `/api/messages/{id}`     | Get a specific message      |
| POST   | `/api/messages`          | Create a new message        |

### Deliveries

| Method | Route                                      | Description                                |
|--------|--------------------------------------------|--------------------------------------------|
| GET    | `/api/deliveries`                          | List all deliveries                        |
| GET    | `/api/deliveries/{id}`                     | Get a specific delivery                    |
| POST   | `/api/deliveries`                          | Create a new delivery (Pending status)     |
| PUT    | `/api/deliveries/{id}/dispatch`            | Mark delivery as InFlight with timestamp   |
| PUT    | `/api/deliveries/{id}/deliver`             | Mark delivery as Delivered with timestamp  |

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/MyFeature`
3. Commit your changes: `git commit -m "Add awesome feature"`
4. Push to branch: `git push origin feature/MyFeature`
5. Open a Pull Request
