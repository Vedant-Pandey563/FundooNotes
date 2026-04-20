Create Database Fundoo;

Use Fundoo;

Create Table Users
(
	Id INT Primary Key Identity(1,1),
	FirstName VARCHAR(100) NOT NULL,
	LastName VARCHAR(100) NOT NULL,
	Email NVARCHAR(255) NOT NULL UNIQUE,
	PasswordHash NVARCHAR(MAX) NOT NULL,
	CreatedAt DATETIME DEFAULT GETDATE(),
);

select * from Users;


SELECT @@SERVERNAME AS [ServerName];