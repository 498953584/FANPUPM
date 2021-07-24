<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountPayoutView.aspx.cs" Inherits="Fund_AccountPayout_AccountPayoutView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>入账查看</title>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            //setAnnxReadOnly('flAnnx');
        });
    </script>
</head>
<body id="print">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr>
            <td class="divHeader">
                入账查看
                <div style="height: 20px; width: 100%; left: 0px; top: 0px; text-align: right; position: absolute;">
                    <input id="btnPrnt" type="button" style="float: right;" class="noprint" onclick="winPrint()" value=" 打 印 " runat="server" />

                </div>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td>
                <table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style: none;" class="viewTable" runat="server"><tr runat="server"><td style="border-style: none;" runat="server">
                            制单人:&nbsp;&nbsp;<asp:Label ID="lblPrintPeople" runat="server"></asp:Label>
                        </td><td style="border-style: none; text-align: right" runat="server">
                            打印日期:&nbsp;&nbsp;<asp:Label ID="lblPrintDate" runat="server"></asp:Label>
                        </td></tr></table>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td style="vertical-align: top;">
                <table border="0px" cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            入账单编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblcode" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            项目
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblProject" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            <asp:Label ID="lblWord" Text="合同支付编号" runat="server"></asp:Label>
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblContPayCode" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            应支付金额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblContMoney" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            已记账金额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPayMoney" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            未记账金额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lbljianMoney" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            账户余额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblAccountYue" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            所属合同
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblContractName" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            本次记账金额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblInMoney" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            经手人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblHandler" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            附件
                        </td>
                        <td class="elemTd" id="upload" colspan="3" runat="server">
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            入账人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblInPeople" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            入账日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblInDate" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            备注
                        </td>
                        <td colspan="3" style="word-break: break-all;">
                            <asp:Label ID="lblRemark" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; height: 50%; margin-top: 10px;" border="0">
                    <tr id="trSate" style="height: 1px;" runat="server"><td runat="server">
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
                                        <td colspan="5" style="text-align: left">
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
                    <tr id="trAudit" style="vertical-align: top;">
                        <td>
                            <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="093" BusiClass="001" runat="server" />
                        </td>
                    </tr>
                </table>
                <input id="hdnCode" type="hidden" runat="server" />

            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    </form>
    <input type="hidden" id="hdnAccountID" runat="server" />

</body>
</html>
