<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrgManage.aspx.cs" Inherits="HumanResource_Organization_OrgManage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server"><title>机构设置管理</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/jquery.easyui.extension.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if (top.ui.version == '2') {
                $('#hfldTargetSrc').val('../../oa/SysAdmin/DeptSet/deptset2.aspx?sid=');
            }

            top.ui._OrgManager = window;

            var data = eval($('#hfldDepartJson').val());
            $('#tree').tree({
                data: data,
                onClick: function (node) {
                    $('#hfldDepId').val(node.id);
                    $('#hfldDepName').val(node.text);

                    var src = $('#hfldTargetSrc').val() + node.id;
                    $('#rFrame').attr('src', src);
                }
            });

            // 选中第一个节点
            if (getRequestParam('sel_id')) {
                var sel_id = getRequestParam('sel_id');
                $('#tree div[node-id=' + sel_id + ']').click();
            } else {
                $('#tree div').first().click();
            }
        })

    </script>
</head>
<body class="body_clear">
    <form id="formx" runat="server">
    <table style="width: 100%; height: 99%;" cellpadding="0" cellspacing="0" id="tablex">
        <tr>
            <td width="20%" valign="top" style="">
                <div class="gridBox">
                    <asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_Click" runat="server">导出组织机构</asp:LinkButton>
                    <hr />
                    <div id="div_tree" style="height: 430px; overflow: auto;">
                        <ul id="tree" style="">
                        </ul>
                    </div>
                </div>
            </td>
            <td valign="top"style=" height:99%" >
                <iframe id="rFrame" name="rFrame" frameborder="0" width="99%" style="padding: 3px;" height="99%" runat="server"></iframe>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfldTargetSrc" Value="../../oa/SysAdmin/DeptSet/deptset.aspx?sid=" runat="server" />
    <asp:HiddenField ID="hfldDepartJson" runat="server" />
    </form>
</body>
</html>
