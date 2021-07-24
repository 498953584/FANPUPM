<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StartWf.aspx.cs" Inherits="StartWf" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>流程模板选择</title>
	<base target="_self" />
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.external/jquery.bgiframe-2.1.2.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
	<script type="text/javascript">
		function pickPerson() {
			var url = "";
			var auditorType = document.getElementById('Type').value; // 节点类型
			var nextDepCode = $('#hfldNextAuditDepCode').val(); 		// 第一个审核人的部门

			// 单人
			if (auditorType == "1") {
				// 如果部门编码非空
				if (nextDepCode.length > 0)
					url = "/Common/SelectOneUser.aspx?depCode=" + nextDepCode;
				else
					url = "/EPC/WorkFLow/SelectUser.aspx";
			}

			// 多人
			if (auditorType == "2") {
				if (nextDepCode.length > 0)
					url = "/Common/SelectOneUser.aspx?type=multi&depCode=" + nextDepCode;
				else
					url = "/EPC/WorkFLow/SelectAllUser.aspx";
			}

			// 群组，项目相关，部门相关
			if (auditorType == "3" || auditorType == "4" || auditorType == "5") {
				url = "/EPC/WorkFLow/PickRole.aspx?tp=" + auditorType;
			}

			top.ui.openWin({ title: '选择人员', url: url, winNo: 2 });
			top.ui.callback = function (user) {
				document.getElementById('hdnReceiver').value = user.code;
				document.getElementById('txtReceiver').value = user.name;
			}
		}

		function selectTemplate() {
			document.getElementById('hdnTemplateID').value = document.getElementById("ddltTemplate").value;
		}

		function clostDia() {
			top.ui.closeWin();
		}           
	</script>
</head>
<body scroll="no">
	<form id="form1" method="post" runat="server">
	<div>
		<table cellspacing="0" cellpadding="5" width="100%" style="height: 100%" border="1">
			<tr>
				<td class="divHeader" style="width: 100%" colspan="2" height="20">
					流程启动选择
				</td>
			</tr>
			<tr>
				<td style="text-align: right; width: 85px; white-space: nowrap;">
					选择流程模板
				</td>
				<td>
					<asp:DropDownList ID="ddltTemplate" Width="370px" AutoPostBack="true" OnSelectedIndexChanged="ddltTemplate_SelectedIndexChanged" runat="server"><asp:ListItem Value="-1" Text="---请选择流程模板---" /></asp:DropDownList>
					<input id="hdnTemplateID" style="width: 10px" type="hidden" name="hdnTemplateID" runat="server" />

					<input id="hdnNodeId" style="width: 10px" type="hidden" name="hdnNodeId" runat="server" />

					<input id="hdnOrderNumber" style="width: 10px" type="hidden" name="hdnOrderNumber" runat="server" />

				</td>
			</tr>
			<tr>
				<td style="text-align: right">
					备注
				</td>
				<td>
					<asp:Label ID="lbltishi" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td style="text-align: right; white-space: nowrap;">
					选择接收人
				</td>
				<td>
					<asp:TextBox ID="txtReceiver" CssClass="td-input" Width="270px" runat="server"></asp:TextBox>&nbsp;
					<img src="../../images/contact1.gif" id="IBPick" runat="server" />

					<input id="hdnReceiver" style="width: 5px" type="hidden" name="hdnReceiver" runat="server" />

					<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtReceiver" ErrorMessage="请选择接收人！" runat="server"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr id="Message" visible="false" runat="server"><td runat="server">
				</td><td runat="server">
					<asp:Label ID="lblMessage" Style="color: Red" runat="server"></asp:Label>
				</td></tr>
			<tr>
				<td colspan="2" height="20" style="text-align: right; padding-right: 20px">
					<asp:Button ID="BtnAdd" Text="确  认" OnClick="BtnAdd_Click" runat="server" />&nbsp;
					<input type="button" value="关闭" onclick="clostDia();" />
				</td>
			</tr>
		</table>
		<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
		<table cellspacing="0" cellpadding="5" width="100%" style="height: 100%" border="1">
			<tr>
				<td class="divHeader" height="20" width="100%">
					工作流程图显示
				</td>
			</tr>
			<tr>
				<td valign="top">
					<div style="width: 490px; overflow: auto;">
						<table id="tbFlowChart" cellpadding="0" cellspacing="0" style="white-space: nowrap;" runat="server"></table>
					</div>
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="Type" runat="server" />
	
	<asp:HiddenField ID="hfldNextAuditDepCode" runat="server" />
	</form>
</body>
</html>
