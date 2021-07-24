<%@ Page Language="C#" AutoEventWireup="true" CodeFile="codeclassedit.aspx.cs" Inherits="CodeClassEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>编码类型编辑</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
	</head>
	<body scroll="no" class="body_popup">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellpadding="0" cellspacing="0" border="0" width="100%">
				<TR>
					<TD height="22" class="td-title">编码类别维护</TD>
				</TR>
				<TR>
					<TD class="mp_frame_top" style="PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; PADDING-TOP: 3px"
						valign="top">
						<table class="table-normal" cellspacing="0" cellpadding="0" width="100%" border="1">
							<tr>
								<TD align="right" width="15%" class="td-label">类型名称：</TD>
								<TD colspan="3">
									<asp:TextBox ID="TxtClassName" Width="70%" Rows="1" CssClass="textarea-input" MaxLength="100" runat="server"></asp:TextBox>
									<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TxtClassName" ErrorMessage="类型名称不能为空!" runat="server"></asp:RequiredFieldValidator></TD>
							</tr>
							<TR>
								<TD align="right" width="15%" class="td-label">特征码：</TD>
								<TD colspan="3">
									<asp:TextBox ID="TxtSignCode" Width="70%" CssClass="textarea-input" MaxLength="80" runat="server"></asp:TextBox>
									<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="特征码不能为空!" ControlToValidate="TxtSignCode" runat="server"></asp:RequiredFieldValidator>
								</TD>
							</TR>
							<tr>
								<TD align="right" width="15%" class="td-label">备注：</TD>
								<TD colspan="3">
									<asp:TextBox ID="TxtRemark" Width="70%" CssClass="textarea-input" MaxLength="18" runat="server"></asp:TextBox></TD>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD colSpan="4" class="td-submit">
						<asp:Button ID="BtnSave" Text="保  存" OnClick="BtnSave_Click" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
						<INPUT onclick="window.close();" type="button" value="关  闭">
					</TD>
				</TR>
			</TABLE>
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
		</form>
	</body>
</HTML>
