<%@ Page Language="C#" AutoEventWireup="true" CodeFile="taskbilladd.aspx.cs" Inherits="TaskBillAdd" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>施工任务书</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript">
			function clickRow(TaskCode)
			{
				document.getElementById('BtnDelWork').disabled = false;
				document.getElementById('HdnTaskCode').value = TaskCode;
			}
		    function checkDecimal(sourObj)
		    {
			  if (sourObj.value=="")
			  {
				sourObj.value = 0;
			  }
			  if (!(new RegExp(/^\d+(\.\d+)?$/).test(sourObj.value)))
			  {
				alert('数据类型不正确！');
				sourObj.focus();
				return;
			  }
		    }
			function OpenPickWBS(projectcode)
			{
				var task = new Array();
				task[0] = "";
				var url = "SelectWBS.aspx?pc="+projectcode+"&tclist="+document.getElementById('HdnTaskList').value;
				window.showModalDialog(url,task,'dialogHeight:560px;dialogWidth:580px;center:1;help:0;status:0;');
				if(task[0] != "")
				{
					document.getElementById('HdnTaskList').value = task[0];
					return true;
				}
				else
				{
					return false;
				}
			}
			function dblclickTaskRow(projectcode,taskcode,bookcode)
			{
				var url = "SelectResource.aspx?pc="+projectcode+"&tc="+taskcode+"&bc="+bookcode;	
				var win = window.showModalDialog(url,window,'dialogHeight:360px;dialogWidth:680px;center:1;help:0;status:0;');
				if(win)
				{
					return true;
				}
				return false;
			}
			//上传附件
			function UpFile(t,RecordCode)
			{			
				var url = "/CommonWindow/Annex/AnnexList.aspx?mid="+t+"&rc="+RecordCode+"&at=0";			
				window.showModalDialog(url,window,'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');	
			}
		</script>
	</head>
	<body class="body_popup" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table class="table-normal" style="TABLE-LAYOUT: fixed" height="100%" cellSpacing="0" cellPadding="0"
				width="100%" border="1">
				<tr>
					<td class="td-head" style="HEIGHT: 25px">施 工 报 量</td>
					<td class="td-toolsbar" style="HEIGHT: 25px" colSpan="3"><asp:Button ID="btnSave" Text="保  存" CssClass="button-normal" OnClick="btnSave_Click" runat="server" /><asp:Button ID="BtnExit" Text="取  消" Visible="false" OnClick="BtnExit_Click" runat="server" />&nbsp;
						<input id="HdnProjectCode" style="WIDTH: 10px" type="hidden" name="HdnProjectCode" runat="server" />

					</td>
				</tr>
				<tr>
					<td colSpan="4">
						<TABLE id="Tablez" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="td-label" align="center" style="width: 2%; height: 23px">上报日期</TD>
								<TD style="width: 3%; height: 23px"><JWControl:DateBox ID="DbConstructDate" Width="121px" runat="server"></JWControl:DateBox></TD>
								<TD class="td-label" align="center" style="width: 3%; height: 23px">记录人</TD>
								<TD style="width: 4%; height: 23px"><asp:TextBox ID="TxtRecordPerson" Width="123px" runat="server"></asp:TextBox></TD>
								<TD class="td-label" width="8%" style="height: 23px">&nbsp;&nbsp;<MyUserControl:epc_usercontrol1_filelink_ascx ID="FileLink1" runat="server" />
                                </TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td align="center" colSpan="4" height="22">施工内容
						<asp:Button ID="BtnPickWBS" Text="选择任务" CssClass="button-normal" OnClick="BtnPickWBS_Click" runat="server" />&nbsp;<asp:Button ID="BtnDelWork" Text="删  除" CssClass="button-normal" Enabled="false" OnClick="BtnDelWork_Click" runat="server" /><input id="HdnTaskList" style="WIDTH: 10px" type="hidden" runat="server" />
<input id="HdnTaskCode" style="WIDTH: 10px" type="hidden" runat="server" />
</td>
				</tr>
				<tr height="100%">
					<td colSpan="4">
						<div class="gridBox" id="divWork"><asp:DataGrid ID="DGrdOverWOrk" CssClass="grid" AutoGenerateColumns="false" Width="100%" runat="server"><AlternatingItemStyle Wrap="false"></AlternatingItemStyle><ItemStyle Wrap="false" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="TaskName" HeaderText="分项工作"></asp:BoundColumn><asp:BoundColumn DataField="TotalQuantity" HeaderText="总工作量"></asp:BoundColumn><asp:BoundColumn DataField="LeavlQuantity" HeaderText="剩余工作量"></asp:BoundColumn><asp:TemplateColumn HeaderText="今日完成量">
<ItemTemplate>
											<asp:TextBox ID="TxtTodayComplete" Width="100%" onblur="checkDecimal(this)" runat="server"></asp:TextBox>
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="工作内容">
<ItemTemplate>
											<asp:TextBox ID="TxtWorkContent" Width="100%" runat="server"></asp:TextBox>
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="备注">
<ItemTemplate>
											<asp:TextBox ID="TxtTodayRemark" Width="100%" runat="server"></asp:TextBox>
										</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn Visible="false" DataField="TaskCode"></asp:BoundColumn><asp:TemplateColumn HeaderText="选择">
<ItemTemplate>
											<asp:Button ID="Button1" Text="选择资源" CssClass="button-normal" runat="server" />
										</ItemTemplate>
</asp:TemplateColumn></Columns></asp:DataGrid></div>
					</td>
				</tr>
				<tr>
					<td colSpan="4">技术质量安全工作纪录：</td>
				</tr>
				<tr>
					<td colSpan="4"><asp:TextBox ID="TxtQualityAndSafety" Width="100%" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox></td>
				</tr>
			</table>
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl><asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary></form>
	</body>
</HTML>
