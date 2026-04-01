# 🌤️ Weather System API - .NET Core

Sistema completo de gestión meteorológica con operaciones CRUD usando ASP.NET Core Web API, Entity Framework Core y SQLite.

## 📋 Requisitos Previos

- **.NET SDK 8.0** o superior
- **Git** (opcional, para control de versiones)
- **Editor de código** (Visual Studio Code, Visual Studio 2022, o Rider)

### Verificar instalación de .NET
```bash
dotnet --version
# Debería mostrar: 8.0.x o superior

# Crear un nuevo proyecto Web API
dotnet new webapi -n WeatherApi

# Navegar a la carpeta del proyecto
cd WeatherApi

# Entity Framework Core para SQLite
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design

# Swagger para documentación de API
dotnet add package Swashbuckle.AspNetCore

# Crear las carpetas necesarias
mkdir Controllers
mkdir Models
mkdir Data

# Crear la migración inicial
dotnet ef migrations add InitialCreate

# Aplicar la migración a la base de datos
dotnet ef database update

# Listar migraciones
dotnet ef migrations list

# Ver el estado de la base de datos
dotnet ef dbcontext info

# Restaurar paquetes
dotnet restore

# Compilar el proyecto
dotnet build

# Ejecutar la aplicación
dotnet run

# GET - Obtener todos los registros
curl https://localhost:5001/api/weather

# GET - Obtener por ciudad
curl https://localhost:5001/api/weather/city/Madrid

# POST - Crear nuevo registro
curl -X POST https://localhost:5001/api/weather \
  -H "Content-Type: application/json" \
  -d '{
    "city": "Lima",
    "temperature": 25.5,
    "feelsLike": 26.0,
    "description": "Soleado",
    "humidity": 70,
    "windSpeed": 10.5,
    "pressure": 1013,
    "icon": "☀️"
  }'

# PUT - Actualizar registro
curl -X PUT https://localhost:5001/api/weather/1 \
  -H "Content-Type: application/json" \
  -d '{
    "id": 1,
    "city": "Madrid",
    "temperature": 23.0,
    "feelsLike": 23.5,
    "description": "Parcialmente nublado",
    "humidity": 50,
    "windSpeed": 11.0,
    "pressure": 1014,
    "icon": "⛅"
  }'

# DELETE - Eliminar registro
curl -X DELETE https://localhost:5001/api/weather/1

# Verificar permisos de escritura
chmod 755 .

# Eliminar y recrear la base de datos
rm -f weather.db
dotnet ef database update
