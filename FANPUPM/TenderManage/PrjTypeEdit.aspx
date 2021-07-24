<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjTypeEdit.aspx.cs" Inherits="TenderManage_PrjTypeEdit" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>工程类型名称</title>
	<script type="text/javascript" src="../Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../Script/jw.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			document.getElementById('btnCancel').onclick = function () {
				winclose('PrjTypeEdit', 'PrjTypeList.aspx', false)
			}
			Val.validate('form1');
		});
		//限制输入字符长度
		function limitTextLenth(txtId) {
			var txtValue = txtId.value;
			if (txtValue.length > 20) {
				txtId.value = txtValue.substring(0, 20);
				top.ui.alert('输入的字数不能大于100个字');
			}
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divContent2">
		<table class="tableContent2">
			<tr>
				<td class="word">
					工程类型名称
				</td>
				<td class="elemTd">
					<asp:TextBox ID="txtPrjTypeName" BackColor="#FEFEF4" Height="15px" CssClass="{required:true, messages:{required:'工程类型名称必须输入!'}}" onkeyup="limitTextLenth(this);" runat="server"></asp:TextBox>
				</td>
			</tr>
		</table>
	</div>
	<div class="divFooter2">
		<table class="tableFooter2">
			<tr>
				<td>
					<asp:Button ID="btnSave" Text="保存" OnClientClick="return valForm()" OnClick="btnSave_Click" runat="server" />
					<input type="button" id="btnCancel" value="取消" onclick="" />
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hlfdItemCode" runat="server" />
	</form>
</body>
</html>
