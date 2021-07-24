<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RTXSetting.aspx.cs" Inherits="oa_SysAdmin_UserSet_PasswordSet_RTXSetting" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>RTX设置</title></head>
<body>
	<form id="form1" runat="server">
	<div>
		<table class="gvdata" cellspacing="0px" border="1px" style="width: 400px; margin: auto;
			border-collapse: collapse;">
			<tr class="rowa">
				<td style="width: 120px; text-align: right;">
					RTX帐号
				</td>
				<td style="text-align: left;">
					<asp:TextBox ID="txtRTXID" Width="200px" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td colspan="2" style="text-align: right;">
					<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
				</td>
			</tr>
	</div>
	</form>
</body>
</html>
