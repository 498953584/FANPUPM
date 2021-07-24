<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubProjectCompare.aspx.cs" Inherits="BudgetManage_Report_SubProjectCompare" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>分项工程成本对比表</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 添加验证
            $('#btnSearch')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            var subProject2 = new JustWinTable('tableSubProject');
            setWidthHeight();
            jw.tooltip();
        });
        function setWidthHeight() {
            $('#div_subproject').height($(this).height() - 58);
            $('#div_project').height($(this).height() - 20);
            $('#div_subproject').width($('#divContent').width() - $('#td_Left').width() - 6);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
        <table>
            <tr>
                <td style="vertical-align: top; height: 98%; border-left: solid 2px #CADEED;">
                    <table class="queryTable" cellpadding="3px" cellspacing="0px">
                        <tr>
                            <td>
                                任务编码
                            </td>
                            <td>
                                <asp:TextBox ID="txtCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                任务名称
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table class="tab">
                        <tr>
                            <td class="divFooter" style="text-align: left; padding-top: 2px;">
                                <asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_OnClick" runat="server" />
                                <asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; height: 100%; width: 100px;">
                                <div id="div_subproject" class="pagediv" style="">
                                    <asp:Repeater ID="rptSubProject" OnItemDataBound="rptSubProject_OnItemDataBound" runat="server">
<HeaderTemplate>
                                            <table id="tableSubProject" class="gvdata" cellspacing="0" rules="all" border="1"
                                                style="border-collapse: collapse;">
                                                <tr class="header" style="color: #727faa;">
                                                    <td class="td_description" rowspan="2" style="width: 25px;">
                                                        序号
                                                    </td>
                                                    <td class="td_description" rowspan="2" style="width: 75px">
                                                        任务编码
                                                    </td>
                                                    <td class="td_description" rowspan="2">
                                                        任务名称
                                                    </td>
                                                    <td class="td_description" colspan="4" style="text-align: center">
                                                        本月数
                                                    </td>
                                                    <td class="td_description" colspan="4" style="text-align: center">
                                                        累计数
                                                    </td>
                                                </tr>
                                                <tr class="header" style="color: #727faa;">
                                                    <td class="td_description" style="width: 75px">
                                                        目标成本
                                                    </td>
                                                    <td class="td_description" style="width: 75px">
                                                        实际成本
                                                    </td>
                                                    <td align="center" class="tooltip" title=" 降低额 = 目标成本 &ndash; 实际成本" style="width: 75px">
                                                        降低额
                                                    </td>
                                                    <td align="center" class="tooltip" title=" 降低率 = 降低额 &divide; 目标成本" style="width: 75px">
                                                        降低率
                                                    </td>
                                                    <td class="td_description" style="width: 75px">
                                                        目标成本
                                                    </td>
                                                    <td class="td_description" style="width: 75px">
                                                        实际成本
                                                    </td>
                                                    <td align="center" class="tooltip" title=" 降低额 = 目标成本 &ndash; 实际成本" style="width: 75px">
                                                        降低额
                                                    </td>
                                                    <td align="center" class="tooltip" title=" 降低率 = 降低额 &divide; 目标成本" style="width: 75px">
                                                        降低率
                                                    </td>
                                                </tr>
                                        </HeaderTemplate>

<ItemTemplate>
                                            <tr class="rowa" id='<%# Eval("TaskId") %>">'>
                                                <td class="td_decimal">
                                                    <%# Container.ItemIndex + 1 + (this.AspNetPager1.CurrentPageIndex - 1) * 15 %>
                                                </td>
                                                <td>
                                                    <%# Eval("TaskCode") %>
                                                </td>
                                                <td>
                                                    <span class="tooltip" title='<%# Eval("TaskName").ToString() %>'>
                                                        <%# StringUtility.GetStr(Eval("TaskName"), 10) %>
                                                    </span>
                                                </td>
                                                <td class="td_decimal">
                                                    <%# Common2.FormatDecimal(Eval("PlanMonthTotal")) %>
                                                </td>
                                                <td class="td_decimal">
                                                    <%# Common2.FormatDecimal(Eval("ActualMonthTotal")) %>
                                                </td>
                                                <td class="td_decimal">
                                                    <%# Common2.FormatDecimal(Eval("MonthDiffTotal")) %>
                                                </td>
                                                <td class="td_decimal">
                                                    <%# Common2.FormatRate(Eval("MonthDiffRate")) %>
                                                </td>
                                                <td class="td_decimal">
                                                    <%# Common2.FormatDecimal(Eval("PlanTotal")) %>
                                                </td>
                                                <td class="td_decimal">
                                                    <%# Common2.FormatDecimal(Eval("ActualTotal")) %>
                                                </td>
                                                <td class="td_decimal">
                                                    <%# Common2.FormatDecimal(Eval("DiffTotal")) %>
                                                </td>
                                                <td class="td_decimal">
                                                    <%# Common2.FormatRate(Eval("DiffRate")) %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

<AlternatingItemTemplate>
                                            <tr class="rowb" id='<%# Eval("TaskId") %>">'>
                                                <td class="td_decimal">
                                                    <%# Container.ItemIndex + 1 + (this.AspNetPager1.CurrentPageIndex - 1) * 15 %>
                                                </td>
                                                <td>
                                                    <%# Eval("TaskCode") %>
                                                </td>
                                                <td>
                                                    <span class="tooltip" title='<%# Eval("TaskName").ToString() %>'>
                                                        <%# StringUtility.GetStr(Eval("TaskName"), 10) %>
                                                    </span>
                                                </td>
                                                <td class="td_decimal">
                                                    <%# Common2.FormatDecimal(Eval("PlanMonthTotal")) %>
                                                </td>
                                                <td class="td_decimal">
                                                    <%# Common2.FormatDecimal(Eval("ActualMonthTotal")) %>
                                                </td>
                                                <td class="td_decimal">
                                                    <%# Common2.FormatDecimal(Eval("MonthDiffTotal")) %>
                                                </td>
                                                <td class="td_decimal">
                                                    <%# Common2.FormatRate(Eval("MonthDiffRate")) %>
                                                </td>
                                                <td class="td_decimal">
                                                    <%# Common2.FormatDecimal(Eval("PlanTotal")) %>
                                                </td>
                                                <td class="td_decimal">
                                                    <%# Common2.FormatDecimal(Eval("ActualTotal")) %>
                                                </td>
                                                <td class="td_decimal">
                                                    <%# Common2.FormatDecimal(Eval("DiffTotal")) %>
                                                </td>
                                                <td class="td_decimal">
                                                    <%# Common2.FormatRate(Eval("DiffRate")) %>
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>

<FooterTemplate>
                                            <tr>
                                                <td colspan="3" align="center">
                                                    合计
                                                </td>
                                                <td class="td_decimal">
                                                    <asp:Literal  ID="ltlPlanMonthTotal" runat="server"></asp:Literal>
                                                </td>
                                                <td class="td_decimal">
                                                    <asp:Literal  ID="ltlActualMonthTotal" runat="server"></asp:Literal>
                                                </td>
                                                <td class="td_decimal">
                                                    <asp:Literal  ID="ltlMonthDiffTotal" runat="server"></asp:Literal>
                                                </td>
                                                <td class="td_decimal">
                                                    <asp:Literal  ID="ltlMonthDiffRate" runat="server"></asp:Literal>
                                                </td>
                                                <td class="td_decimal">
                                                    <asp:Literal  ID="ltlPlanTotal" runat="server"></asp:Literal>
                                                </td>
                                                <td class="td_decimal">
                                                    <asp:Literal  ID="ltlActualTotal" runat="server"></asp:Literal>
                                                </td>
                                                <td class="td_decimal">
                                                    <asp:Literal  ID="ltlDiffTotal" runat="server"></asp:Literal>
                                                </td>
                                                <td class="td_decimal">
                                                    <asp:Literal  ID="ltlDiffRate" runat="server"></asp:Literal>
                                                </td>
                                            </tr>
                                            </table>
                                        </FooterTemplate>
</asp:Repeater>
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
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    </form>
</body>
</html>
