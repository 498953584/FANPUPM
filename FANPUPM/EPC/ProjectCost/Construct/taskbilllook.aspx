<%@ Page Language="C#" AutoEventWireup="true" CodeFile="taskbilllook.aspx.cs" Inherits="TaskBillLook" %>
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

		<base target="_self"/>
		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<script language="javascript">
			function clickRow(TaskCode)
			{
				document.getElementById('HdnTaskCode').value = TaskCode;
			}
			function dblclickTaskRow(projectcode,taskcode,bookcode)
			{
				var url = "SelectResource.aspx?pc="+projectcode+"&tc="+taskcode+"&bc="+bookcode+"&optype=look";	
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
				var url = "/CommonWindow/Annex/AnnexListRead.aspx?mid="+t+"&rc="+RecordCode+"&at=0";			
				window.showModalDialog(url,window,'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');	
			}
		</script>
	</head>
	<body class="body_popup" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table class="table-normal" style="TABLE-LAYOUT: fixed" height="100%" cellSpacing="0" cellPadding="0"
				width="100%" border="1">
				<tr>
					<td class="td-head" style="HEIGHT: 25px" colSpan="4">施 工 报 量<input id="HdnProjectCode" style="WIDTH: 10px" type="hidden" name="HdnProjectCode" runat="server" />

						<input id="HdnId" style="WIDTH: 10px" type="hidden" name="HdnProjectCode" runat="server" />
</td>
					</td></tr>
				<tr>
					<td colSpan="4">
						<TABLE id="Tablez" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="td-label" width="8%" style="height: 22px">上报日期</TD>
								<TD width="25%" style="height: 22px"><asp:Label ID="lblConstructDate" Width="128px" runat="server"></asp:Label></TD>
								<TD class="td-label" width="8%" style="height: 22px">记录人</TD>
								<TD width="25%" style="height: 22px"><asp:Label ID="lblRecordPerson" Width="128px" runat="server"></asp:Label></TD>
								<TD class="td-label" width="8%" style="height: 22px">&nbsp;&nbsp;<INPUT class="button-normal" onclick="javascript:window.returnValue=false;window.close();"
										type="button" value="关 闭"></TD>
							</TR>
						</TABLE>
                        <MyUserControl:epc_usercontrol1_filelink_ascx ID="FileLink1" runat="server" />
					</td>
				</tr>
				<tr>
					<td align="left" colSpan="4" height="22">施工内容<input id="HdnTaskList" style="WIDTH: 10px" type="hidden" name="HdnTaskList" runat="server" />
<input id="HdnTaskCode" style="WIDTH: 10px" type="hidden" name="HdnTaskCode" runat="server" />
</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" colSpan="4">
						<div class="gridBox" id="divWork"><asp:DataGrid ID="DGrdOverWOrk" CssClass="grid" AutoGenerateColumns="false" Width="100%" runat="server"><AlternatingItemStyle Wrap="false"></AlternatingItemStyle><ItemStyle Wrap="false" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="TaskName" HeaderText="分项工作"></asp:BoundColumn><asp:BoundColumn DataField="TotalQuantity" HeaderText="总工作量"></asp:BoundColumn><asp:BoundColumn DataField="LeavlQuantity" HeaderText="剩余工作量"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="QuantityUnit" HeaderText="单位"></asp:BoundColumn><asp:BoundColumn DataField="TodayComplete" HeaderText="今日完成量"></asp:BoundColumn><asp:BoundColumn DataField="WorkContent" HeaderText="工作内容"></asp:BoundColumn><asp:BoundColumn DataField="TodayWorkRemark" HeaderText="备注"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="TaskCode"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="TodayComplete"></asp:BoundColumn><asp:TemplateColumn HeaderText="查看">
<ItemTemplate>
											<asp:Button ID="Button1" Text="查看资源" CssClass="button-normal" runat="server" />
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
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl><asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" ShowMessageBox="true" DisplayMode="List" runat="server"></asp:ValidationSummary></form>
	</body>
</HTML>
