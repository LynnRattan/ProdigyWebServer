﻿Use master

Create Database ProdigyDB

Go

Use ProdigyDB

Go

Create Table Users (

ID int Identity primary key,

Email nvarchar(100) not null,

FirstName nvarchar(30) not null,

LastName nvarchar(30) not null,

UserPswd nvarchar(30) not null,

CONSTRAINT UC_Email UNIQUE(Email)

)

Go

INSERT INTO Users VALUES
('myulzary9@gmail.com', 'maya', 'yulzary', 'maya123');

GO