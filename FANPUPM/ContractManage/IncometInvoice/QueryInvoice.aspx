<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryInvoice.aspx.cs" Inherits="ContractManage_IncometInvoice_QueryInvoice" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>合同发票</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
</head>
<body id="print">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr>
            <td class="divHeader">
                收入合同发票
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
                            发票信息
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            累计收款金额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPaymentSum" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            累计已开发票
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblInvoiceSum" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            累计未开发票
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblBalance" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            本次开票金额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblAmountMoney" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            发票号码
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblInvoiceNo" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            发票类型
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblInvoiceType" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            收款单位
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblSecond" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            付款单位
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblParty" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            纳税人识别号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTaxNo" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            发票代码
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblInvoiceCode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            办理人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTransactor" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            开票日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblInvoiceDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            组织机构代码证号码
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblOrganizationCode" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            地址、电话
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblAddress" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            开户行及账号
                        </td>
                        <td colspan="3" class="elemTd">
                            <asp:Label ID="lblBankCode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            备注
                        </td>
                        <td colspan="4">
                            <asp:Label ID="lblNote" runat="server"></asp:Label>
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
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
