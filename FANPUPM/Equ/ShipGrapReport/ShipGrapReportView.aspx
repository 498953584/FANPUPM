<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShipGrapReportView.aspx.cs" Inherits="Equ_ShipProductionReport_ShipProductionReportView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>查看抓斗船上报</title>
    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr style="height: 1px;">
            <td class="divHeader">
                查看抓斗船上报
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
                            上报日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblReportDate" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            施工日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblConstructionDate" Text="" runat="server"></asp:Label>
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
                            项目区域
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblConstructionPlace" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            抓斗船
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEquCode" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            规格型号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblSpecification" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            产量
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblQty" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            施工时长（小时）
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblConstructionDuration" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            配套泥驳船
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblMudShipCode" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            泥驳船舱容
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblCabinCapacity" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            装驳开始时间
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblStartDate" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            装驳结束时间
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEndDate" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            装驳时长
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblMudDuration" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            扣方
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblDeductQuantity" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            泥驳总方量
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblMudTotalQuantity" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            开单员
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblBillingUser" Text="" runat="server"></asp:Label>
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
                            <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="144" BusiClass="001" runat="server" />
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
