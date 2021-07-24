<%@ Page Language="C#" AutoEventWireup="true" CodeFile="costjudge.aspx.cs" Inherits="CostJudge" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>成本审核</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="0" />
 <!-- 清除页面缓存--><base target="_self">
		<script language="javascript">
			function rdoCheck()
			{
			//window.alert('ok');
			/*
				var check;
				check = document.getElementById(rdo_Pass).innerText;
				window.alert(check);*/
			}
			function Getfocus()
			{
				document.all["txt_Remark"].focus();
			}
		</script>
	</head>
	<body class="body_popup" scroll="no" onload="return Getfocus()">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="1" class="table-form">
				<tr height="22">
					<td class="td-head" colSpan="4">成本审核</td>
				</tr>
				<TR>
					<TD class="td-label" width="40%">审核人</TD>
					<TD class="text-input"><FONT face="宋体">
							<asp:TextBox ID="txt_AuditPeople" Width="100%" Enabled="false" runat="server"></asp:TextBox></FONT></TD>
				</TR>
				<tr>
					<TD class="td-label">审核结论</TD>
					<TD class="text-input"><FONT face="宋体">
							<asp:RadioButton ID="rdo_Pass" Text="通过" Visible="false" runat="server" />
							<asp:RadioButton ID="rdo_UnPass" Text="未通过" Visible="false" runat="server" />
							<asp:DropDownList ID="ddl_AuditResult" Width="100%" runat="server"><asp:ListItem Value="0" Selected="true" Text="未通过" /><asp:ListItem Value="1" Text="通过" /></asp:DropDownList></FONT></TD>
				</tr>
				<tr>
					<TD class="td-label" style="HEIGHT: 21px">审核日期</TD>
					<TD class="text-input" style="HEIGHT: 21px">
						<JWControl:DateBox ID="txt_AuditDate" CssClass="text-input" runat="server"></JWControl:DateBox></TD>
				</tr>
				<tr>
					<TD class="td-label">备注</TD>
					<TD class="text-input"><asp:TextBox ID="txt_Remark" Width="100%" TextMode="MultiLine" runat="server"></asp:TextBox></TD>
				</tr>
				<tr>
					<TD class="td-submit" colSpan="2"><asp:Button ID="btn_save" Text="保 存" OnClick="btn_save_Click" runat="server" /><FONT face="宋体">&nbsp;</FONT><INPUT type="button" value="关 闭" onclick="window.close();"></TD>
				</tr>
			</TABLE>
			<JWControl:JavaScriptControl ID="Js" runat="server"></JWControl:JavaScriptControl>
		</form>
	</body>
</HTML>
