Option Strict On
Imports System.Runtime.InteropServices

<ClassInterface(ClassInterfaceType.AutoDual)> _
Public Class MathClass

    Public Sub New()
    End Sub

    Public Function AddTwoNumbers(ByVal FirstNumber As Double, ByVal SecondNumber As Double) As Double

        'Return the result to VB6
        Return FirstNumber + SecondNumber

    End Function

End Class
