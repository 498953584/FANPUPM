<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editlinkman.aspx.cs" Inherits="EditLinkman" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>EditLinkman</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

        <script type="text/javascript" src="../../../Script/jquery.js"></script>
        <script type="text/javascript" src="../../../Script/jw.js"></script>
		<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></head>
	<body>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<table class="table-normal" id="Table1" cellspacing="1" cellpadding="1" width="99%" align="center" border="1" runat="server"><tr class="grid_head" runat="server"><td align="center" colspan="2" runat="server">
							<asp:Label ID="lblTitle" runat="server">Label</asp:Label></td></tr><tr height="25" runat="server"><td style="WIDTH: 127px" runat="server">姓名：</td><td runat="server">
							<asp:TextBox ID="tbName" MaxLength="10" runat="server"></asp:TextBox>
							<asp:RequiredFieldValidator ID="rfvName" ErrorMessage="RequiredFieldValidator" ControlToValidate="tbName" runat="server">必填</asp:RequiredFieldValidator></td></tr><tr height="25" runat="server"><td style="WIDTH: 127px" runat="server">性别：</td><td runat="server">
							<asp:RadioButtonList ID="rblSex" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server"><asp:ListItem Value="m" Selected="true" Text="男" /><asp:ListItem Value="f" Text="女" /></asp:RadioButtonList></td></tr><tr height="25" runat="server"><td style="WIDTH: 127px" runat="server">办公电话（内线）：</td><td runat="server">
							<asp:TextBox ID="tbDuty" runat="server"></asp:TextBox></td></tr><tr height="25" runat="server"><td style="WIDTH: 127px" runat="server">住址：</td><td runat="server">
							<asp:TextBox ID="tbAddress" MaxLength="25" runat="server"></asp:TextBox></td></tr><tr height="25" runat="server"><td style="WIDTH: 127px" runat="server">邮编：</td><td runat="server">
							<asp:TextBox ID="tbPostalcode" runat="server"></asp:TextBox>
							<asp:RegularExpressionValidator ID="revCode" ErrorMessage="RegularExpressionValidator" ControlToValidate="tbPostalcode" ValidationExpression="\d{6}" runat="server">6位数字</asp:RegularExpressionValidator></td></tr><tr height="25" runat="server"><td style="WIDTH: 127px" runat="server">办公电话（外线）：</td><td runat="server">
							<asp:TextBox ID="tbCorpPhone" runat="server"></asp:TextBox></td></tr><tr height="25" runat="server"><td style="WIDTH: 127px" runat="server">传真：</td><td runat="server">
							<asp:TextBox ID="tbFax" runat="server"></asp:TextBox></td></tr><tr height="25" runat="server"><td style="WIDTH: 127px" runat="server">住宅电话：</td><td runat="server">
							<asp:TextBox ID="tbHomePhone" runat="server"></asp:TextBox></td></tr><tr height="25" runat="server"><td style="WIDTH: 127px" runat="server">手机：</td><td runat="server">
							<asp:TextBox ID="tbHandset" runat="server"></asp:TextBox>
							<asp:RegularExpressionValidator ID="revHandset" ErrorMessage="RegularExpressionValidator" ControlToValidate="tbHandset" ValidationExpression="^13[0-9]{1}[0-9]{8}$|^15[0-9]{1}[0-9]{8}$" runat="server">输入有误</asp:RegularExpressionValidator></td></tr><tr height="25" runat="server"><td style="WIDTH: 127px" runat="server">E-mail:</td><td runat="server">
							<asp:TextBox ID="tbEmail" runat="server"></asp:TextBox>
							<asp:RegularExpressionValidator ID="revEmail" ErrorMessage="RegularExpressionValidator" ControlToValidate="tbEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server">不合法</asp:RegularExpressionValidator></td></tr><tr height="25" runat="server"><td style="WIDTH: 127px" runat="server">OICQ:</td><td runat="server">
							<asp:TextBox ID="tbQQ" runat="server"></asp:TextBox></td></tr><tr height="70" runat="server"><td style="WIDTH: 127px" runat="server">备注：</td><td runat="server">
							<asp:TextBox ID="tbContent" Width="272px" TextMode="MultiLine" Height="76px" runat="server"></asp:TextBox></td></tr><tr height="25" runat="server"><td align="center" colspan="2" class="td-submit" runat="server">
							<asp:Button ID="btnConfirm" Text=" 确 定 " CssClass="button-normal" OnClick="btnConfirm_Click" runat="server" />&nbsp;
                            <input class="button-normal" id="btnClose" onclick="javascript:top.ui.closeWin();" type="button" value=" 关 闭 " name="btnClose" runat="server" />
&nbsp;</td></tr></table>
			</FONT>
		</form>
	</body>
</HTML>
