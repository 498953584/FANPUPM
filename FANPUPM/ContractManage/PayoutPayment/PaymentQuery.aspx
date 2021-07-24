<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentQuery.aspx.cs" Inherits="ContractManage_PayoutPayment_PaymentQuery" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
        table tr
        {
            border-color: Black;
            background-color: Black;
        }
        .fontsize
        {
            font-size: 13px;
        }
    </style>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if (!document.getElementById('gvBudget')) {
                document.getElementById('trKzzb').style.display = 'none';
            }
            if (document.getElementById('hldfIsFundPlan').value == '0') {
                document.getElementById('tr1').style.display = 'none';
                document.getElementById('tr2').style.display = 'none';
                document.getElementById('tr3').style.display = 'none';
                document.getElementById('tr4').style.display = 'none';
            } else {
                var url = window.parent.location.href;
                if (url.indexOf('PayoutPayment') == -1) {
                    changeUsableMoney();
                }
            }
            registerChkDiffEvent();
        });
        function changeUsableMoney() {
            var paymentMoney = document.getElementById('lblPaymentMoney').innerText;
            var usableMoney = document.getElementById('lblUsableMoney').innerText;
            if (parseFloat(paymentMoney) > parseFloat(usableMoney)) {
                document.getElementById('lblPaymentMoney').style.color = "red";
                if (parseFloat(usableMoney) < 0) {
                    document.getElementById('lblUsableMoney').style.color = "red";
                } else {
                    document.getElementById('lblUsableMoney').style.color = "black";
                }
            } else {
                if (parseFloat(usableMoney) < 0) {
                    document.getElementById('lblUsableMoney').style.color = "red";
                } else {
                    document.getElementById('lblUsableMoney').style.color = "black";
                }
                document.getElementById('lblPaymentMoney').style.color = "black";
            }
        }

        function registerChkDiffEvent() {
            if (!document.getElementById('chkDiff')) return false;
            if (!document.getElementById('trSate')) return false;
            addEvent(document.getElementById('chkDiff'), 'click', function () {
                if (this.checked) {
                    document.getElementById('trSate').className = '';
                }
                else {
                    document.getElementById('trSate').className = 'noprint';
                }
            });
        }
    </script>
    <script type="text/javascript">
        function printURL() {
            var paymentId = document.getElementById('hfldPaymentId').value;
            var ContractId = document.getElementById('hldfContractId').value;
            var url = '/ContractManage/PayoutPayment/PaymentQuery2.aspx?Action=Query&PaymentId=' + paymentId + '&ic=' + paymentId + '&ContractId=' + ContractId;
            viewopen(url);
        }
    </script>
 
