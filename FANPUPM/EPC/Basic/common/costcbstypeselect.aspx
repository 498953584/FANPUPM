<%@ Page Language="C#" AutoEventWireup="true" CodeFile="costcbstypeselect.aspx.cs" Inherits="CostcbsTypeSelect" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WBS分解</title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
		<script language="JavaScript">
			function clickRow(obj,workCode,isHaveChild,costType,workName)
			{
				//document.getElementById('hdnWorkCode').value = workCode;
				//document.getElementById('hdnCostType').value = costType;
				//document.getElementById('hdnIsHaveChild').value = isHaveChild;
				
				if (isHaveChild != 'True')
				{
					//window.alert(a != "true");
					parent.document.getElementById('hidCBSCode').value = workCode;
					parent.document.getElementById("hidCBSName").value = workName;
				}	
				else
				{
					//window.alert('请选择子节点!');
				}
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
	</HEAD>
	<body class="body_frame" scroll="no">
		<form id="Formx" method="post" runat="server">
			<table id="Tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td height="20">其它成本结构<input id="hdnWorkCode" style="WIDTH: 10px" type="hidden" size="1" name="hdnWorkCode" runat="server" />

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
