<%@ Page Language="C#" AutoEventWireup="true" CodeFile="linkmanlist.aspx.cs" Inherits="LinkmanList" EnableEventValidation="false" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>LinkmanList</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
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
    <script type="text/javascript">
        //添加/编辑
        function openWin(type) {
        	var id = $('#hfldId').val();
            var url = '/oa/AddressList/ClientAddressList/broker.aspx?Op=' + type + '&Client=' + $('#hdnCorp').val();
            if (type == 'Mod') {
                url = url + '&id=' + id;
            }
            var addInfo = { url: url, title: '联系人', width: 450, height: 550 };
            top.ui.callback = refresh;
            top.ui.openWin(addInfo);
        }
        //刷新联系人
        function refresh(corp) {
            $('#hdnCorp').val(corp);
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
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center" border="0">
            <tr>
                <td class="td-title" align="center">
                    <asp:Label ID="lblTitle" Font-Size="Medium" Font-Bold="true" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style=" height:1px;">
                <td align="right" class="td-toolsbar">
                    
                    <input type="button" id="btnAddLinkman" class="button-normal" value="增加联系人" onclick="openWin('Add');" style="display: none; width: 80px;" runat="server" />

                    &nbsp;
                    <input type="button" id="btnEdit" class="button-normal" onclick="openWin('Mod');" style="display: none;" runat="server" />

                    <asp:Button ID="btnDelLinkman" Visible="false" CssClass="button-normal" Text="删除选中" OnClick="btnDelLinkman_Click" runat="server" />
                    <asp:Button ID="btnGroupManage" CssClass="button-normal" Text="公司管理" OnClick="btnGroupManage_Click" runat="server" />
                    <asp:Button ID="btnAddGroup" Visible="false" CssClass="button-normal" Text="增加公司" OnClick="btnAddGroup_Click" runat="server" />
                    <asp:Button ID="btnExp" CssClass="button-normal" Text="导 出" OnClick="btnExp_Click1" runat="server" />
                </td>
            </tr>
            <tr id="trLinkManInfo" runat="server"><td runat="server">
                    <div class="gridBox" style=" overflow:auto;">
                        <asp:DataGrid ID="dgLinkman" AutoGenerateColumns="false" Width="100%" DataKeyField="i_id" OnPreRender="dgLinkman_PreRender" runat="server"><SelectedItemStyle Wrap="false"></SelectedItemStyle><AlternatingItemStyle Wrap="false"></AlternatingItemStyle><ItemStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn>
<HeaderTemplate>
                                        <asp:CheckBox ID="cbSelAllLinkman" Text="全选" runat="server" />
                                    </HeaderTemplate>

<ItemTemplate>
                                        <asp:CheckBox ID="cbLinkman" runat="server" />
                                    </ItemTemplate>
</asp:TemplateColumn><asp:HyperLinkColumn DataTextField="v_xm" HeaderText="姓名"></asp:HyperLinkColumn><asp:BoundColumn DataField="v_bgdh" HeaderText="办公电话（外线）" DataFormatString="{0}"></asp:BoundColumn><asp:BoundColumn DataField="v_sj" HeaderText="手机" DataFormatString="{0}"></asp:BoundColumn><asp:BoundColumn DataField="v_cz" HeaderText="传真"></asp:BoundColumn><asp:BoundColumn DataField="v_dzyx" HeaderText="Email" DataFormatString="{0}"></asp:BoundColumn><asp:TemplateColumn>
<ItemTemplate>
                                        <span class="link" onclick="clickEdit('<%# Eval("i_id") %>')">编辑</span>
                                    </ItemTemplate>
</asp:TemplateColumn></Columns></asp:DataGrid></div>
                </td></tr>
            <tr id="trCompanyButton" style=" display:none; height:1px;" runat="server"><td runat="server">
                    <asp:Panel ID="Panel1" Visible="false" runat="server">
                        公司名称
                        <asp:TextBox ID="tbGSMC" MaxLength="15" runat="server"></asp:TextBox>地址
                        <asp:TextBox ID="tbDZ" Width="184px" MaxLength="20" runat="server"></asp:TextBox>邮编
                        <asp:TextBox ID="tbYB" Width="93px" MaxLength="6" runat="server"></asp:TextBox>
                        <asp:Button ID="btnAddGroupConfirm" Text="新  增" CssClass="button-normal" OnClick="btnAddGroupConfirm_Click" runat="server" />&nbsp;<input class="button-normal" id="btCancel" type="button" value="取消" name="Button1" OnServerClick="btCancel_ServerClick" runat="server" />
</asp:Panel>
                </td></tr>
            <tr id="trCompanyInfo" style=" display:none;" runat="server"><td runat="server">
                <div class="gridBox">
                    <asp:DataGrid ID="dgGroup" Visible="false" CssClass="table-normal" AutoGenerateColumns="false" Width="100%" CellPadding="4" DataKeyField="i_id" runat="server"><SelectedItemStyle Wrap="false"></SelectedItemStyle><AlternatingItemStyle Wrap="false"></AlternatingItemStyle><ItemStyle Wrap="false" HorizontalAlign="Center"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="公司名称">
<ItemTemplate>
                                    <asp:Label ID="Label1" Text='<%# Convert.ToString(Eval("v_mc")) %>' runat="server"></asp:Label>
                                </ItemTemplate>

<EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" Width="100%" MaxLength="15" Text='<%# Convert.ToString(Eval("v_mc")) %>' runat="server"></asp:TextBox>
                                </EditItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="地址">
<ItemTemplate>
                                    <asp:Label ID="Label2" Text='<%# Convert.ToString(Eval("v_dz")) %>' runat="server"></asp:Label>
                                </ItemTemplate>

<EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" Width="100%" MaxLength="20" Text='<%# Convert.ToString(Eval("v_dz")) %>' runat="server"></asp:TextBox>
                                </EditItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="邮编">
<ItemTemplate>
                                    <asp:Label ID="Label3" Text='<%# Convert.ToString(Eval("v_yb")) %>' runat="server"></asp:Label>
                                </ItemTemplate>

<EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" Width="100%" MaxLength="6" Text='<%# Convert.ToString(Eval("v_yb")) %>' runat="server"></asp:TextBox>
                                </EditItemTemplate>
</asp:TemplateColumn><asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" CancelText="取消" EditText="编辑"></asp:EditCommandColumn><asp:ButtonColumn Text="删除" CommandName="Delete"></asp:ButtonColumn></Columns></asp:DataGrid>
                    </div>
                </td></tr>
        </table>
    </font>
    <input id="hdnCorp" type="hidden" name="Hidden1" runat="server" />

    <asp:HiddenField ID="hfldId" runat="server" />
    <asp:Button ID="btnRefresh" Style="display: none;" OnClick="btnRefresh_Click" runat="server" />
    </form>
</body>
</html>
