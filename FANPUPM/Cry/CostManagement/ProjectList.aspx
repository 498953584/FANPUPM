<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectList.aspx.cs" Inherits="ProjectList" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_uprojectlist_ascx" Src="~/EPC/UserControl1/uprojectlist.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
	<head>
		<title>ProjectList</title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta http-equiv="Expires" content="0">
		<meta http-equiv="Cache-Control" content="no-cache">
		<meta http-equiv="Pragma" content="no-cache">
		<base target="_self"/>
	</head>
	<body class="body_clear" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0" style="table-layout:fixed;">
				<tr>
					<td><div style="width:100%;height:100%;overflow:auto;">
			<MyUserControl:epc_usercontrol1_uprojectlist_ascx ID="UProjectList1" runat="server" />
			</div></td>
				</tr>
			</table>
		</form>
	</body>
</html>
