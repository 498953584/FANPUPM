<%@ Page Language="C#" AutoEventWireup="true" CodeFile="datesetting.aspx.cs" Inherits="dateSetting" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>dateSet</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></head>
	<body>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<br>
				<br>
				<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="60%" align="center" border="1">
					<TR>
						<TD align="center" width="35%"><FONT face="宋体">当前考察日期设置：</FONT></TD>
						<TD><asp:Label ID="currentLbl" runat="server"></asp:Label></TD>
					</TR>
					<TR>
						<TD align="center" width="35%">考察开始日期设置→</TD>
						<TD><asp:TextBox ID="startTxb" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="*" ControlToValidate="startTxb" runat="server"></asp:RequiredFieldValidator></TD>
					</TR>
					<TR>
						<TD align="center" width="35%">考察结束日期设置→</TD>
						<TD><asp:TextBox ID="endTxb" runat="server"></asp:TextBox>(不选择结束日期将考察到系统当天)</TD>
					</TR>
				</TABLE>
				<TABLE cellSpacing="1" cellPadding="1" width="60%" align="center" border="0">
					<TR>
						<TD align="right"><asp:Button ID="btnAdd" Text=" 提交 " CssClass="button-normal" OnClick="btnAdd_Click" runat="server" /></TD>
						<td width="10%"></td>
						<TD align="left"><input type="reset" value=" 取消 " name="btnCancel" class="button-normal">
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
