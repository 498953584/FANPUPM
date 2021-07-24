<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dbrEdit.aspx.cs" Inherits="dbrEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>代办人信息维护</title>
    <base target="_self" />
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            // 设置只读
            $('#txtDbr').attr('readonly', 'readonly');
        });
        function pickOperater() {
            var url = "/EPC/WorkFlow/SelectUser.aspx";
            top.ui.openWin({ title: '选择人员', url: url });
            top.ui.callback = function (user) {
                document.getElementById('hdnDbr').value = user.code;
                document.getElementById('txtDbr').value = user.name;
            };
        }
    </script>
</head>
<body scroll="no">
    <form id="Form1" method="post" runat="server">
    <table id="tableVersion" border="1" cellspacing="0" class="tableAudit" width="100%">
        <tr>
            <td class="divHeader" colspan="4" height="20">
                代办人信息维护
            </td>
        </tr>
        <tr>
            <td width="25%" style="text-align: right">
                模板名称
            </td>
            <td>
                <asp:TextBox ID="txtTemplateName" Style="width: 150px" TabIndex="1" Enabled="false" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font>
                <input id="hdnTemplate" style="width: 10px" type="hidden" name="hdnTemplate" runat="server" />

            </td>
        </tr>
        <tr>
            <td width="25%" style="text-align: right">
                原审核人
            </td>
            <td>
                <asp:TextBox ID="txtAuditor" Style="width: 150px" TabIndex="2" Enabled="false" runat="server"></asp:TextBox><font
                    color="#ff0000">&nbsp;</font>
                <input id="hdnAuditor" style="width: 10px" type="hidden" name="hdnAuditor" runat="server" />

            </td>
        </tr>
        <tr>
            <td width="25%" style="text-align: right">
                代办人
            </td>
            <td style="padding-right: 3px">
                <span class="spanSelect" style="width: 152px">
                    <asp:TextBox ID="txtDbr" Style="width: 127px; height: 15px; border: none; line-height: 16px;
                        margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                    <input type="hidden" id="hdnDbr" runat="server" />

                    <img alt="选择人员" id="imgBt" onclick="pickOperater();" src="../../images/icon.bmp"
                        style="float: right" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtdbr" Display="None" ErrorMessage="代办人不能为空！" runat="server"></asp:RequiredFieldValidator>
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align: right" colspan="4" height="20">
                <asp:Button ID="BtnAdd" Text="保  存" OnClick="BtnAdd_Click" runat="server" />&nbsp;
                <input id="BtnClose" onclick="top.ui.closeTab();"
                    type="button" value="关  闭" name="BtnClose" />
                <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
