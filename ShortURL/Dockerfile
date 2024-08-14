# Stage 1: Build the Blazor WebAssembly app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files and restore dependencies
COPY ["ShortURL/ShortURL.csproj", "ShortURL/"]
COPY ["SharedModels/SharedModels.csproj", "SharedModels/"]
RUN dotnet restore "ShortURL/ShortURL.csproj"

# Copy the rest of the source code and build the app
COPY ShortURL/ ./ShortURL/
COPY SharedModels/ ./SharedModels/
WORKDIR "/src/ShortURL"
RUN dotnet publish "ShortURL.csproj" -c Release -o /app/publish

# Stage 2: Serve the Blazor WebAssembly app using Nginx
FROM nginx:alpine AS base
WORKDIR /usr/share/nginx/html

# Remove default Nginx content
RUN rm -rf ./*

# Copy the published files from the Blazor project to the Nginx html directory
COPY --from=build /app/publish/wwwroot .

# Set proper permissions to ensure Nginx can serve the files
RUN chmod -R 755 /usr/share/nginx/html

# Copy SSL certificates (assuming they are in ShortURL/certs)
COPY ShortURL/certs/nginx.crt /etc/ssl/certs/nginx.crt
COPY ShortURL/certs/nginx.key /etc/ssl/private/nginx.key

# Configure Nginx to serve the Blazor WebAssembly app over HTTP and HTTPS
RUN echo 'server { \
    listen 80; \
    listen 443 ssl; \
    ssl_certificate /etc/ssl/certs/nginx.crt; \
    ssl_certificate_key /etc/ssl/private/nginx.key; \
    location / { \
        root /usr/share/nginx/html; \
        index index.html; \
        try_files $uri $uri/ /index.html; \
    } \
}' > /etc/nginx/conf.d/default.conf

# Expose HTTP and HTTPS ports
EXPOSE 80
EXPOSE 443

# Run Nginx
CMD ["nginx", "-g", "daemon off;"]
