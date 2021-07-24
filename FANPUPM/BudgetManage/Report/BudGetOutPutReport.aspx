<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudGetOutPutReport.aspx.cs" Inherits="BudgetManage_Report_BudGetOutPutReport" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>目标成本产出报表</title><link type="text/css" rel="stylesheet" href="../../../Script/jquery.easyui/themes/default/easyui.css" />
<link type="text/css" rel="stylesheet" href="../../../Script/jquery.easyui/themes/icon.css" />
<link type="text/css" rel="stylesheet" href="../../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.treetable.js"></script>
    <script type="text/javascript" src="../../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../../Script/jw.js"></script>
    <script type="text/javascript" src="../../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#gvwBudGetOutPut').treetable(true, 'BudTask');
            var jwTable = new JustWinTable('gvwBudGetOutPut');
            replaceEmptyTable('emptyBudGetOutPut', 'gvwBudGetOutPut');
            // 显示被截取的信息
            jw.tooltip();
        })
    </script>
    <style type="text/css">
        .redColor
        {
            color: Red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="divFooter" style="text-align: left;">
                <asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <asp:GridView ID="gvwBudGetOutPut" AutoGenerateColumns="false" CssClass="gvdata" Width="100%" OnRowDataBound="gvwBudGetOutPut_RowDataBound" runat="server">
<EmptyDataTemplate>
                        <table id="emptyBudGetOutPut">
                            <tr class="header">
                                <th scope="col" style="width: 25px;" rowspan="2">
                                    序号
                                </th>
                                <th rowspan="2">
                                    清单编码
                                </th>
                                <th rowspan="2">
                                    项目名称
                                </th>
                                <th rowspan="2">
                                    项目特征描述
                                </th>
                                <th rowspan="2">
                                    类型
                                </th>
                                <th rowspan="2">
                                    单位
                                </th>
                                <th rowspan="2">
                                    工程量
                                </th>
                                <th rowspan="2">
                                    综合单价(元)
                                </th>
                                <th rowspan="2">
                                    合价(元)
                                </th>
                                <th colspan="3">
                                    目标成本组成
                                </th>
                                <th rowspan="2">
                                    成本单价(元)
                                </th>
                                <th rowspan="2">
                                    利润(元)
                                </th>
                                <th rowspan="2">
                                    利润率
                                </th>
                            </tr>
                            <tr class="header">
                                <th>
                                    损耗系数
                                </th>
                                <th>
                                    单价(元)
                                </th>
                                <th>
                                    合价(元)
                                </th>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                <%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="清单编码"><ItemTemplate>
                                <%# Eval("TaskCode") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
                                <span class="tooltip" title='<%# Eval("TaskName").ToString() %>'>
                                    <%# StringUtility.GetStr(Eval("TaskName").ToString(), 10) %>
                                </span>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目特征描述"><ItemTemplate>
                                <span class="tooltip" title='<%# Eval("FeatureDescription").ToString() %>'>
                                    <%# StringUtility.GetStr(Eval("FeatureDescription").ToString(), 20) %>
                                </span>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型"><ItemTemplate>
                                <%# Eval("TypeName") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位"><ItemTemplate>
                                <%# Eval("Unit") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程量" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                <%# Eval("Quantity") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="综合单价(元)" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                <%# Eval("ConUnitPrice") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合价(元)" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                <%# Eval("ConTotal") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="损耗系数" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                <%# Eval("LossCoefficient") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单价(元)" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                <%# Eval("ResourcePrice") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合价(元)" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                <%# Eval("BudTotal") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="成本单价(元)" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                <%# Eval("BudUnitPrice") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="利润(元)" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                <%# Eval("Profit") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="利润率" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                <%# Eval("ProfitRate") %>
                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                </webdiyer:AspNetPager>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
