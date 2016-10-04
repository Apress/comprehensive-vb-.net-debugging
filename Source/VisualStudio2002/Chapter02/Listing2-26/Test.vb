Option Strict On

Module Test

    Sub Main()

        Try
            Console.WriteLine(CalcTotalOne(Single.NaN))
            Console.WriteLine(CalcTotalTwo(Single.NaN))
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            Console.ReadLine()
        End Try

    End Sub

    Function CalcTotalOne(ByVal PurchaseAmount As Single) As Single
        If PurchaseAmount < 0.0 Then
            Throw New ArgumentException("PurchaseAmount must be >= zero")
        End If
        Return PurchaseAmount * 1.08F
    End Function

    Function CalcTotalTwo(ByVal PurchaseAmount As Single) As Single
        If PurchaseAmount >= 0.0 Then
            Return PurchaseAmount * 1.08F
        Else
            Throw New ArgumentException("PurchaseAmount must be >= zero")
        End If
    End Function

End Module