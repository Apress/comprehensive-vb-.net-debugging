Option Strict On
Imports System.Threading
Imports System.Runtime.Remoting.Messaging

Public Class MainForm : Inherits System.Windows.Forms.Form

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
    Friend WithEvents cmdCalc As System.Windows.Forms.Button
    Friend WithEvents lblResult As System.Windows.Forms.Label
    Friend WithEvents txtNumber As System.Windows.Forms.TextBox
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtInterval As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdCalc = New System.Windows.Forms.Button
        Me.txtNumber = New System.Windows.Forms.TextBox
        Me.lblResult = New System.Windows.Forms.Label
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtInterval = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'cmdCalc
        '
        Me.cmdCalc.Location = New System.Drawing.Point(160, 8)
        Me.cmdCalc.Name = "cmdCalc"
        Me.cmdCalc.Size = New System.Drawing.Size(144, 40)
        Me.cmdCalc.TabIndex = 0
        Me.cmdCalc.Text = "Accumulate"
        '
        'txtNumber
        '
        Me.txtNumber.Location = New System.Drawing.Point(24, 24)
        Me.txtNumber.Name = "txtNumber"
        Me.txtNumber.Size = New System.Drawing.Size(112, 22)
        Me.txtNumber.TabIndex = 1
        Me.txtNumber.Text = "10000"
        '
        'lblResult
        '
        Me.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblResult.Location = New System.Drawing.Point(16, 128)
        Me.lblResult.Name = "lblResult"
        Me.lblResult.Size = New System.Drawing.Size(288, 40)
        Me.lblResult.TabIndex = 2
        Me.lblResult.Text = "Result is shown here"
        '
        'cmdCancel
        '
        Me.cmdCancel.Enabled = False
        Me.cmdCancel.Location = New System.Drawing.Point(160, 64)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(144, 40)
        Me.cmdCancel.TabIndex = 3
        Me.cmdCancel.Text = "Cancel"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(24, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 16)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Number"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(24, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(120, 16)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Display interval"
        '
        'txtInterval
        '
        Me.txtInterval.Location = New System.Drawing.Point(24, 80)
        Me.txtInterval.Name = "txtInterval"
        Me.txtInterval.Size = New System.Drawing.Size(112, 22)
        Me.txtInterval.TabIndex = 5
        Me.txtInterval.Text = "100"
        '
        'MainForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.ClientSize = New System.Drawing.Size(320, 212)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtInterval)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.lblResult)
        Me.Controls.Add(Me.txtNumber)
        Me.Controls.Add(Me.cmdCalc)
        Me.Name = "MainForm"
        Me.Text = "GUI multithreading"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Delegate Sub CalcDelegate(ByVal AnyNumber As Int32)
    Private Delegate Sub ProgressDelegate(ByVal CurrentTotal As Decimal, ByVal NumberReached As Int32, ByRef CancelRequest As Boolean)
    Private m_CancelRequested As Boolean = False

    Private Sub ButtonCalc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCalc.Click

        'Init calculation
        Me.cmdCalc.Enabled = False
        Me.cmdCancel.Enabled = True
        m_CancelRequested = False

        'Use asynch delegate to launch new thread from the thread pool
        Dim CalcAccumulation As CalcDelegate = New CalcDelegate(AddressOf CalculateAccumulation)
        CalcAccumulation.BeginInvoke(Convert.ToInt32(Me.txtNumber.Text), AddressOf CalcComplete, Nothing)

    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        'Request has been cancelled
        m_CancelRequested = True

    End Sub

    Private Sub CalculateAccumulation(ByVal NumberToAccumulate As Int32)
        Dim CalcObject As New Calc(NumberToAccumulate), CurrentTotal As Decimal = 0
        Dim CancelRequested As Boolean = False

        With CalcObject

            Do While .NumberReached <= NumberToAccumulate
                CurrentTotal = .Accumulate(Convert.ToInt32(Me.txtInterval.Text))
                ShowProgress(CurrentTotal, .NumberReached, CancelRequested)
                'Throw New ApplicationException("Test exception")
                If CancelRequested = True Then
                    Exit Do
                End If
            Loop

        End With

    End Sub

    Private Sub ShowProgress(ByVal CurrentAccumulation As Decimal, ByVal NumberReached As Int32, ByRef CancelRequest As Boolean)

        If Me.InvokeRequired = True Then

            'Transfer to GUI thread to show progress
            Dim CancelRequested As Object = False
            Dim SP As ProgressDelegate = New ProgressDelegate(AddressOf ShowProgress)
            Dim Arguments() As Object = New Object() {CurrentAccumulation, NumberReached, CancelRequested}
            Me.Invoke(SP, Arguments)
            CancelRequest = DirectCast(CancelRequested, Boolean)

        Else

            'We're on the GUI thread, so just show progress
            With Me.lblResult
                .Text = "Number reached: " & NumberReached.ToString
                .Text += Environment.NewLine
                .Text += "Accumulated total: " & CurrentAccumulation.ToString
            End With

            'Pause for a short time to allow user to read display 
            Thread.CurrentThread.Sleep(100)

            'Return any cancellation request
            CancelRequest = m_CancelRequested

        End If

    End Sub

    Private Sub CalcComplete(ByVal CalcResult As System.IAsyncResult)
        Dim Result As AsyncResult = CType(CalcResult, AsyncResult)
        Dim OriginalDelegate As CalcDelegate = CType(Result.AsyncDelegate, CalcDelegate)

        'Called when asynch thread completes
        Me.cmdCalc.Enabled = True
        Me.cmdCancel.Enabled = False

        Try
            'Find out if anything dodgy happened in the async thread
            OriginalDelegate.EndInvoke(CalcResult)
        Catch Exc As ApplicationException
            MsgBox(Exc.Message, MsgBoxStyle.OKOnly, "Async thread exception")
        End Try

    End Sub

End Class
