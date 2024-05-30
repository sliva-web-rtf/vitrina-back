## Required software

- Visual Studio 2022 (https://www.visualstudio.com/downloads/download-visual-studio-vs.aspx) or JetBrains Rider
- .NET SDK 7 (https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- git

## Project initialization

1. Copy `src\Saritasa.RedMan.Web\appsettings.json.template` file to `src\Saritasa.RedMan.Web\appsettings.Development.json`

2. Update the `ConnectionStrings:AppDatabase` setting in that file to target your local development server/database

3. Create a initial migration by using this command:
   
```
dotnet ef migrations add IntialCreate -s Saritasa.RedMan.Web -p Saritasa.RedMan.Infrastructure.DataAccess
```

The database will be created automatically upon application start (if it does not exist yet).
