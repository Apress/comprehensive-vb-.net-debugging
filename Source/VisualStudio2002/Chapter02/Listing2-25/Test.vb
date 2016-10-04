Option Strict On

Module Test

    Sub Main()

        Console.WriteLine(Single.NaN = Single.NaN)
        Console.WriteLine((Single.NaN - Single.NaN) = 0)

        Console.WriteLine(Single.PositiveInfinity = Single.PositiveInfinity)
        Console.WriteLine((Single.PositiveInfinity - Single.PositiveInfinity) = 0)

        Console.WriteLine(1.0 < Single.NaN)
        Console.WriteLine(1.0 >= Single.NaN)
        Console.ReadLine()

    End Sub

End Module
