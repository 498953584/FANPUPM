<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="PrjInfoList.aspx.cs" Inherits="PrjManager_PrjInfoList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>项目信息管理</title><link href="../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="../Script/jquery.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../Script/wf.js"></script>
	<script type="text/javascript" src="../Script/jw.js"></script>
	<script type="text/javascript" src="../Script/json2.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script src="../Script/DecimalInput.js" type="text/javascript"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$('#brnQuery')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
			$('#hfldCheckedIds').val('');
			var jwTable = new JustWinTable('gvPrjInfo');
			replaceEmptyTable('emptyTable', 'gvPrjInfo');
			formateTable('gvPrjInfo', 4);
			showTooltip('tooltip', 25);
			setWfButtonState2(jwTable, 'WF1');
			setButton2(jwTable);
		});

		//设置按钮
		function setButton2(jwTable) {
			if (!jwTable.table) return;
			if (jwTable.table.getElementsByTagName('TR').length == 1) return; 	// 没有数据

			//			// 单击行
			//			jwTable.registClickTrListener(function () {
			//				var typeCode = this.typeCode;
			//				var Privilege = this.limit;
			//				var flowState = this.flowState;
			//				var btnAdd = document.getElementById('btnAdd');
			//				var btnEdit = document.getElementById('btnEdit');
			//				var btnDel = document.getElementById('btnDelete');
			//				var btnQuery = document.getElementById('btnQuery');
			//				//var btnState = document.getElementById('btnStateChange');
			//				if (this.id != '') {
			//					btnQuery.removeAttribute('disabled');
			//					document.getElementById('hfldCheckedIds').value = this.id;
			//					document.getElementById('hfldTender').value = this.tender;
			//					if (Privilege == "1") {
			//						if (flowState == '-1' || flowState == '-3') {
			//							btnEdit.removeAttribute('disabled');
			//						} else {
			//							btnEdit.setAttribute('disabled', 'disabled');
			//						}
			//						//btnState.removeAttribute('disabled');
			//						if (typeCode.length == 5) {
			//							btnAdd.removeAttribute('disabled');
			//							if (this.child == "0") {
			//								//btnDel.removeAttribute('disabled');
			//								if (flowState == '-1' || flowState == '-3') {
			//									btnDel.removeAttribute('disabled');
			//								} else {
			//									btnDel.setAttribute('disabled', 'disabled');
			//								}
			//							}
			//							else {
			//								btnDel.setAttribute('disabled', 'disabled');
			//							}
			//						}
			//						else {
			//							btnAdd.setAttribute('disabled', 'disabled');
			//							//btnDel.removeAttribute('disabled', 'disabled');
			//							if (flowState == '-1' || flowState == '-3') {
			//								btnDel.removeAttribute('disabled');
			//							} else {
			//								btnDel.setAttribute('disabled', 'disabled');
			//							}
			//						}
			//					}
			//					else {
			//						btnAdd.setAttribute('disabled', 'disabled');
			//						btnEdit.setAttribute('disabled', 'disabled');
			//						//btnState.setAttribute('disabled', 'disabled');
			//						btnDel.setAttribute('disabled', 'disabled');
			//					}
			//				}
			//				upAdminPrivilege();
			//			});

			//复选框
			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				var btnAdd = document.getElementById('btnAdd');
				var btnEdit = document.getElementById('btnEdit');
				var btnDel = document.getElementById('btnDelete');
				var btnQuery = document.getElementById('btnQuery');
				if (checkedChk.length == "0") {
					$('#hfldCheckedIds').val('');
					$('#hfldTender').val('');
					$('#btnAdd').removeAttr('disabled');
					$('#btnEdit').attr('disabled', 'disabled');
					$('#btnQuery').attr('disabled', 'disabled');
					$('#btnDelete').attr('disabled', 'disabled');
				}
				else if (checkedChk.length == "1") {
					var tr1 = getFirstParentElement(checkedChk[0].parentNode, 'TR');
					var id = $(tr1).attr('id');
					var typeCode = $(tr1).attr('typeCode');
					var privilege = $(tr1).attr('limit');
					var flowState = $(tr1).attr('flowState');
					var tender = $(tr1).attr('tender');
					btnQuery.removeAttribute('disabled');
					document.getElementById('hfldCheckedIds').value = id;
					document.getElementById('hfldTender').value = tender;

					// privilege == 1 表示有权限
					if (privilege == "1") {
						if (flowState == '-1' || flowState == '-3') {
							btnEdit.removeAttribute('disabled');
						} else {
							btnEdit.setAttribute('disabled', 'disabled');
						}
						if (typeCode.length == 5) {
							// 主项目
							btnAdd.removeAttribute('disabled');
							if (tr1.child == "0") {
								if (flowState == '-1' || flowState == '-3') {
									btnDel.removeAttribute('disabled');
								} else {
									btnDel.setAttribute('disabled', 'disabled');
								}
							}
							else {
								btnDel.setAttribute('disabled', 'disabled');
							}
						}
						else {
							// 子项目，upPriv==0 表示不需要提升管理员权限
							$(btnAdd).attr('disabled', true).attr('upPriv', '0');

							if (flowState == '-1' || flowState == '-3') {
								btnDel.removeAttribute('disabled');
							} else {
								btnDel.setAttribute('disabled', 'disabled');
							}
						}
					}
					else {
						if (typeCode.length == 5) {
							btnAdd.removeAttribute('disabled');
						}
						else {
							btnAdd.setAttribute('disabled', 'disabled');
						}
						btnEdit.setAttribute('disabled', 'disabled');
						//btnState.setAttribute('disabled', 'disabled');
						btnDel.setAttribute('disabled', 'disabled');
					}
				}
				else {
					document.getElementById('hfldCheckedIds').value = jwTable.getCheckedChkIdJson(checkedChk);
					disabledButton();
					var privilegeArray = new Array();
					var flowStateArray = new Array();
					var typeCodeArray = new Array();
					for (var i = 0; i < checkedChk.length; i++) {
						var trSelected = getFirstParentElement(checkedChk[i].parentNode, 'TR');
						typeCodeArray.push(trSelected.typeCode);
						privilegeArray.push(trSelected.limit);
						flowStateArray.push(trSelected.flowState);
					}
					//如果验证权限，验证流程状态，验证父子节点都通过时启动删除按钮
					if (privilegeIsPass(privilegeArray) == true && flowStateIsPass(flowStateArray) == true && typeCodeIsPass(typeCodeArray) == '1') {
						$('#btnDelete').removeAttr('disabled');
					} else {
						$('#btnDelete').attr('disabled', 'disabled');
					}
				}
			});

			//全选
			jwTable.registClickAllCHKLitener(function () {
				disabledButton();
				var privilegeArray = new Array();
				var flowStateArray = new Array();
				var typeCodeArray = new Array();
				var prjIdArray = new Array();
				if (this.checked == true) {
					$('#gvPrjInfo [type=checkbox]').each(function () {
						if (this.id != 'gvPrjInfo_ctl01_cbAllBox') {
							var trSelected = getFirstParentElement(this.parentNode, 'TR');
							typeCodeArray.push(trSelected.typeCode);
							privilegeArray.push(trSelected.limit);
							flowStateArray.push(trSelected.flowState);
							prjIdArray.push(trSelected.id);
						}
					});
					document.getElementById('hfldCheckedIds').value = JSON.stringify(prjIdArray);
					//如果验证权限，验证流程状态，验证父子节点都通过时启动删除按钮
					if (privilegeIsPass(privilegeArray) == true && flowStateIsPass(flowStateArray) == true && typeCodeIsPass(typeCodeArray) == '1') {
						$('#btnDelete').removeAttr('disabled');
					} else {
						$('#btnDelete').attr('disabled', 'disabled');
					}
				} else {
					document.getElementById('hfldCheckedIds').value = '';
					document.getElementById('hfldTender').value = '';
					$('#btnDelete').attr('disabled', 'disabled');
				}
			});
		}

		//判断所选的权限是否通过
		function privilegeIsPass(params) {
			var isPass = true;
			if (params.length == 0) {
				isPass = false;
			}
			for (var i = 0; i < params.length; i++) {
				if (params[i] == '0') {  //如果有一条没有权限 不通过
					isPass = false;
					break;
				}
			}
			return isPass;
		}
		//判断所选的流程状态是否通过
		function flowStateIsPass(params) {
			var isPass = true;
			if (params.length == 0) {
				isPass = false;
			}
			for (var i = 0; i < params.length; i++) {
				if (params[i] != "-1") {   //如果有一个流程状态不是“未提交” 不通过
					isPass = false;
					break;
				}
			}
			return isPass;
		}
		//判断所选的父子节点是否通过
		function typeCodeIsPass(params) {
			var isPass = true;
			if (params.length == 0) {
				isPass = false;
			} else {
				var typeCodes = JSON.stringify(params);
				$.ajax({
					type: 'GET',
					async: false,
					url: '/PrjManager/Handler/CheckTypeCode.ashx?' + new Date().getTime() + '&TypeCodes=' + typeCodes,
					success: function (data) {
						isPass = data;
					}
				});
			}
			return isPass;
		}

		//按钮不可用状态
		function disabledButton() {
			$('#btnAdd').attr('disabled', 'disabled');
			$('#btnEdit').attr('disabled', 'disabled');
			$('#btnQuery').attr('disabled', 'disabled');
		}

		function addPrj() {
			parent.desktop.PrjInfoAdd = window;
			var url = "/PrjManager/PrjInfoAdd.aspx?Action=Add&PrjId=" + document.getElementById("hfldCheckedIds").value;
			toolbox_oncommand(url, "新增项目信息");
		}
		function UpdatePrj() {
			parent.desktop.PrjInfoAdd = window;
			var url = "/PrjManager/PrjInfoAdd.aspx?Action=Update&PrjId=" + document.getElementById("hfldCheckedIds").value;
			toolbox_oncommand(url, "编辑项目信息");
		}
		function QueryViewOpen() {
			var prjId = $('#hfldCheckedIds').val();
			if ($('#hfldTender').val() == 'True') {
				viewopen('/TenderManage/InfoView.aspx?&ic=' + prjId, '信息立项查看');
			} else {
				viewopen('/PrjManager/PrjInfoView.aspx?&ic=' + prjId, '项目信息查看');
			}
		}
		//甲方名称
		function pickCorp(param) {
			jw.selectOneCorp({ nameinput: 'txtOwner', idinput: 'hfldOwner' });
		}
		//选择人员
		function selectUser(id, name) {
			jw.selectOneUser({ nameinput: name, codeinput: id });
		}

		//标签页查看
		function Query(prjId, tender) {
			parent.desktop.StartWorkReportView = window;
			var url = '';
			if (tender == 'False') {
				url = '/PrjManager/PrjInfoView.aspx?&&ic=' + prjId;
			} else {
				url = '/TenderManage/InfoView.aspx?&&ic=' + prjId;
			}
			toolbox_oncommand(url, "项目信息查看");
		}
		//状态变更
		function modifyState() {
			$('#div_modify_state').dialog({
				modal: true,
				buttons: {
					"确定": function () {
						$.ajax({
							type: "GET",
							url: "/Handler/ModifyPrjState.ashx?PrjGuid=" + $('#hfldCheckedIds').val() +
                                "&PrjState=" + $('#dropModifyPrjState').val() + "&Time=" + new Date().getTime(),
							success: function () {
								window.location.href = window.location.href;
							}

						});
						$(this).dialog("close");

					},
					"取消": function () {
						$(this).dialog("close");
					}
				}
			});
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
								<asp:TextBox ID="txtPrjCode" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
							</td>
							<td class="descTd" style="white-space: nowrap;">
								项目名称
							</td>
							<td>
								<asp:TextBox ID="txtPrjName" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
							</td>
							<td class="descTd" style="white-space: nowrap;">
								项目经理
							</td>
							<td>
								<span class="spanSelect" style="width: 122px;">
									<asp:TextBox ID="txtName" Style="width: 97px; height: 15px; border: none;
										line-height: 16px; margin: 1px 0px 1px 2px;" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
									<img src="../../images/icon.bmp" style="float: right;" alt="选择" id="imgName" onclick="selectUser('hfldName','txtName');" runat="server" />

								</span>
								<input id="hfldName" type="hidden" style="width: 1px" runat="server" />

							</td>
							<td class="descTd" style="white-space: nowrap;">
								项目类型
							</td>
							<td>
								<asp:DropDownList ID="dropPrjKindClass" Width="100%" runat="server"></asp:DropDownList>
							</td>
							<td>
							</td>
						</tr>
						<tr>
							<td class="descTd" style="white-space: nowrap;">
								立项申请日期
							</td>
							<td>
								<asp:TextBox ID="txtStartTime" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
							</td>
							<td class="descTd" style="white-space: nowrap;">
								至
							</td>
							<td>
								<asp:TextBox ID="txtEndTime" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
							</td>
							<td class="descTd" style="white-space: nowrap;">
								建设单位
							</td>
							<td>
								<span class="spanSelect" style="width: 122px;">
									<asp:TextBox ID="txtOwner" Style="width: 97px; height: 15px; border: none; line-height: 16px;
										margin: 1px 0px 1px 2px;" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
									<img alt="选择乙方" onclick="pickCorp();" src="../../images/icon.bmp" style="float: right;" />
								</span>
								<asp:HiddenField ID="hfldOwner" runat="server" />
							</td>
							<td class="descTd" style="white-space: nowrap;">
								项目状态
							</td>
							<td>
								<asp:DropDownList ID="dropPrjState" Width="100%" runat="server"></asp:DropDownList>
							</td>
							<td style="white-space: nowrap;">
							</td>
						</tr>
						<tr>
							<td class="descTd" style="white-space: nowrap;">
								项目来源
							</td>
							<td>
								<asp:DropDownList ID="dropPrjSource" Width="100%" runat="server"><asp:ListItem Value="" Text="" /><asp:ListItem Value="0" Text="手工立项" /><asp:ListItem Value="1" Text="投标立项" /></asp:DropDownList>
							</td>
							<td>
								<asp:Button ID="brnQuery" Text="查询" OnClick="brnQuery_Click" runat="server" />
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td>
					<table border="0" class="tab">
						<tr>
							<td class="divFooter" style="text-align: left">
								<input type="button" id="btnAdd" value="新增" onclick="addPrj()" />
								<input type="button" id="btnEdit" value="编辑" disabled="disabled" onclick="UpdatePrj()" />
								<asp:Button ID="btnDelete" Text="删除" OnClientClick="return confirm('您确定要删除吗？');" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
								<input type="button" id="btnQuery" disabled="disabled" value="查看" onclick="QueryViewOpen();" />
                                <asp:Button ID="btnExcel" Text="导出Excel" Width="73px" OnClick="btnExcel_Click" runat="server" />
								
								<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="122" BusiClass="001" runat="server" />
							</td>
						</tr>
						<tr>
							<td style="width: 100%;" valign="top">
								<div>
									<asp:GridView ID="gvPrjInfo" CssClass="gvdata" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="gvPrjInfo_RowDataBound" DataKeyNames="PrjGuid,TypeCode,Primit,i_ChildNum,IsTender,SetUpFlowState" runat="server">
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
														项目编号
													</th>
													<th scope="col">
														项目名称
													</th>
													<th scope="col">
														项目经理
													</th>
													<th scope="col">
														流程状态
													</th>
													<th scope="col">
														建设单位
													</th>
													<th scope="col">
														工程造价
													</th>
													<th scope="col">
														项目来源
													</th>
													<th scope="col">
														项目来源
													</th>
													<th scope="col">
														立项申请日期
													</th>
													<th scope="col">
														开始日期
													</th>
													<th scope="col">
														结束日期
													</th>
												</tr>
											</table>
										</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
													<asp:CheckBox ID="cbAllBox" runat="server" />
												</HeaderTemplate>

<ItemTemplate>
													<asp:CheckBox ID="cbBox" runat="server" />
												</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
													<%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
												</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="PrjStateName" HeaderText="项目状态" /><asp:BoundField DataField="PrjCode" HeaderText="项目编号" /><asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="150px"><ItemTemplate>
													<asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(Eval("PrjName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server">
                                                        
                                                        <span class="link" onclick="Query('<%# Eval("PrjGuid") %>','<%# Eval("IsTender") %>');">
                                                            <%# StringUtility.GetStr(Eval("PrjName").ToString(), 25) %>
                                                        </span>
													</asp:HyperLink>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目经理"><ItemTemplate>
													<span class="tooltip" title="<%# Eval("PrjMangerName") %>">
														<%# StringUtility.GetStr(Eval("PrjMangerName").ToString(), 25) %>
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
													<%# Common2.GetState(Eval("SetUpFlowState").ToString()) %>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="建设单位"><ItemTemplate>
													<span class="tooltip" title="<%# Eval("Owner") %>">
														<%# StringUtility.GetStr(Eval("Owner").ToString(), 25) %>
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程造价" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
													<%# Eval("PrjCost") %></ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目来源"><ItemTemplate>
													<%# ((bool)Eval("IsTender")) ? "投标立项" : "手工立项" %>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="立项申请日期"><ItemTemplate>
													<%# Common2.GetTime(Eval("InputDate")) %>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="开始日期"><ItemTemplate>
													<%# Common2.GetTime(Eval("StartDate")) %>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结束日期"><ItemTemplate>
													<%# Common2.GetTime(Eval("EndDate")) %>
												</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
									<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
									</webdiyer:AspNetPager>
								</div>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	<div id="div_modify_state" title="项目状态变更" style="display: none; text-align: center;">
		项目状态：
		<asp:DropDownList ID="dropModifyPrjState" Width="100px" runat="server"></asp:DropDownList>
	</div>
	<asp:HiddenField ID="hdReturnVal" runat="server" />
	
	<asp:HiddenField ID="hfldCheckedIds" runat="server" />
	<asp:HiddenField ID="hfldTender" runat="server" />
	<link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/default/easyui.css" />
	<link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/icon.css" />
	</form>
</body>
</html>
