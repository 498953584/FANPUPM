<%@ Page Language="C#" AutoEventWireup="true" CodeFile="expertprojectaudit.aspx.cs" Inherits="ExpertProjectAudit" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>施工组织审核</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
	</head>
		<script language="JavaScript">
		function GoBack()
			{
				window.parent.history.go(-1)
			}
	</script>
	<body  scroll="no">
		<form id="Form1" method="post" runat="server">
			<div class="divContent2">
			    <table width="100%">
			       <TR>
						<TD class="divHeader">专项方案审核</TD>
				   </TR>
			    </table>
				<TABLE class="tableContent2" id="Table1" cellSpacing="0" cellPadding="5px" width="100%" border="0">
					
					<TR>
						<TD class="word">审核人：</TD>
						<TD class="txt" colspan="3">
							<asp:TextBox ID="TxtAuditPerson" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD class="word">审核时间：</TD>
						<TD class="txt" colspan="3">
							<JWControl:DateBox ID="DateAuditTime" runat="server"></JWControl:DateBox></TD>
					</TR>
					<TR>
						<TD class="word">审核结果：</TD>
						<TD class="txt" colspan="3">
							<asp:DropDownList ID="DDLAuditResult" runat="server"></asp:DropDownList></TD>
					</TR>
					<TR>
						<TD class="word">审核意见：</TD>
						<TD class="txt" colspan="3">
							<asp:TextBox ID="TxtAuditIdea" TextMode="MultiLine" Rows="5" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD class="word">备注：</TD>
						<TD class="txt" colspan="3">
							<asp:TextBox ID="TxtRemark" TextMode="MultiLine" Rows="3" Height="50px" runat="server"></asp:TextBox></TD>
					</TR>
					</TABLE>
					<div class="divFooter2">
						<table class="tableFooter2">
					     <TR>
						   <TD>
							<asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;<INPUT onclick="javascript:top.frameWorkArea.window.desktop.getActive().close();" type="button" value="退 出">
							<JWControl:JavaScriptControl ID="Js" runat="server"></JWControl:JavaScriptControl></TD>
					    </TR>
				       </table>
				   </div>
			</div>
		</form>
	</body>
</HTML>
