<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquDeployPlanView.aspx.cs" Inherits="Equ_EquDeployPlan_EquDeployPlanView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>设备调配计划查看</title>
    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr style="height: 1px;">
            <td class="divHeader">
                船机调配计划查看
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
                            计划编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPlanCode" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            设备需求计划编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblRequirePlanCode" Text="" runat="server"></asp:Label>
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
                            <asp:Label ID="lblTaskId" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            挖深
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblSump" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            台班
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblMachineTeam" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            设备编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEquipmentCode" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="word">
                            设备来源
                        </td>
                        <td>
                            <asp:Label ID="lblEquSource" Width="100%" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            规格型号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblSpecification" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            本月预算工程量
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblBudQuantity" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            本月预算油耗
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblBudOilWear" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            预计进场时间
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblStartDate" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            预计进场地点
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblStartPlace" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            预计出场时间
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEndDate" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            预计出场地点
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEndPlace" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            调出部门
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblOutDepartment" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            调入部门
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblInDepartment" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            申请日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblApplyDate" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            最迟到位日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblArriveDate" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            设备保养状态
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblMaintenanceState" Text="" runat="server"></asp:Label>
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
                            <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="146" BusiClass="001" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
