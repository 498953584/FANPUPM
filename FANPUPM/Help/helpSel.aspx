<%@ Page Language="C#" AutoEventWireup="true" CodeFile="helpSel.aspx.cs" Inherits="WebSel" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>查看帮助</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<base target="_self"></base>
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />
<link href="../StyleCss/PM1.css" rel="stylesheet" type="text/css" />

	    <style type="text/css">
<!--
	html,body {margin:0px;font-size:9pt;font-family:"宋体";color:#000000;}
	td {font-family: "宋体";font-size: 9pt;line-height: 180%;color: #000000;text-decoration: none;}
	p {text-indent: 24px;color:#000000;line-height: 180%;}
	p.img {margin-bottom:2px;}
	.pt9 {font-family: "宋体";font-size: 9pt;line-height: 180%;color: #000000;text-decoration: none;}
	li {font-size: 9pt;color: #333333;text-decoration: none;list-style-type: disc;line-height: 220%;}

-->
        </style>
</head>
	<body MS_POSITIONING="FlowLayout" scroll="auto">
		<form id="Form1" method="post" runat="server">
			<table class="table-none" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<tr >
					<td    align=left style="color:#000099;font-weight:bold;padding-left:10px;height:27px;width:100%;font-size:14px;background-color: #E9F3FC;">
						<asp:Label ID="Lbbt" runat="server"></asp:Label></td>
				</tr>
				<tr><td height="15"></td></tr>
				<tr>
					<td height="90%" valign="top"  class="txtxw">

						<asp:Label ID="Lbxwnr" runat="server"></asp:Label>

					</td>
						
				</tr>
				<TR class="td-submit">
					<TD align="center">
						<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
