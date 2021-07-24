<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjCompletedView.aspx.cs" Inherits="PrjManager_Completed_PrjCompletedView" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>项目竣工信息查看</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>
</head>
<body id="print">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top; border-collapse: collapse; height: auto;">
        <tr>
            <td class="divHeader">
                项目竣工信息
                <div style="height: 20px; width: 100%; left: 0px; top: 0px; text-align: right; position: absolute;">
                    <input type="button" style="float: right;" class="noprint" onclick="winPrint()" value=" 打 印 " />
                </div>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td>
                <table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style: none;
                    vertical-align: top;" class="viewTable">
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
        <tr style="vertical-align: top; height: 1px;">
            <td style="vertical-align: top;">
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
                            <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            开始日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            结束日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="tr_completedDate" visible="false" runat="server"><td class="descTd" runat="server">
                            竣工日期
                        </td><td class="elemTd" runat="server">
                            <asp:Label ID="lblCompletedDate" runat="server"></asp:Label>
                        </td><td class="descTd" runat="server">
                        </td><td class="elemTd" runat="server">
                        </td></tr>
                    <tr id="tr_completedNote" visible="false" runat="server"><td class="descTd" runat="server">
                            竣工备注
                        </td><td class="elemTd" id="td_completedNote" colspan="3" runat="server">
                        </td></tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
