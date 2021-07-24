<%@ Page Language="C#" AutoEventWireup="true" CodeFile="deptinfo.aspx.cs" Inherits="DeptInfo" %>



<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>WebForm1</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
</head>
<body leftmargin="0" topmargin="0" rightmargin="0">
	<form id="Form1" method="post" runat="server">
	<font face="宋体">
		<table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
			<tr>
				<td valign="top">
					<asp:TreeView ID="TViewDept" ShowLines="true" ExpandDepth="1" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
				</td>
			</tr>
		</table>
	</font>
	</form>
</body>
</html>
