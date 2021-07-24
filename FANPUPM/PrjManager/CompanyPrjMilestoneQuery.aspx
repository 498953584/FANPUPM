<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyPrjMilestoneQuery.aspx.cs" Inherits="PrjManager_CompanyPrjMilestoneQuery" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title>
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
            var justTable = new JustWinTable('gvPrjMilestoneDetail');
            replaceEmptyTable('emptyTable', 'gvPrjMilestoneDetail');
        })
        //选择部门
        function selectOnedepment() {

            jw.selectOneDep({ idinput: 'hlfdepmentCode', nameinput: 'txtdepmentName' });
        }
        //选择人员
        function selectOneUser() {

            jw.selectOneUser({ codeinput: 'hlfuserCode', nameinput: 'txtuserName' });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <table class="queryTable" cellpadding="3px" cellspacing="0px">
                        <tr>                          
                            <td class="descTd" style="white-space: nowrap;">
                                项目负责人
                            </td>
                            <td>
                                <asp:TextBox ID="txtuserName" CssClass="select_input" imgclick="selectOneUser" Width="120px" runat="server"></asp:TextBox>
                                <asp:HiddenField ID="hlfuserCode" runat="server" />
                            </td>
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
                                    <asp:GridView ID="gvPrjMilestoneDetail" CssClass="gvdata" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="gvPrjMilestoneDetail_RowDataBound" DataKeyNames="Id" runat="server">
<EmptyDataTemplate>
                                            <table id="emptyTable">
                                                <tr class="header">
                                                    <th scope="col" style="width: 20px;">
                                                    </th>
                                                    <th scope="col">
                                                        序号
                                                    </th>
                                                    <th scope="col">
                                                        项目编号
                                                    </th>
                                                    <th scope="col">
                                                        项目名称
                                                    </th>
                                                    <th scope="col">
                                                        上报时间
                                                    </th>
                                                    <th scope="col">
                                                        部门
                                                    </th>
                                                    <th scope="col">
                                                        项目负责人
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
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                    <asp:CheckBox ID="cbAllBox" runat="server" />
                                                </HeaderTemplate><ItemTemplate>
                                                    <asp:CheckBox ID="cbBox" runat="server" />
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="20px">
<ItemTemplate>
                                                    <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目编号" HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Left"><ItemTemplate>
                                                    <%# Eval("PrjCode") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Left"><ItemTemplate>
                                                    <%# Eval("PrjName") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="上报时间" HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Left"><ItemTemplate>
                                                    <%# GetRptDate(Eval("RptDate")) %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="部门" HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Left"><ItemTemplate>
                                                    <%# Eval("DepName") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目负责人" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                                    <%# Eval("UserName") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目储备额" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                                    <%# Eval("StoreAmount") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="当年预测" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                                    <%# Eval("ForeCast") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="储备转换率" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                                    <%# Common2.FormatRate(Eval("StoreSwitchRate")) %>
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
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合计"></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
    <asp:HiddenField ID="hfldPrjCount" runat="server" />
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
