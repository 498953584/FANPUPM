<%@ Page Language="C#" AutoEventWireup="true" CodeFile="human.aspx.cs" Inherits="Human" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<html>
<head runat="server"><title>Human</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript">
        function closeMethed() {
            if (parent.jw.getRequestParam('depId')!='') {
                parent.document.getElementById('btnClose').click();
            } else {
                window.close();
            }
        }
    </script>
</head>
<body leftmargin="0" topmargin="0" scroll="no" rightmargin="0">
    <form id="Form1" method="post" runat="server">
    <font face="宋体">
        <table cellspacing="0" cellpadding="0" border="0" height="100%" style="background-color: #EBE9EE;">
            <tr>
                <td colspan="3">
                    按姓名<asp:TextBox ID="txtName" Width="100px" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" CssClass="btn_Search" Text=" 查询" OnClick="btnSearch_Click" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100" border="0">
                        <tr align="center">
                            <td>
                                <asp:ListBox ID="LBoxHuman" Width="107px" SelectionMode="Multiple" Height="365px" runat="server"></asp:ListBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 60px; text-align: center;">
                    <asp:ImageButton ID="BtnAdd" BorderStyle="None" ImageUrl="~/oa/MailAdmin/Images/anniu2.gif" OnClick="BtnAdd_Click" runat="server" />
                    <div style="margin-top: 70px;">
                        <asp:ImageButton ID="BtnRemove" BorderStyle="None" ImageUrl="~/oa/MailAdmin/Images/anniu1.gif" OnClick="BtnRemove_Click" runat="server" />
                    </div>
                </td>
                <td>
                    <table cellspacing="0" cellpadding="0" width="107px" border="0">
                        <tr align="center">
                            <td colspan="2">
                                <asp:ListBox ID="LBoxSelected" Width="107px" SelectionMode="Multiple" Height="365px" runat="server"></asp:ListBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 17px;">
                </td>
                <td style="width: 15px;">
                    <img src="Images/xian.jpg" />
                </td>
                <td style="width: 20px">
                </td>
                <td style="width: 106px;">
                    <table cellspacing="0" cellpadding="0" width="106" border="0" style="height: 365px;">
                        <tr>
                            <td>
                                <input class="button-normal" style="width: 73px" onclick="closeMethed();" type="button"
                                    value="确定"><input id="HdnSysID" style="width: 1px; height: 1px" type="hidden" name="HdnSysID" runat="server" />

                            </td>
                        </tr>
                        <tr>
                            <td style="height: 270px;">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="BtnAddAll" Width="73px" Text="添加所有" CssClass="button-normal" OnClick="BtnAddAll_Click" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="BtnRemoveAll" Width="73px" Text="清除全部" CssClass="button-normal" OnClick="BtnRemoveAll_Click" runat="server" />
                            </td>
                        </tr>
                        <tr style="height: 10px;">
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </font>
    <asp:HiddenField ID="hfldParentPage" runat="server" />
    </form>
</body>
</html>
