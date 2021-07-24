<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resourceitem.aspx.cs" Inherits="ResourceItem" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ResourceItem</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" type="text/javascript">
			function Setid()
			{
				document.getElementById("Hidden1").value = RecordObj.cells(1).innerText;
			}

			function dbClickResRow(ResId,ResName,rc,rn,stand)
			{
				var res = parent.window.dialogArguments;
				res[0] = ResId;
				res[1] = ResName;
				res[2] = rc;
				res[3] = rn;
				res[4] = stand;
				window.parent.close();
			}
			
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<font face="宋体">
				<p>
					<table class="table-none" id="Table1" style="TABLE-LAYOUT: fixed" height="100%" cellspacing="0"
						cellpadding="0" width="100%" border="0">
						<tr>
							<td style="HEIGHT: 3px" align="center" colspan="3"></td>
						<tr>
							<td width="100%" colspan="3">
								<div class="div-scroll" style="WIDTH: 100%; HEIGHT: 100%">
									<asp:DataGrid ID="DgdCategory" AutoGenerateColumns="false" Width="100%" CellPadding="0" CssClass="grid" runat="server"><AlternatingItemStyle CssClass="grid_row"></AlternatingItemStyle><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
													<%# Container.ItemIndex + 1 %>
												</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="ResourceCode" HeaderText="机械编码"></asp:BoundColumn><asp:BoundColumn DataField="ResourceName" HeaderText="机械名称"></asp:BoundColumn><asp:BoundColumn DataField="ResourceTypeName" HeaderText="机械类型名称"></asp:BoundColumn><asp:BoundColumn DataField="ResourceTypeCode" HeaderText="机械类型编码"></asp:BoundColumn><asp:BoundColumn DataField="Specification" HeaderText="规格"></asp:BoundColumn></Columns></asp:DataGrid></div>
								<input id="Hidden1" type="hidden" name="Hidden1" runat="server" />
</td>
						</tr>
					</table>
					<asp:Button ID="Btn_add" CssClass="button-normal" Text="新  增" Visible="false" runat="server" />
			</font>
		</form>
		</P>
	</body>
</HTML>
