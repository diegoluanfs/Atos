


IF NOT EXISTS (SELECT * FROM SYS.OBJECTS WHERE TYPE = 'P' AND OBJECT_ID = OBJECT_ID('[DBO].[SPRPT_CR_OCCURRENCE]'))
   EXEC('CREATE PROCEDURE [DBO].[SPRPT_CR_OCCURRENCE] AS BEGIN SET NOCOUNT ON; END')
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
        
 EXEC [DBO].[SPRPT_CR_OCCURRENCE]        
  @LATITUDE   /* DECIMAL(10,8)  */ = -29.704757879078993         
 ,@LONGITUDE   /* DECIMAL(11,8)  */ = -53.83265341796876         
 ,@CREATED   /* DATETIME    */ = '2022-12-02'        
 ,@UPDATED   /* DATETIME    */ = '2022-12-02'        
 ,@CREATED_BY  /* INT     */ = 1        
 ,@UPDATED_BY  /* INT     */ = 1        
 --,@GEOLOCATION  /* GEOGRAPHY   */ = NULL        
                 
-------------------------------------------------------------------------------------------------------------------------        
        
*/        
        
ALTER PROCEDURE [DBO].[SPRPT_CR_OCCURRENCE]        
(          
  @LATITUDE DECIMAL(11,9)            
 ,@LONGITUDE  DECIMAL(11,9)      
 ,@DESCRIPTION  VARCHAR(MAX) =NULL 
 ,@ID_OCCURRENCE_TYPE INT        
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
        
INSERT INTO [DBO].[OCCURRENCES]    
           ( 
		    [LATITUDE]
           ,[LONGITUDE]
           ,[DESCRIPTION]
           ,[ID_OCCURRENCE_TYPE]
           ,[HASH_OCCURRENCE]
           ,[CREATED]
           ,[UPDATED]
           ,[CREATED_BY]
           ,[UPDATED_BY]
     )  
     VALUES      
     (
		    @LATITUDE      
           ,@LONGITUDE 
		   ,@DESCRIPTION
           ,@ID_OCCURRENCE_TYPE
		   ,NEWID()
           ,@CREATED      
           ,@UPDATED    
           ,@CREATED_BY      
           ,@UPDATED_BY     
	)      
             
 EXEC SPRPT_RT_OCCURRENCE_HASH @ID = @@IDENTITY          
        
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