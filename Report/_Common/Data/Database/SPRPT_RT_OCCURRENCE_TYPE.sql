


IF NOT EXISTS (SELECT * FROM SYS.OBJECTS WHERE TYPE = 'P' AND OBJECT_ID = OBJECT_ID('[DBO].[SPRPT_RT_OCCURRENCE_TYPE]'))
   EXEC('CREATE PROCEDURE [DBO].[SPRPT_RT_OCCURRENCE_TYPE] AS BEGIN SET NOCOUNT ON; END')
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
/*-----------------------------------------------------------------------------------------------------------------------  
-- OBJECTIVE: RETURN OCCURRENCES TYPES  
--   
-- VER DATE   AUTHOR    DESCRIPTION  
-------------------------------------------------------------------------------------------------------------------------  
-- 1.0 2022-12-01  DIEGO L. F. S.  FIRST VERSION.  
-------------------------------------------------------------------------------------------------------------------------  
-------------------------------------------------------------------------------------------------------------------------  
-- TEST:   
-------------------------------------------------------------------------------------------------------------------------  
  
 EXEC [dbo].[SPRPT_RT_OCCURRENCE_TYPE]  
           
-------------------------------------------------------------------------------------------------------------------------  
  
*/   
  
ALTER PROCEDURE [dbo].[SPRPT_RT_OCCURRENCE_TYPE]  
(    
 @ID     INT    = NULL  
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
  
SELECT [ID]  
      ,[NAME]  
  FROM [dbo].[OCCURRENCE_TYPE] (NOLOCK)  
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