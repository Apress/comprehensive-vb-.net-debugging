Option Strict On

Public Class Form1
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
    Friend WithEvents cmdTest As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdTest = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmdTest
        '
        Me.cmdTest.Location = New System.Drawing.Point(64, 104)
        Me.cmdTest.Name = "cmdTest"
        Me.cmdTest.Size = New System.Drawing.Size(152, 80)
        Me.cmdTest.TabIndex = 0
        Me.cmdTest.Text = "Test debug of VB .NET app calling VB6 component"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.ClientSize = New System.Drawing.Size(292, 272)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.cmdTest})
        Me.Name = "Form1"
        Me.Text = "Interop debugging"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmdTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTest.Click
        Dim DebugTest As New ComPrintUtils.ComPrinting()
        DebugTest.PrintPictureToFitPage(DebugTest.CaptureForm(Me.Handle.ToInt32))
        System.Runtime.InteropServices.Marshal.ReleaseComObject(DebugTest)
        DebugTest = Nothing
    End Sub

End Class
