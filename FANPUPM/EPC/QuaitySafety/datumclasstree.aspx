<%@ Page Language="C#" AutoEventWireup="true" CodeFile="datumclasstree.aspx.cs" Inherits="DatumClassTree" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>DatumClassTree</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    
    <table id="Table1" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <asp:TreeView ID="tvProject" ShowLines="true" showtooltip="False" Height="100%" Width="180px" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
            </td>
        </tr>
    </table>
   
    </form>
</body>
</html>
