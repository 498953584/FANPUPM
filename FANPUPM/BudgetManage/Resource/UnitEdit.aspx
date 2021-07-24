<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnitEdit.aspx.cs" Inherits="BudgetManage_Resource_UnitEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>单位设置</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            if (getRequestParam('t') == '1') {
                setAllInputDisabled();
            }
            Val.validate('form1');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 135px;">
        <table style="" width="98%" cellpadding="5px" cellspacing="0">
            <tr>
                <td style="text-align: right; width: 90px;">
                    单位名称
                </td>
                <td>
                    <asp:TextBox ID="txtName" BackColor="#FEFEF4" Height="15px" Width="180px" CssClass="{required:true, messages:{required:'单位名称必须输入'}}" runat="server"></asp:TextBox>
						<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ErrorMessage="请输入字符个数少于30个的字符" ControlToValidate="txtName" ValidationExpression=".{1,30}" runat="server"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                </td>
            </tr>
        </table>
    </div>
    <div style="text-align: right; padding-right: 10px;">
        <asp:Button ID="btnSave" Text="保存" OnClientClick="return valForm()" OnClick="btnSave_Click" runat="server" />
        <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
    </div>
    <asp:HiddenField ID="hdGuid" runat="server" />
    </form>
</body>
</html>
