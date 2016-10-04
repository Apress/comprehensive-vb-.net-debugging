Imports Microsoft.Office.Core
imports Extensibility
imports System.Runtime.InteropServices
Imports EnvDTE

#Region " Read me for Add-in installation and setup information. "
' When run, the Add-in wizard prepared the registry for the Add-in.
' At a later time, if the Add-in becomes unavailable for reasons such as:
'   1) You moved this project to a computer other than which is was originally created on.
'   2) You chose 'Yes' when presented with a message asking if you wish to remove the Add-in.
'   3) Registry corruption.
' you will need to re-register the Add-in by building the DebugAddInSetup project 
' by right clicking the project in the Solution Explorer, then choosing install.
#End Region

<GuidAttribute("83028EEB-B835-4720-8DC3-A83B4F145FB1"), ProgIdAttribute("DebugAddIn.Connect")> _
Public Class Connect
	
	Implements Extensibility.IDTExtensibility2
	Implements IDTCommandTarget
	
	Dim applicationObject As EnvDTE.DTE
    Dim addInInstance as EnvDTE.AddIn
	
	Public Sub OnBeginShutdown(ByRef custom As System.Array) Implements Extensibility.IDTExtensibility2.OnBeginShutdown
	End Sub
	
	Public Sub OnAddInsUpdate(ByRef custom As System.Array) Implements Extensibility.IDTExtensibility2.OnAddInsUpdate
	End Sub
	
	Public Sub OnStartupComplete(ByRef custom As System.Array) Implements Extensibility.IDTExtensibility2.OnStartupComplete
	End Sub
	
	Public Sub OnDisconnection(ByVal RemoveMode As Extensibility.ext_DisconnectMode, ByRef custom As System.Array) Implements Extensibility.IDTExtensibility2.OnDisconnection
	End Sub
	
	Public Sub OnConnection(ByVal application As Object, ByVal connectMode As Extensibility.ext_ConnectMode, ByVal addInInst As Object, ByRef custom As System.Array) Implements Extensibility.IDTExtensibility2.OnConnection
  		
		applicationObject = CType(application, EnvDTE.DTE)
        addInInstance = CType(addInInst, EnvDTE.AddIn)
		If connectMode = Extensibility.ext_ConnectMode.ext_cm_UISetup Then
			Dim objAddIn As AddIn = CType(addInInst, AddIn)
			Dim CommandObj As Command

			' When run, the Add-in wizard prepared the registry for the Add-in.
			' At a later time, the Add-in or its commands may become unavailable for reasons such as:
			'   1) You moved this project to a computer other than which is was originally created on.
			'   2) You chose 'Yes' when presented with a message asking if you wish to remove the Add-in.
			'   3) You add new commands or modify commands already defined.
			' You will need to re-register the Add-in by building the DebugAddInSetup project,
			' right-clicking the project in the Solution Explorer, and then choosing install.
			' Alternatively, you could execute the ReCreateCommands.reg file the Add-in Wizard generated in
			' the project directory, or run 'devenv /setup' from a command prompt.
			Try
				CommandObj = applicationObject.Commands.AddNamedCommand(objAddIn, "DebugAddIn", "DebugAddIn", "Executes the command for DebugAddIn", True, 59, Nothing, 1 + 2)		'1+2 == vsCommandStatusSupported+vsCommandStatusEnabled
				CommandObj.AddControl(applicationObject.CommandBars.Item("Tools"))
			Catch e as System.Exception
			End Try
		End If
	End Sub

	Public Sub Exec(ByVal cmdName As String, ByVal executeOption As vsCommandExecOption, ByRef varIn As Object, ByRef varOut As Object, ByRef handled As Boolean) Implements IDTCommandTarget.Exec
        handled = False
        If (executeOption = vsCommandExecOption.vsCommandExecOptionDoDefault) Then
            If cmdName = "DebugAddIn.Connect.DebugAddIn" Then
                'Here is the active code
                MsgBox("The add-in debugging demo is born", MsgBoxStyle.OKOnly Or MsgBoxStyle.Information, "Welcome")
                handled = True
                Exit Sub
            End If
        End If
	End Sub
	
	Public Sub QueryStatus(ByVal cmdName As String, ByVal neededText As vsCommandStatusTextWanted, ByRef statusOption As vsCommandStatus, ByRef commandText As Object) Implements IDTCommandTarget.QueryStatus
		If neededText = EnvDTE.vsCommandStatusTextWanted.vsCommandStatusTextWantedNone Then
			If cmdName = "DebugAddIn.Connect.DebugAddIn" Then
				statusOption = CType(vsCommandStatus.vsCommandStatusEnabled + vsCommandStatus.vsCommandStatusSupported, vsCommandStatus)
			Else
				statusOption = vsCommandStatus.vsCommandStatusUnsupported
			End If
		End If
	End Sub  
End Class
