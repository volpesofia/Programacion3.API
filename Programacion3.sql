CREATE DATABASE Programacion3;
go
Use Programacion3;
go

CREATE TABLE Productos (

	ID int Primary Key identity (1,1),
	Nombre varchar (200) not null,
	Precio decimal (10,2) not null,
	Stock int not null check (Stock >= 0)
);
	go

	INSERT INTO Productos (Nombre, Precio, Stock) VALUES
('Notebook Lenovo', 850000.00, 10),
('Mouse Logitech', 25000.00, 25),
('Teclado Redragon', 45000.00, 15),
('Monitor Samsung 24"', 180000.00, 8),
('Auriculares JBL', 65000.00, 20);

go