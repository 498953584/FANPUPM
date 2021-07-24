<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inputauditone.aspx.cs" Inherits="InputAuditOne" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>科技投入审核</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
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
						<td colspan="2" class="td-head">审核开发投入</td>
					</tr>
					<TR>
						<TD width="21%" align="center" height="10" class="td-label">审核人</TD>
						<TD height="10">
							<asp:TextBox ID="txtAuditPeople" CssClass="textarea" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD align="center" height="10" class="td-label">审核日期</TD>
						<TD height="10">
							<JWControl:DateBox ID="txtAuditDate" CssClass="textarea" ReadOnly="true" runat="server"></JWControl:DateBox><input id="hidMainId" type="hidden" name="Hidden1" runat="server" />
</TD>
					</TR>
					<TR>
						<TD align="center" height="10" class="td-label">是否通过</TD>
						<TD height="10">
							<asp:DropDownList ID="ddlAuditResult" runat="server"></asp:DropDownList></TD>
					</TR>
					<TR>
						<TD align="center" class="td-label">审核意见</TD>
						<TD>
							<asp:TextBox ID="txtAuditView" TextMode="MultiLine" Width="100%" Height="100%" CssClass="textarea" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD colspan="2" class="td-submit">
							<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
							<asp:Button ID="btnSave" Text="保  存" CssClass="button-normal" OnClick="btnSave_Click" runat="server" />
							&nbsp;<INPUT type="button" value="退  出" onclick="window.returnValue=false;window.close();" class="button-normal"></TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
