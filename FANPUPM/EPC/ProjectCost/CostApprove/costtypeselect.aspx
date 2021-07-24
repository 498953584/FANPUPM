<%@ Page Language="C#" AutoEventWireup="true" CodeFile="costtypeselect.aspx.cs" Inherits="CostTypeSelect" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>核算成本结构</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<script language="JavaScript">
			function clickRow(obj,workCode,isHaveChild,costType)
			{
				//document.getElementById('hdnWorkCode').value = workCode;
				//document.getElementById('hdnCostType').value = costType;
				//document.getElementById('hdnIsHaveChild').value = isHaveChild;
				/*在这之前增加你的处理代码*/
				doClick(obj,'tblWork');//调用样式设置
			}
			function outRow(obj)
			{
				/*在这之前增加你的处理代码*/
				doMouseOut(obj);//调用样式设置
			}
			function overRow(obj)
			{
				/*在这之前增加你的处理代码*/
				doMouseOver(obj);//调用样式设置
			}
			
			function switchDisplay(obj,t1,t2)
			{
				/*在这之前增加你的处理代码*/
				doSwitchDisplay(obj,'tblWork','hdnWorkCodeList',t1,t2,'../../../');//调用样式设置
			}
			function dbClickRow(obj,theCode,theName,isTrue)
			{
				if (isTrue != 'True')
				{
					var task = parent.window.dialogArguments;
					task[0] = theCode;
					task[1]	= theName;
					window.close();
				}
			}
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Formx" method="post" runat="server">
			<table id="Tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td height="20">核算成本结构<input id="hdnWorkCode" style="WIDTH: 10px" type="hidden" size="1" name="hdnWorkCode" runat="server" />

						<input id="hdnWorkCodeList" style="WIDTH: 10px" type="hidden" name="hdnWorkCodeList" runat="server" />
&nbsp;
						<input id="hdnCostType" style="WIDTH: 10px" type="hidden" name="hdnCostType" runat="server" />

						<input id="hdnIsHaveChild" style="WIDTH: 10px" type="hidden" name="hdnIsHaveChild" runat="server" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="3">
						<div id="dvWorkBox" class="gridBox"><asp:Table ID="tblWork" Width="100%" BorderWidth="1px" BorderStyle="None" BorderColor="#E0E0E0" CellPadding="0" CellSpacing="0" GridLines="Both" CssClass="grid" runat="server"><asp:TableRow HorizontalAlign="Center" CssClass="grid_head" runat="server"><asp:TableCell Wrap="false" Text="成本名称" runat="server"></asp:TableCell><asp:TableCell Width="20%" Wrap="false" Text="CBS 编  码" runat="server"></asp:TableCell><asp:TableCell Width="10%" Wrap="false" Text="类型" runat="server"></asp:TableCell><asp:TableCell Width="30%" Wrap="false" Text="说明" runat="server"></asp:TableCell></asp:TableRow></asp:Table></div>
					</td>
				</tr>
			</table>
			<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></form>
	</body>
</HTML>
