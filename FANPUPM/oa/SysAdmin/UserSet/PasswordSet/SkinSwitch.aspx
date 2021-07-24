<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SkinSwitch.aspx.cs" Inherits="oa_SysAdmin_UserSet_PasswordSet_SkinSwitch" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>桌面设置</title></head>
<body>
    <form id="form1" runat="server">
    <div>
        <table  width="100%" height="100%" border="0">
            <tr>
                <td align="center" valign="top">
                    <br /><br /><br /><br /><br /><br />
                    <table class="table-normal" width="280" height="100" border="1">
                        <tr>
                            <td colspan="2" class="td-title">个人皮肤设置
                                </td>
                        </tr>
                        <tr>
                            <td width="100px" class="td-label"><asp:Label ID="Label2" runat="server">选择皮肤：</asp:Label></td>
                            <td >
                                <asp:DropDownList ID="DropDownList1" DataSourceID="SkinSet" DataTextField="SkinName" DataValueField="SkinID" Width="120px" runat="server"></asp:DropDownList><asp:SqlDataSource ID="SkinSet" SelectCommand="SELECT [SkinID], [SkinName] FROM [PT_SkinType] ORDER BY [SkinID]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-label">
                            </td>
                            <td>
                                <asp:CheckBox ID="cbUseNow" Text="是否立即应用" runat="server" /></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="btnConfirm" CssClass="button-normal" Text="确 定" OnClick="btnConfirm_Click" runat="server" /></td>
                        </tr>
                    </table>
                    <JWControl:JavaScriptControl ID="JavaScriptControl1" runat="server"></JWControl:JavaScriptControl>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
