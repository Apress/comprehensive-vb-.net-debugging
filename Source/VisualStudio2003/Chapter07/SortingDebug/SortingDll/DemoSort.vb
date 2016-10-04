Option Strict On

Public Class DemoSort

    'These are the sorting lists
    Private ListUnsorted() As Int32
    Private ListBeingSorted() As Int32
    'These are max and minimum sort settings
    Private ListLengthMin As Int32
    Private ListLengthMax As Int32
    Private ListValueMin As Int32
    Private ListValueMax As Int32

    Public Enum SortType As Integer
        BubbleSort = 1
        SelectionSort = 2
        QuickSort = 3
        CountingSort = 4
    End Enum

    Public Sub ListBuild(ByVal ListLength As Int32, ByVal ValueMin As Int32, ByVal ValueMax As Int32)
        Dim ArrayItem As Int32, objRandom As New System.Random(1)

        'If parameters haven't changed, just use previous list
        'This enables multiple timing checks on an identical array
        If ListLength = ListLengthMax And ValueMin = ListValueMin And ValueMax = ListValueMax Then
            Exit Sub
        End If

        'Build a new list with given parameters
        ReDim ListUnsorted(ListLength)
        For ArrayItem = 1 To ListLength
            ListUnsorted(ArrayItem) = objRandom.Next(ValueMin, ValueMax)
        Next ArrayItem

        'Set length of sorted list to match
        ReDim ListBeingSorted(ListLength)

        'Store min and max list items and values
        ListLengthMin = 1
        ListLengthMax = ListLength
        ListValueMin = ValueMin
        ListValueMax = ValueMax

    End Sub

    Public Function DoSort(ByVal Sort As SortType) As Double
        Dim TimeStart As Int32, TimeFinish As Int32, ArrayItem As Int32

        'Copy to array that will contain newly-sorted list
        For ArrayItem = ListLengthMin To ListLengthMax
            ListBeingSorted(ArrayItem) = ListUnsorted(ArrayItem)
        Next

        'Start the clock so we can report how long the sorting took
        TimeStart = System.Environment.TickCount()

        'Do the sort
        Select Case Sort
            Case SortType.BubbleSort
                SortBubble(ListLengthMin, ListLengthMax)
            Case SortType.QuickSort
                SortQuick(ListLengthMin, ListLengthMax)
            Case SortType.SelectionSort
                SortSelection()
            Case SortType.CountingSort
                SortCounting()
            Case Else
                Trace.Fail(CStr(Sort) & " is an invalid sort type")
        End Select

        'Stop the clock and report how long the sorting took
        TimeFinish = System.Environment.TickCount()
        'Check sort
        SortCheck()
        'Convert elapsed time into seconds
        Return (TimeFinish - TimeStart) / 1000

    End Function

    Private Sub SortBubble(ByVal ItemLow As Int32, ByVal ItemHigh As Int32)
        Dim intLastSwap As Int32, intLoop1 As Int32, intLoop2 As Int32
        Dim intDrop As Int32

        'Repeat until we are done
        Do While ItemLow < ItemHigh

            'First bubble upwards
            intLastSwap = ItemLow - 1
            intLoop1 = ItemLow + 1

            Do While intLoop1 <= ItemHigh
                'Find a bubble
                If ListBeingSorted(intLoop1 - 1) > ListBeingSorted(intLoop1) Then
                    'See where to drop the bubble
                    intDrop = ListBeingSorted(intLoop1 - 1)
                    intLoop2 = intLoop1
                    Do While ListBeingSorted(intLoop2) < intDrop
                        ListBeingSorted(intLoop2 - 1) = ListBeingSorted(intLoop2)
                        intLoop2 += 1
                        If intLoop2 > ItemHigh Then
                            Exit Do
                        End If
                    Loop
                    ListBeingSorted(intLoop2 - 1) = intDrop
                    intLastSwap = intLoop2 - 1
                    intLoop1 = intLoop2 + 1
                Else
                    intLoop1 += 1
                End If
            Loop

            'Update maximum value
            ItemHigh = intLastSwap - 1

            'Next bubble downwards
            intLastSwap = ItemHigh + 1
            intLoop1 = ItemHigh - 1

            Do While intLoop1 >= ItemLow
                'Find a bubble
                If ListBeingSorted(intLoop1 + 1) < ListBeingSorted(intLoop1) Then
                    'See where to drop the bubble
                    intDrop = ListBeingSorted(intLoop1 + 1)
                    intLoop2 = intLoop1
                    Do While ListBeingSorted(intLoop2) > intDrop
                        ListBeingSorted(intLoop2 + 1) = ListBeingSorted(intLoop2)
                        intLoop2 -= 1
                        If intLoop2 < ItemLow Then
                            Exit Do
                        End If
                    Loop
                    ListBeingSorted(intLoop2 + 1) = intDrop
                    intLastSwap = intLoop2 + 1
                    intLoop1 = intLoop2 - 1
                Else
                    intLoop1 -= 1
                End If
            Loop

            'Update minimum value
            ItemLow = intLastSwap + 1

        Loop

    End Sub

    Private Sub SortSelection()
        Dim LoopOuter As Int32, LoopInner As Int32, BestValue As Int32, BestInnerIndex As Int32

        For LoopOuter = ListLengthMin To ListLengthMax - 1

            'Init lowest-value search
            BestValue = ListBeingSorted(LoopOuter)
            BestInnerIndex = LoopOuter

            'Find lowest value that hasn't already been sorted
            For LoopInner = LoopOuter + 1 To ListLengthMax
                If ListBeingSorted(LoopInner) < BestValue Then
                    BestValue = ListBeingSorted(LoopInner) + 1
                    BestInnerIndex = LoopInner
                End If
            Next LoopInner

            'Swap lowest value into proper position
            ListBeingSorted(BestInnerIndex) = ListBeingSorted(LoopOuter)
            ListBeingSorted(LoopOuter) = BestValue

        Next LoopOuter

    End Sub

    Private Sub SortQuick(ByVal ItemLow As Int32, ByVal ItemHigh As Int32)
        Dim IntermediateValue As Int32, Position As Int32
        Dim objRandom As New System.Random()
        Dim intLow As Int32, intHigh As Int32

        'If the list has only 1 element, it's already sorted
        If ItemLow >= ItemHigh Then Exit Sub

        'Pick a dividing item
        Position = objRandom.Next(ItemLow, ItemHigh)
        IntermediateValue = ListBeingSorted(Position)

        'Swap it to the front so we can find it easily
        ListBeingSorted(Position) = ListBeingSorted(ItemLow)

        'Move the items smaller than this into the left
        'half of the list. Move the others into the right.
        intLow = ItemLow
        intHigh = ItemHigh
        Do
            'Look down from low item for a value < IntermediateValue
            Do While ListBeingSorted(intHigh) >= IntermediateValue
                intHigh -= 1
                If intHigh <= intLow Then Exit Do
            Loop
            If intHigh <= intLow Then
                ListBeingSorted(intLow) = IntermediateValue
                Exit Do
            End If

            'Swap the low and high values
            ListBeingSorted(intLow) = ListBeingSorted(intHigh)

            'Look up from ItemLow for a value >= IntermediateValue
            intLow += 1
            Do While ListBeingSorted(intLow) < IntermediateValue
                intLow += 1
                If intLow >= intHigh Then Exit Do
            Loop
            If intLow >= intHigh Then
                intLow = intHigh
                ListBeingSorted(intHigh) = IntermediateValue
                Exit Do
            End If

            'Swap the low and high items
            ListBeingSorted(intHigh) = ListBeingSorted(intLow)
        Loop

        'Sort the two sublists recursively
        SortQuick(ItemLow, intLow - 1)
        SortQuick(intHigh + 1, ItemHigh)

    End Sub

    Private Sub SortCounting()
        Dim ListCounts(ListValueMax) As Int32
        Dim ArrayItem As Int32, ThisCount As Int32, NextOffset As Int32

        'Count the items
        For ArrayItem = ListLengthMin To ListLengthMax
            ListCounts(ListUnsorted(ArrayItem)) += 1
        Next ArrayItem

        'Convert the ListCounts into offsets
        NextOffset = ListLengthMin
        For ArrayItem = ListValueMin To ListValueMax
            ThisCount = ListCounts(ArrayItem)
            ListCounts(ArrayItem) = NextOffset
            NextOffset += ThisCount
        Next ArrayItem

        'Place the items in the sorted array
        For ArrayItem = ListLengthMin To ListLengthMax
            ListBeingSorted(ListCounts(ListUnsorted(ArrayItem))) = ListUnsorted(ArrayItem)
            ListCounts(ListUnsorted(ArrayItem)) += 1
        Next ArrayItem

    End Sub

    Private Function SortCheck() As Boolean
        Dim ArrayItem As Int32

        For ArrayItem = 2 To ListLengthMax
            If ListBeingSorted(ArrayItem - 1) > ListBeingSorted(ArrayItem) Then
                Trace.Fail("Item " & (ArrayItem - 1).ToString & " is larger than item " & ArrayItem.ToString)
                Exit For
            End If
        Next ArrayItem

    End Function

End Class
