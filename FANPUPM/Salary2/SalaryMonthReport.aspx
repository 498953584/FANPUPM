<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryMonthReport.aspx.cs" Inherits="Salary2_SalaryMonthReport" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>工资管理</title>
    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../Script/wf.js"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript" src="../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvwSaMonth');
            replaceEmptyTable('EmptySaMonth', 'gvwSaMonth');
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divContent2" style="height: 100%; width: 100%;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="height: 96%; width: 100%;">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                                    <tr>
                                        <td class="descTd">
                                            员工编号
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUserCode" Width="120px" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="descTd">
                                            员工姓名
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUserName" Width="120px" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="descTd">
                                            是否发放
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlIsPayoff" Width="120px" runat="server"><asp:ListItem Value="" /><asp:ListItem Value="1" Text="是" /><asp:ListItem Value="2" Text="否" /></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="descTd">
                                            年份
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlYear" Width="120px" runat="server"></asp:DropDownList>
                                        </td>
                                        <td class="descTd">
                                            月份
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlMonth" Width="120px" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="divFooter" style="text-align: left;">
                                <asp:Button ID="btnSelect" Text="查询" OnClick="btnSelect_Click" runat="server" />
                                <asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="pagediv">
                                    <asp:GridView ID="gvwSaMonth" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="gvwSaMonth_RowDataBound" DataKeyNames="v_yhdm" runat="server">
<EmptyDataTemplate>
                                            <table class="grid" cellspacing="0" rules="all" border="1" id="EmptySaMonth" style="border-collapse: collapse;">
                                                <tr class="header">
                                                    <th scope="col" style="width: 20px">
                                                        序号
                                                    </th>
                                                    <th scope="col" style="width: 80px">
                                                        员工编号
                                                    </th>
                                                    <th scope="col">
                                                        员工姓名
                                                    </th>
                                                    <th scope="col">
                                                        发放标志
                                                    </th>
                                                    <th scope="col">
                                                        工资明细
                                                    </th>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                    <%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="员工编号">
<ItemTemplate>
                                                    <%# Eval("UserCode") %>
                                                    <asp:HiddenField ID="hfldUserCode" Value='<%# System.Convert.ToString(Eval("v_yhdm"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="员工姓名">
<ItemTemplate>
                                                    <%# Eval("v_xm") %>
                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="是否发放">
<ItemTemplate>
                                                    <%# Eval("IsPayoffName") %>
                                                </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" ShowPageIndexBox="Always" NumericButtonCount="8" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                    </webdiyer:AspNetPager>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfldDepartment" runat="server" />
    <asp:HiddenField ID="hfldYear" runat="server" />
    <asp:HiddenField ID="hfldMonth" runat="server" />
    </form>
</body>
</html>
