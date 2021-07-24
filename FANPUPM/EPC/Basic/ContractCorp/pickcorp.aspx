<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pickcorp.aspx.cs" Inherits="PickCorpSimple" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head id="Head1" runat="server"><title>选择单位...</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
	</head>
	<body class="body_popup" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table class="table-normal" width="100%" height="100%" cellpadding="0" cellspacing="0" border="1">
				<tr>
					<td colspan="3" class="td-head">往来单位</td>
				</tr>
				<tr>
					<td width="150" valign="top">
						<iframe id="FraCorpType" name="FraCorpType" src="CorpTypeTree.aspx?ts=&w=1" width="100%" height="100%" frameborder="0" runat="server"></iframe>
					</td>
					<td width="4"></td>
					<td><iframe id="FraCorpList" name="FraCorpList" width="100%" height="100%" src="about:blank" runat="server"></iframe>
					</td>
				</tr>
		</form>
	</body>
</HTML>
