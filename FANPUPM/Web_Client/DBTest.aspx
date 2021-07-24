<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DBTest.aspx.cs" Inherits="DbSet" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>无标题页</title></head>
<body>
    <form id="form1" runat="server">
        <div >
            <br />
            <br />
            <br />
            <table style="width: 351px; border-right: lightblue 1px solid; table-layout: fixed;
                border-top: lightblue 1px solid; border-left: lightblue 1px solid; border-bottom: lightblue 1px solid;
                border-collapse: collapse;" align="center">
                <tr>
                    <td colspan="2" align="center"   style="background-color: lightsteelblue">
                        数据库登录验证</td>
                </tr>
                <tr>
                    <td style="background-color: aliceblue">
                        &nbsp; 服务器类型:</td>
                        
                    <td>
                        <asp:DropDownList ID="DropDownList1" Width="94px" Enabled="false" runat="server"><asp:ListItem Selected="true" Value="2" Text="SQL2005" /><asp:ListItem Text="ORACLE" /></asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="background-color: aliceblue">
                        &nbsp; 服务器IP/名称:</td>
                    <td>
                        <asp:TextBox ID="TextBox1" Width="166px" ToolTip="例如：.\SQLEXPRESS;默认实例可用'.'表示" Text="." runat="server"></asp:TextBox></td>
                </tr>
                 <tr>
                    <td style="background-color: aliceblue">
                        &nbsp; 数据库名称:</td>
                    <td>
                        <asp:TextBox ID="TextBox2" Width="166px" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 26px; background-color: aliceblue">
                        &nbsp; 身份验证:</td>
                    <td style="height: 26px">
                        <asp:DropDownList ID="DropDownList2" Width="151px" Enabled="false" runat="server"><asp:ListItem Selected="true" Value="2" Text="SQL Server 身份验证" /><asp:ListItem Text="ORACLE" /></asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="background-color: aliceblue; height: 26px;">
                        &nbsp; 用 &nbsp;&nbsp;户 &nbsp;&nbsp;名:</td>
                    <td style="height: 26px">
                        <asp:TextBox ID="TextBox3" Width="166px" ToolTip="例：sa" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="background-color: aliceblue">
                        &nbsp; 密 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 码:</td>
                    <td>
                        <asp:TextBox ID="TextBox4" Width="166px" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 26px" align="right">
                        <asp:Button ID="Button3" Text="连   接" Width="66px" OnClick="Button3_Click" runat="server" />
                        <asp:Button ID="Button2" Text="保存配置" Width="75px" Visible="false" OnClick="Button2_Click" runat="server" />
                        <input id="btnclose" type="button" value="关   闭" onclick="window.close();" />
                        &nbsp; &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
