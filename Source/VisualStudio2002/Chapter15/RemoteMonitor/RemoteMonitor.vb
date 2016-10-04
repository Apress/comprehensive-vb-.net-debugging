Option Strict On

Imports System
Imports System.Runtime.Remoting
Imports Monitoring

Module RemoteMonitor
    Private WithEvents m_Listener As Heartbeat

    Sub Main()

        Console.WriteLine("Starting remote monitor")
        RemotingConfiguration.Configure("RemoteMonitor.config")

        Console.WriteLine("Creating remote listener")
        m_Listener = New Heartbeat
        RemotingServices.Marshal(m_Listener, "Heartbeat.dll")

        Console.WriteLine("Now listening - press 'Enter' to terminate remote monitor...")
        Console.ReadLine()

        Console.WriteLine("Remote monitor terminating")
        RemotingServices.Disconnect(m_Listener)
        m_Listener = Nothing

    End Sub

    Private Sub m_Listener_HeartbeatRemote(ByVal LocalMonitor As String, ByVal StatusReport As String) Handles m_Listener.HeartbeatRemote
        'Listen for regular heartbeats from local monitor
        Console.WriteLine("Status report received from " & LocalMonitor)
        Console.WriteLine(StatusReport)
    End Sub

End Module
