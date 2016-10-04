Option Strict On

Module CatTester

    Sub Main()

        'NormalCat
        Dim objNormalCat As New Cat()
        With objNormalCat
            Console.WriteLine("NormalCat is a " + .GetType.Name)
            Console.WriteLine("It has " + .Legs.ToString + " legs and " + .Feet.ToString + " feet")
            Console.WriteLine()
        End With

        'LameCat
        Dim objLameCat As New LameCat()
        With objLameCat
            Console.WriteLine("LameCat is a " + .GetType.Name)
            Console.WriteLine("It has " + .Legs.ToString + " legs and " + .Feet.ToString + " feet")
            Console.WriteLine("Equal of a cat? " + .Equals(New Cat()).ToString)
            Console.WriteLine()
        End With

        'UglyCat
        Dim objUglyCat As New Cat()
        objUglyCat = New LameCat()
        With objUglyCat
            Console.WriteLine("UglyCat is a " + .GetType.Name)
            Console.WriteLine("It has " + .Legs.ToString + " legs and " + .Feet.ToString + " feet")
            Console.WriteLine("Equal of a cat? " + .Equals(New Cat()).ToString)
            Console.WriteLine()
        End With

        Console.ReadLine()
    End Sub

End Module

Class Cat

    Overridable Function Feet() As Int16
        Return 4
    End Function

    Overridable Function Legs() As Int16
        Return Me.Feet
    End Function

End Class

Class LameCat : Inherits Cat

    Overrides Function Feet() As Int16
        Return 3
    End Function

    Overrides Function Legs() As Int16
        Return Me.Feet
    End Function

    'This overload doesn't work for a subtle reason
    Overloads Function Equals(ByVal AnyCat As Cat) As Boolean
        Return True
    End Function

End Class
