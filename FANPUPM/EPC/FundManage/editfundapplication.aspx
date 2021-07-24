<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editfundapplication.aspx.cs" Inherits="EditFundApplication" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>借款申请</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
	</head>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table class="table-form" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="1">
				<TR align="center">
					<TD class="td-head" colSpan="2" height="22">借款单据</TD>
				</TR>
				<TR>
					<TD class="td-label" width="25%">借款日期</TD>
					<TD><asp:TextBox ID="txtDate" Enabled="false" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="td-label">本期请款</TD>
					<TD><asp:TextBox ID="txtMoney" runat="server"></asp:TextBox>元
						<asp:RequiredFieldValidator ID="RFVMoney" Display="None" ControlToValidate="txtMoney" ErrorMessage="请输入借款金额！" runat="server"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="REVMoney" Display="None" ControlToValidate="txtMoney" ErrorMessage="借款金额必须为数字！" ValidationExpression="^\-?\d+(\.\d+)?$" runat="server"></asp:RegularExpressionValidator></TD>
				</TR>
				<TR>
					<TD class="td-label">借款事由</TD>
					<TD><asp:TextBox ID="txtContent" Height="90px" Width="100%" TextMode="MultiLine" runat="server"></asp:TextBox></TD>
				</TR>
				<TR class="td-submit">
					<TD colSpan="2" height="22"><asp:Button ID="btnConfirm" Text="确 定" OnClick="btnConfirm_Click" runat="server" />&nbsp;
						<input id="btnClose" onclick="javascript:window.close();window.returnValue=false;" type="button"
							value="关 闭">
						<asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" ShowMessageBox="true" runat="server"></asp:ValidationSummary></TD>
				</TR>
			</table>
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl></form>
	</body>
</HTML>
