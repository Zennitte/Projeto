IF NOT EXISTS(
    SELECT *
    FROM sys.databases
    WHERE name = 'DbProjeto'
) BEGIN CREATE DATABASE DbProjeto;
END
GO
USE DbProjeto;
CREATE TABLE Users(
    Id CHAR(32) PRIMARY KEY NOT NULL,
    Username VARCHAR(255) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
);
GO
CREATE TABLE Accounts(
    Id CHAR(32) PRIMARY KEY NOT NULL,
    UserId CHAR(32) NOT NULL,
    Balance DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
GO
CREATE TABLE Transactions(
    Id CHAR(32) PRIMARY KEY NOT NULL,
    DebbitedAccount CHAR(32) NOT NULL,
    CreditedAccount CHAR(32) NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    CreatedAt DATETIME NOT NULL,
    FOREIGN KEY (DebbitedAccount) REFERENCES Accounts(Id),
    FOREIGN KEY (CreditedAccount) REFERENCES Accounts(Id)
);
GO

INSERT INTO Users(Id, Username, Password)
VALUES('e1066fedd1b14c98b2a3ec40d02708a9', 'Kaue', '12345'), ('218a9855448c48f28326123c9482cf42', 'Lucas', '54321');
GO

INSERT INTO Accounts (Id, UserId, Balance)
VALUES('e1ad7e33a2544698b56923cd52b7a9f1', '218a9855448c48f28326123c9482cf42', 100.00), ('a2ffd0f658024d1db8cf56d64c4c9d34', 'e1066fedd1b14c98b2a3ec40d02708a9', 100.00);
GO