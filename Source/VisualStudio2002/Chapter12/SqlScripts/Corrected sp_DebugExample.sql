USE PUBS
GO

/* Create corrected example stored procedure */
ALTER PROCEDURE sp_DebugExample
    @KeyColumn    int 
AS

DECLARE	@rowcount    int,
	@error       int

INSERT	DebugTest VALUES(@KeyColumn)
SELECT  @error = @@error,
        @rowcount = @@rowcount
  
IF @error <> 0
BEGIN
    INSERT  DebugError
            (RowsInserted, ErrorNumber, ProblemValue)
    VALUES  (@rowcount, @error, @Keycolumn)    
END
GO

