### Database Migrations

To use `dotnet-ef` for your migrations first ensure that "UseInMemoryDatabase" is disabled, as described within previous section.
Then, add the following flags to your command (values assume you are executing from repository root)

* `--project src/FinanceManager.Infrastructure` (optional if in this folder)
* `--startup-project src/FinanceManager.WebAPI`
* `--output-dir src/FinanceManager.Infrastructure/Persistence/Migrations`

The full command would look like this:
``` bash
dotnet ef migrations add InitialCreate --project src/FinanceManager.Infrastructure --startup-project src/FinanceManager.WebAPI --output-dir src/FinanceManager.Infrastructure/Persistence/Migrations
```