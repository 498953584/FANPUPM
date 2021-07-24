<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebSel.aspx.cs" Inherits="WebSel" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>查看新闻内容</title><link href="../StyleCss/PM1.css" rel="stylesheet" type="text/css" />

	<style type="text/css">
		ul, li
		{
			list-style: none;
		}
		div
		{
			overflow: auto;
		}
	</style>
</head>
<body ms_positioning="FlowLayout" scroll="auto">
	<form id="Form1" method="post" runat="server">
	<table class="table-none" id="Table1" height="100%" cellspacing="0" cellpadding="0"
		width="100%" border="0">
		<tr height="26px">
			<td align="center" class="txtxw">
				<asp:Label ID="Lbbt" Font-Bold="true" ForeColor="#000040" Font-Size="Small" runat="server">Label</asp:Label>
			</td>
		</tr>
		<tr>
			<td height="80%" valign="top" class="txtxw">
				<asp:Label ID="Lbxwnr" runat="server"></asp:Label>
				<div>
					<ul>
						<asp:Literal ID="Literal1" runat="server"></asp:Literal>
					</ul>
				</div>
			</td>
		</tr>
		<tr class="td-submit">
			<td align="center">
				<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hdnNewUrl" runat="server" />
	</form>
</body>
</html>
