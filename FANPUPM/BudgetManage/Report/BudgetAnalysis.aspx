<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudgetAnalysis.aspx.cs" Inherits="BudgetManage_Report_BudgetAnalysis" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>三算分析</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 添加验证
            $('#btnQuery')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            var gvBudget = new JustWinTable('rptBudget');
            setWidthHeight();

            // showTooltip('tooltip', 10);

            jw.tooltip();
        });
        function prjDetail(path) {
            parent.desktop.PayoutContractEdit = window;
            toolbox_oncommand(path, "三算分析明细");
        }
        function setWidthHeight() {
            $('#divBudget').width($(this).width() - 3);
            $('#divBudget').height($(this).height() - 53);
        }

    </script>
    <style type="text/css">
        .tbrep
        {
            text-align: right;
        }
        .tbrep tr td
        {
            border: solid 1px #CADEED;
            padding: 1px 4px 1px 4px;
        }
        .headerColor
        {
            color: #727faa;
            text-align: center;
        }
    </style>
</head>
<body style="overflow: hidden;">
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                <table cellpadding="3">
                    <tr>
                        <td>
                            项目编码
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrjCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            项目名称
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrjName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            计划开始日期
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartDate" flag="startDate" onclick="WdatePicker();" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            至
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndDate" flag="startDate" onclick="WdatePicker();" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div class="divFooter" style="text-align: left; padding-top: 2px;">
                    <asp:Button ID="btnQuery" Text="查 询" OnClick="btnQuery_Click" runat="server" />
                    <asp:Button ID="btnExcel" Text="导出Excel" Width="73px" OnClick="btnExcel_Click" runat="server" />
                </div>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; height: 100%; width: 100px;">
                <div id="divBudget" class="pagediv" style="margin-top: -2px; overflow: auto;">
                    <asp:Repeater ID="rptBudget" OnItemDataBound="rptBudget_ItemDataBound" runat="server">
<HeaderTemplate>
                            <table class="gvdata tbrep" id="rptBudget" cellspacing="0" rules="all" border="1"
                                style="border-collapse: collapse;">
                                <tbody>
                                    <tr class="header headerColor">
                                        <td rowspan="2">
                                            序号
                                        </td>
                                        <td rowspan="2">
                                            项目编码
                                        </td>
                                        <td rowspan="2">
                                            项目名称
                                        </td>
                                        <td rowspan="2">
                                            计划开始日期
                                        </td>
                                        <td rowspan="2">
                                            合同预算
                                        </td>
                                        <td colspan="2">
                                            目标成本
                                        </td>
                                        <td colspan="2">
                                            实际成本
                                        </td>
                                        
                                        <td rowspan="2" class="tooltip" title=" 开累计划利润 = 合同预算 &ndash; 目标成本">
                                            开累计划利润
                                        </td>
                                        <td rowspan="2" class="tooltip" title="开累实际盈亏 = 合同预算 &ndash; 实际成本">
                                            开累实际盈亏
                                        </td>
                                        <td rowspan="2" class="tooltip" title="开累项目部利润 = 目标成本 &ndash; 实际成本">
                                            开累项目部利润
                                        </td>
                                        
                                        <td rowspan="2" class="tooltip" title="完成百分比 = 实际成本 &divide; 目标成本">
                                            完成百分比
                                        </td>
                                    </tr>
                                    <tr class="header headerColor">
                                        <td>
                                            直接成本预算
                                        </td>
                                        <td>
                                            间接成本预算
                                        </td>
                                        <td>
                                            直接成本
                                        </td>
                                        <td>
                                            间接成本
                                        </td>
                                    </tr>
                        </HeaderTemplate>

