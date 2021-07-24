<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPaymentApplyList.aspx.cs" Inherits="ContractManage_PaymentApply_ViewPaymentApplyList" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>收入合同资金支付</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript">
        function rowQueryA(path) {
            parent.parent.desktop.AddIncometBalance = window;
            toolbox_oncommand(path, "查看收入合同资金支付");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="pagediv" style="border: solid 0px red;">
        <asp:GridView ID="gvConract" AutoGenerateColumns="false" CssClass="gvdata" DataKeyNames="PaymentId" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                        <asp:CheckBox ID="cbAllBox" runat="server" />
                    </HeaderTemplate><ItemTemplate>
                        <asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("PaymentId")) %>' runat="server" />
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="收款编号"><ItemTemplate>
                        <a href="#" class="al" onclick="viewopen('/ContractManage/PaymentApply/PaymentApplyQuery.aspx?t=1&ic=<%# Eval("PaymentId") %>&contractId=<%# Eval("ContractId") %>')">
                            <%# Eval("PaymentCode") %></a>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="收款日期"><ItemTemplate>
                        <%# Common2.GetTime(Eval("PaymentDate").ToString()) %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申请人"><ItemTemplate>
                        <%# Eval("PaymentPenson") %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申请金额" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                        <%# Eval("PaymentAmount") %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                        <%# Common2.GetState(Eval("ApplyFlowState").ToString()) %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
                        <%# GetAnnx(Convert.ToString(Eval("PaymentId"))) %>
                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
    </div>
    <asp:HiddenField ID="hldfIsExamineApprove" runat="server" />
    </form>
</body>
</html>
