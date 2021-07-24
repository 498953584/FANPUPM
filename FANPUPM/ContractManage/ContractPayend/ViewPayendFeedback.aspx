<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPayendFeedback.aspx.cs" Inherits="ContractManage_ContractPayend_ViewPayendFeedback" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>收入合同交底反馈</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript">
        addEvent(window, 'load', function() {
            setAnnxReadOnly('FileLink1');
        });   
    </script>

    <style type="text/css">
        #FileLink1_But_UpFile
        {
            width: auto;
        }
        #FileLink1_Btn_Upload
        {
            width: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table class="tab">
        <tr class="divHeader">
            <td>
                <asp:Label ID="lblTitle" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 100%;" valign="top">
                <table cellpadding="0" cellspacing="2" class="viewTable" style="width: 100%;" border="0">
                    <tr>
                        <td style="width: 10%; text-align: right;">
                            合同名称
                        </td>
                        <td style="width: 40%;">
                            <asp:Label ID="lblContractName" runat="server"></asp:Label>
                        </td>
                        <td style="width: 10%; text-align: right;">
                            合同编号
                        </td>
                        <td style="width: 40%;">
                            <asp:Label ID="lblContractCode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%; text-align: right;">
                            合同金额
                        </td>
                        <td style="width: 40%;">
                            <asp:Label ID="lblContractPrice" runat="server"></asp:Label>
                        </td>
                        <td style="width: 10%; text-align: right;">
                            签订时间
                        </td>
                        <td style="width: 40%;">
                            <asp:Label ID="lblSignedTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%; text-align: right;">
                            被交底人
                        </td>
                        <td style="width: 40%;">
                            <asp:Label ID="lblBWasPerson" runat="server"></asp:Label>
                        </td>
                        <td style="width: 10%; text-align: right;">
                            附件
                        </td>
                        <td style="width: 40%;" colspan="3">
                            <MyUserControl:epc_usercontrol1_filelink_ascx ID="FileLink1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%; text-align: right;">
                            录入人
                        </td>
                        <td style="width: 40%;">
                            <asp:Label ID="lblInputPerson" runat="server"></asp:Label>
                        </td>
                        <td style="width: 10%; text-align: right;">
                            录入时间
                        </td>
                        <td style="width: 40%;">
                            <asp:Label ID="lblInputDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            主要条款及注意事项
                        </td>
                        <td colspan="3">
                            <asp:TextBox Style="border: solid 0px red; height: 80px;" TextMode="MultiLine" ID="txtProvisionMatter" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            支付条件和履约保函
                        </td>
                        <td colspan="3">
                            <asp:TextBox Style="border: solid 0px red; height: 80px;" TextMode="MultiLine" ID="txtProjectCondition" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            其他说明
                        </td>
                        <td colspan="3">
                            <asp:TextBox Style="border: solid 0px red; height: 80px;" TextMode="MultiLine" ID="txtOtherExplain" runat="server"></asp:TextBox>                            
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            反馈意见
                        </td>
                        <td colspan="3">
                            <asp:TextBox Style="border: solid 0px red; height: 80px;" TextMode="MultiLine" ID="txtFeedback" runat="server"></asp:TextBox>                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="bottonrow">
            <td style="text-align: right;">
                <input type="button" id="btnReturn" value="返回" onclick="window.location='PayendReceive.aspx'" />
                <asp:HiddenField ID="hdGuid" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
