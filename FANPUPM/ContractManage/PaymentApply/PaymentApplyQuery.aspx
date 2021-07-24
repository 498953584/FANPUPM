<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentApplyQuery.aspx.cs" Inherits="ContractManage_PaymentApply_PaymentApplyQuery" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
      <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
        table tr
        {
            border-color: Black;
            background-color: Black;
        }
        .fontsize
        {
        	font-size:13px;
        	}
    </style>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
</head>
<body id="print">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
            <tr>
                <td class="divHeader">
                    收入合同资金支付
                    <div style="height: 20px; width: 100%; left: 0px; top: 0px; text-align: right; position: absolute;">
                        <input type="button" id="btnDy" style="float: right;" class="noprint" onclick="winPrint()" value=" 打 印 " />
                    </div>
                </td>
            </tr> 
            <tr style="height:1px;">
                <td>
                    <table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style:none;" class="viewTable">
                        <tr>
                            <td style="border-style:none;">
                                制单人:&nbsp;&nbsp;<asp:Label ID="lblBllProducer" runat="server"></asp:Label>
                            </td>
                            <td style="border-style:none; text-align:right">
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
                    合同基本信息
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    合同编号
                </td>
                <td class="elemTd">
                    <asp:Label ID="lblContractCode" runat="server"></asp:Label>
                </td>
                <td class="descTd">
                    合同名称
                </td>
                <td class="elemTd">
                    <asp:Label ID="lblContractName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    合同最终金额
                </td>
                <td class="elemTd">
                    <asp:Label ID="lblContractMoney" runat="server"></asp:Label>
                </td>
                <td class="descTd">
                    签订时间
                </td>
                <td class="elemTd">
                    <asp:Label ID="lblSignedDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="groupInfo">
                    付款单信息
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    收款累计
                </td>
                <td class="elemTd">
                    <asp:Label ID="lblBalancedSum" runat="server"></asp:Label>
                </td>
                <td class="descTd">
                    付款累计
                </td>
                <td class="elemTd">
                    <asp:Label ID="lblPaySum" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    差额
                </td>
                <td class="elemTd">
                    <asp:Label ID="lblDiff" runat="server"></asp:Label>
                </td>
                <td class="descTd">
                    支付编号
                </td>
                <td class="elemTd">
                    <asp:Label ID="lblPaymentCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    本次支付金额
                </td>
                <td class="elemTd">
                    <asp:Label ID="lblPaymentMoney" runat="server"></asp:Label>
                </td>
                <td class="descTd">
                            大写金额
                </td>
                <td class="elemTd">
                    <asp:Label ID="LblCapitalNumber" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    要求支付日期
                </td>
                <td class="elemTd">
                    <asp:Label ID="lblPaymentDate" runat="server"></asp:Label>
                </td>
                <td class="descTd">
                    申请人
                </td>
                <td class="elemTd">
                    <asp:Label ID="lblPaymentPerson" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    备注
                </td>
                <td colspan="3">
                    <asp:Label ID="lblNotes" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    附件
                </td>
                <td style="padding-right: 0px;" >
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </td>
                <td class="descTd">
                    支付方式
                </td>
                <td class="elemTd">
                    <asp:Label ID="LblPayType" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    录入人
                </td>
                <td class="elemTd">
                    <asp:Label ID="lblInputPerson" runat="server"></asp:Label>
                </td>
                <td class="descTd">
                    录入时间
                </td>
                <td class="elemTd">
                    <asp:Label ID="lblInputDate" runat="server"></asp:Label>
                </td>
            </tr>
            
        </table>
        </td>
    </tr>
            <tr id="trAudit" style="vertical-align: top;">
                <td colspan="4">
                <div>
                    <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="101" BusiClass="001" runat="server" />
                </div>
                </td>
            </tr> 
    </table>  
    
    </form>
</body>
</html>
<script  language="javascript" type="text/javascript">
</script>
