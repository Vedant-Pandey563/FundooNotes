USE Fundoo;
GO

CREATE TABLE Collaborators
(
    Id INT PRIMARY KEY IDENTITY(1,1),

    NoteId NVARCHAR(50) NOT NULL,   -- Mongo Note Id (string)

    OwnerUserId INT NOT NULL,       -- who created the note

    CollaboratorUserId INT NOT NULL, -- shared user

    CreatedAt DATETIME2 DEFAULT GETDATE()
);

select * from Collaborators;