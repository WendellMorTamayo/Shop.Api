using Microsoft.EntityFrameworkCore;

namespace Shop.Api.Data;

public static class DataExtension
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DataStoreContext>();
        await dbContext.Database.MigrateAsync();
    }
}
