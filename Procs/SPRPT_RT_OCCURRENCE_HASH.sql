


IF NOT EXISTS (SELECT * FROM SYS.OBJECTS WHERE TYPE = 'P' AND OBJECT_ID = OBJECT_ID('[DBO].[SPRPT_RT_OCCURRENCE_HASH]'))
   EXEC('CREATE PROCEDURE [DBO].[SPRPT_RT_OCCURRENCE_HASH] AS BEGIN SET NOCOUNT ON; END')
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
-- 1.0 2022-12-02  DIEGO L.F.S   FIRST VERSION  
-------------------------------------------------------------------------------------------------------------------------  
-------------------------------------------------------------------------------------------------------------------------  
-- TEST:   
-------------------------------------------------------------------------------------------------------------------------  
DECLARE @ID_TESTE INT  
  
 SET @ID_TESTE = 1  
   
 SELECT * FROM [dbo].[SETTINGS] WHERE ID_USER = @ID_TESTE  
  
 EXEC [dbo].[SPRPT_RT_OCCURRENCE_HASH]    @ID  = 25  
           
-------------------------------------------------------------------------------------------------------------------------  
  
*/   
  
ALTER PROCEDURE [dbo].[SPRPT_RT_OCCURRENCE_HASH]  
(   
 @ID  INT  
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
    [HASH_OCCURRENCE] AS HASH  
     FROM [dbo].[OCCURRENCES] (NOLOCK)  
     WHERE [ID]     = ISNULL (@ID,[ID])  
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