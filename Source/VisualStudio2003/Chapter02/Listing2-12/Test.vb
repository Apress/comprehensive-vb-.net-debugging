Option Strict On

Class Test

    Shared Sub Main()
        Dim objDerived As New Derived()
        Console.WriteLine(objDerived.ClassName())
        Console.WriteLine(objDerived.BaseName())
        Console.ReadLine()
    End Sub

End Class

Class Base

    Public Overridable Function ClassName() As String
        Return MyClass.ToString
    End Function

End Class

Class Derived : Inherits Base

    Public Overrides Function ClassName() As String
        Return MyClass.ToString
    End Function

    Public Function BaseName() As String
        Return MyBase.ClassName()
    End Function

End Class
