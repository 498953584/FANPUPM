<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayoutInvoiceEdit.aspx.cs" Inherits="ContractManage_PayoutInvoice_PayoutInvoiceEdit" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="cn.justwin.contractBLL"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>支出合同发票</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            replaceEmptyTable('emptyInvoice', 'gvwInvoice');
            var invoiceTable = new JustWinTable('gvwInvoice');
            if (document.getElementById("hdContractID").value == "") {
                document.getElementById("btnAdd").setAttribute('disabled', 'disabled');
                document.getElementById("btnUpdate").setAttribute('disabled', 'disabled');
                document.getElementById("btnQuery").setAttribute('disabled', 'disabled');
            }
            else {
                setButton(invoiceTable, 'btnDelete', 'btnUpdate', 'btnQuery', 'hfldInvoice');
            }

            registerAddEvent();
            registerUpdateEvent();
            registerDeleteEvent();
            registerQueryEvent();
        });

        function registerAddEvent() {
            addEvent(document.getElementById('btnAdd'), 'click', function () {
                parent.parent.desktop.InvoiceEdit = window;
                var url = '/ContractManage/PayoutInvoice/InvoiceEdit.aspx?Action=Add&ContractId=' + document.getElementById('hdContractID').value + '&Random=' + new Date().getTime();
                toolbox_oncommand(url, "新增发票");
            });
        }

        function registerUpdateEvent() {
            addEvent(document.getElementById('btnUpdate'), 'click', function () {
                parent.parent.desktop.InvoiceEdit = window;
                var url = '/ContractManage/PayoutInvoice/InvoiceEdit.aspx?Action=Update&InvoiceId=' + document.getElementById('hfldInvoice').value + '&Random=' + new Date().getTime();
                toolbox_oncommand(url, "编辑发票");
            });
        }

        function registerDeleteEvent() {
            document.getElementById('btnDelete').onclick = function () {
                if (!confirm("确定要删除吗?")) {
                    return false;
                }
            }
        }

        function registerQueryEvent() {
            addEvent(document.getElementById('btnQuery'), 'click', function () {
                //parent.parent.desktop.InvoiceEdit = window;
                var url = '/ContractManage/PayoutInvoice/QueryPayountInvoice.aspx?Action=Query&InvoiceId=' + document.getElementById('hfldInvoice').value + '&Random=' + new Date().getTime();
                //toolbox_oncommand(url, "查看发票");
                viewopen(url);
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="tab">
        <tr style="vertical-align: top;">
            <td class="divFooter" style="text-align: left;">
                <input type="button" id="btnAdd" value="新增" />
                <input type="button" id="btnUpdate" value="编辑" disabled="disabled" />
                <asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
                <input type="button" id="btnQuery" disabled="disabled" value="查看" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div class="pagediv">
                    <asp:GridView ID="gvwInvoice" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwInvoice_RowDataBound" DataKeyNames="InvoiceID" runat="server">
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
                                        实收发票金额
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
                                    <span class="link" onclick="viewopen('/ContractManage/PayoutInvoice/QueryPayountInvoice.aspx?Action=Query&InvoiceId=<%# Eval("InvoiceID") %>')">
                                        <%# Eval("InvoiceNo") %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderText="付款累计" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                    <%# new PayoutPayment().GetPaySum(Eval("ContractID").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderText="已收发票" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                    <%# new PayoutInvoice().GetInvoiceSum(Eval("ContractID").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="Amount" ItemStyle-HorizontalAlign="Right" HeaderText="发票金额" ItemStyle-CssClass="decimal_input" /><asp:TemplateField HeaderText="实收发票金额" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                    <%# WebUtil.SapRealPayMoney(Eval("InvoiceNo")) %>
                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="Payee" HeaderText="收款单位" /><asp:BoundField DataField="Payer" HeaderText="付款单位" /><asp:TemplateField HeaderText="开票时间"><ItemTemplate>
                                    <%# Common2.GetTime(Eval("InvoiceDate").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="Transactor" HeaderText="办理人" /><asp:TemplateField HeaderText="附件"><ItemTemplate>
                                    <%# GetAnnx(Convert.ToString(Eval("InvoiceID"))) %>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdContractID" runat="server" />
    <asp:HiddenField ID="hfldInvoice" runat="server" />
    </form>
</body>
</html>
