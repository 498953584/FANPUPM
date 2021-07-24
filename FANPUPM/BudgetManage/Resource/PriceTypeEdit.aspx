<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PriceTypeEdit.aspx.cs" Inherits="BudgetManage_Resource_PriceTypeEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>价格类型</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />

    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            var action = getRequestParam('Action');
            if (action == 'Query') {
                setAllInputDisabled();
            }
            Val.validate('form1');
            $('#btnSave')[0].onclick = function () {
                var name = $('#txtPriceTypeName').val();
                if (!isNaN(name)) {
                    alert('名称不能全为数字或者名称不能为空');
                    return false;
                }
                return true;
            }
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 150px">
        <table style="width: 385px;" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="descTd" style="width: 50px; text-align: right;">
                    名称
                </td>
                <td class="elemTd mustInput">
                    <asp:TextBox ID="txtPriceTypeName" Height="15px" Width="300px" CssClass="easyui-validatebox" onkeyup="value=value.replace(/[^\d\a-zA-Z\u4E00-\u9FA5]/g,'')" onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d\a-zA-Z\u4E00-\u9FA5]/g,''))" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="noteTd" style="text-align: right;">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNote" TextMode="MultiLine" Width="300px" Height="50px" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div style="text-align: right; padding-right: 10px;">
        <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
        <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
    </div>
    </form>
</body>
</html>
