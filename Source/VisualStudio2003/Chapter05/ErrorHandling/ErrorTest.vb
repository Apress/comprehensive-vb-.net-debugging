Option Strict On
Imports System.Data.SqlClient

Module ErrorTest

    Sub Main()
        Dim strConnection As String
        'Set up database connection
        strConnection = "Initial Catalog = Northwind;"
        strConnection += "Data Source = CHEETAH;"
        strConnection += "Integrated Security = SSPI"
        'Try old and new error-handling functions
        MethodOld(strConnection)
        MethodNew(strConnection)
    End Sub

    Function MethodOld(ByVal ConnectString As String) As Boolean
        Dim objSqlConnect As SqlConnection
        'Test database connection with old error handling
        On Error Resume Next
        objSqlConnect = New SqlConnection(ConnectString)
        MethodOld = CBool(Err.Number = 0)
        objSqlConnect.Close()
        objSqlConnect.Dispose()
    End Function

    Function MethodNew(ByVal ConnectString As String) As Boolean
        Dim objSqlConnect As SqlConnection
        'Test database connection with new error handling
        Try
            objSqlConnect = New SqlConnection(ConnectString)
            objSqlConnect.Close()
            objSqlConnect.Dispose()
            Return True
        Catch
            Return False
        Finally
        End Try
    End Function

End Module
