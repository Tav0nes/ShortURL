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

# Create a directory for the certificate in the container
RUN mkdir /https

# Copy the server certificate PFX
COPY ShortUrlWebAPI/certs/apiserver.pfx /https/apiserver.pfx

# Copy the published app
COPY --from=publish /app/publish .

RUN apt-get update
RUN apt-get install -y curl

# Expose ports for HTTP and HTTPS
EXPOSE 80
EXPOSE 443

# Run the application
ENTRYPOINT ["dotnet", "ShortUrlWebAPI.dll"]
