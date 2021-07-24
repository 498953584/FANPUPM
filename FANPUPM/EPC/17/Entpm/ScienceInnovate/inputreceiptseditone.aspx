<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inputreceiptseditone.aspx.cs" Inherits="InputReceiptsEditOne" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>InputReceiptsEditOne</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
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
				<TR>
					<TD colspan="4" class="td-head">编辑收益与投入年计划</TD>
				</TR>
				<TR>
					<TD align="center" height="10" class="td-label">编 号</TD>
					<TD height="10">
						<asp:TextBox ID="txtSerialNumber" runat="server"></asp:TextBox></TD>
					<TD align="center" height="10" class="td-label">计划年度(年)</TD>
					<TD height="10">
						<asp:DropDownList ID="ddlPlanYear" runat="server"></asp:DropDownList></TD>
				</TR>
				<TR>
					<TD align="center" height="10" class="td-label">制表人</TD>
					<TD height="10"><FONT face="宋体">
							<asp:TextBox ID="txtTabPeople" runat="server"></asp:TextBox></FONT></TD>
					<TD align="center" height="10" class="td-label">制表日期</TD>
					<TD height="10">
						<JWControl:DateBox ID="txtTabTime" runat="server"></JWControl:DateBox></TD>
				</TR>
				<TR>
					<TD align="center" height="10" class="td-label">审核人</TD>
					<TD height="10">
						<asp:TextBox ID="txtExaminePeople" runat="server"></asp:TextBox></TD>
					<TD align="center" height="10" class="td-label">审核日期</TD>
					<TD height="10">
						<JWControl:DateBox ID="txtExamineTime" runat="server"></JWControl:DateBox></TD>
				</TR>
				<TR>
					<TD align="center" class="td-label"><FONT face="宋体" class="td-label">备注</FONT></TD>
					<TD colspan="3"><FONT face="宋体">
							<asp:TextBox ID="txtRemark" TextMode="MultiLine" Width="100%" Height="100%" runat="server"></asp:TextBox></FONT></TD>
				</TR>
				<TR>
					<TD colspan="4" class="td-submit"><FONT face="宋体">
							<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
							<asp:Button ID="btnSave" Text="保  存" CssClass="button-normal" OnClick="btnSave_Click" runat="server" />&nbsp;
							<INPUT type="button" value="退  出" onclick="window.returnValue=true;window.close();" class="button-normal"></FONT></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
