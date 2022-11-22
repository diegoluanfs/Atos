  
  
CREATE PROCEDURE [dbo].[SPRPT_SIGN_UP]  
(    
  @NAME  VARCHAR(100)  
 ,@PASSWORD VARCHAR(100)  
 ,@EMAIL  VARCHAR(100)  
 ,@CREATED DATETIME  
 ,@CREATED_BY INT   
 ,@FK_STATUS  INT  
 ,@ID_LANGUAGE INT  
 ,@TOKEN  VARCHAR(MAX)  
)  
AS  
  
SET NOCOUNT ON  
  
-------------------------------------------------------------------------------------------------------------------------  
-- VARIABLES  
-------------------------------------------------------------------------------------------------------------------------  
  
BEGIN TRY  
  
-------------------------------------------------------------------------------------------------------------------------  
-- INSERT RECORD   
-------------------------------------------------------------------------------------------------------------------------  
  
DECLARE @ID_HASH UNIQUEIDENTIFIER = NEWID()  
  
     --USER  
INSERT INTO [dbo].[USERS]  
     (  
      [PASS]   
     ,[LOGIN]    
     ,[CREATED]   
     ,[CREATED_BY]   
     ,[FK_STATUS]  
     ,[UPDATED]  
     ,[UPDATED_BY]  
     ,[TOKEN]  
     ,[ID_HASH]  
     )  
  VALUES  
     (   
      @PASSWORD   
     ,@EMAIL    
     ,@CREATED   
     ,@CREATED_BY   
     ,@FK_STATUS  
     ,@CREATED   
     ,@CREATED_BY  
     ,@TOKEN  
     ,@ID_HASH  
     )  
  
     DECLARE @ID_USER INT =@@IDENTITY  
        
  
-------------------------------------------------------------------------------------------------------------------------  
-- RETURN RECORD  
-------------------------------------------------------------------------------------------------------------------------  
  
SELECT @ID_HASH AS ID_USER  
             
-------------------------------------------------------------------------------------------------------------------------  
-- INSERT HISTORY  
-------------------------------------------------------------------------------------------------------------------------  
  
  
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
  
 INSERT INTO _LOG VALUES (GETDATE(),@ERRO)  
      
 SELECT  @ERROR_SEVERITY = ERROR_SEVERITY(),  @ERROR_STATE = ERROR_STATE();    
  
 RAISERROR  (@ERRO,    -- MESSAGE TEXT.    
    @ERROR_SEVERITY, -- SEVERITY.    
    @ERROR_STATE  -- STATE.    
    );    
  
END CATCH  