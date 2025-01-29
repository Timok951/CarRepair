
CREATE DATABASE CarRepair;
GO

USE CarRepair;
GO

CREATE TABLE WorkloadCar(
	ID_Workload INT PRIMARY KEY IDENTITY(1,1),
	NameWorkload VARCHAR(30) NOT NULL

);
GO

INSERT INTO WorkloadCar(NameWorkload)
	VALUES('Средняя');
GO

	


CREATE TABLE STO(
	ID_STO INT PRIMARY KEY IDENTITY(1,1),
	AddressSTO VARCHAR(50) NOT NULL,
	ScheduleSTO VARCHAR(3) NOT NULL,
	AmountPlaces INT NOT NULL,
	PhoneNumber VARCHAR(12) NOT NULL,
	ProfitSTO DECIMAL(10,2) NOT NULL,
	Workload_ID INT NOT NULL,
	FOREIGN KEY (Workload_ID) REFERENCES WorkloadCar(ID_Workload),

);
GO

INSERT INTO STO(AddressSTO,ScheduleSTO,AmountPlaces,PhoneNumber,ProfitSTO,Workload_ID)
	VALUES('Ул. Нежинская', '6/1', '72', '+79234653575', '77555.99', '1')
GO


CREATE TABLE StatusCar(
	ID_Status INT PRIMARY KEY IDENTITY(1,1),
	NameStatus VARCHAR(30) NOT NULL
);
GO

INSERT INTO StatusCar(NameStatus)
	VALUES('В работе'),
	('Согласован'),
	('Не согласован')


GO

CREATE TABLE SpareParts(
	
	ID_SpareParts INT PRIMARY KEY IDENTITY(1,1),
	NameSparePart VARCHAR(30) NOT NULL,
	ArticleSparePart INT NOT NULL,
	QuantityInStock INT NOT NULL,
	PriceSparePart INT NOT NULL

);
GO



INSERT INTO SpareParts(NameSparePart,ArticleSparePart,QuantityInStock,PriceSparePart )
	VALUES('Шина','3231', '3', '12')
GO

CREATE TABLE TypeClient(
	ID_TypeClient INT PRIMARY KEY IDENTITY(1,1),
	NameTypeClient VARCHAR(30) NOT NULL
);
GO

INSERT INTO TypeClient(NameTypeClient)
	VALUES('Юр. Лицо'),
	('физ. Лицо')
GO

CREATE TABLE Client(
	ID_Client INT PRIMARY KEY IDENTITY(1,1),
	SurnameClient VARCHAR(30) NOT NULL,
	NameClient VARCHAR(30) NOT NULL,
	PatronymiClient VARCHAR (30),
	TypeClient_ID INT NOT NULL,
	FOREIGN KEY (TypeClient_ID) REFERENCES TypeClient(ID_TypeClient),
	NameCompany VARCHAR(30),
	AddressCompany VARCHAR(30)
);
GO

INSERT INTO Client(SurnameClient, NameClient,PatronymiClient,TypeClient_ID,NameCompany,AddressCompany)
	VALUES('Алексеев', 'Тимофей', 'Валентинович', 1, 'Timofey. INC', 'Ул Братеевская')

GO

INSERT INTO Client(SurnameClient, NameClient,PatronymiClient,TypeClient_ID)
		VALUES('Алексеев', 'Тимофей', 'Валентинович', 2)
GO

CREATE TABLE ModelCar(
	ID_Model INT PRIMARY KEY IDENTITY(1,1),
	NameModel VARCHAR(30) NOT NULL
);
GO

INSERT INTO ModelCar(NameModel)
	VALUES('Легковой')
GO

CREATE TABLE Mark(
	ID_Mark INT PRIMARY KEY IDENTITY(1,1),
	NameMark VARCHAR(30)
);
GO

INSERT INTO Mark(NameMark)
	VALUES('Mazda')
GO

CREATE TABLE Car(
	ID_Car INT PRIMARY KEY IDENTITY(1,1),
	Client_ID INT NOT NULL,
	FOREIGN KEY (Client_ID) REFERENCES Client(ID_Client),
	ModelCar_ID INT NOT NULL,
	FOREIGN KEY (ModelCar_ID) REFERENCES ModelCar(ID_Model),
	Mark_ID INT NOT NULL,
	FOREIGN KEY (Mark_ID) REFERENCES Mark(ID_Mark),
	NumberCar VARCHAR(6) NOT NULL
);
GO

INSERT INTO Car(Client_ID, ModelCar_ID,Mark_ID,NumberCar)
	VALUES(1, 1,1,1)
GO

