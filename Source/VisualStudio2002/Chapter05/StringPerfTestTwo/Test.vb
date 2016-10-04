Option Strict On

Module StringPerfTestTwo

    Sub Main()
        Dim sbTest As New System.Text.StringBuilder("Coming up: ")
        Dim intTest As Integer = 0
        System.Console.WriteLine("Starting...")
        For intTest = 1 To 20000
            sbTest.Append("another test ")
        Next
        System.Console.WriteLine("Finished")
        System.Console.ReadLine()
    End Sub

End Module