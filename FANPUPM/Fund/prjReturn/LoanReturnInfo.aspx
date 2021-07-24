<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoanReturnInfo.aspx.cs" Inherits="Fund_prjReturn_LoanReturnInfo" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>借款还款情况报表</title><link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var aa = new JustWinTable('gvLoanReturn');
        });

        function openProjPicker() {
            jw.selectOneProject({ idinput: 'hdnProjectCode', nameinput: 'txtPrjName' });
        }

        //选择账户
        function openAccoun() {
            var url = '/Common/DivSelectFundAccoun.aspx';
            top.ui.callback = function (o) {
                $('#hdfAccoun').val(o.id);
                $('#txtAccoun').val(o.name);
            }
            top.ui.openWin({ title: '选择帐户', url: url });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr id="header">
            <td>
                <asp:Label ID="lblTitle" Text="借款还款情况报表" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 96%; width: 100%;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <table class="queryTable" cellpadding="3px" cellspacing="0px">
                                <tr>
                                    <td class="descTd">
                                        借款日期
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtStartTime" Width="80px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="descTd">
                                        -
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEndTime" Width="80px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="descTd">
                                        账户名称
                                    </td>
                                    <td>
                                        <span class="spanSelect" style="width: 122px">
                                            <input id="txtAccoun" readonly="readonly" style="width: 97px; height: 15px;
                                                border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                                            <img alt="选择账户" onclick="openAccoun();" src="../../images/icon.bmp" style="float: right;" />
                                        </span>
                                    </td>
                                    <td class="descTd">
                                        还款情况
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlReturnState" runat="server"><asp:ListItem Value="-1" Text="--请选择--" /><asp:ListItem Value="1" Text="已还清" /><asp:ListItem Value="0" Text="未还清" /></asp:DropDownList>
                                    </td>
                                    <td class="descTd">
                                        项目
                                    </td>
                                    <td>
                                        <span class="spanSelect" style="width: 122px">
                                            
                                            <input id="txtPrjName" readonly="readonly" style="width: 97px; height: 15px; border: none;
                                                line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                                            <img alt="选择项目" onclick="openProjPicker();" src="../../images/icon.bmp" style="float: right" />
                                        </span>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="divFooter" style="text-align: left;">
                            <asp:Button ID="btnexecl" CssClass="button-normal" Text="导出Excel" Width="80px" OnClick="btnexecl_Click" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 100%; vertical-align: top;">
                            <div>
                                <asp:GridView ID="gvLoanReturn" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvLoanReturn_PageIndexChanging" OnRowDataBound="gvLoanReturn_RowDataBound" DataKeyNames="LoanID" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目">
<ItemTemplate>
                                                <%# Eval("prjName") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="账户名称"><ItemTemplate>
                                                <span class="tooltip" title='<%# Eval("acountName") %>'>
                                                    <%# StringUtility.GetStr(Eval("acountName"), 10) %>
                                                </span>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="借款单号">
<ItemTemplate>
                                                <span class="al" onclick="toolbox_oncommand('/Fund/PrjLoan/PrjLoanView.aspx?ic=<%# Eval("loanid") %>','查看借款信息')">
                                                    <%# StringUtility.GetStr(Eval("LoanCode").ToString()) %>
                                                </span>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="借款日期">
<ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "LoanDate", "{0:yyyy-MM-dd}") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="借款金额" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                <%# Eval("LoanFund") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="是否还清">
<ItemTemplate>
                                                <asp:Label ID="labState" Text='<%# Convert.ToString(Eval("LoanReturnState")) %>' runat="server"></asp:Label>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="归还本金" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                <%# Eval("returnMoney") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="归还利息" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                <%# Eval("returnInterest") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="其他扣款" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                <asp:Label ID="Label1" Text='<%# Convert.ToString(Eval("returnDeduct")) %>' runat="server"></asp:Label>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注">
<ItemTemplate>
                                                <span class="tooltip" title='<%# Eval("Remark") %>'>
                                                    <%# StringUtility.GetStr(Eval("Remark").ToString()) %>
                                                </span>
                                            </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnProjectCode" runat="server" />
    <asp:HiddenField ID="hdfAccoun" runat="server" />
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    </form>
</body>
</html>
