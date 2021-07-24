<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FunctionManagerEdit.aspx.cs" Inherits="HR_Organization_FunctionManagerEdit" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>资料管理</title>
    <script type="text/javascript">
        window.name = "win";
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
        function checklen(e, maxlength) {
            if (e.value.length > maxlength) {
                alert('备注不能超过' + maxlength + '个字符');
                e.focus();
                return false;
            }
        }

        function successed(action) {
            top.ui.closeWin();
            top.ui.reloadTab();
            top.ui.show( action + '成功!');
        }

    </script>
</head>
<body scroll="no" class="body_popup">
    <form id="Formx" target="win" runat="server">
    <table class="table-normal" id="tablex" cellspacing="0" cellpadding="0" width="100%"
        border="1">
        <tr>
            <td class="td-head" colspan="2" height="20">
                <asp:Label ID="title" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="20%">
                岗级：
            </td>
            <td>
                第<asp:TextBox ID="txtPostLevel" CssClass="text-input" MaxLength="5" Style="width: 60%" runat="server"></asp:TextBox>级(必须为整数)
            </td>
        </tr>
        <tr>
            <td class="td-label" width="20%">
                职级：
            </td>
            <td>
                第<asp:TextBox ID="txtPositionLevel" CssClass="text-input" MaxLength="5" Style="width: 60%" runat="server"></asp:TextBox>级(必须为整数)
            </td>
        </tr>
        <tr>
            <td class="td-label" width="20%">
                职衔：
            </td>
            <td>
                <asp:TextBox ID="txtPostAndRank" CssClass="text-input" MaxLength="25" Style="width: 99%" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="20%">
                备注：
            </td>
            <td>
                <asp:TextBox ID="txtRemark" CssClass="text-input" onkeyup="checklen(this,250);" Style="width: 99%" TextMode="MultiLine" Rows="6" runat="server"></asp:TextBox>
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
