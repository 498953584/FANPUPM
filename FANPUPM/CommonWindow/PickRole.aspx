<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PickRole.aspx.cs" Inherits="PickRole" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>PickRole</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
			function dodblClick(roleId,roleName){
				var role = window.dialogArguments;
				role[0] = roleId;
				role[1] = roleName;
				//alert(corpID);
				//parent.window.returnvalue = role;
				window.close();
			}
		</script>
	</head>
	<body scroll="no" class="body_popup">
		<form id="Form1" method="post" runat="server">
			<table width="100%" height="100%" cellpadding="0" cellspacing="0">
				<tr>
					<td height="24" align="right">
						<asp:DropDownList ID="ddlRoleType" Width="135px" AutoPostBack="true" runat="server"><asp:ListItem Value="1" Text="项目相关" /><asp:ListItem Value="2" Text="项目无关" /><asp:ListItem Value="3" Text="部门相关" /></asp:DropDownList>&nbsp;&nbsp;&nbsp;
					</td>
				</tr>
				<tr>
					<td>
						<div style="OVERFLOW:auto;WIDTH:100%;HEIGHT:100%">
							<asp:DataGrid ID="dgdRoleList" AutoGenerateColumns="false" Width="100%" CssClass="grid" runat="server"><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号"><ItemTemplate>
											
										</ItemTemplate></asp:TemplateColumn><asp:BoundColumn DataField="RoleName" HeaderText="名称"></asp:BoundColumn></Columns><ItemStyle CssClass="grid_row"></ItemStyle></asp:DataGrid>
						</div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
