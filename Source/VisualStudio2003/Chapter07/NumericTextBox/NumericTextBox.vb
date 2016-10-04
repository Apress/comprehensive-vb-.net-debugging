Imports System.Windows.Forms

Public Class NumericTextBox : Inherits TextBox

    Private Sub NumericTextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        If e.KeyChar < "0"c Or e.KeyChar > "9"c Then
            e.Handled = True
        End If
    End Sub

End Class
