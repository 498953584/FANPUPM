<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rolelist.aspx.cs" Inherits="roleList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>角色列表</title>
	<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
	<script language="javascript" type="text/javascript">
		function showEditWin(op, rt, rn) {
			var ret = false;
			if (op == 'add') {
				var url = '/oa/SysAdmin/RoleManage/Broker.aspx?Op=add&rt=' + rt + '&rn=' + escape(rn);
				top.ui.openWin({ title: '新增角色', url: url });
				//ret = window.showModalDialog('Broker.aspx?Op=add&rt=' + rt + '&rn=' + escape(rn), window, 'dialogHeight:200px;dialogWidth:300px;center:1;help:0;status:0;');
			}
			else if (op == 'edit') {
				var url = '/oa/SysAdmin/RoleManage/Broker.aspx?Op=edit&rt=' + rt + '&rn=' + escape(rn) + '&RecordID=' + window.document.getElementById('hfRecord').value;
				top.ui.openWin({ title: '编辑角色', url: url });
				//ret = window.showModalDialog(url, window, 'dialogHeight:200px;dialogWidth:300px;center:1;help:0;status:0;');
			}
			else if (op == 'modPriv') {
				window.showModalDialog('Broker.aspx?Op=modPriv&id=' + window.document.getElementById('hfRecord').value, window, 'dialogHeight:600px;dialogWidth:650px;center:1;help:0;status:0;');
			}

			// 新界面
			if (window.top._reloadTab)
				window.top._reloadTab();
			return ret;
		}

		function getRecordID(RecordID) {
			window.document.getElementById('hfRecord').value = RecordID;
			document.getElementById('btnEdit').disabled = false;
			document.getElementById('btnDel').disabled = false;
			document.getElementById('btnPrivilege').disabled = false;
		}
	</script>
</head>
<body class="body_frame" scroll="no" style="border: 0px;">
	<form id="Form1" method="post" headerstyle-cssclass="grid_head" runat="server">
	<font face="宋体">
		<table id="Table1" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center"
			border="0">
			<tr>
				<td colspan="2" class="td-toolsbar">
					&nbsp;<asp:Button ID="btnAdd" Text="新 增" runat="server" />
					<asp:Button ID="btnEdit" Text="修 改" Enabled="false" runat="server" />&nbsp;
					<asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
					<asp:Button ID="btnPrivilege" Enabled="false" Text="权限设置" runat="server" />
					<asp:HiddenField ID="hfRecord" runat="server" />
				</td>
			</tr>
			<tr>
				<td colspan="2" style="width: 100%; height: 100%" valign="top">
					<div id="Box" style="width: 100%; height: 100%; border: solid 0px red; overflow-y: scroll;">
						<asp:DataGrid ID="DataGrid1" DataSourceID="Sql" Width="100%" CssClass="grid" ItemStyle-CssClass="grid_row" HeaderStyle-CssClass="grid_head" AutoGenerateColumns="false" OnItemDataBound="DataGrid1_ItemDataBound" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
										<%# Container.ItemIndex + 1 %>
									</ItemTemplate>

<EditItemTemplate>
										<asp:TextBox runat="server"></asp:TextBox>
									</EditItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="RoleCode" HeaderText="角色代码" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="RoleName" HeaderText="角色名称"></asp:BoundColumn></Columns></asp:DataGrid>
					</div>
				</td>
			</tr>
		</table>
		<asp:SqlDataSource ID="Sql" SelectCommand="SELECT [RoleCode], [RoleName] FROM [PT_Role] WHERE [IsValid] = '1' ORDER BY RoleCode" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter DefaultValue="000" Name="RoleTypeCode" QueryStringField="rt" Type="String"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
		<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
	</font>
	</form>
</body>
</html>
