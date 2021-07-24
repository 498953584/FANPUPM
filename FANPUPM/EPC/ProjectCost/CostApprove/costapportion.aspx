<%@ Page Language="C#" AutoEventWireup="true" CodeFile="costapportion.aspx.cs" Inherits="CostApportion" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>其它成本</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
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
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Formx" method="post" runat="server">
			<table id="Tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr class="td-toolsbar">
					<td height="20" align="left">其它成本</td>
					<td vAlign="top" height="20" align="right">&nbsp;
						<asp:Button ID="BtnReturn" Text="返 回" OnClick="BtnReturn_Click" runat="server" />
					</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="2">
						<div id="DVRationItem" class="gridBox">
							<asp:DataGrid ID="DGrdCostApprotion" Width="100%" AutoGenerateColumns="false" CssClass="grid" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="NodeCode" HeaderText="编码"></asp:BoundColumn><asp:BoundColumn DataField="NodeName" HeaderText="名称"></asp:BoundColumn><asp:BoundColumn DataField="money" HeaderText="金额(元)"></asp:BoundColumn></Columns></asp:DataGrid></div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
