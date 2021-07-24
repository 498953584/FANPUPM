<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceTypeEdit.aspx.cs" Inherits="BudgetManage_Resource_ResourceTypeEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>资源分类设置</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			if (getRequestParam('t') == '1') {
				setAllInputDisabled();
			}
			Val.validate('form1');

			if ($('#hfldParentId').val() == '') {
				$('#trCBS').show();
			} else {
				$('#trCBS').hide();
			}
		});
          
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divContent" style="height: 300px;">
		<table width="98%" cellpadding="5px" cellspacing="0">
			<tr>
				<td style="text-align: right; width: 80px;">
					资源分类编号
				</td>
				<td align="left">
					<asp:TextBox ID="txtResourceCode" Enabled="false" BackColor="#FEFEF4" Height="15px" Width="60%" runat="server"></asp:TextBox>
					<asp:TextBox ID="txtCode" BackColor="#FEFEF4" Height="15px" Width="40px" CssClass="{required:true, messages:{required:'资源分类编码必须输入'}}" runat="server"></asp:TextBox><span style="color: Red; font-weight: bold;">注:同级编码不可重复</span><asp:RegularExpressionValidator ID="RegularExpressionValidator2" ErrorMessage="请输入字符个数少于20个的字符,空格包含在内" ControlToValidate="txtCode" ValidationExpression=".{1,20}" runat="server"></asp:RegularExpressionValidator>
				</td>
			</tr>
			<tr>
				<td style="text-align: right; width: 60px;">
					资源分类名称
				</td>
				<td>
					<asp:TextBox ID="txtResourceName" BackColor="#FEFEF4" Height="15px" Width="100%" CssClass="{required:true, messages:{required:'资源分类名称必须输入'}}" runat="server"></asp:TextBox>
						<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ErrorMessage="请输入字符个数少于20个的字符,空格包含在内" ControlToValidate="txtResourceName" ValidationExpression=".{1,20}" runat="server"></asp:RegularExpressionValidator>
				</td>
			</tr>
			<tr id="trCBS">
				<td style="text-align: right; width: 60px;">
					直接成本
				</td>
				<td>
					<asp:DropDownList ID="ddlCBS" Width="100%" runat="server"></asp:DropDownList>
				</td>
			</tr>
		</table>
	</div>
	<div style="text-align: right; padding-right: 10px;">
		<asp:Button ID="btnSave" Text="保存" OnClientClick="return valForm()" OnClick="btnSave_Click" runat="server" />
		<input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin()" />
	</div>
	<asp:HiddenField ID="hdGuid" runat="server" />
	<asp:HiddenField ID="hfldParentId" runat="server" />
	</form>
</body>
</html>
