<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StateChangeQuery.aspx.cs" Inherits="TenderManage_StateChangeQuery" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/DecimalInput.js"></script>
</head>
<body id="print">
    <form id="form1" runat="server">
     <table class="tab" style="vertical-align: top; border-collapse: collapse;">
        <tr>
            <td style="vertical-align: top">
                <div class="groupInfo" style="text-align: center; margin-bottom: 5px;">
                    项目状态变更
                </div>
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            项目编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPrjCode" runat="server"></asp:Label>
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
                            计划开始日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            计划结束日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            调整人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblChangeMan" runat="server"></asp:Label>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            调整时间
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblChangeTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            项目状态
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblOldState" runat="server"></asp:Label>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            调整状态
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblChangeState" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                           调整原因
                        </td>
                        <td colspan="3">
                            <div style="width: 95%; word-break: break-all;">
                                <asp:Label ID="lblprjChangeReason" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            备注
                        </td>
                        <td colspan="3">
                            <div style="width: 95%; word-break: break-all;">
                                <asp:Label ID="lblPrjRemark" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
    </form>
</body>
</html>
