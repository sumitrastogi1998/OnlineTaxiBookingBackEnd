create database taxi_booking_system

use taxi_booking_system


CREATE TABLE Employee (
    EmployeeID int PRIMARY KEY IDENTITY (1, 1),
    EmployeeName varchar(255) not null,
    MobileNumber nvarchar(20) not null,
    City varchar(255) not null,
    Email varchar(255) not null,
	LicenseNumber int not null,
	Password nvarchar(50),
	Available bit,
);
alter table Employee add constraint df_Available default 1 for Available


CREATE TABLE Customer (
    CustomerID int PRIMARY KEY IDENTITY (1, 1),
    CustomerPassword varchar(255) not null,
	CustomerName varchar(255) not null,
    MobileNumber nvarchar(20) not null,
    Email varchar(255) not null
);

CREATE TABLE Users(
    UserID int PRIMARY KEY IDENTITY (1, 1),
    UserName varchar(255) not null,
    UserPassword varchar(255) not null,
	UserRole nvarchar(255) not null
);

CREATE TABLE Taxi(
    TaxiID int PRIMARY KEY IDENTITY (1, 1),
    TaxiNumber varchar(255) not null,
    TaxiModel varchar(255) not null,
	TaxiOwnerId int not null FOREIGN KEY REFERENCES Employee(EmployeeID)
);

CREATE TABLE Booking(
    BookingID int PRIMARY KEY IDENTITY (1, 1),
    FromDate date not null,
    ToDate date not null,
	BookTime time not null,
    DropTime time not null,
	PickupPoint varchar(255),
	DropPoint varchar(255),
	FeedBack nvarchar(max),
	Fare int,
	employeeId int FOREIGN KEY REFERENCES Employee(EmployeeID),
	customerId int FOREIGN KEY REFERENCES Customer(CustomerID)
);

Create table EmployeeRoster(
RosterID int primary key identity(1,1),
FromDate date,
ToDate date,
InTime time,
OutTime time,
employeeId int FOREIGN KEY REFERENCES Employee(EmployeeID)
);

Create table logs(
custId int Primary key Identity(1,1),
custName varchar(50) not null,
source varchar(50) not null,
destination varchar(50) not null,
fromdate date not null,
todate date not null,
totalfare money not null,
rating int not null,
eId int FOREIGN KEY REFERENCES Employee(EmployeeID)
)


insert into Users values('admin@gmail.com','admin@123','Admin')

select * from Booking
select * from Customer
select * from Users
select * from Employee
select * from Taxi
select * from logs
select * from EmployeeRoster


drop table Booking
drop table Taxi
drop table Employee
drop table Users
drop table Customer 
drop table logs
drop table EmployeeRoster
