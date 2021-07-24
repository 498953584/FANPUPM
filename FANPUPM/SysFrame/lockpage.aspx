<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lockpage.aspx.cs" Inherits="LockPage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>平台锁定</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript">
		function ReturnWindow()
		{
			window.top.topknot.rows = "65,*,20,0";
		}
		</script>
	</head>
	<body class="body_frame" bgcolor="#eaf3fd">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="475" align="center" background="images/index_login_bg.gif"
					border="0">
					<TR>
						<TD bgColor="#cccccc" height="1"></TD>
					</TR>
					<TR>
						<TD height="100">
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD width="5%">&nbsp;</TD>
									<TD width="95%"><IMG height="45" src="../images/index_login_title.gif" width="373"></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD align="center"><IMG height="1" src="images/index_login_line.gif" width="429"></TD>
					</TR>
					<TR>
						<TD height="76">
							<TABLE id="Table3" style="FONT-SIZE: 9pt; COLOR: #424242" cellSpacing="0" cellPadding="0"
								width="100%" border="0">
								<TR>
									<TD vAlign="middle" align="right" width="39%" height="30">用户名&nbsp;</TD>
									<TD width="24%"><FONT face="宋体"><input class="bginput" id="tb_yhdm" onmouseover="this.focus()" onfocus="this.select()" maxlength="16" size="11" value="admin" name="yhdm" runat="server" />
</FONT></TD>
									<TD width="37%" rowSpan="2">&nbsp;&nbsp;
										<asp:ImageButton ID="ImageButton1" ImageUrl="../images/index_login_login.gif" runat="server" /></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" height="30">密&nbsp;&nbsp;码&nbsp;</TD>
									<TD><FONT face="宋体"><input class="bginput" id="tb_pwd" onmouseover="this.focus()" onfocus="this.select()" type="password" maxlength="16" size="11" value="a" name="pwd" runat="server" />
</FONT></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD><IMG height="1" src="../images/index_login_line.gif" width="429"></TD>
					</TR>
				</TABLE>
			</FONT>
			<JWControl:JavaScriptControl ID="Js" runat="server"></JWControl:JavaScriptControl>
		</form>
	</body>
</HTML>
