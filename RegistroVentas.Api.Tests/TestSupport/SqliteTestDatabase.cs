using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RegistroVentas.Api.Data;

namespace RegistroVentas.Api.Tests.TestSupport;

public sealed class SqliteTestDatabase : IDisposable
{
    private readonly SqliteConnection _connection;

    public RegistroDbContext DbContext { get; }

    public SqliteTestDatabase()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<RegistroDbContext>()
            .UseSqlite(_connection)
            .Options;

        DbContext = new RegistroDbContext(options);

        DbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        DbContext.Dispose();
        _connection.Dispose();
    }
}