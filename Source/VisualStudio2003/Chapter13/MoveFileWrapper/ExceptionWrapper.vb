Option Strict On
Imports System.IO

Module ExceptionWrapper

    Sub Main()
        Dim WrapperNo As Boolean = False, WrapperYes As Boolean = True

        'MOVE commands producing good exception information
        TestMove("C:\Demo\Source.txt", "C:\Demo\Destination.txt", WrapperNo, "Destination is read-only")
        TestMove("C:\Demo\Source.txt", "", WrapperNo, "Destination is empty")
        TestMove("C:\Demo\Source.txt", Nothing, WrapperNo, "Destination is nothing")
        TestMove("C:\Demo\Source.txt", "C:\Dem\Destination.txt", WrapperNo, "Wrong destination folder")
        TestMove("C:\Demo\SourceX.txt", "C:\Demo\Destination.txt", WrapperNo, "Source doesn't exist")

        'MOVE commands producing bad exception information
        TestMove("C:\Demo\Source.txt", "C:\Demo\*.txt", WrapperNo, "Wildcard in destination")
        TestMove("C:\Demo\Sou<rce.txt", "C:\Demo\Destination.txt", WrapperNo, "Bad character in source")
        TestMove("C:\Demo\Source.txt" & Space(300), "C:\Demo\Destination.txt", WrapperNo, "Source too long")
        TestMove("C:\Demo\Source.txt", "C:\Demo\", WrapperNo, "Destination is folder")
        TestMove("C:\Demo\Sou:rce.txt", "C:\Demo\Destination.txt", WrapperNo, "Dodgy character in source")

        'MOVE commands producing bad exception information, using wrapper
        TestMove("C:\Demo\Source.txt", "C:\Demo\*.txt", WrapperYes, "Wildcard in destination")
        TestMove("C:\Demo\Sou<rce.txt", "C:\Demo\Destination.txt", WrapperYes, "Bad character in source")
        TestMove("C:\Demo\Source.txt" & Space(300), "C:\Demo\Destination.txt", WrapperYes, "Source too long")
        TestMove("C:\Demo\Source.txt", "C:\Demo\", WrapperYes, "Destination is folder")
        TestMove("C:\Demo\Sou:rce.txt", "C:\Demo\Destination.txt", WrapperYes, "Dodgy character in source")

    End Sub

    Sub TestMove(ByVal SourceFile As String, ByVal DestinationFile As String, _
                 ByVal UseWrapper As Boolean, ByVal ProbeType As String)

        Try
            If UseWrapper = True Then
                MoveFile(SourceFile, DestinationFile)
            Else
                File.Move(SourceFile, DestinationFile)
            End If

        Catch ShowException As Exception
            Console.WriteLine("TESTING: " & ProbeType)
            Console.WriteLine(ShowException.ToString)
            Console.WriteLine()
            Console.ReadLine()

        End Try

    End Sub

    Sub MoveFile(ByVal SourceFile As String, ByVal DestinationFile As String)

        Try
            File.Move(SourceFile, DestinationFile)

        Catch UnclearException As ArgumentException When UnclearException.Message = "Illegal characters in path."
            Dim HelpfulMessage As String = UnclearException.Message & vbCrLf
            HelpfulMessage += "Source = " & SourceFile & vbCrLf
            HelpfulMessage += "Destination = " & DestinationFile & vbCrLf
            Throw New ArgumentException(HelpfulMessage, UnclearException)

        Catch UnclearException As ArgumentException When UnclearException.Message = "The path contains illegal characters."
            Dim HelpfulMessage As String = UnclearException.Message & vbCrLf
            HelpfulMessage += "Source = " & SourceFile & vbCrLf
            HelpfulMessage += "Destination = " & DestinationFile & vbCrLf
            Throw New ArgumentException(HelpfulMessage, UnclearException)

        Catch UnclearException As IOException When UnclearException.Message = "Cannot create a file when that file already exists." & vbCrLf
            Dim HelpfulMessage As String = "Destination '" & DestinationFile & "' already exists" & vbCrLf
            Throw New IOException(HelpfulMessage, UnclearException)

        Catch UnclearException As PathTooLongException
            Dim HelpfulMessage As String = "Either source or destination path is too long" & vbCrLf
            HelpfulMessage += "Source (" & SourceFile.Length & " chars) = " & SourceFile & vbCrLf
            HelpfulMessage += "Destination (" & DestinationFile.Length & " chars) = " & DestinationFile & vbCrLf
            Throw New PathTooLongException(HelpfulMessage, UnclearException)

        Catch UnclearException As NotSupportedException
            Dim HelpfulMessage As String = "Either source or destination path contains a colon"
            HelpfulMessage += "Source = " & SourceFile & vbCrLf
            HelpfulMessage += "Destination = " & DestinationFile & vbCrLf
            Throw New NotSupportedException(HelpfulMessage, UnclearException)

        End Try

    End Sub

End Module
