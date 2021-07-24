<%@ Page Language="C#" AutoEventWireup="true" CodeFile="outbox.aspx.cs" Inherits="OutBox" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>OutBox</title><JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script src="../../StockManage/Script/Config2.js" type="text/javascript"></script>
    <script src="../../StockManage/Script/JustWinTable.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function () {
            var aa = new JustWinTable('DGrdMail');
        }
    </script>
</head>
<body class="body_clear">
    <form id="Form1" method="post" runat="server">
    <font face="宋体">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0"
            class="table-normal">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr class="td-toolsbar">
                            <td align="left">
                                &nbsp;<asp:Label ID="LabMail" runat="server"></asp:Label>&nbsp;
                            </td>
                            <td colspan="3">
                                <asp:Button ID="BtnDelN" CssClass="button-normal" Text="删 除" OnClick="BtnDelN_Click" runat="server" />
                                <asp:Button ID="BtnDelY" CssClass="button-normal" Width="80px" Text="彻底删除" OnClick="BtnDelY_Click" runat="server" />
                                <asp:Button ID="BtnMove" CssClass="button-normal" Text="转移到" OnClick="BtnMove_Click" runat="server" />
                                <asp:DropDownList ID="DDLtBox" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:DataGrid ID="DGrdMail" Width="100%" DataKeyField="i_sjid" AllowPaging="true" AutoGenerateColumns="false" PageSize="15" HorizontalAlign="Center" CssClass="grid" OnPreRender="DGrdMail_PreRender" OnSelectedIndexChanged="DGrdMail_SelectedIndexChanged" runat="server"><HeaderStyle CssClass="grid_head"></HeaderStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><ItemStyle CssClass="grid_row"></ItemStyle><Columns><asp:TemplateColumn>
<HeaderTemplate>
                                    <asp:CheckBox ID="cbSelAll" Text="全选" runat="server" />
                                </HeaderTemplate>

<ItemTemplate>
                                    <asp:CheckBox ID="cbSelSingle" runat="server" />
                                </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="状态" HeaderImageUrl="~/images/emHead.gif" Visible="false">
<ItemTemplate>
                                    
                                    <asp:Image ID="Image2" ImageUrl="~/images/newEmail.gif" runat="server" />
                                </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="v_jsrxm" HeaderText="收件人"></asp:BoundColumn><asp:TemplateColumn HeaderText="主题">
<ItemTemplate>
                                    <asp:LinkButton CausesValidation="false" Font-Underline="false" CommandName="Select" Text='<%# Convert.ToString(Eval("v_zt")) %>' runat="server"></asp:LinkButton>
                                </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="dtm_sjsj" HeaderText="时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"></asp:BoundColumn><asp:TemplateColumn HeaderText="附件">
<ItemTemplate>
                                    <asp:Image ID="Image1" ImageUrl="~/images/icon_att0b3dfa.gif" runat="server" />
                                </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="i_fjdx" HeaderText="大小"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid><font color="Navy">每页显示</font><asp:TextBox ID="tbSet" Width="30px" runat="server"></asp:TextBox><font color="Navy">条</font><asp:Button ID="btnSet" Text="OK" OnClick="btnSet_Click" runat="server" />
                </td>
            </tr>
        </table>
    </font>
    </form>
</body>
</html>
