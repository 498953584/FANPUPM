<%@ Page Language="C#" AutoEventWireup="true" CodeFile="costcbs.aspx.cs" Inherits="CostCbs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>CBS分解</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<script language="javascript">
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
			<table id="Tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr class="td-toolsbar">
					<td align="left" height="20">其它成本预算</td>
					<td vAlign="top" height="20">&nbsp;
						<asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" /><input id="prjcode" type="hidden" runat="server" />
</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="2">
						<div class="gridBox" id="DVRationItem"><asp:DataGrid ID="DgCostCbs" Width="100%" AutoGenerateColumns="false" CssClass="grid" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="NodeCode" HeaderText="编码"></asp:BoundColumn><asp:BoundColumn DataField="NodeName" HeaderText="名称"></asp:BoundColumn><asp:TemplateColumn HeaderText="预算金额(元)">
<ItemTemplate>
											<asp:TextBox ID="txtPrice" Width="100px" CssClass="text-num" onkeydown="event.returnValue=CheckInputIsFloat(event.keyCode,this)" Text='<%# Convert.ToString(Eval("money", "{0:F2}")) %>' runat="server"></asp:TextBox>
										</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn Visible="false" DataField="IsHaveChild" HeaderText="子节点数"></asp:BoundColumn></Columns></asp:DataGrid></div>
					</td>
				</tr>
			</table>
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl></form>
	</body>
</HTML>
