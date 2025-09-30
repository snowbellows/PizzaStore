# PizzaStore C# example

This project is created by following the "Use a database with minimal API, Entity Framework Core, and ASP.NET Core" example on [Microsoft Learn](https://learn.microsoft.com/en-us/training/modules/build-web-api-minimal-database/).

## Development

### Database

This project uses an SQLite database managed using the EFCore ORM with migrations created using the `dotnet ef` CLI tools.

#### Creating migrations

```bash
dotnet ef migrations add <MIGRATION_NAME>
```

#### Applying migrations

```bash
dotnet ef database update
```

### Running Development server

```bash
dotnet run
```
