<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocClassEdit.aspx.cs" Inherits="DocClassEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server"><title>公文分类维护</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <base target="_self"></base>
    <script type="text/javascript">
        function successed(action) {
            top.ui.show(action + '成功!');
            top.ui.closeWin();
            top.ui.reloadTab();
        }
    </script>
</head>
<body scroll="no" class="body_popup">
    <form id="Form1" method="post" runat="server">
    <table class="table-form" id="Table1" height="100%" cellspacing="0" cellpadding="0"
        width="100%" border="1">
        <tr>
            <td id="Td_title" class="td-head" colspan="4" height="20" runat="server">
                公文分类
            </td>
        </tr>
        <tr>
            <td class="td-label" width="25%">
                分类编码
            </td>
            <td>
                <asp:TextBox ID="txtClassCode" CssClass="text-input" TabIndex="1" Width="80%" MaxLength="3" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font>
                <input id="hdnClassCode" style="width: 10px" type="hidden" name="hdnClassCode" runat="server" />

            </td>
        </tr>
        <tr>
            <td class="td-label" width="25%">
                分类名称
            </td>
            <td>
                <asp:TextBox ID="txtClassName" CssClass="text-input" TabIndex="2" Width="80%" MaxLength="10" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font>
                <input id="hdnClassName" style="width: 10px" type="hidden" name="hdnClassName" runat="server" />

            </td>
        </tr>
        <tr>
            <td class="td-label" width="25%">
                备注
            </td>
            <td>
                <asp:TextBox ID="txtRemark" CssClass="text-input" TabIndex="3" Width="80%" TextMode="MultiLine" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="25%">
                是否有效
            </td>
            <td>
                <asp:DropDownList ID="ddltIsValid" Width="80%" TabIndex="4" runat="server"><asp:ListItem Value="1" Text="有效" /><asp:ListItem Value="0" Text="无效" /></asp:DropDownList>
                <input id="hdnIsValid" style="width: 10px" type="hidden" name="hdnIsValid" runat="server" />

            </td>
        </tr>
        <tr>
            <td class="td-submit" colspan="4" height="20">
                <asp:Button ID="BtnAdd" Text="保 存" OnClick="BtnAdd_Click" runat="server" />&nbsp;
                <input id="BtnClose" onclick="top.ui.closeWin();" type="button" value="取 消" name="BtnClose">
            </td>
        </tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
