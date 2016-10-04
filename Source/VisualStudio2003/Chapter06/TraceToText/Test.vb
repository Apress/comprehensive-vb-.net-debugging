Option Strict On
Imports System.IO

Class Test

    Public Shared Sub Main()

        'Create a trace listener that writes trace information to a text file
        Dim objTextFile As Stream = File.Create("TraceListener.txt")
        Dim objTraceToText As New TextWriterTraceListener(objTextFile)
        'Add the new trace listener to the collection of listeners
        Trace.Listeners.Add(objTraceToText)
        'Write trace information to all listeners, including the text file
        Trace.WriteLine("This trace information is for all listeners")
        'Finish and clean up our console listener
        objTraceToText.Flush()
        objTraceToText.Close()
        objTraceToText.Dispose()

    End Sub

End Class