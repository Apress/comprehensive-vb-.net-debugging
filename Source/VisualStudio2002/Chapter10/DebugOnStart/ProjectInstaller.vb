Imports System.ComponentModel
Imports System.Configuration.Install
Imports Microsoft.Win32

<RunInstaller(True)> Public Class ProjectInstaller
    Inherits System.Configuration.Install.Installer

#Region " Component Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Installer overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ServiceProcessInstaller1 As System.ServiceProcess.ServiceProcessInstaller
    Friend WithEvents ServiceInstaller1 As System.ServiceProcess.ServiceInstaller
    Friend WithEvents ServiceInstaller2 As System.ServiceProcess.ServiceInstaller
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ServiceProcessInstaller1 = New System.ServiceProcess.ServiceProcessInstaller
        Me.ServiceInstaller1 = New System.ServiceProcess.ServiceInstaller
        Me.ServiceInstaller2 = New System.ServiceProcess.ServiceInstaller
        '
        'ServiceProcessInstaller1
        '
        Me.ServiceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem
        Me.ServiceProcessInstaller1.Password = Nothing
        Me.ServiceProcessInstaller1.Username = Nothing
        '
        'ServiceInstaller1
        '
        Me.ServiceInstaller1.ServiceName = "ServiceAdmin"
        '
        'ServiceInstaller2
        '
        Me.ServiceInstaller2.ServiceName = "DummyService"
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.ServiceProcessInstaller1, Me.ServiceInstaller1, Me.ServiceInstaller2})

    End Sub

#End Region

    Public Overrides Sub Install(ByVal stateSaver As System.Collections.IDictionary)

        'First do the install
        MyBase.Install(stateSaver)

        'System.Diagnostics.Debugger.Launch()

        Dim SystemKey As RegistryKey = Registry.LocalMachine.OpenSubKey("System")
        Dim ControlSetKey As RegistryKey = SystemKey.OpenSubKey("CurrentControlSet")
        Dim ServicesKey As RegistryKey = ControlSetKey.OpenSubKey("Services")
        Dim ServiceKey As RegistryKey = ServicesKey.OpenSubKey(Me.ServiceInstaller1.ServiceName, True)

        'Set the service description
        ServiceKey.SetValue("Description", "Allows remote service administration")

        'Cleanup
        ServiceKey.Close()
        ServicesKey.Close()
        ControlSetKey.Close()
        SystemKey.Close()

    End Sub

End Class
