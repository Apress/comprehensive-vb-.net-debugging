Option Strict On

Module Test

    Sub Main()

        Dim X As Double, Y As Double
        X = 10 ^ 18
        Y = X + 1
        Console.WriteLine("X = Y? " + (X = Y).ToString)
        Console.ReadLine()

    End Sub

End Module