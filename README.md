# dotNET.ToDo

dotNET.ToDo is a simple ASP.NET Core MVC application for managing a to-do list. It uses SQLite for data storage and provides basic CRUD operations.


### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQLite](https://www.sqlite.org/download.html)

### Running the Application

1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/dotNET.ToDo.git
    cd dotNET.ToDo
    ```

2. Restore the dependencies:
    ```sh
    dotnet restore
    ```

3. Update the database connection string in `ToDo/Controllers/HomeController.cs` if necessary:
    ```csharp
    new SqliteConnection("Data Source=db.sqlite")
    ```

4. Run the application:
    ```sh
    dotnet run --project ToDo/ToDo.csproj
    ```

5. Open your browser and navigate to `http://localhost:5001` or `https://localhost:5001`.

### Project Configuration

The project configuration is managed through the `launchSettings.json` file located in the `ToDo/Properties/` directory. This file contains settings for different profiles, including HTTP and HTTPS configurations.

### CRUD Operations

The `HomeController` class in `ToDo/Controllers/HomeController.cs` provides the following CRUD operations:

- **Create**: `Insert(ToDoItem toDo)`
- **Read**: `GetAllToDos()`, `GetById(int id)`
- **Update**: `Update(ToDoItem toDo)`
- **Delete**: `Delete(int id)`

### Logging

The application uses `ILogger<HomeController>` for logging purposes. The logger is injected into the `HomeController` via dependency injection.
