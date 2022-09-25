create table OrderProduct
(
    OrdersId   int not null
        constraint FK_OrderProduct_Orders_OrdersId
            references Orders
            on delete cascade,
    ProductsId int not null
        constraint FK_OrderProduct_Products_ProductsId
            references Products
            on delete cascade,
    constraint PK_OrderProduct
        primary key (OrdersId, ProductsId)
)
go

create index IX_OrderProduct_ProductsId
    on OrderProduct (ProductsId)
go

