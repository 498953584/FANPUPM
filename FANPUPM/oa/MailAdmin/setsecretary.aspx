<%@ Page Language="C#" AutoEventWireup="true" CodeFile="setsecretary.aspx.cs" Inherits="SetSecretary" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>SetSecretary</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE id="Table1" cellspacing="0" cellpadding="0" width="90%" align="center" border="0"
					height="100%">
					<TR>
						<TD height="18">&nbsp;</TD>
					</TR>
					<TR>
						<TD width="230" rowspan="2">
							<IFRAME width="100%" name="fraDept" id="fraDept" src="DeptTree.aspx" frameborder="0" style="BORDER-RIGHT:#cccccc 1px solid; BORDER-TOP:#cccccc 1px solid; BORDER-LEFT:#cccccc 1px solid; BORDER-BOTTOM:#cccccc 1px solid" height="100%" runat="server"></IFRAME>
						</TD>
						<TD height="60"><IFRAME width="100%" name="fraSecretary" id="fraSecretary" src="Secretary.aspx" frameborder="0" style="BORDER-RIGHT:#cccccc 1px solid; BORDER-TOP:#cccccc 1px solid; BORDER-LEFT:#cccccc 1px solid; BORDER-BOTTOM:#cccccc 1px solid" height="100%" runat="server"></IFRAME>
						</TD>
					</TR>
					<TR>
						<TD><IFRAME width="100%" name="fraHuman" id="fraHuman" frameborder="0" height="100%" style="BORDER-RIGHT:#cccccc 1px solid; BORDER-TOP:#cccccc 1px solid; BORDER-LEFT:#cccccc 1px solid; BORDER-BOTTOM:#cccccc 1px solid" runat="server"></IFRAME>
						</TD>
					</TR>
					<TR>
						<TD height="18">&nbsp;</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</BODY>
</HTML>
