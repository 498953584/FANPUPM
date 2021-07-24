<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SailorsAttendance.aspx.cs" Inherits="Equ_Report_Ship_SailorsAttendance" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>人员请假统计表</title><link type="text/css" rel="stylesheet" href="../../../Script/jquery.easyui/themes/default/easyui.css" />
<link type="text/css" rel="stylesheet" href="../../../Script/jquery.easyui/themes/icon.css" />
<link type="text/css" rel="stylesheet" href="../../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../../Script/jw.js"></script>
    <script type="text/javascript" src="../../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvwAttendance');
            replaceEmptyTable('emptyAttendance', 'gvwAttendance');
            $('#txtProjectName').attr('readonly', 'readonly');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="tab" border="1" style="border-collapse: collapse;" cellpadding="0"
        cellspacing="0">
        <tr style="height: 100%;">
            <td style="width: 200px; height: 99%" valign="top">
                <div class="pagediv" style="heigth: 100%; width: 195px;">
                    <div id="div_project">
                        <asp:TreeView ID="tvwDep" Font-Size="12px" ShowLines="true" OnSelectedNodeChanged="tvwDep_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                    </div>
                </div>
            </td>
            <td valign="top" style="height: 99%; padding-top: 0; padding-left: 0; padding-bottom: 0;
                margin-top: 0; margin-left: 0; margin-bottom: 0; margin: 0">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <table class="queryTable" cellpadding="3px" cellspacing="0px">
                                <tr>
                                    <td class="descTd">
                                        年份
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dropYear" Width="125px" runat="server"></asp:DropDownList>
                                    </td>
                                    <td class="descTd">
                                        月份
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dropMonth" Width="125px" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="divFooter" style="text-align: left;">
                            <asp:Button ID="btnSearch" Text="查询" Style="cursor: pointer;" OnClick="btnSearch_Click" runat="server" />
                            <asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <div class="pagediv" style="width: 100%">
                                <asp:GridView ID="gvwAttendance" AutoGenerateColumns="false" CssClass="gvdata" Width="100%" ShowFooter="true" OnRowDataBound="gvwAttendance_RowDataBound" runat="server">
<EmptyDataTemplate>
                                        <table id="emptyAttendance">
                                            <tr class="header">
                                                <th style="width: 25px;">
                                                    序号
                                                </th>
                                                <th>
                                                    姓名
                                                </th>
                                                <th>
                                                    1
                                                </th>
                                                <th>
                                                    2
                                                </th>
                                                <th>
                                                    3
                                                </th>
                                                <th>
                                                    4
                                                </th>
                                                <th>
                                                    5
                                                </th>
                                                <th>
                                                    6
                                                </th>
                                                <th>
                                                    8
                                                </th>
                                                <th>
                                                    9
                                                </th>
                                                <th>
                                                    10
                                                </th>
                                                <th>
                                                    11
                                                </th>
                                                <th>
                                                    12
                                                </th>
                                                <th>
                                                    13
                                                </th>
                                                <th>
                                                    14
                                                </th>
                                                <th>
                                                    15
                                                </th>
                                                <th>
                                                    16
                                                </th>
                                                <th>
                                                    17
                                                </th>
                                                <th>
                                                    18
                                                </th>
                                                <th>
                                                    19
                                                </th>
                                                <th>
                                                    20
                                                </th>
                                                <th>
                                                    21
                                                </th>
                                                <th>
                                                    22
                                                </th>
                                                <th>
                                                    23
                                                </th>
                                                <th>
                                                    24
                                                </th>
                                                <th>
                                                    25
                                                </th>
                                                <th>
                                                    26
                                                </th>
                                                <th>
                                                    27
                                                </th>
                                                <th>
                                                    28
                                                </th>
                                                <th>
                                                    29
                                                </th>
                                                <th>
                                                    30
                                                </th>
                                                <th>
                                                    31
                                                </th>
                                                <th>
                                                    请假总天数
                                                </th>
                                                <th>
                                                    签名
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                </webdiyer:AspNetPager>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 5px">
                            标示：<span style='color: Black; font-size: 15px; font-weight: bold;'>√</span>—出勤 <span
                                style='color: Black; font-size: 15px; font-weight: normal;'>╳</span>—请假—日期下总计为当天请假人数
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
