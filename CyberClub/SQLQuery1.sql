CREATE DATABASE CyberClub
GO
USE CyberClub -- создать таблицы
--
CREATE TABLE Users (
UserID INT PRIMARY KEY IDENTITY, UserName NVARCHAR(20) NOT NULL UNIQUE, Email NVARCHAR(64), 
About NVARCHAR(255), UserLevel INT NOT NULL, UserPass NVARCHAR(255), LastLogin DATETIME)
--
CREATE TABLE TextMessages (MessageID INT PRIMARY KEY IDENTITY, 
Sender INT FOREIGN KEY REFERENCES Users (UserID), Reciever INT FOREIGN KEY REFERENCES Users (UserID), 
ShortText NVARCHAR(50) NOT NULL, LongText NVARCHAR(255), DT DATETIME NOT NULL, IsRead BIT)
--
CREATE TABLE Images (
ImageID INT PRIMARY KEY IDENTITY, ImageName NVARCHAR(255) NOT NULL, Bin VARBINARY(MAX) NOT NULL)
--
CREATE TABLE Developers (DeveloperID INT PRIMARY KEY IDENTITY, DeveloperName NVARCHAR(50) NOT NULL UNIQUE)
--
CREATE TABLE Games (GameID INT PRIMARY KEY IDENTITY, GameName NVARCHAR(50) NOT NULL, 
Developer INT FOREIGN KEY REFERENCES Developers (DeveloperID), GameExePath NVARCHAR(255), 
GameIcon INT FOREIGN KEY REFERENCES Images (ImageID), IsSingleplayer BIT, IsMultiplayer BIT)
--
CREATE TABLE Genres (GenreID INT PRIMARY KEY IDENTITY, GenreName NVARCHAR(30) NOT NULL UNIQUE)
--
CREATE TABLE GameGenre (Game INT FOREIGN KEY REFERENCES Games (GameID) ON DELETE CASCADE, 
Genre INT FOREIGN KEY REFERENCES Genres (GenreID) ON DELETE CASCADE, PRIMARY KEY (Game, Genre))
--
CREATE TABLE Subscriptions (Subscriber INT FOREIGN KEY REFERENCES Users (UserID) ON DELETE CASCADE, 
Game INT FOREIGN KEY REFERENCES Games (GameID) ON DELETE CASCADE,
Rate TINYINT CHECK(Rate >= 0 AND Rate <= 10), PRIMARY KEY (Subscriber, Game))
-- триггеры
GO
CREATE TRIGGER Developers_GamesForget
ON Developers
INSTEAD OF DELETE
AS
    UPDATE Games SET Developer = null WHERE Developer IN (SELECT DeveloperID FROM Deleted)
    DELETE FROM Developers WHERE DeveloperID IN (SELECT DeveloperID FROM Deleted)
GO
CREATE TRIGGER Images_GamesForget
ON Images
INSTEAD OF DELETE
AS
    UPDATE Games SET GameIcon = null WHERE GameIcon IN (SELECT ImageID FROM Deleted)
    DELETE FROM Images WHERE ImageID IN (SELECT ImageID FROM Deleted)
GO
CREATE TRIGGER Users_TextMessagesForget
ON Users
INSTEAD OF DELETE
AS
    UPDATE TextMessages SET Sender = null WHERE Sender IN (SELECT UserID FROM Deleted)
    DELETE FROM Users WHERE UserID IN (SELECT UserID FROM Deleted)
GO
-- добавить начальные данные
INSERT Users (UserName, UserLevel) VALUES ('admin', 0)

        /*[Required]
        public virtual int LevelID
        {
            get => (int)Level;
            set
            {
                Level = (UserLevel)LevelID;
            }
        }
        [EnumDataType(typeof(UserLevel))]
        public virtual UserLevel Level { get; set; }*/