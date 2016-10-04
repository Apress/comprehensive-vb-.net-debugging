Option Strict On
Imports System.Runtime.Serialization

<Serializable()> _
Public Class UserLoadException : Inherits System.ApplicationException

    Private m_UserId As String = ""

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub

    Public Sub New(ByVal message As String, ByVal UserId As String)
        MyBase.New(message)
        m_UserId = UserId
    End Sub

    Public Sub New(ByVal message As String, ByVal innerException As Exception)
        MyBase.New(message, innerException)
    End Sub

    Public Sub New(ByVal message As String, ByVal innerException As Exception, ByVal UserId As String)
        MyBase.New(message, innerException)
        m_UserId = UserId
    End Sub

    'To re-materialize exception and custom property at the receiving end
    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
        m_UserId = info.GetString("UserId")
    End Sub

    'To de-materialize custom property at the sending end
    Public Overrides Sub GetObjectData(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.GetObjectData(info, context)
        info.AddValue("UserId", m_UserId)
    End Sub

    'To add custom property to standard exception message
    Public Overrides ReadOnly Property Message() As String
        Get
            Return MyBase.Message & Environment.NewLine & "User id: " & m_UserId & Environment.NewLine
        End Get
    End Property

    ReadOnly Property UserId() As String
        Get
            UserId = m_UserId
        End Get
    End Property

End Class
