Option Strict On
Imports System
Imports System.Windows.Forms

Module UnhandledExceptions

    <STAThread()> _
       Sub Main()

        'First register the unhandled exception filter with the AppDomain.
        'This is useful to handle certain situations:
        '1: An exception is thrown on a non-window thread or before first window launched.
        '2: If a debugger is attached, unhandled exception will still reach here.
        '3: If app config file specifies JIT debugging, unhandled exception still reaches here.
        '4: Exceptions that are not CLS-compliant will still only be caught here.
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf UnhandledExceptionFilter

        'In a Windows Form app, if a managed exception occurs and a debugger
        'isn't attached, the CLR will display an exception warning dialog.
        'If we want to override this dialog, we need to register a delegate
        'so that our method will be called instead.
        AddHandler Application.ThreadException, AddressOf OverrideClrDialog

        'Normal application code goes here
        'In this case, click on the form button to trigger an unhandled exception
        Application.Run(New TestForm())

    End Sub

    Private Sub UnhandledExceptionFilter(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
        'This method deals with anything not handled by OverrideClrDialog
        MsgBox(e.ExceptionObject.ToString, MsgBoxStyle.OKOnly, "UnhandledExceptionFilter: " + e.IsTerminating.ToString)
    End Sub

    Private Sub OverrideClrDialog(ByVal sender As Object, ByVal e As System.Threading.ThreadExceptionEventArgs)
        'This method will be called by any unhandled managed exception
        'assuming that a debugger isn't attached
        MsgBox(e.Exception.ToString, MsgBoxStyle.OKOnly, "OverrideClrDialog")
    End Sub

End Module
