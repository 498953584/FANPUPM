<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleEdit.aspx.cs" Inherits="Priv_RoleEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>角色</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/jquery.easyui.extension.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$('#btnSave')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
		});

		function succeed() {
			top.ui.show('保存成功');
			top.ui.closeWin();
			top.ui.reloadTab();
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<table style="width: 98%; height: 160px;">
			<tr>
				<td style="width: 50px; text-align: right;">
					<span class="star">* </span>名称
				</td>
				<td>
					<asp:TextBox ID="txtName" Width="200px" CssClass="easyui-validatebox" data-options="required:true, validType:'validChars[20]'" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
			</tr>
			<tr>
				<td colspan="2" style="text-align: right;">
					<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
					<input id="btnCancel" type="button" value="取消" onclick="top.ui.closeWin();" />
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
