<%@ Page Language="C#" AutoEventWireup="true" CodeFile="codetypelist.aspx.cs" Inherits="CodeTypeList" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>编码类型</title></head>
<body scroll="no" class="body_frame">
	<form id="Form1" method="post" runat="server">
	<table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0">
		<tr>
			<td>
				<div class="gridBox">
					<asp:TreeView ID="TrVCodeType" ShowLines="true" selectexpands="True" showtooltip="False" expandlevel="1" Height="95%" Width="200px" runat="server"><SelectedNodeStyle BorderColor="Transparent" ForeColor="Red" /></asp:TreeView>
				</div>
			</td>
		</tr>
	</table>
	</form>
</body>
</html>
