USE StudyGuideOrders

GO
CREATE PROCEDURE spGetAllOrders
AS
BEGIN
	SELECT * FROM StudyGuideOrders GROUP BY CustomerId
END

GO
CREATE PROCEDURE spAddCustomer (
	@Id VARCHAR(20),
	@FirstName VARCHAR(50),
	@LastName VARCHAR(50),
	@Email VARCHAR(20)
)
AS
BEGIN
	INSERT INTO Customer (Id, FirstName, LastName, Email)
	VALUES (@Id, @FirstName, @LastName, @Email)
END

GO
CREATE PROCEDURE spAddStudyGuide (
	@Id VARCHAR(20),
	@Name VARCHAR(100),
	@Price FLOAT
)
AS
BEGIN
	INSERT INTO StudyGuide (Id, Name, Price)
	VALUES (@Id, @Name, @Price)
END

GO
CREATE PROCEDURE spAddOrder (
	@CustomerId VARCHAR(20),
	@StudyGuideId VARCHAR(20)
)
AS
BEGIN
	INSERT INTO StudyGuideOrder (customerId, StudyGuideId)
	VALUES (@CustomerId, @StudyGuideId)
END

GO
CREATE PROCEDURE spUpdateOrder (
	@Id INTEGER,
	@IsFulfilled BINARY,
	@DateFulfilled DATE
)
AS
BEGIN
	UPDATE StudyGuideOrder SET
		IsFulfilled=@IsFulfilled,
		DateFulfilled=@DateFulfilled
	WHERE Id=@Id
END