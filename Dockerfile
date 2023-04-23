FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EasyTrade.API/EasyTrade.API.csproj", "EasyTrade.API/"]
COPY ["EasyTrade.DAL/EasyTrade.DAL.csproj", "EasyTrade.DAL/"]
COPY ["EasyTrade.Domain/EasyTrade.Domain.csproj", "EasyTrade.Domain/"]
COPY ["EasyTrade.DTO/EasyTrade.DTO.csproj", "EasyTrade.DTO/"]
COPY ["EasyTrade.Service/EasyTrade.Service.csproj", "EasyTrade.Service/"]
COPY ["EasyTrade.Repositories/EasyTrade.Repositories.csproj", "EasyTrade.Repositories/"]
RUN dotnet restore "EasyTrade.API/EasyTrade.API.csproj"
COPY . .
WORKDIR "/src/EasyTrade.API"
RUN dotnet build "EasyTrade.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EasyTrade.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasyTrade.API.dll"]
