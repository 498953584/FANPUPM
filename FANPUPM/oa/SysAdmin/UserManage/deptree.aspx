<%@ Page Language="C#" AutoEventWireup="true" CodeFile="deptree.aspx.cs" Inherits="depTree" %>


<!DOCTYPE html>
<html>
<head runat="server"><title>部门树状目录</title>
	<style type="text/css">
		.trvw_select a
		{
			background-color: #3399FF;
			color: White;
		}
		trvw_select
		{
			background-color: White;
		}
	</style>
</head>
<body>
	<form id="Form1" method="post" runat="server">
	<font face="宋体">
		<asp:TreeView ID="tvDept" ExpandDepth="1" ShowLines="true" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
	</form>
</body>
</html>
