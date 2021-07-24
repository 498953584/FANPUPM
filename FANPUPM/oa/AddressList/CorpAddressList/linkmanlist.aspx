<%@ Page Language="C#" AutoEventWireup="true" CodeFile="linkmanlist.aspx.cs" Inherits="LinkmanList" EnableEventValidation="false" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>LinkmanList</title>
    <base target="_self" />
    <meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

    <script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
    <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
    <style type="text/css">
        .style1
        {
            text-align: right;
        }
        .link
        {
            cursor: pointer;
            text-decoration: underline;
            color: Blue;
        }
    </style>
    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../../Script/jw.js"></script>
    <script language="javascript" type="text/javascript">
        function DbClickEvent(d) {
            window.alert("事件类型: DoubleClidk  作用对象: " + d);
        }

        function ClickRow(dpid) {
            document.getElementById('btnDel').disabled = false;

            document.getElementById('HdnDPID').value = dpid;
            document.getElementById('HdnID').value = id;
        }

        function setUp() {
            var url = '/oa/AddressList/CorpAddressList/PubSet.aspx?iDept=' + $('#hdnDept').val();
            var setInfo = { url: url, title: '公告信息', width: 500, height: 350 };
            top.ui.callback = refresh;
            top.ui.openWin(setInfo);
        }

        //添加/编辑
        function openWin(type) {
            var id = $('#hfldId').val();
            var url = '/oa/AddressList/CorpAddressList/Broker.aspx?' + new Date().getTime() + '&Op=' + type + '&iDept=' + $('#hdnDept').val();
            if (type == 'Mod') {
                url = url + '&id=' + id;
            }
            var addInfo = { url: url, title: '联系人', width: 450, height: 550 };
            top.ui.callback = refresh;
            top.ui.openWin(addInfo);
        }

        //导入
        function impUser() {
            var url = '/oa/MailAdmin/Consignee.aspx?depId=' + $('#hdnDept').val();
            var impInfo = { url: url, title: '导入联系人', width: 659, height: 450 };
            top.ui.callback = getImpUser;
            top.ui.openWin(impInfo);
        }
        //获得导入的联系人
        function getImpUser(depId, human) {
            for (var i = 0; i < human.length; i++) {
                document.getElementById('hf').value += human[i] + ",";
            }
            $('#hdnDept').val(depId);
            $('#btnModfAdd').click();
        }
        //刷新联系人
        function refresh(depId) {
            $('#hdnDept').val(depId);
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
            <tr>
                <td style="height: 30px" align="center" class="td-title">
                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 30px" align="left" class="td-title">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 100px; text-align: right;">
                                地&nbsp; 址:
                            </td>
                            <td>
                                <asp:Label ID="labaddress" ForeColor="#000099" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                邮&nbsp; 编:
                            </td>
                            <td>
                                <asp:Label ID="labyb" Text="" ForeColor="#000099" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                传&nbsp; 真:
                            </td>
                            <td>
                                <asp:Label ID="labfx" Text="" ForeColor="#000099" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="td-toolsbar">
                <td align="right">
                    <font face="宋体">
                        <input type="button" id="btnpubSet" value="设 置" class="button-normal" onclick="setUp();" runat="server" />

                        &nbsp;</font>
                    <input type="button" id="btnAdd" value="新 增" class="button-normal" onclick="openWin('Add','');" runat="server" />

                    <input type="button" id="btnEdit" class="button-normal" onclick="openWin('Mod');" style="display: none;" runat="server" />

                    <input type="button" id="btnModf" value="导 入" class="button-normal" onclick="impUser();" runat="server" />

                    <asp:Button ID="btnModfAdd" CssClass="button-normal" Style="display: none;" OnClick="btnModfAdd_Click" runat="server" />
                    <asp:HiddenField ID="hf" runat="server" />
                    <asp:Button ID="btnDel" Text="删 除" CssClass="button-normal" OnClick="btnDel_Click" runat="server" />
                    <asp:Button ID="btnExp" CssClass="button-normal" Text="导 出" Style="display: none;" OnClick="btnExp_Click1" runat="server" />
                    <asp:Button ID="btnMessage" Text="短信服务" CssClass="button-normal" Visible="false" OnClick="btnMessage_Click" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <div class="gridBox">
                        <asp:DataGrid ID="dgLinkman" DataKeyField="i_id" Width="100%" CssClass="grid" AutoGenerateColumns="false" OnPreRender="dgLinkman_PreRender" runat="server"><SelectedItemStyle Wrap="false"></SelectedItemStyle><AlternatingItemStyle Wrap="false"></AlternatingItemStyle><ItemStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn>
<HeaderTemplate>
                                        <asp:CheckBox ID="cbSelAll" Text="全选" runat="server" />
                                    </HeaderTemplate>

<ItemTemplate>
                                        <asp:CheckBox ID="cbItem" runat="server" />
                                    </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="序号"><ItemTemplate>
                                        <%# Container.ItemIndex + 1 %>
                                    </ItemTemplate></asp:TemplateColumn><asp:HyperLinkColumn DataTextField="v_xm" HeaderText="姓名"></asp:HyperLinkColumn><asp:BoundColumn DataField="v_bmmc" HeaderText="部门"></asp:BoundColumn><asp:BoundColumn DataField="v_zw" HeaderText="职务" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="v_bgdh" HeaderText="办公电话（外线）"></asp:BoundColumn><asp:BoundColumn DataField="v_sj" HeaderText="手机"></asp:BoundColumn><asp:TemplateColumn>
<ItemTemplate>
                                        <span class="link" onclick="clickEdit('<%# Eval("i_id") %>')">编辑</span>
                                    </ItemTemplate>
</asp:TemplateColumn></Columns></asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    </font>
    <asp:Button ID="btnRefresh" Style="display: none;" OnClick="btnRefresh_Click" runat="server" />
    <asp:HiddenField ID="hfldId" runat="server" />
    <input id="hdnDept" type="hidden" name="hdnDept" runat="server" />

    </form>
</body>
</html>
