


IF NOT EXISTS (SELECT * FROM SYS.OBJECTS WHERE TYPE = 'P' AND OBJECT_ID = OBJECT_ID('[DBO].[SPRPT_RT_VERIFY_LOGIN]'))
   EXEC('CREATE PROCEDURE [DBO].[SPRPT_RT_VERIFY_LOGIN] AS BEGIN SET NOCOUNT ON; END')
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

 EXEC [dbo].[SPRPT_RT_VERIFY_LOGIN]
	  @ID	/*	VARCHAR(50)	*/	= NULL
									
									
-------------------------------------------------------------------------------------------------------------------------

*/			

ALTER PROCEDURE [dbo].[SPRPT_RT_VERIFY_LOGIN]
(	 
	 @LOGIN VARCHAR(255)
	,@PASS	VARCHAR(255)
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
			HASH_USER
			FROM [dbo].[USERS] (NOLOCK)
			WHERE [LOGIN] = ISNULL(@LOGIN,[LOGIN])
			AND [PASS] = ISNULL(@PASS, [PASS])
				
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
