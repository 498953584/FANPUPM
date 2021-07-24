<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pwdsetting.aspx.cs" Inherits="PwdSetting" %>

<html>
	<head runat="server"><title>密码设置</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></head>
	<body>
		<form id="Form1" method="post" runat="server">
		
			<table id="Table1" cellspacing="1" cellpadding="5" width="500" align="center" border="1"
				class="table-normal">
				<tr class="td-title">
					<td colspan="2" align="center"><FONT face="宋体">
							<asp:Label ID="Label1" runat="server">Label</asp:Label></FONT></td>
				</tr>
				<tr>
					<td width="100" class="td-label">登录名：</td>
					<td><FONT face="宋体">
							<asp:TextBox ID="tbLoginName" ReadOnly="true" runat="server"></asp:TextBox></FONT></td>
				</tr>
				<tr>
					<td width="100"class="td-label">旧密码：</td>
					<td><FONT face="宋体">
							<asp:TextBox ID="tbOldPwd" TextMode="Password" runat="server"></asp:TextBox>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="必填" ControlToValidate="tbOldPwd" runat="server">*</asp:RequiredFieldValidator></FONT></td>
				</tr>
				<tr>
					<td width="100"class="td-label">新密码：</td>
					<td>
							<asp:TextBox ID="tbNewPwd1" TextMode="Password" MaxLength="16" runat="server"></asp:TextBox>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="必填" ControlToValidate="tbNewPwd1" runat="server">*</asp:RequiredFieldValidator><FONT face="宋体" style="font-size: 9pt; color: #ff0000; font-variant: normal;">16位以内的数字和字母</FONT></td>
				</tr>
				<tr>
					<td width="100"class="td-label">确认新密码：</td>
					<td><FONT face="宋体">
							<asp:TextBox ID="tbNewPwd2" TextMode="Password" MaxLength="16" runat="server"></asp:TextBox>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="必填" ControlToValidate="tbNewPwd2" runat="server">*</asp:RequiredFieldValidator></FONT><FONT face="宋体" style="font-size: 9pt; color: #ff0000; font-variant: normal;">16位以内的数字和字母</FONT></td>
				</tr>
			</table>
			
			<table id="Table2" width="350" align="center" style="WIDTH: 350px; HEIGHT: 48px">
				<tr>
					<td width="50%" align="center">
						<asp:Button ID="btMod" Text=" 修 改 " CssClass="button-normal" OnClick="btMod_Click" runat="server" /></TD>
					<td align="center"><INPUT class="button-normal" type="reset" value=" 重 填 "></td>
				</tr>
			</table>
		</form>
	</body>
</html>
