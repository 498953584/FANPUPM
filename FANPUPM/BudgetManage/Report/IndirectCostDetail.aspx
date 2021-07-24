<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndirectCostDetail.aspx.cs" Inherits="BudgetManage_Report_IndirectCostDetail" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            // 添加验证
            $('#btnSearch')[0].onclick = function () {
                if (!$('#form1').form('validate')) {
                    return false;
                }
            }
            showTooltip('tooltip', 10);
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd">
							&nbsp;名称:
						</td>
						<td>
							<asp:TextBox ID="txtDiaryName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
						</td>
                        <td class="descTd">
							&nbsp;经手人:
						</td>
						<td>
							<asp:TextBox ID="txtIssuedBy" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
						</td>
                        <td>
                            &nbsp;发生日期:
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartDate" flag="startDate" onclick="WdatePicker();" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            至
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndDate" flag="startDate" onclick="WdatePicker();" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="divFooter" style="text-align: left;">
                <asp:Button ID="btnSearch" Text="查询" Style="cursor: pointer;" OnClick="btnSearch_Click" runat="server" />
                <asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div class="pagediv" style="width: 100%">
                    <asp:GridView ID="gvwCostDetail" CssClass="gvdata" AutoGenerateColumns="false" runat="server">
<EmptyDataTemplate>
                            <table id="emptyCostDetail" class="gvdata">
                                <tr class="header">
                                    <th scope="col">
                                        序号
                                    </th>
                                    <th scope="col">
                                        名称
                                    </th>
                                    <th scope="col">
                                        金额
                                    </th>
                                    <th scope="col">
                                        经手人
                                    </th>
                                    <th scope="col">
                                        录入人
                                    </th>
                                    <th scope="col">
                                        发生单位
                                    </th>
                                    <th scope="col">
                                        发生时间
                                    </th>
                                    <th scope="col">
                                        备注
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="20px">
<ItemTemplate>
                                    <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="CostName" HeaderStyle-Width="90px" HeaderStyle-HorizontalAlign="Left" HeaderText="名称" /><asp:BoundField DataField="IssuedBy" HeaderStyle-Width="90px" HeaderStyle-HorizontalAlign="Left" HeaderText="经手人" /><asp:BoundField DataField="InputUser" HeaderStyle-Width="90px" HeaderStyle-HorizontalAlign="Left" HeaderText="录入人" /><asp:BoundField DataField="DepartMent" HeaderStyle-Width="90px" HeaderStyle-HorizontalAlign="Left" HeaderText="发生单位" /><asp:TemplateField HeaderText="发生时间" HeaderStyle-Width="90px">
<ItemTemplate>
                                    <%# string.Format("{0:d}", Eval("InputDate")) %>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="金额" HeaderStyle-Width="70px">
<ItemTemplate>
                                    <%# string.IsNullOrEmpty(Eval("Amount").ToString()) ? "0.000" : System.Convert.ToDecimal(Eval("Amount")).ToString("0.000") %>
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="Note" ItemStyle-HorizontalAlign="Left" HeaderText="备注" ItemStyle-Width="200px" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                    </webdiyer:AspNetPager>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
