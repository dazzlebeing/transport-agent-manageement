# TMS Application Setup Guide

This guide provides instructions for setting up and running the TMS application using either Docker or XAMPP.

## Prerequisites

- Docker and Docker Compose (for Docker setup)
- XAMPP (for XAMPP setup)
- .NET SDK (for running the application)

## Option 1: Using Docker (Recommended)

### Step 1: Start MySQL Container
1. Open a terminal in the project root directory
2. Run the following command to start MySQL:
```bash
docker-compose up -d
```
3. Verify the container is running:
```bash
docker ps
```

### Step 2: Configure Application
1. Update your application's connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=tms_db;User=tms_user;Password=tms_password;"
  }
}
```

### Step 3: Run the Application
1. Start your .NET application:
```bash
dotnet run
```

## Option 2: Using XAMPP

### Step 1: Start XAMPP Services
1. Open XAMPP Control Panel
2. Start Apache and MySQL services
3. Click on "Admin" next to MySQL to open phpMyAdmin

### Step 2: Create Database
1. In phpMyAdmin, create a new database named `tms_db`
2. Create a new user with the following credentials:
   - Username: tms_user
   - Password: tms_password
3. Grant all privileges to the user on the `tms_db` database

### Step 3: Configure Application
1. Update your application's connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=tms_db;User=tms_user;Password=tms_password;"
  }
}
```

### Step 4: Run the Application
1. Start your .NET application:
```bash
dotnet run
```

## Switching Between Docker and XAMPP

- When using Docker, make sure XAMPP's MySQL service is stopped to avoid port conflicts
- When using XAMPP, make sure the Docker MySQL container is stopped
- The connection string remains the same for both setups

## Troubleshooting

### Docker Issues
1. If you can't connect to MySQL:
   - Check if the container is running: `docker ps`
   - Check container logs: `docker logs tms_mysql`
   - Ensure port 3306 is not in use by another service

### XAMPP Issues
1. If MySQL won't start:
   - Check if port 3306 is already in use
   - Check XAMPP logs in the XAMPP Control Panel
   - Ensure you have proper permissions

## Database Management

### Docker
- Access MySQL CLI:
```bash
docker exec -it tms_mysql mysql -u tms_user -p
```

### XAMPP
- Use phpMyAdmin at http://localhost/phpmyadmin
- Or use MySQL CLI from XAMPP's MySQL bin directory

## Notes
- Both setups use the same port (3306) and credentials
- Data persistence is handled automatically in both cases
- Docker setup is recommended for better isolation and easier deployment 