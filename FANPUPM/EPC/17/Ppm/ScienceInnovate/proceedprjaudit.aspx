<%@ Page Language="C#" AutoEventWireup="true" CodeFile="proceedprjaudit.aspx.cs" Inherits="ProceedPrjAudit" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>项目经理部审核</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
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
			<TABLE id="Table1" class="table-form" cellSpacing="0" cellPadding="0" width="100%" height="100%">
				<tr>
					<td colspan="4" class="td-head">项目部评审意见</td>
				</tr>
				<TR>
					<TD align="center" width="20%" height="10">编号</TD>
					<TD height="10"><FONT face="宋体">
							<asp:TextBox ID="txtPPMSerialNumber" runat="server"></asp:TextBox><input id="hidMainId" type="hidden" name="Hidden1" runat="server" />
</FONT></TD>
					<TD align="center" height="10">总体评审意见</TD>
					<TD height="10">
						<asp:DropDownList ID="ddlPPMAuditResult" runat="server"></asp:DropDownList></TD>
				</TR>
				<TR>
					<td align="center">申报单位意见</td>
					<TD colspan="3"><FONT face="宋体">
							<asp:TextBox ID="txtPPMDeclareUnitIdea" TextMode="MultiLine" Width="100%" Height="100%" runat="server"></asp:TextBox></FONT></TD>
				</TR>
				<TR>
					<td align="center">评审小组意见</td>
					<TD colspan="3"><FONT face="宋体">
							<asp:TextBox ID="txtPPMGroupIdea" TextMode="MultiLine" Width="100%" Height="100%" runat="server"></asp:TextBox></FONT></TD>
				</TR>
				<TR>
					<td align="center">评审委员会意见</td>
					<TD colspan="3"><FONT face="宋体">
							<asp:TextBox ID="txtPPMCommitteeIdea" TextMode="MultiLine" Width="100%" Height="100%" runat="server"></asp:TextBox></FONT></TD>
				</TR>
				<TR>
					<td align="center" style="HEIGHT: 116px">备注</td>
					<TD colspan="3" style="HEIGHT: 112px">
						<asp:TextBox ID="txtPPMRemark" TextMode="MultiLine" Width="100%" Height="100%" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD colspan="4" class="td-submit">
						<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
						<asp:Button ID="btnSave" Text="保  存" OnClick="btnSave_Click" runat="server" /><FONT face="宋体">&nbsp;</FONT><INPUT type="button" value="退  出" onclick="window.close();"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
