<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryEmployee.aspx.cs" Inherits="HR_Personnel_QueryEmployee" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>员工查询</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />
<link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            // 添加验证
            $('#Button1')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            var empTable = new JustWinTable('gvEmployee');
            replaceEmptyTable('emptyTable', 'gvEmployee');
        });

        function viewopen_n(employeeCode, deparment) {
            window.open('EmployeeView.aspx?t=3&cc=' + deparment + '&uc=' + employeeCode, '', 'left=150,top=150,width=700,height=600,toolbar=no,status=yes,scrollbars=yes,resiz able=no');
        }
        
    </script>
    <style type="text/css">
        .padding
        {
            margin-left: 3px;
            margin-right: 3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd">
                            员工编号
                        </td>
                        <td>
                            <asp:TextBox ID="txtCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            员工姓名
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            部门名称
                        </td>
                        <td>
                            <asp:TextBox ID="txtDepartment" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            岗位
                        </td>
                        <td>
                            <asp:TextBox ID="txtPost" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            职级
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPositionLevel" Width="120px" runat="server"></asp:DropDownList>
                        </td>
                        <td class="descTd">
                            职称
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPostAndRank" Width="120px" runat="server"></asp:DropDownList>
                        </td>
                        <td class="descTd">
                            学历
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEducationalBackground" Width="120px" runat="server"></asp:DropDownList>
                        </td>
                        <td class="descTd">
                            人员类型
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlClassID" Width="120px" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" class="tab">
                    <tr>
                        <td class="divFooter" style="text-align: left">
                            <asp:Button ID="Button1" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
                            <asp:Button ID="Button2" CssClass="button-normal" Text="导 出" OnClick="btnExp_Click1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;" valign="top">
                            <div>
                                <asp:GridView ID="gvEmployee" AutoGenerateColumns="false" CssClass="gvdata" ShowFooter="false" OnRowDataBound="gvEmployee_RowDataBound" DataKeyNames="v_yhdm" runat="server">
<EmptyDataTemplate>
                                        <table id="emptyTable">
                                            <tr class="header">
                                                <th scope="col">
                                                    序号
                                                </th>
                                                <th scope="col">
                                                    员工编号
                                                </th>
                                                <th scope="col">
                                                    员工姓名
                                                </th>
                                                <th scope="col">
                                                    部门名称
                                                </th>
                                                <th scope="col">
                                                    岗位
                                                </th>
                                                <th scope="col">
                                                    入司日期
                                                </th>
                                                <th scope="col">
                                                    年龄
                                                </th>
                                                <th scope="col">
                                                    职级
                                                </th>
                                                <th scope="col">
                                                    职称
                                                </th>
                                                <th scope="col">
                                                    学历
                                                </th>
                                                <th scope="col">
                                                    人员类型
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                                <%# Eval("Num") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="员工编号" HeaderStyle-Width="50px">
<ItemTemplate>
                                                <%# Eval("UserCode") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="员工姓名" HeaderStyle-Width="50px"><ItemTemplate>
                                                <span class="link" onclick="viewopen_n('<%# Eval("v_yhdm") %>','<%# Eval("i_bmdm") %>');">
                                                    <%# Eval("v_xm") %>
                                                </span>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="部门名称" HeaderStyle-Width="80px"><ItemTemplate>
                                                <%# Eval("v_bmqc") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="岗位" HeaderStyle-Width="100px"><ItemTemplate>
                                                <%# Eval("RoleTypeName") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="入司日期" HeaderStyle-Width="80px"><ItemTemplate>
                                                <%# WebUtil.ConvertDateTime(Eval("EnterCorpDate")) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="年龄" HeaderStyle-Width="30px"><ItemTemplate>
                                                <%# Eval("Age") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="职级"><ItemTemplate>
                                                <%# ConvertWay(Eval("PositionLevel").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="职称"><ItemTemplate>
                                                <%# Eval("PostName") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="学历"><ItemTemplate>
                                                <%# Eval("EducationalBackground") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="人员类型"><ItemTemplate>
                                                <%# Eval("ClassName") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="系统帐号"><ItemTemplate>
                                                <%# Eval("V_Dlid") %>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                </webdiyer:AspNetPager>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfldEmployee" runat="server" />
    </form>
</body>
</html>
