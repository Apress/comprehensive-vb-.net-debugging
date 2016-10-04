Option Strict On

Class Base
    Overridable Sub DoSomething()
        'The method definition goes here
    End Sub
End Class

Class Derived : Inherits Base
    Sub DoSomething()
        'This method will actually shadow its base method rather than Override it,
        'because the developer forgot to add the Overrides keyword
    End Sub
End Class
