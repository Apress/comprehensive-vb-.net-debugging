Option Strict On

Module Test

    'Create switch for controlling tracing in hypothetical component and set
    'the boolean for this switch – setting could come from registry or config file
    Private bswTraceOutput As New Diagnostics.BooleanSwitch("MyAppTracing", "Test BooleanSwitch")

    Public Sub Main()
        'Create switch for controlling tracing in hypothetical component and set
        'the boolean for this switch – setting could come from registry or config file
        bswTraceOutput.Enabled = True
        Test()
    End Sub

    Public Sub Test()
        'Show whether the BooleanSwitch is enabled or disabled
        If bswTraceOutput.Enabled Then
            Trace.WriteLine(bswTraceOutput.DisplayName & " is enabled")
        Else
            Trace.WriteLine(bswTraceOutput.DisplayName & " is disabled")
        End If
    End Sub

End Module
