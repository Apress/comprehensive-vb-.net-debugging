Option Strict On

Public Class Calc

    Private m_NumberToAccumulate As Int32 = 0
    Private m_NumberReached As Int32 = 0
    Private m_RunningTotal As Decimal = 0

    Public Sub New(ByVal NumberToAccumulate As Int32)
        m_NumberToAccumulate = NumberToAccumulate
    End Sub

    Public Property NumberToAccumulate() As Int32
        Get
            Return m_NumberToAccumulate
        End Get
        Set(ByVal NewValue As Int32)
            m_NumberToAccumulate = NewValue
            m_NumberReached = 0
            m_RunningTotal = 0
        End Set
    End Property

    Public ReadOnly Property NumberReached() As Int32
        Get
            Return m_NumberReached
        End Get
    End Property

    Public Function Accumulate(ByVal ShowProgressInterval As Int32) As Decimal
        Dim LoopCounter As Int32

        For LoopCounter = m_NumberReached To (m_NumberReached + ShowProgressInterval - 1)
            If m_NumberReached <= m_NumberToAccumulate Then
                m_RunningTotal += LoopCounter
                m_NumberReached += 1
            Else
                Exit For
            End If
        Next LoopCounter

        Return m_RunningTotal

    End Function

End Class
