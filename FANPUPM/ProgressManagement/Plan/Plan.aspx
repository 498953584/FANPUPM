<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Plan.aspx.cs" Inherits="ProgressManagement_Plan_Planl" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>计划编制</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.autocomplete/jquery.autocomplete.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/Script/jquery.autocomplete/jquery.autocomplete.min.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript">
		var flowStateIndex = 5;
		$(document).ready(function () {
			var jwTable = new JustWinTable('gvwPlans');
			setWidthHeight();
			showTooltip("tooltip", 25);
			setButton(jwTable, 'hfldChecked');
			//			$('#btnSave')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
			setWfButtonState2(jwTable, 'WF_1');
			replaceEmptyTable('tblEmpty', 'gvwPlans');
		});

		function setWidthHeight() {
			$('#divBudget').height($(this).height());
			$('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 7);
			$('#div_project').height($(this).height() - 20);
		}

		// 新增|编辑 进度计划
		function editPlan(type) {
			var title = '新增';
			if (type == 'edit')
				title = '编辑';

			var url = '/ProgressManagement/Plan/EditPlan.aspx?prjId=' + $('#hfldPrjId').val()
				+ '&action=' + type + '&planId=' + $('#hfldChecked').val();
			top.ui._editplan = window;
			top.ui.openWin({ title: title, url: url });
		}

		// 编制进度
		function plaitPlanWBS() {
			top.ui._PlanDetail = window;
			var progressVerId = $('#hfldChecked').val();
			var url = '/ProgressManagement/Plan/PlanDetail.aspx?prjId=' + $('#hfldPrjId').val() + '&verId=' + progressVerId;
			var titleText = '编制进度计划';
			toolbox_oncommand(url, titleText);
		}

		// 编制进度，点击超链接是调用
		function plaitPlanWBS2(elem) {
			$('#hfldChecked').val($(elem).attr('vid'));
			plaitPlanWBS();
		}

		//查看进度
		function lookPlanWBS() {
			progressId = $('#' + $('#hfldChecked').val()).attr('guid');
			var url = '/ProgressManagement/Plan/PlanView.aspx?ic=' + progressId;
			var titleText = '查看进度计划';
			toolbox_oncommand(url, titleText);
		}
		//复选框选择
		function setButton(jwTable, hdChkId) {
			if (!jwTable.table) return;
			if (jwTable.table.firstChild.childNodes.length == 1) {
				//table中没有数据
				return;
			}

			jwTable.registClickTrListener(function () {
				checkedSingle(this.id);
				if (hdChkId != '') {
					$('#hfldChecked').val(this.id);
				}
			});
			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				var values = jwTable.getCheckedChkIdJson(checkedChk);
				if (checkedChk.length == 0) {
					disabledButton();
					values = '';
				}
				else if (checkedChk.length == 1) {
					checkedSingle(values);
				}
				else {
					checkMultiple(values);
				}
				$('#' + hdChkId).val(values);
			});
			jwTable.registClickAllCHKLitener(function () {
				var values = '';
				if (this.checked) {
					values = jwTable.getCheckedChkIdJson();
					if (values.indexOf('[') == -1) {
						checkedSingle(values);
					}
					else {
						checkMultiple(values);
					}
				}
				else {
					disabledButton();
				}
				$('#' + hdChkId).val(values);
			});
		};

		//单选
		function checkedSingle(id) {
			//启用所有按钮
			$('#btnEdit').removeAttr('disabled');
			$('#btnDel').removeAttr('disabled');
			$('#btnplaitWBS').removeAttr('disabled');
			$('#btnLook').removeAttr('disabled');

			if (document.getElementById(id).childNodes[flowStateIndex].firstChild) {
				var state = document.getElementById(id).childNodes[flowStateIndex].firstChild.state;
				if (state == '0' || state == '1' || state == '-2') {
					$('#btnEdit').attr('disabled', 'disabled');
					$('#btnDel').attr('disabled', 'disabled');
					$('#btnplaitWBS').attr('disabled', 'disabled');
				}
				else {
					$('#btnEdit').removeAttr('disabled');
					$('#btnDel').removeAttr('disabled');
				}
			}
		}
		//多选
		function checkMultiple(ids) {
			disabledButton();
			var allow = true;
			var array = ids.substr(1, ids.length - 2).replace(/"/g, '').split(',');

			if (array.length == 1) {
				checkedSingle(array[0]);
			}
			else {
				for (i = 0; i < array.length; i++) {
					var id = array[i];
					if (document.getElementById(id).childNodes[flowStateIndex].firstChild) {
						var state = document.getElementById(id).childNodes[flowStateIndex].firstChild.state;
						if (state == '0' || state == '1') {
							allow = false;
							break;
						}
					}
				}

				if (allow) {
					document.getElementById('btnDel').removeAttribute('disabled');
				}
				else {
					document.getElementById('btnDel').setAttribute('disabled', 'disabled');
				}
			}

		}

		//禁用按钮
		function disabledButton() {
			$('#btnEdit').attr('disabled', 'disabled');
			$('#btnDel').attr('disabled', 'disabled');
			$('#btnplaitWBS').attr('disabled', 'disabled');
			$('#btnLook').attr('disabled', 'disabled');

		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
		<table>
			<tr>
				<td style="width: 100%; height: 100%;">
					<table style="width: 100%; height: 100%;">
						<tr>
							<td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
								<table class="tab">
									<tr class="divFooter" style="text-align: left;">
										<td>
											<input type="button" id="btnAdd" value="新增" onclick="editPlan('add')" runat="server" />

											<input type="button" id="btnEdit" value="编辑" state="1" disabled="disabled" onclick="editPlan('edit')" />
											<asp:Button ID="btnDel" Text="删除" state="1" disabled="disabled" OnClientClick="return confirm('您确定要删除吗？');" OnClick="btnDel_Click" runat="server" />
											<input type="button" id="btnplaitWBS" value="编制进度" state="1" disabled="disabled"
												style="width: auto;" onclick="plaitPlanWBS()" />
											<input type="button" id="btnLook" value="查看进度" state="1" disabled="disabled" style="width: auto;"
												onclick="lookPlanWBS()" />
											<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="107" BusiClass="001" runat="server" />
										</td>
									</tr>
									<tr>
										<td style="vertical-align: top;" colspan="3">
											<div id="divBudget" class="pagediv" style="overflow: hidden;">
												<asp:GridView ID="gvwPlans" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwPlans_RowDataBound" DataKeyNames="ProgressVersionId,ProgressId" runat="server">
<EmptyDataTemplate>
														<table id="tblEmpty">
															<tr class="header">
																<th scope="col" style="width: 20px;">
																	<input type="checkbox" />
																</th>
																<th scope="col">
																	序号
																</th>
																<th scope="col">
																	计划名称
																</th>
																<th scope="col">
																	版本号
																</th>
																<th scope="col">
																	执行
																</th>
																<th scope="col">
																	流程状态
																</th>
																<th scope="col">
																	主计划
																</th>
																<th scope="col">
																	计划编制人
																</th>
																<th scope="col">
																	计划编制日期
																</th>
															</tr>
														</table>
													</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
																<input type="checkbox" id="chkAll" />
															</HeaderTemplate><ItemTemplate>
																<input type="checkbox" id="chkItem" />
															</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="序号" DataField="No" HeaderStyle-Width="20px" /><asp:TemplateField HeaderText="计划名称" HeaderStyle-Width="320px"><ItemTemplate>
																<a class="link tooltip" title='<%# Eval("VersionName") %>' vid='<%# Eval("ProgressVersionId") %>' onclick="plaitPlanWBS2(this)">
																	<%# StringUtility.GetStr(Eval("VersionName"), 25) %></a>
															</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="版本号" DataField="VersionCode" /><asp:BoundField HeaderText="执行" DataField="Latest" HeaderStyle-Width="30px" /><asp:TemplateField HeaderText="流程状态" HeaderStyle-Width="70px"><ItemTemplate>
																<%# Common2.GetState(Eval("FlowState").ToString()) %>
															</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="主计划" HeaderStyle-Width="30px" DataField="Main" /><asp:BoundField HeaderText="计划编制人" DataField="UserName" /><asp:BoundField HeaderText="计划编制日期" HeaderStyle-Width="80px" DataField="InputDate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
												<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
												</webdiyer:AspNetPager>
											</div>
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hfldChecked" runat="server" />
	<asp:HiddenField ID="hfldAddPlan" runat="server" />
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	</form>
</body>
</html>
