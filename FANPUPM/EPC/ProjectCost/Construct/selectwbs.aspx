<%@ Page Language="C#" AutoEventWireup="true" CodeFile="selectwbs.aspx.cs" Inherits="SelectWBS" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>WBS选择</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<script type="text/javascript">
			function switchDisplay(obj,t1,t2)
			{
				/*在这之前增加你的处理代码*/
				doSwitchDisplay(obj,'tblSchedule','hdnScheduleCodeList',t1,t2,'../../../');//调用样式设置
			}
			function checkme(id)
			{
				var tasklist = document.getElementById('HdnTaskList').value;
				chkid = document.getElementById('cb'+id);
				if(chkid.checked)
				{
					if(tasklist.indexOf(id) == -1)
					{
						tasklist = tasklist+,+id;
					}
				}
				else
				{
					if(tasklist.indexOf(id) != -1)
					{
						var arr = tasklist.split(',');
						var task = '';
						for(i=0;i<arr.length;i++)
						{
							if(arr[i] != id)
							{
								task += ','+arr[i]
							}
						}
						tasklist = task;
					}
				}
				document.getElementById('HdnTaskList').value = tasklist;
			}
			function confirmTask()
			{
				var tasklist = document.getElementById('HdnTaskList').value;
				var task = parent.window.dialogArguments;
				task[0] = tasklist;
				window.close();
			}
		</script>
	</head>
	<body class="body_clear" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table class="table-none" id="Table2" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<tr>
					<td class="td-head" width="50%" style="HEIGHT: 23px"><asp:Label ID="lblProjectName" runat="server"></asp:Label>
						<input id="HdnTaskList" style="WIDTH:10px" type="hidden" runat="server" />
</td>
					<td class="td-toolsbar" align="right" style="HEIGHT: 23px">&nbsp; <INPUT type="button" value="确  定" onclick="confirmTask();" id="Button1">
						<INPUT id="BtnCancel" onclick="window.returnValue = false;window.close();" type="button"
							value="取  消">&nbsp;
					</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="2" height="100%">
						<div class="gridBox" id="dvScheduleBox"><asp:Table ID="tblSchedule" Width="100%" BorderWidth="1px" BorderStyle="Solid" BorderColor="#E0E0E0" CellPadding="0" CellSpacing="0" GridLines="Both" CssClass="grid" runat="server"><asp:TableRow HorizontalAlign="Center" CssClass="grid_head" runat="server"><asp:TableCell Width="40px" Wrap="false" Text="选择" runat="server"></asp:TableCell><asp:TableCell Wrap="false" Text="任务名称" runat="server"></asp:TableCell><asp:TableCell Width="90px" Wrap="false" Text="任务编码" runat="server"></asp:TableCell><asp:TableCell Width="70px" Wrap="false" Text="类型" runat="server"></asp:TableCell></asp:TableRow></asp:Table></div>
					</td>
				</tr>
			</table>
			<input id="hdnTaskCode" style="WIDTH: 10px" type="hidden" name="hdnTaskCode" runat="server" />

			<input id="hdnProjectName" style="WIDTH: 10px" type="hidden" name="hdnProjectName" runat="server" />
&nbsp;
			<input id="hdnProjectCode" style="WIDTH: 10px" type="hidden" name="hdnProjectCode" runat="server" />

			<input id="hdnScheduleCodeList" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" runat="server" />

			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
		</form>
	</body>
</HTML>
