<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayoutPayment.aspx.cs" Inherits="ContractManage_PayoutPayment_PayoutPayment" %>
<%@ Import Namespace="cn.justwin.BLL"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>支出合同付款</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
     <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
        <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
 
    <script type="text/javascript">
        addEvent(window,'load',function(){
           replaceEmptyTable('emptyContractType','gvwContract');
           var contractTable = new JustWinTable('gvwContract');
       });
       //查看支出合同变更
       function rowQueryA(path) {
           parent.parent.desktop.PaymentEdit = window;
           toolbox_oncommand(path, "查看支出合同付款");
       }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="pagediv">
            <asp:GridView ID="gvwContract" CssClass="gvdata" AutoGenerateColumns="false" DataKeyNames="ID" runat="server">
<EmptyDataTemplate>
                    <table id="emptyContractType" class="gvdata">
                        <tr class="header">
                            <th scope="col" style="width: 20px;">
                                <input id="chk1" type="checkbox" />
                            </th>
                            <th scope="col" style="width: 25px;">
                                序号</th>
                            <th scope="col">
                                支付编号</th>
                            <th scope="col">
                                支付金额</th>
                            <th scope="col">
                                支付时间</th>
                            <th scope="col">
                                支付人</th>
                            <th scope="col">
                                流程状态</th>
                            <th scope="col">
                                附件</th>
                        </tr>
                    </table>
                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" />
                        </HeaderTemplate><ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" />
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="支付编号"><ItemTemplate>
                                        <span class="link" onclick="rowQueryA('/ContractManage/PayoutPayment/PaymentEdit.aspx?Action=Query&PaymentId=<%# Eval("ID") %>')">
                                            <%# Eval("PaymentCode") %>
                                        </span>
                                    </ItemTemplate></asp:TemplateField><asp:BoundField DataField="PaymentMoney" ItemStyle-HorizontalAlign="Right" HeaderText="支付金额" /><asp:TemplateField HeaderText="支付时间"><ItemTemplate>
                            <%# Common2.GetTime(Eval("PaymentDate").ToString()) %>
                        </ItemTemplate></asp:TemplateField><asp:BoundField DataField="PaymentPerson" HeaderText="支付人" /><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                            <%# Common2.GetState(Eval("FlowState").ToString()) %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
                            <%# GetAnnx(Convert.ToString(Eval("ID"))) %>
                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
        </div>
    </form>
</body>
</html>
