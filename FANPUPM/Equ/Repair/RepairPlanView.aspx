<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RepairPlanView.aspx.cs" Inherits="Equ_Repair_RepairPlanView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>维修申请查看</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#chkIsEntrustPurchase').attr('disabled', 'disabled');
        });
    </script>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr style="height: 1px;">
            <td class="divHeader">
                维修申请查看
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
                            维修计划编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblCode" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            设备编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEquCode" runat="server"></asp:Label>
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
                            计划维修内容
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblRepairContent" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            计划开始日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblRepairStartDate" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            计划结束日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblRepairEndDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            计划维修费用
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblCost" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            维修方式
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblRepairType" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            申请部门
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            维保标识
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblRMFlag" runat="server"></asp:Label>
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
                <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiClass="001" BusiCode="145" runat="server" />
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    </form>
</body>
</html>
