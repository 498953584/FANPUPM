<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanSummaryView.aspx.cs" Inherits="Fund_AccounIncome_AccounIncomeView" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="com.jwsoft.pm.entpm"%>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>计划汇总查看</title>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            replaceEmptyTable('emptyDetail', 'gvwDetail');
            var accTable = new JustWinTable('gvwDetail');
        });
    </script>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;width:800px;">
        <tr>
            <td class="divHeader">
                <asp:Label ID="lblTitle" Text="Label" runat="server"></asp:Label>
                
                <input type="button"  style="float: right;" class="noprint" onclick="winPrint()"
                    value=" 打 印 " />

            </td>
        </tr>
        <tr style="height: 1px;">
            <td>
                <table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style: none;"
                    class="viewTable">
                    <tr>
                        <td style="border-style: none;">
                            制单人:&nbsp;&nbsp;<asp:Label ID="lblPrintPeople" runat="server"></asp:Label>
                        </td>
                        <td style="border-style: none; text-align: right">
                            打印日期:&nbsp;&nbsp;<asp:Label ID="lblPrintDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td style="vertical-align: top;">
                <table border="0px" cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            计划月份
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPlanName" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                        </td>
                        <td class="elemTd">
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            附件
                        </td>
                        <td class="elemTd" id="upload" colspan="3" runat="server">
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            上报人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblInPeople" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            上报日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblInDate" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            备注
                        </td>
                        <td colspan="3" style="word-break: break-all;">
                            <asp:Label ID="lblRemark" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="100%" cellpadding="5px" cellspacing="0">
                    <tr style="height: 20px;">
                        <td style="font-size: 13px; font-weight: bold; text-align: center; position: relative">
                            计划详细
                        </td>
                    </tr>
                    <tr style="vertical-align: top">
                        <td>
                            <asp:GridView ID="gvwDetail" AutoGenerateColumns="false" CssClass="viewTable" ShowFooter="true" OnRowDataBound="gvwDetail_RowDataBound" DataKeyNames="MonthPlanID" runat="server">
<EmptyDataTemplate>
                                    <table id="emptyDetail" class="tab">
                                        <tr class="header">
                                            <th scope="col" style="width: 25px;">
                                                序号
                                            </th>
                                            <th scope="col">
                                                项目
                                            </th>
                                            <th scope="col">
                                                上期计划金额
                                            </th>
                                            <th scope="col">
                                                上期实际发生金额
                                            </th>
                                            <th scope="col">
                                                上期计划执行完成情况
                                            </th>
                                            <th scope="col">
                                                本期计划金额
                                            </th>
                                            <th scope="col">
                                                流程状态
                                            </th>
                                            <th scope="col">
                                                填报人
                                            </th>
                                            <th scope="col">
                                                填报日期
                                            </th>
                                            <th scope="col">
                                                备注
                                            </th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目" HeaderStyle-Width="100px">
<ItemTemplate>
                                            <span class="link" onclick="viewopen('/Fund/MonthPlan/MonthPlanView.aspx?ic=<%# Eval("MonthPlanID") %>')">
                                                <%# Eval("PrjName") %>
                                            </span>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上期计划金额" HeaderStyle-Width="70px">
<ItemTemplate>
                                            <%# Eval("LastPlanMoney").ToString() %>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上期实际发生金额" HeaderStyle-Width="50px">
<ItemTemplate>
                                            <%# Eval("LastActualMoney").ToString() %>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上期计划执行完成情况" HeaderStyle-Width="60px">
<ItemTemplate>
                                            <%# Convert.ToDecimal(Eval("BL")).ToString("0.00") %>%
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="本期计划金额" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
                                            <%# Eval("CurrPlanMoney").ToString() %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态" HeaderStyle-Width="60px">
<ItemTemplate>
                                            <%# Common2.GetState(Eval("FlowState").ToString()) %>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="填报人" HeaderStyle-Width="60px">
<ItemTemplate>
                                            <%# PageHelper.QueryUser(this, Eval("OperatorCode").ToString()) %>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="填报日期" HeaderStyle-Width="70px">
<ItemTemplate>
                                            <%# Common2.GetTime(Eval("OperateTime").ToString()) %>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                <%# Eval("Remark") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; height: 50%; margin-top: 10px;" border="0">
                    <tr id="trAudit" style="vertical-align: top;">
                        <td>
                            <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="093" BusiClass="001" runat="server" />
                        </td>
                    </tr>
                </table>
                <input id="hdnCode" type="hidden" runat="server" />

            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    </form>
    <input type="hidden" id="hdnAccountID" runat="server" />

</body>
</html>
