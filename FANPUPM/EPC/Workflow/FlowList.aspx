<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlowList.aspx.cs" Inherits="FlowList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>FlowList</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
			function doClickRow(nodeId,theOrder)
			{
				document.getElementById('hdnNodeID').value = nodeId;
				document.getElementById('hdnTheOrder').value = theOrder;
				document.getElementById('btnStartWF').disabled = false;
			}
			function doClear()
			{
				document.getElementById('btnStartWF').disabled = true;
			}
			function openAuditdit()
			{
				var instanceCode = document.getElementById('hdnInstanceCode').value;
				var nodeId = document.getElementById('hdnNodeID').value;
				var theOrder = document.getElementById('hdnTheOrder').value;
				var url= "AuditEdit.aspx?ic="+instanceCode+"&ni="+nodeId+"&order="+theOrder;				
				return window.showModalDialog(url,window,"dialogHeight:180px;dialogWidth:400px;center:1;help:0;status:0;");
			}
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table class="table-none" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="td-toolsbar" align="right" height="24"><input id="hdnNodeID" style="WIDTH: 10px" type="hidden" name="hdnNodeID" runat="server" />

						<input id="hdnTheOrder" style="WIDTH: 10px" type="hidden" name="hdnTheOrder" runat="server" />

						<input id="hdnInstanceCode" style="WIDTH: 10px" type="hidden" name="hdnInstanceCode" runat="server" />
&nbsp;&nbsp;<asp:Button ID="btnStartWF" Text="审  核" Enabled="false" runat="server" />&nbsp;&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:DataGrid ID="dgdFlow" CssClass="grid" CellPadding="0" Width="100%" AutoGenerateColumns="false" runat="server"><AlternatingItemStyle CssClass="grid_row"></AlternatingItemStyle><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head" VerticalAlign="Middle"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
											
										</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="NodeName" HeaderText="节点名称"></asp:BoundColumn><asp:BoundColumn HeaderText="主审核人"></asp:BoundColumn><asp:BoundColumn HeaderText="最终审核人"></asp:BoundColumn><asp:BoundColumn DataField="AuditDate" HeaderText="审核时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"></asp:BoundColumn><asp:BoundColumn HeaderText="结果"></asp:BoundColumn><asp:BoundColumn DataField="AuditInfo" HeaderText="备注"></asp:BoundColumn></Columns></asp:DataGrid></div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
