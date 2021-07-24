<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResPriceDifference.aspx.cs" Inherits="BudgetManage_Report_ResPriceDifference" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link type="text/css" rel="stylesheet" href="../../../Script/jquery.easyui/themes/default/easyui.css" />
<link type="text/css" rel="stylesheet" href="../../../Script/jquery.easyui/themes/icon.css" />
<link type="text/css" rel="stylesheet" href="../../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../../Script/jw.js"></script>
    <script type="text/javascript" src="../../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvwResPriceDiff');
            replaceEmptyTable('emptyResPriceDiff', 'gvwResPriceDiff');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="divFooter" style="text-align: left;">
                    <asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;">
                    <div class="pagediv" style="width: 100%">
                        <asp:GridView ID="gvwResPriceDiff" AutoGenerateColumns="false" ShowFooter="false" CssClass="gvdata" Width="100%" OnRowDataBound="gvwResPriceDiff_RowDataBound" runat="server">
<EmptyDataTemplate>
                                <table id="emptyResPriceDiff">
                                    <tr class="header">
                                        <th style="width: 20px">
                                            序号
                                        </th>
                                        <th>
                                            资源编号
                                        </th>
                                        <th>
                                            资源名称
                                        </th>
                                        <th>
                                            单位
                                        </th>
                                        <th>
                                            规格
                                        </th>
                                        <th>
                                            数量
                                        </th>
                                        <th>
                                            单价
                                        </th>
                                        <th>
                                            预算价
                                        </th>
                                        <th>
                                            价差
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" ItemStyle-Width="20px"><ItemTemplate>
                                        <%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源编号" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                        <%# Eval("ResourceCode") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源名称" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                        <%# Eval("ResourceName") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                        <%# Eval("UnIt") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                        <%# Eval("Specification") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                        <%# Eval("ResourceQuantity") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单价" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                        <%# Eval("ResourcePrice") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="预算价" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                        <%# Eval("BudGetPrice") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="价差" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                        <%# Eval("PriceDiff") %>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                        <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                        </webdiyer:AspNetPager>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
