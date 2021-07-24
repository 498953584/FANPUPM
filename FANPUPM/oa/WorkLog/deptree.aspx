<%@ Page Language="C#" AutoEventWireup="true" CodeFile="deptree.aspx.cs" Inherits="depTree" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>部门管理</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
<body>
	<form id="Form1" method="post" runat="server">
	<asp:TreeView ID="tv" ShowLines="true" ExpandDepth="1" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
	</form>
</body>
</html>
