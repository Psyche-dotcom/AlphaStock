# Use the official ASP.NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Set the PORT environment variable
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

# Use the official ASP.NET Core SDK as a build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /build

# Copy the project files and restore dependencies
COPY ["AlpaStock.Api/AlpaStock.Api.csproj", "AlpaStock.Api/"]
COPY ["AlpaStock.Core/AlpaStock.Core.csproj", "AlpaStock.Core/"]
COPY ["AlpaStock.Infrastructure/AlpaStock.Infrastructure.csproj", "AlpaStock.Infrastructure/"]
RUN dotnet restore "AlpaStock.Api/AlpaStock.Api.csproj"

# Copy the rest of the files and build the project
COPY . .
WORKDIR "/build/AlpaStock.Api"
RUN dotnet build "AlpaStock.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the project
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AlpaStock.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Create the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy entrypoint script
COPY entrypoint.sh .
RUN chmod +x ./entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]
