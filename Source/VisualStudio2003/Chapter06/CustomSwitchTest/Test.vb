Option Strict On

Module Test

    'Create switch for controlling tracing in hypothetical component and set
    'the boolean for this switch – setting could come from registry or config file
    Private AppTrace As New TraceSwitchCustom("AppTrace", "A custom trace switch")

    Public Sub Main()
        'Show a trace information message
        AppTrace.MessageInfo("Trace information message", _
                             "This is a verbose version of the trace information")
        'Show a trace warning message
        AppTrace.MessageWarning("Trace warning message", _
                                "This is a verbose version of the trace warning")
        'Show a trace error message
        AppTrace.MessageError("Trace error message", _
                              "This is a verbose version of the trace error")
    End Sub

End Module

Public Class TraceSwitchCustom : Inherits TraceSwitch

    Sub New(ByVal DisplayName As String, ByVal Description As String)
        'Chain call to base class, then show that tracing has started
        MyBase.New(DisplayName, Description)
        ControlTraceOutput(TraceLevel.Info, Me.DisplayName & _
            " trace listener created - trace level is " & Me.Level.ToString, "", 1)
    End Sub

    Protected Overrides Sub OnSwitchSettingChanged()
        'Show that switch setting has changed
        If Me.TraceInfo Then
            ControlTraceOutput(TraceLevel.Info, Me.DisplayName & _
                " trace level is now " & Me.Level.ToString, "", 1)
        End If
    End Sub

    Public Sub MessageError(ByVal Message As String, ByVal VerboseMessage As String)
        'Show trace error message if errors are switched on
        If Me.TraceError Then
            ControlTraceOutput(TraceLevel.Error, Message, VerboseMessage, 2)
        End If
    End Sub

    Public Sub MessageWarning(ByVal Message As String, ByVal VerboseMessage As String)
        'Show trace warning message if warnings are switched on
        If Me.TraceWarning Then
            ControlTraceOutput(TraceLevel.Warning, Message, VerboseMessage, 2)
        End If
    End Sub

    Public Sub MessageInfo(ByVal Message As String, ByVal VerboseMessage As String)
        'Show trace information message if information messages are switched on
        If Me.TraceInfo Then
            ControlTraceOutput(TraceLevel.Info, Message, VerboseMessage, 2)
        End If
    End Sub

    Private Sub ControlTraceOutput(ByVal MessageLevel As TraceLevel, ByVal Message As String, ByVal VerboseMessage As String, ByVal StackDepth As Int16)
        'Validate parameters supplied by caller
        If Message Is Nothing Then Message = ""
        If VerboseMessage Is Nothing Then VerboseMessage = ""
        'Add system date/time to the trace message
        Dim strDateTime As String = New System.DateTime().Now.ToString
        'Add originating module/procedure name to the trace message
        '(catch any stackframe error, probably a permission problem)
        Dim strTraceSource As String
        Try
            Dim objStackFrame As New System.Diagnostics.StackFrame(StackDepth)
            strTraceSource = objStackFrame.GetMethod.DeclaringType.FullName & _
            "." & objStackFrame.GetMethod.Name
        Catch
            strTraceSource = _
            "unknown procedure (stackframe call failed - check permissions)"
        Finally
            Trace.WriteLine _
            (MessageLevel.ToString & " message at " & strDateTime & _
            " from " & strTraceSource, Me.DisplayName)
        End Try
        'Write augmented trace message
        Trace.Indent()
        Trace.WriteLine(Message)
        'Emit verbose trace message if allowed and supplied
        If Me.TraceVerbose And VerboseMessage.Length > 0 Then
            Trace.Indent()
            Trace.WriteLine(VerboseMessage)
            Trace.Unindent()
        End If
        'Finish message
        Trace.Unindent()
        Trace.Flush()
    End Sub

End Class