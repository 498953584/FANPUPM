<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostSummarylist.aspx.cs" Inherits="BudgetManage_Report_CostSummarylist" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>直接目标成本总表</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />
<link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>

    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script src="/Script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../Script/Budget/BudgetPait.js"></script>

    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>


    <style type="text/css">
        .style1
        {
            width: 80px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
        <table style="border: 0px; width: 100%; height: 100%;">
            <tr style="width: 100%; height: 20px;">
              <td style="text-align: left;" class="divFooter">
                    工程名称：
                    <asp:DropDownList ID="ddlPrject" AutoPostBack="true" OnSelectedIndexChanged="ddlPrject_SelectedIndexChanged" runat="server"></asp:DropDownList>
                </td>
                <td style="text-align: right;" class="divFooter" colspan="2">
                    <asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
                    <asp:Button Width="80px" ID="btnWord" Text="导出Word" OnClick="btnWord_Click" runat="server" />
                </td>
            </tr>
            <tr style="width: 100%;">
                <td class="resTotal">
                    工程名称:
                    <asp:Label Font-Bold="false" ID="lblPrjName" Text="" runat="server"></asp:Label>
                </td>
                <td class="resTotal">
                    项目经理:
                    <asp:Label Font-Bold="false" ID="lblPrjManager" Text="" runat="server"></asp:Label>
                </td>
                <td class="resTotal">
                    单位: 元
                </td>
            </tr>
            <tr>
                <td colspan="3" style="width: 100%; height: 90%;">
                   <div id="divBudget" class="pagediv" style="border-bottom: solid 0px red;">
                        <asp:GridView ID="gvCost" AutoGenerateColumns="false" DataKeyNames="" CssClass="gvdata" AllowPaging="true" OnPageIndexChanging="gvCost_PageIndexChanging" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
                                        序号
                                    </HeaderTemplate>

<ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
                                        项目
                                    </HeaderTemplate>

<ItemTemplate>
                                        <%# Eval("Name") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<HeaderTemplate>
                                        目标成本
                                    </HeaderTemplate>

<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("BudAmount")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<HeaderTemplate>
                                        实际发生的成本
                                    </HeaderTemplate>

<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("Cost")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<HeaderTemplate>
                                        差额
                                    </HeaderTemplate>

<ItemTemplate>
                                        <%# System.Convert.ToDecimal(Eval("Disparity")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
