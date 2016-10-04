Option Strict On
Imports System.Diagnostics
Imports System.Threading
Imports System.Text
Imports System.IO
Imports System

Public Module Test

    Public Sub Main()
        'Activate the custom trace listener and its output file stream
        Dim objPerformOutput As Stream = File.Create("Performance.xml")
        Dim objPerformRecorder As New TimerTraceListener(objPerformOutput)
        Trace.Listeners.Add(objPerformRecorder)
        'Add this statement at the start of every monitored procedure
        Trace.WriteLine("Entering procedure")
        'Add this statement at the end of every monitored procedure
        Trace.WriteLine("Leaving procedure")
        'Add these statements when closing the custom trace listener
        objPerformRecorder.Flush()
        objPerformRecorder.Close()
    End Sub

End Module

Public Class TimerTraceListener : Inherits TextWriterTraceListener

    Private msbBuffer As New StringBuilder()

    'All of our constructors are here
    Public Sub New(ByVal Stream As Stream)
        MyBase.New(Stream)
        EmitHeader()
    End Sub

    Public Sub New(ByVal Stream As Stream, ByVal Name As String)
        MyBase.New(Stream, Name)
        EmitHeader()
    End Sub

    Public Sub New(ByVal Writer As TextWriter)
        MyBase.New(Writer)
        EmitHeader()
    End Sub

    Public Sub New(ByVal Writer As TextWriter, ByVal Name As String)
        MyBase.New(Writer, Name)
        EmitHeader()
    End Sub

    Protected Sub EmitHeader()
        'Start tracing performance data
        MyBase.WriteLine("<trace>")
    End Sub

    Public Overloads Overrides Sub Write(ByVal Message As String)
        If msbBuffer Is Nothing Then
            msbBuffer = New StringBuilder()
        End If
        msbBuffer.Append(Message)
    End Sub

    Public Overloads Overrides Sub WriteLine(ByVal Message As String)
        If Not (msbBuffer Is Nothing) Then
            Message = msbBuffer.ToString & Message
            msbBuffer = Nothing
        End If
        EmitMessage(Message)
    End Sub

    Protected Sub EmitMessage(ByVal Message As String)
        'Write performance data
        Dim objNow As DateTime = New System.DateTime().Now
        Dim objCalledFrom As New StackFrame(4)
        MyBase.WriteLine("<entry>")
        'Timing data
        With objNow
            MyBase.WriteLine(CreateTag("time", .ToLongTimeString() & "." & .Millisecond.ToString))
            MyBase.WriteLine(CreateTag("message", Message))
        End With
        'Caller data
        With objCalledFrom.GetMethod
            MyBase.WriteLine(CreateTag("method", .DeclaringType.FullName & "." & .Name))
        End With
        'Thread data
        MyBase.WriteLine(CreateTag("thread", Thread.CurrentThread.Name))
        'Finish log entry
        MyBase.WriteLine("</entry>")
    End Sub

    Protected Function CreateTag(ByVal TagName As String, ByVal TagContents As String) As String
        'Return an XML tag
        Return "<" & TagName & ">" & TagContents & "</" & TagName & ">"
    End Function

    Public Overrides Sub Flush()
        'Flush any remaining information to file
        If Not (msbBuffer Is Nothing) Then
            EmitMessage(msbBuffer.ToString)
            msbBuffer = Nothing
        End If
        'Don’t forget to chain to MyBase
        MyBase.Flush()
    End Sub

    Public Overrides Sub Close()
        'Finish tracing performance data
        MyBase.WriteLine("</trace>")
        Me.Flush()
        'Don’t forget to chain to MyBase
        MyBase.Close()
    End Sub

End Class