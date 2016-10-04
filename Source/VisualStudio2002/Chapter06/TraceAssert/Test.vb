Option Strict On

Module Test

    Sub Main()
        Dim VbClassic As Int16, VbNet As Int16
        'Does TRUE have the same value when using
        'VB.Classic functions and VB .NET functions?
        VbClassic = CInt(True)
        VbNet = Convert.ToInt16(True)
        'This overload just shows the call stack leading to the assertion failure
        Trace.Assert(VbClassic = VbNet)
        'This overload adds an brief explanation of the assertion failure
        Trace.Assert(VbClassic = VbNet, "Assertion failed: VbClassic(True) <> VbNet(True)")
        'This overload adds a more detailed explanation of the assertion failure
        Trace.Assert(VbClassic = VbNet, "Assertion failed: VbClassic(True) <> VbNet(True)", _
                "CInt(True)=" & VbClassic.ToString & _
                " : Convert.ToInt16(True)=" & VbNet.ToString)
    End Sub

End Module
