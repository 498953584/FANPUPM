<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TreasuryEdit.aspx.cs" Inherits="StockManage_Treasury_TreasuryEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>仓库管理</title>
	<script type="text/javascript" src="../script/Config2.js"></script>
	<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/jwJson.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript">

		$(document).ready(function () {
			var action = getRequestParam('Action');
			if (action == 'Query') {
				setAllInputDisabled();
				$('#btnSave').hide();
			}
			document.getElementById('btnSave').onclick = btnSaveClick;
			document.getElementById('btnCancel').onclick = function () {
				winclose('TreasuryEdit', 'Treasury.aspx', false)
			}
		});

		function btnSaveClick() {
			if (!document.getElementById('tbtname').value) {
				top.ui.alert("仓库名称不能为空");
				return false;
			}
			if (document.getElementById('trTflag') != null) {
				if (!getRadioButtonListValue('rblTflag')) {
					top.ui.alert('仓库标识不能为空');
					return false;
				}
			}

			if (!document.getElementById('tbtaddress').value) {
				top.ui.alert("仓库地址不能为空");
				return false;
			}
		}

		//选择项目
		function openProjPicker() {
			var action = getRequestParam('Action');
			if (action != 'Query') {
				jw.selectOneProject({ idinput: 'hdnProjectCode', nameinput: 'txtProject' });
			}
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divHeader2">
		<table class="tableHeader">
			<tr>
				<td>
					<asp:Label ID="lblTitle" Font-Bold="true" Text="仓库设置" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<div class="divContent2">
		<table class="tableContent2" cellpadding="5px" cellspacing="0">
			<tr>
				<td class="word">
					仓库名称
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="tbtname" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr id="trTflag" runat="server"><td class="word" runat="server">
					仓库标识
				</td><td class="txt" runat="server">
					<asp:RadioButtonList ID="rblTflag" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblTflag_SelectedIndexChanged" runat="server"><asp:ListItem Value="0" Text="分库" /><asp:ListItem Value="1" Text="总库" /></asp:RadioButtonList>
				</td></tr>
			<tr id="tishi" visible="false" runat="server"><td class="word" runat="server">
				</td><td class="txt" runat="server">
					<asp:Label ID="lblTishi" Style="color: Red" runat="server"></asp:Label>
				</td></tr>
			<tr id="promptzf" runat="server"><td class="word" runat="server">
				</td><td class="txt" runat="server">
					<span style="color: Red;">【注：若仓库为总库，则不能关联项目】</span>
				</td></tr>
			<tr id="tr2" runat="server"><td class="word" runat="server">
					作为对比库
				</td><td class="txt mustInput" runat="server">
					<asp:CheckBox ID="chkIsContrast" AutoPostBack="true" OnCheckedChanged="chkIsContrast_CheckedChanged" runat="server" /><span style="color: Red;">【注：此项仅应用于平行模式】</span>
				</td></tr>
			<tr id="tishi1" visible="false" runat="server"><td class="word" runat="server">
				</td><td class="txt" runat="server">
					<asp:Label ID="lblTishi1" Style="color: Red" runat="server"></asp:Label>
				</td></tr>
			<tr>
				<td class="word">
					仓库地址
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="tbtaddress" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					关联项目
				</td>
				<td class="txt" style="padding-right: 3px">
					<input type="text" id="txtProject" readonly="readonly" class="select_input" imgclick="openProjPicker" runat="server" />

					<asp:HiddenField ID="hdnProjectCode" runat="server" />
				</td>
			</tr>
			<tr>
				<td class="word">
					仓库说明
				</td>
				<td class="txt">
					<asp:TextBox ID="tbtexplain" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
				</td>
			</tr>
		</table>
	</div>
	<div class="divFooter2">
		<table class="tableFooter2">
			<tr>
				<td>
					<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
					<input type="button" id="btnCancel" value="取消" />
				</td>
			</tr>
		</table>
		<div id="divFram" title="" style="display: none">
			<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
		</div>
		<div id="divFramPrj" title="" style="display: none">
			<iframe id="ifFramPrj" frameborder="0" width="100%" height="100%" src=""></iframe>
		</div>
	</div>
	</form>
</body>
</html>
