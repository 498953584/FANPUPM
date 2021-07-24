<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NodeEdit.aspx.cs" Inherits="NodeEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>节点编辑</title>
	<base target="_self" />
	<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$("#img2").tooltip({ showURL: false });
			$("#img3").tooltip({ showURL: false });
			$("#img4").tooltip({ showURL: false });
			$('#txtDep').attr('disabled', true); 	// 部门只读
		});

		function SelDept() {
			var _callback = top.ui.callback;
			var url = "/oa/common/selDept.aspx?yhdm=00000000&dept=0";
			top.ui.openWin({ title: '选择部门', url: url, width: 400, height: 500, winNo: 2 });
			top.ui.callback = function (user) {
				$('#hfldDep').val(user.code);
				$('#txtDep').val(user.name);
				top.ui.callback = _callback;
			}
		}

		function check() {
			if (document.getElementById('CkbIsSelReceiver').checked) {
				document.getElementById('txtOperater').value = '0';
				tr1.style.display = 'none';
				tr2.style.display = 'none';
				tr3.style.display = '';
			}
			else {
				document.getElementById('txtOperater').value = '';
				tr1.style.display = '';
				tr2.style.display = '';
				tr3.style.display = 'none';
			}
		}

		function clear() {
			document.getElementById('hdnOperater').value = "";
			document.getElementById('txtOperater').value = "";
		}

		function conditionSet(nodeid) {
			var url = "ConditionSet.aspx?nodeid=" + nodeid;
			return window.showModalDialog(url, window, "dialogHeight:600px;dialogWidth:456px;center:1;help:0;status:0;");
		}

		function pickOperater(type) {
			var url = "";
			var title = '选择人员';
			if (document.getElementById('rbSingle').checked) {
				// 单人
				url = "/EPC/Workflow/SelectUser.aspx";
			}
			else if (document.getElementById('rbMilti').checked) {
				// 多人
				url = "/EPC/Workflow/SelectAllUser.aspx?UserCode=" + document.getElementById('hdnOperater').value + "&UserName=" + encodeURI(document.getElementById('txtOperater').value);
			}
			else {
				url = "/EPC/Workflow/PickRole.aspx?tp=" + type;
				title = '选择群组';
			}
			top.ui.openWin({ title: title, url: url, winNo: 2 });
			var _callback = top.ui.callback;
			top.ui.callback = function (user) {
				document.getElementById('hdnOperater').value = user.code;
				document.getElementById('txtOperater').value = user.name;
				top.ui.callback = _callback;
			}
		}

		function SetUserPwd() {
			var usercode = document.getElementById("hdnOperater").value;
			window.showModalDialog("SetUserAuditPwd.aspx?hid=" + usercode, window, "dialogHeight:300px;dialogWidth:500px;center:1;help:0;status:0;");
		}
	</script>
