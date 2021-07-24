<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PettyCashClearEdit.aspx.cs" Inherits="PettyCash_PettyCashClearEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />

    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnSave')[0].onclick = function () {
                var time = $('#txtStatetime').val();
                if (!isNaN(time)) {
                    alert('清理时间不能为空');
                    return false;
                }
                var Note = $('#txtStatetime').val();
                if (!isNaN(Note)) {
                    alert('清理时间不能为空');
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
                <td class="descTd" style="width: 50px; text-align: right; margin-top:40px">
                    清理时间
                </td>
                <td class="descTd">
                    <asp:TextBox ID="txtStatetime" Height="15px" Width="300px" onclick="WdatePicker()" runat="server"></asp:TextBox>
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
    <div style="text-align: right; padding-right: 10px; margin-top:30px">
        <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
        <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
    </div>
    </form>
</body>
</html>
