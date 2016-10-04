Option Strict On

Class Animal

    Public Shared Sub Main()

        Dim objMan As New Man()
        Dim objFeline As New Feline(), objCat As New Cat()

        Console.WriteLine(objMan.ClassName("This Man"))
        Console.WriteLine(objFeline.ClassName("This Feline"))
        Console.WriteLine(objCat.ClassName("This Cat"))

        Console.ReadLine()

    End Sub

    Protected Overridable Function ClassName(ByVal CallingType As String) As String
        Return CallingType + " appears to be an Animal"
    End Function

End Class

Class Man : Inherits Animal

    Protected Overrides Function ClassName(ByVal CallingType As String) As String
        Return CallingType + " appears to be a Man"
    End Function

End Class

Class Feline : Inherits Animal

    Protected Overridable Shadows Function ClassName(ByVal CallingType As String) As String
        Return CallingType + " appears to be a Feline"
    End Function

End Class

Class Cat : Inherits Feline

    Protected Overrides Function ClassName(ByVal CallingType As String) As String
        Return CallingType + " appears to be a Cat"
    End Function

End Class
