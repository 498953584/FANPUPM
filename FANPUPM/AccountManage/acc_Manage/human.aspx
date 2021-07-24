<%@ Page Language="C#" AutoEventWireup="true" CodeFile="human.aspx.cs" Inherits="SalaryManage_SetSalary_human" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>Human</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
</head>
<body leftmargin="0" topmargin="0" scroll="no" rightmargin="0">
    <form id="Form1" method="post" runat="server">
    <br>
    <font face="宋体">
        <table cellspacing="0" cellpadding="0" border="0" height="100%">
            <tr>
                <td colspan="2">
                    按姓名<asp:TextBox ID="txtName" Width="85px" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" CssClass="btn_Search" OnClick="btnSearch_Click" runat="server" />
                   
                </td>
            </tr>
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100" border="0">
                        <tr align="center">
                            <td>
                                <asp:ListBox ID="LBoxHuman" Width="98px" SelectionMode="Multiple" Rows="25" runat="server"></asp:ListBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:Button ID="BtnAdd" Width="98px" Text="添    加" CssClass="button-normal" OnClick="BtnAdd_Click" runat="server" />
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:Button ID="BtnAddAll" Width="98px" Text="添加所有" CssClass="button-normal" OnClick="BtnAddAll_Click" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100" border="0">
                        <tr align="center">
                            <td colspan="2">
                                <asp:ListBox ID="LBoxSelected" Width="98px" SelectionMode="Multiple" Rows="25" runat="server"></asp:ListBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <asp:Button ID="BtnRemoveAll" Width="98px" Text="清除全部" CssClass="button-normal" OnClick="BtnRemoveAll_Click" runat="server" />
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:Button ID="BtnRemove" Width="48px" Text="删除" CssClass="button-normal" OnClick="BtnRemove_Click" runat="server" />
                            </td>
                            <td>
                                <input class="button-normal" style="width: 48px" onclick="window.close()" type="button"
                                    value="确定"><input id="HdnSysID" style="width: 1px; height: 1px" type="hidden" name="HdnSysID" runat="server" />

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </font>
    </form>
</body>
</html>
