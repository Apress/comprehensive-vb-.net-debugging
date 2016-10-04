Option Strict On

Module Test

    'Create switch for controlling tracing in hypothetical component and set
    'the boolean for this switch – setting could come from registry or config file
    Private tswTraceControl As New Diagnostics.TraceSwitch("SwitchTrace", "Test trace switch")

    Public Sub Main()
        'Create switch for controlling tracing in hypothetical component and set
        'the boolean for this switch – setting could come from registry or config file
        tswTraceControl.Level = TraceLevel.Warning
        Test()
    End Sub

    Public Sub Test()
        'Either check the TraceError, TraceWarning, TraceInfo
        'or TraceVerbose properties
        If tswTraceControl.TraceError Then
            Trace.WriteLine("Trace only shows if traceswitch is set to show errors")
        End If
        'Or check the Level property against the TraceLevel enumeration
        If tswTraceControl.Level = TraceLevel.Warning Then
            Trace.WriteLine("Trace shows if traceswitch is set to show warnings or errors")
        End If
    End Sub

End Module
