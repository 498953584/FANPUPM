<%@ Page Language="C#" AutoEventWireup="true" CodeFile="costframeanalyze.aspx.cs" Inherits="ProjectGSQuery" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>模板管理</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

        <script src="../Web_Client/Tree.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
			function clickRow(obj,workCode,PrjGuid,isHaveChild)
			{
				document.getElementById('btnAdd').disabled = false;
				document.getElementById('btnDel').disabled = isHaveChild;
				
				document.getElementById('btnUpd').disabled = false;
				/*在这之前增加你的处理代码*/
				doClick(obj,'tblWork');//调用样式设置
				document.getElementById('hdnWorkCode').value = workCode;
				document.getElementById('hdnPrjGuid').value = PrjGuid;
				//alert(workCode);
			}
			function ClickMarketRow(TempCode,PrjGuid,isHaveChild)
		    {
		    	document.getElementById('btnBind').disabled = isHaveChild;
		        document.getElementById('hdnWorkCode').value = TempCode;
		        document.getElementById('hdnPrjGuid').value = PrjGuid;
		        url="gonggongbufen.aspx?TempCode="+escape(TempCode)+"&PrjGuid="+PrjGuid;
		        //alert(url);
			    document.getElementById("BindButtom").src =url; 			
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
			    var TempCode= document.getElementById('hdnWorkCode').value;
		        var PrjGuid= document.getElementById('hdnPrjGuid').value;
			    var cbsCode = document.getElementById('hdnWorkCode').value;
				switch (opType)
				{
					case 'add':
						self.location.href = "CostFrameAnalyzeEdit.aspx?t=Add&nc="+cbsCode;
						break;
					case 'upd':
						self.location.href = "CostFrameAnalyzeEdit.aspx?t=Upd&nc="+cbsCode;
						break;
					case 'bind':
						url = "wbsquery.aspx?TempCode="+escape(TempCode)+"&PrjGuid="+PrjGuid;
						//alert(url);
						window.open(url,'', 'height=500, width=600, top=100, left=300, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=n o, status=no');
						//re = window.showModalDialog(url,window,'dialogHeight:400px;dialogWidth:800px;center:1;help:0;status:0;');
						break;
				}
				return false;
			}
		</script>
	</head>
	<body class="body_clear" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table class="table-none" id="Table2" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<tr class="td-toolstbar">
				    <td align="left">模板管理</td>
					<td>
					    <asp:Button ID="btnAdd" Text="新  增" CssClass="button-normal" runat="server" />
						<asp:Button ID="btnUpd" Text="修  改" Enabled="false" CssClass="button-normal" runat="server" />
						<asp:Button ID="btnDel" Text="删  除" Enabled="false" CssClass="button-normal" OnClick="btnDel_Click" runat="server" />
						<asp:Button ID="btnBind" Text="绑定预算" Enabled="false" CssClass="button-normal" runat="server" />
						<input id="hdnWorkCode" style="WIDTH: 10px" type="hidden" size="1" name="hdnWorkCode" runat="server" />

						<input id="hdnPrjGuid" style="WIDTH: 10px" type="hidden" name="hdnPrjGuid" runat="server" />

					</td>
				</tr>
				<tr>
					<td vAlign="top" height="100%" colspan="2">
						<div class="gridBox" id="dvScheduleBox">
						<asp:Table ID="tblWork" CssClass="grid" Width="100%" BorderWidth="1px" BorderStyle="Solid" BorderColor="#E0E0E0" CellPadding="0" CellSpacing="0" GridLines="Both" runat="server"><asp:TableRow HorizontalAlign="Center" CssClass="grid_head" runat="server"><asp:TableCell Text="任务名称" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Text="任务编码" Wrap="false" runat="server"></asp:TableCell></asp:TableRow></asp:Table></div>
					</td>
				</tr>
				<tr id="bottom" runat="server"><td colspan="2" runat="server">
				         <iframe id="BindButtom" frameborder="0" src="gonggongbufen.aspx?TempCode=&PrjGuid=ABB5D4B2-3A08-4C1D-881F-DEF5826F8267" width="100%" scrolling="auto" runat="server"></iframe>
				    </td></tr>
			</table>
			<input id="hdnScheduleCodeList" style="WIDTH: 10px" type="hidden" name="hdnScheduleCodeList" runat="server" />
 
			<input id="hdnProjectCode" style="WIDTH: 10px" type="hidden" name="hdnProjectCode" runat="server" />
</form>
	</body>
</HTML>
