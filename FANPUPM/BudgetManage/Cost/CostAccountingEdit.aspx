<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostAccountingEdit.aspx.cs" Inherits="BudgetManage_Cost_CostAccountingEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnCancel').click(wClose);
            Val.validate('form1', 'btnSave');
        });

        //取消
        function wClose() {
            winclose('CostAccountingEdit', 'CostAccounting.aspx', false);
        }

        function checkSel(id) {
            var action = getRequestParam('Action');
            var parent = getRequestParam('parent');
            if (action == 'add')
                parent = '';
            $.ajax({
                type: "GET",
                async: false,
                dataType: "text",
                url: "/BudgetManage/Handler/CheckResType.ashx?" + new Date().getTime() + "&resTypeId=" + id + '&parent=' + parent,
                success: function (data) {
                    if (data == '1') {
                        alert('系统提示：\n此资源类型已经被指定，请重新选择！');
                        $('#btnSave').attr('disabled', 'disabled');
                        return;
                    }
                    else
                        $('#btnSave').removeAttr('disabled');
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center; padding: 1px 1px;">
        <table style="width: 70%; margin: auto; margin-top: 10px;" cellpadding="5px">
            <tr>
                <td class="word" style="white-space: nowrap;">
                    CBS编码
                </td>
                <td>
                    <asp:TextBox ID="txtCBSCode" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    名称
                </td>
                <td class="txt  mustInput">
                    <asp:TextBox ID="txtCBSName" MaxLength="200" CssClass="{required:true, messages:{required:'名称必须输入'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    成本类型
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="ddlType" Enabled="false" runat="server"><asp:ListItem Text="直接成本" Value="D" Selected="true" /><asp:ListItem Text="间接成本" Value="I" /></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    说明
                </td>
                <td>
                    <asp:TextBox ID="txtNote" TextMode="MultiLine" Rows="3" Columns="50" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2" style="margin-bottom: 0px;">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClientClick="return checkResType('aa')" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
