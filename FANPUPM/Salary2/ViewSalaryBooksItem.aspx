<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewSalaryBooksItem.aspx.cs" Inherits="Salary2_ViewSalaryBooksItem" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>查看帐套明细</title><link type="text/css" rel="Stylesheet" href="../Script/jquery.tooltip/jquery.tooltip.css" />
<link type="text/css" rel="Stylesheet" href="../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript" src="../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvSalaryBooksItem');
            replaceEmptyTable('emptySalaryBooksItem', 'gvSalaryBooksItem'); 	// 数据为空时替换表格
            showTooltip('tooltip', 25);                         // 提示
        });
    </script>
    <style type="text/css">
        .descTd
        {
            width: 60px;
        }
        .descTd span
        {
            margin: 0px;
            color: Red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%; height: 100%;">
            <tr>
                <td>
                    <asp:GridView ID="gvSalaryBooksItem" CssClass="gvdata" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvSalaryBooksItem_RowDataBound" DataKeyNames="Id,No" runat="server">
<EmptyDataTemplate>
                            <table class="gvdata" id="emptySalaryBooksItem" cellspacing="0" rules="all" border="1"
                                style="width: 100%; border-collapse: collapse;">
                                <tr class="header">
                                    <td scope="col" style="width: 25px;">
                                        序号
                                    </td>
                                    <td scope="col">
                                        名称
                                    </td>
                                    <td scope="col" align="center" style="width: 80px;">
                                        默认值
                                    </td>
                                    <td scope="col" align="center" style="width: 50px;">
                                        是否公式
                                    </td>
                                    <td scope="col" align="center" style="width: 50px;">
                                        是否显示
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Right"><HeaderTemplate>
                                    序号
                                </HeaderTemplate><ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="150px"><HeaderTemplate>
                                    名称
                                </HeaderTemplate><ItemTemplate>
                                    <span class="tooltip" title="<%# GetSaItemName(Eval("ItemId").ToString()) %>">
                                        <%# StringUtility.GetStr(base.GetSaItemName(Eval("ItemId").ToString()), 25) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="默认值" ItemStyle-CssClass="decimal_input" HeaderStyle-Width="80px">
<ItemTemplate>
                                    <%# Eval("DefaultValue") %>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField ItemStyle-HorizontalAlign="Left"><HeaderTemplate>
                                    公式
                                </HeaderTemplate><ItemTemplate>
                                <span class="tooltip" title="<%# ConvertFormula((Eval("Formula") == null) ? "" : Eval("Formula").ToString()) %>">
                                    <%# StringUtility.GetStr(base.ConvertFormula((Eval("Formula") == null) ? "" : Eval("Formula").ToString()), 25) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
                                    是否显示
                                </HeaderTemplate><ItemTemplate>
                                    <%# ((bool)Eval("Isshow")) ? "是" : "否" %>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <link type="text/css" rel="Stylesheet" href="../Script/jquery.easyui/themes/default/easyui.css" />
    <link type="text/css" rel="Stylesheet" href="../Script/jquery.easyui/themes/icon.css" />
    </form>
</body>
</html>
