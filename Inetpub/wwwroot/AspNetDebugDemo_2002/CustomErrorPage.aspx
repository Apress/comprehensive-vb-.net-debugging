<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CustomErrorPage.aspx.vb" Inherits="AspNetDebugDemo.CustomErrorPage"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CustomErrorPage</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:label id="lblText" style="Z-INDEX: 101; LEFT: 72px; POSITION: absolute; TOP: 60px" runat="server"
				Width="301px" Height="54px">This PAGE-level custom error page was defined in MainForm.ErrorPage</asp:label><asp:button id="btnBack" style="Z-INDEX: 102; LEFT: 72px; POSITION: absolute; TOP: 150px" runat="server"
				Width="216px" Height="72px" Text="Go back to main page"></asp:button></form>
	</body>
</HTML>
