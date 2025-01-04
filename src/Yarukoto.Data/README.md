# Initial migration

`dotnet ef migrations add InitialCreate`

# Subsequent migrations

`dotnet ef migrations add <MigrationName>`

# Update/Initialize the database

`dotnet ef database update`

# PGSQL Configuration

```sql
CREATE USER yarukoto WITH PASSWORD 'yarukoto';
ALTER USER yarukoto createdb;
```

EntityFramework Core will add the tables to the `public` schema
