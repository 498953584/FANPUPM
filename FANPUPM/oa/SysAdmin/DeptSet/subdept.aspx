<%@ Page Language="C#" AutoEventWireup="true" CodeFile="subdept.aspx.cs" Inherits="SubDept" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>SubDept</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<link rel="Stylesheet" type="text/css" href="../../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../../Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <style type="text/css">
        .scro
        {
            overflow: auto;
        }
    </style>
    <script language="JavaScript">
        $(document).ready(function () {
            if ($('#hfldbtnadd').val() == "1") {
                // 添加验证
                $('#BtnAdd')[0].onclick = function () { if (!$('#Form1').form('validate')) return false; }
            }
        });
        function showMember(deptCode) {
            url = "broker.aspx?go=2&bmdm=" + deptCode + "&yx=y";
            window.showModalDialog(url, window, "dialogHeight:400px;dialogWidth:300px;center:1;help:0;status:0;");
        }
        function showPrivilage(deptCode) {
            url = "broker.aspx?go=3&bmdm=" + deptCode;
            window.showModalDialog(url, window, "dialogHeight:320px;dialogWidth:320px;center:1;help:0;status:0;");
        }
    </script>
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" scroll="no" rightmargin="0">
    <font face="宋体">
        <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td align="center">
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr align="center" height="30">
                            <td>
                                <asp:Label ID="LabDept" runat="server"></asp:Label><input id="HdnLevel" style="width: 1px" type="hidden" name="HdnLevel" runat="server" />

                            </td>
                            <td width="90">
                                <asp:Button ID="BtnMove" Text="部门调动" CssClass="button-normal" OnClick="BtnMove_Click" runat="server" />
                            </td>
                            <td width="90">
                                <asp:Button ID="BtnMerge" Text="部门合并" CssClass="button-normal" OnClick="BtnMerge_Click" runat="server" />
                            </td>
                            <td width="90">
                                <input id="HdnDeptCode" style="width: 1px" type="hidden" name="HdnDeptCode" runat="server" />

                                <asp:Button ID="BtnAddSub" Text="增加下级" CssClass="button-normal" OnClick="BtnAddSub_Click" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Panel ID="PanMove" Visible="false" runat="server">
                        <table style="border-collapse: collapse" cellspacing="1" cellpadding="1" width="100%"
                            border="1">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="LabTitle" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLtAllDept" Width="250px" runat="server"></asp:DropDownList>
                                </td>
                                <td align="center">
                                    <asp:Button ID="BtnMoveOK" CssClass="button-normal" Text="确 定" CausesValidation="false" OnClick="BtnMoveOK_Click" runat="server" />
                                    <asp:Button ID="BtnMergeOK" CssClass="button-normal" Text="确 定" CausesValidation="false" OnClick="BtnMergeOK_Click" runat="server" />
                                </td>
                                <td align="center">
                                    <asp:Button ID="BtnMoveCancel" CssClass="button-normal" Text="取 消" OnClick="BtnMoveCancel_Click" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="PanNewDept" Visible="false" runat="server">
                        <table id="Table3" style="border-collapse: collapse" cellspacing="1" cellpadding="1"
                            width="100%" border="1">
                            <tr>
                                <td class="td-label" align="center">
                                    名 称
                                </td>
                                <td width="16%">
                                    <asp:TextBox ID="TBoxDeptName" MaxLength="50" Width="128px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                                </td>
                                <td class="td-label" align="center">
                                    序 号
                                </td>
                                <td width="10%">
                                    <asp:TextBox ID="TBoxNumber" MaxLength="8" Width="64px" runat="server"></asp:TextBox>
                                </td>
                                <td class="td-label" align="center" id="td1" runat="server">
                                    分子机构
                                </td>
                                <td align="center" id="td2" runat="server">
                                    <asp:DropDownList ID="ddl" Width="100px" runat="server"></asp:DropDownList>
                                </td>
                                <td align="center">
                                    <asp:Button ID="BtnAdd" Text="新  增" CssClass="button-normal" OnClick="BtnAdd_Click" runat="server" />
                                </td>
                                <td align="center">
                                    <asp:Button ID="BtnAddCancel" Text="取 消" CssClass="button-normal" OnClick="BtnAddCancel_Click" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td valign="top" align="center" height="400">
                    <asp:Panel ID="PanDept" Width="100%" CssClass="scro" Height="400px" runat="server">
                        <asp:DataGrid ID="DGrdSubDept" Width="100%" AutoGenerateColumns="false" DataKeyField="i_bmdm" CellPadding="0" runat="server"><ItemStyle Height="20px"></ItemStyle><HeaderStyle Height="20px" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="部门">
<ItemTemplate>
                                        <asp:Label ID="Label1" Text='<%# Convert.ToString(Eval("v_bmmc")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>

<EditItemTemplate>
                                        <asp:TextBox MaxLength="50" ID="Textbox1" Width="100%" Text='<%# Convert.ToString(Eval("v_bmmc")) %>' runat="server"></asp:TextBox>
                                    </EditItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="分子机构">
<ItemTemplate>
                                        <asp:Label ID="Lab" Text='<%# Convert.ToString(Eval("CorpName")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>

<EditItemTemplate>
                                        <asp:TextBox ID="Tb" Width="100%" ReadOnly="true" Text='<%# Convert.ToString(Eval("CorpName")) %>' runat="server"></asp:TextBox>
                                    </EditItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
                                        <asp:Label ID="Label3" NAME="Label3" Text='<%# Convert.ToString(Eval("i_xh")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>

<EditItemTemplate>
                                        <asp:TextBox Width="100%" ID="Textbox3" NAME="Textbox3" MaxLength="8" Text='<%# Convert.ToString(Eval("i_xh")) %>' runat="server"></asp:TextBox>
                                    </EditItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="下级">
<ItemTemplate>
                                        <asp:Label ID="Label4" NAME="Label4" Text='<%# Convert.ToString(Eval("i_xjbm", "{0:d}个")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn>
<ItemTemplate>
                                        <a href="#" onclick='<%# Eval("i_bmdm", "showPrivilage({0:d})") %>'>
                                            职务</a>
                                    </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn>
<ItemTemplate>
                                        <a href="#" onclick='<%# Eval("i_bmdm", "showMember({0:d})") %>'>
                                            成员</a>
                                    </ItemTemplate>
</asp:TemplateColumn><asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" CancelText="取消" EditText="编辑"></asp:EditCommandColumn><asp:ButtonColumn Text="删除" CommandName="Delete"></asp:ButtonColumn></Columns></asp:DataGrid>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <div id="WarnBlock" runat="server">
        </div>
        <asp:HiddenField ID="hfldbtnadd" runat="server" />
        </form>
    </font>
</body>
</html>
