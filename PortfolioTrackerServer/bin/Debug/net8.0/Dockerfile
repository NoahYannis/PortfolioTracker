#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["PortfolioTrackerClient/PortfolioTrackerClient.csproj", "PortfolioTrackerClient/"]
COPY ["PortfolioTrackerServer.csproj", "PortfolioTrackerServer/"]
COPY ["PortfolioTrackerShared/PortfolioTrackerShared.csproj", "PortfolioTrackerShared/"]

RUN dotnet restore "./PortfolioTrackerServer/PortfolioTrackerServer.csproj"

COPY . .

WORKDIR "/src/PortfolioTrackerServer"
RUN dotnet build "./PortfolioTrackerServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release

WORKDIR "/src/PortfolioTrackerServer"
RUN dotnet publish "./PortfolioTrackerServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "PortfolioTrackerServer.dll"]
