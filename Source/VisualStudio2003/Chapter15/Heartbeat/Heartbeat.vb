Option Strict On

Imports System
Imports System.Runtime.Remoting.Messaging

Public Class Heartbeat : Inherits MarshalByRefObject
    Public Event HeartbeatLocal(ByVal LocalComponent As String)
    Public Event HeartbeatRemote(ByVal LocalMonitor As String, ByVal StatusReport As String)

    <OneWay()> _
    Public Sub Heartbeat(ByVal LocalComponent As String)
        RaiseEvent HeartbeatLocal(LocalComponent)
    End Sub

    <OneWay()> _
    Public Sub RemoteHeartbeat(ByVal LocalMonitor As String, ByVal StatusReport As String)
        RaiseEvent HeartbeatRemote(LocalMonitor, StatusReport)
    End Sub

End Class