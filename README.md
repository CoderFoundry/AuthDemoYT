# AuthDemoYT - Blazor Authorization Demonstration

This repository demonstrates how to use built-in authentication and authorization features in .NET Blazor applications.

## Overview

Specifically, this project highlights:

- Securing pages and components using the `[Authorize]` attribute.
- Conditionally rendering content with the `AuthorizeView` component based on user authentication status.
- Integration with the built-in ASP.NET Core Identity and authentication system.

## Features

- Examples of secured Razor components.
- Conditional UI rendering based on user authentication.
- Easy-to-follow authentication logic implementation.

## Video Tutorial

Watch the detailed video walkthrough:

📺 [![Watch on YouTube](https://img.youtube.com/vi/XNk3SxbhVa0/maxresdefault.jpg)](https://www.youtube.com/watch?v=XNk3SxbhVa0)

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/CoderFoundry/AuthDemoYT.git
```

### Run the Application

1. Open the solution in Visual Studio or your preferred IDE.
2. Build and run the project.
3. The application will automatically:
   - Create a SQLite database named `AuthDemoApp.db`.
   - Run migrations to configure ASP.NET Identity.
   - Set up two default users for immediate testing.

## Configuration

Modify the settings in `appsettings.json` if needed:

```json
"ConnectionStrings": {
  "DbConnection": "Data Source=AuthDemoApp.db"
},
"DefaultPassword": "Password123@"
```

- **`DbConnection`**: Set a custom database file or connection string.
- **`DefaultPassword`**: Change the default password for the created users.

> **Important:** The `DbConnection` setting is required unless you're modifying the database connection handling logic in the code.


## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- Visual Studio or Visual Studio Code

## Contributing

Contributions via pull requests or issue submissions are welcome!

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE) for more details.