CREATE TABLE RoleStaff(
	ID_Role INT PRIMARY KEY IDENTITY(1,1),
	NameRole VARCHAR(30) NOT NULL
);
GO

INSERT INTO RoleStaff(NameRole)
	VALUES('Администратор'),
	('Мастер-Консультант'),
	('Автослесарь'),
	('Бухгалтер')
GO

CREATE TABLE OrderCar(
	ID_Order INT PRIMARY KEY IDENTITY(1,1),
	Status_ID INT Not NULL,
	FOREIGN KEY (Status_ID) REFERENCES StatusCar(ID_Status),
	ListOfWorks NVARCHAR(MAX) NOT NULL,
	TotalPrice DECIMAL (10,2) NOT NULL,
	SpareParts_ID INT NOT NULL,
	FOREIGN KEY (SpareParts_ID) REFERENCES SpareParts(ID_SpareParts),
	DateRequest VARCHAR (10) NOT NULL,
	STO_ID INT NOT NULL,
	FOREIGN KEY (STO_ID) REFERENCES STO(ID_STO),
	Car_ID INT,
	FOREIGN KEY (Car_ID) REFERENCES Car(ID_Car),

);




INSERT INTO OrderCar(Status_ID, ListOfWorks, TotalPrice, SpareParts_ID, DateRequest, STO_ID, Car_ID)

	VALUES(1,'Поменяли двигатель сменили шины',35999.99 ,1,'15-10-2025',1 , 1)
GO



CREATE TABLE Qualification(
	ID_Qualification INT PRIMARY KEY IDENTITY(1,1),
	NameQualification VARCHAR(50) NOT NULL
);

INSERT INTO Qualification(NameQualification)
	VALUES('Третий разряд')
GO


CREATE TABLE UserCredentials(
	ID_UserCredentials INT PRIMARY KEY IDENTITY(1,1),
	LoginUser VARCHAR(30) NOT NULL,
	PasswordUser VARCHAR(30) NOT NULL,

);

INSERT INTO UserCredentials(LoginUser,PasswordUser)
	VALUES('Tom', 'password1'),
	('Tim', 'password2'),
	('Rom', 'password3'),
	('Ader', 'password4')


GO


CREATE TABLE Staff(
	ID_Staff INT PRIMARY KEY IDENTITY(1,1),
	SurnameStaff VARCHAR(30) NOT NULL,
	NameSaff VARCHAR(30) NOT NULL,
	PatronymicStaff VARCHAR(30),
	Role_ID INT
	FOREIGN KEY (Role_ID) REFERENCES RoleStaff(ID_Role),
	UserCredentials_ID INT NOT NULL,
	FOREIGN KEY (UserCredentials_ID) REFERENCES UserCredentials(ID_UserCredentials),
	Qualification_ID INT NOT NULL,
	FOREIGN KEY (Qualification_ID) REFERENCES Qualification(ID_Qualification)

);

INSERT INTO Staff(SurnameStaff,NameSaff,PatronymicStaff,Role_ID,UserCredentials_ID,Qualification_ID)
	VALUES('Андрей', 'Андрей', 'Андреевич', 1, 1,1),
	('Андрей', 'Андрей', 'Андреевич', 2, 2,1),
	('Андрей', 'Андрей', 'Андреевич', 3, 3,1),
	('Андрей', 'Андрей', 'Андреевич', 4, 3,1)

GO

CREATE TABLE ScheduleStaff(
	ID_ScheduleStaff INT PRIMARY KEY IDENTITY(1,1),
	Order_ID INT,
	FOREIGN KEY (Order_ID) REFERENCES OrderCar(ID_Order),
	Staff_ID INT NOT NULL,
	FOREIGN KEY (Staff_ID) REFERENCES Staff(ID_Staff)
	);

GO


INSERT INTO ScheduleStaff(Order_ID,Staff_ID )
	VALUES(1,1)
GO

CREATE TABLE RepeatHistory(
	ID_RepairHistory INT PRIMARY KEY IDENTITY(1,1),
	Orders_ID INT NOT NULL,
	FOREIGN KEY (Orders_ID) REFERENCES OrderCar(ID_Order),
	ListOfRepair TEXT,
	Staff_ID INT,
	FOREIGN KEY (Staff_ID) REFERENCES Staff(ID_Staff)
);
GO
INSERT INTO RepeatHistory(Orders_ID,ListOfRepair,Staff_ID)
	VALUES(1, 'Починен двигатель', 1 )
GO

SELECT * FROM Staff

SELECT o.ID_Order, o.ListOfWorks, o.TotalPrice, s.NameSparePart
FROM OrderCar o
JOIN SpareParts s ON o.SpareParts_ID = s.ID_SpareParts;