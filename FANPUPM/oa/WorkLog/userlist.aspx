<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userlist.aspx.cs" Inherits="userList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>userList</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<table align="center" id="table1" width="100%" border="0">
					<tr>
						<td width="100%">
							<asp:Label ID="headLbl" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td width="100%">
							<asp:DataGrid ID="DataGrid1" CssClass="grid" AutoGenerateColumns="false" Width="100%" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="姓名"><ItemTemplate>
											<asp:HyperLink runat="server"></asp:HyperLink>
										</ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="完成日志">
<ItemTemplate>
											<asp:Label runat="server"></asp:Label>
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="剔除">
<ItemTemplate>
											<asp:Label runat="server"></asp:Label>
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="含剔除总欠缺">
<ItemTemplate>
											<asp:Label runat="server"></asp:Label>
										</ItemTemplate>
</asp:TemplateColumn></Columns></asp:DataGrid>
						</td>
					</tr>
				</table>
			</FONT>
		</form>
	</body>
</HTML>
