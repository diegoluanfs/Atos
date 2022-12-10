


IF NOT EXISTS (SELECT * FROM SYS.OBJECTS WHERE TYPE = 'P' AND OBJECT_ID = OBJECT_ID('[DBO].[SPRPT_CR_USER]'))
   EXEC('CREATE PROCEDURE [DBO].[SPRPT_CR_USER] AS BEGIN SET NOCOUNT ON; END')
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
          
/*-----------------------------------------------------------------------------------------------------------------------        
-- OBJECTIVE: RETURN SETTINGS        
--         
-- VER DATE   AUTHOR    DESCRIPTION        
-------------------------------------------------------------------------------------------------------------------------        
-- 1.0 2022-12-01  DIEGO L. F. S.  FIRST VERSION.        
-------------------------------------------------------------------------------------------------------------------------        
-------------------------------------------------------------------------------------------------------------------------        
-- TEST:         
-------------------------------------------------------------------------------------------------------------------------        
SELECT * FROM OCCURRENCES        
        
 EXEC [DBO].[SPRPT_CR_USER]        
  @LATITUDE   /* DECIMAL(10,8)  */ = -29.704757879078993         
 ,@LONGITUDE   /* DECIMAL(11,8)  */ = -53.83265341796876         
 ,@CREATED   /* DATETIME    */ = '2022-12-02'        
 ,@UPDATED   /* DATETIME    */ = '2022-12-02'        
 ,@CREATED_BY  /* INT     */ = 1        
 ,@UPDATED_BY  /* INT     */ = 1        
 --,@GEOLOCATION  /* GEOGRAPHY   */ = NULL        
                 
-------------------------------------------------------------------------------------------------------------------------        
        
*/        
        
ALTER PROCEDURE [DBO].[SPRPT_CR_USER]        
(          
  @FULL_NAME VARCHAR(50) 
 ,@DOCUMENT VARCHAR(50)
 ,@EMAIL  VARCHAR(100)
 ,@CREATED  DATETIME             
 ,@UPDATED  DATETIME           
 ,@CREATED_BY  INT     = NULL        
 ,@UPDATED_BY  INT     = NULL     
)      
AS        
        
SET NOCOUNT OFF        
        
-------------------------------------------------------------------------------------------------------------------------        
-- VARIABLES        
-------------------------------------------------------------------------------------------------------------------------        
        
BEGIN TRY        
        
-------------------------------------------------------------------------------------------------------------------------        
-- RETURN RECORD        
-------------------------------------------------------------------------------------------------------------------------        
        
INSERT INTO [dbo].[USERS]
           (
		    [LOGIN]
           ,[PASS]
           ,[TOKEN]
           ,[CREATED]
           ,[UPDATED]
           ,[CREATED_BY]
           ,[UPDATED_BY]
           ,[STATUS]
           ,[HASH_USER])
     VALUES
           (
		    @EMAIL
           ,'123'
           ,'token'
           ,@CREATED
           ,@UPDATED
           ,@CREATED_BY
           ,@UPDATED_BY
           ,1
           ,NEWID()
		   )SELECT SCOPE_IDENTITY() 
END TRY        
        
-------------------------------------------------------------------------------------------------------------------------        
-- ON ERROR        
-------------------------------------------------------------------------------------------------------------------------        
        
BEGIN CATCH        
         
 DECLARE @ERRO VARCHAR(MAX)        
 DECLARE @ERROR_SEVERITY INT;          
 DECLARE @ERROR_STATE INT;          
               
 SET @ERRO = (SELECT  'PROC: ' + CAST(ERROR_PROCEDURE()  AS VARCHAR(300)) +         
     ' ||  NUMB: ' + CAST(ERROR_NUMBER()  AS VARCHAR) +          
     ' ||  LINE: ' + CAST(ERROR_LINE()  AS VARCHAR) +           
     ' ||  MSSG: ' + CAST(ERROR_MESSAGE() AS VARCHAR(MAX)))         
        
 --INSERT INTO _LOG VALUES (GETDATE(),@ERRO)        
            
 SELECT  @ERROR_SEVERITY = ERROR_SEVERITY(),  @ERROR_STATE = ERROR_STATE();          
        
 RAISERROR  (@ERRO,    -- MESSAGE TEXT.          
    @ERROR_SEVERITY, -- SEVERITY.         
    @ERROR_STATE  -- STATE.         
    );          
        
END CATCH 