</head>
<body id="print">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr>
            <td class="divHeader">
                支出合同付款
                    <input type="button" id="btnPrint" style="float:right;text-align:center;width:65px" value="财务打印" onclick="printURL()"/>
                    <input type="button" id="btnDy" style="float:right; text-align:center" class="noprint" onclick="winPrint()"
                    value=" 打 印 " />
            </td>
        </tr>
        <tr style="height: 1px;">
            <td>
                <table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style: none;"
                    class="viewTable">
                    <tr>
                        <td style="border-style: none;">
                            制单人:&nbsp;&nbsp;<asp:Label ID="lblBllProducer" runat="server"></asp:Label>
                        </td>
                        <td style="border-style: none; text-align: right">
                            打印日期:&nbsp;&nbsp;<asp:Label ID="lblPrintDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td style="vertical-align: top;">
                <table border="0px" cellpadding="0" cellspacing="0" class="viewTable" style="width: 100%;
                    margin: auto;">
                    <tr>
                        <td colspan="4" class="groupInfo">
                            合同基本信息
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            项目编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="LblProjectCode" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            项目名称
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="LblProjectName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            合同编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblContractCode" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            合同名称
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblContractName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            合同最终金额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblContractMoney" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            签订时间
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblSignedDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="tr1" class="groupInfo">
                        <td colspan="4" class="groupInfo">
                            资金计划
                        </td>
                    </tr>
                    <tr id="tr2">
                        <td class="descTd">
                            计划月份
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblMonthDate" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            计划金额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPlanMoney" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="tr3">
                        <td class="word">
                            计划已用金额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblUsedMoney" runat="server"></asp:Label>
                        </td>
                        <td class="word">
                            计划可用金额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblUsableMoney" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="tr4">
                        <td class="word">
                            备注
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblRemark" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="groupInfo">
                            付款单信息
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            结算累计
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblBalancedSum" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            付款累计
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPaySum" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            差额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblDiff" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            支付编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPaymentCode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            本次支付金额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPaymentMoney" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            大写金额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="LblCapitalNumber" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            要求支付日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPaymentDate" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            申请人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPaymentPerson" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            收款单位
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblBeneficiary" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            开户行
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblBank" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            账户
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lalAccount" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            备注
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblNotes" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            附件
                        </td>
                        <td style="padding-right: 0px;">
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd">
                            支付方式
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="LblPayType" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            录入人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblInputPerson" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            录入时间
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblInputDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trSate" style="height: 1px;" runat="server"><td colspan="4" runat="server">
                <div>
                    <div id="diffTitle" style="position: relative;" runat="server">
                        指标对比表
                        <div class="noprint" style="height: 20px; width: 100%; left: 0px; bottom: 0px; text-align: right;
                            position: absolute; font-weight: normal;">
                            <asp:CheckBox ID="chkDiff" Style="float: right;" Checked="true" Text="打印" runat="server" />
                        </div>
                    </div>
                    <table cellpadding="0" cellspacing="0" class="viewTable" style="width: 100%; margin: auto;">
                        <tr>
                            <td align="center">
                                状态
                            </td>
                            <td align="center">
                                合同金额
                            </td>
                            <td align="center">
                                已结算金额
                            </td>
                            <td align="center">
                                已支付金额
                            </td>
                            <td align="center">
                                本次支付金额
                            </td>
                            <td align="center">
                                已支付金额/合同金额
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblState" CssClass="alarm" Text="" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lblContractAmount" CssClass="alarm" Text="" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lblBalancedAmount" CssClass="alarm" Text="" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lblPaymentedAmount" CssClass="alarm" Text="" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lblPaymentAmount" CssClass="alarm" Text="" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lblRate" CssClass="alarm" Text="" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="text-align: left">
                                <div style="width: 12%; float: left">
                                    预警级别：
                                </div>
                                <div style="width: 88%; float: left">
                                    高：红色字体，表示已超；中：紫色字体；低：蓝色字体；
                                    <br />
                                    普通：黑色字体，表示未达到预警阀值，正常。
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td></tr>
        <tr id="trKzzb" style="vertical-align: top; height: 1px;">
            <td style="vertical-align: top; padding-top: 10px;">
                <div style="font-size: 13px; font-weight: bold; text-align: center;">
                    <asp:Label ID="lblTitlTarget" Font-Size="13px" Font-Bold="true" runat="server"></asp:Label></div>
                <asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" Width="100%" DataKeyNames="TargetId" runat="server"><Columns><asp:TemplateField HeaderText="序号"><ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="名称"><ItemTemplate>
                                <%# Eval("TaskName") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="总金额">
<ItemTemplate>
                                <%# Convert.ToDecimal(Eval("BudTotal")).ToString("0.000") %>
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合同签订金额">
<ItemTemplate>
                                <%# Convert.ToDecimal(Eval("SignAmount")).ToString("0.000") %>
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="已支付金额">
<ItemTemplate>
                                <%# Convert.ToDecimal(Eval("PaymentedAmount")).ToString("0.000") %>
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="本次支付金额">
<ItemTemplate>
                                <%# Convert.ToDecimal(Eval("CurrentPaymentAmount")).ToString("0.000") %>
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="已支付金额<br/>/合同签订金额">
<ItemTemplate>
                                <%# Convert.ToDecimal(Eval("Rate")).ToString("P2") %>
                            </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
            </td>
        </tr>
        <tr id="trAudit" style="vertical-align: top;">
            <td colspan="4">
                <div>
                    <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="084" BusiClass="001" runat="server" />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr style="height: 25px; display: none;">
                        <td class="fontsize">
                            领款人签名：
                        </td>
                        <td class="fontsize">
                            日期：
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="border-bottom: dashed 1px black;">
                        </td>
                    </tr>
                    <tr style="height: 50px">
                        <td class="fontsize">
                            回执
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td class="fontsize">
                            分包商/供应商：
                        </td>
                        <td class="fontsize">
                            金额：
                        </td>
                        <td class="fontsize">
                            日期：
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    <asp:HiddenField ID="hldfIsFundPlan" runat="server" />
    <asp:HiddenField ID="hldfContractId" runat="server" />
    <asp:HiddenField ID="hfldPaymentId" runat="server" />
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    <asp:HiddenField ID="hfldPaymentIds" runat="server" />
    
    </form>
</body>
</html>
