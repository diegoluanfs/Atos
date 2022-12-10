
IF NOT EXISTS (SELECT * FROM SYS.OBJECTS WHERE TYPE = 'P' AND OBJECT_ID = OBJECT_ID('[DBO].[SPZIP_RT_LOGIN_EXISTS]'))
   EXEC('CREATE PROCEDURE [DBO].[SPZIP_RT_LOGIN_EXISTS] AS BEGIN SET NOCOUNT ON; END')
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

 EXEC [dbo].[SPZIP_RT_LOGIN_EXISTS]
	  @LOGIN	/*	VARCHAR(50)	*/	= 'diegoluanfs@gmail.com'
									
									
-------------------------------------------------------------------------------------------------------------------------

*/			

ALTER PROCEDURE [dbo].[SPZIP_RT_LOGIN_EXISTS]
(	 
 	
	  @LOGIN		VARCHAR(50)
	
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


			SELECT 
				 ID
			  FROM [dbo].[USERS] (NOLOCK)
			  WHERE [LOGIN]	= @LOGIN
				
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

	--INSERT INTO _LOG VALUES (GETDATE(),@ERRO)
			 
	SELECT 	@ERROR_SEVERITY = ERROR_SEVERITY(),  @ERROR_STATE = ERROR_STATE();  

	RAISERROR  (@ERRO,			 -- MESSAGE TEXT.  
				@ERROR_SEVERITY, -- SEVERITY.  
				@ERROR_STATE	 -- STATE.  
				);  

END CATCH
GO
