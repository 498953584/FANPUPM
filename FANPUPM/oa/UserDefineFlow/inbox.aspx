<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inbox.aspx.cs" Inherits="InBox" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>InBox</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

    <base target="_self">
    </base>
    <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></head>
<body class="body_clear" style="font-size: large; text-decoration: overline">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0"
            class="table-normal">
            <tr>
                <td style="height: 43px">
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr class="td-toolsbar">
                            <td align="left">
                                &nbsp;<asp:Label ID="LabMail" runat="server"></asp:Label>&nbsp;</td>
                            <td colspan="4">
                                <asp:Button ID="BtnDelN" Text="删 除" CssClass="button-normal" Visible="false" OnClick="BtnDelN_Click" runat="server" />
                                <asp:Button ID="BtnDelY" Text="彻底删除" Width="80px" CssClass="button-normal" Visible="false" OnClick="BtnDelY_Click" runat="server" />
                                <asp:Button ID="BtnMove" Text="转移到" CssClass="button-normal" Visible="false" OnClick="BtnMove_Click" runat="server" />
                                <asp:DropDownList ID="DDLtBox" Visible="false" runat="server"></asp:DropDownList></td>
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
</asp:TemplateColumn><asp:BoundColumn DataField="v_fxrxm" HeaderText="发件人"></asp:BoundColumn><asp:BoundColumn DataField="V_JSRXM" HeaderText="收件人"></asp:BoundColumn><asp:TemplateColumn HeaderText="主题"><ItemTemplate>
                                    <asp:LinkButton CausesValidation="false" CommandName="Select" Font-Underline="false" Text='<%# System.Convert.ToString(Eval("v_zt"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:LinkButton>
                                </ItemTemplate></asp:TemplateColumn><asp:BoundColumn DataField="dtm_fssj" HeaderText="发信时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"></asp:BoundColumn><asp:TemplateColumn HeaderText="附件">
<ItemTemplate>
                                    <asp:Image ID="Image1" ImageAlign="Middle" ImageUrl="~/images1/fujian.gif" runat="server" />
                                </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="I_FJDX" HeaderText="大小"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid><font color="Navy">每页显示</font><asp:TextBox ID="tbSet" Width="30px" runat="server"></asp:TextBox><font color="Navy">条</font><asp:Button ID="btnSet" Text="OK" OnClick="btnSet_Click" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
