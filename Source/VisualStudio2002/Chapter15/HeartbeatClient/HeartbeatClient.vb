Option Strict On

Imports System
Imports System.Runtime.Remoting
Imports Monitoring
Imports System.Threading

Module Client
    Private Const MY_ID As String = "Client"
    Private Delegate Sub HeartbeatThread(ByVal HeartbeatInterval As Int32)
    Private m_StopHeartbeats As Boolean = False

    Sub Main()

        Console.WriteLine("Starting client")
        Dim HT As New HeartbeatThread(AddressOf Heartbeats)
        Dim ThreadResult As IAsyncResult = HT.BeginInvoke(2, Nothing, Nothing)

        Console.WriteLine("Press 'Enter' to terminate client...")
        Console.ReadLine()

        Console.WriteLine("Terminating client")
        m_StopHeartbeats = True
        HT.EndInvoke(ThreadResult)

    End Sub

    Private Sub Heartbeats(ByVal HeartbeatInterval As Int32)

        'Find local monitor
        Console.WriteLine("Finding local monitor")
        Dim Listener As New Heartbeat
        Listener = CType(Activator.GetObject(GetType(Heartbeat), _
                   "tcp://localhost:8080/Heartbeat.dll"), Heartbeat)

        'Send regular heartbeats to local monitor
        Do While m_StopHeartbeats = False
            Listener.Heartbeat(MY_ID)
            Console.WriteLine("Heartbeat sent to local monitor")
            Thread.Sleep(HeartbeatInterval * 1000)
        Loop

        'Terminate heartbeat thread
        Console.WriteLine("Stopping heartbeats")

    End Sub

End Module
