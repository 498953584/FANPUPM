<%@ Page Language="C#" AutoEventWireup="true" CodeFile="costapprovegather.aspx.cs" Inherits="CostApproveGather" EnableEventValidation="false" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>汇总表</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<script language="JavaScript">
			function clickRow(obj,workCode,isHaveChild,costType)
			{
				document.getElementById('hdnWorkCode').value = workCode;
				document.getElementById('hdnCostType').value = costType;
				document.getElementById('hdnIsHaveChild').value = isHaveChild;				
				
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
			
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Formv" method="post" runat="server">
			<table id="Tablex" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server"><tr runat="server"><td valign="top" height="20" runat="server"><input id="hdnWorkCode" style="WIDTH: 10px" type="hidden" size="1" name="hdnWorkCode" runat="server" />

						<input id="hdnWorkCodeList" style="WIDTH: 10px" type="hidden" name="hdnWorkCodeList" runat="server" />
&nbsp;
						<input id="hdnCostType" style="WIDTH: 10px" type="hidden" name="hdnCostType" runat="server" />

						<input id="hdnIsHaveChild" style="WIDTH: 10px" type="hidden" name="hdnIsHaveChild" runat="server" />

						<TABLE id="Tablev" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR id="TrDate">
								<td>成本核算汇总</td>
								<TD><asp:DropDownList ID="DDLYear" Width="70px" runat="server"></asp:DropDownList>年
									<asp:DropDownList ID="DDLMonth" Width="70px" runat="server"></asp:DropDownList>月
									<asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" runat="server" />
									 <asp:Button ID="btnExp" CssClass="button-normal" Text="导 出" Width="0px" OnClick="btnExp_Click1" runat="server" /></TD>
							</TR>
						</TABLE>
					</td></tr><tr runat="server"><td runat="server">
						<table id="Tablexc" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td vAlign="top" colSpan="3">
									<div class="gridBox" id="dvWorkBox"><asp:Table ID="tblWork" GridLines="Both" CellSpacing="0" CellPadding="0" BorderColor="#E0E0E0" BorderStyle="None" BorderWidth="1px" Width="100%" CssClass="grid" runat="server"><asp:TableRow HorizontalAlign="Center" CssClass="grid_head" runat="server"><asp:TableCell Wrap="false" Text="成本名称" runat="server"></asp:TableCell><asp:TableCell Width="20%" Wrap="false" Text="CBS 编  码" runat="server"></asp:TableCell><asp:TableCell Width="20%" Wrap="false" Text="金额(元)" runat="server"></asp:TableCell></asp:TableRow></asp:Table></div>
								</td>
							</tr>
						</table>
					</td></tr></table>
		</form>
	</body>
</HTML>
