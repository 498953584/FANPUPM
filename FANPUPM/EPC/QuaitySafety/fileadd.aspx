<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fileadd.aspx.cs" Inherits="FileAdd" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>FileAdd</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
	</head>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="td-head" colSpan="2" height="24">附件添加</td>
				<tr>
					<td class="td-label" width="18%">文件：</td>
					<td><input id="FileKnowledge" style="WIDTH: 320px; HEIGHT: 21px" type="file" size="35" name="File1" runat="server" />
</td>
				</tr>
				<tr>
					<td class="td-label" vAlign="top">备注：</td>
					<td><asp:TextBox ID="txtRemark" TextMode="MultiLine" Rows="2" Columns="50" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="td-submit" vAlign="middle" colSpan="2" height="20"><asp:Button ID="btnAdd" CssClass="button-normal" Text="确  定" OnClick="btnAdd_Click" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<input class="button-normal" id="btnClose" onclick="window.close();" type="button" value="关  闭">
						<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
