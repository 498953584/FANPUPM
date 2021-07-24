<%@ Page Language="C#" AutoEventWireup="true" CodeFile="basiccode.aspx.cs" Inherits="BasicCode" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>BasicCode</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table width="99%" height="100%" cellpadding="0" cellspacing="0">
				<tr>
					<td colspan="3" class="td-title">系统编码维护</td>
				</tr>
				<tr>
					<td width="220" valign="top">
						<iframe src="CodeTypeList.aspx" width="100%" height="100%" frameborder="0"></iframe>
					</td>
					<td width="4"></td>
					<td><iframe id="FraCodeList" name="FraCodeList" width="100%" height="100%" src="../UserControl1/webTreeTS.aspx"></iframe>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
