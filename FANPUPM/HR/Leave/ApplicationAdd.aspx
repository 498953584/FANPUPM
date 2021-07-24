<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicationAdd.aspx.cs" Inherits="HR_Leave_ApplicationAdd" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>我的请假记录</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#BtnClose').click(function () {
                winClose();
            });
        });

        function winClose() {
            top.ui.closeWin();
            if (typeof top.ui.callback == 'function')
                top.ui.callback();
        }
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
        <table class="table-normal" cellspacing="0" cellpadding="0" width="100%" border="1">
            <tr>
                <td class="td-title" colspan="2">
                    请假申请
                </td>
            </tr>
            <tr>
                <td class="td-label" style="width: 25%">
                    请(休)假类型:
                </td>
                <td>
                    <asp:DropDownList ID="DDLLeaveType" runat="server"><asp:ListItem Value="1" Text="事假" /><asp:ListItem Value="2" Text="婚假" /><asp:ListItem Value="3" Text="年休假" /><asp:ListItem Value="4" Text="工伤" /><asp:ListItem Value="5" Text="病假" /><asp:ListItem Value="6" Text="产假" /><asp:ListItem Value="7" Text="丧假" /></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td-label">
                    请休假开始时间:
                </td>
                <td align="left">
                    <asp:TextBox ID="DBBeginDate" Width="180px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-label">
                    请假天数:
                </td>
                <td>
                    <asp:TextBox ID="txtDays" Width="60px" CssClass="text-normal" runat="server"></asp:TextBox>天<asp:Label ID="Label1" Font-Size="12px" ForeColor="Red" Text="（注意：整数或x.5）" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td-label">
                    请假事由:
                </td>
                <td>
                    <asp:TextBox ID="txtReason" Width="90%" CssClass="text-normal" MaxLength="250" Rows="6" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" class="td-submit">
                    <asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;
                    <input id="BtnClose" type="button" value="取 消" />&nbsp;
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtDays" ErrorMessage="请假天数的格式不正确!" ValidationExpression="(^[0-9]*$)|(^[0-9]+(.[0,5]{1})$)" Display="None" runat="server"></asp:RegularExpressionValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="DBBeginDate" Display="None" ErrorMessage="请休假开始时间不能为空!" runat="server"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtDays" Display="None" ErrorMessage="请假天数不能为空!" runat="server"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </div>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
