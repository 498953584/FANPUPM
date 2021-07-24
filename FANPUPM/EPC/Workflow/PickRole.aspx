<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PickRole.aspx.cs" Inherits="PickRole" %>


<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>PickRole</title>
	<base target="_self" />
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
	<script language="javascript" type="text/javascript">
		var winNo = jw.getRequestParam('winNo');
		$(document).ready(function () {
			var jwTable = new JustWinTable('dgdRoleList');
			setHeight();
		});

		function dodblClick(roleId, roleName) {
			if (typeof top.ui.callback == 'function') {
				top.ui.callback({ code: roleId, name: roleName });
				top.ui.callback = null;
			}
			top.ui.closeWin({ winNo: winNo });
		}

		function doClick(roleId, roleName) {
			$('#hfldRowId').val(roleId);
			$('#hfldRowName').val(roleName);
			$('#btnSave').attr('disabled', false);
		}

		function setHeight() {
			var height = document.getElementById("td-role").clientHeight;
			document.getElementById("divrole").style.height = height;
		}

		// 关闭按钮事件
		function btncancel_click() {
			top.ui.closeWin({ winNo: winNo });
		}

		// 保存按钮事件
		function btnok_click() {
			if (typeof top.ui.callback == 'function') {
				top.ui.callback({ code: $('#hfldRowId').val(), name: $('#hfldRowName').val() });
				top.ui.callback = null;
			}
			top.ui.closeWin({ winNo: winNo });
		}
	</script>
</head>
<body>
	<form id="Form1" method="post" runat="server">
	<table width="100%" style="height: 100%" border="1">
		<tr>
			<td align="right">
				<asp:DropDownList ID="ddlRoleType" Width="135px" AutoPostBack="true" runat="server"><asp:ListItem Value="1" Text="项目相关" /><asp:ListItem Value="2" Text="群组" /><asp:ListItem Value="3" Text="部门相关" /></asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td id="td-role">
				<div style="overflow: auto; width: 100%;" id="divrole">
					<asp:DataGrid ID="dgdRoleList" AutoGenerateColumns="false" PageSize="15" Width="100%" CssClass="gvdata" AllowPaging="true" OnPageIndexChanged="dgdRoleList_PageIndexChanged" runat="server"><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号"><ItemTemplate>
									
								</ItemTemplate></asp:TemplateColumn><asp:BoundColumn DataField="RoleName" HeaderText="名称"></asp:BoundColumn></Columns><ItemStyle CssClass="rowa"></ItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><PagerStyle Mode="NumericPages"></PagerStyle></asp:DataGrid>
				</div>
			</td>
		</tr>
		<tr>
			<td style="text-align: right">
				<input id="btnSave" type="button" class="button-normal" value="确 定" onclick="btnok_click();"
					disabled="disabled" />
				<input id="btnCancel" type="button" value="取 消" class="button-normal" onclick="btncancel_click();" />
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hfldRowId" runat="server" />
	<asp:HiddenField ID="hfldRowName" runat="server" />
	</form>
</body>
</html>
