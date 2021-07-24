<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Apply.aspx.cs" Inherits="ProgressManagement_Modify_Apply" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>调整申请</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript">
		var fIndex = 5; //流程状态的的列索引
		$(document).ready(function () {
		    var jwTable = new JustWinTable('gvwPlans');
		    setWidthHeight();
		    showTooltip("tooltip", 25);
		    setButton2(jwTable, 'hfldChecked');
		    setWfButtonState2(jwTable, 'WF_1');
		    $('#btnLook').bind('click', function () {
		        var id = $('#hfldChecked').val();
		        $('#' + id).find('a[class="link tooltip"]').click();
		    });
		    replaceEmptyTable('tblEmpty', 'gvwPlans');
		});

		function setWidthHeight() {
			$('#divBudget').height($(this).height());
			$('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 7);
			$('#div_project').height($(this).height() - 20);
		}
		//调整申请 | 编辑申请
		//type:add/edit
		function addApply(type) {
			var title = (type == 'add') ? '新增申请' : '编辑申请';
			var id = $('#hfldChecked').val();
			var progressId = $('#' + id).attr('progressId');
			var prjId = $('#hfldPrjId').val();
			var prjYear = $('#hfldYear').val();
			top.ui._ApplyAdd = window;
			var url = '/ProgressManagement/Modify/ApplyAdd.aspx?prjId=' + prjId + '&year=' + prjYear + '&progressId=' + progressId + '&verId=' + id + '&type=' + type;
			toolbox_oncommand(url, title);
		}

		//编制进度
		function plaitPlanWBS() {
			top.ui._ApplyAdd = window;
			var progressVerId = $('#hfldChecked').val();
			var prjId = $('#hfldPrjId').val();
			var url = '/ProgressManagement/Plan/PlanDetail.aspx?prjId=' + prjId + '&verId=' + progressVerId;
			var titleText = '编制进度计划';
			toolbox_oncommand(url, titleText);
		}

		//历史版本 
		function lookVersion() {
			top.ui._PlanDetail = window;
			var id = $('#hfldChecked').val();
			var progressId = $('#' + id).attr('progressId');
			var url = '/ProgressManagement/Modify/HistoryVersions.aspx?progressId=' + progressId;
			var titleText = '历史版本查询';
			toolbox_oncommand(url, titleText);
		}

		//查看计划申请
		function lookApply(verId, verName, state) {
			top.ui._ApplyView = window;
			var url = '/ProgressManagement/Modify/ApplyView.aspx?ic=' + verId;
			if (state == '') {
				url += '&modify=false';
			}
			var titleText = verName + '-查看计划';
			toolbox_oncommand(url, titleText);
		}

		//按钮控制权限
		function setButton2(jwTable, hdChkId) {
			if (!jwTable.table) return;
			if (jwTable.table.firstChild.childNodes.length == 1) {
				return;
			}

			jwTable.registClickTrListener(function () {
				checkedSingle(this.id);
				$('#' + hdChkId).val(this.id);
			});
			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				var values = jwTable.getCheckedChkIdJson(checkedChk);

				if (checkedChk.length == 0) {
					//禁用所有按钮
					disabledAllButton();
					values = '';
				}
				else if (checkedChk.length == 1) {
					var ckId = jwTable.getCheckedChkIdJson(checkedChk);
					checkedSingle(ckId);
				}
				else {
					checkMultiple(values);
				}
				$('#' + hdChkId).val(values);
			});
			jwTable.registClickAllCHKLitener(function () {
				var values = '';
				if (this.checked) {
					var values = jwTable.getCheckedChkIdJson();
					if (values.indexOf('[') == -1) {
						checkedSingle(values);
					}
					else {
						checkMultiple(values);
					}
				}
				else {
					disabledAllButton();
				}
				$('#' + hdChkId).val(values);
			});
		};

		//单选
		function checkedSingle(id) {

			$('#btnQueryVersion').removeAttr('disabled');
			$('#btnLook').removeAttr('disabled');
			var state = $('#' + id).find('TD').eq(fIndex).find('SPAN').attr('state');
			if (state == '-1' || state == '-3') {
				$('#btnEdit').removeAttr('disabled');
				$('#btnDel').removeAttr('disabled');
				$('#btnPlait').removeAttr('disabled');
				$('#btnStartWF').removeAttr('disabled'); //控制流程按钮
				$('#btnAdd').attr('disabled', true);
			}
			else if (state == '1') {
				$('#btnAdd').removeAttr('disabled');
			}
		}

		//多选
		function checkMultiple(ids) {
			disabledAllButton();
			var allow = true;
			var array = ids.substr(1, ids.length - 2).replace(/"/g, '').split(',');
			if (array.length == 1) {
				checkedSingle(array[i]);
			}
			else {
				for (i = 0; i < array.length; i++) {
					var id = array[i];
					if (document.getElementById(id).childNodes[fIndex].firstChild) {
						var state = document.getElementById(id).childNodes[fIndex].firstChild.state;
						if (state == '-1') {
							allow = true;
						}
						else {
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

		//禁用所有按钮
		function disabledAllButton() {
			$(':button').each(function () {
				$(this).attr('disabled', 'disabled');
			});
			$('#btnDel').attr('disabled', 'disabled');
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
											<input type="button" id="btnAdd" value="调整申请" state="1" style="width: auto" disabled="disabled"
												onclick="addApply('add')" />
											<input type="button" id="btnEdit" value="编辑申请" state="1" style="width: auto" disabled="disabled"
												onclick="addApply('edit')" />
											<asp:Button ID="btnDel" Text="删除申请" OnClientClick="return confirm('您确定要删除吗？');" Style="width: auto" OnClick="btnDel_Click" runat="server" />
											<input type="button" id="btnPlait" value="编制进度" state="1" style="width: auto" disabled="disabled"
												onclick="plaitPlanWBS()" />
											<input type="button" id="btnLook" value="查看进度" state="1" style="width: auto" disabled="disabled" />
											<input type="button" id="btnQueryVersion" value="历史版本" state="1" style="width: auto"
												disabled="disabled" onclick="lookVersion()" />
											<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="108" BusiClass="001" runat="server" />
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
																<th scope="col" style="width: 20px;">
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
																	申请人
																</th>
																<th scope="col">
																	录入日期
																</th>
															</tr>
														</table>
													</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
																<input type="checkbox" id="chkAll" />
															</HeaderTemplate><ItemTemplate>
																<input type="checkbox" id="chkItem" />
															</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="序号" DataField="No" HeaderStyle-Width="20px" /><asp:TemplateField HeaderText="计划名称" HeaderStyle-Width="320px"><ItemTemplate>
																<a class="link tooltip" title='<%# Eval("VersionName") %>' onclick="lookApply('<%# Eval("ProgressVersionId") %>','<%# StringUtility.GetStr(Eval("VersionName"), 10) %>','<%# Eval("MFlowState") %>')">
																	<%# StringUtility.GetStr(Eval("VersionName"), 25) %></a>
															</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="版本号" DataField="VersionCode" /><asp:BoundField HeaderText="执行" DataField="Latest" HeaderStyle-Width="30px" /><asp:TemplateField HeaderText="流程状态" HeaderStyle-Width="70px"><ItemTemplate>
																<%# GetState(Eval("MFlowState").ToString()) %>
															</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="主计划" DataField="Main" HeaderStyle-Width="30px" /><asp:BoundField HeaderText="申请人" DataField="UserName" /><asp:BoundField HeaderText="录入日期" HeaderStyle-Width="80px" DataField="InputDate" DataFormatString="{0:yyyy-MM-dd}" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	<asp:HiddenField ID="hfldYear" runat="server" />
	</form>
</body>
</html>
