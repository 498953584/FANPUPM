<%@ Page Language="C#" AutoEventWireup="true" CodeFile="depttree.aspx.cs" Inherits="oa_zdgl_depttree" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
	<title>制度管理</title>
	<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
	<meta name="CODE_LANGUAGE" content="C#">
	<meta name="vs_defaultClientScript" content="JavaScript">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body>
	<form id="Form1" method="post" runat="server">
	<asp:TreeView ID="tv" ShowLines="true" ExpandDepth="1" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
	</form>
</body>
</html>
