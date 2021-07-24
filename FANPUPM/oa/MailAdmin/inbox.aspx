<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inbox.aspx.cs" Inherits="InBox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server"><title>InBox</title>
    <base target="_self"/>

    <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script src="../../StockManage/Script/Config2.js" type="text/javascript"></script>

    <script src="../../StockManage/Script/JustWinTable.js" type="text/javascript"></script>
     <script type="text/javascript">
        window.onload = function()
        {
            var aa = new JustWinTable('DGrdMail');
        }
    </script>
</head>
<body class="body_clear" style="font-size: large;" >
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0"
            class="table-normal">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr class="td-toolsbar">
                            <td align="left">
                                &nbsp;<asp:Label ID="LabMail" runat="server"></asp:Label>&nbsp;</td>
                            <td colspan="4">
                                <asp:Button ID="BtnDelN" Text="删 除" CssClass="button-normal" OnClick="BtnDelN_Click" runat="server" />
                                <asp:Button ID="BtnDelY" Text="彻底删除" Width="80px" CssClass="button-normal" OnClick="BtnDelY_Click" runat="server" />
                                <asp:Button ID="BtnMove" Text="转移到" CssClass="button-normal" OnClick="BtnMove_Click" runat="server" />
                                <asp:DropDownList ID="DDLtBox" runat="server"></asp:DropDownList></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:DataGrid ID="DGrdMail" Width="100%" DataKeyField="i_sjid" AutoGenerateColumns="false" AllowPaging="true" CssClass="grid" OnPreRender="DGrdMail_PreRender" OnSelectedIndexChanged="DGrdMail_SelectedIndexChanged" runat="server"><HeaderStyle CssClass="grid_head"></HeaderStyle><ItemStyle CssClass="grid_row"></ItemStyle><Columns><asp:TemplateColumn>
<HeaderTemplate>
                                    <asp:CheckBox ID="cbSelAll" Text="全选" runat="server" />
                                </HeaderTemplate>

<ItemTemplate>
                                    <asp:CheckBox ID="cbSelSingle" runat="server" />
                                </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="状态" HeaderImageUrl="~/images/emHead.gif">
<ItemTemplate>
                                    <asp:Image ID="Image2" ImageUrl="~/images/newEmail.gif" runat="server" />
                                </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="v_fxrxm" HeaderText="发件人"></asp:BoundColumn><asp:TemplateColumn HeaderText="主题">
<ItemTemplate>
                                    <asp:LinkButton CausesValidation="false" CommandName="Select" Font-Underline="false" Text='<%# Convert.ToString(Eval("v_zt")) %>' runat="server"></asp:LinkButton>
                                </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="dtm_fssj" HeaderText="时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"></asp:BoundColumn><asp:TemplateColumn HeaderText="附件">
<ItemTemplate>
                                    <asp:Image ID="Image1" ImageAlign="Middle" ImageUrl="~/images/icon_att0b3dfa.gif" runat="server" />
                                </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="I_FJDX" HeaderText="大小"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid><font color="Navy">每页显示</font><asp:TextBox ID="tbSet" Width="30px" runat="server"></asp:TextBox><font color="Navy">条</font><asp:Button ID="btnSet" Text="OK" OnClick="btnSet_Click" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
