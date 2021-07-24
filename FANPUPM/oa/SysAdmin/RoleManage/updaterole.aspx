<%@ Page Language="C#" AutoEventWireup="true" CodeFile="updaterole.aspx.cs" Inherits="UpdateRole" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>UpdateRole</title></head>
<body scroll="no" class="body_popup">
	<form id="Form1" method="post" runat="server">
	<table cellspacing="0" cellpadding="0" width="100%" border="1">
		<tr>
			<td class="td-head" align="center" colspan="2">
				填写基本资料
			</td>
		</tr>
		<tr>
			<td class="td-label" width="25%">
				角色名称：
			</td>
			<td>
				<asp:TextBox ID="tbJsmc" Width="85%" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td colspan="2" class="td-submit">
				<asp:Button ID="BtnAdd" CssClass="button-normal" Text=" 修 改 " OnClick="BtnAdd_Click" runat="server" />&nbsp;<input class="button-normal" id="BtnReset" type="reset" value=" 重 填 "
					name="BtnReset">&nbsp; <font face="宋体"></font>
				<input class="button-normal" id="BtnClose" onclick="top.ui.closeWin();" type="button"
					value=" 关 闭 " name="BtnClose" />
				<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="角色名称不能为空！" Display="None" ControlToValidate="tbJsmc" runat="server"></asp:RequiredFieldValidator>
				<asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
			</td>
		</tr>
	</table>
	</form>
</body>
</html>
