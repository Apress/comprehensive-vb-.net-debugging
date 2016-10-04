Option Strict On
Imports System.Data.SqlClient

Public Class SqlDebug
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
    Friend WithEvents ConnectionQuery As System.Windows.Forms.Button
    Friend WithEvents ConnectionDestroy As System.Windows.Forms.Button
    Friend WithEvents ConnectionCreate As System.Windows.Forms.Button
    Friend WithEvents ValueToInsert As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ConnectionQuery = New System.Windows.Forms.Button()
        Me.ConnectionDestroy = New System.Windows.Forms.Button()
        Me.ConnectionCreate = New System.Windows.Forms.Button()
        Me.ValueToInsert = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'ConnectionQuery
        '
        Me.ConnectionQuery.Location = New System.Drawing.Point(16, 96)
        Me.ConnectionQuery.Name = "ConnectionQuery"
        Me.ConnectionQuery.Size = New System.Drawing.Size(152, 40)
        Me.ConnectionQuery.TabIndex = 0
        Me.ConnectionQuery.Text = "Insert new row"
        '
        'ConnectionDestroy
        '
        Me.ConnectionDestroy.Location = New System.Drawing.Point(16, 168)
        Me.ConnectionDestroy.Name = "ConnectionDestroy"
        Me.ConnectionDestroy.Size = New System.Drawing.Size(152, 40)
        Me.ConnectionDestroy.TabIndex = 1
        Me.ConnectionDestroy.Text = "Destroy connection"
        '
        'ConnectionCreate
        '
        Me.ConnectionCreate.Location = New System.Drawing.Point(16, 24)
        Me.ConnectionCreate.Name = "ConnectionCreate"
        Me.ConnectionCreate.Size = New System.Drawing.Size(152, 40)
        Me.ConnectionCreate.TabIndex = 2
        Me.ConnectionCreate.Text = "Create connection"
        '
        'ValueToInsert
        '
        Me.ValueToInsert.Location = New System.Drawing.Point(184, 104)
        Me.ValueToInsert.Name = "ValueToInsert"
        Me.ValueToInsert.Size = New System.Drawing.Size(80, 22)
        Me.ValueToInsert.TabIndex = 3
        Me.ValueToInsert.Text = ""
        '
        'SqlDebug
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.ClientSize = New System.Drawing.Size(292, 272)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.ValueToInsert, Me.ConnectionCreate, Me.ConnectionDestroy, Me.ConnectionQuery})
        Me.Name = "SqlDebug"
        Me.Text = "SQL Debugging"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private m_DatabaseConnection As SqlConnection

    Private Sub ConnectionCreation(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectionCreate.Click
        Dim ConnectionString As String, ConnectStatus As String

        'Setup database connection
        ConnectionString = "Initial Catalog = pubs;"
        ConnectionString += "Data Source = CHEETAH;"
        ConnectionString += "Integrated Security = SSPI;"
        ConnectionString += "Pooling = 'false'"

        'Attempt database connection
        Try
            m_DatabaseConnection = New SqlConnection(ConnectionString)
            ConnectStatus = "Connected"
        Catch objException As Exception
            ConnectStatus = "Not connected: " & objException.ToString
            m_DatabaseConnection.Close()
            m_DatabaseConnection.Dispose()
        Finally
            Me.Text = ConnectStatus
        End Try

    End Sub

    Private Sub ConnectionTest(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectionQuery.Click
        Dim QuerySql As String, QueryResult As New DataSet()
        Dim QueryTest As New SqlDataAdapter()

        'Check that we have a valid number
        If Len(Trim$(Me.ValueToInsert.Text)) = 0 Then
            MsgBox("You should enter a number before adding new row", MsgBoxStyle.Information Or MsgBoxStyle.OKOnly, "Ooops")
            Me.ValueToInsert.Focus()
            Exit Sub
        End If

        'Setup and perform database query
        QuerySql = "EXEC sp_DebugExample " & Me.ValueToInsert.Text
        Try
            QueryTest.SelectCommand = New SqlCommand(QuerySql, m_DatabaseConnection)
            QueryTest.Fill(QueryResult)
        Catch objException As Exception
            MsgBox(objException.ToString)
        Finally
            QueryTest.Dispose()
        End Try
    End Sub

    Private Sub ConnectionDestruction(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectionDestroy.Click

        m_DatabaseConnection.Close()
        m_DatabaseConnection.Dispose()
        Me.Text = "Not connected"

    End Sub

End Class
