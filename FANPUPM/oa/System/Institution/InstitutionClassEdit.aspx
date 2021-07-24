<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InstitutionClassEdit.aspx.cs" Inherits="Enterprise_InstitutionClassEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>企业制度编辑</title>
    <script type="text/javascript">
        function ValidTXT() {
            if (document.getElementById("txtClassName").value == "" || document.getElementById("txtClassName").value.length == 0) {
                top.ui.alert('分类名称不能为空!');
                return false;
            }
            if (document.getElementById("txtClassCode").value == "" || document.getElementById("txtClassCode").value.length == 0) {
                top.ui.alert('分类编码不能为空!');
                return false;
            }
        }
        function setPurview() {
            var ContClassID = document.getElementById('HdnLc').value;
            var url = "ProManage.aspx?opType=InsClass&InsCCode=" + ContClassID;
            var result = window.showModalDialog(url, window, 'dialogHeight:550px;dialogWidth:650px;center:1;help:0;status:0;');
            if (result != "") {
                document.getElementById("HdnPer").value = result;
                return true;
            }
            else {
                return false;
            }
        }
        function setPurviewPerson(ucode, un) {
            var p = new Array();
            p[0] = "";
            p[1] = "";
            var url = "";
            url = "consignee.aspx?ucode=" + ucode + "&un=" + escape(un);
            var result = window.showModalDialog(url, p, "dialogHeight:420px;dialogWidth:430px;center:1;help:0;status:0;");
            document.getElementById('HdnPer').value = p[0];
            if (result == true) {
                return true;
            }
            else {
                return false;
            }
        }

        function successed() {
            top.ui.show( '保存成功');
            top.ui.closeWin();
            top.ui.reloadTab();
        }
    </script>
</head>
<body class="body_clear">
    <form id="form1" runat="server">
    <table style="width: 100%;" class="table-normal" border="1">
        <tr>
            <td class="td-head" colspan="2">
                企业制度编辑
            </td>
        </tr>
        <tr>
            <td class="td-label">
                分类名称：
            </td>
            <td>
                <asp:TextBox ID="txtClassName" Width="60%" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-label">
                分类编码：
            </td>
            <td>
                <asp:TextBox ID="txtClassCode" Width="60%" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-label">
                权限控制：
            </td>
            <td>
                <asp:RadioButtonList ID="RblPer" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server"><asp:ListItem Value="-1" Selected="true" Text="默认" /><asp:ListItem Value="0" Text="以岗位设定权限" /><asp:ListItem Value="1" Text="以个人设定权限" /></asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="td-label">
                是否只读
            </td>
            <td>
                <asp:RadioButtonList ID="radlReadOnly" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server"><asp:ListItem Value="1" Text="是" /><asp:ListItem Value="0" Selected="true" Text="否" /></asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="td-label">
                备注：
            </td>
            <td>
                <asp:TextBox ID="txtRemark" TextMode="MultiLine" Width="236px" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-submit" colspan="2">
                <asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />
                <input type="button" id="btnClose" value="取 消" onclick="top.ui.closeWin();" />
            </td>
        </tr>
    </table>
    <input type="hidden" id="HdnPer" style="width: 1px" runat="server" />

    <input type="hidden" id="HdnLc" style="width: 1px" runat="server" />

    </form>
</body>
</html>
