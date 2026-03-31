using Microsoft.EntityFrameworkCore;

namespace SearchService.Data;

public class DBInitializer
{
    public static async void Initialize(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
        var httpClient = 
  
    }
}
