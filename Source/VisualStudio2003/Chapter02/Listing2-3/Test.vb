Option Strict On

Class Test

    Public Shared Sub Main()
        Dim objDerived As New Derived()
        Dim objTest As New MyTest()
        objDerived.DoSomething(objTest)
        Console.ReadLine()
    End Sub

End Class

Class MyTest
End Class

Class Base

    Public Overridable Sub DoSomething(ByVal NewValue As MyTest)
        Console.WriteLine("Base:DoSomething(MyTest) called")
    End Sub

End Class

Class Derived : Inherits Base

    Public Overloads Overrides Sub DoSomething(ByVal NewValue As MyTest)
        Console.WriteLine("Derived:DoSomething(MyTest) called")
    End Sub

    Public Overloads Sub DoSomething(ByVal NewValue As Object)
        Console.WriteLine("Derived:DoSomething(Object) called")
    End Sub

End Class