</head>
<body scroll="no">
	<form id="Form1" method="post" runat="server">
	<table class="tableAudit" id="TableVersion" cellspacing="0" cellpadding="0" width="100%"
		border="1">
		<tr>
			<td class="td-label">
				前置节点
			</td>
			<td id="tdFront" runat="server">
				<asp:TextBox ID="txtFrontNode" Enabled="false" Width="160px" runat="server"></asp:TextBox>
				<input id="hdnFrontNode" style="width: 10px" type="hidden" name="hdnFrontNode" runat="server" />

				<input id="hdnPos" style="width: 10px" type="hidden" name="hdnPos" runat="server" />

			</td>
		</tr>
		<tr>
			<td class="td-label">
				节点名称
			</td>
			<td>
				<asp:TextBox ID="txtNodeName" Width="160px" TabIndex="1" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNodeName" ErrorMessage="节点名称不能为空！" runat="server"></asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr id="tr0" runat="server"><td class="td-label" width="25%" runat="server">
				流转过程中设置
			</td><td runat="server">
				<asp:CheckBox ID="CkbIsSelReceiver" onclick="javascript:check();" Text="提交审核时选择审核人" runat="server" />
				<img id="img2" alt="" src="../../images/help.jpg" title="相关单据使用该流程提交时，指定审核人。或者流程审核时指定审核人"
					style="vertical-align: middle;" />
			</td></tr>
		<tr id="tr1" style="display: none">
			<td class="td-label" width="25%">
				审核人类型
			</td>
			<td>
				<asp:RadioButton ID="rbSingle" Checked="true" Text="单人" GroupName="opType" TabIndex="2" runat="server" />
				<asp:RadioButton ID="rbMilti" Text="多人" GroupName="opType" TabIndex="2" runat="server" />
				<asp:RadioButton ID="rbGroup" Text="群组" GroupName="opType" TabIndex="2" runat="server" />
				<asp:RadioButton ID="rbItem" Text="项目相关" GroupName="opType" TabIndex="2" runat="server" />
				<asp:RadioButton ID="rbDept" Text="部门相关" GroupName="opType" TabIndex="2" runat="server" />
			</td>
		</tr>
		<tr id="tr2">
			<td class="td-label" width="25%">
				默认审核人
			</td>
			<td>
				<asp:TextBox ID="txtOperater" Width="160px" TabIndex="4" runat="server"></asp:TextBox>
				<img id="imgPick" src="../../images/pickRation.gif" align="absmiddle" width="18" height="18" style="cursor: hand" runat="server" />

				<input id="hdnOperater" style="width: 10px" type="hidden" name="hdnOperater" runat="server" />

				<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtOperater" ErrorMessage="审核人不能为空！" runat="server"></asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr id="tr3" style="display: none;">
			<td class="td-label" width="25%">
				选择部门
			</td>
			<td>
				<span class="spanSelect" style="width: 160px;">
					<asp:TextBox ID="txtDep" Style="width: 135px; height: 15px; border: none;
						line-height: 16px; margin: 1px 0 1px 2px" runat="server"></asp:TextBox>
					<img src="../../images/icon.bmp" style="float: right;" alt="选择" id="img6" onclick="SelDept();" runat="server" />

				</span>
				<input id="hfldDep" type="hidden" style="width: 1px" runat="server" />

			</td>
		</tr>
		<tr>
			<td class="td-label" width="25%">
				全部通过才通过
			</td>
			<td>
				<asp:RadioButton ID="rbIsAllPass1" Text="是" GroupName="gpPass" Checked="true" TabIndex="7" runat="server" />
				<asp:RadioButton ID="rbIsAllPass2" Text="否" GroupName="gpPass" TabIndex="8" runat="server" />
				<img id="img3" alt="" src="../../images/help.jpg" align="middle" title="该选项主要针对多人审核时，如果选择'否'，则多人审核中如果有一个人审核通过，流程就好走到下一个流程节点。如果选择是，只有多人审核中所有审核人通过后流程才会走到下一个流程节点" />
			</td>
		</tr>
		<tr>
			<td class="td-label" width="25%">
				失效时限(小时)
			</td>
			<td>
				<asp:TextBox ID="txtDuring" Width="160px" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtDuring" ErrorMessage="失效时限不能为空！" runat="server"></asp:RequiredFieldValidator>
				<asp:CompareValidator ID="comparevalidator1" ControlToValidate="txtDuring" ErrorMessage="输入类型错误！" Operator="DataTypeCheck" Type="Integer" Display="None" runat="server"></asp:CompareValidator>
			</td>
		</tr>
		<tr>
			<td class="td-label" width="25%">
				超时处理
			</td>
			<td>
				<asp:RadioButtonList ID="RbDueMode" RepeatDirection="Horizontal" runat="server"><asp:ListItem Value="1" Text="通过" /><asp:ListItem Value="0" Text="驳回" /><asp:ListItem Value="3" Text="重报" /><asp:ListItem Value="2" Selected="true" Text="红字提醒(不处理)" /></asp:RadioButtonList>
			</td>
		</tr>
		<tr>
			<td class="td-label">
				审核要点
			</td>
			<td>
				<asp:TextBox ID="txtAuditMain" MaxLength="250" Rows="3" TextMode="MultiLine" Width="304px" runat="server"></asp:TextBox>
				<img id="img4" alt="" src="../../images/help.jpg" style="vertical-align: middle"
					title="流程审核时，告诉审核人审核时需要注意的事项" />
			</td>
		</tr>
		<tr style="display: none">
			<td class="td-label" width="25%">
				条件描述
			</td>
			<td>
				<asp:TextBox ID="txtRemark" MaxLength="100" Width="304px" TabIndex="10" ValidationGroup="all" runat="server"></asp:TextBox><asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" ShowMessageBox="true" runat="server"></asp:ValidationSummary>
			</td>
		</tr>
		<tr>
			<td class="td-label" width="25%">
				审核验证
			</td>
			<td>
				<asp:CheckBox ID="CkbIsSec" Text="需要二次验证" runat="server" />
				<img id="img5" alt="" src="../../images/help.jpg" style="vertical-align: middle"
					title='1、“二次验证密码”功能：用于审核时的针对性验证，可以有效防止离开坐位时，或者遭到恶意入侵时的审核权的安全；<br />2、需要注意的是，只有在“流程安全设置”中，节点的审核人设置了审核密码后，本功能才能生效。<br/>3、默认所有人的审核密码为easy' />
				&nbsp;&nbsp;&nbsp; <span id="span_a" style="display: none" runat="server"><a href="#"
					onclick="SetUserPwd()">设置</a></span>
			</td>
		</tr>
		<tr>
			<td class="td-submit" colspan="4">
				<asp:Button ID="BtnCondition" Text="条件设置" Width="0px" Style="display: none" runat="server" />&nbsp;
				<asp:Button ID="BtnAdd" Text="保  存" runat="server" />&nbsp;
				<input id="BtnClose" onclick="top.ui.closeWin();" type="button" value="关  闭" name="BtnClose" />
			</td>
		</tr>
	</table>
	<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
	</form>
</body>
</html>
