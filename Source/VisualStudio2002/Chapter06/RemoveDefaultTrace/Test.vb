Option Strict On

Class Test

    Public Shared Sub Main()
        'Remove the default trace listener from the listeners collection
        'This method takes either a string containing the listener name
        'or the listener object
        Trace.Listeners.Remove("DefaultTraceListener")
    End Sub

End Class
