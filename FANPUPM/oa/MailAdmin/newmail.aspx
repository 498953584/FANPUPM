<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newmail.aspx.cs" Inherits="NewMail" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server"><title>NewMail</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

    <base target="_self" />
    <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script src="../../StockManage/script/Config2.js"  type="text/javascript"></script>

    <script src="../../StockManage/script/JustWinTable.js" type="text/javascript"></script>

    <script type="text/javascript">
        window.onload = function()
        {
            var aa = new JustWinTable('DGrdNew');
        }
    </script>

</head>
<body class="body_frame" scroll="no">
    <form id="Form1" method="post" runat="server">
    <table class="table-normal" id="Table1" cellspacing="0" cellpadding="0" width="100%"
        align="center" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" align="right" border="0">
                    <tr align="center" class="td-toolsbar">
                        <td align="left">
                            <input id="HdnMailSpace" style="width: 1px; height: 1px" type="hidden" name="HdnMailSpace" runat="server" />
<input id="HdnMailNumber" style="width: 1px; height: 1px" type="hidden" name="HdnMailNumber" runat="server" />
&nbsp;<asp:Label ID="LabMail" runat="server"></asp:Label>&nbsp;
                        </td>
                        <td colspan="4">
                            <asp:Button ID="BtnDelN" CssClass="button-normal" Width="80px" Text="删 除" OnClick="BtnDelN_Click" runat="server" />
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
                <asp:DataGrid ID="DGrdNew" Width="100%" HorizontalAlign="Center" DataKeyField="i_sjid" PageSize="20" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" OnPreRender="DGrdDNew_PreRender" OnSelectedIndexChanged="DGrdNew_SelectedIndexChanged" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn>
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
</asp:TemplateColumn><asp:TemplateColumn HeaderText="发信人">
<ItemTemplate>
                                <asp:Label Text='<%# Convert.ToString(Eval("v_fxrxm", "{0}...")) %>' runat="server"></asp:Label>
                            </ItemTemplate>
</asp:TemplateColumn><asp:ButtonColumn Text="选择" DataTextField="v_zt" HeaderText="主题" CommandName="Select"></asp:ButtonColumn><asp:BoundColumn DataField="dtm_fssj" HeaderText="时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"></asp:BoundColumn><asp:TemplateColumn HeaderText="附件">
<ItemTemplate>
                                <asp:Image ID="Image1" ImageUrl="~/images/icon_att0b3dfa.gif" runat="server" />
                            </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="i_fjdx" HeaderText="大小"></asp:BoundColumn></Columns><PagerStyle HorizontalAlign="Center" Mode="NumericPages"></PagerStyle></asp:DataGrid>每页显示<asp:TextBox ID="tbSet" Width="30px" runat="server"></asp:TextBox>条<asp:Button ID="btnSet" Text="OK" OnClick="btnSet_Click" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
