<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoadMixerReportView.aspx.cs" Inherits="Equ_RoadMixerReport_RoadMixerReportView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>搅拌车上报查看</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr style="height: 1px;">
            <td class="divHeader">
                搅拌车上报查看
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
                            上报日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblReportDate" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            设备
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEquCode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            项目
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPrjName" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            分部分项
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTaskCode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            施工日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblConstructionDate" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            司机
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblDriver" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            车数
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTruckQty" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            出库方数
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblCubeQty" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            出场确认人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblExworksUser" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            现场确认方数
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblAffirmCubeQty" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            现场交接人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblAssociater" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            现场负责人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblChargeMan" runat="server"></asp:Label>
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
                <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiClass="001" BusiCode="162" runat="server" />
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    </form>
</body>
</html>
