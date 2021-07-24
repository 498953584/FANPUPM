<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="InfoSetUp.aspx.cs" Inherits="TenderManage_InfoSetUp" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>信息立项申请</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/ecmascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../Script/jw.js"></script>
	<script type="text/javascript" src="../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../Script/jwJson.js"></script>
	<script type="text/javascript" src="../Script/wf.js"></script>
	<script type="text/javascript" src="/Script/DecimalInput.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			// 添加验证
			$('#brnQuery')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
			replaceEmptyTable('emptyTable', 'gvDataInfo');
			var jwTable = new JustWinTable('gvDataInfo');
			showTooltip('tooltip', 25);
			setWfButtonState2(jwTable, 'WF1');

			jwTable.registClickTrListener(function () {
				var btnEdit = document.getElementById('btnEdit');
				var btnDelete = document.getElementById('btnDelete');
				var btnQuery = document.getElementById('btnQuery');
				document.getElementById('hfldCheckedIds').value = this.id;
				if (this.id != '') {
					btnQuery.removeAttribute('disabled');
				}
				var WFState = $(this).attr('WFState');
				if (WFState == "-1") {
					btnEdit.removeAttribute('disabled');
					btnDelete.removeAttribute('disabled');
				}
				if (WFState == "-3" || WFState == "1") {
					btnEdit.removeAttribute('disabled');
					btnDelete.setAttribute('disabled', 'disabled');
				}
				if (WFState == "0" || WFState == "-2") {
					btnEdit.setAttribute('disabled', 'disabled');
					btnDelete.setAttribute('disabled', 'disabled');
				}
			});
			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				var btnEdit = document.getElementById('btnEdit');
				var btnDelete = document.getElementById('btnDelete');
				var btnQuery = document.getElementById('btnQuery');
				if (checkedChk.length == "1") {
					var tr1 = getFirstParentElement(checkedChk[0].parentNode, 'TR');
					document.getElementById('hfldCheckedIds').value = tr1.id;
					btnQuery.removeAttribute('disabled');
					var WFState = $(tr1).attr('WFState');
					if (WFState == "-1") {
						btnEdit.removeAttribute('disabled');
						btnDelete.removeAttribute('disabled');
					}
					if (WFState == "-3" || WFState == "1") {
						btnEdit.removeAttribute('disabled');
						btnDelete.setAttribute('disabled', 'disabled');
					}
					if (WFState == "0" || WFState == "-2") {
						btnEdit.setAttribute('disabled', 'disabled');
						btnDelete.setAttribute('disabled', 'disabled');
					}
				}
				else if (checkedChk.length > 1) {
					btnEdit.setAttribute('disabled', 'disabled');
					btnQuery.setAttribute('disabled', 'disabled');
					btnDelete.setAttribute('disabled', 'disabled');
					var ISDelete = true;
					var ids = "";
					for (var i = 0; i < checkedChk.length; i++) {
						var trs = getFirstParentElement(checkedChk[i].parentNode, 'TR');
						ids += trs.id + ",";
						var WFState = $(trs).attr('WFState');
						if (WFState != "-1") {
							ISDelete = false;
						}

					}
					if (ISDelete == true) {
						btnDelete.removeAttribute('disabled');
					}
					document.getElementById('hfldCheckedIds').value = ids;

				}

				else {
					btnEdit.setAttribute('disabled', 'disabled');
					btnDelete.setAttribute('disabled', 'disabled');
					btnQuery.setAttribute('disabled', 'disabled');
				}

			});

			jwTable.registClickAllCHKLitener(function () {
				var btnEdit = document.getElementById('btnEdit');
				var btnDelete = document.getElementById('btnDelete');
				var btnQuery = document.getElementById('btnQuery');
				if (jwTable.isCheckedAll()) {
					btnEdit.setAttribute('disabled', 'disabled');
					btnDelete.setAttribute('disabled', 'disabled');
					btnQuery.setAttribute('disabled', 'disabled');
					var checkedChk = jwTable.getAllChk();
					var ISDelete = true;
					var ids = "";
					for (var i = 0; i < checkedChk.length; i++) {
						var tr = checkedChk[i].parentNode.parentNode;
						ids += tr.id + ",";
						var WFState = $(tr).attr('WFState');
						if (WFState != "-1") {
							ISDelete = false;
						}
					}
					document.getElementById('hfldCheckedIds').value = ids;
					if (ISDelete == true) {
						btnDelete.removeAttribute('disabled');
					}
				}
				else {
					btnEdit.setAttribute('disabled', 'disabled');
					btnDelete.setAttribute('disabled', 'disabled');
					btnQuery.setAttribute('disabled', 'disabled');
				}
			});

			registerDeleteTenderEvent();
			registerUpdateTenderEvent();

		});
		function registerDeleteTenderEvent() {
			var btnDelete = document.getElementById('btnDelete');
			btnDelete.onclick = function () {
				if (!window.confirm('系统提示：\n\n确定要删除吗？')) {
					return false;
				}
			}
		}
		function registerUpdateTenderEvent() {
			var btnUpdate = document.getElementById('btnEdit');
			var hfldPrjId = document.getElementById('hfldCheckedIds');
			addEvent(btnUpdate, 'click', function () {
				parent.desktop.InfoAdd = window;
				//top.ui._InfoAdd = window;
				var url = "/TenderManage/InfoAdd.aspx?Action=Update&purl=InfoSetUp.aspx&PrjId=" + hfldPrjId.value;
				toolbox_oncommand(url, "编辑项目信息");
			});
		}
		function Query() {
			var hfldPrjId = document.getElementById('hfldCheckedIds');
			var url = "/TenderManage/InfoView.aspx?ic=" + hfldPrjId.value;
			viewopen(url, '查看项目信息');
		}

		// 标签页查看
		function QueryTab(prjId) {
			parent.desktop.InfoView = window;
			var url = '/TenderManage/InfoView.aspx?&&ic=' + prjId;
			toolbox_oncommand(url, "查看项目信息");
		}
		function addPrj() {
			parent.desktop.InfoAdd = window;
			//            parent._infoAdd = window;
			var url = "/TenderManage/InfoAdd.aspx?Action=Add&Random=" + new Date().getTime();
			toolbox_oncommand(url, "新增项目信息");
		}

		// 甲方名称
		function pickCorp(param) {
			jw.selectOneCorp({ idinput: 'hfldOwner', nameinput: 'txtOwner' });
		}

		// 选择部门
		function SelDept() {
			jw.selectOneDep({ nameinput: 'txtProjPeopleDep', idinput: 'hfldProjPeopleDep' });
		}

		// 管理日期
		function controlDate(para) {
			var startDates = document.getElementById('txtStartTime').value;
			var startDateList = startDates.split('-');
			var startDate = new Date(startDateList[0], startDateList[1] - 1, startDateList[2]);

			var endDates = document.getElementById('txtEndTime').value;
			var endDatesList = endDates.split('-');
			var endDate = new Date(endDatesList[0], endDatesList[1] - 1, endDatesList[2]);

			if (startDates != '') {
				if (startDate == 'NaN') {
					$.messager.alert('系统提示', '开始日期输入日期格式不正确！');
					para.value = '';
					return;
				}
			}
			if (endDates != '') {
				if (endDate == 'NaN') {
					$.messager.alert('系统提示', '结束日期输入日期格式不正确！');
					para.value = '';
					return;
				}
			}
		}

		// 选择申请人
		function selectApplyUser() {
			jw.selectOneUser({ nameinput: 'txtName', codeinput: 'hfldName' });
		}

	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<table border="0" cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td style="vertical-align: top;">
					<table class="queryTable" cellpadding="3px" cellspacing="0px">
						<tr>
							<td class="descTd" style="white-space: nowrap;">
								项目编号
							</td>
							<td>
								<asp:TextBox ID="txtPrjCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
							</td>
							<td class="descTd" style="white-space: nowrap;">
								项目名称
							</td>
							<td>
								<asp:TextBox ID="txtPrjName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
							</td>
							<td class="descTd" style="white-space: nowrap;">
								立项申请人
							</td>
							<td>
								<asp:TextBox ID="txtName" Style="width: 120px;" CssClass="easyui-validatebox select_input" data-options="validType:'validChars[50]'" imgclick="selectApplyUser" runat="server"></asp:TextBox>
								<input id="hfldName" type="hidden" style="width: 1px" runat="server" />

							</td>
							<td class="descTd" style="white-space: nowrap;">
								项目类型
							</td>
							<td>
								<asp:DropDownList ID="dropPrjKindClass" Width="100%" runat="server"></asp:DropDownList>
							</td>
						</tr>
						<tr>
							<td class="descTd" style="white-space: nowrap;">
								立项申请日期
							</td>
							<td>
								<asp:TextBox ID="txtStartTime" Width="120px" onclick="WdatePicker()" onblur="controlDate(this);" runat="server"></asp:TextBox>
							</td>
							<td class="descTd" style="white-space: nowrap;">
								至
							</td>
							<td>
								<asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" onblur="controlDate(this);" runat="server"></asp:TextBox>
							</td>
							<td class="descTd" style="white-space: nowrap;">
								建设单位
							</td>
							<td>
								<asp:TextBox ID="txtOwner" Style="width: 120px;" CssClass="easyui-validatebox select_input" data-options="validType:'validChars[50]'" imgclick="pickCorp" runat="server"></asp:TextBox>
								<asp:HiddenField ID="hfldOwner" runat="server" />
							</td>
							<td class="descTd" style="white-space: nowrap;">
								流程状态
							</td>
							<td>
								<asp:DropDownList ID="dropWFState" Width="100%" runat="server"><asp:ListItem Value="" Text="" /><asp:ListItem Value="-1" Text="未提交" /><asp:ListItem Value="0" Text="审核中" /><asp:ListItem Value="1" Text="已审核" /><asp:ListItem Value="-2" Text="驳回" /><asp:ListItem Value="-3" Text="重报" /></asp:DropDownList>
							</td>
						</tr>
						<tr>
							<td class="descTd" style="white-space: nowrap;">
								申请部门
							</td>
							<td>
								<asp:TextBox ID="txtProjPeopleDep" Style="width: 120px;" CssClass="easyui-validatebox select_input" data-options="validType:'validChars[50]'" imgclick="SelDept" runat="server"></asp:TextBox>
								<input id="hfldProjPeopleDep" type="hidden" style="width: 1px" runat="server" />

							</td>
							<td class="descTd" style="white-space: nowrap;">
								项目属性
							</td>
							<td>
								<asp:DropDownList ID="dropPrjProperty" Width="100%" runat="server"></asp:DropDownList>
							</td>
							<td style="white-space: nowrap;">
								<asp:Button ID="brnQuery" Text="查询" OnClick="brnQuery_Click" runat="server" />
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td style="vertical-align: top">
					<table class="tab" style="vertical-align: top;">
						<tr>
							<td class="divFooter" style="text-align: left">
								<input type="button" id="btnAdd" value="新增" onclick="addPrj()" />
								<input type="button" id="btnEdit" value="编辑" disabled="disabled" />
								<asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
								<input type="button" id="btnQuery" disabled="disabled" value="查看" onclick="Query();" />
								<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="089" BusiClass="001" runat="server" />
							</td>
						</tr>
						<tr>
							<td>
								<div>
									<asp:GridView ID="gvDataInfo" CssClass="gvdata" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="gvDataInfo_RowDataBound" DataKeyNames="PrjGuid,ProjFlowSate,StateText" runat="server">
