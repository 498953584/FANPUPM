<%@ Page Language="C#" AutoEventWireup="true" CodeFile="selecteddept.aspx.cs" Inherits="SelectedDept" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>SelectedDept</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE id="Table1" style="WIDTH: 100%; HEIGHT: 283px" cellSpacing="1" cellPadding="1" border="0">
					<TR>
						<TD style="HEIGHT: 244px" align="center">
							<asp:ListBox ID="LBoxSelectedMan" Width="100%" Rows="15" SelectionMode="Multiple" runat="server"></asp:ListBox></TD>
					</TR>
					<TR>
						<TD></TD>
					</TR>
					<TR>
						<TD align="center"><input type="button" value=" 确 定 " id="Button1" name="Button1" OnServerClick="Button1_ServerClick" runat="server" />
</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
