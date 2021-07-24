<%@ Page Language="C#" AutoEventWireup="true" CodeFile="corptypetree.aspx.cs" Inherits="CorpTypeTree" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>单位信息</title><meta Name="vs_showGrid" Content="True" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />


    <script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>

    <meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
<body class="body_frame" scroll="no" onload="selrow3('TVTypet1')">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td valign="top">
                    <asp:TreeView ID="TVType" Target="Corp" ShowLines="true" ExpandDepth="1" Height="100%" Width="200px" runat="server"><SelectedNodeStyle BorderColor="Transparent" ForeColor="Red" /></asp:TreeView>
                    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
