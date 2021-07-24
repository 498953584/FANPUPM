<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Business_Data_ItemView.aspx.cs" Inherits="OPM_Business_Data_Business_Data_ItemView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>查看图纸信息</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
</head>
<body id="print">
    <form id="form1" runat="server">
    <div>
        <table class="tab" style="vertical-align: top; border-collapse: collapse; height: 200px">
            <tr>
                <td class="divHeader">
                    图纸信息
                    
                        <input type="button" style="float: right;" class="noprint" onclick="winPrint()" value=" 打 印 " />
                    
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
            <tr id="tr_tb" runat="server"><td style="vertical-align: top" runat="server">
                    <table cellpadding="0" cellspacing="0" class="viewTable">
                        <tr>
                            <td class="descTd">
                                图纸编号
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblPCode" runat="server"></asp:Label>
                            </td>
                            <td class="descTd">
                                图纸名称
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblPName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">
                                设计时间
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblDesignDate" runat="server"></asp:Label>
                            </td>
                            <td class="descTd">
                                计划完成时间
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">
                                责任人
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblDutyUser" runat="server"></asp:Label>
                            </td>
                            <td class="descTd">
                                设计人
                            </td>
                            <td ="descTd">
                                <asp:Label ID="lblDesigner" runat="server"></asp:Label>
                            </td></tr>
                        <tr>
                            <td class="descTd">
                                流程状态
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblFlowState" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">
                                附件
                            </td>
                            <td class="elemTd" id="upload" colspan="3" runat="server"></td>
                        </tr>
                        <tr>
                            <td class="descTd">
                                备注
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblRemark" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="tr_lb" runat="server"><td style="vertical-align: top" colspan="4" runat="server">
                    <table style="width: 100%; height: 50%; margin-top: 10px;" border="0">
                        <tr id="trAudit" style="vertical-align: top;">
                            <td>
                                <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="826" BusiClass="001" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td></tr>
        </table>
        <div id="divOpenAdjunct" title="查看附件" style="display: none;">
        <table id="annexTable" class="gvdata" style="width: 100%;" runat="server"><tr class="header" runat="server"><td style="width: 60%" runat="server">
                        名称
                    </td><td style="width: 30%" runat="server">
                        大小
                    </td><td runat="server">
                    </td></tr></table>
    </div>
    </div>
    <asp:HiddenField ID="hfldAdjunctPath" runat="server" />
    </form>
</body>
</html>
