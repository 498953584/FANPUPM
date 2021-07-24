<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayoutInvoice.aspx.cs" Inherits="ContractManage_PayoutInvoice_PayoutInvoice" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="cn.justwin.contractBLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>支出合同发票</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            replaceEmptyTable('emptyInvoice', 'gvwInvoice');
            var contractTable = new JustWinTable('gvwInvoice');
        });
        //查看支出合同变更
        function rowQueryA(path) {
            parent.parent.desktop.ModifyEdit = window;
            winopen(path, "查看发票");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="pagediv">
        <asp:GridView ID="gvwInvoice" CssClass="gvdata" AutoGenerateColumns="false" DataKeyNames="InvoiceID" runat="server">
<EmptyDataTemplate>
                <table id="emptyInvoice" class="gvdata">
                    <tr class="header">
                        <th scope="col" style="width: 20px;">
                            <input id="chk1" type="checkbox" />
                        </th>
                        <th scope="col" style="width: 25px;">
                            序号
                        </th>
                        <th scope="col">
                            发票号码
                        </th>
                        <th scope="col">
                            付款累计
                        </th>
                        <th scope="col">
                            已收发票
                        </th>
                        <th scope="col">
                            发票金额
                        </th>
                        <th scope="col">
                            收款单位
                        </th>
                        <th scope="col">
                            付款单位
                        </th>
                        <th scope="col">
                            开票时间
                        </th>
                        <th scope="col">
                            办理人
                        </th>
                        <th scope="col">
                            附件
                        </th>
                    </tr>
                </table>
            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                        <asp:CheckBox ID="chkAll" runat="server" />
                    </HeaderTemplate><ItemTemplate>
                        <asp:CheckBox ID="chk" runat="server" />
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="发票号码"><ItemTemplate>
                        <span class="link" onclick="rowQueryA('/ContractManage/PayoutInvoice/InvoiceEdit.aspx?Action=Query&InvoiceId=<%# Eval("InvoiceID") %>')">
                            <%# Eval("InvoiceNo") %>
                        </span>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderText="付款累计"><ItemTemplate>
                        <%# new PayoutPayment().GetPaySum(Eval("ContractID").ToString()) %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderText="已收发票"><ItemTemplate>
                        <%# new PayoutInvoice().GetInvoiceSum(Eval("ContractID").ToString()) %>
                    </ItemTemplate></asp:TemplateField><asp:BoundField DataField="Amount" ItemStyle-HorizontalAlign="Right" HeaderText="发票金额" /><asp:BoundField DataField="Payee" HeaderText="收款单位" /><asp:BoundField DataField="Payer" HeaderText="付款单位" /><asp:TemplateField HeaderText="开票时间"><ItemTemplate>
                        <%# Common2.GetTime(Eval("InvoiceDate").ToString()) %>
                    </ItemTemplate></asp:TemplateField><asp:BoundField DataField="Transactor" HeaderText="办理人" /><asp:TemplateField HeaderText="附件"><ItemTemplate>
                        <%# GetAnnx(Convert.ToString(Eval("InvoiceID"))) %>
                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
    </div>
    <asp:HiddenField ID="hdContractID" runat="server" />
    </form>
</body>
</html>
