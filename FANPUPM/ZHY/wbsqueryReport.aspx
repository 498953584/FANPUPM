<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wbsqueryReport.aspx.cs" Inherits="ProjectGSQuery" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>WBS查询</title><meta http-equiv="Content-Type" content="text/html; charset=uft-8" />
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
			function ParCheck()
			{
				try
				{
					window.parent.document.getElementById('BtnImport').disabled = false;
					window.parent.document.getElementById('hdnPR').value = document.getElementById('hdnPR').value;
					window.parent.document.getElementById('hdnPrjName').value = document.getElementById('lblProjectName').innerText;
					window.parent.document.getElementById('hdnPrjGuid').value = document.getElementById('hdnPrjGuid').value;	
				}
				catch(e){}
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
			}
	
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
				<tr class="td-toolstbar">
					<td align="left" width="0%"><asp:Label ID="lblProjectName" runat="server"></asp:Label></td>
					<td></td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="2" height="100%">
						<div class="gridBox" id="dvScheduleBox"><asp:Table ID="tblSchedule" CssClass="grid" Width="100%" BorderWidth="1px" BorderStyle="Solid" BorderColor="#E0E0E0" CellPadding="0" CellSpacing="0" GridLines="Both" runat="server"><asp:TableRow HorizontalAlign="Center" CssClass="grid_head" runat="server"><asp:TableCell Text="任务名称" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="90px" Text="任务编码" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="80px" Text="预算量" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="80px" Text="实际完成量" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="80px" Text="预算额度" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="80px" Text="实际成本" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="80px" Text="完成度" Wrap="false" runat="server"></asp:TableCell></asp:TableRow></asp:Table></div>
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
<input id="hdnPR" style="WIDTH: 10px" type="hidden" size="1" runat="server" />

			<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl><input id="HdnIsConfirm" type="hidden" size="1" runat="server" />
</form>
		<script language="javascript">
	ParCheck();
		</script>
	</body>
</HTML>
