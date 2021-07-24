<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectOneUser.aspx.cs" Inherits="Common_SelectOneUser" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
	<base target="_self" />
	<script type="text/javascript" src="../Script/jquery.js"></script>
	<script type="text/javascript" src="../Script/json2.js"></script>
	<script type="text/javascript" src="../Script/jw.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var userJt = new JustWinTable('gvwUser');

			// 当前节点是单人的时候，隐藏复选框
			var type = getRequestParam('type');
			if (type != 'multi')
				$('.chk').remove();

			// 添加行单击事件
			userJt.registClickTrListener(function () {
				$('#hfldUserCode').val(this.id);
				$('#hfldUserName').val($(this).attr('name'));
			});

			// 添加点击复选框事件
			userJt.registClickSingleCHKListener(function () {
				var chks = userJt.getCheckedChk();
				clickChkEvent(chks);
			});

			// 添加全选事件
			userJt.registClickAllCHKLitener(function () {
				var chks = userJt.getAllChk();
				clickChkEvent(chks);
			});

			userJt.registDbClickListener(saveEvent);
		});

		function saveEvent() {
			var obj = {};
			obj.code = $('#hfldUserCode').val();
			obj.name = $('#hfldUserName').val();
			if (typeof top.ui.callback == 'function') {
				top.ui.callback(obj);
				top.ui.callback = null;
			}
			top.ui.closeWin({ winNo: getRequestParam('winNo') });
		}

		function clickChkEvent(chks) {
			var id = '';
			var name = '';

			for (var i = 0; i < chks.length; i++) {
				var td = getFirstParentElement(chks[i], 'TR');
				id += td.id + ',';
				name += $(td).attr('name') + ',';
			}

			$('#hfldUserCode').val(id);
			$('#hfldUserName').val(name);
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table class="" cellpadding="3px" cellspacing="0px" style="">
		<tr>
			<td style="white-space: nowrap; width: 70px;">
				用户名称
			</td>
			<td style="width: 200px">
				<asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
			</td>
			<td>
				<asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
			</td>
		</tr>
	</table>
	<div class="pagediv" style="height: 385px; overflow:auto;">
		<asp:GridView ID="gvwUser" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwUser_RowDataBound" DataKeyNames="v_yhdm,v_xm" runat="server"><Columns><asp:TemplateField HeaderStyle-CssClass="chk" ItemStyle-CssClass="chk">
<HeaderTemplate>
						<asp:CheckBox ID="chkAll" runat="server" />
					</HeaderTemplate>

<ItemTemplate>
						<asp:CheckBox ID="chkSingle" runat="server" />
					</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="No" HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Right" /><asp:BoundField DataField="v_xm" HeaderText="用户名称" HeaderStyle-Width="150px" /><asp:BoundField DataField="V_BMMC" HeaderText="部门名称" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
	</div>
	<div style="text-align: right;  padding: 10px 10px;">
		<input type="button" id="btnOk" value="保存" onclick="saveEvent();" />
		<input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
	</div>
	<asp:HiddenField ID="hfldUserCode" runat="server" />
	<asp:HiddenField ID="hfldUserName" runat="server" />
	</form>
</body>
</html>
