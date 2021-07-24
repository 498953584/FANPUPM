<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectResType.aspx.cs" Inherits="BudgetManage_Resource_SelectResType" %>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>资源类型选择</title>

    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript">
        //保存
        function saveData() {
            parent.document.getElementById("hfldSelResType").value = $('#hfldTreeViewSelectedValue').val();
            parent.document.getElementById('btnAddRes').click();
            //           closeDialog();
        }
        //关闭Dialog
        function closeDialog() {
            $(parent.document).find('.ui-icon-closethick').each(function(i) {
                this.click();
            });
        }
        //点击TreeView设置节点Value
        function saveSelectedValue(value) {
            $('#hfldTreeViewSelectedValue').val(value);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style=" height:90%; width:100%; overflow:auto;">
        <asp:TreeView ID="tvResource" ExpandDepth="1" ShowLines="true" OnTreeNodePopulate="TreeView_Populate" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
    </div>
    <div style=" text-align:right; margin:5px 0px;">
        <input type="button" id="btnSave" value="保存" onclick="saveData()" />
        <input type="button" id="btnCancel" value="取 消" onclick="closeDialog()" />
        
        <asp:HiddenField ID="hfldTreeViewSelectedValue" runat="server" />
    </div>
    </form>
</body>
</html>
