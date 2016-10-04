Public Class FactorialCalc

    Public Event FactorialComplete(ByVal Factorial As Double, _
                                   ByVal TotalCalculations As Double)

    Public Shared Sub Factorial()

        Dim Working As Long = 1, FactorialResult As Double = 1
        Dim TotalCalcs As Long = 0
        Threading.Thread.Sleep(5000)
        For Working = 1 To 20000000
            FactorialResult *= Working
            TotalCalcs += 1
        Next Working

    End Sub

End Class
