<%@ Page Language="C#" AutoEventWireup="true" CodeFile="datasetting.aspx.cs" Inherits="DataSetting" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>DataSetting</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></head>
	<body>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<br>
				<br>
				<table class="table-normal" id="Table1" cellspacing="1" cellpadding="1" width="450" align="center" border="1" runat="server"><tr class="grid_head" runat="server"><td align="center" colspan="2" runat="server">
							<asp:Label ID="lblTitle" runat="server">Label</asp:Label></td></tr><tr height="25" runat="server"><td style="WIDTH: 127px" runat="server">性别：</td><td runat="server">
							<asp:RadioButtonList ID="rblSex" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server"><asp:ListItem Value="m" Selected="true" Text="男" /><asp:ListItem Value="f" Text="女" /></asp:RadioButtonList></td></tr><tr height="25" runat="server"><td style="WIDTH: 127px" runat="server">职务：</td><td runat="server">
							<asp:TextBox ID="tbDuty" runat="server"></asp:TextBox></td></tr><tr height="25" runat="server"><td style="WIDTH: 127px" runat="server">住址：</td><td runat="server">
							<asp:TextBox ID="tbAddress" runat="server"></asp:TextBox></td></tr><tr height="25" runat="server"><td style="WIDTH: 127px" runat="server">邮编：</td><td runat="server">
							<asp:TextBox ID="tbPostalcode" runat="server"></asp:TextBox>
							<asp:RegularExpressionValidator ID="revCode" ErrorMessage="RegularExpressionValidator" ControlToValidate="tbPostalcode" ValidationExpression="\d{6}" runat="server">6位数字</asp:RegularExpressionValidator></td></tr><tr height="25" runat="server"><td style="WIDTH: 127px" runat="server">办公电话：</td><td runat="server">
							<asp:TextBox ID="tbCorpPhone" runat="server"></asp:TextBox></td></tr><tr height="25" runat="server"><td style="WIDTH: 127px" runat="server">传真：</td><td runat="server">
							<asp:TextBox ID="tbFax" runat="server"></asp:TextBox></td></tr><tr height="25" runat="server"><td style="WIDTH: 127px" runat="server">住宅电话：</td><td runat="server">
							<asp:TextBox ID="tbHomePhone" runat="server"></asp:TextBox>
							<asp:CheckBox ID="cbZdbs" Text="保密" runat="server" /></td></tr><tr height="25" runat="server"><td style="WIDTH: 127px" runat="server">手机：</td><td runat="server">
							<asp:TextBox ID="tbHandset" runat="server"></asp:TextBox>
							<asp:CheckBox ID="cbSjbs" Text="保密" runat="server" />
							<asp:RegularExpressionValidator ID="revHandset" ErrorMessage="RegularExpressionValidator" ControlToValidate="tbHandset" ValidationExpression="13\d{9}" Display="Dynamic" runat="server">以13开头的11位数字
							</asp:RegularExpressionValidator></td></tr><tr height="25" runat="server"><td style="WIDTH: 127px" runat="server">E-mail:</td><td runat="server">
							<asp:TextBox ID="tbEmail" runat="server"></asp:TextBox>
							<asp:RegularExpressionValidator ID="revEmail" ErrorMessage="RegularExpressionValidator" ControlToValidate="tbEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server">不合法</asp:RegularExpressionValidator></td></tr><tr height="25" runat="server"><td style="WIDTH: 127px" runat="server">OICQ:</td><td runat="server">
							<asp:TextBox ID="tbQQ" runat="server"></asp:TextBox></td></tr><tr height="70" runat="server"><td style="WIDTH: 127px" runat="server">备注：</td><td runat="server">
							<asp:TextBox ID="tbContent" Width="272px" TextMode="MultiLine" Height="76px" runat="server"></asp:TextBox></td></tr><tr height="25" runat="server"><td align="center" colspan="2" runat="server">
							<asp:Button ID="btnConfirm" Text=" 确定 " CssClass="button-normal" OnClick="btnConfirm_Click" runat="server" />&nbsp;<INPUT class="button-normal" type="reset" value=" 取消 "></td></tr></table>
			</FONT>
		</form>
	</body>
</HTML>
