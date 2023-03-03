using EasyTrade.API;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var startApp = new Startup(configuration);
startApp.CreateBuilder();
startApp.AddServices();
startApp.Build();
startApp.AddMiddleware();
startApp.Run();