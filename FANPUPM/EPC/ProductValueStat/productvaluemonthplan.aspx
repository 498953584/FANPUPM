<%@ Page Language="C#" AutoEventWireup="true" CodeFile="productvaluemonthplan.aspx.cs" Inherits="ProductValueMonthPlan" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ProductValueMonthPlan</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
		<script language="JavaScript">
			function OpenProduceValue(projectCode)
			{
				//if(projectCode != "")
				//{
				//	location.href = "ProductValuePlanFrm.aspx?t=1&pc="+projectCode;
				//}
				//return false;
				window.history.go(-1);
				return false;
			}
			function switchDisplay(obj,t1,t2)
			{
				/*在这之前增加你的处理代码*/
				doSwitchDisplay(obj,'tblScheduleView','hdnScheduleCodeList',t1,t2,'../../../');//调用样式设置
			}
			function CheckInputIsFloat(keyCode,e)
			{
				if((keyCode>95 && keyCode<106) || (keyCode>47 && keyCode<58) || keyCode == 8|| keyCode == 46 || keyCode == 37 || keyCode == 39 || keyCode == 9 || keyCode == 13)
				{
			    }
				else if(keyCode == 110 || keyCode==190)
				{
					 if(e.value == "" || e.value.indexOf(".") != -1)
					 {
						 return false;
					 }
				} 
				else
				{
					return false;
				}
			}
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Formx" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" style="TABLE-LAYOUT:fixed">
				<tr>
					<td class="td-toolsbar" height="20">
						<asp:Label ID="Lb_title" Width="70%" runat="server"></asp:Label>
						<input id="hdnScheduleCodeList" style="WIDTH: 10px" type="hidden" name="hdnScheduleCodeList" runat="server" />

						<asp:Button ID="BtnExamine" Visible="false" Text="审  核" style="DISPLAY:none" OnClick="BtnExamine_Click" runat="server" />
						<asp:Button ID="BtnSave" TabIndex="-1" Text="保  存" CssClass="button-normal" OnClick="BtnSave_Click" runat="server" />
						<asp:Button ID="BtnExit" Text="取  消" runat="server" />&nbsp;
					</td>
				</tr>
				<tr>
					<td vAlign="top">
						<div id="dvScheduleBox" class="gridBox"><asp:Table ID="tblScheduleView" Width="100%" BorderWidth="1px" BorderStyle="None" BorderColor="#E0E0E0" CellPadding="0" CellSpacing="0" GridLines="Both" runat="server"><asp:TableRow HorizontalAlign="Center" CssClass="grid_head" runat="server"><asp:TableCell Wrap="false" Text="任务名称" runat="server"></asp:TableCell><asp:TableCell Width="70px" Wrap="false" Text="计量单位" runat="server"></asp:TableCell><asp:TableCell Width="70px" Wrap="false" Text="工程量" runat="server"></asp:TableCell><asp:TableCell Width="70px" Wrap="false" Text="计划完成" runat="server"></asp:TableCell><asp:TableCell Width="70px" Wrap="false" Text="计划累计" runat="server"></asp:TableCell><asp:TableCell Width="110px" Wrap="false" Text="承包合同单价(元)" runat="server"></asp:TableCell><asp:TableCell Width="70px" Wrap="false" Text="计划产值" runat="server"></asp:TableCell><asp:TableCell Width="0px" Visible="false" Text="WorkLayer" runat="server"></asp:TableCell><asp:TableCell Width="0px" Visible="false" Text="TaskCode" runat="server"></asp:TableCell></asp:TableRow></asp:Table></div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
