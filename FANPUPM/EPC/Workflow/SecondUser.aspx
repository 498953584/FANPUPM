<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecondUser.aspx.cs" Inherits="SecondUser" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>SecondUser</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript" type="text/javascript">
			function pickOperater() {
				var p = new Array();
				p[0] = "";
				p[1] = "";
				var url = "";
				url = "../../ModuleSet/Common/PickSinglePerson.aspx";
				window.showModalDialog(url,p,"dialogHeight:475px;dialogWidth:480px;center:1;help:0;status:0;");
				if (p[0]!=""){
					document.getElementById('hdnUserCode').value = p[0];
					document.getElementById('txtSecondUser').value = p[1];
				}
			}
		</script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE class="table-form" id="TableVersion" cellSpacing="0" cellPadding="0" width="100%" border="1">
				<TR>
					<TD class="td-head" colSpan="4" height="20">代办人设置</TD></TR>
				<tr>
					<td class="td-label" width="25%">代办人</td>
					<td><asp:TextBox ID="txtSecondUser" runat="server"></asp:TextBox><input id="hdnUserCode" type="hidden" name="hdnUserCode" runat="server" />
<IMG onclick="pickOperater()" src="../../images/pickRation.gif" align="bottom" width="22" height="25"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="代办人不能为空！" Display="None" ControlToValidate="txtSecondUser" runat="server"></asp:RequiredFieldValidator></td></tr>
				<TR>
					<TD class="td-label" width="25%">开始时间</TD>
					<TD><JWControl:DateBox ID="txtBeginDate" ReadOnly="true" runat="server"></JWControl:DateBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="开始时间不能为空！" Display="None" ControlToValidate="txtBeginDate" runat="server"></asp:RequiredFieldValidator></TD></TR>
				<TR>
					<TD class="td-label" width="25%">结束时间</TD>
					<TD><JWControl:DateBox ID="txtEndDate" ReadOnly="true" runat="server"></JWControl:DateBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="结束时间不能为空！" Display="None" ControlToValidate="txtEndDate" runat="server"></asp:RequiredFieldValidator></TD></TR>
				<TR>
					<TD class="td-label" width="25%"></TD>
					<TD></TD></TR>
				<TR>
					<TD class="td-submit" colSpan="4" height="20">&nbsp;&nbsp;<asp:Button ID="btnOK" Text="确 定" runat="server" />&nbsp;<asp:Button ID="btnClear" Text="收 回" runat="server" />&nbsp;<asp:Button ID="btnUpd" Text="更 新" runat="server" /><asp:ValidationSummary ID="ValidationSummary1" runat="server"></asp:ValidationSummary>
					</TD></TR></TABLE>
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
		</form>
	</body>
</HTML>
