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
CREATE TABLE gamegenre (game INT FOREIGN KEY REFERENCES games (gameid), 
genre INT FOREIGN KEY REFERENCES genres (genreid), PRIMARY KEY (game, genre))
CREATE TABLE subscriptions (who INT FOREIGN KEY REFERENCES users (userid), 
game INT FOREIGN KEY REFERENCES games (gameid), rate tinyint check(rate >= 0 AND rate <= 10), 
PRIMARY KEY (who, game))
-- добавить начальные данные
INSERT hierarchy VALUES ('admin'), ('player'), ('banned')
INSERT users (username, authority, passwd) VALUES ('admin', 1, '')