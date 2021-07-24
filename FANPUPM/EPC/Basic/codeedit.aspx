<%@ Page Language="C#" AutoEventWireup="true" CodeFile="codeedit.aspx.cs" Inherits="CodeEdit" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>基本编码编辑</title><link href="/StockManage/skins/sm4.css" rel="stylesheet" type="text/css" />
<link href="/StyleCss/Common.css" rel="stylesheet" type="text/css" />

	<style type="text/css">
		.buttonq
		{
			background: #fff url(/images1/btnBack3.jpg);
			text-align: center;
			vertical-align: middle;
			width: 51px;
			height: 20px;
			border-style: none;
			border-left: solid 1px #dadada;
			border-right: solid 1px #dadada;
		}
		.texta
		{
			border: solid 1px #B5CCDE;
			width: 99%;
			height: 20px;
		}
	</style>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
		});
	</script>
</head>
<body scroll="no">
	<form id="Form1" method="post" runat="server">
	<table width="100%">
		<tr>
			<td class="divHeader" height="20">
				<asp:Label ID="LblBasicCode" runat="server"></asp:Label>
			</td>
		</tr>
	</table>
	<table class="tableContent2" id="TableCode" cellspacing="0" cellpadding="5px" width="100%"
		border="0">
		<tr>
			<td class="word" width="25%">
				编码名称
			</td>
			<td class="txt" colspan="3">
				<asp:TextBox ID="TxtCodeName" CssClass="texta" MaxLength="50" Width="100%" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td align="right" class="word">
				&nbsp;
			</td>
			<td class="txt" colspan="3">
				<asp:CheckBox ID="ChkIsDefault" Text="是否默认" runat="server" />
			</td>
		</tr>
	</table>
	<div class="divFooter2">
		<table class="tableFooter2">
			<tr>
				<td colspan="4" height="20">
					<asp:Button ID="BtnSave" Text="保  存" OnClick="BtnAdd_Click" runat="server" />&nbsp;
					<input id="BtnClose" type="button" onclick="top.ui.closeTab();" class="buttonq" value="取  消"
						name="BtnClose" />
					<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
				</td>
			</tr>
		</table>
	</div>
	<asp:ValidationSummary ID="ValSErrmsg" ShowSummary="false" ShowMessageBox="true" DisplayMode="List" runat="server"></asp:ValidationSummary>
	<asp:RequiredFieldValidator ID="RFValTxtCodeName" Display="None" EnableViewState="false" ControlToValidate="TxtCodeName" ErrorMessage="编码名称不能为空" runat="server"></asp:RequiredFieldValidator></form>
</body>
</html>
