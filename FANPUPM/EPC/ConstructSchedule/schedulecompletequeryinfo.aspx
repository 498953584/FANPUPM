<%@ Page Language="C#" AutoEventWireup="true" CodeFile="schedulecompletequeryinfo.aspx.cs" Inherits="ScheduleCompleteQueryInfo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>WBSQuery</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<script language="javascript">
			var objRow = null;
			
			function switchDisplay(obj,t1,t2)
			{
				/*在这之前增加你的处理代码*/
				doSwitchDisplay(obj,'tblSchedule','hdnScheduleCodeList',t1,t2,'../../../');//调用样式设置
			}
		</script>
	</head>
	<body class="body_clear">
		<form id="Form1" method="post" runat="server">
			<table class="table-none" id="Table2" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<tr class="td-toolsbar">
					<td align="left"><asp:Label ID="lblProjectName" runat="server"></asp:Label></td>
					<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="2" height="100%">
						<div class="gridBox" id="dvScheduleBox"><asp:Table ID="tblSchedule" Width="100%" BorderWidth="1px" BorderStyle="Solid" BorderColor="#E0E0E0" CellPadding="0" CellSpacing="0" GridLines="Both" CssClass="grid" runat="server"><asp:TableRow HorizontalAlign="Center" CssClass="grid_head" runat="server"><asp:TableCell Text="任务名称" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="70px" Text="工程量" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="70px" Text="完成工程量" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="50px" Text="完成度" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="70px" Text="剩余工程量" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="70px" Text="工程额" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="70px" Text="完成工程额" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="50px" Text="完成额度" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="70px" Text="剩余工程额" Wrap="false" runat="server"></asp:TableCell></asp:TableRow></asp:Table></div>
					</td>
				</tr>
			</table>
			<JWControl:PersistScrollPosition ID="PersistScrollPosition1" ControlToPersist="dvScheduleBox" runat="server"></JWControl:PersistScrollPosition>&nbsp;&nbsp;
			<input id="hdnPrjGuid" style="WIDTH: 10px" type="hidden" name="hdnPrjGuid" runat="server" />

			<input id="hdnTaskCode" style="WIDTH: 10px" type="hidden" name="hdnTaskCode" runat="server" />

			<input id="hdnProjectName" style="WIDTH: 10px" type="hidden" name="hdnProjectName" runat="server" />

			<input id="hdnScheduleCodeList" style="WIDTH: 10px" type="hidden" name="hdnScheduleCodeList" runat="server" />
 <input id="hdnView" style="WIDTH: 10px" type="hidden" value="0" name="hdnView" runat="server" />

			<input id="hdnNoteID" style="WIDTH: 10px" type="hidden" value="0" name="hdnNoteID" runat="server" />
&nbsp;
			<input id="hdnProjectCode" style="WIDTH: 10px" type="hidden" name="hdnProjectCode" runat="server" />
<input id="hdnPR" style="WIDTH: 10px" type="hidden" size="1" name="hdnPR" runat="server" />

			<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
			<script language="javascript">
			try
			{
				window.parent.document.getElementById("HdnProjectCode").value = document.getElementById("hdnProjectCode").value;
			}
			catch(e){}
			try
			{
				window.parent.document.getElementById("HdnProjectName").value = document.getElementById("hdnProjectName").value;
			}
			catch(e){}
			</script>
		</form>
	</body>
</HTML>
