Option Strict On

Module Test

    Sub Main()

        Try
            UserDetailsLoad("MarkPearce")
        Catch Exc As Exception
            Console.WriteLine(Exc.Message)
        Finally
            Console.ReadLine()
        End Try

    End Sub

    Function UserDetailsLoad(ByVal UserId As String) As Collection
        Dim HelpfulMessage As String
        Dim FileStream As System.IO.StreamReader

        'First try to open the specified file
        Try
            FileStream = New System.IO.StreamReader(UserId & ".data")

        Catch Exc As System.IO.FileNotFoundException
            'The specified file didn't exist, but this exception won't mean
            'much to a caller who doesn't know how this method is implemented
            UserDetailsLoad = Nothing
            HelpfulMessage = "User details for '" & UserId & "' cannot be located"
            Throw New System.ArgumentException(HelpfulMessage, Exc)

        Finally

        End Try

        'Code goes here to load and return user details

    End Function

End Module