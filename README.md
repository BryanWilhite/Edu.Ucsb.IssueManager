# Edu.Ucsb.IssueManager

## a very simple prototype 🚧 for an issue management system

This prototype is an ASP.NET Core app based on the guidance from Microsoft in “[Introduction to Identity on ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-3.1&tabs=netcore-cli).”

When the root folder of this repo was empty we started with:

```console
dotnet tool install --global dotnet-ef
dotnet new webapp --auth Individual -uld -o Edu.Ucla.IssueManager.Web
```

For users on platforms that do not support `LocalDb`, edit the `Edu.Ucla.IssueManager.Web/appsettings.json` JSON file, changing `ConnectionStrings.DefaultConnection` to something like this:

```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=Edu.Ucla.IssueManager.Web;MultipleActiveResultSets=true;User id=your-user;Password=your-password;"
  },
```

When a SQL Server connection is configured then run migrations:

```console
dotnet ef database update --project ./Edu.Ucla.IssueManager.Web
```

## related links

- “[Entity Framework Core tools reference—.NET CLI](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet)”
- “[Introduction to Identity on ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-3.1&tabs=netcore-cli)”

@[BryanWilhite](https://twitter.com/BryanWilhite)
