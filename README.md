# CompanyName.ServiceName
ASP.NET Core service built using WebAPI and MVC.

## Used technologies, tools, frameworks and libraries
- [Visual Studio 2017](https://visualstudio.microsoft.com)
- [.NET Core SDK 2.2.203](https://dotnet.microsoft.com/download/dotnet-core/2.2)
- [ASP.NET Core 2.1](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.1)
- [Automapper](https://docs.automapper.org/en/stable/Dependency-injection.html#asp-net-core)
- [FluentValidation](https://fluentvalidation.net/aspnet#asp-net-core)
- [Microsoft.EntityFrameworkCore](https://docs.microsoft.com/en-us/ef/core/)[.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer)
- [Refit](https://github.com/reactiveui/refit#using-httpclientfactory)
- [Serilog](https://github.com/serilog)
- [StyleCop.Analyzers](https://www.nuget.org/packages/StyleCop.Analyzers)
- [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore#getting-started)
- [SQL Server 2017 Developer edition](https://www.microsoft.com/en-us/sql-server/developer-get-started)

## How to run
Install [Visual Studio 2017 Community](https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&rel=15) [.NET Core SDK 2.2.203](https://dotnet.microsoft.com/download/dotnet-core/2.2) and [SQL Server 2017 Developer edition](https://www.microsoft.com/en-us/sql-server/developer-get-started). Clone repo:
```sh
git clone https://github.com/Rukastyi/CompanyName.ServiceName
```
In `./src/CompanyName.ServiceName.Api/appsettings.Development.json` change `ConnectionStrings`->`DefaultConnection` connection string to your valid accessable SQL Server's instance. Build and run `CompanyName.ServiceName.Api` and `CompanyName.ServiceName.WebApp` (yes, both simultaneously).