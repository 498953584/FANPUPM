<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WinWebMailUpdate.aspx.cs" Inherits="oa_SysAdmin_UserSet_PasswordSet_WinWebMailUpdate" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>外部邮件用户名填写</title></head>
<body>
    <form id="form1" runat="server">
    <div align="center">
     <table id="Table1" cellspacing="1" cellpadding="5" width="450" border="1" class="table-normal">
				<tr class="td-title">
					<td colspan="2" align="center">
							<asp:Label ID="Label1" runat="server">外部邮件用户名填写</asp:Label></td>
				</tr>
				<tr>
					<td class="td-label">外部邮件用户名：</td>
					<td ><asp:TextBox ID="txtwbyjmc" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="td-label">外部邮件密码：</td>
					<td><asp:TextBox ID="txtpassword" TextMode="Password" runat="server"></asp:TextBox>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="必填" ControlToValidate="txtpassword" runat="server">*</asp:RequiredFieldValidator></td>
				</tr>			
				<tr>
					<td style="width:50%" align="center">
						<asp:Button ID="btMod" Text=" 确  认 " CssClass="button-normal" OnClick="btMod_Click" runat="server" /></td>
					<td align="center"><input class="button-normal" type="reset" value=" 重 填 "/></td>
				</tr>
				<tr>
				    <td colspan="2"><font color="red"><b>使用须知：</b></font><br />
                        &nbsp; &nbsp;
1、外部邮件使用从此处设定了正确的用户名和密码后，方能从系统直接登录，已设置好初始用户名为姓名的拼音全拼，密码为<STRONG>"hx"</STRONG>（自动申请的除外）；<br />
                        <br />
                        &nbsp; &nbsp; 
2、更改外部邮件密码需在第一次登录后从<STRONG>“选项”</STRONG>——“密码修改与帐号保护“中修改，在OA系统<STRONG>“个人设置”</STRONG>中重设“<STRONG>外部邮件”</STRONG>设置；<br />
                        <br />
                        &nbsp; &nbsp;
3、外部邮件系统支持pop3\smtp，具体设置请浏览<STRONG>“帮助中心”</STRONG>-<STRONG>“OA帮助"</STRONG>。</td>
				</tr>
	</table>
    </div>
     <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
