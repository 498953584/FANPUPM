<%@ Page Language="C#" AutoEventWireup="true" CodeFile="datumaffirm.aspx.cs" Inherits="DatumAffirm1" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>����ȷ��</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
			<TABLE class="table-form" id="Table1" cellSpacing="0" cellPadding="0" width="100%" align="center"
				border="0">
				<TR>
					<TD class="td-head" align="center" colSpan="2">�� �� ȷ ��</TD>
				</TR>
				<TR>
					<TD class="td-label" width="20%" height="10%">ȷ��ʱ�䣺</TD>
					<TD height="10%">
						<JWControl:DateBox ID="CalDate" CssClass="text-input" ReadOnly="true" runat="server"></JWControl:DateBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="ʱ�䲻�ܿ�!" ControlToValidate="CalDate" runat="server"></asp:RequiredFieldValidator></FONT></TD>
				</TR>
				<tr>
					<TD class="td-label" height="15%">ȷ�����ݣ�</TD>
					<TD vAlign="top" height="15%"><asp:TextBox ID="TxtRemark" Width="80%" CssClass="textarea-input" Height="60px" TextMode="MultiLine" runat="server"></asp:TextBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="ȷ�����ݲ���Ϊ��!" ControlToValidate="TxtRemark" runat="server"></asp:RequiredFieldValidator></TD>
				</tr>
				<TR>
					<TD class="td-submit" align="center" colSpan="2" height="10%"><asp:Button ID="BtnSave" CssClass="button-normal" Text="ȷ ��" OnClick="BtnSave_Click" runat="server" />&nbsp;
						<INPUT class="button-normal" onclick="javascipt:window.returnValue=false;window.close();"
							type="button" value="ȡ ��">&nbsp; <input id="hdnDacumClass" type="hidden" name="hdnDacumClass" runat="server" />

					</TD>
				</TR>
			</TABLE>
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
		</form>
	</body>
</HTML>
