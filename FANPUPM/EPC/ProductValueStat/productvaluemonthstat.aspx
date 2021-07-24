<%@ Page Language="C#" AutoEventWireup="true" CodeFile="productvaluemonthstat.aspx.cs" Inherits="ProductValueMonthStat" %>

<HTML>
	<head runat="server"><title>产值统计</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
			function OpenMonthProductValue(projectCode,taskCode)
			{
				var url= "MonthProductValueStatData.aspx?pc="+projectCode+"&tc="+taskCode;
				return window.showModalDialog(url,window,"dialogHeight:300px;dialogWidth:600px;center:1;help:0;status:0;");
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
			window.parent.document.getElementById('HdnProduceValue').value = '<%=ProjectCode %>';
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Formx" method="post" runat="server">
			<input id="hdnScheduleCodeList" style="WIDTH: 10px" type="hidden" name="hdnScheduleCodeList" runat="server" />

			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" style="table-layout:fixed">
				<tr class="td-toolsbar">
					<td height="22" align=left>
						<asp:Label ID="LblMsg" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td vAlign="top">
						<div id="dvScheduleBox" class="gridBox"><asp:Table ID="tblScheduleView" GridLines="Both" CellSpacing="0" CellPadding="0" BorderColor="#E0E0E0" BorderStyle="None" BorderWidth="1px" Width="100%" runat="server"><asp:TableRow HorizontalAlign="Center" CssClass="grid_head" runat="server"><asp:TableCell Wrap="false" Text="" runat="server"></asp:TableCell><asp:TableCell Width="120px" Wrap="false" Text="产值计划" runat="server"></asp:TableCell><asp:TableCell Width="120px" Wrap="false" Text="产值上报" runat="server"></asp:TableCell><asp:TableCell Width="120px" Wrap="false" Text="监理批量" runat="server"></asp:TableCell></asp:TableRow></asp:Table></div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
