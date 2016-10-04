Class Test

    Public Shared Sub Main()
        Dim objDerived As New Derived(), objBase As New Base()
        Console.WriteLine(objDerived.ClassName())
        Console.WriteLine(objDerived.BaseName())
        Console.ReadLine()
    End Sub

    Public Shared Function ImmediateClassName() As String
        Dim objStackFrame As New Diagnostics.StackFrame(1)
        Return objStackFrame.GetMethod.DeclaringType.FullName
    End Function

End Class

Class Base

    Public Overridable Function ClassName() As String
        'Return MyClass.ToString
        Return Test.ImmediateClassName
    End Function

End Class

Class Derived : Inherits Base

    Public Overrides Function ClassName() As String
        'Return MyClass.ToString
        Return Test.ImmediateClassName
    End Function

    Public Function BaseName() As String
        Return MyBase.ClassName()
    End Function

End Class
