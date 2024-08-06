CREATE TABLE IF NOT EXISTS URL (
    OriginalURL VARCHAR(255),
    ShortenedUrl VARCHAR(100),
    DateCreated DATETIME,
    CreatedBy VARCHAR(100)
);

INSERT INTO URL (OriginalURL, ShortenedUrl, DateCreated, CreatedBy) VALUES
('https://www.example.com', 'https://exmpl.com/1', NOW(), 'user1'),
('https://www.anotherexample.com', 'https://anexmpl.com/2', NOW(), 'user2');
