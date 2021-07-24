<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TypeTree.aspx.cs" Inherits="TypeTree" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>TypeTree</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
	</head>
	<body  onclick="selrow3('tvProjectt0')" >
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellPadding="0" width="100%" border="0">
				<TR>
				<TD><asp:TreeView ID="tvProject" ShowLines="true" showtooltip="False" expandlevel="2" Height="100%" Width="180px" runat="server"><SelectedNodeStyle BorderColor="Transparent" ForeColor="Red" /></asp:TreeView>
					</TD>
				</TR>
			</TABLE>
			<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
		</form>
	</body>
</HTML>
