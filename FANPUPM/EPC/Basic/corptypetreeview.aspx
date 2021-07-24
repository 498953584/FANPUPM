<%@ Page Language="C#" AutoEventWireup="true" CodeFile="corptypetreeview.aspx.cs" Inherits="CorpTypeTreeView" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="HEAD1" runat="server"><title id="Title1">单位信息</title>
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
	<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
</head>
<body class="body_frame" onload="selrow3('TVTypet1')">
	<form id="Form1" method="post" runat="server">
	<table id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
		
		<tr>
			<td valign="top">
				
				<asp:TreeView ID="TVType" selectexpands="True" showtooltip="False" expandlevel="1" Height="100%" Width="300px" ForeColor="Black" ShowLines="true" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
				<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
			</td>
		</tr>
	</table>
	</form>
</body>
</html>
