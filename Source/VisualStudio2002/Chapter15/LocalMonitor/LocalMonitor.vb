Option Strict On

Imports System
Imports System.Runtime.Remoting
Imports System.Threading
Imports Monitoring

Module Monitor
    Private Const MY_ID As String = "LocalMonitor"
    Private Delegate Sub HeartbeatThread(ByVal HeartbeatInterval As Int32)
    Private m_StopHeartbeats As Boolean = False
    Private m_StatusReport As String = "No heartbeats received"
    Private WithEvents m_Listener As Heartbeat

    Sub Main()

        Console.WriteLine("Starting local monitor")
        RemotingConfiguration.Configure("LocalMonitor.config")

        Console.WriteLine("Creating local listener")
        m_Listener = New Heartbeat
        RemotingServices.Marshal(m_Listener, "Heartbeat.dll")

        Dim HT As New HeartbeatThread(AddressOf Heartbeats)
        Dim ThreadResult As IAsyncResult = HT.BeginInvoke(10, Nothing, Nothing)

        Console.WriteLine("Now listening - press 'Enter' to terminate local monitor...")
        Console.ReadLine()

        Console.WriteLine("Local monitor terminating")
        m_StopHeartbeats = True
        HT.EndInvoke(ThreadResult)

        RemotingServices.Disconnect(m_Listener)
        m_Listener = Nothing

    End Sub

    Private Sub Heartbeats(ByVal HeartbeatInterval As Int32)

        'Find remote monitor
        Console.WriteLine("Finding remote monitor")
        Dim Listener As New Heartbeat
        Listener = CType(Activator.GetObject(GetType(Heartbeat), _
                   "tcp://localhost:8081/Heartbeat.dll"), Heartbeat)

        'Send regular heartbeats to remote monitor
        Do While m_StopHeartbeats = False
            Listener.RemoteHeartbeat(MY_ID, m_StatusReport)
            Console.WriteLine("Heartbeat sent to remote monitor")
            Thread.Sleep(HeartbeatInterval * 1000)
        Loop

        'Terminate heartbeat thread
        Console.WriteLine("Stopping heartbeats")

    End Sub

    Private Sub m_Listener_HeartbeatLocal(ByVal LocalComponent As String) Handles m_Listener.HeartbeatLocal
        'Listen for regular heartbeats from local component
        Console.WriteLine("Heartbeat received from " & LocalComponent)
        m_StatusReport = "Last heartbeat received from " & LocalComponent & " at " & Format$(Now, "hh:mm:ss")
    End Sub

End Module
