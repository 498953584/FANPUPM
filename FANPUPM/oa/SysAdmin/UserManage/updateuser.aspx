<%@ Page Language="C#" AutoEventWireup="true" CodeFile="updateuser.aspx.cs" Inherits="UpdateUser" %>

<!DOCTYPE HTML >
<html>
<head runat="server"><title>权限管理</title></head>
<body class="body_popup" scroll="auto">
	<form id="Form1" method="post" runat="server">
	<table class="table-normal" id="Table1" cellspacing="1" cellpadding="1" style="width: 100%;"
		border="1">
		<tr>
			<td class="td-title" align="center" colspan="2">
				<asp:Label ID="Label1" runat="server">Label</asp:Label>
			</td>
		</tr>
		<tr>
			<td class="td-label">
				用户姓名：
			</td>
			<td>
				<asp:TextBox ID="tbUserName" MaxLength="5" Enabled="false" ReadOnly="true" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="td-label">
				用户登录ID：
			</td>
			<td>
				<asp:TextBox ID="tbID" Enabled="false" ReadOnly="true" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="td-label">
				用户密码：
			</td>
			<td align="left">
				&nbsp;
				<asp:RadioButton ID="rb1" Text="当前密码" Checked="true" GroupName="gPwd" runat="server" /><asp:RadioButton ID="rb2" Text="初始密码:easy" GroupName="gPwd" runat="server" />
			</td>
		</tr>
        <tr>
			<td class="td-label">
				二次验证密码：
			</td>
			<td align="left">
				&nbsp;
                <asp:RadioButton ID="rb3" Text="初始密码:easy" GroupName="Pwd" runat="server" />
			</td>
		</tr>
		<tr id="tr_mailSace" runat="server"><td class="td-label" runat="server">
				邮箱容量：
			</td><td runat="server">
				<asp:TextBox ID="tbSpace" runat="server"></asp:TextBox>M
			</td></tr>
		<tr style="display: none">
			<td class="td-label">
				是否管理员：
			</td>
			<td>
				<asp:CheckBox ID="cbIsManager" Text="是否管理员" runat="server" />
			</td>
		</tr>
		<tr id="tr_role" style="display: none;" runat="server"><td class="td-label" runat="server">
				角色指定：
			</td><td runat="server">
				<div style="width: 100%;">
					<asp:CheckBoxList ID="cblRoleList" DataSourceID="RoleList" DataTextField="RoleName" DataValueField="RoleCode" RepeatColumns="2" RepeatDirection="Horizontal" OnDataBound="cblRoleList_DataBound" runat="server"></asp:CheckBoxList> 
					<asp:SqlDataSource ID="RoleList" SelectCommand="SELECT * FROM PT_Role WHERE IsValid = '1'" SelectCommandType="Text" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
				</div>
			</td></tr>
		<tr class="td-submit">
			<td colspan="2">
				<asp:Button ID="BtnAdd" Text=" 修 改 " CssClass="button-normal" OnClick="BtnAdd_Click" runat="server" />
				<input class="button-normal" id="BtnReset" type="reset" value=" 重 填 " name="BtnReset" />
				<input class="button-normal" id="BtnClose" onclick="top.ui.closeWin();" type="button"
					value=" 关 闭 " name="BtnClose" />
			</td>
		</tr>
	</table>
	<input id="hidd_userCode" style="width: 80px; height: 22px" type="hidden" size="8" name="hidd_userCode" runat="server" />

	<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
	</form>
</body>
</html>
