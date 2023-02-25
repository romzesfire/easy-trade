using System.Text.Json.Serialization;
using EasyTrade.API;
using EasyTrade.API.Configuration;
using EasyTrade.API.Extension;
using EasyTrade.API.Validation;
using EasyTrade.DAL.Configuration;
using EasyTrade.DAL.DatabaseContext;
using EasyTrade.DTO.Abstractions;
using EasyTrade.DTO.Model;
using EasyTrade.Service.Configuration;
using EasyTrade.Service.Extensions;
using EasyTrade.Service.Services;
using Microsoft.EntityFrameworkCore;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var startApp = new Startup(configuration);
startApp.CreateBuilder();
startApp.AddServices();
startApp.Build();
startApp.AddMiddleware();
startApp.Run();