<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayoutPaymentEdit.aspx.cs" Inherits="ContractManage_PayoutPayment_PayoutPaymentEdit" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>支出合同付款</title>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/wf.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            replaceEmptyTable('emptyContractType', 'gvwContract');
            var contractTable = new JustWinTable('gvwContract');
            if (document.getElementById("hdContractID").value == "") {
                document.getElementById("btnAdd").setAttribute('disabled', 'disabled');
                document.getElementById("btnUpdate").setAttribute('disabled', 'disabled');
                document.getElementById("btnQuery").setAttribute('disabled', 'disabled');
            }
            else {
                setButton(contractTable, 'btnDelete', 'btnUpdate', 'btnQuery', 'hfldPaymentId');
                setWfButtonState2(contractTable, 'WF1');
                //document.getElementById("btnAdd").removeAttribute('disabled');               
            }

            registerAddEvent();
            registerDeleteEvent();
            registerUpdateEvent();
            registerQueryEvent();
        });

        function registerAddEvent() {
            addEvent(document.getElementById('btnAdd'), 'click', function () {
                parent.parent.desktop.PaymentEdit = window;
                var url = '/ContractManage/PayoutPayment/PaymentEdit.aspx?Action=Add&ContractId=' + document.getElementById('hdContractID').value + '&Random=' + new Date().getTime();
                toolbox_oncommand(url, "新增支出合同付款");
            });
        }

        function registerDeleteEvent() {
            document.getElementById('btnDelete').onclick = function () {
                if (!confirm('确定要删除吗？')) {
                    return false;
                }
            }
        }

        function registerUpdateEvent() {
            addEvent(document.getElementById('btnUpdate'), 'click', function () {
                parent.parent.desktop.PaymentEdit = window;
                var url = '/ContractManage/PayoutPayment/PaymentEdit.aspx?Action=Update&PaymentId=' + document.getElementById('hfldPaymentId').value + '&Random=' + new Date().getTime()
                toolbox_oncommand(url, "编辑支出合同付款");
            });
        }

        function registerQueryEvent() {
            addEvent(document.getElementById('btnQuery'), 'click', function () {
                //                parent.parent.desktop.PaymentEdit = window;
                var paymentId = document.getElementById('hfldPaymentId').value;
                //                var url = '/ContractManage/PayoutPayment/PaymentQuery.aspx?Action=Query&PaymentId=' + paymentId + '&ic=' + paymentId + '&Random=' + new Date().getTime();
                //                toolbox_oncommand(url, "查看支出合同付款");
                rowQueryA('/ContractManage/PayoutPayment/PaymentQuery.aspx?showAudit=1&Action=Query&PaymentId=' + paymentId + '&ic=' + paymentId);
            });
        }
        //查看支出合同变更
        function rowQueryA(path) {
            parent.parent.desktop.PaymentEdit = window;
            //toolbox_oncommand(path, "查看支出合同付款");
            viewopen(path, "查看支出合同付款");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="tab">
        <tr style="vertical-align: top;">
            <td class="divFooter" style="text-align: left;">
                <asp:Button ID="btnAdd" Enabled="false" Text="新增" runat="server" />
                <input type="button" id="btnUpdate" value="编辑" disabled="disabled" />
                <asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
                <input type="button" id="btnQuery" disabled="disabled" value="查看" />
                <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="084" BusiClass="001" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div class="pagediv">
                    <asp:GridView ID="gvwContract" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwContract_RowDataBound" DataKeyNames="ID,PrjGuid,PaymentCode" runat="server">
<EmptyDataTemplate>
                            <table id="emptyContractType" class="gvdata">
                                <tr class="header">
                                    <th scope="col" style="width: 20px;">
                                        <input id="chk1" type="checkbox" />
                                    </th>
                                    <th scope="col" style="width: 25px;">
                                        序号
                                    </th>
                                    <th scope="col">
                                        支付编号
                                    </th>
                                    <th scope="col">
                                        支付金额
                                    </th>
                                    <th scope="col">
                                        实际支付金额
                                    </th>
                                    <th scope="col">
                                        支付时间
                                    </th>
                                    <th scope="col">
                                        申请人
                                    </th>
                                    <th scope="col">
                                        流程状态
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
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="支付编号"><ItemTemplate>
                                    <span class="link" onclick="rowQueryA('/ContractManage/PayoutPayment/PaymentQuery.aspx?Action=Query&PaymentId=<%# Eval("ID") %>&ic=<%# Eval("ID") %>')">
                                        <%# Eval("PaymentCode") %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="PaymentMoney" ItemStyle-HorizontalAlign="Right" HeaderText="支付金额" ItemStyle-CssClass="decimal_input" /><asp:TemplateField HeaderText="实际支付金额" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                    <%# WebUtil.SapRealPayMoney(Eval("PaymentCode")) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="支付时间"><ItemTemplate>
                                    <%# Common2.GetTime(Eval("PaymentDate").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="PaymentPerson" HeaderText="申请人" /><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                                    <%# Common2.GetState(Eval("FlowState").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
                                    <%# GetAnnx(Convert.ToString(Eval("ID"))) %>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdContractID" runat="server" />
    <asp:HiddenField ID="hfldPaymentId" runat="server" />
    </form>
</body>
</html>
