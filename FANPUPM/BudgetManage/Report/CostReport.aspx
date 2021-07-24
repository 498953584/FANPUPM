<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostReport.aspx.cs" Inherits="BudgetManage_Report_CostReport" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>目标成本执行情况分析</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 添加验证
            $('#brnQuery')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            replaceEmptyTable('emptygvCost', 'gvCost');
            var table = new JustWinTable('gvCost');
            formateTable('gvCost', 2, true);
            jw.tooltip();
        });
        function prjDetail(path) {
            parent.desktop.PayoutContractEdit = window;
            toolbox_oncommand(path, "分项工程成本对比表");
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table class="queryTable" cellpadding="3px" cellspacing="0px">
                        <tr>
                            <td>
                                项目编码
                            </td>
                            <td>
                                <asp:TextBox ID="txtCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                项目名称
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="divFooter" style="text-align: left; padding-top: 2px;">
                    <asp:Button ID="brnQuery" Text="查询" OnClick="brnQuery_Click" runat="server" />
                    <asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvCost" AutoGenerateColumns="false" CssClass="gvdata" Width="100%" ShowFooter="true" OnRowDataBound="gvCost_RowDataBound" runat="server">
<EmptyDataTemplate>
                            <table class="gvdata" cellspacing="0" rules="all" border="1" id="emptygvCost" style="width: 100%;
                                border-collapse: collapse;">
                                <tr>
                                    <th class="header" rowspan="3">
                                        序号
                                    </th>
                                    <th class="header" rowspan="3">
                                        项目编码
                                    </th>
                                    <th class="header" rowspan="3">
                                        项目名称
                                    </th>
                                    <th class="header" colspan="6">
                                        本月数
                                    </th>
                                    <th class="header" colspan="6">
                                        累计数
                                    </th>
                                </tr>
                                <tr class='header'>
                                    </th><th colspan="2">
                                        目标成本
                                    </th>
                                    <th colspan="2">
                                        实际成本
                                    </th>
                                    <th rowspan="2" class="tooltip" title=" 降低额 = 目标成本 &ndash; 实际成本">
                                        降低额
                                    </th>
                                    <th rowspan="2" class="tooltip" title=" 降低率 = 降低额 &divide; 目标成本">
                                        降低率
                                    </th>
                                    <th colspan="2">
                                        目标成本
                                    </th>
                                    <th colspan="2">
                                        实际成本
                                    </th>
                                    <th rowspan="2"  class="tooltip" title=" 降低额 = 目标成本 &ndash; 实际成本">
                                        降低额
                                    </th>
                                    <th rowspan="2" class="tooltip" title=" 降低率 = 降低额 &divide; 目标成本">
                                        降低率
                                    </th>
                                </tr>
                                <tr class='header'>
                                    </th><th rowspan="1">
                                        直接成本预算
                                    </th>
                                    <th rowspan="1">
                                        间接成本预算
                                    </th>
                                    <th rowspan="1">
                                        直接成本
                                    </th>
                                    <th rowspan="1">
                                        间接成本
                                    </th>
                                    <th rowspan="1">
                                        直接成本预算
                                    </th>
                                    <th rowspan="1">
                                        间接成本预算
                                    </th>
                                    <th rowspan="1">
                                        直接成本
                                    </th>
                                    <th rowspan="1">
                                        间接成本
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<ItemTemplate>
                                    <%# Container.DataItemIndex + 1 + (this.AspNetPager1.CurrentPageIndex - 1) * 15 %></ItemTemplate>
</asp:TemplateField><asp:TemplateField><ItemTemplate>
                                    <span class="link" onclick="prjDetail('/BudgetManage/Report/SubProjectCompare.aspx?prjId=<%# Eval("PrjGuid") %>&year=<%# System.Convert.ToDateTime(Eval("StartDate")).Year %>')">
                                        <%# Eval("PrjCode") %></span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("PrjName").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("PrjName").ToString(), 10) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="MonthRealitybud" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="Monthbud" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="MonthReality" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="Monthacc" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="MonthChazhi" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="MonthBilu" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="Target" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="IndirBud" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="Reality" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="IndirAcc" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="Chazhi" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="Bilu" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                    </webdiyer:AspNetPager>
                </td>
            </tr>
        </table>
    </div>
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    </form>
</body>
</html>
