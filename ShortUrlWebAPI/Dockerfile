##See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER app
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["ShortUrlWebAPI.csproj", "."]
#RUN dotnet restore "./ShortUrlWebAPI.csproj"
#COPY . .
#WORKDIR "/src/."
#RUN dotnet build "./ShortUrlWebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./ShortUrlWebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "ShortUrlWebAPI.dll"]
# Use .NET SDK image to build the application

# Use .NET SDK image to build the application
# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ShortUrlWebAPI/ShortUrlWebAPI.csproj", "ShortUrlWebAPI/"]
COPY ["SharedModels/SharedModels.csproj", "SharedModels/"]
RUN dotnet restore "ShortUrlWebAPI/ShortUrlWebAPI.csproj"
COPY . .
WORKDIR "/src/ShortUrlWebAPI"
RUN dotnet build "ShortUrlWebAPI.csproj" -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish "ShortUrlWebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Copy the published app
COPY --from=publish /app/publish .

# Copy the HTTPS certificate
COPY .aspnet/https/aspnetapp.pfx /https/aspnetapp.pfx

# Set environment variables for the certificate
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=yourpassword

# Expose ports for HTTP and HTTPS
EXPOSE 80
EXPOSE 443

# Run the application
ENTRYPOINT ["dotnet", "ShortUrlWebAPI.dll"]
