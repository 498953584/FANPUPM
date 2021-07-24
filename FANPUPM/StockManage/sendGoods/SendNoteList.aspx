<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendNoteList.aspx.cs" Inherits="StockManage_sendGoods_SendNoteList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>发货通知单列表</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../script/Config2.js"></script>
	<script type="text/javascript" src="../script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/jwJson.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var purchasePlanTable = new JustWinTable('gvPurchaseplan');
			setButton2(purchasePlanTable, 'btnDel', 'btnEdit', 'btnLook', 'hfldPurchaseChecked');
			addEvent(document.getElementById('btnEdit'), 'click', rowEdit);
			addEvent(document.getElementById('btnLook'), 'click', rowQuery);
			addEvent(document.getElementById('btnAdd'), 'click', rowAdd);

			function rowAdd() {
				top.ui._addSendNote = window;
				var url = "/StockManage/sendGoods/AddSendNote.aspx?prjId=" + $('#hfldPrjId').val();
				toolbox_oncommand(url, "新增发货通知单");
			}
			function rowEdit() {
				top.ui._addSendNote = window;
				var url = "/StockManage/sendGoods/AddSendNote.aspx?id=" + $("#hfldPurchaseChecked").val();
				toolbox_oncommand(url, "编辑发货通知单");
			}
			function rowQuery() {
				var url = "/StockManage/sendGoods/ViewSendNote.aspx?t=1&ic=" + document.getElementById("hfldPurchaseChecked").value;
				viewopen(url, 820, 500);
			}
			replaceEmptyTable('empetyPurchaseplan', 'gvPurchaseplan');
			showTooltip('tooltip', 15);
		});

		function ClickRow(AuditState, code) {
			document.getElementById('hfldPurchaseChecked').value = code;
			document.getElementById('hdfState').value = AuditState;
			if (AuditState == "已验收") {
				document.getElementById("btnEdit").setAttribute('disabled', 'disabled');
				document.getElementById("btnDel").setAttribute('disabled', 'disabled');
				document.getElementById("btnLook").removeAttribute('disabled');
			}
			else if (AuditState == "未验收") {
				document.getElementById("btnEdit").removeAttribute('disabled');
				document.getElementById("btnDel").removeAttribute('disabled');
				document.getElementById("btnLook").removeAttribute('disabled');
			}

		}

		//选择人员
		function selectUser() {
			jw.selectOneUser({ nameinput: 'txtPeople', codeinput: 'hdnPeople' });
		}

		//按钮权限
		function setButton2(jwTable, btnDel, btnUpdate, btnQuery, hdChkId) {
			if (!jwTable.table) return;
			if (jwTable.table.firstChild.childNodes.length == 1) {
				//table中没有数据
				return;
			}
			jwTable.registClickTrListener(function () {
				if (hdChkId != '')
					document.getElementById(hdChkId).value = this.id;
				if (this.guid) {
					document.getElementById(btnQuery).guid = this.guid;
				}
				selSingle(this.id);
			});

			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				if (checkedChk.length == 0) {
					disabledButton();
				}
				else if (checkedChk.length == 1) {
					if (hdChkId != '')
						document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);
					selSingle($('#' + hdChkId).val());
				}
				else {
					if (hdChkId != '')
						document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);
					selMultiple($('#' + hdChkId).val());
				}
			});
			jwTable.registClickAllCHKLitener(function () {
				//                alert('CkAllClick');
				if (this.checked) {
					if (hdChkId != '')
						document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
					var checkedChks = jwTable.getAllChk();
					if (checkedChks.length == 1)
						selSingle($('#' + hdChkId).val());
					else
						selMultiple($('#' + hdChkId).val());
				}
				else {
					if (hdChkId != '')
						document.getElementById(hdChkId).value = '';
					disabledButton();
				}
			});
		}
		//单选
		function selSingle(id) {
			var state = Trim($('#' + id).get(0).cells[5].innerText);
			if (state == "已验收") {
				document.getElementById("btnEdit").setAttribute('disabled', 'disabled');
				document.getElementById("btnDel").setAttribute('disabled', 'disabled');
				document.getElementById("btnLook").removeAttribute('disabled');
			}
			else if (state == "未验收") {
				document.getElementById("btnEdit").removeAttribute('disabled');
				document.getElementById("btnDel").removeAttribute('disabled');
				document.getElementById("btnLook").removeAttribute('disabled');
			}
		}
		//多选
		function selMultiple(id) {
			var reExp = /\"/g;
			id = id.replace(reExp, '');
			var ids = id.substring(1, id.length - 1).split(',');
			var state = Trim($('#' + ids[0]).get(0).cells[5].innerText);
			var isSame = true;
			for (index in ids) {
				var itemId = ids[index];
				var itemState = Trim($('#' + itemId).get(0).cells[5].innerText);
				if (itemState != state) {
					isSame = false;
					break;
				}
			}
			disabledButton();
			if (state == "未验收") {
				disabledButton();
				$('#btnDel').removeAttr('disabled');
			}
		}
		//禁用查看、编辑、删除按钮
		function disabledButton() {
			document.getElementById("btnEdit").setAttribute('disabled', 'disabled');
			document.getElementById("btnDel").setAttribute('disabled', 'disabled');
			document.getElementById("btnLook").setAttribute('disabled', 'disabled');
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
							<asp:TextBox ID="txtsnCode" Height="15px" Width="120px" runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr style="height: 25px;">
						<td class="descTd">
							录 &nbsp; 入 人
						</td>
						<td>
							<input type="text" style="width: 120px;" id="txtPeople" class="select_input" imgclick="selectUser" runat="server" />

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
							<input type="button" id="btnAdd" value="新增" />
							<input type="button" id="btnEdit" disabled="disabled" value="编辑" />
							<asp:Button ID="btnDel" Text="删  除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗??')" OnClick="btnDel_Click" runat="server" />
							<input type="button" id="btnLook" disabled="disabled" value="查看" />
						</td>
					</tr>
					<tr>
						<td>
							<div class="">
								<asp:GridView ID="gvPurchaseplan" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvPurchaseplan_PageIndexChanging" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="snId,snCode,prjCode" runat="server">
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
												<span class="al" onclick="viewopen('ViewSendNote.aspx?ic=<%# Eval("snId") %>',820,500)">
													<%# Eval("snCode") %>
												</span>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入人"><ItemTemplate>
												<asp:Label ID="labUser" Text='<%# Convert.ToString(Eval("snAddUser")) %>' runat="server"></asp:Label>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入时间"><ItemTemplate>
												<%# Common2.GetTime(Eval("snAddTime").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="收货状态"><ItemTemplate>
												<asp:Label ID="labState" Text='<%# Convert.ToString((DataBinder.Eval(Container.DataItem, "sendState").ToString() == "0") ? "未验收" : "已验收") %>' runat="server"></asp:Label>
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
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	<asp:HiddenField ID="hdfState" runat="server" />
	</form>
</body>
</html>
