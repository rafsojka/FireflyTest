CREATE PROCEDURE [dbo].[CreateUser]
	@pUserName NVARCHAR(100),
	@pPassword NVARCHAR(50)
AS
BEGIN
	BEGIN TRY

        INSERT INTO dbo.[Users] (UserName, Password)
        VALUES(@pUserName, @pPassword)
      
    END TRY
    BEGIN CATCH
        PRINT 'Exception occured';
		THROW;
    END CATCH
END
