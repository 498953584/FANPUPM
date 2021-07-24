<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicationView.aspx.cs" Inherits="HR_Leave_ApplicationView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>请假查看</title>
    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr style="height: 1px;">
            <td class="divHeader">
                请假查看
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
                            流程发起人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="LbUserName" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            请(休)假类型
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="LbLeaveType" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            请休假开始时间
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="LbBeginDate" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            销假时间
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="LbBackDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            请假天数
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="LbDays" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            实际离开天数
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="LbLeaveDays" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            请假事由
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="LbReason" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; height: 50%; margin-top: 10px;" border="0">
                    <tr id="trAudit" style="vertical-align: top;">
                        <td>
                            <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="004" BusiClass="001" runat="server" />
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
