CREATE PROCEDURE [dbo].[AuthenticateUser]
	@pUserName NVARCHAR(100),
	@pPassword NVARCHAR(50)
AS
BEGIN

    SET NOCOUNT ON

    DECLARE @userID INT

    IF EXISTS (SELECT TOP 1 UserID FROM [dbo].[Users] WHERE UserName=@pUserName)
    BEGIN
        SET @userID=(SELECT UserID FROM [dbo].[Users] WHERE UserName=@pUserName AND Password = @pPassword)

       IF(@userID IS NULL)
           THROW 50002, 'Invalid Password', 2
    END
    ELSE
       THROW 50001, 'Invalid UserName', 1

END
