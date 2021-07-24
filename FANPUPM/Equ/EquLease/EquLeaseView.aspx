<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquLeaseView.aspx.cs" Inherits="Equ_EquLease_EquLeaseView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>查看设备租赁</title>
    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr style="height: 1px;">
            <td class="divHeader">
                查看设备租赁
                <input type="button" id="btnDy" style="float: right;" onclick="winPrint()" value=" 打 印 " />
            </td>
        </tr>
        <tr style="height: 1px;">
            <td style="vertical-align: top;">
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
                <table border="0px" cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            租赁单号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblCode" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            设备调配计划单号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblDeployPlan" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            设备
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEquipmentCode" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            租赁方式
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblLeaseType" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            租用单位
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblACorpName" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            出租单位
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblBCorpName" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            起租日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblStartDate" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            停租日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEndDate" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            租用时长
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblDuration" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            租用合同
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblContract" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            租用原因
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblReason" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            租用费用（元）
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblCost" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            责任人
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblChargeMan" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            备注
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblNote" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; height: 50%; margin-top: 10px;" border="0">
                    <tr id="trAudit" style="vertical-align: top;">
                        <td>
                            <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="154" BusiClass="001" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    </form>
</body>
</html>
