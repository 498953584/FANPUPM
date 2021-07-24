<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adduser.aspx.cs" Inherits="AddUser" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>AddUser</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
		<script language="javascript">
			function openDeptPicker()
			{
				var dept = new Array();
				dept[0] = "";
				dept[1] = "";
				var url = "/CommonWindow/PickDept.aspx";
				
				window.showModalDialog(url,dept,'dialogHeight:500px;dialogWidth:240px;center:1;help:0;status:0;');
				if (dept[0] !="")
				{
					document.all.txtDept.value = dept[1];
					document.all.hdnTempDeptCode.value = dept[0];
				}
			}
			
		</script>
	</head>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE class="table-normal" id="Table1" cellSpacing="1" cellPadding="1" width="98%" align="center"
					border="1">
					<TR>
						<TD class="td-title" align="center" colSpan="2"><asp:Label ID="Label1" runat="server">Label</asp:Label></TD>
					</TR>
					<TR>
						<TD class="td-label">用户姓名：</TD>
						<TD><asp:TextBox ID="tbUserName" MaxLength="5" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD class="td-label">用户登录ID：</TD>
						<TD><asp:TextBox ID="tbID" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD class="td-label">用户密码：</TD>
						<TD><asp:TextBox ID="tbPwd" MaxLength="15" Text="justwin" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD class="td-label">所属部门：</TD>
						<TD><asp:TextBox ID="txtDept" ReadOnly="true" runat="server"></asp:TextBox><IMG style="CURSOR: hand; valign: bottom" onclick="openDeptPicker();"  src="/Images/corp.gif" align=absmiddle
								width="16" border="0"> <input id="hdnTempDeptCode" style="WIDTH: 88px; HEIGHT: 22px" type="hidden" size="9" name="hdnTempDeptCode" runat="server" />
 <input id="hdnTempDeptName" style="WIDTH: 80px; HEIGHT: 22px" type="hidden" size="8" name="hdnTempDeptName" runat="server" />
</TD>
					</TR>
					<tr>
						<td class="td-label">用户角色</td>
						<td>
							<asp:DropDownList ID="DDLyhjs" runat="server"></asp:DropDownList></td>
					</tr>
				</TABLE>
			</FONT>
			<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="90%" align="center" border="0">
				<TR align="center">
					<TD><FONT face="宋体"><asp:Button ID="BtnAdd" Text="新  增" CssClass="button-normal" OnClick="BtnAdd_Click" runat="server" /></FONT></TD>
					<TD><INPUT class="button-normal" id="BtnReset" type="reset" value=" 重 填 " name="BtnReset"></TD>
					<TD><INPUT class="button-normal" id="BtnClose" onclick="JavaScript:window.close();" type="button"
							value=" 关 闭 " name="BtnClose"></TD>
				</TR>
			</TABLE>
		</form>
	</BODY>
</HTML>
