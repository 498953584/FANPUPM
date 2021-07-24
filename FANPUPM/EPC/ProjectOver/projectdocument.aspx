<%@ Page Language="C#" AutoEventWireup="true" CodeFile="projectdocument.aspx.cs" Inherits="ProjectDocument" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_uprojectlist_ascx" Src="~/EPC/UserControl1/uprojectlist.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ProjectDocument</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />

		<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
		<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<link href="/StockManage/skins/sm4.css" rel="stylesheet" type="text/css" />
</head>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" onload="selrow3('UProjectList11_tvProjectt1')">
		<form id="Form1" method="post" runat="server">
			<table width="100%" height="100%">
				<tr>
					<td width="190" valign="top" style="border:solid 1px #eeeeee;">
					<div style="width:100%; height:100%;margin:0px; overflow:auto;">
						<MyUserControl:epc_usercontrol1_uprojectlist_ascx ID="UProjectList11" runat="server" />
					</div>
					</td>
					<td>
						<table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0">
							<tr>
								<td><iframe id="fraList" name="fraList" src="../UserControl1/webTreeTS.aspx" width="100%" height="100%"></iframe>
								</td>
							</tr>
							<tr>
								<td height="160px"><iframe id="fraDocument" name="fraDocument" src="../UserControl1/webNullTS.aspx" width="100%"
										scrolling="no" height="100%"></iframe>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
