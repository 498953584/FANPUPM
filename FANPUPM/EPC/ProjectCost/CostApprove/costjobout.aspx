<%@ Page Language="C#" AutoEventWireup="true" CodeFile="costjobout.aspx.cs" Inherits="CostJobOut" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>分包成本</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table id="Tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr class="td-toolsbar">
					<td height="20" align="left">分包成本</td>
					<td vAlign="top" height="20" align="right">&nbsp;
						<asp:Button ID="BtnReturn" Text="返 回" OnClick="BtnReturn_Click" runat="server" />
					</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="2">
						<div id="DVRationItem" class="gridBox">
							<asp:DataGrid ID="dgCostJobOut" Width="100%" AutoGenerateColumns="false" CssClass="grid" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
											<%# Container.ItemIndex + 1 %>
										</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="ContractCode" HeaderText="合同编号"></asp:BoundColumn><asp:BoundColumn DataField="ContractName" HeaderText="合同名称"></asp:BoundColumn><asp:BoundColumn DataField="ContractOther" HeaderText="分包商"></asp:BoundColumn><asp:BoundColumn DataField="SumMoney" HeaderText="合同金额(元)"></asp:BoundColumn><asp:BoundColumn DataField="CurrentSumMoney" HeaderText="本月计量"></asp:BoundColumn></Columns></asp:DataGrid></div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
