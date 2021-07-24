<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StuffSummarizing.aspx.cs" Inherits="StockManage_Report_StuffSummarizing" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>材料明细</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 添加验证
            $('#btn_Search')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            replaceEmptyTable('emptyStuff', 'gvStuff');
            setWidthHeight();
            showTooltip('tooltip', 10);
        });
        function setWidthHeight() {
            $('#divBudget').height($(this).height() - 61);
            $('#div_project').height($(this).height() - 20);
            $('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 8);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader" style="display: none;">
        <table class="tableHeader">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="主材差异分析表" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
        <table>
            <tr>
                <td style="width: 100%; height: 100%;">
                    <table style="width: 100%; height: 100%;">
                        <tr>
                            <td id="td_Left" style="width: 194px; vertical-align: top; height: 100%;">
                                <div>
                                    <asp:DropDownList ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                </div>
                                <div id="div_project" class="pagediv" style="width: 194px; height: 100%;">
                                    <asp:TreeView ID="tvProject" Font-Size="12px" ShowLines="true" OnSelectedNodeChanged="tvProject_SelectedNodeChanged" runat="server"><SelectedNodeStyle BackColor="#3399FF" ForeColor="White" Height="9px" /></asp:TreeView>
                                </div>
                            </td>
                            <td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
                                <table style="border: 0px; width: 100%; height: 100%;">
                                    <tr>
                                        <td>
                                            <table class="queryTable" cellpadding="3px" cellspacing="0px">
                                                <tr>
                                                    <td class="descTd">
                                                        材料编号
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="descTd">
                                                        材料名称
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="descTd">
                                                        规格
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSpecification" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="descTd">
                                                        品牌
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBrand" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="descTd">
                                                        型号
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtModelNumber" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
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
                                    <tr>
                                        <td style="width: 100%; height: 90%;">
                                            <div id="divBudget" class="pagediv" style="border-bottom: solid 0px red; margin-top: 0px;">
                                                <asp:GridView ID="gvStuff" AutoGenerateColumns="false" DataKeyNames="" CssClass="gvdata" Width="100%" ShowFooter="true" OnRowDataBound="gvStuff_RowDataBound" runat="server">
<EmptyDataTemplate>
                                                        <table id="emptyStuff">
                                                            <tr class="header">
                                                                <th rowspan="2">
                                                                    序号
                                                                </th>
                                                                <th rowspan="2">
                                                                    材料编号
                                                                </th>
                                                                <th rowspan="2">
                                                                    材料名称
                                                                </th>
                                                                <th rowspan="2">
                                                                    规格
                                                                </th>
                                                                <th rowspan="2">
                                                                    品牌
                                                                </th>
                                                                <th rowspan="2">
                                                                    型号
                                                                </th>
                                                                <th colspan="3">
                                                                    目标成本
                                                                </th>
                                                                <th colspan="5">
                                                                    在途成本
                                                                </th>
                                                                <th colspan="3">
                                                                    实际成本
                                                                </th>
                                                                <th colspan="2">
                                                                    盈亏
                                                                </th>
                                                                <th colspan="2">
                                                                    结存
                                                                </th>
                                                            </tr>
                                                            <tr class="header">
                                                                <th>
                                                                    目标数量
                                                                </th>
                                                                <th>
                                                                    目标价格
                                                                </th>
                                                                <th>
                                                                    目标总金额
                                                                </th>
                                                                <th>
                                                                    采购数量累计
                                                                </th>
                                                                <th>
                                                                    采购价格
                                                                </th>
                                                                <th>
                                                                    采购金额累计
                                                                </th>
                                                                <th>
                                                                    入库数量累计
                                                                </th>
                                                                <th>
                                                                    入库金额累计
                                                                </th>
                                                                <th class="tooltip" title=" 实际数量 = 出库数量 &ndash; 退库数量" >
                                                                    实际数量
                                                                </th>
                                                                <th class="tooltip" title=" 实际价格 = 编制采购单时的价格 " >
                                                                    实际价格
                                                                </th>
                                                                <th class="tooltip" title=" 实际数量 = 出库中成本归集于材料的物资金额 &ndash; 退库中成本归集于材料的物资金额" >
                                                                    实际金额
                                                                </th>
                                                                <th class="tooltip" title=" 数量 = 目标数量 &ndash; 采购数量" >
                                                                    数量
                                                                </th>
                                                                <th class="tooltip" title=" 金额 = 目标金额 &ndash; 采购金额 " >
                                                                    金额
                                                                </th>
                                                                <th class="tooltip" title=" 数量 = 采购数量 &ndash; 出库数量 " >
                                                                    数量
                                                                </th>
                                                                <th class="tooltip" title=" 金额 = 采购金额 &ndash;  出库金额 " >
                                                                    金额
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
                                                                <%# Eval("ResourceCode") %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<ItemTemplate>
                                                                <span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
                                                                    <%# StringUtility.GetStr(Eval("ResourceName").ToString(), 15) %>
                                                                </span>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<ItemTemplate>
                                                                <%# Eval("Specification") %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<ItemTemplate>
                                                                <%# Eval("Brand") %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<ItemTemplate>
                                                                <%# Eval("ModelNumber") %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                                <%# System.Convert.ToDecimal(Eval("BudQuantity")).ToString("0.000") %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                                <%# System.Convert.ToDecimal(Eval("BudPrice")).ToString("0.000") %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                                <%# System.Convert.ToDecimal(Eval("BudTotal")).ToString("0.000") %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                                <%# System.Convert.ToDecimal(Eval("PurchaseNumber")).ToString("0.000") %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                                <%# System.Convert.ToDecimal(Eval("PurchaseSprice")).ToString("0.000") %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                                <%# System.Convert.ToDecimal(Eval("PurchaseCost")).ToString("0.000") %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                                <%# System.Convert.ToDecimal(Eval("StorageNumber")).ToString("0.000") %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                                <%# System.Convert.ToDecimal(Eval("StorageCost")).ToString("0.000") %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                                <%# System.Convert.ToDecimal(Eval("RealityNumber")).ToString("0.000") %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                                <%# System.Convert.ToDecimal(Eval("RealityPrice")).ToString("0.000") %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                                <%# System.Convert.ToDecimal(Eval("RealityTotal")).ToString("0.000") %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                                <%# System.Convert.ToDecimal(Eval("ProfitLossNumber")).ToString("0.000") %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                                <%# System.Convert.ToDecimal(Eval("ProfitLossCost")).ToString("0.000") %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                                <%# System.Convert.ToDecimal(Eval("BalanceNumber")).ToString("0.000") %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                                <%# System.Convert.ToDecimal(Eval("BalanceCost")).ToString("0.000") %>
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
                </td>
            </tr>
        </table>
    </div>
    
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    <asp:HiddenField ID="hfldYear" runat="server" />
    <asp:HiddenField ID="hfldIsLocked" runat="server" />
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    </form>
</body>
</html>
