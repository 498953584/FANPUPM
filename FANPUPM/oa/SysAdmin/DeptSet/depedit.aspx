<%@ Page Language="C#" AutoEventWireup="true" CodeFile="depedit.aspx.cs" Inherits="oa_SysAdmin_DeptSet_depedit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
	<style type="text/css">
		body
		{
		}
		div
		{
			vertical-align: top;
		}
		.tab-win
		{
			vertical-align: top;
		}
	</style>
	<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/jquery.easyui.extension.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$('#btnOk')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
		});
		function addSucessed() {
			top.ui.show('添加成功');
			top.ui.closeWin();
			var sjdm = getRequestParam('sjdm');
			top.ui._OrgManager.location.href = jw.modifyParam({ url: top.ui._OrgManager.location.href, name: 'sel_id', value: sjdm });
		}

		function editSucessed() {
			top.ui.show('编辑成功');
			top.ui.closeWin();
			top.ui.deptset2.location.href = top.ui.deptset2.location.href;
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table class="tab-win">
		<tr>
			<td style="width: 150px; text-align: right;">
				序号：
			</td>
			<td>
				<asp:TextBox ID="txtNo" Width="220px" CssClass="easyui-validatebox easyui-numberbox" data-options="required:true" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td style="text-align: right;">
				名称：
			</td>
			<td>
				<asp:TextBox ID="txtName" Width="220px" CssClass="easyui-validatebox" data-options="required:true, validType:'validChars[50]'" runat="server"></asp:TextBox>
			</td>
		</tr>
	</table>
	<div style="position: absolute; bottom: 8px; right: 10px;">
		<asp:Button ID="btnOk" Text="保存" OnClick="btnOk_Click" runat="server" />
		<input type="button" id="btnCancel" onclick="top.ui.closeWin();" value="取消" />
	</div>
	</form>
</body>
</html>
