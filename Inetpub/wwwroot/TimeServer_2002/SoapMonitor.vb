Option Strict On
Imports System.Web.Services.Protocols
Imports System.IO

Public Class SoapMonitor : Inherits SoapExtension

    Private OldStream As Stream
    Private NewStream As Stream

    Public Overloads Overrides Function GetInitializer(ByVal serviceType As System.Type) As Object
    End Function

    Public Overloads Overrides Function GetInitializer(ByVal methodInfo As System.Web.Services.Protocols.LogicalMethodInfo, ByVal attribute As System.Web.Services.Protocols.SoapExtensionAttribute) As Object
    End Function

    Public Overrides Sub Initialize(ByVal initializer As Object)
    End Sub

    Public Overrides Sub ProcessMessage(ByVal Message As System.Web.Services.Protocols.SoapMessage)
        'Called after incoming or outgoing data 
        'has been serialized into SOAP message
        Dim Logfile As New FileStream("C:\test\test.log", FileMode.Append, FileAccess.Write)
        Dim LogfileWriter As New StreamWriter(Logfile)

        Select Case Message.Stage

            Case SoapMessageStage.BeforeDeserialize
                'Write header details
                With LogfileWriter
                    .WriteLine("******************************************************")
                    .WriteLine("Web service: " & Message.Url)
                    .WriteLine("Web method: " & Message.Action)
                    .WriteLine("Called at " & DateTime.Now)
                    .WriteLine("Message stage: SOAP REQUEST (" & Message.Stage.ToString & ")")
                    .WriteLine()
                    .Flush()
                End With
                'Write incoming SOAP request message
                CopyStream(OldStream, NewStream)
                NewStream.Position = 0
                CopyStream(NewStream, Logfile)
                NewStream.Position = 0
                'Blank separating line
                LogfileWriter.WriteLine()

            Case SoapMessageStage.AfterSerialize
                'Write header details
                With LogfileWriter
                    .WriteLine("******************************************************")
                    .WriteLine("Web service: " & Message.Url)
                    .WriteLine("Web method: " & Message.Action)
                    .WriteLine("Called at " & DateTime.Now)
                    .WriteLine("Message stage: SOAP RESPONSE (" & Message.Stage.ToString & ")")
                    .Write("Exception thrown? ")
                    If Message.Exception Is Nothing Then
                        .WriteLine("No")
                    Else
                        .WriteLine("Yes")
                    End If
                    .WriteLine()
                    .Flush()
                End With
                'Write outgoing SOAP response message
                NewStream.Position = 0
                CopyStream(NewStream, Logfile)
                NewStream.Position = 0
                CopyStream(NewStream, OldStream)
                'Blank separating line
                LogfileWriter.WriteLine()

        End Select

        Logfile.Flush()
        Logfile.Close()

    End Sub

    Public Overrides Function ChainStream(ByVal stream As System.IO.Stream) As System.IO.Stream

        OldStream = stream
        NewStream = New MemoryStream
        Return NewStream

    End Function

    Private Sub CopyStream(ByVal FromStream As Stream, ByVal ToStream As Stream)
        Dim FromReader As TextReader = New StreamReader(FromStream)
        Dim ToWriter As TextWriter = New StreamWriter(ToStream)

        ToWriter.WriteLine(FromReader.ReadToEnd)
        ToWriter.Flush()

    End Sub

End Class

Public Class SoapMonitorAttribute : Inherits SoapExtensionAttribute
    'Add this custom attribute to any web method where you want
    'to monitor the SOAP request and response messages

    Public Overrides ReadOnly Property ExtensionType() As System.Type
        Get
            Return GetType(SoapMonitor)
        End Get
    End Property

    Public Overrides Property Priority() As Integer
        Get
            Return 0
        End Get
        Set(ByVal Value As Integer)
        End Set
    End Property

End Class