<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebLinkAdd.aspx.cs" Inherits="UserSet" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <style type="text/css">
        table
        {
            width: 100%;
        }
        .style1
        {
            height: 41px;
        }
        .style2
        {
            height: 41px;
            width: 108px;
            text-align: right;
        }
        .style3
        {
            width: 108px;
            text-align: right;
        }
    </style>
    <script src="../Script/jquery.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        window.onload = function () {
            document.getElementById('BtnSave').onclick = btnSaveClick;
        }
        function btnSaveClick() {
            if (!document.getElementById('txtWebName').value) {
                alert("系统提示：\n\n网站名称不能为空");
                return false;
            }
            if (!document.getElementById('txtWebAddr').value) {
                alert("系统提示：\n\n网址不能为空");
                return false;
            }
        }

        //点确定
        function btnOk() {
            top.ui.closeWin();
        }
        //Div关闭方法
        function divClose(obj) {
            $(obj.document).find('.ui-icon-closethick').each(function () {
                this.click();
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="tableContent2" style="width: 100%; height: 150px" cellspacing="0px"
            cellpadding="5px">
            <tr>
                <td class="style2">
                    显示顺序
                </td>
                <td class="style1">
                    <asp:DropDownList ID="GirdColNum" Width="130px" runat="server"><asp:ListItem Value="2" Text="1" /><asp:ListItem Value="2" Text="2" /><asp:ListItem Value="3" Text="3" /><asp:ListItem Value="4" Text="4" /><asp:ListItem Value="5" Text="5" /><asp:ListItem Value="6" Text="6" /><asp:ListItem Value="7" Text="7" /><asp:ListItem Value="8" Text="8" /><asp:ListItem Value="8" Text="9" /><asp:ListItem Value="8" Text="10" /></asp:DropDownList>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="word">
                    网站名称
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtWebName" Width="80%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr class="word">
                <td class="style3">
                    网址
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtWebAddr" Text="http://" Width="80%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr class="word">
                <td class="style3">
                    备注
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtRemark" Rows="5" TextMode="MultiLine" Width="80%" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div style="text-align: right; padding-right: 10px;">
        <asp:Button ID="BtnSave" Text="保存" OnClick="BtnSave_Click" runat="server" />
        <asp:Button ID="BtnClear" Text="取消" OnClientClick="btnOk()" runat="server" />
    </div>
    </form>
</body>
</html>
