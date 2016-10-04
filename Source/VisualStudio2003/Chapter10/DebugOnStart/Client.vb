Option Strict On

Imports System.Net
Imports System.Text
Imports System.Threading
Imports System
Imports System.IO
Imports System.ServiceProcess

Public Class Client

    Private ThisClient As Sockets.Socket = Nothing
    Private ReadBuffer(16384) As Byte
    Private PageBuffer(16384) As Byte
    Private EmptyBuffer() As Byte = Encoding.ASCII.GetBytes(Space$(1024))

    Public Sub New(ByVal NewClient As Sockets.Socket)

        Me.ThisClient = NewClient

    End Sub

    Public Sub Start()
        Dim HtmlStream As StreamReader, PageRequest As String, PageText As String

        Try
            Me.ThisClient.Receive(Me.ReadBuffer, Me.ReadBuffer.Length, Sockets.SocketFlags.None)
            PageRequest = ReturnPage(Encoding.ASCII.GetString(Me.ReadBuffer))
            'Open requested page
            HtmlStream = New StreamReader(PageRequest)
            PageText = HtmlStream.ReadToEnd()
            HtmlStream.Close()
            'If status page, display services
            PageText = PageText.Replace("#1#", Environment.MachineName & " (" & Me.ThisClient.LocalEndPoint.ToString & ")")
            PageText = PageText.Replace("#2#", Me.DisplayServices)
            'Display requested page
            Trace.WriteLine("Sending page " & PageRequest)
            Me.PageBuffer = Encoding.ASCII.GetBytes(PageText)
            Me.ThisClient.Send(Me.PageBuffer, Me.PageBuffer.Length, Sockets.SocketFlags.None)
            Me.Close()

        Catch Exc As Sockets.SocketException
            Trace.WriteLine(Exc.Message)
            Close()

        End Try

    End Sub

    Private Function ReturnPage(ByVal PageRequest As String) As String
        Dim HtmlPage As String, IndexStart As Int32, IndexEnd As Int32

        'Extract page
        IndexStart = InStr(PageRequest, "/") + 1
        IndexEnd = InStr(IndexStart, PageRequest, " ")
        HtmlPage = Mid$(PageRequest, IndexStart, IndexEnd - IndexStart)
        'add suffix if necessary
        If Right$(HtmlPage, 5) <> ".html" Then
            HtmlPage += ".html"
        End If
        'Add local path
        HtmlPage = Me.ReturnPath & HtmlPage
        'Check page exists
        If File.Exists(HtmlPage) = False Then
            HtmlPage = Me.ReturnPath & "error.html"
        End If
        'Return page
        Trace.WriteLine("Page requested: " & HtmlPage)
        Return HtmlPage

    End Function

    Private Function ReturnPath() As String

        If Right$(AppDomain.CurrentDomain.BaseDirectory, 1) = "\" Then
            Return AppDomain.CurrentDomain.BaseDirectory
        Else
            Return AppDomain.CurrentDomain.BaseDirectory & "\"
        End If

    End Function

    Private Function DisplayServices() As String
        Dim ThisService As ServiceController, TempPage As String

        For Each ThisService In ServiceController.GetServices
            TempPage += "<tr>" & Environment.NewLine
            'Add service name
            TempPage += "<td width=" & Chr(34) & "200" & Chr(34) & "> <div align=" & Chr(34) & "left" & Chr(34)
            TempPage += "><font size=" & Chr(34) & "2" & Chr(34) & ">"
            TempPage += ThisService.ServiceName & "</font></div></td>" & Environment.NewLine
            'Add service friendly name
            TempPage += "<td width=" & Chr(34) & "430" & Chr(34) & "> <div align=" & Chr(34) & "left" & Chr(34)
            TempPage += "><font size=" & Chr(34) & "2" & Chr(34) & ">"
            TempPage += ThisService.DisplayName & "</font></div></td>" & Environment.NewLine
            'Add service status
            TempPage += "<td width=" & Chr(34) & "100" & Chr(34) & "> <div align=" & Chr(34) & "left" & Chr(34)
            TempPage += "><font size=" & Chr(34) & "2" & Chr(34) & ">"
            TempPage += ThisService.Status.ToString & "</font></div></td>" & Environment.NewLine
            TempPage += "</tr>" & Environment.NewLine
            'Finished with this service
            ThisService.Dispose()
        Next

        Return TempPage

    End Function

    Public Function IsConnected() As Boolean

        If Me.ThisClient Is Nothing Then
            Return False
        Else
            Return Me.ThisClient.Connected
        End If

    End Function

    Public Sub Close()

        If Not (Me.ThisClient Is Nothing) Then
            Trace.WriteLine("Closing client socket")
            Me.ThisClient.Shutdown(Sockets.SocketShutdown.Both)
            Me.ThisClient.Close()
            Me.ThisClient = Nothing
        End If

    End Sub

End Class
