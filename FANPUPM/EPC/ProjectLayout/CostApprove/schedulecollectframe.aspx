<%@ Page Language="C#" AutoEventWireup="true" CodeFile="schedulecollectframe.aspx.cs" Inherits="ScheduleCollectFrame" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_uprojectlist_ascx" Src="~/EPC/UserControl1/uprojectlist.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>工程清单</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<body scroll="no" style="margin:0px;" onload="selrow3('UProjectList1_tvProjectt1')">
		<form id="Form1" method="post" runat="server">
			<table id="Table3" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
				<tr>
					<td vAlign="top" width="12%" height="100%">
						<div style="border:solid 0px red; overflow:auto;width:200px;height:100%;"><MyUserControl:epc_usercontrol1_uprojectlist_ascx ID="UProjectList1" runat="server" /></div>
					</td>
					<td vAlign="top" width="88%"><iframe id="FraSch" name="FraSch" frameborder="0" width="100%" scrolling="no" height="100%" src="../../UserControl1/webTreeTS.aspx" runat="server"></iframe>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
