Public Class DebugDemo
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
    Friend WithEvents lblTwo As System.Windows.Forms.Label
    Friend WithEvents lblOne As System.Windows.Forms.Label
    Friend WithEvents boxSort As System.Windows.Forms.GroupBox
    Friend WithEvents Timing As System.Windows.Forms.Label
    Friend WithEvents StartSort As System.Windows.Forms.Button
    Friend WithEvents MaxValue As System.Windows.Forms.TextBox
    Friend WithEvents NumberItems As System.Windows.Forms.TextBox
    Friend WithEvents SortCounting As System.Windows.Forms.RadioButton
    Friend WithEvents SortQuick As System.Windows.Forms.RadioButton
    Friend WithEvents SortSelection As System.Windows.Forms.RadioButton
    Friend WithEvents SortBubble As System.Windows.Forms.RadioButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lblTwo = New System.Windows.Forms.Label()
        Me.lblOne = New System.Windows.Forms.Label()
        Me.StartSort = New System.Windows.Forms.Button()
        Me.MaxValue = New System.Windows.Forms.TextBox()
        Me.NumberItems = New System.Windows.Forms.TextBox()
        Me.boxSort = New System.Windows.Forms.GroupBox()
        Me.SortCounting = New System.Windows.Forms.RadioButton()
        Me.SortQuick = New System.Windows.Forms.RadioButton()
        Me.SortSelection = New System.Windows.Forms.RadioButton()
        Me.SortBubble = New System.Windows.Forms.RadioButton()
        Me.Timing = New System.Windows.Forms.Label()
        Me.boxSort.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTwo
        '
        Me.lblTwo.Location = New System.Drawing.Point(32, 96)
        Me.lblTwo.Name = "lblTwo"
        Me.lblTwo.Size = New System.Drawing.Size(112, 30)
        Me.lblTwo.TabIndex = 14
        Me.lblTwo.Text = "Max item value"
        '
        'lblOne
        '
        Me.lblOne.Location = New System.Drawing.Point(32, 40)
        Me.lblOne.Name = "lblOne"
        Me.lblOne.Size = New System.Drawing.Size(112, 30)
        Me.lblOne.TabIndex = 12
        Me.lblOne.Text = "Number of items"
        '
        'StartSort
        '
        Me.StartSort.Location = New System.Drawing.Point(280, 285)
        Me.StartSort.Name = "StartSort"
        Me.StartSort.Size = New System.Drawing.Size(92, 39)
        Me.StartSort.TabIndex = 10
        Me.StartSort.Text = "Start sort"
        '
        'MaxValue
        '
        Me.MaxValue.Location = New System.Drawing.Point(160, 96)
        Me.MaxValue.Name = "MaxValue"
        Me.MaxValue.Size = New System.Drawing.Size(72, 22)
        Me.MaxValue.TabIndex = 13
        Me.MaxValue.Text = "10000"
        '
        'NumberItems
        '
        Me.NumberItems.Location = New System.Drawing.Point(160, 40)
        Me.NumberItems.Name = "NumberItems"
        Me.NumberItems.Size = New System.Drawing.Size(72, 22)
        Me.NumberItems.TabIndex = 11
        Me.NumberItems.Text = "1000"
        '
        'boxSort
        '
        Me.boxSort.Controls.AddRange(New System.Windows.Forms.Control() {Me.SortCounting, Me.SortQuick, Me.SortSelection, Me.SortBubble})
        Me.boxSort.Location = New System.Drawing.Point(280, 16)
        Me.boxSort.Name = "boxSort"
        Me.boxSort.Size = New System.Drawing.Size(232, 247)
        Me.boxSort.TabIndex = 9
        Me.boxSort.TabStop = False
        Me.boxSort.Text = "Sort algorithm"
        '
        'SortCounting
        '
        Me.SortCounting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SortCounting.Location = New System.Drawing.Point(41, 178)
        Me.SortCounting.Name = "SortCounting"
        Me.SortCounting.Size = New System.Drawing.Size(164, 29)
        Me.SortCounting.TabIndex = 3
        Me.SortCounting.Text = "Counting sort"
        '
        'SortQuick
        '
        Me.SortQuick.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SortQuick.Location = New System.Drawing.Point(41, 128)
        Me.SortQuick.Name = "SortQuick"
        Me.SortQuick.Size = New System.Drawing.Size(164, 30)
        Me.SortQuick.TabIndex = 2
        Me.SortQuick.Text = "Quick sort"
        '
        'SortSelection
        '
        Me.SortSelection.Location = New System.Drawing.Point(41, 79)
        Me.SortSelection.Name = "SortSelection"
        Me.SortSelection.Size = New System.Drawing.Size(164, 30)
        Me.SortSelection.TabIndex = 1
        Me.SortSelection.Text = "Selection sort"
        '
        'SortBubble
        '
        Me.SortBubble.Checked = True
        Me.SortBubble.Location = New System.Drawing.Point(41, 30)
        Me.SortBubble.Name = "SortBubble"
        Me.SortBubble.Size = New System.Drawing.Size(164, 29)
        Me.SortBubble.TabIndex = 0
        Me.SortBubble.TabStop = True
        Me.SortBubble.Text = "Bubble sort"
        '
        'Timing
        '
        Me.Timing.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Timing.Location = New System.Drawing.Point(400, 288)
        Me.Timing.Name = "Timing"
        Me.Timing.Size = New System.Drawing.Size(112, 32)
        Me.Timing.TabIndex = 15
        '
        'DebugDemo
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.ClientSize = New System.Drawing.Size(544, 348)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.Timing, Me.lblTwo, Me.lblOne, Me.StartSort, Me.MaxValue, Me.NumberItems, Me.boxSort})
        Me.Name = "DebugDemo"
        Me.Text = "Debug demo"
        Me.boxSort.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private m_SortType As DemoSort.SortType = DemoSort.SortType.BubbleSort

    Private Sub StartSort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartSort.Click
        Dim SortObject As New DemoSort()

        'Show that we're busy
        Cursor.Current = Cursors.WaitCursor
        Me.StartSort.Enabled = False

        'Build a list to sort
        Me.Timing.Text = "Building list to sort..."
        Me.Timing.Refresh()
        SortObject.ListBuild(Convert.ToInt32(Me.NumberItems.Text), 1, Convert.ToInt32(Me.MaxValue.Text))

        'Do the sort and report timing
        Me.Timing.Text = "Performing sort..."
        Me.Timing.Refresh()
        Me.Timing.Text = SortObject.DoSort(m_SortType).ToString & " seconds"

        'Show that we've finished
        Me.StartSort.Enabled = True
        Cursor.Current = Cursors.Default

    End Sub

    Private Sub SortBubble_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SortBubble.CheckedChanged
        m_SortType = DemoSort.SortType.BubbleSort
    End Sub

    Private Sub SortSelection_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SortSelection.CheckedChanged
        m_SortType = DemoSort.SortType.SelectionSort
    End Sub

    Private Sub SortQuick_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SortQuick.CheckedChanged
        m_SortType = DemoSort.SortType.QuickSort
    End Sub

    Private Sub SortCounting_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SortCounting.CheckedChanged
        m_SortType = DemoSort.SortType.CountingSort
    End Sub

End Class
