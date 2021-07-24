<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquOilEnterView.aspx.cs" Inherits="Equ_OilWearManage_EquOilEnterView" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>查看入库单</title>
    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr style="height: 1px;">
            <td class="divHeader">
                查看入库单
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
                            项目
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPrjName" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            合同
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblContract" CssClass="decimal_input" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            采购单
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPurchaseCode" CssClass="decimal_input" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            入库日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEnterDate" CssClass="decimal_input" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            签收人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblSignInUser" CssClass="decimal_input" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            单价（元/L）
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblUnitPrice" CssClass="decimal_input" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            数量（L）
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblQuantity" CssClass="decimal_input" Text="" runat="server"></asp:Label>
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
