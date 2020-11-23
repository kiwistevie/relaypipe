IF OBJECT_ID('dbo.Entry', 'U') IS NOT NULL DROP TABLE dbo.Entry;
IF OBJECT_ID('dbo.PointChannel', 'U') IS NOT NULL DROP TABLE dbo.PointChannel;
IF OBJECT_ID('dbo.Channel', 'U') IS NOT NULL DROP TABLE dbo.Channel;
IF OBJECT_ID('dbo.SessionPoint', 'U') IS NOT NULL DROP TABLE dbo.SessionPoint;
IF OBJECT_ID('dbo.SessionUser', 'U') IS NOT NULL DROP TABLE dbo.SessionUser;
IF OBJECT_ID('dbo.Session', 'U') IS NOT NULL DROP TABLE dbo.Session;
IF OBJECT_ID('dbo.Tag', 'U') IS NOT NULL DROP TABLE dbo.Tag;
IF OBJECT_ID('dbo.EnUserPointResponsibilitytry', 'U') IS NOT NULL DROP TABLE dbo.UserPointResponsibility;
IF OBJECT_ID('dbo.GroupPoint', 'U') IS NOT NULL DROP TABLE dbo.GroupPoint;
IF OBJECT_ID('dbo.UserPoint', 'U') IS NOT NULL DROP TABLE dbo.UserPoint;
IF OBJECT_ID('dbo.Point', 'U') IS NOT NULL DROP TABLE dbo.Point;
IF OBJECT_ID('dbo.Status', 'U') IS NOT NULL DROP TABLE dbo.Status;
IF OBJECT_ID('dbo.GroupTopic', 'U') IS NOT NULL DROP TABLE dbo.GroupTopic;
IF OBJECT_ID('dbo.UserTopic', 'U') IS NOT NULL DROP TABLE dbo.UserTopic;
IF OBJECT_ID('dbo.Topic', 'U') IS NOT NULL DROP TABLE dbo.Topic;
IF OBJECT_ID('dbo.TopicGroup', 'U') IS NOT NULL DROP TABLE dbo.TopicGroup;
IF OBJECT_ID('dbo.UserGroup', 'U') IS NOT NULL DROP TABLE dbo.UserGroup;
IF OBJECT_ID('dbo.Groups', 'U') IS NOT NULL DROP TABLE dbo.Groups;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;

CREATE TABLE Users (
    Id INT IDENTITY(1,1) NOT NULL,
    FirstName NVARCHAR(60),
    LastName NVARCHAR(60),
    UserName NVARCHAR(60),
    Active BIT DEFAULT 1,
    PRIMARY KEY (Id)
);

CREATE TABLE Groups (
    Id INT IDENTITY(1,1) NOT NULL,
    Title NVARCHAR(60),
    PRIMARY KEY (Id)
);

CREATE TABLE UserGroup (
    UserId INT NOT NULL,
    GroupId INT NOT NULL,
    PRIMARY KEY (UserId, GroupId),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (GroupId) REFERENCES Groups(Id)
);

CREATE TABLE TopicGroup (
    Id INT IDENTITY(1,1) NOT NULL,
    Title NVARCHAR(30),
    ImageUri NVARCHAR(80),
    PRIMARY KEY (Id)
);

CREATE TABLE Topic (
    Id INT IDENTITY(1,1) NOT NULL,
    CreatedAt DATETIME NOT NULL,
    CreatedBy INT NOT NULL,
    ChangedAt DATETIME NULL,
    ChangedBy INT NULL,
    Title NVARCHAR(60) NOT NULL,
    GroupId INT NOT NULL,
    Active BIT NOT NULL DEFAULT 1,
    DoneDate DATETIME NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY (GroupId) REFERENCES TopicGroup(Id),
    FOREIGN KEY (CreatedBy) REFERENCES Users(Id),
    FOREIGN KEY (ChangedBy) REFERENCES Users(Id)
);

CREATE TABLE UserTopic (
    UserId INT NOT NULL,
    TopicId INT NOT NULL,
    Permission NVARCHAR(3) NULL,
    PRIMARY KEY (UserId, TopicId),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (TopicId) REFERENCES Topic(Id)
);

CREATE TABLE GroupTopic (
    GroupId INT NOT NULL,
    TopicId INT NOT NULL,
    Permission NVARCHAR(3) NULL,
    PRIMARY KEY (GroupId, TopicId),
    FOREIGN KEY (GroupId) REFERENCES Groups(Id),
    FOREIGN KEY (TopicId) REFERENCES Topic(Id)
);

CREATE TABLE Status (
    Id INT IDENTITY(1,1) NOT NULL,
    Title NVARCHAR(30) NOT NULL,
    PRIMARY KEY (Id)
);

