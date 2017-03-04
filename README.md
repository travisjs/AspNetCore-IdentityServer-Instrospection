# AspNetCore-IdentityServer-Instrospection
AspNetCore-IdentityServer-Instrospection

Answer to StackOverflow Question:
http://stackoverflow.com/questions/42495331/identityserver4-introspection-endpoint-api-uses-invalid-hashing-algorithm

To add EF Migrations in Visual Studio 2017 RC:

```
Add-Migration InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
Add-Migration InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb
Update-Database -c PersistedGrantDbContext
Update-Database -c ConfigurationDbContext
```