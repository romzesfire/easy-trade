using EasyTrade.DAL.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class SampleContextFactory : IDesignTimeDbContextFactory<EasyTradeDbContext>
{
    public EasyTradeDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EasyTradeDbContext>();
 
        // получаем конфигурацию из файла appsettings.json
        ConfigurationBuilder builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json");
        IConfigurationRoot config = builder.Build();
 
        // получаем строку подключения из файла appsettings.json
        string connectionString = config.GetSection("Database:ConnectionString").Value;
        optionsBuilder.UseNpgsql(connectionString);
        return new EasyTradeDbContext(optionsBuilder.Options);
    }
}