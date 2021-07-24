<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadLogo.aspx.cs" Inherits="TableTop_UploadLogo" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>上传logo</title></head>
<body>
    <form id="form1" runat="server">
    <div class="pagediv" style="height: 49%; border-bottom: solid 2px #CADEED;">
        <table class="gvdata" cellspacing="0px" border="1px" style="width: 100%; margin: auto;
            border-collapse: collapse;">
            <tr>
                <td style="text-align: right; width: 5%;">
                    登录页面的logo
                </td>
                <td>
                    <img src="/UploadFiles/UserLogo/logo3.jpg?date=<%=this.imaLoginStr %>" width="195" height="169"/>
                    建议图片大小为：195*169
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 5%;">
                    更换的logo
                </td>
                <td style="text-align: left; width: 95%;">
                    <asp:FileUpload ID="fplLoginLogo" BackColor="White" Width="60%" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left;">
                    <asp:Button ID="btnSaveLogin" Text="保存" OnClick="btnSaveLogin_Click" runat="server" />
                    <asp:Button ID="btnReturnLastLogin" Text="还原上一次Logo" Width="100px" OnClientClick="return confirm('您确定要还原上一次Logo吗?')" OnClick="btnReturnLastLogin_Click" runat="server" />
                    <asp:Button ID="btnReturnBakLogin" Text="还原初始Logo" Width="100px" OnClientClick="return confirm('您确定要还原初始Logo吗?')" OnClick="btnReturnBakLogin_Click" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div class="pagediv" style="height: 49%;">
        <table class="gvdata" cellspacing="0px" border="1px" style="width: 100%; margin: auto;
            border-collapse: collapse;">
            <tr>
                <td>
                    登录后页面的logo
                </td>
                <td>
                    <img src="/UploadFiles/UserLogo/logo.jpg?date=<%=this.imaLoginedStr %>" width="365" height="36"/>
                    建议图片大小为：365*35
                </td>
            </tr>
            <tr>
                <td>
                    更换的logo
                </td>
                <td style="text-align: left; width: 95%;">
                    <asp:FileUpload ID="fplLoginedLogo" BackColor="White" Width="60%" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left;">
                    <asp:Button ID="btnSaveLogined" Text="保存" OnClick="btnSaveLogined_Click" runat="server" />
                    <asp:Button ID="btnReturnLastLogined" Text="还原上一次Logo" Width="100px" OnClientClick="return confirm('您确定要还原上一次Logo吗?')" OnClick="btnReturnLastLogined_Click" runat="server" />
                    <asp:Button ID="btnReturnBakLogined" Text="还原初始Logo" Width="100px" OnClientClick="return confirm('您确定要还原初始Logo吗?')" OnClick="btnReturnBakLogined_Click" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
