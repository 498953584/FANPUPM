<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectSub.aspx.cs" Inherits="Fund_AccountPayout_SelectSub" %>
<%@ Import Namespace="cn.justwin.BLL"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>项目合同支付</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.cookie.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            replaceEmptyTable('emptyPayment', 'gvBudget');
            var jwTable = new JustWinTable('gvBudget');
        });

        //查看附件
        function queryAdjunct(path) {
            alert(path);
            showFiles(path, 'divOpenAdjunct');
        }
        //查看
        function openQuery() {
            var id = document.getElementById('hfldPurchaseChecked').value;
            if (id != "") {
                var url = "/BudgetManage/Cost/CostVerifyRecord.aspx?ic=" + id;
                var title = "";
                title = "项目合同支付";
                parent.desktop.RequestPaymentEdit = window;
                title += '明细';
                toolbox_oncommand(url, title);
            }
        }
        function dbClickRow(Guid) {
            //alert(Guid);
            //            $(parent.document).find('#hdnRPUID').val(Guid);
            //            $(parent.document).find('.ui-icon-closethick').each(function() {
            //                 this.click();
            //             });
            //            parent.document.getElementById('btnPlan').click();
            if (typeof top.ui.callback == 'function') {
                top.ui.callback(Guid);
                top.ui.callback = null;
            }
            top.ui.closeWin();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
        <table style="width: 100%; height: 100%;">
            <tr>
                <td style="width: 100%; vertical-align: top; height: 100%;">
                    <table class="tab" style="height: 100%;">
                        <tr id="header">
                            <td>
                                <asp:Label ID="lblTitle" Text="项目请款" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                                    <tr>
                                        <td class="descTd">
                                            发生时间：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSignDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                            
                                        </td>
                                        <td class="descTd">
                                            -
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEndDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                            
                                        </td>
                                        <td>
                                            &nbsp;费用项目：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtmoneyPrj" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                            <asp:Button ID="btnSerch" Text="查询" OnClick="btnSerch_Click" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; height: 100%;">
                                <div id="divBudget" style="overflow: hidden; height: 100%;">
                                    <div id="divDiaries" style="overflow: auto;">
                                        <asp:GridView ID="gvBudget" AllowPaging="true" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvBudget_RowDataBound" OnPageIndexChanging="gvBudget_PageIndexChanging" DataKeyNames="IndiaryId" runat="server">
<EmptyDataTemplate>
                                                <table id="emptyPayment" class="tab">
                                                    <tr class="header">
                                                        <th scope="col" style="width: 25px;">
                                                            序号 </td>
                                                            <th scope="col">
                                                                费用项目
                                                            </th>
                                                            <th scope="col">
                                                                发生单位
                                                            </th>
                                                            <th scope="col">
                                                                发生时间
                                                            </th>
                                                            <th scope="col">
                                                                填报人
                                                            </th>
                                                            <th scope="col">
                                                                经手人
                                                            </th>
                                                            <th scope="col">
                                                                流程状态</td>
                                                                <th scope="col">
                                                                    间接费用</td>
                                                                    <th scope="col">
                                                        入账率</td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="费用项目">
<ItemTemplate>
                                                        <span class="al" onclick="viewopen('/BudgetManage/Cost/CostVerifyRecord.aspx?ic=<%# Eval("IndiaryId") %>')">
                                                            <%# Eval("Name") %>
                                                        </span>
                                                    </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="发生单位" DataField="department" /><asp:BoundField HeaderText="发生时间" DataField="InputDate" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" /><asp:BoundField HeaderText="填报人" DataField="InputUser" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" /><asp:BoundField HeaderText="经手人" DataField="IssuedBy" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" /><asp:TemplateField HeaderText="流程状态" HeaderStyle-Width="80px">
<ItemTemplate>
                                                        <%# Common2.GetState(Eval("FlowState").ToString()) %>
                                                    </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="间接费用" DataField="Total" DataFormatString="{0:F2}" /><asp:TemplateField HeaderText="入账率">
<ItemTemplate>
                                                        <%# PayOutp(Eval("Total").ToString(), Eval("PayMoney").ToString()) %>
                                                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="divOpenAdjunct" title="查看附件" style="display: none;">
        <table id="annexTable" class="gvdata" style="width: 100%;" runat="server"><tr class="header" runat="server"><td style="width: 60%" runat="server">
                        名称
                    </td><td style="width: 30%" runat="server">
                        大小
                    </td><td runat="server">
                    </td></tr></table>
    </div>
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    <asp:HiddenField ID="hfldYear" runat="server" />
    <asp:HiddenField ID="hfplantype" runat="server" />
    <asp:HiddenField ID="hfPrjName" runat="server" />
    <asp:HiddenField ID="hfldMonthPlanID" runat="server" />
    </form>
</body>
</html>
