<%@ Page Language="C#" AutoEventWireup="true" CodeFile="prjReturnView.aspx.cs" Inherits="Fund_prjReturn_prjReturnView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script src="../../StockManage/script/Config2.js" type="text/javascript"></script>

</head>
<body id="print">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr>
            <td class="divHeader">
                账户还款信息
                <div style="height: 20px; width: 100%; left: 0px; top: 0px; text-align: right; position: absolute;">
                    <input type="button" id="btnDy" style="float: right;" class="noprint" onclick="winPrint()"
                        value=" 打 印 " />
                </div>
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
                <table border="0px" cellpadding="0" cellspacing="0" class="viewTable" style="width: 100%;
                    margin: auto;">
                    <tr>
                        <td colspan="4" class="groupInfo">
                            借款单基本信息
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            借款单号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="txtLoanID" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            约定还款日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="txtLoanDate" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            借款利率
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="txtLoanRate" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            借款金额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="txtLoanFund" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            已还款本金
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="txtReturnMoney" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            未还本金
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="txtNoRetMoney" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            已还利息
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="txtReturnInterest" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            已还其他
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="txtReturnOther" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="groupInfo">
                            还款单信息
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            还款单号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="txtCode" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            归还人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="txtPeople" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            归还日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="txtData" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            归还本金
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="txtMoney" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            归还利息
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="txtInterest" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            其他扣款
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="txtDeduct" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            是否还清
                        </td>
                        <td class="elemTd">
                            
                            <asp:Label ID="labType" Text="Label" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                        </td>
                        <td class="elemTd">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="white-space: nowrap;">
                            备注
                        </td>
                        <td colspan="3">
                            <asp:Label ID="txtremark" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            填报人
                        </td>
                        <td class="txt">
                            <asp:Label ID="txtUser" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            填报日期
                        </td>
                        <td class="txt" style="padding-right: 1px">
                            <asp:Label ID="txtTime" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trAudit" style="vertical-align: top;">
            <td>
            <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="102" BusiClass="001" runat="server" />
            </td>
        </tr> 
    </table>
   <asp:HiddenField ID="hdfReturnId" runat="server" />
    </form>
</body>
</html>
