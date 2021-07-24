<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProfitStatistics.aspx.cs" Inherits="BudgetManage_Report_ProfitStatistics" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>统计利润</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 添加验证
            $('#btn_Search')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            setWidthHeight();
            replaceEmptyTable('emptyProfit', 'gvProfit');
            jw.tooltip();
        });
        function setWidthHeight() {
            $('#divBudget').height($(this).height() - 70);
            $('#divBudget').width($(this).width() - 2);
        }
        function viewopen(prjId) {
            parent.desktop.QueryStuff = window;
            var url = "/BudgetManage/Report/QueryStuff.aspx?prjId=" + prjId;
            toolbox_oncommand(url, "材料明细");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
        <table style="border: 0px; width: 100%; height: 100%;">
            <tr>
                <td>
                    <table class="queryTable" cellpadding="3px" cellspacing="0px">
                        <tr>
                            <td class="descTd">
                                项目编码
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrjCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                项目名称
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrjName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                项目状态
                            </td>
                            <td>
                                <asp:DropDownList ID="dropPrjState" Width="120px" runat="server"></asp:DropDownList>
                            </td>
                            <td class="descTd">
                                项目申请人
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrjPeopleName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                开始时间
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrjStartDate" onclick="WdatePicker()" Width="120px" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                结束时间
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrjEndDate" onclick="WdatePicker()" Width="120px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="divFooter" style="text-align: left;">
                    <asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
                    <asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td style="width: 100%; height: 90%; padding: 0px;">
                    <div id="divBudget" class="pagediv" style="border-bottom: solid 0px red; margin-top: 0px;">
                        <asp:GridView ID="gvProfit" AutoGenerateColumns="false" DataKeyNames="" CssClass="gvdata" Width="100%" ShowFooter="true" OnRowDataBound="gvProfit_RowDataBound" runat="server">
<EmptyDataTemplate>
                                <table id="emptyProfit">
                                    <tr class="header">
                                        <th rowspan="2">
                                            序号
                                        </th>
                                        <th rowspan="2">
                                            项目编号
                                        </th>
                                        <th rowspan="2">
                                            项目名称
                                        </th>
                                        <th rowspan="2">
                                            开始日期
                                        </th>
                                        <th rowspan="2">
                                            结束日期
                                        </th>
                                        <th rowspan="2">
                                            项目状态
                                        </th>
                                        <th rowspan="2">
                                            项目申请人
                                        </th>
                                        <th rowspan="2">
                                            投标预算
                                        </th>
                                        <th rowspan="2">
                                            目标成本
                                        </th>
                                        <th rowspan="2">
                                            报量成本
                                        </th>
                                        <th rowspan="2">
                                            间接费用
                                        </th>
                                        <th colspan="6">
                                            直接费用
                                        </th>
                                        <th colspan="2">
                                            利润
                                        </th>
                                    </tr>
                                    <tr class="header">
                                        <th class="tooltip" title=" 直接材料费 = 出库物资金额 &ndash; 退库物资金额" >
                                            直接材料费
                                        </th>
                                        <th class="tooltip" title=" 公司项目人员费用 = 合同类型中成本归集于人工的合同金额" >
                                            公司项目<br />
                                            人员费用
                                        </th>
                                        <th class="tooltip" title=" 公司项目机械设备费用 = 合同类型中成本归集于机械的合同金额" >
                                            公司项目<br />
                                            机械设备费用
                                        </th>
                                        <th class="tooltip" title=" 外包费用 = 合同类型中成本归集于外包费用的合同金额" >
                                            外包费用
                                        </th>
                                        <th class="tooltip" title=" 其他 = 合同类型中成本归集于其他的合同金额" >
                                            其他
                                        </th>
                                        <th class="tooltip" title=" 实际直接费用 = 直接材料费 + 公司项目人员费用 + 公司项目机械设备费用 + 外包费用 + 其他 " >
                                            实际直接成本
                                        </th>
                                        <th class="tooltip" title=" 目标利润 = 投标预算金额 &ndash; 目标成本 " >
                                            目标利润
                                        </th>
                                        <th class="tooltip" title=" 实际利润 = 投标预算金额 &ndash; 实际直接成本 &ndash; 间接费用 " >
                                            实际利润
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<ItemTemplate>
                                        <%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<ItemTemplate>
                                        <span class="al" onclick="viewopen('<%# Eval("prjGuid") %>');">
                                            <%# Eval("prjCode") %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("PrjName").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("PrjName").ToString(), 15) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<ItemTemplate>
                                        <%# string.Format("{0:d}", Eval("StartDate")) %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<ItemTemplate>
                                        <%# string.Format("{0:d}", Eval("EndDate")) %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<ItemTemplate>
                                        <%# Eval("PrjStateName") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<ItemTemplate>
                                        <%# Eval("ProjPeopleName") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("ContractCost")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("BudCost")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("ConsTotal")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("StuffCost")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("LaborCost")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("MachineCost")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("OutSourceCost")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("ElseCBSCost")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("DirectCost")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("IndirectCost")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("TargetProfit")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("RealityProfit")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                        <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                        </webdiyer:AspNetPager>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    </form>
</body>
</html>
