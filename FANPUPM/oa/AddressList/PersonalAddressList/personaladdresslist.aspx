<%@ Page Language="C#" AutoEventWireup="true" CodeFile="personaladdresslist.aspx.cs" Inherits="PersonalAddressList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>个人通讯录</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<body scroll=no class=body_frame>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0"
					height="100%">
					<TR>
						<TD width="180"><iframe width="100%" name="group" id="group" frameborder="1" height="100%" runat="server"></iframe>
						</TD>
						<TD><iframe width="100%" name="linkman" id="linkman" frameborder="1" height="100%" runat="server"></iframe>
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
