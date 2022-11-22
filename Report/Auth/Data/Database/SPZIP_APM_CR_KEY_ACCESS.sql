
IF NOT EXISTS (SELECT * FROM SYS.OBJECTS WHERE TYPE = 'P' AND OBJECT_ID = OBJECT_ID('[DBO].[SPRPT_CR_KEY_ACCESS]'))
   EXEC('CREATE PROCEDURE [DBO].[SPRPT_CR_KEY_ACCESS] AS BEGIN SET NOCOUNT ON; END')
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*-----------------------------------------------------------------------------------------------------------------------
-- OBJECTIVE: LOGIN USER
-- 
-- VER	DATE			AUTHOR				DESCRIPTION
-------------------------------------------------------------------------------------------------------------------------
-- 1.0	2022-10-31		DIEGO L. F. S.		FIRST VERSION.
-------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------
-- TEST: 
-------------------------------------------------------------------------------------------------------------------------

 EXEC [dbo].[SPRPT_CR_KEY_ACCESS]
	 @DT_CREATED	/* INT			*/ = '2022-11-01 00:00:00'
	,@FK_USER		/* VARCHAR(256)	*/ = '116'
	,@FK_HASH		/* VARCHAR(100)	*/ = 'B245B428-375D-4308-8AE1-35327E58F821'
	,@TX_KEY		/* VARCHAR(256)	*/ = '123456'
	,@TX_IP			/* VARCHAR(100) */ = '::1'
			
			
-------------------------------------------------------------------------------------------------------------------------

*/			
ALTER PROCEDURE [dbo].[SPRPT_CR_KEY_ACCESS]
(	 
	 @DT_CREATED	DATETIME
	,@FK_USER		VARCHAR(256)
	,@FK_HASH		VARCHAR(100)
	,@TX_KEY		VARCHAR(256)
	,@TX_IP			VARCHAR(100)
	,@DT_EXPIRE	DATETIME
	
)
AS

SET NOCOUNT ON

-------------------------------------------------------------------------------------------------------------------------
-- VARIABLES
-------------------------------------------------------------------------------------------------------------------------

BEGIN TRY

-------------------------------------------------------------------------------------------------------------------------
-- RETURN RECORD
-------------------------------------------------------------------------------------------------------------------------

UPDATE ACCESS_KEY_CLIENT SET FK_STATUS = 2 WHERE FK_USER = @FK_USER AND FK_STATUS = 1

SET NOCOUNT OFF

INSERT INTO ACCESS_KEY_CLIENT(
			 DT_CREATED
			,FK_USER
			,FK_HASH
			,TX_KEY
			,DT_EXPIRE
			,FK_STATUS
			,TX_IP
		) 
		VALUES 
		(
			 @DT_CREATED
			,@FK_USER
			,@FK_HASH
			,@TX_KEY
			,@DT_EXPIRE
			,1
			,@TX_IP
		)
				
END TRY


-------------------------------------------------------------------------------------------------------------------------
-- ON ERROR
-------------------------------------------------------------------------------------------------------------------------


BEGIN CATCH
 

	DECLARE @ERRO VARCHAR(MAX)
	DECLARE @ERROR_SEVERITY INT;  
	DECLARE @ERROR_STATE INT;  
       
	SET @ERRO = (SELECT	 'PROC: ' + CAST(ERROR_PROCEDURE()  AS VARCHAR(300)) + 
					' ||  NUMB: ' + CAST(ERROR_NUMBER()		AS VARCHAR) +  
					' ||  LINE: ' + CAST(ERROR_LINE()		AS VARCHAR) +   
					' ||  MSSG: ' + CAST(ERROR_MESSAGE()	AS VARCHAR(MAX))) 

	INSERT INTO _LOG VALUES (GETDATE(),@ERRO)
			 
	SELECT 	@ERROR_SEVERITY = ERROR_SEVERITY(),  @ERROR_STATE = ERROR_STATE();  

	RAISERROR  (@ERRO,			 -- MESSAGE TEXT.  
				@ERROR_SEVERITY, -- SEVERITY.  
				@ERROR_STATE	 -- STATE.  
				);  

END CATCH
GO
