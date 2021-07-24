<%@ Page Language="C#" AutoEventWireup="true" CodeFile="deptlist.aspx.cs" Inherits="DeptList" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>DeptList</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

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
        <div id="divDept" class="pagediv" style="border-right: solid 0px red; width: 175px;
            height: 100%;">
            <asp:TreeView ID="tvDept" Font-Size="12px" ShowLines="true" ExpandDepth="1" OnSelectedNodeChanged="tvDept_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
        </div>
    </font>
    <asp:HiddenField ID="hfldDepId" runat="server" />
    </form>
</body>
</html>