<EmptyDataTemplate>
											<table id="emptyTable">
												<tr class="header">
													<th scope="col" style="width: 20px;">
													</th>
													<th scope="col">
														序号
													</th>
													<th scope="col">
														项目状态
													</th>
													<th scope="col">
														立项申请人
													</th>
													<th scope="col">
														项目类型
													</th>
													<th scope="col">
														项目编号
													</th>
													<th scope="col">
														项目名称
													</th>
													<th scope="col">
														建设单位
													</th>
													<th scope="col">
														工程造价
													</th>
													<th scope="col">
														工程工期
													</th>
													<th scope="col">
														立项申请日期
													</th>
													<th scope="col">
														流程状态
													</th>
												</tr>
											</table>
										</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
													<asp:CheckBox ID="cbAllBox" runat="server" />
												</HeaderTemplate><ItemTemplate>
													<asp:CheckBox ID="cbBox" runat="server" />
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="20px">
<ItemTemplate>
													<%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
												</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="项目状态" DataField="StateText" /><asp:TemplateField HeaderText="立项申请人"><ItemTemplate>
													<span class="tooltip" title="<%# Eval("ProjPeopleName") %>">
														<%# StringUtility.GetStr(Eval("ProjPeopleName").ToString(), 25) %>
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申请部门"><ItemTemplate>
													<span class="tooltip" title="<%# Eval("ProjPeopleDep") %>">
														<%# StringUtility.GetStr(Eval("ProjPeopleDep").ToString(), 25) %>
													</span>
												</ItemTemplate></asp:TemplateField><asp:BoundField DataField="PrjCode" HeaderText="项目编号" /><asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="150px"><ItemTemplate>
													<asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" runat="server">
                        <span class="link tooltip" title='<%# Eval("PrjName").ToString() %>' onclick="QueryTab('<%# Eval("PrjGuid") %>');">
                        <%# StringUtility.GetStr(Eval("PrjName").ToString(), 25) %>
                       </span></asp:HyperLink>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="建设单位"><ItemTemplate>
													<span class="tooltip" title="<%# Eval("WorkUnitName") %>">
														<%# StringUtility.GetStr(Eval("WorkUnitName").ToString(), 25) %>
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程造价" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
													<%# Eval("PrjCost") %></ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="工程工期" DataField="Duration" /><asp:TemplateField HeaderText="立项申请日期"><ItemTemplate>
													<%# Common2.GetTime(Eval("InputDate")) %>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
													<%# Common2.GetState(Eval("ProjFlowSate").ToString()) %></ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
								</div>
							</td>
						</tr>
						<tr>
							<td style="padding-top: 5px">
								<asp:Label ID="lblTotal" runat="server"></asp:Label>
							</td>
						</tr>
						<tr>
							<td>
								<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
								</webdiyer:AspNetPager>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	
	<asp:HiddenField ID="hfldCheckedIds" runat="server" />
	<div id="divFramPerson" title="选择人员" style="display: none">
		<iframe id="IframePerson" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<asp:HiddenField ID="hdReturnVal" runat="server" />
	</form>
</body>
</html>
