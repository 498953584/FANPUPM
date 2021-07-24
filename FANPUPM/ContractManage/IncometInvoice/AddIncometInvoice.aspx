<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddIncometInvoice.aspx.cs" Inherits="ContractManage_IncometInvoice_AddIncometInvoice" %>
<%@ Import Namespace="com.jwsoft.pm.entpm"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>开票登记</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if (getRequestParam('t') == '1') {
                setAllInputDisabled();
            }
            Val.validate('form1');
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
    <div class="divHeader2">
        <table class="tableHeader2">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="divContent2">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
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
                    <asp:TextBox ID="txtContractCode" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    合同名称
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtContractName" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    合同最终金额
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtContractPrice" CssClass="decimal_input" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    签订时间
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtSignedTime" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    项目编号
                </td>
                <td class="txt readonly">
                    <asp:TextBox Width="100%" ReadOnly="true" ID="txtPrjCode" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    项目名称
                </td>
                <td class="txt readonly">
                    <asp:TextBox Width="100%" class="readonly" ReadOnly="true" ID="txtPrjName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="groupInfo">
                    发票信息
                </td>
            </tr>
            <tr>
                <td class="word">
                    累计收款金额
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtPaymentSum" CssClass="decimal_input" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    累计已开发票
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtInvoiceSum" CssClass="decimal_input" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    累计未开发票
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtBalance" CssClass="decimal_input" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    本次开票金额
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtAmount" Height="15px" Width="100%" CssClass=" decimal_input  {required:true,number:true, messages:{required:'发票金额必须输入',number:'发票金额格式错误'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    发票号码
                </td>
                <td class="txt mustInput">
                    <asp:TextBox Height="15px" Width="100%" CssClass="{required:true,messages:{required:'发票号码必须输入!'}}" ID="txtInvoiceNo" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    发票类型
                </td>
                <td class="txt mustInput">
                    <asp:DropDownList ID="ddlInvoiceType" Width="100%" DataTextField="CodeName" DataValueField="NoteID" CssClass="{required:true, messages:{required:'请选择发票类型'}}" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="word">
                    收款单位
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtSecond" CssClass="{required:true, messages:{required:'收款单位必须输入'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    付款单位
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtParty" CssClass="{required:true, messages:{required:'付款单位必须输入'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    纳税人识别号
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtTaxNo" Width="100%" CssClass="{required:true, messages:{required:'纳税人识别号必须输入'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    发票代码
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtInvoiceCode" Width="100%" CssClass="{required:true, messages:{required:'发票代码必须输入'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    办理人
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtTransactor" Height="15px" Width="100%" CssClass="{required:true, messages:{required:'办理人必须输入'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    开票日期
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
                    <asp:TextBox ID="txtContact" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    开户行及账号
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="txtBankCode" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNotes" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" id="td1">
                    附件
                </td>
                <td colspan="3" style="text-align: left; padding-right: 0px;">
                    <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    录入人
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtInputPerson" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    录入时间
                </td>
                <td class="txt readonly">
                    <asp:TextBox Width="100%" ReadOnly="true" ID="txtInputDate" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td align="right" style="border: solid 0px red; width: 390px">
                    登记人:<%=PageHelper.QueryUser(this, base.UserCode) %>
                </td>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClientClick="return valForm()" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="winclose('AddIncometInvoice','ShowInvoiceList.aspx',false)" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdGuid" runat="server" />
    <asp:HiddenField ID="hdChangeCode" runat="server" />
    </form>
</body>
</html>
