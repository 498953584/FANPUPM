<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleList.aspx.cs" Inherits="RoleList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>RoleList</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var jwTable = new JustWinTable('dgdRole');
			setHeight();
		});

		function doClickRow(roleId) {
			document.getElementById('hdnRoleID').value = roleId;
			document.getElementById('btnDel').disabled = false;
			document.getElementById('btnEdit').disabled = false;
		}
		function openRoleEdit(op) {
			var roleID = 0;
			if (op == 2) {
				roleID = document.getElementById('hdnRoleID').value;
			}
			var roleType = document.getElementById('hdnTypeID').value;
			var url = "RoleEdit.aspx?t=" + op + "&ri=" + roleID + "&rt=" + roleType;
			return window.showModalDialog(url, window, "dialogHeight:200px;dialogWidth:450px;center:1;help:0;status:0;");
		}
		function openRoleEdit(op) {
			var roleID = 0;
			if (op == 2) {
				roleID = document.getElementById('hdnRoleID').value;
			}
			var roleType = document.getElementById('hdnTypeID').value;
			var url = "/EPC/Workflow/RoleEdit.aspx?t=" + op + "&ri=" + roleID + "&rt=" + roleType;
			top.ui.rolelist = window;
			top.ui.openWin({ title: '角色信息维护', url: url });
		}
		function setHeight() {
			var height = document.getElementById("td-role").clientHeight;
			document.getElementById("rolediv").style.height = height;
		}
		function successed() {
			top.ui.show('保存成功');
			top.ui.closeWin();
			var url = { "url": "EPC/Workflow/FlowRole.aspx" };
			top.ui.reloadTab(url);

		}
	</script>
</head>
<body>
	<form id="Form1" method="post" runat="server">
	<table cellspacing="0" cellpadding="0" style="height: 99%; width: 100%;">
		<tr>
			<td height="24px">
				<input id="hdnRoleID" style="width: 10px" type="hidden" name="hdnRoleID" runat="server" />

				<input id="hdnTypeID" style="width: 10px" type="hidden" name="hdnTypeID" runat="server" />

				<input type="button" value="新增" id="btnAdd" runat="server" />

				<input type="button" value="修改" disabled="true" id="btnEdit" runat="server" />

				<asp:Button ID="btnDel" Text="删 除" Enabled="false" runat="server" />&nbsp;
			</td>
		</tr>
		<tr>
			<td valign="top" id="td-role">
				<div style="overflow: auto; width: 100%; vertical-align: top; height: 100%" id="rolediv">
					<asp:DataGrid ID="dgdRole" CssClass="gvdata" CellPadding="0" AllowPaging="true" PageSize="25" Width="100%" AutoGenerateColumns="false" OnPageIndexChanged="dgdRole_PageIndexChanged" runat="server"><ItemStyle CssClass="rowa"></ItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
									
								</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="RoleName" HeaderText="角色名称"></asp:BoundColumn><asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn></Columns><PagerStyle Mode="NumericPages"></PagerStyle></asp:DataGrid></div>
			</td>
		</tr>
	</table>
	</form>
</body>
</html>
