create table Orders
(
    Id        int identity
        constraint PK_Orders
            primary key,
    UserId    int not null
        constraint FK_Orders_Users_UserId
            references Users
            on delete cascade,
    TotalCost int not null,
    Quantity  int not null
)
go

create unique index Id_Index
    on Orders (Id)
go

create index IX_Orders_UserId
    on Orders (UserId)
go

