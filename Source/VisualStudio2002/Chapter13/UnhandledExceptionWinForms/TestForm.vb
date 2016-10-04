Public Class TestForm
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents FireException As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.FireException = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'FireException
        '
        Me.FireException.Location = New System.Drawing.Point(48, 136)
        Me.FireException.Name = "FireException"
        Me.FireException.Size = New System.Drawing.Size(184, 56)
        Me.FireException.TabIndex = 0
        Me.FireException.Text = "&Fire exception"
        '
        'TestForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.ClientSize = New System.Drawing.Size(292, 272)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.FireException})
        Me.Name = "TestForm"
        Me.Text = "WinForms exception demo"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub FireException_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FireException.Click
        Dim objTest As Object
        objTest.GetType()
    End Sub

End Class
