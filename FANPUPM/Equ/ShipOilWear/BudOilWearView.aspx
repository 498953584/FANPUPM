<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudOilWearView.aspx.cs" Inherits="Equ_ShipOilWear_BudOilWearView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>油耗预算查看</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#chkIsOutLease').attr('disabled', 'disabled');
        });
    </script>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr style="height: 1px;">
            <td class="divHeader">
                油耗预算查看
                <input type="button" id="btnDy" style="float: right;" class="noprint" onclick="winPrint()"
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
                <table class="viewTable" cellpadding="0px" cellspacing="0">
                    <tr>
                        <td class="descTd">
                            油耗预算编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblOilWearCode" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            项目名称
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPrjName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            分项工程
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTaskCode" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            合同预算油耗(KG/m³)
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblConBudOilWear" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            预算挖深(m)
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblSump" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            土质
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblSoilTexture" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            需求船型
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblDemandShipType" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            对外租赁
                        </td>
                        <td class="elemTd">
                            <asp:CheckBox ID="chkIsOutLease" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            预算开工日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            预算完工日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            施工地点
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblConstructionPlace" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            预算油单价(元/KG)
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblBudOilPrice" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            定额油耗(KG/m³)
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblQuotaOilWear" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            预算挖泥方量(m³)
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblBudQutity" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            预算挖泥油耗(KG/m³)
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblBudOilWear" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            定额油耗数量(KG)
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblQuotaOilWearQuantiy" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            预算油耗数量(KG)
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblBudOilWearQuantity" runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            备注
                        </td>
                        <td colspan="3" style="word-break: break-all;">
                            <asp:Label ID="lblNotes" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trAudit" style="vertical-align: top;">
            <td>
                <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiClass="001" BusiCode="142" runat="server" />
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    </form>
</body>
</html>
