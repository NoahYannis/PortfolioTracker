#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["PortfolioTrackerClient/PortfolioTrackerClient.csproj", "PortfolioTrackerClient/"]
RUN dotnet restore "./PortfolioTrackerClient/PortfolioTrackerClient.csproj"

COPY ["PortfolioTrackerServer/PortfolioTrackerServer.csproj", "PortfolioTrackerServer/"]
RUN dotnet restore "./PortfolioTrackerServer/PortfolioTrackerServer.csproj"

COPY ["PortfolioTrackerShared/PortfolioTrackerShared.csproj", "PortfolioTrackerShared/"]
RUN dotnet restore "./PortfolioTrackerShared/PortfolioTrackerShared.csproj"


COPY . .

WORKDIR "/src/PortfolioTrackerClient"
RUN dotnet build "./PortfolioTrackerClient.csproj" -c $BUILD_CONFIGURATION -o /app/build-client

WORKDIR "/src/PortfolioTrackerServer"
RUN dotnet build "./PortfolioTrackerServer.csproj" -c $BUILD_CONFIGURATION -o /app/build-server


WORKDIR "/src/PortfolioTrackerShared"
RUN dotnet build "./PortfolioTrackerShared.csproj" -c $BUILD_CONFIGURATION -o /app/build-shared

FROM build AS publish
ARG BUILD_CONFIGURATION=Release

WORKDIR "/src/PortfolioTrackerClient"
RUN dotnet publish "./PortfolioTrackerClient.csproj" -c $BUILD_CONFIGURATION -o /app/publish-client

WORKDIR "/src/PortfolioTrackerServer"
RUN dotnet publish "./PortfolioTrackerServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish-server

WORKDIR "/src/PortfolioTrackerShared"
RUN dotnet publish "./PortfolioTrackerShared.csproj" -c $BUILD_CONFIGURATION -o /app/publish-shared

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish-client .
COPY --from=publish /app/publish-server .
COPY --from=publish /app/publish-shared .

ENTRYPOINT ["dotnet", "PortfolioTrackerServer.dll"]
