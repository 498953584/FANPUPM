<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RptDatePrjMilestoneQuery.aspx.cs" Inherits="PrjManager_RptDatePrjMilestoneQuery" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/ecmascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript" src="../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../Script/jwJson.js"></script>
    <script type="text/javascript" src="../Script/wf.js"></script>
    <script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var justTable = new JustWinTable('RptDatePrjMilestoneQuery');
            replaceEmptyTable('emptyTable', 'RptDatePrjMilestoneQuery');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="vertical-align: top;">
                    <table class="queryTable" cellpadding="3px" cellspacing="0px">
                        <tr>
                            <td class="descTd" style="white-space: nowrap;">
                                上报时间
                            </td>
                            <td>
                                <asp:TextBox ID="txtRptDateStart" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd" style="white-space: nowrap;">
                                至
                            </td>
                            <td>
                                <asp:TextBox ID="txtRptDateEnd" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    <table class="tab" style="vertical-align: top;">
                        <tr>
                            <td class="divFooter" style="text-align: left">
                                <asp:Button ID="brnQuery" Text="查询" OnClick="brnQuery_Click" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <asp:GridView ID="RptDatePrjMilestoneQuery" CssClass="gvdata" AutoGenerateColumns="false" ShowFooter="true" runat="server">
<EmptyDataTemplate>
                                            <table id="emptyTable">
                                                <tr class="header">
                                                    <th scope="col">
                                                        序号
                                                    </th>
                                                    <th scope="col">
                                                        上报时间
                                                    </th>
                                                    <th scope="col">
                                                        当年目标额
                                                    </th>
                                                    <th scope="col">
                                                        项目储备额
                                                    </th>
                                                    <th scope="col">
                                                        当年预测
                                                    </th>
                                                    <th scope="col">
                                                        项目储备转换率
                                                    </th>
                                                    <th scope="col">
                                                        明年预测
                                                    </th>
                                                    <th scope="col">
                                                        （1）初步接洽
                                                    </th>
                                                    <th scope="col">
                                                        （2）提供样品
                                                    </th>
                                                    <th scope="col">
                                                        （3）样品质量被接纳
                                                    </th>
                                                    <th scope="col">
                                                        （4）投标
                                                    </th>
                                                    <th scope="col">
                                                        （5）中标
                                                    </th>
                                                    <th scope="col">
                                                        （6）下订单
                                                    </th>
                                                    <th scope="col">
                                                        （7）交货
                                                    </th>
                                                    <th scope="col">
                                                        （8）销售确认
                                                    </th>
                                                    <th scope="col">
                                                        （9）项目失败
                                                    </th>
                                                    <th scope="col">
                                                        工程总数
                                                    </th>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="20px">
<ItemTemplate>
                                                    <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上报时间"><ItemTemplate>
                                                    <%# Common2.GetTime(Eval("RptDate")) %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目储备额"><ItemTemplate>
                                                    <%# Eval("StoreAmount") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="当年预测"><ItemTemplate>
                                                    <%# Eval("ForeCast") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="储备转换率"><ItemTemplate>
                                                    <%# FormatRate(Eval("ForeCast"), Eval("StoreAmount")) %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="明年预测"><ItemTemplate>
                                                    <%# Eval("NextForeCast") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="（1）初步接洽"><ItemTemplate>
                                                    <%# Eval("Stone1") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="（2）提供样品"><ItemTemplate>
                                                    <%# Eval("Stone2") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="（3）样品质量被接纳"><ItemTemplate>
                                                    <%# Eval("Stone3") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="（4）投标"><ItemTemplate>
                                                    <%# Eval("Stone4") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="（5）中标"><ItemTemplate>
                                                    <%# Eval("Stone5") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="（6）下订单"><ItemTemplate>
                                                    <%# Eval("Stone6") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="（7）交货"><ItemTemplate>
                                                    <%# Eval("Stone7") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="（8）销售确认"><ItemTemplate>
                                                    <%# Eval("Stone8") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="（9）项目失败"><ItemTemplate>
                                                    <%# Eval("Stone9") %>
                                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                </webdiyer:AspNetPager>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