<ItemTemplate>
                            <tr class="rowa" id='<%# Eval("PrjGuid") %>'>
                                <td>
                                    <%# Container.ItemIndex + 1 + (this.AspNetPager1.CurrentPageIndex - 1) * 15 %>
                                </td>
                                <td align="left">
                                    <span class="link" onclick="prjDetail('/BudgetManage/Report/BudgetDetail.aspx?prjId=<%# Eval("PrjGuid") %>&year=<%# System.Convert.ToDateTime(Eval("StartDate")).Year %>')">
                                        <%# Eval("PrjCode") %></span>
                                </td>
                                <td align="left">
                                    <span class="tooltip" title='<%# Eval("PrjName").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("PrjName").ToString(), 10) %>
                                    </span>
                                </td>
                                <td align="left">
                                    <%# Common2.GetTime(Eval("StartDate")) %>
                                </td>
                                <td class="decimal_input">
                                    <%# Eval("Tender") %>
                                </td>
                                <td class="decimal_input">
                                    <%# Eval("Target") %>
                                </td>
                                <td class="decimal_input">
                                    <%# Eval("IndirBud") %>
                                </td>
                                <td class="decimal_input">
                                    <%# Eval("Reality") %>
                                </td>
                                <td class="decimal_input">
                                    <%# Eval("IndirAcc") %>
                                </td>
                                <td class="decimal_input">
                                    <%# decimal.Parse(Eval("Tender").ToString()) - decimal.Parse(Eval("IndirBud").ToString()) - decimal.Parse(Eval("Target").ToString()) %>
                                </td>
                                <td class="decimal_input">
                                    <%# decimal.Parse(Eval("Tender").ToString()) - decimal.Parse(Eval("IndirAcc").ToString()) - decimal.Parse(Eval("Reality").ToString()) %>
                                </td>
                                <td class="decimal_input">
                                    <%# decimal.Parse(Eval("IndirBud").ToString()) + decimal.Parse(Eval("Target").ToString()) - decimal.Parse(Eval("IndirAcc").ToString()) - decimal.Parse(Eval("Reality").ToString()) %>
                                </td>
                                <td>
                                    <%# Eval("PercentCompleted").ToString() %>
                                </td>
                            </tr>
                        </ItemTemplate>

<AlternatingItemTemplate>
                            <tr class="rowb" id='<%# Eval("PrjGuid") %>'>
                                <td>
                                    <%# Container.ItemIndex + 1 + (this.AspNetPager1.CurrentPageIndex - 1) * 15 %>
                                </td>
                                <td align="left">
                                    <span class="link" onclick="prjDetail('/BudgetManage/Report/BudgetDetail.aspx?prjId=<%# Eval("PrjGuid") %>&year=<%# System.Convert.ToDateTime(Eval("StartDate")).Year %>')">
                                        <%# Eval("PrjCode") %></span>
                                </td>
                                <td align="left">
                                    <span class="tooltip" title='<%# Eval("PrjName").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("PrjName").ToString(), 10) %>
                                    </span>
                                </td>
                                <td align="left">
                                    <%# Common2.GetTime(Eval("StartDate")) %>
                                </td>
                                <td class="decimal_input">
                                    <%# Eval("Tender") %>
                                </td>
                                <td class="decimal_input">
                                    <%# Eval("Target") %>
                                </td>
                                <td class="decimal_input">
                                    <%# Eval("IndirBud") %>
                                </td>
                                <td class="decimal_input">
                                    <%# Eval("Reality") %>
                                </td>
                                <td class="decimal_input">
                                    <%# Eval("IndirAcc") %>
                                </td>
                                <td class="decimal_input">
                                    <%# decimal.Parse(Eval("Tender").ToString()) - decimal.Parse(Eval("IndirBud").ToString()) - decimal.Parse(Eval("Target").ToString()) %>
                                </td>
                                <td class="decimal_input">
                                    <%# decimal.Parse(Eval("Tender").ToString()) - decimal.Parse(Eval("IndirAcc").ToString()) - decimal.Parse(Eval("Reality").ToString()) %>
                                </td>
                                <td class="decimal_input">
                                    <%# decimal.Parse(Eval("IndirBud").ToString()) + decimal.Parse(Eval("Target").ToString()) - decimal.Parse(Eval("IndirAcc").ToString()) - decimal.Parse(Eval("Reality").ToString()) %>
                                </td>
                                <td>
                                    <%# Eval("PercentCompleted").ToString() %>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>

<FooterTemplate>
                            <tr>
                                <td colspan="4" align="center">
                                    合计
                                </td>
                                <td>
                                    <asp:Label ID="lblSumTender" Text="aa" CssClass="decimal_input" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblSumTarget" CssClass="decimal_input" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblSumIndirBud" CssClass="decimal_input" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblSumReality" CssClass="decimal_input" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblSumIndirAcc" CssClass="decimal_input" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblSumkljhlr" CssClass="decimal_input" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblSumklsjyk" CssClass="decimal_input" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblSumklxmlr" CssClass="decimal_input" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPercentCompleted" runat="server"></asp:Label>
                                </td>
                            </tr>
                            </tbody> </table>
                        </FooterTemplate>
</asp:Repeater>
                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                    </webdiyer:AspNetPager>
                </div>
            </td>
        </tr>
    </table>
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    </form>
</body>
</html>
