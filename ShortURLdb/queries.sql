SELECT * FROM testdb.URL;
SELECT COUNT(*) FROM testdb.URL;


DROP TABLE testdb.URL;

CREATE TABLE IF NOT EXISTS testdb.URL (
    OriginalURL VARCHAR(255) PRIMARY KEY,
    ShortenedUrl VARCHAR(100),
    DateCreated DATETIME,
    CreatedBy VARCHAR(100)
);

INSERT INTO testdb.URL (OriginalURL, ShortenedUrl, DateCreated, CreatedBy) VALUES
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
