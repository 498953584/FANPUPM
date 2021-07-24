<%@ Page Language="C#" AutoEventWireup="true" CodeFile="roleuserlist.aspx.cs" Inherits="roleUserList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>roleUserList</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<font face="宋体">
				<table id="Table1" cellspacing="0" cellpadding="0" width="98%" align="center" border="0">
					<tr>
						<td>
							<asp:Label ID="lblCaption" runat="server"></asp:Label></td>
						<td></td>
					</tr>
					<tr>
						<td colspan="2">
							<asp:DataGrid ID="DataGrid1" AutoGenerateColumns="false" Width="100%" CellPadding="5" CssClass="table-nomal" AllowSorting="true" runat="server"><SelectedItemStyle Wrap="false"></SelectedItemStyle><EditItemStyle Wrap="false"></EditItemStyle><AlternatingItemStyle Wrap="false"></AlternatingItemStyle><ItemStyle Wrap="false" HorizontalAlign="Center"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><FooterStyle Wrap="false"></FooterStyle><Columns><asp:BoundColumn DataField="v_xm" HeaderText="用户名称" DataFormatString="{0}"></asp:BoundColumn><asp:BoundColumn DataField="v_bmqc" HeaderText="部门名称"></asp:BoundColumn><asp:TemplateColumn HeaderText="修改"><ItemTemplate>
											<asp:HyperLink ID="hlMod" runat="server">HyperLink</asp:HyperLink>
										</ItemTemplate></asp:TemplateColumn></Columns></asp:DataGrid></td>
					</tr>
				</table>
			</font>
		</form>
	</body>
</HTML>
