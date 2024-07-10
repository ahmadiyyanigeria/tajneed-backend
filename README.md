# Overview

Welcome to the Tajneed API Project

# Details

This project is built with .Net 8.0 and Postgresql
Clean Code Architecture
- /src/Application
- /src/Domain
- /src/Infrastructure
- /src/Api
- /tests/IntegrationTests
- /tests/UnitTests

You can read more about [.Net 8.0](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8)

### 1.2 Project Configuration

Start by cloning this project on your workstation.

```sh
git clone https://github.com/ahmadiyyanigeria/tajneed-backend.git
```

## Test Framework Used

Include both unit tests and Integration tests as vital elements of your testing approach, covering all significant workflows to ensure comprehensive testing.

- We use xUnit Snapshot for Unit test
- We use xUnit and XUnit.Verify for integration test

## Development Environment

### Running on your local host (with the dotnet SDK installed)

- Ensure you have .NET SDK installed on your computer.
- Ensure to have postgresql installed on your computer.
- Update the connection string in the appsettings.development.json file to point to your local postgresql database.

```sh
# To run the application from the root of the repository
dotnet run --project src/Api/Api.csproj

# To run the application from the application folder
cd src/Api
dotnet run # or dotnet watch run to get hot reloading

# To run the tests tests project application folder. Unit tests repository data are mocked while acceptance test uses the testcontainer. Ensure to have docker running while running the acceptance tests.
dotnet test
```


