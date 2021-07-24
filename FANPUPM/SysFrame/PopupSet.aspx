<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupSet.aspx.cs" Inherits="SysFrame_PopupSet" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>

    <script src="../Script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        //点确定
        function btnOk() {
            divClose(parent);
        }
        //Div关闭方法
        function divClose(obj) {
            $(obj.document).find('.ui-icon-closethick').each(function() {
                this.click();
            });
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table class="gvdata" cellspacing="0px" border="1px" style="width: 180px; margin: auto;
        border-collapse: collapse;">
        <tr class="rowa">
            <td style="width: 40px; text-align: right;">
                <asp:CheckBox ID="chkBulletin" runat="server" />
            </td>
            <td style="text-align: left; padding-left: 20px;">
                <asp:Label ID="lblBulletin" runat="server"></asp:Label>
            </td>
        </tr>
        <tr class="rowb">
            <td style="text-align: right;">
                <asp:CheckBox ID="chkCompanyNews" runat="server" />
            </td>
            <td style="text-align: left; padding-left: 20px;">
                <asp:Label ID="lblCompanyNews" runat="server"></asp:Label>
            </td>
        </tr>
        <tr class="rowa">
            <td style="text-align: right;">
                <asp:CheckBox ID="chkWorkflow" runat="server" />
            </td>
            <td style="text-align: left; padding-left: 20px;">
                <asp:Label ID="lblWorkflow" runat="server"></asp:Label>
            </td>
        </tr>
        <tr class="rowb">
            <td style="text-align: right;">
                <asp:CheckBox ID="chkSchedule" runat="server" />
            </td>
            <td style="text-align: left; padding-left: 20px;">
                <asp:Label ID="lblSchedule" runat="server"></asp:Label>
            </td>
        </tr>
        <tr class="rowa">
            <td style="text-align: right;">
                <asp:CheckBox ID="chkMail" runat="server" />
            </td>
            <td style="text-align: left; padding-left: 20px;">
                <asp:Label ID="lblMail" runat="server"></asp:Label>
            </td>
        </tr>
        <tr class="rowb">
            <td style="text-align: right;">
                <asp:CheckBox ID="chkWarning" runat="server" />
            </td>
            <td style="text-align: left; padding-left: 20px;">
                <asp:Label ID="lblWarning" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: right;">
                <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />&nbsp;
                <asp:Button ID="btnClear" Text="取消" OnClientClick="btnOk()" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
