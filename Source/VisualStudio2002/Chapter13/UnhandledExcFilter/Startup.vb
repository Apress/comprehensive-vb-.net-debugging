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

    Sub UnhandledExceptionFilter(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
        'This method will be called by any unhandled managed exception
        'Using DEBUG or RELEASE config
        Dim JitDebugSetting As Object, RegKey As RegistryKey = Registry.LocalMachine
        RegKey = RegKey.OpenSubKey("Software\Microsoft\.NetFramework")
        JitDebugSetting = RegKey.GetValue("DbgJitDebugLaunchSetting")
        RegKey.Close()

#If Debug Then
        Console.WriteLine("DEBUG configuration")

#Else
        Console.WriteLine("RELEASE configuration")
#End If
        'Who is user?
        Console.WriteLine("User: " + Environment.UserName)
        'Is debugger attached?
        Console.WriteLine("Debugger attached? " + Debugger.IsAttached.ToString)
        'Is there an end-user to show a message to?
        Console.WriteLine("End-user present? " + Environment.UserInteractive.ToString)
        'Is this a CLS-compliant exception?
        Console.WriteLine("CLS-compliant exception? " + ((TypeOf e.ExceptionObject Is Exception).ToString))
        'What's the CLR going to do with the process?
        If e.IsTerminating = True Then
            Console.WriteLine("CLR will terminate this process")
        Else
            Console.WriteLine("CLR won't terminate this process")
        End If
        'What's the CLR going to do about debugging?
        If Debugger.IsAttached = True Then
            Console.WriteLine("CLR didn't talk to user or spawn a debugger")
        Else
            'If process is terminating, debugger checks registry for action.
            'Otherwise, CLR will deal with only the offending thread.
            If e.IsTerminating = True Then
                If JitDebugSetting Is Nothing Then
                    Console.WriteLine("No JIT debug setting in registry")
                Else
                    Console.WriteLine("JIT debug setting: " + JitDebugSetting.ToString)
                    Select Case JitDebugSetting
                        Case 0
                            Console.WriteLine("CLR asked user whether to spawn a debugger")
                        Case 1
                            Console.WriteLine("CLR didn't talk to user or spawn a debugger")
                        Case 2
                            Console.WriteLine("CLR spawned debugger and asked user about debugging")
                        Case Else
                            Console.WriteLine("JIT debug setting is invalid!")
                    End Select
                End If
            Else
                Console.WriteLine("CLR didn't talk to user or spawn a debugger")
            End If
        End If
        'Write exception to console
        Console.WriteLine(Environment.NewLine + "Exception text:")
        Console.WriteLine(e.ExceptionObject.ToString)
        Console.ReadLine()

    End Sub

End Module
