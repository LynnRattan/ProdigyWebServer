Use master

Create Database ProdigyDB

Go

Use ProdigyDB

Go

Create Table Users (

ID int Identity primary key,

Username nvarchar(100) not null,

FirstName nvarchar(30) not null,

LastName nvarchar(30) not null,

UserPswd nvarchar(30) not null,

Email nvarchar(30) not null,

[Image] nvarchar(250) not null,

CONSTRAINT UC_Username UNIQUE(Username)

)

Create Table UsersStarredBooks (

ID int Identity primary key,

BookISBN nvarchar(55) not null,

UserID int not null foreign key references Users(ID),


)

Create Table UsersTBR(

ID int Identity primary key, 

BookISBN nvarchar(55) not null,

UserID int not null foreign key references Users(ID),


)



Go

INSERT INTO Users VALUES
('m', 'maya', 'yulzary', '123', 'maya@gmail.com');

INSERT INTO Users VALUES
('avig', 'avig', 'dorrrr', '456', 'avigi@gmail.com');



GO

--scaffold-dbcontext "Server=localhost\sqlexpress;Database=ProdigyDB;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models –force