CREATE TABLE Point (
    Id INT IDENTITY(1,1) NOT NULL,
    CreatedAt DATETIME NOT NULL,
    CreatedBy INT NOT NULL,
    ChangedAt DATETIME NULL,
    ChangedBy INT NULL,
    TopicId INT NOT NULL,
    ParentPointId INT NULL,
    CopyFromPointId INT NULL,
    Number INT NOT NULL DEFAULT 1,
    Title NVARCHAR(60) NOT NULL,
    Priority INT NOT NULL DEFAULT 1,
    StatusId INT NOT NULL,
    DueDate DATETIME NULL,
    DoneDate DATETIME NULL,
    Active BIT NOT NULL DEFAULT 1,
    PRIMARY KEY (Id),
    FOREIGN KEY (CreatedBy) REFERENCES Users(Id),
    FOREIGN KEY (ChangedBy) REFERENCES Users(Id),
    FOREIGN KEY (TopicId) REFERENCES Topic(Id),
    FOREIGN KEY (ParentPointId) REFERENCES Point(Id),
    FOREIGN KEY (CopyFromPointId) REFERENCES Point(Id),
    FOREIGN KEY (StatusId) REFERENCES Status(Id)
);

CREATE TABLE UserPoint (
    UserId INT NOT NULL,
    PointId INT NOT NULL,
    Permission NVARCHAR(3) NULL
    PRIMARY KEY (UserId, PointId),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (PointId) REFERENCES Point(Id)
);

CREATE TABLE GroupPoint (
    GroupId INT NOT NULL,
    PointId INT NOT NULL,
    Permission NVARCHAR(3) NULL,
    PRIMARY KEY (GroupId, PointId),
    FOREIGN KEY (GroupId) REFERENCES Groups(Id),
    FOREIGN KEY (PointId) REFERENCES Point(Id)
);

CREATE TABLE UserPointResponsibility (
    UserId INT NOT NULL,
    PointId INT NOT NULL,
    Level INT NOT NULL DEFAULT 1,
    PRIMARY KEY (UserId, PointId),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (PointId) REFERENCES Point(Id)
);

CREATE TABLE Tag (
    Id INT IDENTITY(1,1) NOT NULL,
    Title NVARCHAR(30) NOT NULL,
    Color NVARCHAR(10) NOT NULL,
    TopicId INT NOT NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY (TopicId) REFERENCES Topic(Id)
);

CREATE TABLE Session (
    Id INT IDENTITY(1,1) NOT NULL,
    CreatedAt DATETIME NOT NULL,
    CreatedBy INT NOT NULL,
    ChangedAt DATETIME NULL,
    ChangedBy INT NULL,
    Title NVARCHAR(60) NOT NULL,
    StartDate DATETIME NOT NULL DEFAULT GETDATE(),
    EndDate DATETIME NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY (CreatedBy) REFERENCES Users(Id),
    FOREIGN KEY (ChangedBy) REFERENCES Users(Id)
);

CREATE TABLE SessionUser (
    SessionId INT NOT NULL,
    UserId INT NOT NULL,
    Attended BIT DEFAULT 1,
    PRIMARY KEY (SessionId, UserId),
    FOREIGN KEY (SessionId) REFERENCES Session(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE SessionPoint (
    SessionId INT NOT NULL,
    PointId INT NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    PRIMARY KEY (SessionId, PointId),
    FOREIGN KEY (SessionId) REFERENCES Session(Id),
    FOREIGN KEY (PointId) REFERENCES Point(Id)
);

CREATE TABLE Channel (
    Id INT IDENTITY(1,1) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    PRIMARY KEY (Id)
);

CREATE TABLE PointChannel (
    PointId INT NOT NULL,
    ChannelId INT NOT NULL,
    PRIMARY KEY (PointId, ChannelId),
    FOREIGN KEY (PointId) REFERENCES Point(Id),
    FOREIGN KEY (ChannelId) REFERENCES Channel(Id)
);

CREATE TABLE Entry (
    Id INT IDENTITY(1,1) NOT NULL,
    CreatedAt DATETIME NOT NULL,
    CreatedBy INT NOT NULL,
    ChangedAt DATETIME NULL,
    ChangedBy INT NULL,
    ChannelId INT NOT NULL,
    SessionId INT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    ResponseTo INT NULL,
    Type INT NOT NULL DEFAULT 1,
    PRIMARY KEY (Id),
    FOREIGN KEY (CreatedBy) REFERENCES Users(Id),
    FOREIGN KEY (ChangedBy) REFERENCES Users(Id),
    FOREIGN KEY (ChannelId) REFERENCES Channel(Id),
    FOREIGN KEY (SessionId) REFERENCES Session(Id)
);