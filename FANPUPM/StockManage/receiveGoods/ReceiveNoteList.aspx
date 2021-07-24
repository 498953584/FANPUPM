<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReceiveNoteList.aspx.cs" Inherits="StockManage_receiveGoods_ReceiveNoteList" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>发货通知单列表</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../script/Config2.js"></script>
	<script type="text/javascript" src="../script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/jwJson.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$('#btn_Search')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
			var purchasePlanTable = new JustWinTable('gvPurchaseplan');
			addEvent(document.getElementById('btnAdd'), 'click', rowAdd);
			function rowAdd() {
				top.ui._addReceiveNote = window;
				var url = "/StockManage/receiveGoods/AddReceiveNote.aspx?type=add&id=" + document.getElementById("hfldPurchaseChecked").value;
				toolbox_oncommand(url, "收货验收");
			}
			replaceEmptyTable('empetyPurchaseplan', 'gvPurchaseplan');
			showTooltip('tooltip', 15);
		});

		function ClickRow(FlowGuid, AuditState) {
			document.getElementById('hfldPurchaseChecked').value = FlowGuid;
			if (AuditState == "1") {
				document.getElementById("btnAdd").setAttribute('disabled', 'disabled');
			}
			else if (AuditState == "0") {
				document.getElementById("btnAdd").removeAttribute('disabled');
			}

		}


		//选择人员返回值
		function selectUser(id, name) {
			jw.selectOneUser({ nameinput: 'txtPeople', codeinput: 'hdnPeople' });
		}
  
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		<tr style="height: 20px;">
			<td class="divHeader">
				<asp:Label ID="lblProject" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td style="width: 100%; vertical-align: top;">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							起始时间
						</td>
						<td>
							<asp:TextBox ID="txtStartTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							结束时间
						</td>
						<td>
							<asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							发货通知单号
						</td>
						<td>
							<asp:TextBox ID="txtsnCode" Height="15px" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr style="height: 25px;">
						<td class="descTd">
							录 &nbsp; 入 人
						</td>
						<td>
							<input type="text" style="width: 120px;" id="txtPeople" class="easyui-validatebox select_input" data-options="validType:'validQueryChars[50]'" imgclick="selectUser" runat="server" />

							<asp:HiddenField ID="hdnPeople" runat="server" />
						</td>
						<td colspan="6">
							<asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td style="vertical-align: top;">
				<table class="tab" style="vertical-align: top;">
					<tr>
						<td class="divFooter" style="text-align: left">
							<input type="button" id="btnAdd" value="收货验收" style="width: 80px" disabled="disabled" />
						</td>
					</tr>
					<tr>
						<td>
							<div class="">
								<asp:GridView ID="gvPurchaseplan" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvPurchaseplan_PageIndexChanging" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="snId,snCode,prjCode,sendState" runat="server">
<EmptyDataTemplate>
										<table id="empetyPurchaseplan" class="rowa" style="width: 100%; text-align: center;">
											<tr class="header">
												<th scope="col" style="width: 20px;">
													<asp:CheckBox ID="cbAllBox" runat="server" />
												</th>
												<th scope="col" style="width: 25px;">
													序号
												</th>
												<th scope="col">
													发货通知单
												</th>
												<th scope="col">
													录入人
												</th>
												<th scope="col">
													录入时间
												</th>
												<th scope="col">
													说明
												</th>
											</tr>
										</table>
									</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
												<asp:CheckBox ID="cbAllBox" runat="server" />
											</HeaderTemplate><ItemTemplate>
												<asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("snId")) %>' runat="server" />
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="发货通知单号"><ItemTemplate>
												<%# Eval("snCode") %>
												</span>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入人"><ItemTemplate>
												<asp:Label ID="labUser" Text='<%# Convert.ToString(Eval("snAddUser")) %>' runat="server"></asp:Label>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入时间"><ItemTemplate>
												<%# Common2.GetTime(Eval("snAddTime").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="收货状态"><ItemTemplate>
												<%# (DataBinder.Eval(Container.DataItem, "sendState").ToString() == "0") ? "未验收" : "已验收" %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="说明"><ItemTemplate>
												<span class="tooltip" title='<%# Eval("remark") %>'>
													<%# StringUtility.GetStr(Convert.ToString(Eval("remark"))) %>
												</span>
											</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hdfState" runat="server" />
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	</form>
</body>
</html>
