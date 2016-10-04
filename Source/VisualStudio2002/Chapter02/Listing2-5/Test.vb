Option Strict On

Class Test

    Public Shared Sub Main()
        Dim objDerived As New Derived()
        objDerived.DoSomething(CLng(8))
        Console.ReadLine()
    End Sub

End Class

Class Base

    Public Overridable Sub DoSomething(ByVal NewValue As Long)
        Console.WriteLine("Base:DoSomething(Long) called")
    End Sub

    Public Sub DoSomething(ByVal NewValue As Double)
        Console.WriteLine("Base:DoSomething(Double) called")
    End Sub
End Class

Class Derived : Inherits Base

    Public Overloads Overrides Sub DoSomething(ByVal NewValue As Long)
        Console.WriteLine("Derived:DoSomething(Long) called")
    End Sub

    Public Overloads Sub DoSomething(ByVal NewValue As Integer)
        Console.WriteLine("Derived:DoSomething(Integer) called")
    End Sub
End Class
