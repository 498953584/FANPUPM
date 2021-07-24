<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayoutBalance.aspx.cs" Inherits="ContractManage_PayoutBalance_PayoutBalance" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>合同结算</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            replaceEmptyTable('emptyContractType', 'gvwContract');
            var contractTable = new JustWinTable('gvwContract');
        });

        //查看支出合同变更
        function rowQueryA(balance) {
            parent.parent.desktop.BalanceQuery = window;
            var path = '/ContractManage/PayoutBalance/BalanceQuery.aspx?Action=Query&BalanceId=' + balance + '&ic=' + balance + '&Random=' + new Date().getTime();
            viewopen(path, "查看支出合同结算");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="pagediv">
        <asp:GridView ID="gvwContract" AutoGenerateColumns="false" CssClass="gvdata" runat="server">
<EmptyDataTemplate>
                <table id="emptyContractType" class="gvdata">
                    <tr class="header">
                        <th scope="col" style="width: 20px;">
                            <input id="chk1" type="checkbox" />
                        </th>
                        <th scope="col" style="width: 25px;">
                            序号
                        </th>
                        <th scope="col">
                            结算编号
                        </th>
                        <th scope="col">
                            结算日期
                        </th>
                        <th scope="col">
                            结算人
                        </th>
                        <th scope="col">
                            金额
                        </th>
                        <th scope="col">
                            流程状态
                        </th>
                        <th scope="col">
                            附件
                        </th>
                    </tr>
                </table>
            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结算编号"><ItemTemplate>
                        <span class="link" onclick="rowQueryA('<%# Eval("BalanceID") %>')">
                            <%# Eval("BalanceCode") %>
                        </span>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结算日期"><ItemTemplate>
                        <%# Common2.GetTime(Eval("BalanceDate").ToString()) %>
                    </ItemTemplate></asp:TemplateField><asp:BoundField DataField="BalancePerson" HeaderText="结算人" /><asp:BoundField DataField="BalanceMoney" ItemStyle-HorizontalAlign="Right" HeaderText="金额" /><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                        <%# Common2.GetState(Eval("FlowState").ToString()) %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
                        <%# GetAnnx(Convert.ToString(Eval("BalanceID"))) %>
                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
    </div>
    </form>
</body>
</html>
