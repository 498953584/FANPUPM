<%@ Page Language="C#" AutoEventWireup="true" CodeFile="accountList.aspx.cs" Inherits="AccountManage_acc_Manage_accountList" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>账户设置</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script src="../../StockManage/script/Config2.js" type="text/javascript"></script>

    <script src="../../StockManage/script/JustWinTable.js" type="text/javascript"></script>

    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>

    <script type="text/javascript" language="javascript">
        addEvent(window, 'load', function() {
        var aa = new JustWinTable('gvwContract');
        setButton(aa,'btnDel', 'btnEdit', 'btnSel', 'hdfAccId')
//            addEvent(document.getElementById('Button1'), 'click', addContract);
            formateTable('gvConract', 2)
        });
        function addContract() {
            parent.desktop.accountList = window;
            var url = "/AccountManage/acc_Manage/PayoutContract.aspx";
            toolbox_oncommand(url, "111");
        }

        function rowEdit() {
            parent.desktop.flowclass = window
            var authority = document.getElementById("hdflimits").value;
            var accAllot = document.getElementById("hdfaccAllot").value;
            var url = "";
            //项目
            if (accAllot == 0) {
                url = "/AccountManage/acc_Manage/PrjInfo.aspx?limits=" + authority;
            }
            else if (accAllot == 1) {
                 url = "/AccountManage/acc_Manage/PayoutContract.aspx?limits=" + authority;
            }      
            toolbox_oncommand(url, "新增账户");
        }
         function attributeEdit() {
             parent.desktop.flowclass = window
             var id = document.getElementById("hdfAccId").value;
             var limits = document.getElementById("hdflimits").value;
             var url = "/AccountManage/acc_Manage/accountEdit.aspx?accountid=" + id + "&limits="+limits;        
            toolbox_oncommand(url, "账户属性设置");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table class="tab">
        <tr>
            <td class="divHeader">
                账户设置
            </td>
        </tr>
        <tr>
            <td class="divFooter" style="text-align: right;">
                编号：<asp:TextBox ID="txtCode" Width="80px" runat="server"></asp:TextBox>
               别名：<asp:TextBox ID="txtName" Width="80px" runat="server"></asp:TextBox>
                <asp:Button ID="btnSel" Text="查询" OnClick="btnSel_Click" runat="server" />
                <asp:Button ID="btnAdd" Text="新增" runat="server" />
                <asp:Button ID="btnActive" Text="激活" OnClick="btnActive_Click" runat="server" />
                <asp:Button ID="btnDel" Text="注销" OnClick="btnDel_Click" runat="server" />
                <asp:Button ID="btnEdit" Text="账号设置" Width="100px" runat="server" />
                <asp:HiddenField ID="hidSel" runat="server" />
                <asp:HiddenField ID="hdfaccAllot" runat="server" />
                <asp:HiddenField ID="hdfAccId" runat="server" />
                <asp:HiddenField ID="hdflimits" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="height: 100%;">
                <div class="pagediv" id="contract" runat="server">
                    <asp:GridView ID="gvwContract" Width="100%" CssClass="gvdata" AutoGenerateColumns="false" AllowPaging="true" PageSize="19" OnPageIndexChanging="gvwContract_PageIndexChanging" OnRowDataBound="gvwContract_RowDataBound" DataKeyNames="accountID" runat="server">
<EmptyDataTemplate>
                            <table class="tab" width="100%">
                                <tr class="header">
                                    <td>
                                        <input id="Checkbox1" type="checkbox" />
                                    </td>
                                    <td>
                                        序号
                                    </td>
                                    <td>
                                        账号编码
                                    </td>
                                    <td>
                                        账号别名
                                    </td>
                                    <td>账户状态</td>
                                    <td>
                                        创建人员
                                    </td>
                                    <td>
                                        创建日期
                                    </td>
                                    <td>
                                        是否激活
                                    </td>
                                    <td>
                                        激活人员
                                    </td>
                                    <td>
                                        激活日期
                                    </td>
                                    <td>
                                        合同编码
                                    </td>
                                    <td>
                                        合同名称
                                    </td>
                                    <td>
                                        项目编号
                                    </td>
                                    <td>
                                        项目名称
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
<HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" runat="server" />
                                </HeaderTemplate>

<ItemTemplate>
                                    <asp:CheckBox ID="chkAcc" runat="server" />
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="accountNum" HeaderText="账户编号" /><asp:BoundField DataField="acountName" HeaderText="账户名称" ItemStyle-HorizontalAlign="Left" /><asp:TemplateField HeaderText="账户状态" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                    <%# (Eval("isnullify").ToString() == "0") ? "正常" : "注销" %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="是否激活" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                    <%# (Eval("isActivity").ToString() == "0") ? "否" : "是" %>
                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="aName" HeaderText="激活人员" ItemStyle-HorizontalAlign="Center" /><asp:TemplateField HeaderText="激活时间" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                    <%# Eval("activeData", "{0:d}") %>
                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="cName" HeaderText="创建人员" ItemStyle-HorizontalAlign="Center" /><asp:TemplateField HeaderText="创建时间" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                    <%# Eval("creatData", "{0:d}") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同编号" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                    <%# Eval("contractCode") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同名称" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                    <%# Eval("contractName") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目编号" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                    <%# Eval("prjCode") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                    <%# StringUtility.GetStr(Eval("prjName").ToString()) %>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
