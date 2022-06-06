#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/FinanceManager.WebAPI/FinanceManager.WebAPI.csproj", "FinanceManager.WebAPI/"]
COPY ["src/FinanceManager.Infrastructure/FinanceManager.Infrastructure.csproj", "FinanceManager.Infrastructure/"]
RUN dotnet restore "FinanceManager.WebAPI/FinanceManager.WebAPI.csproj"
COPY src /src
WORKDIR "/src/FinanceManager.WebAPI"
RUN dotnet build "FinanceManager.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FinanceManager.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinanceManager.WebAPI.dll"]