<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CalendarAdd.aspx.cs" Inherits="oa_Calendar_CalendarAdd" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>电子日程</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$('#BtnSave')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
			$('#TxtHours').hide();
			$('#TxtMinutes').hide();
		});

		function Exppress(obj) {
			if (obj.value != "") {
				if (!(new RegExp(/^\d+$/).test(obj.value))) {
					alert('完成度应该是整数！');
					obj.focus();
					return;
				}
				else {
					if (parseInt(obj.value) > 23) {
						alert('完成度应该小于等于23！');
						obj.focus();
						return;
					}
				}
			}
			else {
				obj.value = 0;
			}
		}

		function Exppress2(obj) {
			if (obj.value != "") {
				if (!(new RegExp(/^\d+$/).test(obj.value))) {
					alert('完成度应该是整数！');
					obj.focus();
					return;
				}
				else {
					if (parseInt(obj.value) > 59) {
						alert('完成度应该小于等于59！');
						obj.focus();
						return;
					}
				}
			}
			else {
				obj.value = 0;
			}
		}
	</script>
</head>
<body class="body_clear">
	<form id="form1" action="CalendarAdd.aspx" runat="server">
	<div>
		<table class="table-normal" cellspacing="0" cellpadding="0" width="100%" border="1">
			<tr>
				<td class="td-title" align="center" colspan="2">
					日程安排
				</td>
			</tr>
			<tr>
				<td class="td-label" width="10%">
					添加日期:
				</td>
				<td width="23%">
					<asp:TextBox ID="TxtRecordDate" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="td-label">
					标题:
				</td>
				<td>
					<asp:TextBox ID="TxtTitle" Width="200px" MaxLength="100" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtTitle" ErrorMessage="标题不能为空" runat="server"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
				<td class="td-label">
					详细内容
				</td>
				<td>
					<asp:TextBox ID="TxtContent" TextMode="MultiLine" Rows="5" Width="200px" MaxLength="1000" AutoPostBack="false" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="td-label">
					添加人
				</td>
				<td>
					<asp:TextBox ID="TxtUserName" runat="server"></asp:TextBox>
					<asp:HiddenField ID="HdnUserCode" runat="server" />
				</td>
			</tr>
			<tr>
				<td class="td-label" style="height: 26px">
					<asp:CheckBox ID="CBIsRemind" Text="是否提醒" AutoPostBack="true" OnCheckedChanged="CBIsRemind_CheckedChanged" runat="server" />
				</td>
				<td style="height: 26px">
					<asp:CheckBox ID="CBIsSms" Text="短信" runat="server" />
					<asp:CheckBox ID="CBIsMessage" Text="待办消息" runat="server" />
					<asp:TextBox ID="TxtHours" Enabled="false" Width="0px" onblur="Exppress(this);" Text="0" runat="server"></asp:TextBox>
					<asp:TextBox ID="TxtMinutes" Enabled="false" Width="0px" onblur="Exppress2(this);" Text="0" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td align="center" colspan="2" class="td-submit" style="height: 26px">
					<asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
				</td>
			</tr>
		</table>
		<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
	</div>
	</form>
</body>
</html>
