create table OrderProducts
(
    ProductId int not null
        constraint FK_OrderProducts_Products_ProductId
            references Products
            on delete cascade,
    OrderId   int not null
        constraint FK_OrderProducts_Orders_OrderId
            references Orders
            on delete cascade,
    constraint PK_OrderProducts
        primary key (OrderId, ProductId)
)
go

create index IX_OrderProducts_ProductId
    on OrderProducts (ProductId)
go

