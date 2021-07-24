<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PmTypeTreeSet.aspx.cs" Inherits="PmTypeTreeSet" %>


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
                            <td colspan="2" class="td-title">项目树显示设置
                                </td>
                        </tr>
                        <tr>
                            <td width="100px" class="td-label"><asp:Label ID="Label2" runat="server">选择类型：</asp:Label></td>
                            <td >
                                <asp:DropDownList ID="DropDownList1" DataValueField="SkinID" Width="120px" runat="server"><asp:ListItem Value="0" Text="按年份显示" /><asp:ListItem Value="1" Text="按项目状态显示" /></asp:DropDownList>
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
