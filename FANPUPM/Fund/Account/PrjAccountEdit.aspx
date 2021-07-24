<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjAccountEdit.aspx.cs" Inherits="PrjAccountEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<script language="javascript" type="text/javascript">
		$(function () {
			// 添加验证
			$("#btnSave")[0].onclick = function () {
				if (!$('#form1').form('validate')) {
					return false;

				}
				else {
					jw.preventSubmit2('btnSave');
				}
			}
			$("td").attr("style", "white-space:nowrap;");
			if (request("Action") == "query") {
				$("#FileLink1_But_UpFile").attr("disabled", "disabled");
				$("#FileLink1_Btn_Upload").attr("disabled", "disabled");
				$("#FileLink1_txtFile").attr("disabled", "disabled");
			}
		});

		// 取消
		function btnCancel_onclick() {
			top.ui.closeTab();
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
		function CheckInputIsFloat(keyCode, e) {
			if ((keyCode > 95 && keyCode < 106) || (keyCode > 47 && keyCode < 58) || keyCode == 8 || keyCode == 46 || keyCode == 37 || keyCode == 39 || keyCode == 9 || keyCode == 13) {
			}
			else if (keyCode == 110 || keyCode == 190) {
				if (e.value == "" || e.value.indexOf(".") != -1) {
					return false;
				}
			}
			else {
				return false;
			}
		}
		// 可以输入负数的验证
		function CheckInputIsFloat1(keyCode, e) {
			if ((keyCode > 95 && keyCode < 106) || (keyCode > 47 && keyCode < 58) || keyCode == 8 || keyCode == 46 || keyCode == 37 || keyCode == 39 || keyCode == 9 || keyCode == 13 || keyCode == 189 || keyCode == 109) {
			}
			else if (keyCode == 110 || keyCode == 190) {
				if (e.value == "" || e.value.indexOf(".") != -1) {
					return false;
				}
			}
			else {
				return false;
			}
		}
		// 选择项目
		function openProjPicker() {
			var url = '/Common/DivSelectProject3.aspx';
			top.ui.callback = function (o) {
				// 数据库中存储的prjId使用单引号括起来
				var idArr = eval(o.id);
				var idArr2 = new Array();
				for (var i = 0; i < idArr.length; i++) {
					idArr2.push("'" + idArr[i] + "'");
				}
				var idCsv = jw.arrayToCsv(idArr2);
				var nameCsv = jw.arrayToCsv(eval(o.name));
				$('#hdnProjectCode').val(idCsv);
				$('#txtPrjName').val(nameCsv);
			}
			top.ui.openWin({ title: '选择项目', url: url, width: 700 });
		}

	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divHeader2">
		<table class="tableHeader2">
			<tr>
				<td>
					<span id="lblTitle" style="font-weight: bold;">项目账户</span>
				</td>
			</tr>
		</table>
	</div>
	<div id="divContent2" class="divContent2">
		<table id="tableContent2" class="tableContent2" cellpadding="5px" cellspacing="0">
			<tr>
				<td class="word">
					账户编号
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtaccountNum" required="required" CssClass="easyui-validatebox" data-options="required:true, validType:'validChars[25]'" Style="height: 15px;
						width: 100%;" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					账户名称
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtacountName" required="required" CssClass="easyui-validatebox" data-options="required:true, validType:'validChars[50]'" Style="height: 15px;
						width: 100%;" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					启动资金
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtinitialFund" CssClass="easyui-validatebox easyui-numberbox" required="required" data-options="min:0,max:9999999999,precision:3,groupSeparator:','" runat="server"></asp:TextBox>
				</td>
				<td class="">
				</td>
				<td class="">
				</td>
			</tr>
			<tr>
				<td class="word">
					项目
				</td>
				<td colspan="3" class="txt mustInput">
					<asp:TextBox ID="txtPrjName" ReadOnly="true" required="required" CssClass="easyui-validatebox" data-options="required:true" Style="height: 60px; width: 100%;" runat="server"></asp:TextBox>
					<br />
					<input type="hidden" id="hdnProjectCode" runat="server" />

					<input id="btnSelPrj" style="width: 70px;" type="button" value="选择项目" onclick="openProjPicker()" runat="server" />

				</td>
			</tr>
			<tr>
				<td class="word">
					附件
				</td>
				<td class="txt">
					<MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="附件" runat="server" />
				</td>
				<td>
				</td>
				<td>
				</td>
			</tr>
			<tr>
				<td class="word">
					备注
				</td>
				<td colspan="3" class="txt">
					<textarea id="txtRemark" cols="20" name="S1" rows="2" style="width: 100%" runat="server"></textarea>
				</td>
			</tr>
		</table>
	</div>
	<div id="divFooter" class="divFooter2">
		<table class="tableFooter2">
			<tr>
				<td>
					<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
					<input type="button" id="btnCancel" value="取消" onclick="return btnCancel_onclick()" />
				</td>
			</tr>
		</table>
	</div>
	
	
	<input type="hidden" id="hdnUser" runat="server" />

	<input type="hidden" id="hdnAccount" runat="server" />

	<div style="display: none;">
		<asp:Button ID="btnSel" Text="Button" OnClick="btnSel_Click" runat="server" />
	</div>
	</form>
</body>
</html>
