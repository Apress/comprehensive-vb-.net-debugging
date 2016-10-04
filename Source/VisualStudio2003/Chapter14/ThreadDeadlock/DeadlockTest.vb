Option Strict On
Imports System.Threading

Module DeadlockTest

    Sub Main()
        Dim TransferTest As New Bank(2, 10000)
        'Console.ReadLine()
    End Sub

End Module

Class Bank

    Private m_AccountOne As New Account(1000000)
    Private m_AccountTwo As New Account(1000000)

    Public Sub New(ByVal NumberOfCashiers As Integer, ByVal NumberOfTransfers As Integer)
        Dim EachWorker As Integer, NewThread As Thread, Worker As Cashier

        'Show starting conditions
        Console.WriteLine("{0} cashiers are performing {1} transfers each.", _
                     NumberOfCashiers.ToString, NumberOfTransfers.ToString)

        'Start specified number of worker threads
        For EachWorker = 1 To NumberOfCashiers
            Worker = New Cashier(Me, NumberOfTransfers)
            NewThread = New Thread(AddressOf Worker.TransferMoney)
            NewThread.Name = "Cashier" & EachWorker.ToString
            NewThread.Start()
        Next EachWorker

    End Sub

    Public ReadOnly Property AccountOne() As Account
        Get
            AccountOne = m_AccountOne
        End Get
    End Property

    Public ReadOnly Property AccountTwo() As Account
        Get
            AccountTwo = m_AccountTwo
        End Get
    End Property

End Class

Class Cashier
    Private m_Bank As Bank, m_WorkerId As Integer
    Private m_NumberOfTransfers As Integer

    Public Sub New(ByVal AnyBank As Bank, ByVal NumberOfTransfers As Integer)
        m_Bank = AnyBank
        m_NumberOfTransfers = NumberOfTransfers
    End Sub

    Public Sub TransferMoney()
        Dim CurrentTransfer As Integer

        With m_Bank

            For CurrentTransfer = 1 To m_NumberOfTransfers

                If TrueOrFalse() = True Then
                    SyncLock (.AccountOne)
                        'Thread.Sleep(0)
                        SyncLock (.AccountTwo)
                            .AccountOne.CreditBalance(100)
                            .AccountTwo.DebitBalance(100)
                            Console.WriteLine("{0}: Transfer {1}", _
                                 Thread.CurrentThread.Name, CurrentTransfer.ToString)
                        End SyncLock
                    End SyncLock
                Else
                    SyncLock (.AccountTwo)
                        'Thread.Sleep(0)
                        SyncLock (.AccountOne)
                            .AccountOne.DebitBalance(100)
                            .AccountTwo.CreditBalance(100)
                            Console.WriteLine("{0}: Transfer {1}", _
                                 Thread.CurrentThread.Name, CurrentTransfer.ToString)
                        End SyncLock
                    End SyncLock
                End If
            Next CurrentTransfer

        End With

    End Sub

    Private Function TrueOrFalse() As Boolean
        Randomize()
        Dim Test As Single = (Int((2 * Rnd()) + 1))
        Return CBool(Test = 1)
    End Function

End Class

Class Account

    Private m_AccountBalance As Decimal = 0

    Public Sub New(ByVal StartingBalance As Decimal)
        m_AccountBalance = StartingBalance
    End Sub

    Public ReadOnly Property AccountBalance() As Decimal
        Get
            AccountBalance = m_AccountBalance
        End Get
    End Property

    Public ReadOnly Property HasSufficientBalance(ByVal AmountToTransfer As Decimal) As Boolean
        Get
            HasSufficientBalance = CBool(m_AccountBalance >= AmountToTransfer)
        End Get
    End Property

    Public Function DebitBalance(ByVal AmountToDebit As Decimal) As Decimal
        m_AccountBalance -= AmountToDebit
        Return m_AccountBalance
    End Function

    Public Function CreditBalance(ByVal AmountToCredit As Decimal) As Decimal
        m_AccountBalance += AmountToCredit
        Return m_AccountBalance
    End Function

End Class
