CREATE DATABASE CyberClub
GO
USE CyberClub -- создать таблицы
CREATE TABLE hierarchy (authid INT PRIMARY KEY IDENTITY , authname NVARCHAR(30) NOT NULL UNIQUE)
CREATE TABLE users (userid INT PRIMARY KEY IDENTITY, username NVARCHAR(20) NOT NULL UNIQUE, 
email NVARCHAR(64), info NVARCHAR(200), authority INT FOREIGN KEY REFERENCES hierarchy (authid), 
passwd NVARCHAR(255))
CREATE TABLE feedback (messageid INT PRIMARY KEY IDENTITY, 
who INT FOREIGN KEY REFERENCES users (userid), briefly NVARCHAR(50) NOT NULL, 
indetails NVARCHAR(255), dt DATETIME NOT NULL, isread BIT)
CREATE TABLE pics (picid INT PRIMARY KEY IDENTITY, picname NVARCHAR(255) NOT NULL, bin VARBINARY(MAX) NOT NULL)
CREATE TABLE devs (devid INT PRIMARY KEY IDENTITY, devname NVARCHAR(50) NOT NULL UNIQUE)
CREATE TABLE games (gameid INT PRIMARY KEY IDENTITY, gamename NVARCHAR(50) NOT NULL, 
madeby INT FOREIGN KEY REFERENCES devs (devid), gamelink NVARCHAR(255), 
gamepic INT FOREIGN KEY REFERENCES pics (picid), singleplayer BIT, multiplayer BIT)
CREATE TABLE genres (genreid INT PRIMARY KEY IDENTITY, genrename NVARCHAR(30) NOT NULL UNIQUE)
CREATE TABLE gamegenre (game INT FOREIGN KEY REFERENCES games (gameid) ON DELETE CASCADE, 
genre INT FOREIGN KEY REFERENCES genres (genreid) ON DELETE CASCADE, PRIMARY KEY (game, genre))
CREATE TABLE subscriptions (who INT FOREIGN KEY REFERENCES users (userid) ON DELETE CASCADE, 
game INT FOREIGN KEY REFERENCES games (gameid) ON DELETE CASCADE,
rate tinyint check(rate >= 0 AND rate <= 10), PRIMARY KEY (who, game))
-- триггеры
GO
CREATE TRIGGER Devs_GamesForget
ON devs
INSTEAD OF DELETE
AS
    UPDATE games SET madeby = null WHERE madeby IN (SELECT devid FROM deleted)
    DELETE FROM devs WHERE devid IN (SELECT devid FROM deleted)
GO
CREATE TRIGGER Pics_GamesForget
ON pics
INSTEAD OF DELETE
AS
    UPDATE games SET gamepic = null WHERE gamepic IN (SELECT picid FROM deleted)
    DELETE FROM pics WHERE picid IN (SELECT picid FROM deleted)
GO/*
CREATE TRIGGER Users_FeedbackForget
ON users
INSTEAD OF DELETE
AS
    UPDATE feedback SET who = null WHERE who IN (SELECT who FROM deleted)
    DELETE FROM users WHERE userid IN (SELECT userid FROM deleted)
*/GO
-- добавить начальные данные
INSERT hierarchy VALUES ('admin'), ('player'), ('banned')
INSERT users (username, authority, passwd) VALUES ('admin', 1, '')
