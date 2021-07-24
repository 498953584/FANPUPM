<%@ Page Language="C#" AutoEventWireup="true" CodeFile="codelist.aspx.cs" Inherits="CodeList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>基本编码</title><link type="text/css" rel="stylesheet" href="/Script/themes/base/jquery.ui.all.css" />

	<script type="text/javascript" src="../../web_client/tree.js" type="text/javascript"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
	<script type="text/javascript">
		var relationsKey = '';
		$(document).ready(function () {
			// 单位类型
			if (getRequestParam('tid') === '4')
				$('#btnPrivilege').css('display', 'inline');
			if ($('#hfldSignCode').val() == 'ProjectProperty')
				$('#btnPrivilege').css('display', 'inline');
		});

		// 分配权限人员
		function allocUser() {
			var url = '/Common/AllocUser.aspx?type=basicPriv&tableName=XPM_Basic_CodeList&idJson=' + relationsKey;
			top.ui.openWin({ title: '选择人员', url: url });
		}

		var globalCodeID = 0;
		var globalParentCodeID = 0;

		function clickCodeRow(codeID, parentCodeID, isAllowUpd, isAllowDel, noteId) {
			globalCodeID = codeID;
			globalParentCodeID = parentCodeID;
			document.getElementById('HdnCodeID').value = codeID;
			document.getElementById('BtnUpd').disabled = !isAllowUpd;
			document.getElementById('BtnDel').disabled = !isAllowDel;
			relationsKey = noteId;
			var signCode = $('#hfldSignCode').val();
			if (codeID != '0' && parentCodeID == '0') {
				// parentCodeID === '0'表示没有父节点
				$('#btnPrivilege').removeAttr('disabled');
				//单位类型和建筑类别允许增加子节点
				if (signCode != 'CorpType' && signCode != 'jzType') {
					$('#BtnAdd').attr('disabled', 'disabled');
				}
			}
			else {
				$('#btnPrivilege').attr('disabled', 'disabled');
				if (signCode != 'CorpType' && signCode != 'jzType') {
					$('#BtnAdd').removeAttr('disabled');
				}
			}
		}

		function switchDisplay(obj, t1, t2) {
			/*在这之前增加你的处理代码*/
			doSwitchDisplay(obj, 'TblCodeTree', 'HdnCodeUse', t1, t2, '../../'); //调用样式设置
		}
		function isChange() {
			return (document.getElementById('HdnIsChange').value == "1");
		}

		function OpenBasicCodeEdit(opType) {
			var state = document.getElementById("hdnstate").value;
			var typeID = document.getElementById('HdnTypeID').value;

			var url = "/EPC/Basic/CodeEdit.aspx?t=" + opType + "&cid=" + globalCodeID + "&pid=" + globalParentCodeID + "&tid=" + typeID;
			top.ui._codelist = window;
			toolbox_oncommand(url, "类别管理"); //引用js
		}
		function request(paras) {
			var url = location.href;
			var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
			var paraObj = {}
			for (i = 0; j = paraString[i]; i++) {
				paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
			}
			var returnValue = paraObj[paras.toLowerCase()];
			if (typeof (returnValue) == "undefined") {
				return "";
			} else {
				return returnValue;
			}
		}
	</script>
	<style type="text/css">
		.dgheader
		{
			background-color: #EEF2F5;
			white-space: nowrap;
			overflow: hidden;
			font-weight: normal;
			text-align: center;
			border-color: #CADEED;
			color: #727FAA;
			padding-left: 6px;
			padding-right: 6px;
			height: 25px;
		}
		.divFooter
		{
			height: 24px;
			background: url(/images1/divBottomBack.jpg) repeat-x;
			vertical-align: middle;
		}
		.button-1
		{
			background: #fff;
			text-align: center;
			vertical-align: middle;
			width: 51px;
			height: 20px;
			border-style: none;
			border: solid 1px #dadada;
		}
	</style>
</head>
<body class="body_frame" style="overflow: auto;">
	<form id="Form1" method="post" runat="server">
	<table class="table-none" id="Table2" height="100%" cellspacing="0" cellpadding="0"
		width="100%" border="0">
		<tr>
			<td class="divFooter" style="text-align: left">
				<input id="HdnTypeID" type="hidden" size="1" name="HdnTypeID" runat="server" />

				<input id="HdnCodeID" type="hidden" size="1" name="HdnCodeID" runat="server" />

				<input id="HdnCodeUse" type="hidden" size="1" name="HdnCodeUse" runat="server" />

				<input id="HdnIsChange" type="hidden" size="1" name="HdnIsChange" value="0" runat="server" />

				<asp:Button ID="BtnClose" Text="关  闭" CssClass="button-1" runat="server" />&nbsp;
				<asp:Button ID="BtnAdd" Text="新  增" CssClass="button-1" OnClick="BtnAdd_Click" runat="server" />
				<asp:Button ID="BtnUpd" Text="编  辑" CssClass="button-1" Enabled="false" OnClick="BtnUpd_Click" runat="server" />
				<asp:Button ID="BtnDel" Text="删  除" CssClass="button-1" Enabled="false" OnClick="BtnDel_Click" runat="server" />
				<input id="btnPrivilege" type="button" value="权  限" class="button-1" disabled="disabled"
					style="display: none;" onclick="allocUser();" />
			</td>
		</tr>
		<tr>
			<td valign="top">
				<div id="dvScheduleBox" class="gridBox">
					<asp:Table ID="TblCodeTree" CssClass="grid" GridLines="Both" CellSpacing="0" CellPadding="0" BorderColor="#E0E0E0" BorderStyle="None" BorderWidth="0px" Width="100%" runat="server"><asp:TableRow HorizontalAlign="Center" CssClass="dgheader" runat="server"><asp:TableCell Wrap="false" Text="编码名称" runat="server"></asp:TableCell><asp:TableCell Width="400px" ID="priv" Wrap="false" Text="权限人员" runat="server"></asp:TableCell><asp:TableCell Width="60px" Wrap="false" Text="是否默认" runat="server"></asp:TableCell><asp:TableCell Width="60px" Wrap="false" Text="是否固定" Visible="false" runat="server"></asp:TableCell><asp:TableCell Width="60px" Wrap="false" Text="编码ID" runat="server"></asp:TableCell></asp:TableRow></asp:Table>
				</div>
			</td>
		</tr>
	</table>
	<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
	<input type="hidden" id="hdnstate" runat="server" />

	<div id="divFramPerson" title="选择人员" style="display: none">
		<iframe id="IframePerson" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<asp:HiddenField ID="hfldSignCode" runat="server" />
	</form>
</body>
</html>
