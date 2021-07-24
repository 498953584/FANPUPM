<%@ Page Language="C#" AutoEventWireup="true" CodeFile="corplist.aspx.cs" Inherits="CorpList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>CorpList</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript">
        function setVal(dep) {
            $(parent.document).find('iframe').each(function () {
                if (this.id == 'LinkmanList') {
                    $(this).attr('src', dep);
                }
            });
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <font face="宋体">
        <asp:TreeView ID="tvClient" ExpandDepth="1" ShowLines="true" OnSelectedNodeChanged="tvClient_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
    </form>
</body>
</html>
