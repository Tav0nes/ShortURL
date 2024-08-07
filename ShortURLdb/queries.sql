SELECT * FROM testdb.Urls;
SELECT COUNT(*) FROM testdb.Urls;


DROP TABLE testdb.Urls;

CREATE TABLE IF NOT EXISTS testdb.Urls (
    OriginalURL VARCHAR(255) PRIMARY KEY,
    ShortenedUrl VARCHAR(100),
    DateCreated DATETIME,
    CreatedBy VARCHAR(100)
);

INSERT INTO testdb.Urls (OriginalURL, ShortenedUrl, DateCreated, CreatedBy) VALUES
    (
        'https://www.example.com', 
        'https://exmpl.com/1', 
        NOW(), 
        'user1'
    ),
    (
        'https://www.anotherexample.com', 
        'https://anexmpl.com/2', 
        NOW(), 
        'user2'
    );
SHOW CREATE TABLE testdb.Urls;
