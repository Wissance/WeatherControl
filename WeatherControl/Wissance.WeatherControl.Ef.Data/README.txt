Using Nuget Package Manager Console you could easly create Migrations using VisualStudio.

* Prior to running any commands don't forget to select DefaultProject in PackageManagerConsole and set them as s StartUp project

1. Prior to any migrations run Update Database command to apply all migrations:
Update-Database -Context {ContextClassName}
Where:
* {ContextClassName} - is a class deriving from DbContext

Example: in our project {ContextClassName} class is a ModelContext, therefore this command looks like:
Update-Database -Context ModelContext

2. Migration could be generated as follows:
Add-Migration {name} -Context {ContextClassName} -Output {ProjectMigrationDir}
Where:
* {name} - is a name of migration
* {ContextClassName} - is a class deriving from DbContext
* {ProjectMigrationDir} - directory where all migrations will be saved (this should be same direcoty for all migrations)

Example: Consider that we genering `Migration_1_Initial` in Migrations Project directory:
Add-Migration Migration_1_Initial -Context ModelContext -Output Migrations

3. Migration could be removed
3.1 To remove Last migration do following: Remove-Migration -Context ModelContext
