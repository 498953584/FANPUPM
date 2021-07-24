<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleEdit.aspx.cs" Inherits="RoleEidt" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>角色编辑</title>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript">
		function successed() {
			top.ui.show('保存成功');
			top.ui.closeWin();
			var url = { "url": "/EPC/Workflow/FlowRole.aspx?val=" + document.getElementById("hfldSelectedValue").value };
			top.ui.reloadTab(url);
		}
	</script>
    <base target="_self" />
</head>
<body scroll="no">
    <form id="Form1" method="post" runat="server">
    <table id="tableVersion" class="tableAudit" cellspacing="0" cellpadding="0" border="1"
        width="100%">
        <tr>
            <td class="divHeader" colspan="2" height="20">
                角色维护
            </td>
        </tr>
        <tr>
            <td width="25%" class="td-label">
                角色名称
            </td>
            <td>
                <asp:TextBox ID="txtRoleName" Width="160px" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtroleName" Display="None" ErrorMessage="角色名称不能为空！" runat="server"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="td-label">
                角色类型
            </td>
            <td>
                <asp:DropDownList ID="ddlRoleType" Enabled="false" runat="server"><asp:ListItem Value="1" Text="项目相关" /><asp:ListItem Value="2" Text="群组" /><asp:ListItem Value="3" Text="部门相关" /></asp:DropDownList>
                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
            </td>
        </tr>
        <tr>
            <td class="td-label" width="25%">
                备注
            </td>
            <td style="width: 300px">
                <asp:TextBox ID="txtRemark" TextMode="MultiLine" Height="80px" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="20" style="text-align: right">
                <asp:Button ID="BtnAdd" Text="保  存" runat="server" />&nbsp;
                <input id="BtnClose" onclick="top.ui.closeWin();" type="button" value="关  闭" name="BtnClose">
                <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
            </td>
        </tr>
    </table>
	<asp:HiddenField ID="hfldSelectedValue" runat="server" />
    </form>
</body>
</html>
