<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MailView.aspx.cs" Inherits="OA2_Mail_MailView" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript">
        //页面跳转到收件箱
        function selectSend() {
            parent.parent.desktop.ViewMail = window;
            var url
            url = '/OA2/Mail/Inbox.aspx?';
            this.location.href = url;
        }
    </script>
</head>
<body id="print">
    <table cellspacing="0" cellpadding="0" width="100%" height="100%" align="center"
        border="0">
        <tr>
            <td valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="100%" height="50" style="float: left; margin-left: 1%; padding: 0px;">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="50" style="float: left; margin-left: 1%; padding: 0px;">
                            <span style="font-family: '幼圆'; float: left; font-size: 18px; font-weight: bold">
                                <img src="../../images/sendmail_title.gif" width="24" height="24" align="middle">
                                <asp:Label ID="lbltx" Text="" Font-Size="18px" Font-Bold="true" runat="server"></asp:Label></span>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="40px" style="float: left; margin-left: 1%; padding: 0px;">
                            <input type="button" id="btnInbox" value="返回收件箱" style="line-height: 21px; font-size: 12px;
                                margin-bottom: 4px; background: url(/images1/email/btn.jpg) repeat-x; height: 21px;
                                color: White; font-weight: bold; width: 80px;" onclick="selectSend()" />
                            &nbsp;&nbsp;<a id="write_mail" href="WriteMail.aspx" style="color: Black; font-weight: bold;">再写一封</a>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td id="TDAllName" align="left" style="float: left; margin-left: 1%;
                            padding: 0px; width: 100%" runat="server">
                            <table style="background-color: #F3F8FE; width: 50%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblB" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>
