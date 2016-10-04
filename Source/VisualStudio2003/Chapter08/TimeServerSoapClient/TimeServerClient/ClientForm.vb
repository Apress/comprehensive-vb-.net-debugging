Option Strict On
Imports System.Windows.Forms

Public Class ClientForm : Inherits System.Windows.Forms.Form

    Private m_TimeTest As localhost.TimeService

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents GetTime As System.Windows.Forms.Button
    Friend WithEvents ShowTime As System.Windows.Forms.Label
    Friend WithEvents ProxyDebugDemo As System.Windows.Forms.Button
    Friend WithEvents TriggerRawException As System.Windows.Forms.Button
    Friend WithEvents TriggerCustomException As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.GetTime = New System.Windows.Forms.Button
        Me.ShowTime = New System.Windows.Forms.Label
        Me.ProxyDebugDemo = New System.Windows.Forms.Button
        Me.TriggerRawException = New System.Windows.Forms.Button
        Me.TriggerCustomException = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'GetTime
        '
        Me.GetTime.Location = New System.Drawing.Point(40, 40)
        Me.GetTime.Name = "GetTime"
        Me.GetTime.Size = New System.Drawing.Size(160, 56)
        Me.GetTime.TabIndex = 0
        Me.GetTime.Text = "Test time retrieval"
        '
        'ShowTime
        '
        Me.ShowTime.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ShowTime.Location = New System.Drawing.Point(240, 48)
        Me.ShowTime.Name = "ShowTime"
        Me.ShowTime.Size = New System.Drawing.Size(128, 48)
        Me.ShowTime.TabIndex = 1
        '
        'ProxyDebugDemo
        '
        Me.ProxyDebugDemo.Location = New System.Drawing.Point(40, 120)
        Me.ProxyDebugDemo.Name = "ProxyDebugDemo"
        Me.ProxyDebugDemo.Size = New System.Drawing.Size(160, 56)
        Me.ProxyDebugDemo.TabIndex = 2
        Me.ProxyDebugDemo.Text = "Demo proxy debugging"
        '
        'TriggerRawException
        '
        Me.TriggerRawException.Location = New System.Drawing.Point(40, 200)
        Me.TriggerRawException.Name = "TriggerRawException"
        Me.TriggerRawException.Size = New System.Drawing.Size(160, 56)
        Me.TriggerRawException.TabIndex = 4
        Me.TriggerRawException.Text = "Trigger raw exception"
        '
        'TriggerCustomException
        '
        Me.TriggerCustomException.Location = New System.Drawing.Point(40, 280)
        Me.TriggerCustomException.Name = "TriggerCustomException"
        Me.TriggerCustomException.Size = New System.Drawing.Size(160, 56)
        Me.TriggerCustomException.TabIndex = 5
        Me.TriggerCustomException.Text = "Trigger custom exception"
        '
        'ClientForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.ClientSize = New System.Drawing.Size(432, 396)
        Me.Controls.Add(Me.TriggerCustomException)
        Me.Controls.Add(Me.TriggerRawException)
        Me.Controls.Add(Me.ProxyDebugDemo)
        Me.Controls.Add(Me.ShowTime)
        Me.Controls.Add(Me.GetTime)
        Me.Name = "ClientForm"
        Me.Text = "TimeServer client"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub ClientForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Create web service instance (via proxy) and
        'ensure timeout never happens during debugging
        m_TimeTest = New localhost.TimeService
#If DEBUG Then
        m_TimeTest.Timeout = -1
#End If

    End Sub

    Private Sub GetTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetTime.Click
        'Get time details
        Me.ShowTime.Text = m_TimeTest.CurrentTime

    End Sub

    Private Sub ProxyDebugDemo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProxyDebugDemo.Click
        'Trigger exception without catching it
        'Ensure proxy has DebuggerStepthroughAttribute removed
        'This exception will then break into the debugger
        m_TimeTest.ThrowExceptionRaw()

    End Sub

    Private Sub TriggerRawException_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TriggerRawException.Click
        Dim ExceptionMessage As String, Sep As String = Space$(4)
        'Trigger raw exception and show it
        Try
            m_TimeTest.ThrowExceptionRaw()

        Catch SoapExc As System.Web.Services.Protocols.SoapException

            With SoapExc
                ExceptionMessage += "RECEIVED EXCEPTION INFORMATION"
                ExceptionMessage += Environment.NewLine
                ExceptionMessage += "Type: " & Environment.NewLine
                ExceptionMessage += Sep & .GetType.ToString
                ExceptionMessage += Environment.NewLine
                ExceptionMessage += "Message: " & Environment.NewLine
                ExceptionMessage += Sep & .Message
                ExceptionMessage += Environment.NewLine
                ExceptionMessage += "Stack trace: " & Environment.NewLine
                ExceptionMessage += Sep & .StackTrace
                ExceptionMessage += Environment.NewLine
                ExceptionMessage += "SOAP fault: " & Environment.NewLine
                ExceptionMessage += Sep & .Code.ToString
                ExceptionMessage += Environment.NewLine
                ExceptionMessage += "Web service URL: " & Environment.NewLine
                ExceptionMessage += Sep & .Actor.ToString
            End With

            MessageBox.Show(ExceptionMessage, "Web service raw exception", MessageBoxButtons.OK)

        End Try

    End Sub

    Private Sub TriggerCustomException_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TriggerCustomException.Click
        Dim ExceptionMessage As String, Sep As String = Space$(4)
        'Trigger custom exception and show it
        Try
            m_TimeTest.ThrowExceptionCustom()

        Catch SoapExc As System.Web.Services.Protocols.SoapException

            With SoapExc
                ExceptionMessage = "ORIGINAL EXCEPTION INFORMATION"
                ExceptionMessage += Environment.NewLine
                ExceptionMessage += "Type: " & Environment.NewLine
                ExceptionMessage += Sep & .Detail("ExceptionType").InnerText
                ExceptionMessage += Environment.NewLine
                ExceptionMessage += "Message: " & Environment.NewLine
                ExceptionMessage += Sep & .Detail("ExceptionMessage").InnerText
                ExceptionMessage += Environment.NewLine
                ExceptionMessage += "Stack trace: " & Environment.NewLine
                ExceptionMessage += Sep & .Detail("ExceptionTrace").InnerText
                ExceptionMessage += Environment.NewLine
                ExceptionMessage += "SOAP fault: " & Environment.NewLine
                ExceptionMessage += Sep & .Code.ToString
                ExceptionMessage += Environment.NewLine
                ExceptionMessage += "Web service URL: " & Environment.NewLine
                ExceptionMessage += Sep & .Actor.ToString
                ExceptionMessage += Environment.NewLine
                ExceptionMessage += "Raw SOAP message: " & Environment.NewLine
                ExceptionMessage += Sep & .Message
            End With

            MessageBox.Show(ExceptionMessage, "Web service custom exception", MessageBoxButtons.OK)

        End Try

    End Sub

    Private Sub ClientForm_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        'Close web service instance
        m_TimeTest.Dispose()
        m_TimeTest = Nothing

    End Sub

End Class
