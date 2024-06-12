use Test1;

create table Employee(
userId int primary key,
fName varchar(50),
lName varchar(50),
email varchar(25),
phNo varchar(20),
Address varchar(200),
pincode int,
roleId int,
foreign key (roleId) references Roles(roleId)
);

create table Roles(
roleId int primary key,
role varchar(10));

create table Credentials(
userId int,
password varchar(20),
foreign key (userId) references Employee(userId)
);

exec sp_help 'Employee';

insert into Roles values (1,'Admin');
insert into Roles values (2,'Manager');
insert into Roles values (3,'Employee');

insert into Employee values (1,'Yash','Pathak','yp@xyz.com',62523572181,'Indira nagar, Nashik',422010,1);
select * from Employee;

insert into Credentials values(1,'abcd');
select * from Credentials;
select * from Credentials where userId = 1 and password = 'abcd';