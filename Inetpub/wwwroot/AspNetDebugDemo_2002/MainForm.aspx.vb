Public Class MainForm : Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblMain As System.Web.UI.WebControls.Label
    Protected WithEvents lblException As System.Web.UI.WebControls.Label
    Protected WithEvents btnPageLevel As System.Web.UI.WebControls.Button
    Protected WithEvents btnNoLevel As System.Web.UI.WebControls.Button
    Protected WithEvents btnAppLevel As System.Web.UI.WebControls.Button
    Protected WithEvents btnProcLevel As System.Web.UI.WebControls.Button
    Protected WithEvents btnPageTracingOn As System.Web.UI.WebControls.Button
    Protected WithEvents btnAppTracingShow As System.Web.UI.WebControls.Button
    Protected WithEvents lblTrace As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Private Sub btnNoLevel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNoLevel.Click
        'Doesn't catch error.
        'Doesn't deal with error.
        'Displays error using ASP.NET default error page.
        'NB Ensure page-level and app-level error handling 
        'switched-off before testing this procedure.
        Dim Test As Object
        Test.ToString()
    End Sub

    Private Sub btnProcLevel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcLevel.Click
        'Catches error at procedure-level 
        'Deals with error in Catch clause
        'Displays error using Catch clause
        Try
            Dim Test As Object
            Test.ToString()
        Catch ex As Exception
            Me.lblException.Text = ex.Message
        Finally
            Server.ClearError()
        End Try
    End Sub

    Private Sub btnPageLevel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageLevel.Click
        'Catches error at page-level 
        'Deals with error in Me.Page_Error event
        'Displays error using Me.ErrorPage property
        Me.ErrorPage = "CustomErrorPage.aspx"
        Dim Test As Object
        Test.ToString()
    End Sub

    Private Sub btnAppLevel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAppLevel.Click
        'Catches error at application-level 
        'Deals with error in Global.asax.Application_Error event
        'Displays error using default redirect in Web.config
        Dim Test As Object
        Test.ToString()
    End Sub

    Private Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Error
        Trace.Write(Server.GetLastError.Message)
        'Server.ClearError()
    End Sub

    Private Sub btnPageTracingOn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageTracingOn.Click
        With Me.Trace
            .IsEnabled = True
            .TraceMode = TraceMode.SortByTime
            .Write("Information", "This is some custom trace information")
            .Warn("Warning", "This is a custom trace warning")
        End With
    End Sub

    Private Sub btnAppTracingShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAppTracingShow.Click
        Response.Redirect("trace.axd")
    End Sub

End Class
