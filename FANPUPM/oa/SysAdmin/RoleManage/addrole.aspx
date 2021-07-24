<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addrole.aspx.cs" Inherits="AddRole" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>添加角色</title></head>
<body scroll="no" class="body_popup">
	<form id="Form1" method="post" runat="server">
	<table cellspacing="0" cellpadding="0" width="100%" border="1">
		<tr>
			<td class="td-head" colspan="2">
				填写基本资料
			</td>
		</tr>
		<tr>
			<td class="td-label" width="25%">
				角色名称：
			</td>
			<td>
				<asp:TextBox ID="tbJsmc" MaxLength="50" Width="85%" CssClass="text-input" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td colspan="2" class="td-submit">
				<asp:Button ID="BtnAdd" Text="新  增" OnClick="BtnAdd_Click" runat="server" />&nbsp;
				<input id="BtnReset" type="reset" value=" 重 填 " name="BtnReset">&nbsp;
				<input id="BtnClose" onclick="top.ui.closeWin();" type="button" value=" 取 消 " name="BtnClose">&nbsp;
			</td>
		</tr>
	</table>
	<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="角色名称不能为空！" ControlToValidate="tbJsmc" Display="None" runat="server"></asp:RequiredFieldValidator>
	<asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
	</form>
</body>
</html>
