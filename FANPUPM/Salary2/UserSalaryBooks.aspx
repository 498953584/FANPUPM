<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserSalaryBooks.aspx.cs" Inherits="Salary2_UserSalaryBooks" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>员工帐套管理</title><link type="text/css" rel="Stylesheet" href="../Script/jquery.tooltip/jquery.tooltip.css" />
<link type="text/css" rel="Stylesheet" href="../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript" src="Script/UserSalaryBooks.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="height: 96%; width: 100%;">
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
                            <asp:TextBox ID="txtName" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            岗位
                        </td>
                        <td>
                            <asp:TextBox ID="txtPost" Width="120px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            是否指定
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIsAssign" Width="120px" runat="server"><asp:ListItem Value="" /><asp:ListItem Value="false" Text="未指定" /><asp:ListItem Value="true" Text="已指定" /></asp:DropDownList>
                        </td>
                        <td class="descTd">
                            状态
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlState" Width="120px" runat="server"><asp:ListItem Value="" /><asp:ListItem Value="1" Text="在职" /><asp:ListItem Value="2" Text="离职" /></asp:DropDownList>
                        </td>
                        <td class="descTd">
                            帐套
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSaBooksSearch" Width="120px" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" Text="查询" Style="cursor: pointer;" OnClick="btnSearch_Click" runat="server" />
                        </td>
                    </tr>
                </table>
                <table class="tab">
                    <tr>
                        <td class="divFooter" style="text-align: left;">
                            <input type="button" id="btnAssign" value="工资帐套" disabled="disabled" style="width: 80px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 100%;">
                            <div>
                                <asp:GridView ID="gvwUserInfo" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" OnRowDataBound="gvwUserInfo_RowDataBound" DataKeyNames="v_yhdm,Id,BooksId" runat="server">
<EmptyDataTemplate>
                                        <table class="grid" cellspacing="0" rules="all" border="1" id="EmptyUserInfo" style="border-collapse: collapse;">
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
                                                <th scope="col" style="width: 150px">
                                                    岗位
                                                </th>
                                                <th scope="col" style="width: 80px">
                                                    入司日期
                                                </th>
                                                <th scope="col" style="width: 50px">
                                                    状态
                                                </th>
                                                <th scope="col" style="width: 100px">
                                                    工资帐套
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
                                                <input type="checkbox" id="chkAll" />
                                            </HeaderTemplate><ItemTemplate>
                                                <input type="checkbox" id="chkSelectSingle" />
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                <%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="员工编号">
<ItemTemplate>
                                                <%# Eval("UserCode") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="员工姓名">
<ItemTemplate>
                                                <span class="tooltip" title="<%# Eval("v_xm") %>">
                                                    <%# StringUtility.GetStr(Eval("v_xm").ToString(), 25) %>
                                                </span>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="岗位">
<ItemTemplate>
                                                <span class="tooltip" title="<%# Eval("RoleTypeName") %>">
                                                    <%# StringUtility.GetStr(Eval("RoleTypeName").ToString(), 25) %>
                                                </span>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="入司日期">
<ItemTemplate>
                                                <%# Common2.GetTime(Eval("EnterCorpDate")) %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="状态">
<ItemTemplate>
                                                <%# Eval("StateName") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="工资帐套" HeaderStyle-Width="150px"><ItemTemplate>
                                                <asp:DropDownList Width="150px" ID="ddlSaBooks" runat="server"></asp:DropDownList>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" ShowPageIndexBox="Always" NumericButtonCount="8" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                </webdiyer:AspNetPager>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divSaBooks" style="display: none;">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="descTd">
                    &nbsp;&nbsp;&nbsp;帐套
                </td>
                <td>
                    <asp:DropDownList ID="ddlSaUserBooks" Style="width: 100%;" runat="server"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <asp:Button ID="btnSave" Style="display: none;" OnClick="btnSave_Click" runat="server" />
    <asp:HiddenField ID="hfldCheckedId" runat="server" />
    <asp:HiddenField ID="hfldSelectedSaBooks" runat="server" />
    <link type="text/css" rel="Stylesheet" href="../Script/jquery.easyui/themes/default/easyui.css" />
    <link type="text/css" rel="Stylesheet" href="../Script/jquery.easyui/themes/icon.css" />
    </form>
</body>
</html>
