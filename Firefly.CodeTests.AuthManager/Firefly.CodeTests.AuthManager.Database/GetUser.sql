CREATE PROCEDURE [dbo].[GetUser]
	@pUserName NVARCHAR(100)
AS
BEGIN

    SET NOCOUNT ON

    IF EXISTS (SELECT TOP 1 UserID FROM [dbo].[Users] WHERE UserName=@pUserName)
    BEGIN
        SELECT UserName, Password FROM [dbo].[Users] WHERE UserName=@pUserName
    END
    ELSE
       THROW 50001, 'Invalid UserName', 1

END
