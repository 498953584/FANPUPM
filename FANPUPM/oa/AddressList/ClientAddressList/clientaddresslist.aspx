<%@ Page Language="C#" AutoEventWireup="true" CodeFile="clientaddresslist.aspx.cs" Inherits="ClientAddressList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" >
<html>
	<head runat="server"><title>ClientAddressList</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<body scroll="no" class="body_frame" style="overflow:hidden" >
		<form id="Form1" method="post" runat="server">
				<table id="Table1" cellspacing="0" cellpadding="0" align="center" border="0" style="height:100%; width:100%">
					<tr>
						<td height="100%"width="180" >
                        <iframe name="CorpList" id="CorpList" frameborder="1" style="height:100%; width:100%" runat="server"></iframe>
						</td>
						<td height="100%">
                        <iframe name="LinkmanList" id="LinkmanList" frameborder="1" style="height:100%; width:100%" runat="server"></iframe>
						</td>
					</tr>
				</table>
		</form>
	</body>
</html>
