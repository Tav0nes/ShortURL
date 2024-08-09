FROM mysql:8.0.39

# Add your custom configuration or initialization scripts here
COPY ./init.sql /docker-entrypoint-initdb.d/

EXPOSE 3306