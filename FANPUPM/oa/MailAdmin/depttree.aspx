<%@ Page Language="C#" AutoEventWireup="true" CodeFile="depttree.aspx.cs" Inherits="DeptTree" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>DeptTree</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<iewc:TreeView ID="TViewDept" AutoSelect="true" SelectExpands="true" ShowToolTip="false" ExpandLevel="1" runat="server"></iewc:TreeView></FONT>
		</form>
	</BODY>
</HTML>
