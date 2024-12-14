# Product Api

This project was generated using .NET8 and Clean Architeture Pattern.

## Prerequisites
Ensure the following are installed:

- .NET 8 SDK: Download .NET 8
- SQL Server: For database management.
- Visual Studio 2022 or Visual Studio Code: IDE for development.
- Git: Version control system.

## Setup Instructions
Follow these steps to set up and run the project:

1. Clone the github
   
```bash
git clone https://github.com/roshanjayawardena/product-api.git
cd your-repo-name
```
2. Configure the Database
   
Update the appsettings.json file with your database connection string:

```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=ProductDB;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
}
```
3. Run the Project
   
Open Visual Studio 2022 and build and run the project.
Once the server is running, autimatically navigate to `https://localhost:7119/swagger/index.html`.The application will automatically seed the users and roles and pending migrations.

## Technologies
- C#
- .NET 8 Web API
- LINQ
- Entityframework Core
- Asp.NET Identity
- JWT Token based Authentication
- AutoMapper
- SQL Server
- Swagger/OpenAPI
