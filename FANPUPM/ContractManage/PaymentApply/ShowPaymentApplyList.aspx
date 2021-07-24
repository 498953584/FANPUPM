<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowPaymentApplyList.aspx.cs" Inherits="ContractManage_PaymentApply_ShowPaymentApplyList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>收入合同收款列表</title>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/wf.js"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            var aa = new JustWinTable('gvConract');
            if (document.getElementById("hdContractID").value == "") {
                document.getElementById("btnAdd").setAttribute('disabled', 'disabled');
                document.getElementById("btnEdit").setAttribute('disabled', 'disabled');
                document.getElementById("btnLook").setAttribute('disabled', 'disabled');
            }
            else {
                setButton(aa, 'btnDel', 'btnEdit', 'btnLook', 'hfldPurchaseChecked');
                setWfButtonState2(aa, 'WF1');
                document.getElementById("btnAdd").removeAttribute('disabled');
            }
            addEvent(document.getElementById('btnEdit'), 'click', rowEdit);
            addEvent(document.getElementById('btnLook'), 'click', rowQuery);
            addEvent(document.getElementById('btnAdd'), 'click', rowAdd);
        });
        function rowEdit() {
            parent.parent.desktop.AddIncometPaymentApply = window;
            var url = "/ContractManage/PaymentApply/AddIncometPaymentApply.aspx?id=" + document.getElementById("hfldPurchaseChecked").value + "&ContractID=" + document.getElementById("hdContractID").value + "&action=Update";
            toolbox_oncommand(url, "编辑收入合同资金支付");
        }
        function rowAdd() {
            parent.parent.desktop.AddIncometPaymentApply = window;
            var url = "/ContractManage/PaymentApply/AddIncometPaymentApply.aspx?ContractID=" + document.getElementById("hdContractID").value + "&action=Add";
            toolbox_oncommand(url, "新增收入合同资金支付");
        }
        function rowQuery() {
            var url = "/ContractManage/PaymentApply/PaymentApplyQuery.aspx?t=1&ic=" + document.getElementById("hfldPurchaseChecked").value + "&ContractID=" + document.getElementById("hdContractID").value;
            viewopen(url, "查看收入合同资金支付");
        }
        function rowQueryA(path) {
            parent.parent.desktop.AddIncometPaymentApply = window;
            toolbox_oncommand(path, "查看收入合同资金支付");
        }
        function ClickRow(AuditState) {
            if (AuditState == "是") {
                document.getElementById('btnDel').disabled = true;
                document.getElementById('btnEdit').disabled = true;
                document.getElementById('btnPayment').disabled = true;
            }
            else if (AuditState == "否") {
                document.getElementById('btnDel').disabled = false;
                document.getElementById('btnEdit').disabled = false;
                document.getElementById('btnPayment').disabled = false;
            }
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="tab">
        <tr style="vertical-align: top;">
            <td class="divFooter" style="text-align: left;">
                <input type="button" id="btnAdd" value="新增" />
                <input type="button" id="btnEdit" disabled="disabled" value="编辑" />
                <asp:Button ID="btnDel" Text="删  除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗?')" OnClick="btnDel_Click" runat="server" />
                <input type="button" id="btnLook" disabled="disabled" value="查看" />
                <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="101" BusiClass="001" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div class="pagediv">
                    <asp:GridView ID="gvConract" AutoGenerateColumns="false" CssClass="gvdata" OnPageIndexChanging="gvPurchaseplan_PageIndexChanging" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="PaymentId,Project" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                    <asp:CheckBox ID="cbAllBox" runat="server" />
                                </HeaderTemplate><ItemTemplate>
                                    <asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("PaymentId")) %>' runat="server" />
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="支付编号"><ItemTemplate>
                                    <a href="#" class="al" onclick="viewopen('/ContractManage/PaymentApply/PaymentApplyQuery.aspx?t=1&ic=<%# Eval("PaymentId") %>&contractId=<%# Eval("ContractId") %>')">
                                        <%# Eval("PaymentCode") %></a>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="要求支付日期"><ItemTemplate>
                                    <%# Common2.GetTime(Eval("PaymentDate").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申请人"><ItemTemplate>
                                    <%# Eval("PaymentPenson") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="本次支付金额" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                    <%# Eval("PaymentAmount") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                                    <%# Common2.GetState(Eval("ApplyFlowState").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
                                    <%# GetAnnx(Convert.ToString(Eval("PaymentId"))) %>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    <asp:HiddenField ID="hdContractID" runat="server" />
    <asp:HiddenField ID="hldfIsExamineApprove" runat="server" />
    </form>
</body>
</html>
