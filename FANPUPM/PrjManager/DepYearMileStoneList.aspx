<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepYearMileStoneList.aspx.cs" Inherits="PrjManager_DepYearMileStoneList" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>部门月度</title>
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
            var justTable = new JustWinTable('depmentPrjMilestoneQuery');
            replaceEmptyTable('emptyTable', 'depmentPrjMilestoneQuery');
        });
        //选择人员
        function selectOnedepment() {

            jw.selectOneDep({ idinput: 'hlfdepmentCode', nameinput: 'txtdepmentName' });
        }
        //查看明细
        function Query(name) {
            var date = new Date();
            var year = date.getFullYear();
            parent.desktop.PrjMileStoneDetailList = window;
            var url = '/PrjManager/DepYearMileStoneDetail.aspx?name=' + name + '&year=' + year;
            toolbox_oncommand(url, "部门年度里程碑明细查看");
        }
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
                                部门名称
                            </td>
                            <td>
                                <asp:TextBox ID="txtdepmentName" CssClass="select_input" imgclick="selectOnedepment" Width="120px" runat="server"></asp:TextBox>
                                <asp:HiddenField ID="hlfdepmentCode" runat="server" />
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
                                <asp:Button ID="btnImput" Text="导出" OnClick="btnImput_Click" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <asp:GridView ID="depmentPrjMilestoneQuery" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="depmentPrjMilestoneQuery_RowDataBound" DataKeyNames="NUM" runat="server">
<EmptyDataTemplate>
                                            <table id="emptyTable">
                                                <tr class="header">
                                                    <th scope="col">
                                                        序号
                                                    </th>
                                                    <th scope="col">
                                                        部门名称
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
</asp:TemplateField><asp:TemplateField HeaderText="部门名称" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                                    <asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(Eval("i_bmdm").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server">                       
                                                        <span class="link" onclick="Query('<%# Eval("i_bmdm") %>');">
                                                            <%# Eval("v_BMMC") %>
                                                        </span>
                                                    </asp:HyperLink>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目储备额" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                                    <%# Eval("StoreAmount") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="当年预测" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                                    <%# Eval("ForeCast") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="储备转换率" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                                    <%# FormatRate(Eval("ForeCast"), Eval("StoreAmount")) %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="明年预测" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
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
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程总数"><ItemTemplate>
                                                    <%# System.Convert.ToInt32(Eval("Stone9")) + System.Convert.ToInt32(Eval("Stone8")) + System.Convert.ToInt32(Eval("Stone7")) + System.Convert.ToInt32(Eval("Stone6")) + System.Convert.ToInt32(Eval("Stone5")) + System.Convert.ToInt32(Eval("Stone4")) + System.Convert.ToInt32(Eval("Stone3")) + System.Convert.ToInt32(Eval("Stone2")) + System.Convert.ToInt32(Eval("Stone1")) %>
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
    <asp:HiddenField ID="hfldForeCast" runat="server" />
    <asp:HiddenField ID="hfldStoreAmount" runat="server" />
    <asp:HiddenField ID="hfldNextForeCast" runat="server" />
    <asp:HiddenField ID="hfldStone1" runat="server" />
    <asp:HiddenField ID="hfldStone2" runat="server" />
    <asp:HiddenField ID="hfldStone3" runat="server" />
    <asp:HiddenField ID="hfldStone4" runat="server" />
    <asp:HiddenField ID="hfldStone5" runat="server" />
    <asp:HiddenField ID="hfldStone6" runat="server" />
    <asp:HiddenField ID="hfldStone7" runat="server" />
    <asp:HiddenField ID="hfldStone8" runat="server" />
    <asp:HiddenField ID="hfldStone9" runat="server" />
    </form>
</body>
</html>
