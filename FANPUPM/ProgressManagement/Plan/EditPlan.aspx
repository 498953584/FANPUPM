<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditPlan.aspx.cs" Inherits="ProgressManagement_Plan_EditPlan" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>编制进度计划</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/Script/DecimalInput.js"></script>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<script type="text/javascript">
		$(function () {
			$("#btnSave")[0].onclick = function () {
				if (!$('#form1').form('validate')) {
					return false;
				}
			}
		});
		function isValideData() {
			if (!$('#btnSave').get(0).retVal)
				return false;
			else
				return true;
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<table cellpadding="5" style="text-align: center; width: 90%; height: 90%;">
			<tr>
				<td>
					计划名称
				</td>
				<td class="elemTd mustInput" style="text-align: left;">
					<asp:TextBox Width="70%" ID="txtProgressName" CssClass="easyui-validatebox" data-options="required:true,validType:'validChars[50]'" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td>
					版本号
				</td>
				<td class="elemTd mustInput" style="text-align: left;">
					<asp:TextBox Width="70%" ID="txtVersionCode" CssClass="easyui-validatebox" data-options="required:true,validType:'validChars[50]'" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td>
				</td>
				<td style="text-align: left;">
					<asp:CheckBox ID="chkIsMainPlan" Text="项目主计划" ToolTip="进度分析的计划" runat="server" /><span
						style="padding-left: 10px; color: Red;">(一个项目只允许设置一个主计划)</span>
				</td>
			</tr>
			<tr>
				<td>
					备注
				</td>
				<td>
					<asp:TextBox ID="txtArea" TextMode="MultiLine" Height="80px" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td colspan="2" style="text-align: right; vertical-align: bottom; height: 80px;">
					<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
					<input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
