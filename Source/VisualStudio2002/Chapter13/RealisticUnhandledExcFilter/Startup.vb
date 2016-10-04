Option Strict On
Imports System
Imports Microsoft.Win32

Module Startup

    Sub Main()

        'Before running this program within the IDE, please use the Debug...Exceptions menu
        'item to ensure that the program continues in the face of an unhandled exception.

        'Register the unhandled exception filter with the AppDomain.
        'This means that UnhandledExceptionFilter will be called whenever
        'a managed unhandled exception occurs.
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf UnhandledExceptionFilter

        'Here's where the normal application code should appear.
        'In this case, deliberately create an exception so that
        'we can test the unhandled exception filter.
        Dim objTest As Object
        objTest.ToString()

    End Sub

    Private Sub UnhandledExceptionFilter(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
        'This method will be called by any unhandled managed exception
        Dim MessageFriendly As String, MessageDetail As String

        'Log detailed exception message if no debugger is attached to this process
        If Debugger.IsAttached = False Then
            MessageDetail = "An unhandled exception occurred!" + Environment.NewLine
            MessageDetail += e.ExceptionObject.ToString + Environment.NewLine
            MessageDetail += "Application user was '" + Environment.UserName + "'." + Environment.NewLine
            If e.IsTerminating = True Then
                MessageDetail += "CLR will terminate this process (exception was on main thread)."
            Else
                MessageDetail += "CLR won't terminate this process (exception wasn't on main thread)."
            End If
            'Write message to the Windows event Application log
            Dim LogWriter As New EventLog("Application", ".", System.AppDomain.CurrentDomain.FriendlyName)
            LogWriter.WriteEntry(MessageDetail, EventLogEntryType.Warning)
            LogWriter.Close()
            LogWriter.Dispose()
        End If

#If Debug = True Then
        'Launch the debugger if DEBUG build, no debugger already attached
        'and this is an interactive process (user is present)
        If (Environment.UserInteractive = True) And (Debugger.IsAttached = False) Then
            Debugger.Launch()
        End If
#Else
        'If RELEASE build and this is an interactive process (user is present),
        'then show user either a warning or a critical message
        If Environment.UserInteractive = True Then
            MessageFriendly = "Unfortunately, this application hit an problem." + Environment.NewLine
            If e.IsTerminating = True Then
                MessageFriendly += "It will close now, but you can restart it." + Environment.NewLine
                MessageFriendly += "Please contact application support and ask them to look in the event log."
                MsgBox(MessageFriendly, MsgBoxStyle.Critical Or MsgBoxStyle.OKOnly, "Sorry for the inconvenience")
            Else
                MessageFriendly += "Please save your work asap before restarting the application."
                MsgBox(MessageFriendly, MsgBoxStyle.Exclamation Or MsgBoxStyle.OKOnly, "Sorry for the inconvenience")
            End If
        End If
#End If

    End Sub

End Module
