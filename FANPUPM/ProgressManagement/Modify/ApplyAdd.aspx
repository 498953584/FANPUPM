<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyAdd.aspx.cs" Inherits="ProgressManagement_Modify_ApplyAdd" %>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>新增/编辑调整申请</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.autocomplete/jquery.autocomplete.css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/Script/jquery.autocomplete/jquery.autocomplete.min.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$('#btnSave')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
		});

		//加载甘特图页面
		function loadGantt(progressVerId) {
			var prjId = getRequestParam('prjId');
			var prjYear = getRequestParam('year');
			var url = "../../ProgressManagement/Gantt/PlanDetail.htm?year=" + prjYear + "&prjId=" + prjId + "&id=" + progressVerId;
			$('#ifPlusGantt').attr("src", url);
		}

		//选择人员
		function selectUser() {
			jw.selectOneUser({ codeinput: 'hfldUserCode', nameinput: 'txtModifyUser' });
		}
	</script>
</head>
<body style="overflow: hidden;">
	<form id="form1" runat="server">
	<div id="divApply" runat="server">
		<div style="text-align: center; width: 100%; height: auto;">
			<table style="margin: 0 auto; width: 75%; text-align: left;" cellpadding="10" cellspacing="10"
				class="tableContent2">
				<tr>
					<td class="word">
						原计划名称
					</td>
					<td>
						<asp:TextBox ID="txtOldProgressName" ReadOnly="true" runat="server"></asp:TextBox>
					</td>
					<td class="word">
						原计划版本号
					</td>
					<td>
						<asp:TextBox ID="txtOldVersionCode" ReadOnly="true" runat="server"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td class="word">
						调整计划名称
					</td>
					<td class="elemTd mustInput">
						<asp:TextBox ID="txtModifyProgressName" CssClass="easyui-validatebox" data-options="required:true, validType:'validChars[200]'" runat="server"></asp:TextBox>
					</td>
					<td class="word">
						调整计划版本号
					</td>
					<td class="elemTd mustInput">
						<asp:TextBox ID="txtModifyVersionCode" CssClass="easyui-validatebox" data-options="required:true, validType:'validChars[50]'" runat="server"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td class="word">
						计划调整日期
					</td>
					<td>
						<asp:TextBox ID="txtModifyDate" ReadOnly="true" runat="server"></asp:TextBox>
					</td>
					<td class="word">
						调整申请人
					</td>
					<td>
						<input id="hfldUserCode" type="hidden" style="width: 1px" runat="server" />

						<input type="text" readonly="readonly" class="select_input {required:true, messages:{required:'请选择项目'}}" id="txtModifyUser" imgclick="selectUser" runat="server" />

						<input id="hdnProjectCode" type="hidden" name="hdnProjectCode" runat="server" />

					</td>
				</tr>
				<tr>
					<td class="word">
						调整原因
					</td>
					<td colspan="3">
						<asp:TextBox ID="txtModifyNote" TextMode="MultiLine" Height="60px" runat="server"></asp:TextBox>
					</td>
				</tr>
			</table>
		</div>
	</div>
	<div id="divGantt" visible="false" runat="server">
		<iframe id="ifPlusGantt" style="width: 100%; height: 100%;" frameborder="0" scrolling="yes">
		</iframe>
	</div>
	<div id="divFooter" style="width: 100%; text-align: right; padding-top: 3px;
		padding-right: 3px; position: absolute; bottom: 0px;" class="divFooter" runat="server">
		<asp:Button ID="btnSave" ToolTip="保存调整计划" Text="保存" OnClick="btnSave_Click" runat="server" />
	</div>
	<div id="divFramPerson" title="选择人员" style="display: none">
		<iframe id="IframePerson" frameborder="0" width="100%" height="100%" src="" scrolling="auto">
		</iframe>
	</div>
	</form>
</body>
</html>
