<%@ Page Language="C#" AutoEventWireup="true" CodeFile="costframeanalyzeedit.aspx.cs" Inherits="CostFrameAnalyzeEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title></title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<base target="_self">
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />
</head>
	<body class="body_popup" scroll="no">
		<form id="Formx" method="post" runat="server">
			<TABLE class="table-form" id="Tablex" cellSpacing="0" cellPadding="0" width="100%" align="center"
				border="0">
				<TR>
					<TD colspan="2" vAlign="top" height="20">
						<TABLE id="Tablexx" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR class="td-toolsbar">
								<TD align="left">模板编辑</TD>
								<TD>
									<asp:Button ID="BtnSave" CssClass="button-input" Text="保  存" OnClick="BtnSave_Click" runat="server" />
									<INPUT class="button-input" onclick="javascript:location.href='CostFrameAnalyze.aspx?type=manage';" type="button" value="取  消">&nbsp;
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="td-label" align="right">任务编码：</TD>
					<TD><asp:TextBox ID="TxtCBSCode" ReadOnly="true" style="background-color:#FFFFC0;" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="td-label" align="right">任务名称：</TD>
					<TD><asp:TextBox ID="TxtCostName" runat="server"></asp:TextBox>
						<asp:RequiredFieldValidator ID="rfvCost" ErrorMessage="必填" ControlToValidate="TxtCostName" runat="server"></asp:RequiredFieldValidator></TD>
				</TR>
			</TABLE>
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
		</form>
	</body>
</HTML>
