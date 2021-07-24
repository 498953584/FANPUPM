<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProfitView.aspx.cs" Inherits="ProfitView" CodePage="936" EnableViewState="false" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ProfitView</title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Expires" content="0">
		<meta http-equiv="Cache-Control" content="no-cache">
		<meta http-equiv="Pragma" content="no-cache">
	</HEAD>
	<body class="body_clear" scroll="no">
		<table style="TABLE-LAYOUT: fixed; BORDER-COLLAPSE: collapse" height="100%" cellSpacing="0"
			cellPadding="0" width="100%" border="1">
			<tr>
				<td class="td-head" align="center" height="30"><%=this.ProjectName %></td>
			</tr>
			<tr>
				<td vAlign="middle" align="center">
					<table height="100%" width="100%" align="center" bgColor="#f0f0f7">
						<tr>
							<td align="center">
								<div id="CXChart1_0_NoSVGViewer" style="DISPLAY: none; FONT-SIZE: 10pt; WIDTH: 480px"
									align="center"></div>
								<embed id="SvgFile" src="/aspnet_client/CX/Chart1_0/GRAPH.JS" width="540" height="264" 
									type="image/svg+xml" pluginspace="http://www.adobe.com/svg/viewer/install/"></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td align="center" height="200">
					<form id="Form1" method="post" runat="server">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 200px"><asp:Table ID="tblData" GridLines="Both" CellPadding="0" BorderWidth="1px" CellSpacing="1" Width="99%" EnableViewState="false" runat="server"></asp:Table></div>
						<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></form>
				</td>
			</tr>
		</table>
	</body>
</HTML>
