# Book Rental Service - Project Setup

This README outlines the steps to set up and run the Book Rental Service project locally, along with details on project assumptions and how to run tests.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Entity Framework Core Tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)

## Getting Started

### 1. Clone the Repository
Clone the repository to your local machine:
```bash
git clone https://github.com/BhopenderKumar/BookRental
cd BookRental
```

### 2. Update Connection String
In `appsettings.json` located in the `BookRental.Api` project, update the `ConnectionStrings` section to point to your SQL Server instance:

```json
"ConnectionStrings": {
  "BookRentalDatabase": "Server=YOUR_SERVER_NAME;Database=BookRentalDB;Trusted_Connection=True;"
}
```

### 3. Apply Migrations
Run Entity Framework Core migrations to create the database schema:

```bash
cd BookRental.Data
dotnet ef database update
```

### 4. Run Sample SQL Script
Go to the SQL server Management Studio and run following script:
https://github.com/BhopenderKumar/BookRental/blob/main/Sample%20Script/Sample%20Data%20Script.sql

### 5. Run the Project
To start the API, run the following command:

```bash
dotnet run
```

The API will be available at `https://localhost:44332` or `http://localhost:44332`.

User following postman collection to test the APIs
https://github.com/BhopenderKumar/BookRental/blob/main/Postman%20collection/BookRental.Api.postman_collection.json

## Assumptions

- The project assumes that SQL Server is running locally.
- Make sure to have sufficient permissions to create databases and tables.

## Running Tests

### Unit Tests

To run unit tests, navigate to the test project and use the `dotnet test` command:

```bash
cd BookRental.Tests
dotnet test
```

This will execute all the tests and display the results in the terminal.

## Additional Notes

- **Concurrency Handling**: The rental logic in the project includes concurrency handling for managing book availability.
- **DTO Mapping**: AutoMapper is used to map entities to DTOs.
  
---

This guide should cover the basic setup for getting the Book Rental Service up and running locally and explain the testing process and any assumptions made during development. Let me know if you need more details!
