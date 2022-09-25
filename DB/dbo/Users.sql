create table Users
(
    Id       int identity
        constraint PK_Users
            primary key,
    Username nvarchar(max) not null,
    Password nvarchar(max) not null,
    Role     nvarchar(max) not null
)
go

create unique index Id_Index2
    on Users (Id)
go

