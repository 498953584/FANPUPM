<%@ Page Language="C#" AutoEventWireup="true" CodeFile="returnlogin.aspx.cs" Inherits="ReturnLogin" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ReturnLogin</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<body scroll="no" bgcolor="#dbf2e1">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体"></FONT>
			<table border="0" width="80%" align="center">
				<tr>
					<td>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="85%" align="center" border="0">
							<tr>
								<td class="td-head">重复登陆信息</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
							</tr>
							<TR>
								<TD colspan="2" align="center"><i>
									<asp:Label ID="l_name" ForeColor="#008AFF" Font-Size="14px" runat="server"></asp:Label></i>您的帐号已经在其它地方登陆到本系统，请您先点击<i><font color="#008aff">“强制退出”</font></i>按钮，撤消此用户，然后再登陆到本系统。</TD>
							</TR>
							<tr>
								<td>&nbsp;</td>
							</tr>
						</TABLE>
						<table cellpadding="0" cellspacing="0" width="50%" align="center" border="1" class="table-form">
							<TR class="td-label">
								<TD align="right" width="50%">用户登陆ID：</TD>
								<TD>
									<asp:Label ID="l_name2" runat="server"></asp:Label></TD>
							</TR>
							<TR class="td-label">
								<TD align="right" width="50%"><FONT face="宋体">登陆时间：</FONT></TD>
								<TD>
									<asp:Label ID="l_loginTime" runat="server"></asp:Label></TD>
							</TR>
							<TR class="td-label">
								<TD align="right" width="50%">用户IP地址：</TD>
								<TD>
									<asp:Label ID="l_ip" runat="server"></asp:Label></TD>
							</TR>
						</table>
						<br>
						<br>
						<table border="0" width="50%" align="center">
							<TR>
								<TD align="center" colspan="2">
									<asp:Button ID="Button1" Text="强制退出" CssClass="button-normal" Width="78px" OnClick="Button1_Click" runat="server" /></TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
