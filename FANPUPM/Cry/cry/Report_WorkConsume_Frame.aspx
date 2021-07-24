<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report_WorkConsume_Frame.aspx.cs" Inherits="Report_WorkConsume_Frame" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Report_WorkConsume_Frame</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<meta http-equiv="Expires" content="0">
		<meta http-equiv="Cache-Control" content="no-cache">
		<meta http-equiv="Pragma" content="no-cache">
		<script language="javascript">
		
			//function switchDisplay(obj,t1,t2)
			//{
				/*在这之前增加你的处理代码*/
				
				//doSwitchDisplay(obj,'tblTask','HdnTaskCodeList',t1,t2,'../../../');//调用样式设置
			//}
			function doSelected(obj,TaskCode,Pc)
			{
				var hdnSelected = document.getElementById("hdnSelectedCodes");
				if(obj.checked)
				{
					hdnSelected.value += ,+TaskCode+,;
					hdnSelected.value = hdnSelected.value.replace(",,,,");
				}
				else
				{
					hdnSelected.value = hdnSelected.value.replace(,+TaskCode+,,,);
				}
				var Url = "Report_WorkConsume.aspx?pc="+Pc+"&tcs="+hdnSelected.value;
				document.getElementById("frmReport").src = Url;
			}
		</script>
	</HEAD>
	<body class="body_clear">
		<form id="Form1" method="post" runat="server">
			<table width="100%" height="100%" cellSpacing="0" cellPadding="0">
				<tr class="report_grid_head" id="TrHeader" bgColor="Gainsboro" >
					<td height="30">
                        项目名称
						<asp:DropDownList ID="ddlProjectList" AutoPostBack="true" runat="server"></asp:DropDownList>
					</td>
					<td rowspan="2"><iframe id="frmReport" width="100%" height="100%" src="../../EPC/UserControl1/webTreeTS.aspx" name="frmReport" scrolling=auto>
						</iframe>
					</td>
				</tr>
				<tr>
					<td width="200" valign="top">
						<DIV id="DVTaskBox" class="gridBox">
							<asp:Table ID="tblTask" GridLines="Both" CellSpacing="0" CellPadding="0" BorderColor="#E0E0E0" BorderStyle="Solid" BorderWidth="1px" Width="100%" CssClass="grid" runat="server"><asp:TableRow HorizontalAlign="Center" CssClass="grid_head" runat="server"><asp:TableCell Text="任务名称" Wrap="false" runat="server"></asp:TableCell></asp:TableRow></asp:Table></DIV>
						<input id="HdnTaskCodeList" type="hidden" runat="server" />
 <INPUT id="hdnSelectedCodes" type="hidden">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
