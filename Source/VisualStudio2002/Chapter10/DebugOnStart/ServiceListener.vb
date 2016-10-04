Option Strict On

Imports System.Threading
Imports System.Net

Public Class ServiceListener

    Private ListenThread As Thread = Nothing
    Private ThreadExit As Boolean = False
    Private Clients As ArrayList = New ArrayList

    Public Sub New()

    End Sub

    Public Sub Start()

        If Me.ListenThread Is Nothing Then
            Me.ThreadExit = False
            Me.ListenThread = New Thread(New ThreadStart(AddressOf ListeningThread))
            Me.ListenThread.Start()
        Else
            Throw New InvalidOperationException("Listener thread already started")
        End If

    End Sub

    Private Sub ListeningThread()
        Dim MyTcpListener As Sockets.TcpListener = Nothing
        Dim LocalAddress As Net.IPAddress = IPAddress.Any
        Dim ThisClient As Client = Nothing, LoopCounter As Int32

        Try
            MyTcpListener = New Sockets.TcpListener(LocalAddress, 5005)
            MyTcpListener.Start()
            Trace.WriteLine("Listening thread starting on " & MyTcpListener.LocalEndpoint.ToString)

            While Me.ThreadExit = False
                Thread.Sleep(500)
                If MyTcpListener.Pending = True Then
                    Trace.WriteLine(Environment.NewLine & "Connecting new client")
                    ThisClient = New Client(MyTcpListener.AcceptSocket())
                    Clients.Add(ThisClient)
                    ThisClient.Start()
                Else
                    For LoopCounter = Me.Clients.Count - 1 To 0 Step -1
                        ThisClient = DirectCast(Clients(LoopCounter), Client)
                        If ThisClient.IsConnected = False Then
                            Trace.WriteLine("Performing client cleanup")
                            ThisClient.Close()
                            Me.Clients.RemoveAt(LoopCounter)
                        End If
                    Next
                End If
            End While

        Catch Exc As Sockets.SocketException
            Trace.WriteLine(Exc.Message)

        Finally
            Trace.WriteLine("Listen thread ending...")
            If Not (MyTcpListener Is Nothing) Then
                MyTcpListener.Stop()
            End If
            Trace.WriteLine("Listen thread ended")

        End Try

    End Sub

    Public Sub Close()
        Dim ThisClient As Client = Nothing, LoopCounter As Int32

        Me.ThreadExit = True
        'Pause to process pending requests and do cleanup
        Thread.Sleep(1000)
        'Time to die
        If Me.ListenThread.IsAlive Then
            Me.ListenThread.Abort()
            Me.ListenThread = Nothing
        End If
        'Dump any client still connected!
        For LoopCounter = Me.Clients.Count - 1 To 0 Step -1
            ThisClient = DirectCast(Clients(LoopCounter), Client)
            ThisClient.Close()
            Me.Clients.RemoveAt(LoopCounter)
        Next
        Me.Clients = Nothing

    End Sub

End Class
