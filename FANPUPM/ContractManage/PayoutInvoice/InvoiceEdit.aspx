<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoiceEdit.aspx.cs" Inherits="ContractManage_PayoutInvoice_InvoiceEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>支出合同发票</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            Val.validate('form1');
            addEvent(document.getElementById('btnCancel'), 'click', function () {
                winclose('InvoiceEdit', 'PayoutInvoiceEdit.aspx', false);
            })
            if (getRequestParam('Action') == 'Query' || !getRequestParam('Action')) {
                setAllInputDisabled();
            }
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader2">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="支出合同发票" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="divContent" class="divContent2">
        <table id="tableContent" class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td colspan="4" class="groupInfo">
                    合同基本信息
                </td>
            </tr>
            <tr>
                <td class="word">
                    合同编号
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtContractCode" ReadOnly="true" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    合同名称
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtContractName" Height="15px" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    合同最终金额
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtContractMoney" class="decimal_input" Height="15px" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td style="text-align: right;">
                    签订时间
                </td>
                <td>
                    <asp:TextBox class="readonly" ID="txtSignDate" Height="15px" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    项目编号
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtPrjCode" Height="15px" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    项目
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtProject" Height="15px" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="groupInfo">
                    结算单信息
                </td>
            </tr>
            <tr>
                <td class="word">
                    累计付款金额
                </td>
                <td class="txt readonly">
                    <asp:TextBox class="decimal_input" Height="15px" Width="100%" ID="txtPaymentSum" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    累计已收发票
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtInvoiceSum" class="decimal_input" Height="15px" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    累计未到票额
                </td>
                <td class="txt readonly">
                    <asp:TextBox class="decimal_input" Height="15px" Width="100%" ID="txtDiff" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    发票金额
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtAmount" Height="15px" Width="100%" CssClass=" decimal_input {required:true,number:true, messages:{required:'发票金额必须输入!',number:'发票金额格式错误!'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    发票号码
                </td>
                <td class="txt mustInput">
                    <asp:TextBox Height="15px" Width="100%" ID="txtInvoiceNo" CssClass="{required:true, messages:{required:'发票号码必须输入!'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    发票类型
                </td>
                <td class="txt mustInput">
                    <asp:DropDownList ID="dropInvoiceType" Height="19px" Width="102%" DataTextField="CodeName" DataValueField="NoteID" CssClass="{required:true, messages:{required:'请选择发票类型!'}}" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="word">
                    付款单位
                </td>
                <td class="txt">
                    <asp:TextBox Height="15px" Width="100%" ID="txtPayer" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    收款单位
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtPayee" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    纳税人识别号
                </td>
                <td class="txt mustInput">
                    <asp:TextBox Height="15px" Width="100%" ID="txtTaxNo" CssClass="{required:true, messages:{required:'纳税人识别号必须输入!'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    发票代码
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtInvoiceCode" Height="15px" Width="100%" CssClass="{required:true, messages:{required:'发票代码必须输入!'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    办理人
                </td>
                <td class="txt mustInput">
                    <asp:TextBox Height="15px" Width="100%" ID="txtTransactor" CssClass="{required:true, messages:{required:'办理人必须输入!'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    开票时间
                </td>
                <td class="txt mustInput">
                    
                    <asp:TextBox ID="txtInvoiceDate" Height="15px" Width="100%" onClick="WdatePicker()" CssClass="{required:true, messages:{required:'开票日期必须输入'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    组织机构代码证号码
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtOrganizationCode" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    地址、电话
                </td>
                <td class="txt">
                    <asp:TextBox Height="15px" Width="100%" ID="txtContact" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    开户银行及账号
                </td>
                <td colspan="3">
                    <asp:TextBox Height="15px" Width="100%" ID="txtBankCode" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNotes" TextMode="MultiLine" Height="50px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    附件
                </td>
                <td colspan="3" style="padding-right: 0px;">
                    <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    录入人
                </td>
                <td class="txt mustInput readonly">
                    <asp:TextBox ID="txtInputPerson" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    录入时间
                </td>
                <td class="txt mustInput readonly">
                    <asp:TextBox ReadOnly="true" ID="txtInputDate" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div id="divFooter" class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="window.close();" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdGuid" runat="server" />
    <asp:HiddenField ID="hdChangeCode" runat="server" />
    </form>
</body>
</html>
