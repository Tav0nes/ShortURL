CREATE DATABASE IF NOT EXISTS testdb;
USE testdb;

CREATE TABLE IF NOT EXISTS Urls (
    OriginalURL VARCHAR(255) PRIMARY KEY,
    ShortenedUrl VARCHAR(100),
    DateCreated DATETIME,
    CreatedBy VARCHAR(100)
);

INSERT INTO Urls (OriginalURL, ShortenedUrl, DateCreated, CreatedBy) VALUES
('https://www.example.com', 'https://exmpl.com/1', NOW(), 'user1'),
('https://www.anotherexample.com', 'https://anexmpl.com/2', NOW(), 'user2');
