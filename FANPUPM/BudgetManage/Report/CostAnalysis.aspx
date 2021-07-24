<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostAnalysis.aspx.cs" Inherits="BudgetManage_Report_CostAnalysis" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>项目成本分析</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.core.js"></script>

    <script type="text/javascript" src="../../Script/jquery.cookie.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.dialog.js"></script>

    <script type="text/javascript">
        addEvent(window, 'load', function() {
            var gvBudget = new JustWinTable('gvBudget');
        });
        //    function openQuery() {
        //        var url = "QueryCostAnalysis.aspx";
        //        return window.showModalDialog(url, window, "dialogHeight:266px;dialogWidth:430px;center:1;help:0;status:0;");
        //    }
        //ShowDialog
        function openWindow() {
            var src = 'QueryCostAnalysis.aspx';
            $('#iframeMonthBudgets').attr('src', src);
            $('#divMonthBudgets').dialog({ width: 433, height: 266, modal: true });
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                <div class="divFooter" style="text-align: right;">
                    <input type="button" id="btnAdd" value="高级查询" style="width: 73px;" onclick="openWindow()" />
                    <asp:Button ID="btnExcel" Text="导出Excel" Width="73px" OnClick="btnExcel_Click" runat="server" />
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; font-weight: bold; font-size: 18px;">
                项目成本报表
            </td>
        </tr>
        <tr>
            <td style="font-weight: bold; font-size: 15px;">
                <div style="float: left;">
                    制表人:<asp:Label ID="lblTabulator" Text="管理员" runat="server"></asp:Label></div>
                <div style="float: right;">
                    日期:<asp:Label ID="lblDate" runat="server"></asp:Label></div>
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <asp:GridView ID="gvBudget" Style="width: 100%; margin-top: 20px;" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvBudget_RowDataBound" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                    <%# (Container.DataItemIndex + 1 + (this.AspNetPager1.CurrentPageIndex - 1) * 15).ToString() %>
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="项目编号" DataField="PrjCode" /><asp:BoundField HeaderText="项目名称" DataField="PrjName" /><asp:BoundField HeaderText="合同金额" DataField="Contract" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="预算人工费" DataField="BudgetRen" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="预算材料费" DataField="BudgetCai" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="预算机械费" DataField="BudgetJi" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="预算间接成本" DataField="Budget" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="实际人工费" DataField="AccountRen" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="实际材料费" DataField="AccountCai" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="实际机械费" DataField="AccountJi" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="实际间接成本" DataField="Account" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" /></Columns><HeaderStyle CssClass="header"></HeaderStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><RowStyle CssClass="rowa"></RowStyle></asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="true" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                    </webdiyer:AspNetPager>
                </div>
            </td>
        </tr>
    </table>
    <div id="divMonthBudgets" title="高级查询" style="display: none">
        <iframe id="iframeMonthBudgets" frameborder="0" width="100%" height="100%"></iframe>
    </div>
    <asp:HiddenField ID="hfldProName" runat="server" />
    <asp:HiddenField ID="hfldProNo" runat="server" />
    </form>
</body>
</html>
