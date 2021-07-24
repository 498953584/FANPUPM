<%@ Page Language="C#" AutoEventWireup="true" CodeFile="linkmanlist.aspx.cs" Inherits="LinkmanList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>通讯录</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

    <style type="text/css">
        .link
        {
            cursor: pointer;
            text-decoration: underline;
            color: Blue;
        }
    </style>
    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../../Script/jw.js"></script>
    <script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
    <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
    <script type="text/javascript">
        //添加/编辑
        function openWin(type) {
            var id = $('#hfldId').val();
            var url = '/oa/AddressList/PersonalAddressList/broker.aspx?Op=' + type + '&Group=' + $('#hdnGroup').val();
            if (type == 'Mod') {
                url = url + '&id=' + id;
            }
            var addInfo = { url: url, title: '联系人', width: 450, height: 550 };
            top.ui.callback = refresh;
            top.ui.openWin(addInfo);
        }
        //刷新联系人
        function refresh(corp) {
            $('#hdnGroup').val(corp);
            $('#btnRefresh').click();
        }
        //点击编辑
        function clickEdit(id) {
            $('#hfldId').val(id);
            $('#btnEdit').click();
        }
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="Form1" method="post" runat="server">
    <font face="宋体">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center"
            border="0">
            <tr class="td-toolsbar">
                <td class="td-title" align="center">
                    <asp:Label ID="lblTitle" Font-Size="Medium" Font-Bold="true" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style=" height:1px;">
                <td align="right" class="td-toolsbar">
                    
                    <input type="button" id="btnAddLinkman" class="button-normal" value="增加联系人" onclick="openWin('Add');" style="display: none; width: 80px;" runat="server" />

                    &nbsp;
                    <input type="button" id="btnEdit" class="button-normal" onclick="openWin('Mod');" style="display: none;" runat="server" />

                    <asp:Button ID="btnDelLinkman" Visible="false" CssClass="button-normal" Text="删除选中" OnClick="btnDelLinkman_Click" runat="server" />&nbsp;
                    <asp:Button ID="btnMessage" Text="短信服务" CssClass="button-normal" Visible="false" OnClick="btnMessage_Click" runat="server" /><asp:Button ID="btnGroupManage" CssClass="button-normal" Text="分组管理" OnClick="btnGroupManage_Click" runat="server" /><asp:Button ID="btnAddGroup" Visible="false" CssClass="button-normal" Text="增加分组" OnClick="btnAddGroup_Click" runat="server" />&nbsp;
                </td>
            </tr>
            <tr id="trLinkmanInfo" runat="server"><td runat="server">
                    <div class="gridBox">
                        <asp:DataGrid ID="dgLinkman" AutoGenerateColumns="false" Width="100%" DataKeyField="i_grtxl_id" OnPreRender="dgLinkman_PreRender" runat="server"><SelectedItemStyle Wrap="false"></SelectedItemStyle><AlternatingItemStyle Wrap="false"></AlternatingItemStyle><ItemStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn>
<HeaderTemplate>
                                        <asp:CheckBox ID="cbSelAllLinkman" Text="全选" runat="server" />
                                    </HeaderTemplate>

<ItemTemplate>
                                        <asp:CheckBox ID="cbLinkman" runat="server" />
                                    </ItemTemplate>
</asp:TemplateColumn><asp:HyperLinkColumn DataTextField="v_xm" HeaderText="姓名"></asp:HyperLinkColumn><asp:BoundColumn DataField="v_dw" HeaderText="单位" DataFormatString="{0}"></asp:BoundColumn><asp:BoundColumn DataField="v_bgdh" HeaderText="办公电话" DataFormatString="{0}"></asp:BoundColumn><asp:BoundColumn DataField="v_sj" HeaderText="手机" DataFormatString="{0}"></asp:BoundColumn><asp:BoundColumn DataField="v_zzdh" HeaderText="住宅电话" DataFormatString="{0}"></asp:BoundColumn><asp:TemplateColumn>
<ItemTemplate>
                                        <span class="link" onclick="clickEdit('<%# Eval("i_grtxl_id") %>')">编辑</span>
                                    </ItemTemplate>
</asp:TemplateColumn></Columns></asp:DataGrid></div>
                </td></tr>
            <tr id="trGroupButton" style="display: none; height:1px;" runat="server"><td runat="server">
                    <asp:Panel ID="Panel1" Visible="false" runat="server">
                        分组名称：
                        <asp:TextBox ID="tbFZMC" MaxLength="15" runat="server"></asp:TextBox>备注：
                        <asp:TextBox ID="tbBZ" Width="248px" MaxLength="20" runat="server"></asp:TextBox>
                        <asp:Button ID="btnAddGroupConfirm" Text="新  增" CssClass="button-normal" OnClick="btnAddGroupConfirm_Click" runat="server" />&nbsp;<input class="button-normal" id="Button1" type="button" value="取消" name="Button1" OnServerClick="Button1_ServerClick" runat="server" />
</asp:Panel>
                </td></tr>
            <tr id="trGroupInfo" style="display: none;" runat="server"><td runat="server">
                    <div class="gridBox">
                        <asp:DataGrid ID="dgGroup" Visible="false" CssClass="table-normal" AutoGenerateColumns="false" Width="100%" DataKeyField="i_id" CellPadding="4" runat="server"><SelectedItemStyle Wrap="false"></SelectedItemStyle><AlternatingItemStyle Wrap="false"></AlternatingItemStyle><ItemStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="分组名称">
<ItemTemplate>
                                        <asp:Label ID="Label1" Text='<%# Convert.ToString(Eval("v_fzmc", "{0}")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>

<EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" Width="100%" MaxLength="20" Text='<%# Convert.ToString(Eval("v_fzmc", "{0}")) %>' runat="server"></asp:TextBox>
                                    </EditItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="备注">
<ItemTemplate>
                                        <asp:Label ID="Label2" Text='<%# Convert.ToString(Eval("v_bz", "{0}")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>

<EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" Width="100%" MaxLength="50" Text='<%# Convert.ToString(Eval("v_bz", "{0}")) %>' runat="server"></asp:TextBox>
                                    </EditItemTemplate>
</asp:TemplateColumn><asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" CancelText="取消" EditText="编辑"></asp:EditCommandColumn><asp:ButtonColumn Text="删除" CommandName="Delete"></asp:ButtonColumn></Columns></asp:DataGrid>
                    </div>
                </td></tr>
        </table>
    </font>
    <input id="hdnGroup" type="hidden" name="Hidden1" runat="server" />

    <asp:HiddenField ID="hfldId" runat="server" />
    <asp:Button ID="btnRefresh" Style="display: none;" OnClick="btnRefresh_Click" runat="server" />
    </form>
</body>
</html>
