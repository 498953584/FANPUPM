<%@ Page Language="C#" AutoEventWireup="true" CodeFile="linkmansearch.aspx.cs" Inherits="LinkmanSearch" EnableEventValidation="false" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_expbtn_ascx" Src="~/EPC/UserControl1/ExpBtn.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>LinkmanSearch</title><meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />


    <script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>

    <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></head>
<body class="body_clear" scroll="yes">
    <form id="Form1" method="post" runat="server">
    <font face="宋体">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center"
            border="0">
            <tr class="td-search">
                <td align="left">
                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                </td>
                <td width="60%">
                    搜索
                    <asp:TextBox ID="tbKey" Width="70px" runat="server"></asp:TextBox>按
                    <asp:DropDownList ID="ddlClass" Width="62px" runat="server"><asp:ListItem Value="v_xm" Selected="true" Text="姓名" /><asp:ListItem Value="v_bgdh" Text="办公电话（外线）" /><asp:ListItem Value="v_cz" Text="传真" /><asp:ListItem Value="v_zzdh" Text="住宅电话" /><asp:ListItem Value="v_sj" Text="手机" /></asp:DropDownList>
                    <asp:Button ID="btnSearch" CssClass="button-normal" OnClick="btnSearch_Click" runat="server" />
                </td>
            </tr>
            <TR >
						<TD style="HEIGHT: 30px" align="left"  class=td-title>
                            <table style="width:100%;">
                                <tr>
                                    <td style=" width:100px">
                                        地址:</td>
                                    <td>
                                        &nbsp;
                                        <asp:Label ID="labaddress" ForeColor="#000099" runat="server"></asp:Label>
                                    </td>
                                   
                                </tr>
                                <tr>
                                    <td class="style1">
                                        邮编:</td>
                                    <td>
                                        &nbsp;
                                        <asp:Label ID="labyb" Text="" ForeColor="#000099" runat="server"></asp:Label>
                                    </td>
                                  
                                </tr>
                                <tr>
                                    <td class="style1">
                                        传真:</td>
                                    <td>
                                        &nbsp;
                                        <asp:Label ID="labfx" Text="" ForeColor="#000099" runat="server"></asp:Label>
                                    </td>
                                   
                                </tr>
                            </table>
                        </TD>
					</TR>
            <tr class="td-toolsbar">
                <td align="right" colspan="2">
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnMessage" Text="短信服务" CssClass="button-normal" Visible="false" OnClick="btnMessage_Click" runat="server" />
                    <asp:Button ID="btnExp" CssClass="button-normal" Text="导 出" OnClick="btnExp_Click1" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="gridBox">
                        <asp:DataGrid ID="dgLinkman" DataKeyField="i_id" Width="100%" AutoGenerateColumns="false" UseAccessibleHeader="true" OnPreRender="dgLinkman_PreRender" runat="server"><SelectedItemStyle Wrap="false"></SelectedItemStyle><AlternatingItemStyle Wrap="false"></AlternatingItemStyle><ItemStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn Visible="false">
<HeaderTemplate>
                                        <asp:CheckBox ID="cbSelAll" Text="全选" runat="server" />
                                    </HeaderTemplate>

<ItemTemplate>
                                        <asp:CheckBox ID="cbItem" runat="server" />
                                    </ItemTemplate>
</asp:TemplateColumn><asp:HyperLinkColumn DataTextField="v_xm" HeaderText="姓名"></asp:HyperLinkColumn><asp:BoundColumn DataField="v_bmmc" HeaderText="部门"></asp:BoundColumn><asp:BoundColumn DataField="v_zw" HeaderText="职位" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="v_bgdh" HeaderText="办公电话（外线）"></asp:BoundColumn><asp:BoundColumn DataField="v_sj" HeaderText="手机"></asp:BoundColumn><asp:BoundColumn DataField="v_dzyx" HeaderText="Email"></asp:BoundColumn><asp:TemplateColumn Visible="false">
<ItemTemplate>
                                        <asp:LinkButton ID="lbtnEdit" runat="server">编辑</asp:LinkButton>
                                    </ItemTemplate>
</asp:TemplateColumn></Columns></asp:DataGrid></div>
                </td>
            </tr>
        </table>
    </font>
    </form>
</body>
</html>
