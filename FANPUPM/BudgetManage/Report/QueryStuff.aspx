<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryStuff.aspx.cs" Inherits="BudgetManage_Report_QueryStuff" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>查看物资</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 添加验证
            $('#btn_Search')[0].onclick = function () {
                if (!$('#form1').form('validate')) {
                    return false;
                }
            }
            setWidthHeight();
            replaceEmptyTable('emptyStuff', 'gvStuff');
            jw.tooltip();
        });
        function setWidthHeight() {
            $('#divBudget').height($(this).height() - 70);
            $('#divBudget').width($(this).width() - 2);
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
                                单位
                            </td>
                            <td>
                                <asp:TextBox ID="txtUnitName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
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
                                            单位
                                        </th>
                                        <th colspan="3">
                                            材料数量
                                        </th>
                                        <th colspan="3">
                                            材料价格
                                        </th>
                                        <th colspan="3">
                                            材料价格
                                        </th>
                                    </tr>
                                    <tr class="header">
                                        <th>
                                            目标数量
                                        </th>
                                        <th>
                                            报量数量
                                        </th>
                                        <th class="tooltip" title=" 实际数量 = 出库数量 &ndash; 退库数量" >
                                            实际数量
                                        </th>
                                        <th>
                                            目标价格
                                        </th>
                                        <th>
                                            报量价格
                                        </th>
                                        <th>
                                            实际价格
                                        </th>
                                        <th>
                                            目标金额
                                        </th>
                                        <th>
                                            报量金额
                                        </th>
                                        <th class="tooltip" title=" 实际数量 = 出库中成本归集于材料的资源金额 &ndash; 退库中成本归集于材料的资源金额 " >
                                            实际金额
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
                                        <%# Eval("UnitName") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("BudQuantity")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("ConsQuantity")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("RealityNumber")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("BudPrice")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("ConsPrice")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("RealityPrice")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("BudTotal")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("ConsTotal")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("RealityTotal")).ToString("0.000") %>
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
