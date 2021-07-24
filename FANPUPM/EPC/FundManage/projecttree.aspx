<%@ Page Language="C#" AutoEventWireup="true" CodeFile="projecttree.aspx.cs" Inherits="ProjectTree" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_uprojectlist_ascx" Src="~/EPC/UserControl1/uprojectlist.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ProjectTree</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<body>
		<form id="Form1" method="post" runat="server">
				<MyUserControl:epc_usercontrol1_uprojectlist_ascx ID="UProjectList1" runat="server" />
		</form>
	</body>
</HTML>
