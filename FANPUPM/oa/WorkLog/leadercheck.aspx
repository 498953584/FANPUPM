<%@ Page Language="C#" AutoEventWireup="true" CodeFile="leadercheck.aspx.cs" Inherits="leaderCheck" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>leaderCheck</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0"
				height="100%">
				<TR>
					<TD width="230">
						<iframe id="lFrame" name="lFrame" width="100%" height="100%" frameborder="0" runat="server">
						</iframe>
					</TD>
					<TD>
						<iframe id="rFrame" name="rFrame" width="100%" height="100%" frameborder="0" runat="server">
						</iframe>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
