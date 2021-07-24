<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndirectMonthBudget.aspx.cs" Inherits="BudgetManage_Cost_IndirectMonthBudget" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>间接成本月度预算</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.cookie.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            calcSumAccount();   // 计算总预算金额
        });

        var cbsCodeIndex = 1;
        var amountIndex = 3;
        $(document).ready(function () {
            setWidthHeight();
            var gvBudget = new JustWinTable('gvBudget');
            fillSpace();
            replaceEmptyTable('gvEmpty', 'gvBudget')
        });
        function setWidthHeight() {
            $('#divBudget').height($(this).height() - 2);
            $('#div_project').height($(this).height() - $('#ddlYear').height() - 4);
            $('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 8);
        }
        //ShowDialog
        function openWindow(id, amount) {
            var src = '/BudgetManage/Cost/MoreBudget.aspx?id=' + id + '&type=' + $('#hfldYear').val() + '&amount=' + amount;
            top.ui._IndirectMonthBudget = window;
            top.ui.openWin({ title: '月度预算', url: src });
        }

        // 计算总预算金额
        function calcSumAccount() {
            var sumAccount = 0;
            $('#gvBudget tr[id]').each(function () {
                var account = $(this).find("td:eq(3)").html();
                sumAccount += parseFloat(account);
            });
            $('#gvBudget tr:eq(1) td:eq(3)').html(getFormat(sumAccount));
        }

        //格式化三位小数
        function getFormat(num) {
            var numFormat;
            if (num.toFixed) {
                numFormat = num.toFixed(3);
            } else {
                var f3 = Math.pow(10, 3);
                numFormat = Math.round(num * f3) / f3;
            }
            return numFormat;
        }
        //填充其他成本空白字符
        function fillSpace() {
            if (document.getElementById('gvBudget')) {
                var rows = document.getElementById('gvBudget').rows;
                if (rows) {
                    for (i = 2; i < rows.length; i++) {
                        rows[i].cells[cbsCodeIndex].innerText = '         ' + rows[i].cells[cbsCodeIndex].innerText;
                    }
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
        <table>
            <tr>
                <td style="width: 100%; height: 100%;">
                    <table style="width: 100%; height: 100%;">
                        <tr>
                            
                            <td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
                                <table class="tab">
                                    <tr>
                                        <td style="vertical-align: top; height: 100%;">
                                            <div id="divBudget" class="pagediv">
                                                <asp:GridView ID="gvBudget" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvBudget_RowDataBound" DataKeyNames="Id,AccountAmount" runat="server">
<EmptyDataTemplate>
                                                        <table id="gvEmpty" class="gvdata">
                                                            <tr class="header">
                                                                <th scope="col" style="width: 25px;">
                                                                    序号
                                                                </th>
                                                                <th scope="col" style="width: 100px;">
                                                                    CBS编码
                                                                </th>
                                                                <th scope="col">
                                                                    名称
                                                                </th>
                                                                <th scope="col" style="width: 100px;">
                                                                    预算金额
                                                                </th>
                                                                <th scope="col" style="width: 50px;">
                                                                    详细信息
                                                                </th>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
<EmptyDataRowStyle CssClass="header"></EmptyDataRowStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="CBS编码" DataField="CBSCode" HeaderStyle-Width="100px" /><asp:TemplateField HeaderText="名称"><ItemTemplate>
                                                                <%# Eval("CostAcc.Name") %>
                                                            </ItemTemplate></asp:TemplateField><asp:BoundField DataField="AccountAmount" HeaderText="预算金额" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:TemplateField HeaderStyle-Width="50px" HeaderText="详细信息"><ItemTemplate>
                                                                <a class="link" id="moreMonthBudgets" runat="server">月度预算</a>
                                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfldYear" runat="server" />
    </form>
</body>
</html>
