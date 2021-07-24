<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editmodule.aspx.cs" Inherits="EditModule" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title id="PageTitle">EditModule</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/jquery.easyui.extension.css" />

	<script type="text/javascript" src="../../../Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../../../Script/jw.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$('#btnSave')[0].onclick = function () { if (!$('#Form1').form('validate')) return false; }
			$('#btnClose').click(function () {
				jw.closeWin();
			});
		});
	</script>
</head>
<body scroll="no" class="body_popup">
	<form id="Form1" method="post" runat="server">
	<table id="Table1" width="100%" border="0" class="table-form">
		<tr>
			<td align="right" class="td-label">
				模块代码：
			</td>
			<td>
				<asp:TextBox ID="tbxPreCode" Columns="12" runat="server"></asp:TextBox>
				<asp:TextBox ID="tbxModuleCode" Columns="4" MaxLength="2" ReadOnly="true" runat="server"></asp:TextBox>
				<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ErrorMessage="代码规则不正确！" ControlToValidate="tbxModuleCode" ValidationExpression="\d{2}" Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
			</td>
		</tr>
		<tr>
			<td align="right" width="120" class="td-label">
				模块名称：
			</td>
			<td>
				<asp:TextBox ID="tbxModuleName" CssClass="easyui-validatebox" data-options="required:true, validType:'validChars[50]'" Columns="30" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td align="right" class="td-label">
				序号：
			</td>
			<td>
				<asp:TextBox ID="tbxModuleOrder" CssClass="easyui-validatebox easyui-numberbox" required="required" data-options="min:0,max:999" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td align="right" class="td-label">
				菜单路径：
			</td>
			<td>
				<asp:TextBox ID="tbxModulePath" Columns="45" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td align="right" class="td-label">
				对应图标：
			</td>
			<td>
				<asp:TextBox ID="tbxModuleIcon" Columns="45" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td align="right" class="td-label">
				是否基本权限：
			</td>
			<td>
				<asp:CheckBox ID="cbBasic" runat="server" />
			</td>
		</tr>
		<tr>
			<td align="right" class="td-label">
				帮助路径：
			</td>
			<td>
				<asp:TextBox ID="TxthelpPath" Columns="45" MaxLength="50" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr style="display: none">
			<td align="right" class="td-label">
				是否维护页面：
			</td>
			<td>
				<asp:CheckBox ID="cbMaintainable" runat="server" />
			</td>
		</tr>
		<tr>
			<td colspan="2" class="td-submit">
				<asp:Button ID="btnSave" Text="保  存" OnClick="btnSave_Click" runat="server" />
				<input type="button" id="btnClose" value="关  闭" />
			</td>
		</tr>
	</table>
	<asp:ValidationSummary ID="ValidationSummary1" EnableClientScript="true" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
	</form>
</body>
</html>
