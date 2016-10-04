Option Strict On

Imports System.Xml
Imports System.Web.Services
Imports System.Web.Services.Protocols


<System.Web.Services.WebService(Namespace:="http://debugvb.net/TimeServer/TimeService")> _
Public Class TimeService : Inherits System.Web.Services.WebService

#Region " Web Services Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Web Services Designer.
        InitializeComponent()

        'Add your own initialization code after the InitializeComponent() call

    End Sub

    'Required by the Web Services Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'CODEGEN: This procedure is required by the Web Services Designer
        'Do not modify it using the code editor.
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

#End Region

    <WebMethod()> _
    Public Function CurrentTime() As String
        Return System.DateTime.Now.ToLongTimeString
    End Function

    <WebMethod()> _
    Public Sub ThrowExceptionRaw()
        'This method throws a raw exception
        Dim Test As Object
        Test.ToString()

    End Sub

    <WebMethod(), SoapMonitor()> _
    Public Sub ThrowExceptionCustom()
        'This method throws a customized exception
        Try
            Dim Test As Object
            Test.ToString()
        Catch Exc As Exception
            Throw CustomException(Exc)
        End Try

    End Sub

    Private Function CustomException(ByVal Exc As Exception) As SoapException
        Dim doc As New System.Xml.XmlDocument

        'Create detail node
        Dim DetailNode As System.Xml.XmlNode = _
                        doc.CreateNode(XmlNodeType.Element, _
                                       SoapException.DetailElementName.Name, _
                                       SoapException.DetailElementName.Namespace)

        'Add original exception type
        Dim ExcType As System.Xml.XmlNode = _
                           doc.CreateNode(XmlNodeType.Element, _
                                          "ExceptionType", _
                                          SoapException.DetailElementName.Namespace)
        If Context.Request.UserHostAddress = "127.0.0.1" Then
            ExcType.InnerText = Exc.GetType.ToString
        Else
            ExcType.InnerText = "SoapException"
        End If

        'Add original exception message
        Dim ExcMessage As System.Xml.XmlNode = _
                       doc.CreateNode(XmlNodeType.Element, _
                                      "ExceptionMessage", _
                                      SoapException.DetailElementName.Namespace)
        If Context.Request.UserHostAddress = "127.0.0.1" Then
            ExcMessage.InnerText = Exc.Message
        Else
            ExcMessage.InnerText = "Error - no details available"
        End If

        'Add original exception stack trace
        Dim ExcStackTrace As System.Xml.XmlNode = _
                       doc.CreateNode(XmlNodeType.Element, _
                                      "ExceptionTrace", _
                                      SoapException.DetailElementName.Namespace)
        If Context.Request.UserHostAddress = "127.0.0.1" Then
            ExcStackTrace.InnerText = Exc.StackTrace
        Else
            ExcStackTrace.InnerText = "No stack trace available"
        End If

        'Append the extra details to main detail node
        DetailNode.AppendChild(ExcType)
        DetailNode.AppendChild(ExcMessage)
        DetailNode.AppendChild(ExcStackTrace)

        'Return new custom SoapException
        Return New SoapException("", SoapException.ServerFaultCode, _
                                 Context.Request.Url.AbsoluteUri, DetailNode)

    End Function

End Class

