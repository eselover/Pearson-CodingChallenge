GO
CREATE DATABASE StudyGuideOrders;

GO
CREATE TABLE StudyGuideOrders.dbo.Customer (
	Id VARCHAR(20) NOT NULL PRIMARY KEY,
	FirstName VARCHAR(50) NOT NULL,
	LastName VARCHAR(50) NOT NULL,
	Email VARCHAR(20) NOT NULL,
);

GO
CREATE TABLE StudyGuideOrders.dbo.StudyGuide (
	Id VARCHAR(20) NOT NULL PRIMARY KEY,
	Name VARCHAR(100) NOT NULL,
	Price FLOAT NOT NULL,
);

GO
CREATE TABLE StudyGuideOrders.dbo.StudyGuideOrder (
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	CustomerId VARCHAR(20) NOT NULL,
	StudyGuideId VARCHAR(20) NOT NULL,
	IsFulfilled BINARY,
	DateFulfilled DATE,
	CONSTRAINT CustomerId_FK FOREIGN KEY (CustomerId) REFERENCES StudyGuideOrders.dbo.Customer(Id),
	CONSTRAINT StudyGuideId_FK FOREIGN KEY (StudyGuideId) REFERENCES StudyGuideOrders.dbo.StudyGuide(Id),
);