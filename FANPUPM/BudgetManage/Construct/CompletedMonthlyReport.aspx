<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompletedMonthlyReport.aspx.cs" Inherits="BudgetManage_Construct_CompletedMonthlyReport" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var gvConstruct = new JustWinTable('gvConstruct');
            showTooltip('tooltip', 25);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
        <table border="0" style="width: 100%; height: 95%;">
            <tr>
                <td class="divFooter" style="width: 150px; text-align: left;">
                    <asp:Button ID="btnExcel" Text="导出Excel" Width="73px" OnClick="btnExcel_Click" runat="server" />
                </td>
                <td class="divFooter" style="text-align: center;">
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="" runat="server"></asp:Label>
                </td>
                <td class="divFooter" style="width: 150px;">
                </td>
            </tr>
            <tr>
                <td style="height: 100%; vertical-align: top;" colspan="3">
                    <div id="divBudget" class="pagediv">
                        <asp:GridView ID="gvConstruct" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvConstruct_RowDataBound" DataKeyNames="TaskId" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                        <%# Eval("Num") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目编码" HeaderStyle-Width="90px"><ItemTemplate>
                                        <%# Eval("TaskCode") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="50px">
<ItemTemplate>
                                        <asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(Eval("TaskName"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(Eval("TaskName"), 25) %></asp:HyperLink>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="计量单位" HeaderStyle-Width="50px"><ItemTemplate>
                                        <%# Eval("Unit") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程量" HeaderStyle-Width="50px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# Eval("Quantity") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="综合单价" HeaderStyle-Width="50px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# Eval("UnitPrice") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合计" HeaderStyle-Width="50px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# Eval("Sum") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="数量" HeaderStyle-Width="50px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# Eval("preQuantity") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="金额" HeaderStyle-Width="50px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# Eval("preSum") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="数量" HeaderStyle-Width="50px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# Eval("curQuantity") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="金额" HeaderStyle-Width="50px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# Eval("curSum") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="数量" HeaderStyle-Width="50px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# Eval("curEndQuantity") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="金额" HeaderStyle-Width="50px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# Eval("curEndSum") %>
                                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    </form>
</body>
</html>
