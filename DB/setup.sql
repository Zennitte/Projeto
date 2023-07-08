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
    Username VARCHAR(255) NOT NULL,
    PASSWORD VARCHAR(255) NOT NULL,
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
    FOREIGN KEY (DebbitedAccount) REFERENCES Accounts(Id),
    FOREIGN KEY (CreditedAccount) REFERENCES Accounts(Id)
);