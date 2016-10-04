USE pubs
GO

/* Build the first table */
CREATE TABLE DebugTest
(
Id		int NOT NULL IDENTITY (1,1),
KeyPrimary	int NOT NULL PRIMARY KEY CHECK (KeyPrimary > 0)
)
GO
 
/* Build the second table */
CREATE TABLE DebugError
(
Id		int NOT NULL IDENTITY (1,1),
RowsInserted	int NOT NULL,
ErrorNumber     int NOT NULL,
ProblemValue    int NOT NULL  
)
GO
