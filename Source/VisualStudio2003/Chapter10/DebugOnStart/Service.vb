Option Strict On
Imports System.ServiceProcess

Public Class ServiceAdmin : Inherits System.ServiceProcess.ServiceBase

#Region " Component Designer generated code "

    Private Listener As ServiceListener = Nothing

    Public Sub New()
        MyBase.New()

        ' This call is required by the Component Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call

    End Sub

    'UserService overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    ' The main entry point for the process
    <MTAThread()> _
    Shared Sub Main()

#If DEBUG Then
        Dim DebugService As New ServiceAdmin
        DebugService.OnStart(Nothing)
#Else
        Dim ServicesToRun() As System.ServiceProcess.ServiceBase
        'The following line instantiates just your service
        'ServicesToRun = New ServiceBase() {New ServiceAdmin()}

        'The following line (instead of the previous line) additionally instantiates a dummy service within the same
        'process as your real service. Starting the dummy service allows you to attach to the service process - then
        'you can debug your service's OnStart method by setting a breakpoint before starting your service.
        ServicesToRun = New ServiceBase() {New ServiceAdmin, New DummyService}

        'Start the service
        ServiceBase.Run(ServicesToRun)
#End If

    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: The following procedure is required by the Component Designer
    ' It can be modified using the Component Designer.  
    ' Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
        Me.ServiceName = "ServiceAdmin"
    End Sub

#End Region

    Protected Overrides Sub OnStart(ByVal args() As String)

        Me.Listener = New ServiceListener
        Me.Listener.Start()

    End Sub

    Protected Overrides Sub OnStop()

        If Not (Me.Listener Is Nothing) Then
            Me.Listener.Close()
            Me.Listener = Nothing
        End If

    End Sub

End Class
