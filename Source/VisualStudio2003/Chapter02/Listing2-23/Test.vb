Option Strict On

Module BoxingTest

    Sub Main()
        Dim MyBoolean As Boolean = False
        Dim MyObject As Object = False

        SwitchBoolean(CType(MyBoolean, Boolean))
        Console.WriteLine("MyBoolean = " & MyBoolean.ToString)

        SwitchBoolean(MyObject)
        Console.WriteLine("MyObject = " & MyObject.ToString)

        Console.ReadLine()

    End Sub

    Private Sub SwitchBoolean(ByRef TestBoolean As Object)
        TestBoolean = True
    End Sub

End Module