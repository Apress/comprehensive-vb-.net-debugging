Option Strict On

Class Test

    Public Shared Sub Main()

        'Create an event log trace listener, give it a name, and configure it
        Dim objTraceToAppLog As New EventLogTraceListener("LogTraceListener")
        With objTraceToAppLog
            'Write to the Application event log
            .EventLog.Log = "Application"
            'Specify source of tracing information
            .EventLog.Source = "AppName." & .Name
        End With
        'Add the new trace listener to the collection of listeners
        Trace.Listeners.Add(objTraceToAppLog)
        'Write trace information to all listeners,
        'including the Application event log
        Trace.WriteLine("This trace information is for all listeners")
        'Finish and clean up our console listener
        With objTraceToAppLog
            .Flush()
            .Close()
            .Dispose()
        End With

    End Sub

End Class
