<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EvenAnalysis.aspx.cs" Inherits="BudgetManage_Report_EvenAnalysis" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>盈亏分析</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />
<link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script src="/Script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Script/Budget/BudgetPait.js"></script>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 添加验证
            $('#btn_Search')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            setWidthHeight();
            var table = new JustWinTable('gvCost');
            replaceEmptyTable('empetyFillTable', 'gvCost');
            //showTooltip('tooltip', 25);
            jw.tooltip();
        });
        function setWidthHeight() {
            $('#divBudget').height($(this).height() - 70);
            $('#divBudget').width($('#divBudget').width() - 2);
        }
        function viewopen(prjId) {
            parent.desktop.EvenAnalysisDetail = window;
            var url = "/BudgetManage/Report/EvenAnalysisDetail.aspx?prjId=" + prjId;
            toolbox_oncommand(url, "盈亏明细表");
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
                        <asp:GridView ID="gvCost" AutoGenerateColumns="false" DataKeyNames="" CssClass="gvdata" Width="100%" ShowFooter="true" OnRowDataBound="gvCost_RowDataBound" runat="server">
<EmptyDataTemplate>
                                <table id="empetyFillTable">
                                    <tr class="header">
                                        <th rowspan="3">
                                            序号
                                        </th>
                                        <th rowspan="3">
                                            项目编码
                                        </th>
                                        <th rowspan="3">
                                            项目名称
                                        </th>
                                        <th colspan="5">
                                            累计数
                                        </th>
                                    </tr>
                                    <tr class="header">
                                        <th rowspan="2">
                                            合同预算
                                        </th>
                                        <th colspan="2">
                                            实际成本
                                        </th>
                                        <th rowspan="2">
                                            盈亏
                                        </th>
                                        <th rowspan="2">
                                            盈亏率
                                        </th>
                                    </tr>
                                    <tr class="header">
                                        <th>
                                            直接成本
                                        </th>
                                        <th>
                                            间接成本
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
                                            <%# StringUtility.GetStr(Eval("PrjName").ToString(), 25) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("ContractBud")).ToString("0.000") %>
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
                                        <%# System.Convert.ToDecimal(Eval("GainLoss")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<ItemTemplate>
                                        <%# Eval("RatioGainLoss") %>
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
