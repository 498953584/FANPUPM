<%@ Page Language="C#" AutoEventWireup="true" CodeFile="projectdocumentsearch.aspx.cs" Inherits="ProjectDocumentSearch" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_uprojectlist_ascx" Src="~/EPC/UserControl1/uprojectlist.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ProjectDocumentSearch</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />

		<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
		<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<link href="/StockManage/skins/sm4.css" rel="stylesheet" type="text/css" />
</head>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0"  onload="selrow3('UProjectList11_tvProjectt1')">
		<form id="Form1" method="post" runat="server">
			<table width="100%" height="100%">
				<tr>
					<td width="190" valign="top" style="BORDER-RIGHT:#eeeeee 1px solid; BORDER-TOP:#eeeeee 1px solid; BORDER-LEFT:#eeeeee 1px solid; BORDER-BOTTOM:#eeeeee 1px solid">
					<div style="width:100%; height:100%;margin:0px; overflow:auto;">
						<MyUserControl:epc_usercontrol1_uprojectlist_ascx ID="UProjectList11" runat="server" />
					</div>
					</td>
					<td>
						<table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0">
							<tr>
								<td><iframe id="fraList" name="fraList" src="../UserControl1/webTreeTS.aspx" width="100%" scrolling="no" height="100%"></iframe>
								</td>
							</tr>
							<tr>
								<td height="3"></td>
							</tr>
							<tr>
								<td height="160"><iframe id="fraDocument" name="fraDocument" src="DocumentView.aspx?dc=00000000-0000-0000-0000-000000000000&amp;allowadd=0"
										width="100%" scrolling=auto height="100%"></iframe>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
