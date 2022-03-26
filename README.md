# NetCoreApi

Script Base de datos Sql Server

Create database library
go

use library
go
create table Books(
	id int primary key,
    title nvarchar(1000),
    [description]  nvarchar(max),
    [pageCount] int,
    excerpt nvarchar(max),
    publishDate datetime
)
go
create table Authors(
		id int primary key,
        idBook int,
        firstName nvarchar(500),
        lastName nvarchar(500)
)
go
