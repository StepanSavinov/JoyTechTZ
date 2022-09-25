create table Products
(
    Id    int identity
        constraint PK_Products
            primary key,
    Name  nvarchar(max) not null,
    Price int           not null
)
go

create unique index Id_Index1
    on Products (Id)
go

