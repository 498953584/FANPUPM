<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquOilOutView.aspx.cs" Inherits="Equ_OilWearManage_EquOilOutView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>查看出库单</title>
    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr style="height: 1px;">
            <td class="divHeader">
                查看出库单
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
        <tr style="height: 1px;">
            <td style="vertical-align: top;">
                <table border="0px" cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            入库单号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEnterCode" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            出库单号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblOutCode" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            项目
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblProject" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            分项工程
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTask" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            出库日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblOutDate" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            签收人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblSignInUser" Text="" runat="server"></asp:Label>
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
                            租用类型
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblHireType" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            外租合同
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblHireContract" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            是否甲供
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblIsAsupply" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            甲供合同
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblAsupplyContract" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            结算方式
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblBalanceMode" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            单价（元/L）
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblUnitPrice" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            数量（L）
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblQuantity" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            库管员
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblStoreKeeper" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; height: 50%; margin-top: 10px;" border="0">
                    <tr id="trAudit" style="vertical-align: top;">
                        <td>
                            <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="149" BusiClass="001" runat="server" />
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
