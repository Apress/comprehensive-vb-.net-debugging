Option Strict On

Class Test

    Public Shared Sub Main()
        'Create a trace listener that writes trace information to the console
        Dim objTraceToConsole As New TextWriterTraceListener(System.Console.Out)
        'Add the new trace listener to the collection of listeners
        Trace.Listeners.Add(objTraceToConsole)
        'Write trace information to all listeners, including the console
        Trace.WriteLine("This trace information is for all listeners")
        'Write trace information to just the console listener
        objTraceToConsole.WriteLine _
        ("This trace information is just for the console listener")
        'Pause application to see console output
        Console.ReadLine()
        'Finish and clean up our console listener
        objTraceToConsole.Flush()
        objTraceToConsole.Close()
        objTraceToConsole.Dispose()
    End Sub

End Class