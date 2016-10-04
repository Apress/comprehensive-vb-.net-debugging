Option Strict On

Module Test

    Sub Main()
        Dim intTest As Integer, dblTest As Double
        Dim intZero As Integer = 0, blnExceptionThrown As Boolean = False

        'First test
        Console.WriteLine("Integer division by zero assigned to integer:")
        blnExceptionThrown = False
        Try
            intTest = 5 \ intZero
        Catch objException As Exception
            Console.WriteLine(objException.Message)
            blnExceptionThrown = True
        Finally
            If blnExceptionThrown = True Then
                Console.WriteLine("Result: not available")
            Else
                Console.WriteLine("No exception was thrown")
                Console.WriteLine("Result: " + intTest.ToString)
            End If
            Console.WriteLine()
        End Try

        'Second test
        Console.WriteLine("Integer division by zero assigned to double:")
        blnExceptionThrown = False
        Try
            dblTest = 5 \ intZero
        Catch objException As Exception
            Console.WriteLine(objException.Message)
            blnExceptionThrown = True
        Finally
            If blnExceptionThrown = True Then
                Console.WriteLine("Result: not available")
            Else
                Console.WriteLine("No exception was thrown")
                Console.WriteLine("Result: " + dblTest.ToString)
            End If
            Console.WriteLine()
        End Try

        'Third test
        Console.WriteLine("Float division by zero assigned to integer:")
        blnExceptionThrown = False
        Try
            intTest = CInt(5 / intZero)
        Catch objException As Exception
            Console.WriteLine(objException.Message)
            blnExceptionThrown = True
        Finally
            If blnExceptionThrown = True Then
                Console.WriteLine("Result: not available")
            Else
                Console.WriteLine("No exception was thrown")
                Console.WriteLine("Result: " + intTest.ToString)
            End If
            Console.WriteLine()
        End Try

        'Fourth test
        Console.WriteLine("Float division by zero assigned to double:")
        blnExceptionThrown = False
        Try
            dblTest = 5 / intZero
        Catch objException As Exception
            Console.WriteLine(objException.Message)
            blnExceptionThrown = True
        Finally
            If blnExceptionThrown = True Then
                Console.WriteLine("Result: not available")
            Else
                Console.WriteLine("No exception was thrown")
                Console.WriteLine("Result: " + dblTest.ToString)
                Console.WriteLine("Is infinity: " + CStr(Double.IsInfinity(dblTest)))
                Console.WriteLine("Is not a number: " + CStr(Double.IsNaN(dblTest)))
            End If
            Console.WriteLine()
        End Try

        'Fifth test
        Console.WriteLine("Float division of zero by zero assigned to double:")
        blnExceptionThrown = False
        Try
            dblTest = intZero / intZero
        Catch objException As Exception
            Console.WriteLine(objException.Message)
            blnExceptionThrown = True
        Finally
            If blnExceptionThrown = True Then
                Console.WriteLine("Result: not available")
            Else
                Console.WriteLine("No exception was thrown")
                Console.WriteLine("Result: " + dblTest.ToString)
                Console.WriteLine("Is infinity: " + CStr(Double.IsInfinity(dblTest)))
                Console.WriteLine("Is not a number: " + CStr(Double.IsNaN(dblTest)))
            End If
            Console.WriteLine()
        End Try

        Console.ReadLine()

    End Sub

End Module
