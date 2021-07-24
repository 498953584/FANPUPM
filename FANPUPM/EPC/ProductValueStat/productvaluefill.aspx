<%@ Page Language="C#" AutoEventWireup="true" CodeFile="productvaluefill.aspx.cs" Inherits="ProductValueFill" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>产值填报</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<script language="JavaScript">
			function OpenProduceValue(projectCode)
			{
				//if(projectCode != "")
				//{
				//	location.href = "ProductValueMonthReport.aspx?t=1&pc="+projectCode+"&pn="+'<%=ProjectName %>';
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
			function OpenAnnex(projectCode,type)
			{
				var t = 0;
				if(type == "Upd")
				{
					t = 0;
				}
				if(type == "Other")
				{
					t = 1;
				}
				var url = "/CommonWindow/Annex/AnnexList.aspx?mid=33&rc="+projectCode+"&at=0&type="+t;
				window.showModalDialog(url,window,'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
			}
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Formx" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" style="TABLE-LAYOUT:fixed">
				<tr class="td-toolsbar">
					<td align="left">
						<asp:Label ID="LblMsg" runat="server"></asp:Label></td>
					<td height="20"><input id="hdnScheduleCodeList" style="WIDTH: 10px" type="hidden" name="hdnScheduleCodeList" runat="server" />
&nbsp; <input id="BtnAnnex" type="button" style="DISPLAY:none; wdith:70px" value="附  件" runat="server" />

						<asp:Button ID="BtnSave" TabIndex="-1" CssClass="button-normal" Text="保  存" OnClick="BtnSave_Click" runat="server" />
						<asp:Button ID="BtnExit" Text="取  消" runat="server" />&nbsp;
					</td>
				</tr>
				<tr>
					<td vAlign="top" colspan="2">
						<div id="dvScheduleBox" class="gridBox"><asp:Table ID="tblScheduleView" GridLines="Both" CellSpacing="0" CellPadding="0" BorderColor="#E0E0E0" BorderStyle="None" BorderWidth="1px" Width="100%" runat="server"><asp:TableRow HorizontalAlign="Center" CssClass="grid_head" runat="server"><asp:TableCell Wrap="false" Text="任务名称" runat="server"></asp:TableCell><asp:TableCell Width="60px" Wrap="false" Text="计量单位" runat="server"></asp:TableCell><asp:TableCell Width="50px" Wrap="false" Text="工程量" runat="server"></asp:TableCell><asp:TableCell Width="80px" Wrap="false" Text="统计完成量" runat="server"></asp:TableCell><asp:TableCell Width="60px" Wrap="false" Text="上报量" runat="server"></asp:TableCell><asp:TableCell Width="60px" Wrap="false" Text="复核量" runat="server"></asp:TableCell><asp:TableCell Width="80px" Wrap="false" Text="累计复核量" runat="server"></asp:TableCell><asp:TableCell Width="110px" Wrap="false" Text="承包合同单价(元)" runat="server"></asp:TableCell><asp:TableCell Width="60px" Wrap="false" Text="产值" runat="server"></asp:TableCell><asp:TableCell Width="0px" Visible="false" Text="WorkLayer" runat="server"></asp:TableCell><asp:TableCell Width="0px" Visible="false" Text="TaskCode" runat="server"></asp:TableCell></asp:TableRow></asp:Table></div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
