using EasyTrade.DAL.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class SampleContextFactory : IDesignTimeDbContextFactory<EasyTradeDbContext>
{
    public EasyTradeDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EasyTradeDbContext>();

        ConfigurationBuilder builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json");
        IConfigurationRoot config = builder.Build();

        string connectionString = config.GetSection("Database:ConnectionString").Value;
        optionsBuilder.UseNpgsql(connectionString);
        return new EasyTradeDbContext(optionsBuilder.Options);
    }
}