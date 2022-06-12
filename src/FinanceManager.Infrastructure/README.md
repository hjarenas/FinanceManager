# Database Migrations

## Generating the migrations
To use `dotnet-ef` for your migrations first ensure that "UseInMemoryDatabase" is disabled, as described within previous section.
Then, add the following flags to your command (values assume you are executing from repository root)

* `--project src/FinanceManager.Infrastructure` (optional if in this folder)
* `--startup-project src/FinanceManager.WebAPI`
* `--output-dir Persistence/Migrations`

The full command would look like this:
``` bash
dotnet ef migrations add InitialCreate --project src/FinanceManager.Infrastructure --startup-project src/FinanceManager.WebAPI --output-dir Persistence/Migrations
```
## Applying the migrations
According to [Microsoft](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying) the recommended way
is to generate the SQL scripts with the cli tool.

It is possible to apply the migrations using the command line tool though. In order to apply the migrations,
we only need to run the following command:
``` bash
dotnet ef database update --project src/FinanceManager.WebAPI/
```