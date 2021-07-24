<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editlinkman.aspx.cs" Inherits="EditLinkman" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>EditLinkman</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
        <script type="text/javascript" src="../../../Script/jquery.js"></script>
        <script type="text/javascript" src="../../../Script/jw.js"></script>
	</head>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE class="table-normal" id="Table1" cellSpacing="1" cellPadding="1" width="99%" align="center"
					border="1">
					<TR>
						<TD class="grid_head" align="center" colSpan="2"><asp:Label ID="Label1" runat="server">Label</asp:Label></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px">姓名：</TD>
						<TD><asp:TextBox ID="tbName" Width="120px" runat="server"></asp:TextBox>
							<asp:RequiredFieldValidator ID="rfvName" ErrorMessage="RequiredFieldValidator" ControlToValidate="tbName" runat="server">必填</asp:RequiredFieldValidator></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px">性别：</TD>
						<TD><asp:RadioButtonList ID="rbtSex" RepeatLayout="Flow" CellSpacing="0" CellPadding="0" BorderStyle="None" BorderWidth="0px" RepeatDirection="Horizontal" runat="server"><asp:ListItem Value="m" Selected="true" Text="男" /><asp:ListItem Value="f" Text="女" /></asp:RadioButtonList></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px">分组：</TD>
						<TD><asp:DropDownList ID="ddlGroup" Width="120px" runat="server"></asp:DropDownList></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px">单位：</TD>
						<TD><asp:TextBox ID="tbUnit" Width="256px" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px">手机：</TD>
						<TD><asp:TextBox ID="tbHandset" Width="120px" MaxLength="11" runat="server"></asp:TextBox>
							<asp:RegularExpressionValidator ID="revHandset" ErrorMessage="RegularExpressionValidator" ControlToValidate="tbHandset" ValidationExpression="^13[0-9]{1}[0-9]{8}$|^15[0-9]{1}[0-9]{8}$" runat="server">输入有误</asp:RegularExpressionValidator></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px">住宅电话：</TD>
						<TD><asp:TextBox ID="tbHomePhone" Width="120px" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px">公司电话：</TD>
						<TD><asp:TextBox ID="tbCorpPhone" Width="120px" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px">
							<P>生日：</P>
						</TD>
						<TD><asp:TextBox ID="tbBirthday" Width="120px" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px">地址：</TD>
						<TD><asp:TextBox ID="tbAddress" Width="256px" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px">
							<P>邮政编码：</P>
						</TD>
						<TD><asp:TextBox ID="tbPostalcode" Width="120px" MaxLength="6" runat="server"></asp:TextBox>
							<asp:RegularExpressionValidator ID="revCode" ErrorMessage="RegularExpressionValidator" ControlToValidate="tbPostalcode" ValidationExpression="\d{6}" runat="server">6位数字</asp:RegularExpressionValidator></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px">OICQ:</TD>
						<TD><asp:TextBox ID="tbQQ" Width="120px" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px">
							<P>E-Mail:</P>
						</TD>
						<TD><asp:TextBox ID="tbEmail" Width="256px" runat="server"></asp:TextBox>
							<asp:RegularExpressionValidator ID="revEmail" ErrorMessage="RegularExpressionValidator" ControlToValidate="tbEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server">不合法</asp:RegularExpressionValidator></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 90px">备注：</TD>
						<TD><asp:TextBox ID="tbContent" Width="264px" Height="72px" TextMode="MultiLine" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD align="center" colSpan="2" class=td-submit><asp:Button ID="tbnAdd" CssClass="button-normal" Text=" 确 定 " OnClick="tbnAdd_Click" runat="server" />&nbsp;<input type="button" value=" 取 消 " class="button-normal" onclick="window.close();" id="butcancel" runat="server" />
&nbsp;</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</BODY>
</HTML>
