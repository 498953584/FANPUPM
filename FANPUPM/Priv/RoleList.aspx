<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleList.aspx.cs" Inherits="Priv_RoleList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>角色</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/jquery.easyui.extension.css" />

	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var roleTable = new JustWinTable('gvwRole');
			$('#btnDelete').get(0).onclick = deleteRole;
			setButton(roleTable, 'btnDelete', 'btnEdit', 'btnConfigPerson', 'hfldRoleId');
		});

		// 新增角色
		function addRole() {
			var url = '/Priv/RoleEdit.aspx?action=add';
			top.ui.openWin({ title: '新增角色', url: url, width: 480, height: 200 });
		}

		// 编辑角色
		function editRole() {
			var url = '/Priv/RoleEdit.aspx?action=edit&id=' + $('#hfldRoleId').val();
			top.ui.openWin({ title: '编辑角色', url: url, width: 480, height: 200 });
		}

		// 删除角色
		function deleteRole() {
			return confirm("你确定要删除吗？");
		}

		// 配置人员
		function configPerson() {
			var url = '/Common/AllocUser.aspx?type=role&idJson=' + $('#hfldRoleId').val();
			top.ui.openWin({ title: '选择人员', url: url });
		}

	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<input id="btnAdd" type="button" value="新增" onclick="addRole();" />
		<input id="btnEdit" type="button" value="编辑" onclick="editRole();" disabled="disabled" />
		<asp:Button ID="btnDelete" Text="删除" OnClick="btnDelete_Click" runat="server" />
		<input id="btnConfigPerson" type="button" value="分配人员" onclick="configPerson();"
			disabled="disabled" style="width: 70px;" />
	</div>
	<div>
		<asp:GridView ID="gvwRole" CssClass="gvdata" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvwRole_RowDataBound" DataKeyNames="Id" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
						<asp:CheckBox ID="chkSelectAll" runat="server" />
					</HeaderTemplate><ItemTemplate>
						<asp:CheckBox ID="chkSelectSingle" runat="server" />
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
						<%# Container.DataItemIndex + 1 %>
					</ItemTemplate></asp:TemplateField><asp:BoundField DataField="Name" HeaderText="名称" HeaderStyle-Width="300px" /><asp:TemplateField HeaderText="人员"><ItemTemplate>
						<%# GetUserNameCsv(Eval("Id")) %>
					</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
	</div>
	<asp:HiddenField ID="hfldRoleId" runat="server" />
	</form>
</body>
</html>
