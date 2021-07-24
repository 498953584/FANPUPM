<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Part.aspx.cs" Inherits="oa_WorkPlanAndSummary_Part" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript">
        addEvent(window, 'load', function() {
        $('#btnCancel').click(function() {
                divClose(parent);
            });
        });     
        //给父页面设置值
        function setVal() {
//            var id = "";
//            var name = "";
//            var method = getRequestParam('Method');
//            //var selval = window.dialogArguments;
//            var tree = document.getElementById('TVCorp');
//            var nodes = tree.getElementsByTagName('TreeNode');
//            var treenode = nodes.length;
//            for (i = 0; i < treenode; i++) {
//                if (nodes[i].selected) {
//                    id = nodes[i].value;
//                    name = nodes[i].text;
//                }
            //            }
            window.location.href+= "&text=" + document.getElementById('hdntext').value + "&value=" + document.getElementById('hdnDeptID').value;
            divClose();
            //alert(document.getElementById('hdnurl').value + "&text=" + document.getElementById('hdntext').value + "&value=" + document.getElementById('hdnDeptID').value);
            //alert(document.getElementById('hdntext').value + document.getElementById('hdnDeptID').value)
            //divClose(parent);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="gridBox" >
            <asp:TreeView ID="TVCorp" ShowLines="true" ExpandDepth="7" Width="100%" ForeColor="Transparent" OnSelectedNodeChanged="TVCorp_SelectedNodeChanged" runat="server"><SelectedNodeStyle BorderColor="Transparent" ForeColor="Red" /><Nodes /></asp:TreeView>
           <div style="border: solid 0px red; text-align: right;">
            <input id="btnSure" type="submit" value="确定" onclick="setVal()"/>
            <input id="btnCancel" type="button" value="取消" onclick="window.close()"/>
           </div>
    </div>
    <input id="hdnDeptID" type="hidden" runat="server" />

    <input id="hdntext" type="hidden" runat="server" />

    <input id="hdnurl" type="hidden" runat="server" />

    </form>
</body>
</html>
