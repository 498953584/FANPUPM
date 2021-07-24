<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndirectBudgetQuery.aspx.cs" Inherits="BudgetManage_Cost_IndirectBudgetQuery" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>已上报间接成本预算</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <script type="text/javascript">
          addEvent(window, 'load', function() {
          var gvBudget = new JustWinTable('gvBudget');
          });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="divFooter" style="padding-top: 8px;">
        <asp:Label Font-Bold="true" ID="lblPoster" Text="已上报的间接成本预算" runat="server"></asp:Label>
    </div>
    <div>
        <asp:GridView ID="gvBudget" CssClass="gvdata" AutoGenerateColumns="false" runat="server"><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                        <%# (Container.DataItemIndex + 1).ToString() %>
                    </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="CBS编码" DataField="CBSCode" HeaderStyle-Width="100px" /><asp:TemplateField HeaderText="名称"><ItemTemplate>
                        <%# Eval("CostAcc.Name") %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="上报状态">
<ItemTemplate>
                        <%# Common2.GetIndirectState(Eval("State").ToString()) %>
                    </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="预算金额" DataField="BudgetAmount" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
    </div>
    </form>
</body>
</html>
