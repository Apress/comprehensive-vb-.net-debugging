USE PUBS
GO

/* Create original example stored procedure */
CREATE PROCEDURE sp_DebugExample
    @KeyColumn    int 
AS

INSERT	DebugTest VALUES(@KeyColumn)
  
IF @@error <> 0
BEGIN
    INSERT  DebugError
            (RowsInserted, ErrorNumber, ProblemValue)
    VALUES  (@@rowcount, @@error, @Keycolumn)    
END
GO
