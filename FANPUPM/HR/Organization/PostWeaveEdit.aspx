<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PostWeaveEdit.aspx.cs" Inherits="HR_Organization_PostWeaveEdit" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>岗位编制信息</title><link rel="Stylesheet" type="text/css" href="../../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../../Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnAdd')[0].onclick = function () { if (!$('#Formx').form('validate')) return false; }
        });

        window.name = "win";

        function SelectRole() {
            var role = new Array();
            role[0] = "";
            role[1] = "";
            role[2] = "";
            var url = "PostWeaveRoleSelect.aspx";
            var ref = window.showModalDialog(url, role, 'dialogHeight:400px;dialogWidth:360px;center:1;help:0;status:0;');
            if (role[0] != "") {
                document.getElementById('txtRoleTypeName').value = role[0];
                document.getElementById('HdnDutyCode').value = role[1];
                document.getElementById('HdnRoleTypeCode').value = role[2];
            }
        }
        function CheckNum(sourObj) {
            if (sourObj.value == "") {
                sourObj.value = 1;
            }
            else {
                if (!(new RegExp(/^\d+$/).test(sourObj.value))) {
                    alert('数据类型不正确！');
                    sourObj.focus();
                    return;
                }
            }
        }

        function successed(action) {
            var parentUrl = top.ui.parentWin.location.pathname;
            parentUrl = jw.modifyParam({ url: parentUrl, name: 'sel_val', value: getRequestParam('dc') });
            top.ui.show( action + '成功!');
            top.ui.closeWin();
            top.ui.reloadTab({ url: parentUrl });
        }

    </script>
</head>
<body scroll="no" class="body_popup">
    <form id="Formx" target="win" runat="server">
    <table class="table-normal" id="tablex" cellspacing="0" cellpadding="0" width="100%"
        border="1">
        <tr>
            <td class="td-head" colspan="2" height="20px">
                岗位编制信息
            </td>
        </tr>
        <tr>
            <td class="td-label" width="20%">
                所属部门
            </td>
            <td>
                <asp:TextBox ID="txtDepartment" CssClass="text-input" Style="width: 99%" Enabled="false" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="20%">
                岗位名称
            </td>
            <td>
                <asp:TextBox ID="txtDutyName" MaxLength="100" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="20%">
                岗位编制
            </td>
            <td>
                <asp:TextBox ID="txtDutyNumber" MaxLength="6" runat="server"></asp:TextBox>人
            </td>
        </tr>
        <tr>
            <td class="td-label" width="20%">
                备注
            </td>
            <td>
                <asp:TextBox ID="txtRemark" CssClass="text-input" MaxLength="250" Style="width: 99%" TextMode="MultiLine" Rows="6" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-submit" colspan="2" height="20">
                <asp:Button ID="btnAdd" Text="保  存" OnClick="btnAdd_Click" runat="server" />&nbsp;
                <input id="BtnClose" onclick="top.ui.closeWin();" type="button" value="取 消" />
                <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
                <input id="HdnRecordCode" type="hidden" style="width: 20px" runat="server" />

            </td>
        </tr>
    </table>
    </form>
</body>
</html>
