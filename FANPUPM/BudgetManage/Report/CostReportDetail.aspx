<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostReportDetail.aspx.cs" Inherits="BudgetManage_Report_CostReportDetail" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>目标成本执行情况明细表</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 添加验证
            $('#btnQuery')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            replaceEmptyTable('emptygvCost', 'gvCost');
            var table = new JustWinTable('gvCost');
            formateTable('gvCost', 2, true);
            setWidthHeight();
            jw.tooltip();
        });
        function setWidthHeight() {
            $('#div_cost').height($(this).height() - 58);
            $('#div_project').height($(this).height() - 20);
            $('#div_cost').width($('#divContent').width() - $('#td_Left').width() - 6);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
        <table>
            <tr>
                <td style="vertical-align: top; border-left: solid 2px #CADEED;">
                    <table class="queryTable" cellpadding="3px" cellspacing="0px">
                        <tr>
                            <td>
                                CBS编码
                            </td>
                            <td>
                                <asp:TextBox ID="txtCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                成本科目
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table class="tab">
                        <tr>
                            <td class="divFooter" style="text-align: left; padding-top: 2px;">
                                <asp:Button ID="btnQuery" Width="80px" Text="查询" OnClick="btnQuery_Click" runat="server" />
                                <asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; height: 100%; width: 100%">
                                <div id="div_cost" class="pagediv">
                                    <asp:GridView ID="gvCost" AutoGenerateColumns="false" CssClass="gvdata" Width="100%" ShowFooter="true" OnRowDataBound="gvCost_RowDataBound" runat="server">
<EmptyDataTemplate>
                                            <table class="gvdata" cellspacing="0" rules="all" border="1" id="emptygvCost" style="width: 100%;
                                                border-collapse: collapse;">
                                                <tr>
                                                    <th class="header" rowspan="2">
                                                        序号
                                                    </th>
                                                    <th class="header" rowspan="2">
                                                        CBS编码
                                                    </th>
                                                    <th class="header" rowspan="2">
                                                        成本科目
                                                    </th>
                                                    <th class="header" colspan="4">
                                                        本月数
                                                    </th>
                                                    <th class="header" colspan="4">
                                                        累计数
                                                    </th>
                                                </tr>
                                                <tr class='header'>
                                                    </th><th rowspan="1">
                                                        目标成本
                                                    </th>
                                                    <th rowspan="1">
                                                        实际成本
                                                    </th>
                                                    <th rowspan="1" class="tooltip" title=" 降低额 = 目标成本 &ndash; 实际成本">
                                                        降低额
                                                    </th>
                                                    <th rowspan="1" class="tooltip" title=" 降低率 = 降低额 &divide; 目标成本">
                                                        降低率
                                                    </th>
                                                    <th rowspan="1">
                                                        目标成本
                                                    </th>
                                                    <th rowspan="1">
                                                        实际成本
                                                    </th>
                                                    <th rowspan="1" class="tooltip" title=" 降低额 = 目标成本 &ndash; 实际成本">
                                                        降低额
                                                    </th>
                                                    <th rowspan="1" class="tooltip" title=" 降低率 = 降低额 &divide; 目标成本">
                                                        降低率
                                                    </th>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 + (this.AspNetPager1.CurrentPageIndex - 1) * 15 %></ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="CBSCode" /><asp:BoundField DataField="CBSName" /><asp:BoundField DataField="MonthBud" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="MonthAcc" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                    <%# decimal.Parse(Eval("MonthBud").ToString()) - decimal.Parse(Eval("MonthAcc").ToString()) %>
                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                    <%# (decimal.Parse(Eval("MonthBud").ToString()) > 0m) ? ((decimal.Parse(Eval("MonthBud").ToString()) - decimal.Parse(Eval("MonthAcc").ToString())) / decimal.Parse(Eval("MonthBud").ToString())).ToString("P2") : 0m.ToString("P2") %>
                                                </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="Bud" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="Acc" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                    <%# decimal.Parse(Eval("Bud").ToString()) - decimal.Parse(Eval("Acc").ToString()) %>
                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                    <%# (decimal.Parse(Eval("Bud").ToString()) > 0m) ? ((decimal.Parse(Eval("Bud").ToString()) - decimal.Parse(Eval("Acc").ToString())) / decimal.Parse(Eval("Bud").ToString())).ToString("P2") : 0m.ToString("P2") %>
                                                </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                    </webdiyer:AspNetPager>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
