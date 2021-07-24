<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeptSing.aspx.cs" Inherits="CommonWindow_DeptSing" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>部门树选择</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <style type="text/css">
        .tree-node-selected
        {
            color: White;
            background-color: #3399FF;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            var data = eval($('#hfldDepartJson').val());
            $('#tree').tree({
                data: data,
                onClick: function (node) {
                    $('#hfldDepId').val(node.id);
                    $('#hfldDepName').val(node.text);
                },
                onDblClick: okEvent
            });
        });

        function okEvent() {
            try {
                if (typeof top.ui.callback == 'function') {
                    top.ui.callback({ id: $('#hfldDepId').val(), name: $('#hfldDepName').val() })
                }
                top.ui.closeWin();
            } catch (err) {
                var clist = window.dialogArguments;
                clist[0] = $('#hfldDepName').val();
                clist[1] = $('#hfldDepId').val();
                window.returnvalue = clist;
                window.close();
            }
        }

        function cancelEvent() {
            try {
                top.ui.closeWin();
            } catch (err) {
                window.close();
            }
        }

        function selClass() {
            var DeptID = document.getElementById("HdnDeptID").value;
            var DeptName = document.getElementById("HdnDeptName").value;
            var clist = window.dialogArguments;
            clist[0] = DeptID;
            clist[1] = DeptName;
            window.returnvalue = clist;
            window.close();
        }
    </script>
</head>
<body class="body_clear">
    <form id="form1" runat="server">
    <div id="div_tree" style="height: 430px; overflow: auto;">
        <ul id="tree" style="">
        </ul>
    </div>
    <div style="text-align: right;">
        <input type="button" id="btnOk" onclick="okEvent();" value="保存" />
        <input type="button" id="Button1" onclick="cancelEvent();" value="取消" />
    </div>
    <asp:HiddenField ID="hfldDepartJson" runat="server" />
    <asp:HiddenField ID="hfldDepId" runat="server" />
    <asp:HiddenField ID="hfldDepName" runat="server" />
    </form>
</body>
</html>
