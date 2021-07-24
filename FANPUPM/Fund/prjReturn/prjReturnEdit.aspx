<%@ Page Language="C#" AutoEventWireup="true" CodeFile="prjReturnEdit.aspx.cs" Inherits="Fund_prjReturn_prjReturnEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>账户还款</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script src="../../SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
    <script src="../../StockManage/script/Config2.js" type="text/javascript"></script>
    <script src="../../StockManage/script/JustWinTable.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script src="../../SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Val.validate('form1', 'btnSave');
        });
        //选择人员
        function selectUser() {
            jw.selectOneUser({ idinput: 'hdfUserCode', nameinput: 'txtPeople' });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="divContent2">
        <table id="tableContent" class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td colspan="4" class="groupInfo">
                    借款单基本信息
                </td>
            </tr>
            <tr>
                <td class="word">
                    借款单号
                </td>
                <td class="elemTd">
                    <asp:TextBox ID="txtLoanID" ReadOnly="true" Height="15px" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    约定还款日期
                </td>
                <td class="elemTd">
                    <asp:TextBox ID="txtLoanDate" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    借款利率
                </td>
                <td class="elemTd">
                    <asp:TextBox ID="txtLoanRate" ReadOnly="true" Height="15px" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    借款金额
                </td>
                <td class="elemTd">
                    <asp:TextBox ID="txtLoanFund" Height="15px" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    已还款本金
                </td>
                <td class="elemTd">
                    <asp:TextBox ID="txtReturnMoney" ReadOnly="true" Height="15px" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    未还本金
                </td>
                <td class="elemTd">
                    <asp:TextBox ID="txtNoRetMoney" Height="15px" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    已还利息
                </td>
                <td class="elemTd">
                    <asp:TextBox ID="txtReturnInterest" ReadOnly="true" Height="15px" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    已还其他
                </td>
                <td class="elemTd">
                    <asp:TextBox ID="txtReturnOther" Height="15px" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="groupInfo">
                    还款单信息
                </td>
            </tr>
            <tr>
                <td class="word">
                    还款单号
                </td>
                <td class="txt  mustInput">
                    <asp:TextBox ID="txtCode" Height="15px" ReadOnly="true" CssClass="{required:true, messages:{required:'还款单号必须输入'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    归还人
                </td>
                <td class="txt  mustInput">
                    <span class="spanSelect">
                        <asp:TextBox ID="txtPeople" ReadOnly="true" CssClass="{required:true, messages:{required:'归还人必须选择'}}" Style="width: 97px; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择录入人" onclick="selectUser();" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                </td>
            </tr>
            <tr>
                <td class="word">
                    归还日期
                </td>
                <td class="txt  mustInput">
                    <asp:TextBox ID="txtData" CssClass="{required:true,messages:{required:'归还日期必须输入'}}" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    归还本金
                </td>
                <td class="txt  mustInput">
                    <asp:TextBox ID="txtMoney" CssClass="decimal_input {required:true, messages:{required:'归还本金必须输入'}}" Height="15px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    归还利息
                </td>
                <td class="txt  mustInput">
                    <asp:TextBox ID="txtInterest" Height="15px" CssClass="decimal_input {required:true,messages:{required:'归还利息必须输入'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    其他扣款
                </td>
                <td class="txt  mustInput">
                    <asp:TextBox ID="txtDeduct" CssClass="decimal_input {required:true,messages:{required:'其他扣款必须输入'}}" Height="15px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    是否还清
                </td>
                <td class="elemTd">
                    <asp:RadioButton ID="rdbYes" Text="是" GroupName="type" runat="server" />
                    <asp:RadioButton ID="rdbNo" Text="否" GroupName="type" runat="server" />
                </td>
                <td class="word">
                </td>
                <td class="elemTd">
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space: nowrap;">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtremark" TextMode="MultiLine" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    填报人
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtUser" Height="15px" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td class="word" style="white-space: nowrap;">
                    填报日期
                </td>
                <td class="txt" style="padding-right: 1px">
                    <asp:TextBox ID="txtTime" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="winclose('prjReturnEdit','prjReturnList.aspx',false)" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hdfUserCode" runat="server" />
    <asp:HiddenField ID="hdfReturnId" runat="server" />
    </form>
</body>
</html>
