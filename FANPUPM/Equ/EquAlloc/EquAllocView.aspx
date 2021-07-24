<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquAllocView.aspx.cs" Inherits="Equ_EquAlloc_EquAllocView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>查看设备调拨</title>
    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr style="height: 1px;">
            <td class="divHeader">
                查看设备调拨
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
                            设备调配计划单号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblDeployPlan" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            设备编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEquipmentCode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            设备名称
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEquName" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            规格
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblSpecification" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            资产原值
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPurchasePrice" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            购置日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPurchaseDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            船机设备部负责人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblShipEquChargeMan" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            调出部门
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblCalloutDep" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            调出部门设备管理员
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblCallouEquAdmin" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            调出部门负责人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblCalloutEquChargeMan" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            调入部门
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblCallinDep" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            调入部门设备管理员
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblCallinEquAdmin" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            调入单位负责人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblCallinEquChargeMan" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            设备状态
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEquState" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            接收人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblReceiver" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            调拨日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblAllocDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; height: 50%; margin-top: 10px;" border="0">
                    <tr id="trAudit" style="vertical-align: top;">
                        <td>
                            <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="156" BusiClass="001" runat="server" />
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
