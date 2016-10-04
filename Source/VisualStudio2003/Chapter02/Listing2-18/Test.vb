Option Strict Off

Module Test

    Sub Main()

        Console.WriteLine("Developer sees: " & WhatDeveloperSees.ToString)
        Console.WriteLine("Compiler says: " & WhatCompilerSees.ToString)
        Console.ReadLine()

    End Sub

    Function WhatDeveloperWrites() As Integer
        Dim A As Integer = 0, B As Integer = 1, C As Integer = 2
        A = B = C
        Return A
    End Function

    Function WhatDeveloperSees() As Integer
        Dim A As Integer = 0, B As Integer = 1, C As Integer = 2
        B = C
        A = B
        Return A
    End Function

    Function WhatCompilerSees() As Integer
        Dim A As Integer = 0, B As Integer = 1, C As Integer = 2
        A = CInt(CBool(B = C))
        Return A
    End Function

End Module
