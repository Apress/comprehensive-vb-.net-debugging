Option Strict On

Module Test

    Sub Main()
        Dim X As Double = 2.45, Y As Double = 245
        X *= 100
        Console.WriteLine("X = Y? " + (X = Y).ToString)
        Console.ReadLine()
    End Sub

End Module
