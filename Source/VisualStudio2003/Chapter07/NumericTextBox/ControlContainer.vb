Option Strict On

Public Class ControlContainer
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Switch on control debugging
        Me.CustomControl.DebugMode = True

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
    Friend WithEvents CustomControl As DebugDemo.NumericTextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.CustomControl = New DebugDemo.NumericTextBox()
        Me.SuspendLayout()
        '
        'CustomControl
        '
        Me.CustomControl.DebugMode = False
        Me.CustomControl.Location = New System.Drawing.Point(80, 112)
        Me.CustomControl.Name = "CustomControl"
        Me.CustomControl.TabIndex = 0
        Me.CustomControl.Text = ""
        '
        'ControlContainer
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.ClientSize = New System.Drawing.Size(292, 272)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.CustomControl})
        Me.Name = "ControlContainer"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub ShowDebugMessage(ByVal DebugMethod As String, ByVal DebugComment As String) Handles CustomControl.DebugMessage
        'Show the debug message whenever user types non-numeric character
        MsgBox(DebugComment, MsgBoxStyle.OKOnly Or MsgBoxStyle.Information, DebugMethod)
    End Sub

End Class
