<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PettyCashRepayEdit.aspx.cs" Inherits="PettyCash_PettyCashRepayEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>备用金还款明细</title>
    <style type="text/css">
        .tableFooter2
        {
            width: 580px;
        }
        
        .tableFooter2 tr
        {
            vertical-align: middle;
            height: 32px;
        }
        .descTd
        {
            width: 75px;
            padding-right: 5px;
            height: 40px;
        }
        .elemTd
        {
            padding-left: 5px;
            height: 45px;
        }
    </style>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        //  取消单击事件
        function cancelEvent() {
            var winNo = jw.getRequestParam('winNo');
            top.ui.closeWin({ winNo: winNo });
        }
    </script>
</head>
<body id="print">
    <form id="form1" runat="server">
    <div id="editBudget" style="height: 120px; vertical-align: bottom; width: 100%;">
        <table class="tableFooter2" border="1" cellpadding="1px" cellspacing="1px">
            <tr>
                <td class="descTd">
                    申请日期
                </td>
                <td class="elemTd readonly" align="left">
                    <asp:TextBox ID="txtDatetime" Width="180px" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td class="descTd">
                    申请金额
                </td>
                <td class="elemTd readonly" align="left">
                    <asp:TextBox ID="txtMoney" Width="180px" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    事项
                </td>
                <td class="elemTd readonly" align="left">
                    <asp:TextBox ID="txtMatter" Width="180px" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td class="descTd">
                    用款日期
                </td>
                <td class="elemTd readonly" align="left">
                    <asp:TextBox ID="txtCashDate" Width="180px" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    预报金额
                </td>
                <td class="elemTd readonly" align="left">
                    <asp:TextBox ID="txtAmount" Width="180px" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td class="descTd">
                    未报金额
                </td>
                <td class="elemTd readonly" align="left">
                    <asp:TextBox ID="txtReportMoney" Width="180px" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    核销金额
                </td>
                <td class="elemTd readonly" align="left">
                    <asp:TextBox ID="txtAuditAmount" Width="180px" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td class="descTd">
                    欠款金额
                </td>
                <td class="elemTd readonly" align="left">
                    <asp:TextBox ID="txtOweMoney" Width="180px" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    申请人
                </td>
                <td class="elemTd readonly" align="left">
                    <asp:TextBox ID="txtApplicant" Width="180px" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td class="descTd">
                    还款金额
                </td>
                <td class="elemTd mustInput" align="left">
                    <asp:TextBox ID="txtReturnMoney" Width="180px" CssClass="decimal_input" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    项目
                </td>
                <td class="elemTd readonly" align="left" colspan="3">
                    <asp:TextBox ID="txtProject" Width="180px" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div style="text-align: right; vertical-align: bottom; margin-top: 140px">
            <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
            &nbsp;&nbsp;
            <input type="button" id="Button2" value="取消" onclick="top.ui.closeWin();" />
        </div>
    </div>
    </form>
</body>
</html>
