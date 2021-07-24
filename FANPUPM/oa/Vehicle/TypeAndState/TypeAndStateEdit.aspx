<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TypeAndStateEdit.aspx.cs" Inherits="oa_Vehicle_TypeAndState_TypeAndStateEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>车辆类型和状态</title>
    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script src="../../../StockManage/script/Config2.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            var action = getRequestParam('Action');
            if (action == 'Query') {
                setAllInputDisabled();
            }
            document.getElementById('btnSave').onclick = validate;
        });

        function validate() {
            if (!document.getElementById('txtName').value) {
                top.ui.alert('系统提示：\n\n类型名称必须输入！');
                document.getElementById('txtName').focus();
                return false;
            }
            return true;
        }

        function successed() {
            top.ui.show('保存成功');
            top.ui.closeWin();
            var url = { "url": "oa/Vehicle/TypeAndState/TypeAndState.aspx?&prjID=" + $('#hidenPTID').val() };
            top.ui.reloadTab(url);
        }
    </script>
    <style type="text/css">
        .desTd
        {
            width: 19%;
            text-align: right;
        }
        .elemTd
        {
            width: 31%;
            text-align: left;
        }
        .elemTd input
        {
            width: 100%;
        }
        .word
        {
            width: 18%;
            text-align: right;
        }
        .txt
        {
            width: 40%;
            text-align: left;
        }
    </style>
    <link href="/StockManage/skins/sm4.css" rel="stylesheet" type="text/css" />
<link href="/StyleCss/Common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="overflow: auto; margin: auto; height: 95%;">
        <table style="width: 80%; margin: auto;" cellspacing="0" cellpadding="5px">
            <tr>
                <td class="desTd">
                    名称
                </td>
                <td>
                    <asp:TextBox ID="txtName" CssClass="{required:true, messages:{required:'类型名称必须输入'}}" Width="180px" Height="15px" runat="server"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="desTd">
                    编码
                </td>
                <td>
                    <asp:TextBox ID="txtCode" Width="180px" Height="15px" runat="server"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr id="trr" runat="server"><td class="desTd" runat="server">
                    类型/状态
                </td><td runat="server">
                    <asp:RadioButtonList ID="RadioButtonList1" RepeatColumns="2" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" runat="server"><asp:ListItem Value="0" Selected="true" Text="类型" /><asp:ListItem Value="1" Text="状态" /></asp:RadioButtonList>
                </td><td runat="server">
                </td></tr>
        </table>
    </div>
    <div id="divFooter" class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td align="right">
                    <asp:Button ID="btnSave" Text="保存" OnClick="addBtn_Click" runat="server" />
                    &nbsp;<input id="btnCancel" type="button" value="取消" onclick="top.ui.closeWin();" />
                    <asp:HiddenField ID="HidenTypeNumber" runat="server" />
                    <asp:HiddenField ID="hidenPTID" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
