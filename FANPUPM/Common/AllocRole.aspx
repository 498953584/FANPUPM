<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AllocRole.aspx.cs" Inherits="Common_AllocRole" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>选择角色</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var jwTable = new JustWinTable('gvwRole');

			// 选中已经存在的角色
			if ($('#hfldRoleId').val()) {
				$('#gvwRole tr[id]').each(function () {
					if ($('#hfldRoleId').val().indexOf(this.id) > -1) {
						$(this).find(':checkbox').attr('checked', 'true');
					}
				});
			}
		});

		function saveEvent() {
			var json = getCheckedIdJson('gvwRole');
			$('#hfldRoleId').val(json);
		}

		function succeed() {
			var winNo = jw.getRequestParam('winNo');
			top.ui.show('保存成功！');
			top.ui.closeWin({ winNo: winNo });
			top.ui.reloadTab();
		}

		function cancelEvent() {
			top.ui.closeWin();
		}
		
	</script>
</head>
<body style="overflow: hidden">
	<form id="form1" runat="server">
	<div style="height: 95%; overflow:auto">
		<asp:GridView ID="gvwRole" CssClass="gvdata" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvwRole_RowDataBound" DataKeyNames="Id" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
						<asp:CheckBox ID="chkSelectAll" runat="server" />
					</HeaderTemplate><ItemTemplate>
						<asp:CheckBox ID="chkSelectSingle" runat="server" />
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
						<%# Container.DataItemIndex + 1 %>
					</ItemTemplate></asp:TemplateField><asp:BoundField DataField="Name" HeaderText="名称" HeaderStyle-Width="300px" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
	</div>
	<div style="width: 98%; text-align: right; margin-top: 5px; overflow:hidden">
		<asp:Button ID="btnSave" Text="保存" OnClientClick="saveEvent();" OnClick="btnSave_Click" runat="server" />
		<input type="button" id="btnCancel" value="取消" onclick="cancelEvent();" />
	</div>
	<asp:HiddenField ID="hfldRoleId" runat="server" />
	</form>
</body>
</html>
