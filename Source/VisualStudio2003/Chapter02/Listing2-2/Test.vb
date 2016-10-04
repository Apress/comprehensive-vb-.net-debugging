Option Strict On

Class Test

    Public Shared Sub Main()
        Dim objMyTest As New Derived()
        With objMyTest
            .WriteLine(10)
            .WriteLine(10.5)
            .WriteLine("11")
        End With
        Console.ReadLine()
    End Sub

End Class

Class Base

    Public Sub WriteLine(ByVal AnyString As String)
        Console.WriteLine(AnyString + " called Base:String")
    End Sub

    Public Sub WriteLine(ByVal AnyInteger As Integer)
        Console.WriteLine(AnyInteger.ToString + " called Base:Integer")
    End Sub

End Class

Class Derived : Inherits Base

    Public Overloads Sub WriteLine(ByVal AnyDouble As Double)
        Console.WriteLine(AnyDouble.ToString + " called Derived:Double")
    End Sub

End Class