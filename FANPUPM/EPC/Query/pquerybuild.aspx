<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pquerybuild.aspx.cs" Inherits="PQueryBuild" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>综合查询</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript">
			function doCheck(obj,dType,isEmpty)
			{
				
			}
		</script>
	</head>
	<body class="body_popup" scroll="no">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体"></FONT>
			<table class="table-normal" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="1">
				<tr>
					<td class="td-head" colSpan="3" height="28">综合查询</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="3">
						<div style="OVERFLOW:auto;WIDTH:100%;HEIGHT:100%">
							<asp:DataGrid ID="dgdQuery" AutoGenerateColumns="false" CellPadding="0" Width="100%" CssClass="grid" runat="server"><AlternatingItemStyle CssClass="grid_row"></AlternatingItemStyle><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="查询条件项">
<ItemTemplate>
											<asp:Label Text='<%# Convert.ToString(Eval("ItemName")) %>' runat="server"></asp:Label>
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="查询条件值"><ItemTemplate>
											<asp:TextBox Width="100%" Text='<%# Convert.ToString(Eval("ItemValue")) %>' runat="server"></asp:TextBox>
										</ItemTemplate></asp:TemplateColumn></Columns></asp:DataGrid></div>
					</td>
				</tr>
				<tr>
					<td colSpan="3" height="28" align="right"><JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl><asp:Button ID="btnOk" Text="确 定" CssClass="button-normal" OnClick="btnOk_Click" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" Text="取 消" CssClass="button-normal" OnClick="btnCancel_Click" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
