<%@ Page Language="C#" AutoEventWireup="true" CodeFile="boxstate.aspx.cs" Inherits="BoxState" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>邮箱状态</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<BR>
				<asp:Table ID="TabBox" Width="75%" HorizontalAlign="Center" BorderStyle="Solid" BorderColor="Silver" BorderWidth="1px" GridLines="Both" CellPadding="1" CellSpacing="0" runat="server"><asp:TableRow Width="400px" Height="20px" HorizontalAlign="Center" CssClass="grid_head" runat="server"><asp:TableCell runat="server"></asp:TableCell><asp:TableCell Text="邮件数量" runat="server"></asp:TableCell><asp:TableCell Text="空间数量" runat="server"></asp:TableCell></asp:TableRow></asp:Table>
			</FONT>
		</form>
	</body>
</HTML>
