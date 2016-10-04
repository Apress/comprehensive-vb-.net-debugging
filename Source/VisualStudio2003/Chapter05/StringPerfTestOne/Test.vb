Option Strict On

Module StringPerfTestOne

    Sub Main()
        Dim strTest As String = "Coming up: ", intTest As Integer = 0
        System.Console.WriteLine("Starting...")
        For intTest = 1 To 20000
            strTest += "another test "
        Next
        System.Console.WriteLine("Finished")
        System.Console.ReadLine()
    End Sub

End Module
