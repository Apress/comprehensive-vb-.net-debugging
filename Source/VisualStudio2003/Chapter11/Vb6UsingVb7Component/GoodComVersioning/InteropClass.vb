Option Strict On
Imports System.Runtime.InteropServices

Public Interface IMathClass
    Function AddTwoNumbers(ByVal FirstNumber As Double, _
    ByVal SecondNumber As Double) As Double
End Interface

<ClassInterface(ClassInterfaceType.None)> _
Public Class MathClass : Implements IMathClass

    Public Sub New()
    End Sub

    Public Function AddTwoNumbers(ByVal FirstNumber As Double, ByVal SecondNumber As Double) As Double Implements IMathClass.AddTwoNumbers
        'Return the result to VB6
        Return FirstNumber + SecondNumber
    End Function

End Class