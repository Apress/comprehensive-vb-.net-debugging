Option Strict On
Imports System.Threading
Imports System.Diagnostics

Public Class MainForm : Inherits System.Windows.Forms.Form

    Private UserThreads As New Collection

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
    Friend WithEvents ProcessList As System.Windows.Forms.ListBox
    Friend WithEvents LabelThread As System.Windows.Forms.Label
    Friend WithEvents LabelProcess As System.Windows.Forms.Label
    Friend WithEvents ThreadList As System.Windows.Forms.ListView
    Friend WithEvents LabelThreadName As System.Windows.Forms.Label
    Friend WithEvents DisplayTimer As System.Windows.Forms.Timer
    Friend WithEvents LaunchThread As System.Windows.Forms.Button
    Friend WithEvents Splitter As System.Windows.Forms.Label
    Friend WithEvents ManagedThreadList As System.Windows.Forms.ListView
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ProcessList = New System.Windows.Forms.ListBox
        Me.LabelThread = New System.Windows.Forms.Label
        Me.LabelProcess = New System.Windows.Forms.Label
        Me.ThreadList = New System.Windows.Forms.ListView
        Me.LabelThreadName = New System.Windows.Forms.Label
        Me.DisplayTimer = New System.Windows.Forms.Timer(Me.components)
        Me.LaunchThread = New System.Windows.Forms.Button
        Me.Splitter = New System.Windows.Forms.Label
        Me.ManagedThreadList = New System.Windows.Forms.ListView
        Me.SuspendLayout()
        '
        'ProcessList
        '
        Me.ProcessList.ItemHeight = 16
        Me.ProcessList.Location = New System.Drawing.Point(16, 40)
        Me.ProcessList.Name = "ProcessList"
        Me.ProcessList.Size = New System.Drawing.Size(120, 292)
        Me.ProcessList.Sorted = True
        Me.ProcessList.TabIndex = 0
        '
        'LabelThread
        '
        Me.LabelThread.Location = New System.Drawing.Point(168, 8)
        Me.LabelThread.Name = "LabelThread"
        Me.LabelThread.Size = New System.Drawing.Size(168, 24)
        Me.LabelThread.TabIndex = 2
        Me.LabelThread.Text = "Win32 threads in process:"
        '
        'LabelProcess
        '
        Me.LabelProcess.Location = New System.Drawing.Point(16, 8)
        Me.LabelProcess.Name = "LabelProcess"
        Me.LabelProcess.Size = New System.Drawing.Size(96, 24)
        Me.LabelProcess.TabIndex = 3
        Me.LabelProcess.Text = "Processes"
        '
        'ThreadList
        '
        Me.ThreadList.Location = New System.Drawing.Point(176, 40)
        Me.ThreadList.Name = "ThreadList"
        Me.ThreadList.Size = New System.Drawing.Size(600, 296)
        Me.ThreadList.TabIndex = 4
        Me.ThreadList.View = System.Windows.Forms.View.Details
        '
        'LabelThreadName
        '
        Me.LabelThreadName.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelThreadName.Location = New System.Drawing.Point(336, 8)
        Me.LabelThreadName.Name = "LabelThreadName"
        Me.LabelThreadName.Size = New System.Drawing.Size(128, 24)
        Me.LabelThreadName.TabIndex = 5
        Me.LabelThreadName.Text = "Thread name"
        '
        'DisplayTimer
        '
        Me.DisplayTimer.Interval = 1000
        '
        'LaunchThread
        '
        Me.LaunchThread.Location = New System.Drawing.Point(16, 384)
        Me.LaunchThread.Name = "LaunchThread"
        Me.LaunchThread.Size = New System.Drawing.Size(120, 40)
        Me.LaunchThread.TabIndex = 6
        Me.LaunchThread.Text = "Launch thread"
        '
        'Splitter
        '
        Me.Splitter.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Splitter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Splitter.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Splitter.Location = New System.Drawing.Point(16, 360)
        Me.Splitter.Name = "Splitter"
        Me.Splitter.Size = New System.Drawing.Size(760, 3)
        Me.Splitter.TabIndex = 7
        '
        'ManagedThreadList
        '
        Me.ManagedThreadList.Location = New System.Drawing.Point(176, 384)
        Me.ManagedThreadList.Name = "ManagedThreadList"
        Me.ManagedThreadList.Size = New System.Drawing.Size(600, 296)
        Me.ManagedThreadList.TabIndex = 8
        Me.ManagedThreadList.View = System.Windows.Forms.View.Details
        '
        'MainForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.ClientSize = New System.Drawing.Size(800, 700)
        Me.Controls.Add(Me.ManagedThreadList)
        Me.Controls.Add(Me.Splitter)
        Me.Controls.Add(Me.LaunchThread)
        Me.Controls.Add(Me.LabelThreadName)
        Me.Controls.Add(Me.ThreadList)
        Me.Controls.Add(Me.LabelProcess)
        Me.Controls.Add(Me.LabelThread)
        Me.Controls.Add(Me.ProcessList)
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Thread monitor"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Setup thread display
        With Me.ThreadList
            .Columns.Add("Id", 50, HorizontalAlignment.Right)
            .Columns.Add("Priority", 100, HorizontalAlignment.Left)
            .Columns.Add("State", 80, HorizontalAlignment.Left)
            .Columns.Add("Wait reason", 120, HorizontalAlignment.Left)
            .Columns.Add("Time in app", 100, HorizontalAlignment.Left)
            .Columns.Add("Total time", 100, HorizontalAlignment.Left)
        End With

        'Setup managed thread display
        With Me.ManagedThreadList
            .Columns.Add("Id", 50, HorizontalAlignment.Right)
            .Columns.Add("Name", 150, HorizontalAlignment.Left)
            .Columns.Add("Priority", 100, HorizontalAlignment.Left)
            .Columns.Add("State", 120, HorizontalAlignment.Left)
            .Columns.Add("Background?", 100, HorizontalAlignment.Left)
            .Columns.Add("Pooled?", 80, HorizontalAlignment.Left)
        End With

        'Show processes
        UpdateProcessDisplay()
        'Show Win32 threads for first process
        Me.ProcessList.SetSelected(0, True)
        'Set thread displays to update regularly
        Me.DisplayTimer.Interval = 1000
        Me.DisplayTimer.Enabled = True

    End Sub

    Private Sub ProcessList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProcessList.SelectedIndexChanged

        'Display threads for process chosen by end-user
        UpdateWin32ThreadDisplay(DirectCast(ProcessList.SelectedItem, Process).Id)

    End Sub

    Private Sub LaunchThread_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LaunchThread.Click

        Dim TempThread As New Thread(New ThreadStart(AddressOf FactorialCalc.Factorial))
        'User wants new thread - launch and add to collection
        With TempThread
            .Name = "User thread " & UserThreads.Count.ToString
            .Priority = ThreadPriority.BelowNormal
            UserThreads.Add(TempThread, .GetHashCode.ToString)
            .Start()
        End With

    End Sub

    Private Sub DisplayTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisplayTimer.Tick

        'Re-display threads for currently-selected process
        UpdateUserThreadDisplay()
        UpdateWin32ThreadDisplay(DirectCast(ProcessList.SelectedItem, Process).Id)

    End Sub

    Private Sub UpdateProcessDisplay()
        Dim MachineProcesses() As Process

        'Retrieve, store and display processes on this machine
        MachineProcesses = Process.GetProcesses()
        With Me.ProcessList
            .DataSource = MachineProcesses
            .DisplayMember = "ProcessName"
        End With

    End Sub

    Private Sub UpdateWin32ThreadDisplay(ByVal SelectedProcessId As Integer)
        Dim SelectedProcess As Process
        Dim ThisThread As ProcessThread, LV_item As ListViewItem

        'Get Win32 threads for this process and display them
        SelectedProcess = Process.GetProcessById(SelectedProcessId)
        LabelThreadName.Text = SelectedProcess.ProcessName

        With Me.ThreadList
            .BeginUpdate()
            .Items.Clear()

            'Iterate through every Win32 thread in this process
            For Each ThisThread In SelectedProcess.Threads

                Try
                    'Add thread id
                    LV_item = New ListViewItem(ThisThread.Id.ToString)
                    'Add thread details
                    With LV_item.SubItems
                        'Thread priority
                        .Add(ThisThread.PriorityLevel.ToString)
                        'Thread state
                        .Add(ThisThread.ThreadState.ToString)
                        'Reason for thread wait
                        If ThisThread.ThreadState = Diagnostics.ThreadState.Wait Then
                            .Add(ThisThread.WaitReason.ToString)
                        Else
                            .Add(vbNullString)
                        End If
                        'Thread time in app
                        .Add(ThisThread.UserProcessorTime.TotalMilliseconds.ToString)
                        'Thread time in OS
                        .Add(ThisThread.TotalProcessorTime.TotalMilliseconds.ToString)
                    End With
                    'Display the thread
                    .Items.Add(LV_item)

                Catch Exc As InvalidOperationException
                    'Thread's disappeared - ignore

                End Try

            Next ThisThread

            .EndUpdate()
        End With

    End Sub

    Private Sub UpdateUserThreadDisplay()
        Dim ThisThread As Threading.Thread, LV_item As ListViewItem

        'Iterate through managed threads for current process
        With Me.ManagedThreadList
            .BeginUpdate()
            .Items.Clear()

            'Iterate through every thread in this process
            For Each ThisThread In UserThreads

                If ThisThread.IsAlive Then

                    Try
                        'Add thread id
                        LV_item = New ListViewItem(ThisThread.GetHashCode.ToString)
                        'Add thread details
                        With LV_item.SubItems
                            'Add thread name
                            .Add(ThisThread.Name)
                            'Thread priority
                            .Add(ThisThread.Priority.ToString)
                            'Thread state
                            .Add(ThisThread.ThreadState.ToString)
                            'Thread is alive?
                            .Add(ThisThread.IsAlive.ToString)
                            'Background thread?
                            .Add(ThisThread.IsBackground.ToString)
                            'Threadpool thread?
                            .Add(ThisThread.IsThreadPoolThread.ToString)
                        End With
                        'Display the thread
                        .Items.Add(LV_item)

                    Catch Exc As Threading.ThreadStateException
                        'Thread's disappeared - ignore
                        UserThreads.Remove(ThisThread.GetHashCode.ToString)

                    End Try

                Else

                    'Thread's dead - remove from collection
                    UserThreads.Remove(ThisThread.GetHashCode.ToString)

                End If

            Next ThisThread

            .EndUpdate()
        End With

    End Sub

End Class
