VERSION 5.00
Begin VB.Form FormInterop 
   Caption         =   "Interop debugging"
   ClientHeight    =   2544
   ClientLeft      =   48
   ClientTop       =   288
   ClientWidth     =   3744
   LinkTopic       =   "Form1"
   ScaleHeight     =   2544
   ScaleWidth      =   3744
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton DebugTest 
      Caption         =   "Test debug of VB6 app calling VB .NET component"
      Height          =   1092
      Left            =   720
      TabIndex        =   0
      Top             =   720
      Width           =   2172
   End
End
Attribute VB_Name = "FormInterop"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub DebugTest_Click()
Dim InteropTest As InteropDemo.MathClass
Set InteropTest = New InteropDemo.MathClass

Me.DebugTest.Caption = "Result = " & CStr(InteropTest.AddTwoNumbers(1.1, 2.2))

Set InteropTest = Nothing

End Sub

Private Sub Form_Load()

Me.Caption = "Test interop debugging"

End Sub
