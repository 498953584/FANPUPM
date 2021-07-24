<%@ Page Language="C#" AutoEventWireup="true" CodeFile="proceedentaudit.aspx.cs" Inherits="ProceedEntAudit" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>收益额公司审核</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
	</head>
	<body class="body_popup">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE id="Table1" class="table-form" cellSpacing="0" cellPadding="0" width="100%" height="100%">
					<tr>
						<td colspan="2" class="td-head">审核进步收益额</td>
					</tr>
					<TR>
						<TD width="20%" align="center" height="10" class="td-label">审核人</TD>
						<TD height="10">
							<asp:TextBox ID="txtEntAuditPeople" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD align="center" height="10" class="td-label">审核日期</TD>
						<TD height="10">
							<JWControl:DateBox ID="txtEntAuditDate" ReadOnly="true" runat="server"></JWControl:DateBox></TD>
					</TR>
					<TR>
						<TD align="center" height="21" class="td-label" style="HEIGHT: 21px">是否通过</TD>
						<TD height="21" style="HEIGHT: 21px">
							<asp:DropDownList ID="ddlEntAuditResult" runat="server"></asp:DropDownList></TD>
					</TR>
					<tr>
						<td class="td-label">审核金额</td>
						<td>
							<asp:TextBox ID="txtAuditValue" runat="server"></asp:TextBox>
							<asp:RangeValidator ID="RangeValidator1" ErrorMessage="请输入数值" ControlToValidate="txtAuditValue" Type="Double" MinimumValue="0" MaximumValue="200000000" runat="server"></asp:RangeValidator></td>
					</tr>
					<TR>
						<TD align="center" class="td-label">审核意见</TD>
						<TD>
							<asp:TextBox ID="txtEntAuditIdea" TextMode="MultiLine" Width="100%" Height="100%" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD colspan="2" class="td-submit"><input id="hidMainId" type="hidden" runat="server" />

							<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
							<asp:Button ID="btnSave" Text="保  存" OnClick="btnSave_Click" runat="server" />&nbsp; <INPUT type="button" value="退  出" onclick="window.returnValue=false;window.close();"></TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
