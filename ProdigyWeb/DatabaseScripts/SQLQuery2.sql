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

CONSTRAINT UC_Username UNIQUE(Username)

)

Create Table UsersStarredBooks (

ID int Identity primary key,

BookISBN nvarchar(55) not null,

UserID int not null foreign key references Users(ID),


)



Go

INSERT INTO Users VALUES
('m', 'maya', 'yulzary', '123', 'maya@gmail.com');

INSERT INTO Users VALUES
('avig', 'avigidigidigi', 'dorrrr', '69', 'avigi@gmail.com');

--INSERT INTO UsersStarredBooks VALUES
--('0063412632', 1);

GO

--scaffold-dbcontext "Server=localhost\sqlexpress;Database=ProdigyDB;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models –force