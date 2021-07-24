<%@ Page Language="C#" AutoEventWireup="true" CodeFile="costcbshypotaxistable.aspx.cs" Inherits="CostCbsHypotaxisTable" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>�����ɱ�Ԥ��</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript">
			function ClickRow(id)
			{
				document.getElementById('HdnRowId').value = id;
				document.getElementById('BtnDel').disabled = false;
			}
			function checkDecimal(sourObj)
			{
				if (sourObj.value=="")
				{
					sourObj.value = 0;
				}
				if (!(new RegExp(/^\d+(\.\d+)?$/).test(sourObj.value)))
				{
					alert('�������Ͳ���ȷ��');
					sourObj.focus();
					return;
				}
			}
			function checkNum(sourObj)
			{
				if (sourObj.value=="")
				{
					var d = new Date();
					sourObj.value = d.getMonth()+1;
				}
				if (!(new RegExp(/^\d+?$/).test(sourObj.value)))
				{
					alert('�·����Ͳ���ȷ!');
					sourObj.focus();
					return;
				}
				else
				{
					if(parseInt(sourObj.value)>12 || parseInt(sourObj.value)<1)
					{
						alert('��������ȷ�·�!');
						sourObj.focus();
						return;
					}
				}
			}
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Formx" method="post" runat="server">
			<table id="Tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr class="td-toolsbar">
					<td align="left">�����ɱ�Ԥ��</td>
					<td vAlign="top" height="20">&nbsp;
						<asp:Button ID="BtnAdd" Text="��  ��" OnClick="BtnAdd_Click" runat="server" />
						<asp:Button ID="BtnDel" Text="ɾ  ��" Enabled="false" OnClick="BtnDel_Click" runat="server" />
						<asp:Button ID="BtnSave" Text="��  ��" OnClick="BtnSave_Click" runat="server" />
						<INPUT onclick="javascript:returnValue=false;window.close();" type="button" value="��  ��">
						<input id="HdnRowId" style="WIDTH: 20px" type="hidden" runat="server" />
</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="2">
						<div class="gridBox" id="DVRationItem"><asp:DataGrid ID="DgCostCbs" CssClass="grid" AutoGenerateColumns="false" Width="100%" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="����">
<ItemTemplate>
											<%# Eval("NodeCode") %>
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="����">
<ItemTemplate>
											<%# Eval( "NodeName") %>
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="���">
<ItemTemplate>
											<asp:TextBox ID="TxtBudgetMoney" Width="100%" Text='<%# Convert.ToString(Eval( "BudgetMoney")) %>' runat="server"></asp:TextBox>
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="�·�">
<ItemTemplate>
											<asp:DropDownList ID="ddlYear" runat="server"><asp:ListItem Value="2006" Text="2006" /><asp:ListItem Value="2007" Text="2007" /><asp:ListItem Value="2008" Text="2008" /><asp:ListItem Value="2009" Text="2009" /><asp:ListItem Value="2010" Text="2010" /><asp:ListItem Value="2011" Text="2011" /></asp:DropDownList><FONT face="����">��</FONT>
											<asp:DropDownList ID="ddlMonth" runat="server"><asp:ListItem Value="1" Text="1" /><asp:ListItem Value="2" Text="2" /><asp:ListItem Value="3" Text="3" /><asp:ListItem Value="4" Text="4" /><asp:ListItem Value="5" Text="5" /><asp:ListItem Value="6" Text="6" /><asp:ListItem Value="7" Text="7" /><asp:ListItem Value="8" Text="8" /><asp:ListItem Value="9" Text="9" /><asp:ListItem Value="10" Text="10" /><asp:ListItem Value="11" Text="11" /><asp:ListItem Value="12" Text="12" /></asp:DropDownList><FONT face="����">��</FONT>
										</ItemTemplate>
</asp:TemplateColumn></Columns></asp:DataGrid></div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
