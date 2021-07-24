<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExplainPlan_1.aspx.cs" Inherits="oa_WorkPlanAndSummary_ExplainPlan" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title>
    <style type="text/css">
        .divFooter2
        {
            height: 24px;
            text-align: right;
            background: url(/images1/divBottomBack.jpg) repeat-x;
            vertical-align: middle;
            position: absolute;
            bottom: 0px;
            margin-bottom: 0px;
            width: 100%;
        }
        .tableFooter2
        {
            width: 100%;
            text-align: right;
            bottom: 0px;
        }
        .txt
        {
            width: 40%;
            text-align: left;
            border: 1px solid black;
        }
        .word
        {
            width: 10%;
            text-align: right;
            border: 1px solid black;
        }
        .sum
        {
            border-bottom: 1px solid black;
            border-right: 1px solid black;
        }
        .sumright
        {
            border-right: 1px solid black;
        }
        .toptable
        {
            border-top: 0px solid black;
            border-left: 1px solid black;
            border-right: 1px solid black;
            border-bottom: 1px solid black;
        }
        .plantoplbl
        {
            width: 40%;
            text-align: left;
            border-top: 0px solid black;
            border-left: 1px solid black;
            border-right: 1px solid black;
            border-bottom: 1px solid black;
        }
        .plantoptd
        {
            width: 10%;
            text-align: right;
            border-top: 0px solid black;
            border-left: 1px solid black;
            border-right: 1px solid black;
            border-bottom: 1px solid black;
        }
    </style>
    <script src="/Script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 95%; overflow: auto; width: 100%">
        <div>
            <table class="tab" style="vertical-align: top;">
                <tr>
                    <td class="divHeader" style="font-size: 16px;">
                        工作计划说明与总结
                    </td>
                </tr>
                <tr style="height: 30px;">
                    <td style="text-align: center;">
                        <table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style: none;"
                            width="80%">
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
            </table>
        </div>
        <div style="overflow: hidden;" align="center">
            <table cellpadding="5px" cellspacing="0" border="1px solid black" width="80%">
                <tr>
                    <td class="word">
                        计划编号
                    </td>
                    <td class="txt">
                        <asp:Label ID="lblCode" ReadOnly="true" Height="15px" runat="server"></asp:Label>
                    </td>
                    <td class="word">
                        填报日期
                    </td>
                    <td class="txt">
                        <asp:Label ID="lblDate" Height="15px" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="word">
                        填报人
                    </td>
                    <td class="txt">
                        <asp:Label ID="lblReportName" Height="15px" Enabled="true" runat="server"></asp:Label>
                    </td>
                    <td class="word">
                        部门名称
                    </td>
                    <td class="txt">
                        <asp:Label ID="lblPart" Height="15px" TextMode="MultiLine" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="word">
                        计划说明
                    </td>
                    <td class="txt" colspan="3" style="white-space: inherit;">
                        <div style="width: 100%; word-break: break-all;">
                            <asp:Label ID="lblPlanSumm" Height="15px" TextMode="MultiLine" runat="server"></asp:Label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="background-color: lightgray; height: 20px; border: 1px solid black;
                        font-size: 13px; font-weight: bold;" colspan="4">
                        各项计划详细介绍
                    </td>
                </tr>
            </table>
            <asp:DataList ID="dlData" Width="80%" CellPadding="0" CellSpacing="0" BorderStyle="None" runat="server">
<ItemTemplate>
                    <table border="0px solid #CADEED" width="99.9%" cellpadding="5" cellspacing="0" style="border-style: none;">
                        <tr>
                            <td rowspan="6" class="plantoplbl" style="width: 10%; vertical-align: middle; text-align: center;">
                                计划<%# Container.ItemIndex + 1%>
                            </td>
                        </tr>
                        <tr>
                            <td class="plantoptd">
                                开始时间
                            </td>
                            <td class="plantoplbl">
                                <asp:Label runat="server"><%# Convert.ToString(DateTime.Parse(Eval("wkpstarttime").ToString()).ToShortDateString()) %></asp:Label>
                            </td>
                            <td class="plantoptd">
                                结束时间
                            </td>
                            <td class="plantoplbl">
                                <asp:Label runat="server"><%# Convert.ToString(DateTime.Parse(Eval("wkpendtime").ToString()).ToShortDateString()) %></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="word">
                                负 责 人
                            </td>
                            <td class="txt">
                                <asp:Label runat="server"><%# Convert.ToString(Eval("wkpchief")) %></asp:Label>
                            </td>
                            <td class="word">
                                责 任 人
                            </td>
                            <td class="txt">
                                <asp:Label runat="server"><%# Convert.ToString(Eval("wkppersons")) %></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="word">
                                计划内容
                            </td>
                            <td colspan="3" style="word-break: break-all; text-align: left; border: solid 1px black;">
                                <asp:Label runat="server"><%# Convert.ToString(Eval("wkpcontents")) %></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="word">
                                总结时间
                            </td>
                            <td class="txt">
                                <asp:Label runat="server"><%# Convert.ToString((Eval("wkprecorddate") == "") ? " " : DateTime.Parse(Eval("wkprecorddate").ToString()).ToShortDateString()) %></asp:Label>
                            </td>
                            <td class="word">
                                完成比例
                            </td>
                            <td class="txt">
                                <asp:Label ID="Label1" runat="server"><%# Convert.ToString(Eval("wkppercent")) %></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="word">
                                总结内容
                            </td>
                            <td colspan="3" style="width: 90%; text-align: left; border: 1px solid black">
                                <div style="width: 100%; word-break: break-all;">
                                    <asp:Label ID="Label2" runat="server"><%# Convert.ToString(Eval("wkpsmcontents")) %></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
</asp:DataList>
        </div>
        <div align="center" style="margin-top: 0px;">
            <table width="80%" cellpadding="5" cellspacing="0" style="border-style: none;">
                <tr>
                    <td style="width: 10%; text-align: right;" class="toptable">
                        自 评 分
                    </td>
                    <td style="width: 90%; text-align: left;" class="toptable">
                        <asp:Label ID="lblScore" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%; text-align: right; border: 1px solid black">
                        总结说明
                    </td>
                    <td style="width: 90%; text-align: left; border: 1px solid black">
                        <div style="width: 100%; word-break: break-all;">
                            <asp:Label ID="lblSumm" runat="server"></asp:Label>
                        </div>
                    </td>
                </tr>
                <tr style="vertical-align: top;">
                    <td colspan="2">
                        <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiClass="001" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <input id="btnSave" type="button" onclick="this.style.display='none';document.getElementById('btnCancel').style.display='none';winPrint()"
                        value="打印" />
                    <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeTab();" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
