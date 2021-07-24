<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tasklist.aspx.cs" Inherits="TaskList" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>任务列表1</title>
		<META http-equiv="Content-Type" content="text/html; charset=utf-8">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta http-equiv="Expires" content="0">
		<meta http-equiv="Cache-Control" content="no-cache">
		<meta http-equiv="Pragma" content="no-cache">
		<base target="_self">
		<script language="javascript" src="../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript" >
			function clickRow(obj)
			{
				/*在这之前增加你的处理代码*/
				doClick(obj,'TblSchedule');//调用样式设置
			}

			function dbClickRow(obj,theCode,theName,projectCode,projectName,w)
			{
				if (w=3)
				{
					var task = parent.window.dialogArguments;
					task[0] = theCode;
					task[1]	= theName;
//					task[2] = projectCode;
//					task[3] = projectName;
					window.close();
				}
			}
		
			function switchDisplay(obj,t1,t2)
			{
				/*在这之前增加你的处理代码*/
				doSwitchDisplay(obj,'TblSchedule','HdnScheduleCodeList',t1,t2,'../../');//调用样式设置
			}
		</script>
	</HEAD>
	<body class="body_clear" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table style="BORDER-COLLAPSE: collapse" height="100%" cellspacing="0" cellpadding="0" width="100%" border="1">
				<tr>
					<td bgcolor="#666666" height="24"><asp:Label ID="LblProject" Font-Bold="true" ForeColor="White" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td valign="top">
						<div id="dvScheduleBox" class="gridBox"><asp:Table ID="TblSchedule" CssClass="grid" GridLines="Both" CellSpacing="0" CellPadding="0" BorderColor="#E0E0E0" BorderStyle="Solid" BorderWidth="1px" Width="100%" runat="server"></asp:Table></div>
					</td>
				</tr>
			</table>
			<input id="HdnScheduleCodeList" style="WIDTH: 10px" type="hidden" name="HdnScheduleCodeList" runat="server" />

			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
		</form>
	</body>
</HTML>
