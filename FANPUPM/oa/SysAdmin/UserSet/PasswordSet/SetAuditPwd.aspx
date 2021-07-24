<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetAuditPwd.aspx.cs" Inherits="oa_SysAdmin_UserSet_PasswordSet_SetAuditPwd" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>无标题页</title></head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <P><FONT face="宋体"></FONT>&nbsp;</P>
			<P><FONT face="宋体"></FONT>&nbsp;</P>
			<P><FONT face="宋体"></FONT>&nbsp;</P>
			<FONT face="宋体"></FONT>
    <table id="Table1" cellSpacing="1" cellPadding="5" width="450" border="1" class="table-normal">
				<tr class="td-title">
					<TD colspan="2" align="center">
							<asp:Label ID="Label1" runat="server">审核密码设置</asp:Label></TD>
				</TR>
				<TR>
					<TD class="td-label">姓 名：</TD>
					<TD ><asp:TextBox ID="tbLoginName" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="td-label">新密码：</TD>
					<TD><asp:TextBox ID="tbNewPwd1" TextMode="Password" runat="server"></asp:TextBox>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="必填" ControlToValidate="tbNewPwd1" runat="server">*</asp:RequiredFieldValidator></TD>
				</TR>
				<TR>
					<TD class="td-label">确认新密码：</TD>
					<TD>
							<asp:TextBox ID="tbNewPwd2" TextMode="Password" runat="server"></asp:TextBox>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="必填" ControlToValidate="tbNewPwd2" runat="server">*</asp:RequiredFieldValidator></TD>
				</TR>
				<tr style="display :none">
				    <td class="td-label">上传个人签名：</td>
				    <td>
				    <asp:FileUpload ID="FUFilePath" Width="80%" runat="server" /></td>
				 </tr>
				 <tr style="display :none">
				    <td class="td-label"></td>
				    <td>
				     <asp:Image ID="ImgName" runat="server" /></td>
				 </tr>
				<TR>
					<TD style="width:50%" align="center">
						<asp:Button ID="btMod" Text=" 修 改 " CssClass="button-normal" OnClick="btMod_Click" runat="server" /></TD>
					<TD align="center"><INPUT class="button-normal" type="reset" value=" 重 填 "></TD>
				</TR>
	</table>
    </div>
        <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
