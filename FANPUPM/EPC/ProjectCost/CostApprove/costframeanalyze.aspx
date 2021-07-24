<%@ Page Language="C#" AutoEventWireup="true" CodeFile="costframeanalyze.aspx.cs" Inherits="CostFrameAnalyze" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>WBS分解</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
		<script language="JavaScript">
			function clickRow(obj,workCode,isHaveChild,costType)
			{
				if(costType == "核算成本")
				{
					document.getElementById('hdnCostType').value = 1;
				}
				if(costType == "其它成本")
				{
					document.getElementById('hdnCostType').value = 2;
				}
				if(costType == "分包成本")
				{
					document.getElementById('hdnCostType').value = 3;
				}
				document.getElementById('hdnWorkCode').value = workCode;
				document.getElementById('hdnIsHaveChild').value = isHaveChild;
				document.getElementById('btnAdd').disabled = false;
				document.getElementById('btnDel').disabled = isHaveChild;
				document.getElementById('btnUpd').disabled = false;
				if((workCode.length == 9 ) || costType == "分包成本")
				{
				//alert(costType);
					document.getElementById('btnAdd').disabled = true;
					document.getElementById('btnDel').disabled = true;
				}
				if((workCode.length == 6 ) && costType == "其它成本")
				{
				//alert(costType);
					document.getElementById('btnAdd').disabled = true;
					//document.getElementById('btnDel').disabled = true;
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
		
			
			function openCBSEdit(opType)
			{
				var cbsCode = document.getElementById('hdnWorkCode').value;
				var IsTrue = document.getElementById('hdnIsHaveChild').value;
				var costType = document.getElementById('hdnCostType').value;
				var costApproveType = "";
				switch(costType)
				{
					case '核算成本':
						costApproveType = "1";
						break;
					case '其它成本':
						costApproveType = "2";
						break;
					case '分包成本':
						costApproveType = "3";
						break;
				}
				switch (opType)
				{
					case 'add':
						self.location.href = "CostFrameAnalyzeEdit.aspx?t=Add&nc="+cbsCode+"&ct="+costType+"&tf="+IsTrue;	
						break;
					case 'upd':
						self.location.href = "CostFrameAnalyzeEdit.aspx?t=Upd&nc="+cbsCode+"&ct="+costType+"&tf="+IsTrue;		
						break;
				}
				return false;
			}
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Formx" method="post" runat="server">
			<table id="Tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr class="td-toolsbar">
					<td align=left>成本结构分解</td>
					<td><input id="hdnWorkCode" style="WIDTH: 10px" type="hidden" size="1" name="hdnWorkCode" runat="server" />

						<input id="hdnWorkCodeList" style="WIDTH: 10px" type="hidden" name="hdnWorkCodeList" runat="server" />
&nbsp;
						<input id="hdnCostType" style="WIDTH: 10px" type="hidden" name="hdnCostType" runat="server" />

						<input id="hdnIsHaveChild" style="WIDTH: 10px" type="hidden" name="hdnIsHaveChild" runat="server" />

						<asp:Button ID="btnAdd" Text="新  增" CssClass="button-normal" runat="server" />
						<asp:Button ID="btnUpd" Text="修  改" CssClass="button-normal" Enabled="false" runat="server" />
						<asp:Button ID="btnDel" Text="删  除" CssClass="button-normal" Enabled="false" Width="0px" OnClick="btnDel_Click" runat="server" />&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="3">
						<div id="dvWorkBox" class="gridBox"><asp:Table ID="tblWork" CssClass="grid_row" Width="100%" BorderWidth="1px" BorderStyle="None" BorderColor="#E0E0E0" CellPadding="0" CellSpacing="0" GridLines="Both" runat="server"><asp:TableRow HorizontalAlign="Center" CssClass="grid_head" runat="server"><asp:TableCell Wrap="false" Text="成本名称" runat="server"></asp:TableCell><asp:TableCell Width="20%" Wrap="false" Text="CBS 编  码" runat="server"></asp:TableCell><asp:TableCell Width="10%" Wrap="false" Text="类型" runat="server"></asp:TableCell><asp:TableCell Width="30%" Wrap="false" Text="说明" runat="server"></asp:TableCell></asp:TableRow></asp:Table></div>
					</td>
				</tr>
			</table>
			<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></form>
	</body>
</HTML>
