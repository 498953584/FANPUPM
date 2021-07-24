<%@ Page Language="C#" AutoEventWireup="true" CodeFile="scienceinnovatesumaudit.aspx.cs" Inherits="ScienceInnovateSumAudit" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ScienceInnovateSumAudit</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<base target="_self">
	</head>
	<body class="body_popup">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE class="table-form" id="Table1" height="100%" cellSpacing="1" cellPadding="1" width="100%"
					border="1">
					<TR>
						<TD class="td-head" colSpan="2">施工技术总计审核</TD>
					</TR>
					<TR>
						<TD class="td-label" width="30%">审核人</TD>
						<TD><asp:TextBox ID="txtAuditPeople" runat="server"></asp:TextBox>
							<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></TD>
					</TR>
					<TR>
						<TD class="td-label">审核日期</TD>
						<TD><JWControl:DateBox ID="txtAuditTime" runat="server"></JWControl:DateBox></TD>
					</TR>
					<TR>
						<TD class="td-label">审核结果</TD>
						<TD><asp:RadioButtonList ID="chkResult" Width="152px" RepeatDirection="Horizontal" runat="server"><asp:ListItem Value="1" Selected="true" Text="通过" /><asp:ListItem Value="0" Text="不通过" /></asp:RadioButtonList></TD>
					</TR>
					<TR>
						<TD class="td-label">审核意见</TD>
						<TD>
							<asp:TextBox ID="txtRemark" Width="100%" TextMode="MultiLine" Height="100px" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD colspan="2" class="td-submit">
							<asp:Button ID="Button1" Text="确  定" OnClick="Button1_Click" runat="server" />&nbsp; 
							<INPUT type="button" value="退  出" onclick="window.close();"></TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
