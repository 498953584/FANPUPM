<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewShowPayment.aspx.cs" Inherits="ContractManage_IncometPayment_ViewShowPayment" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>合同收款</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
</head>
<body id="print">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr>
            <td class="divHeader">
                收入合同合同收款
                <input type="button" id="btnDy" style="float: right;" class="noprint" onclick="winPrint()"
                    value=" 打 印 " />
            </td>
        </tr>
        <tr>
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
        <tr>
            <td style="vertical-align: top;">
                <table class="viewTable" style="height: 100%;" cellpadding="5px" cellspacing="0">
                    <tr>
                        <td colspan="4" class="groupInfo">
                            合同基本信息
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
                            合同金额
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
                    <tr>
                        <td class="descTd">
                            项目编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPrjCode" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            项目名称
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPrjName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="groupInfo">
                            资金计划
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            计划月份
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPlanMonth" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            计划金额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPlanMoney" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            计划已完成金额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblFinshMoney" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            计划未完成金额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblAllowCollectMoney" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            备注
                        </td>
                        <td colspan="3" style="height: 30">
                            <asp:Label ID="lblPlanote" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="groupInfo">
                            收款信息
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            收款累计
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPaymentSum" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            差额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lbldiff" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            收款编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPaymentCode" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            收款人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPaymentUser" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            收款金额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPayMoney" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            收款日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPayTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            备注
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblnote" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            录入人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblInputUser" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            录入时间
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblInputTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            附件
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblUpFiled" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
