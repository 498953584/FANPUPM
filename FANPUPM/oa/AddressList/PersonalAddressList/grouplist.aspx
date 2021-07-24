<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grouplist.aspx.cs" Inherits="GroupList" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>GroupList</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <asp:TreeView ID="tvGroup" ExpandDepth="1" ShowLines="true" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
    </form>
</body>
</html>
