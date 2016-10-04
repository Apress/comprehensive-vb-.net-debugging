Option Strict On
Imports System.Threading

Module CountMonitor

    Sub Main()
        Dim CountTest As New CountCoordinator(5)
        Console.ReadLine()
    End Sub

End Module

Class CountCoordinator

    Private Const MAX_COUNT As Integer = 99
    Private m_Counter As Integer = 0

    Public Sub New(ByVal NumberOfCounters As Integer)
        Dim EachWorker As Integer, NewThread As Thread, Worker As CountWorker

        'Show starting conditions
        Console.WriteLine("Count started at {0} with max value of {1}.", _
                     CStr(Me.CurrentCount), CStr(Me.MaxCount))
        Console.WriteLine("{0} worker threads are doing the counting.", _
                     CStr(NumberOfCounters))
        'Start specified number of worker threads
        For EachWorker = 1 To NumberOfCounters
            Worker = New CountWorker(Me, EachWorker)
            NewThread = New Thread(AddressOf Worker.IncrementCount)
            NewThread.Start()
        Next EachWorker

    End Sub

    Public Property CurrentCount() As Integer
        Get
            Return m_Counter
        End Get
        Set(ByVal Value As Integer)
            m_Counter = Value
        End Set
    End Property

    Public ReadOnly Property MaxCount() As Integer
        Get
            Return MAX_COUNT
        End Get
    End Property

End Class

Class CountWorker
    Private m_Coordinator As CountCoordinator
    Private m_WorkerId As Integer

    Public Sub New(ByVal Coordinator As CountCoordinator, ByVal WorkerId As Integer)
        m_Coordinator = Coordinator
        m_WorkerId = WorkerId
    End Sub

    Public Sub IncrementCount()

        'Increment shared counter until equal to maximum allowed
        With m_Coordinator

            Do While .CurrentCount < .MaxCount
                'SyncLock (m_Coordinator)
                Select Case .CurrentCount
                    Case Is < (.MaxCount - 10)
                        Thread.Sleep(0)
                        .CurrentCount += 10
                    Case Is < .MaxCount
                        Thread.Sleep(0)
                        .CurrentCount += 1
                    Case Else
                End Select
                'End SyncLock
                Console.WriteLine("Worker {0} current count {1}", _
                           CStr(m_WorkerId), CStr(.CurrentCount))
            Loop

        End With

    End Sub

End Class
