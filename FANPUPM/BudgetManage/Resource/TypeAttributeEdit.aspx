<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TypeAttributeEdit.aspx.cs" Inherits="BudgetManage_Resource_TypeAttributeEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>类别属性</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            var action = getRequestParam('Action');
            if (action == 'Query') {
                setAllInputDisabled();
            }
            Val.validate('form1');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 150px;">
        <table style="" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="descTd" style="width: 70px; text-align: right;">
                    名称
                </td>
                <td class="elemTd mustInput">
                    <asp:TextBox ID="txtAttributeName" Height="15px" Style="width: 250px;" CssClass="{required:true, messages:{required:'名称必须输入'}}" runat="server"></asp:TextBox>
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
