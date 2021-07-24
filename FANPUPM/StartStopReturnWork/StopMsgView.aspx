<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="StopMsgView.aspx.cs" Inherits="StartStopReturnWork_StopMsgView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>停工通知单</title>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>
    <style type="text/css">
        #bllProducer
        {
        }
        #bllProducer td
        {
        }
    </style>
    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="../StockManage/script/Config2.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
        });
    </script>
</head>
<body id="print">
    <form id="form2" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr>
            <td class="divHeader">
                停工通知单
                <input type="button" id="Button1" style="float: right;" class="noprint" onclick="winPrint()"
                    value=" 打 印 " />
            </td>
        </tr>
        <tr style="height: 1px;">
            <td>
                <table id="Table1" cellpadding="0" cellspacing="0" style="border-style: none;" class="viewTable">
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
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            附件
                        </td>
                        <td class="td-content" id="FileUpload1" colspan="3" runat="server">
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            工程名称
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtPrjName" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd">
                            施工地段
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtConstArea" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            施工单位
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtConstructionUnit" runat="server"></asp:Literal>
                        </td>
                         <td class="descTd">
                            工程里程
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtProjectMileage" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            停工日期
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Literal ID="txtStopDate" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        
                        <td class="elemTd" colspan="4" style=" height:80px;">
                            <asp:Literal ID="txtStopReason" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            停工主要内容
                        </td>
                        <td colspan="3" class="elemTd">
                            <asp:Literal ID="txtMainContent" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                        工程产生问题
                        </td>
                        <td colspan="3" class="elemTd">
                            <asp:Literal ID="txtProjectProblem" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            产生问题的原因
                        </td>
                        <td colspan="3" class="elemTd">
                            <asp:Literal ID="txtProblemReason" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            影响及预计损失程度
                        </td>
                        <td colspan="3" class="elemTd">
                            <asp:Literal ID="txtImpactLossDegree" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="descTd">
                            建议补救措施
                        </td>
                        <td colspan="3" class="elemTd">
                            <asp:Literal ID="txtRemedialMeasure" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            监理工程师
                        </td>
                        <td class="elemTd" style=" border-right-width:0px;">
                            <asp:Literal ID="txtSupervisorSign" runat="server"></asp:Literal>
                        </td>
                        <td colspan="2" class="elemTd" align="right" style=" border-left-width:0px;">
                            <asp:Literal ID="txtSupervisorSignDate" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="descTd">
                            总监理工程师
                        </td>
                        <td class="elemTd"  style=" border-right-width:0px;">
                            <asp:Literal ID="txtGeneralSign" runat="server"></asp:Literal>
                        </td>
                        <td colspan="2" class="elemTd" align="right" style=" border-left-width:0px;">
                            <asp:Literal ID="txtGeneralSignDate" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trAudit" style="vertical-align: top;">
            <td>
                <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="127" BusiClass="001" runat="server" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfldPrjState" runat="server" />
    <asp:HiddenField ID="hfldStartReportId" runat="server" />
    </form>
</body>
</html>
