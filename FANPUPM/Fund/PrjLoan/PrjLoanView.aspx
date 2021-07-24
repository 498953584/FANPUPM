<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjLoanView.aspx.cs" Inherits="PrjLoanView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>
    <style type="text/css">
        table tr
        {
            border-width: 1px;
            border-color: #000000;
        }
        .fontsize
        {
            font-size: 13px;
        }
        a
        {
            color: #0066FF;
            text-decoration: none;
        }
    </style>
    <script language="javascript" type="text/javascript">
        // <!CDATA[
        $(function () {
            //$("td").attr("style", "white-space:nowrap;");
            // setAnnxReadOnly('flAnnx');
            $("#showaudit1_chkAudit").click(function () {
                var flag = $(this).attr("checked");
                if (flag) {
                    $("#trAudit").removeClass("noprint");
                } else {
                    $("#trAudit").addClass("noprint");
                }
            });
        });
        //取消
                 function btnCancel_onclick() {
                 	top.ui.closeTab();
        }
        // ]]>
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr style="height: 1px;">
            <td style="vertical-align: top;">
                <div class="divHeader" style="height: 28px; line-height: 28px;">
                    项目借款单
                    <div style="height: 20px; width: 100%; left: 0px; top: 2px; text-align: right; position: absolute;">
                        <input id="Button1" style="float: right;" class="noprint" onclick="btnCancel_onclick()"
                            type="button" value="关闭" />
                        &nbsp;&nbsp;<input type="button" style="float: right;" class="noprint" onclick="winPrint()"
                            value=" 打 印 " />&nbsp;&nbsp;
                    </div>
                </div>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td style="vertical-align: top;">
                <table cellpadding="0" cellspacing="0" style="border-style: none; margin-left: 1px;"
                    class="viewTable">
                    <tr>
                        <td style="border-style: none;">
                            制单人：
                            <asp:Literal ID="ltadduser" runat="server"></asp:Literal>
                        </td>
                        <td style="border-style: none;" align="right">
                            打印日期：
                            <asp:Literal ID="ltadddate" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td style="vertical-align: top;">
                <table border="0px" cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            借款单号
                        </td>
                        <td>
                            <asp:Literal ID="ltrAccountNum" runat="server"></asp:Literal>
                        </td>
                        
                        <td class="descTd" style="white-space: nowrap;">
                            借款人
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="ltrLoanMan" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            项目
                        </td>
                        <td>
                            <asp:Literal ID="ltrprjname" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            流程状态
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="ltrFlowState" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            借款金额
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="ltrLoanFund" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            借款用途
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="ltrLoanCause" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            借款日期
                        </td>
                        <td>
                            <asp:Literal ID="ltrLoanDate" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            借款利率
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="ltrLoanRate" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            约定归还日期
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="ltrPlanReturnDate" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                        </td>
                        <td class="elemTd" style="white-space: nowrap;">
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            附件
                        </td>
                        <td class="elemTd" id="upload" runat="server">
                            
                        </td>
                        <td class="">
                        </td>
                        <td class="">
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            备注
                        </td>
                        <td class="elemTd" style="vertical-align: top;" colspan="3">
                            <div>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Literal ID="ltrRemark" runat="server"></asp:Literal>
                            </div>
                        </td>
                    </tr>
                    <tr id="trrepayment" style="display: none;" runat="server"><td class="descTd" runat="server">
                            &nbsp;&nbsp;还款情况：
                        </td><td class="elemTd" colspan="3" runat="server">
                            &nbsp;&nbsp;<asp:Label ID="lbRepayment" Text="Ladddbel" runat="server"></asp:Label>
                        </td></tr>
                </table>
            </td>
        </tr>
        <tr id="trAudit">
            <td>
                <MyUserControl:stockmanage_common_showaudit_ascx ID="showaudit1" BusiCode="095" BusiClass="001" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
