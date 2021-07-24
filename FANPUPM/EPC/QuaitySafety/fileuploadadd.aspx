<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fileuploadadd.aspx.cs" Inherits="FileUploadAdd" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>文件上传</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
	</head>
	<body class="body_frame">
		<form class="table-none" id="Form1" method="post" runat="server">
			<TABLE id="Table1" height="100%" cellSpacing="1" cellPadding="1" width="100%" border="1">
				<TR>
					<TD class="td-head" align="center" colSpan="4" height="24">
						<asp:Label ID="lblTitle" runat="server">Label</asp:Label>文件上传</TD>
				</TR>
				<TR>
					<TD class="td-label" width="100">文件名称:</TD>
					<TD><asp:TextBox ID="txtName" Width="100%" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="td-label" width="100">文件备注:</TD>
					<TD><asp:TextBox ID="txtbz" Width="100%" runat="server"></asp:TextBox></TD>
				</TR>
				<tr>
					<td class="td-label" width="100">上传路径:</td>
					<td><input id="FileUp" type="file" name="File" runat="server" />
</td>
				</tr>
				<tr>
					<td colspan="2" class="td-submit" style="HEIGHT: 100%">
						<asp:Button ID="BtnOK" Text="保  存" CssClass="button-normal" OnClick="BtnOK_Click" runat="server" />&nbsp;&nbsp;
						<input type="submit" name="BtnCancel" value="取 消" class="button-normal" onclick="window.close();return false;">
					</td>
				</tr>
			</TABLE>
		</form>
		<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
	</body>
</HTML>
