<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="SetPrjPrivilege.aspx.cs" Inherits="PrjManager_SetPrjPrivilege" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>项目设置权限</title><link href="../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />
<link href="../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/ecmascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			// 添加验证
			$('#btnQuery')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
			replaceEmptyTable('emptyTable', 'gvDataInfo');
			var jwTable = new JustWinTable('gvDataInfo');
			showTooltip('tooltip', 25);
			var btnPrivilege = $('#btnPrivilege');
			var btnAllocRole = $('#btnAllocRole');
			formateTable('gvDataInfo', 4);

			// 单击行
			jwTable.registClickTrListener(function () {
				$('#hfldCheckedIds').val(this.id);
				if (this.id != '') {
					btnPrivilege.removeAttr('disabled');
					btnAllocRole.removeAttr('disabled');
				}
			});

			// 单击复选框
			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				if (checkedChk.length == 1) {
					var tr1 = getFirstParentElement(checkedChk[0], 'TR');
					btnPrivilege.removeAttr('disabled');
					btnAllocRole.removeAttr('disabled');
					$('#hfldCheckedIds').val(tr1.id);
				}
				else if (checkedChk.length > 1) {
					var idArr = new Array();
					for (var i = 0; i < checkedChk.length; i++) {
						var tr = getFirstParentElement(checkedChk[i], 'TR');
						idArr.push(tr.id);
					}
					var json = jw.array1dToJson(idArr);
					$('#hfldCheckedIds').val(json);
					btnPrivilege.removeAttr('disabled');
					btnAllocRole.removeAttr('disabled');
				}
				else {
					btnPrivilege.attr('disabled', 'disabled');
					btnAllocRole.attr('disabled', 'disabled');
				}
			});


			// 单击全选按钮
			jwTable.registClickAllCHKLitener(function () {
				if (jwTable.isCheckedAll()) {
					btnPrivilege.removeAttr('disabled');
					var checkedChk = jwTable.getAllChk();
					var idArr = new Array();
					for (var i = 0; i < checkedChk.length; i++) {
						var tr = getFirstParentElement(checkedChk[i], 'TR');
						idArr.push(tr.id);
					}
					var json = jw.array1dToJson(idArr);
					$('#hfldCheckedIds').val(json);
					btnPrivilege.removeAttr('disabled');
					btnAllocRole.removeAttr('disabled');
				}
				else {
					btnPrivilege.attr('disabled', 'disabled');
					btnAllocRole.attr('disabled', 'disabled');
				}
			});
		});

		// 选择人员
		function selectUser(id, name) {
			jw.selectOneUser({ nameinput: name, codeinput: id });
		}

		// 选择部门
		function SelDept(id, name) {
			jw.selectOneDep({ nameinput: 'txtProjPeopleDep', idinput: 'hfldProjPeopleDep' });
		}

		// 分配人员
		function allocUser() {
			var url = '/Common/AllocUser.aspx?type=tender&idJson=' + $('#hfldCheckedIds').val();
			top.ui.openWin({ title: '设置权限', url: url, width: 650, height: 500 });
		}

		// 分配角色
		function allocRole() {
			var url = '/Common/AllocRole.aspx?tableName=PT_PrjInfo_ZTB_Detail&dataId=' + $('#hfldCheckedIds').val();
			top.ui.openWin({ title: '选择角色', url: url });
		}

	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		<tr>
			<td style="vertical-align: top;">
				<table class="queryTable" id="tbQuery" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd" style="white-space: nowrap;">
							项目编号
						</td>
						<td>
							<asp:TextBox ID="txtPrjCode" Style="width: 120px;" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
						</td>
						<td class="descTd" style="white-space: nowrap;">
							项目名称
						</td>
						<td>
							<asp:TextBox ID="txtPrjName" Style="width: 120px;" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
						</td>
						<td class="descTd" style="white-space: nowrap;">
							立项申请日期
						</td>
						<td>
							<asp:TextBox ID="txtStartTime" Style="width: 120px;" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd" style="white-space: nowrap;">
							至
						</td>
						<td>
							<asp:TextBox ID="txtEndTime" Style="width: 120px;" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td class="descTd" style="white-space: nowrap;">
							项目经理
						</td>
						<td>
							<span class="spanSelect" style="width: 122px;">
								<asp:TextBox ID="txtTenderPrjManager" Style="width: 97px; height: 15px;
									border: none; line-height: 16px; margin: 1px 0px 1px 2px;" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
								<img src="../../images/icon.bmp" style="float: right; height: 18px;" alt="选择" id="img2"
									onclick="selectUser('hfldTenderPrjManager','txtTenderPrjManager');" />
							</span>
							
							<input id="hfldTenderPrjManager" type="hidden" style="width: 1px" runat="server" />

						</td>
						<td class="descTd" style="white-space: nowrap;">
							项目状态
						</td>
						<td>
							<asp:DropDownList ID="dropPrjState" Width="100%" runat="server"></asp:DropDownList>
						</td>
						<td class="descTd" style="white-space: nowrap;">
							项目来源
						</td>
						<td>
							<asp:DropDownList ID="dropPrjSource" Width="100%" runat="server"><asp:ListItem Value="" Text="" /><asp:ListItem Value="0" Text="手动立项" /><asp:ListItem Value="1" Text="投标立项" /></asp:DropDownList>
						</td>
						<td class="descTd" style="white-space: nowrap;">
							项目属性
						</td>
						<td>
							<asp:DropDownList ID="dropProperty" Width="100%" runat="server"></asp:DropDownList>
						</td>
					</tr>
					<tr>
						<td class="descTd" style="white-space: nowrap;">
							项目类型
						</td>
						<td>
							<asp:DropDownList ID="dropPrjKindClass" Width="100%" runat="server"></asp:DropDownList>
						</td>
						<td class="descTd" style="white-space: nowrap;">
							申请部门
						</td>
						<td>
							<span class="spanSelect" style="width: 122px;">
								<asp:TextBox ID="txtProjPeopleDep" Style="width: 97px; height: 15px;
									border: none; line-height: 16px; margin: 1px 0px 1px 2px;" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
								<img src="../../images/icon.bmp" style="float: right;" alt="选择" id="img1" onclick="SelDept('hfldProjPeopleDep','txtProjPeopleDep');" runat="server" />

							</span>
							<input id="hfldProjPeopleDep" type="hidden" style="width: 1px" runat="server" />

						</td>
						<td style="white-space: nowrap;">
							<asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
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
							<input type="button" value="分配人员" id="btnPrivilege" onclick="allocUser();" disabled="disabled"
								style="width: 70px;" />
							<input type="button" value="分配角色" id="btnAllocRole" onclick="allocRole();" style="width: 70px;"
								disabled="disabled" />
						</td>
					</tr>
					<tr>
						<td>
							<div>
								<asp:GridView ID="gvDataInfo" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvDataInfo_RowDataBound" DataKeyNames="PrjGuid,TypeCode,Primit,IsTender" runat="server">
<EmptyDataTemplate>
										<table id="emptyTable">
											<tr>
												<th class="header">
													序号
												</th>
												<th class="header">
													项目状态
												</th>
												<th class="header">
													项目经理
												</th>
												<th class="header">
													项目编号
												</th>
												<th class="header">
													项目名称
												</th>
												<th class="header">
													权限人员
												</th>
												<th class="header">
													项目来源
												</th>
												<th class="header">
													立项申请日期
												</th>
												<th class="header">
													开始日期
												</th>
												<th class="header">
													结束日期
												</th>
												<th class="header">
													申请部门
												</th>
												<th class="header">
													项目属性
												</th>
											</tr>
										</table>
									</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
												<asp:CheckBox ID="cbAllBox" runat="server" />
											</HeaderTemplate><ItemTemplate>
												<asp:CheckBox ID="cbBox" runat="server" />
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
												<%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
											</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="项目状态" DataField="PrjStateName" /><asp:TemplateField HeaderText="项目编号"><ItemTemplate>
												<%# Eval("PrjCode") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="150px"><ItemTemplate>
												<asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(Eval("PrjName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server">
                                                        <span class="link" onclick="<%# ViewInfo(Eval("PrjGuid").ToString(), (bool)Eval("IsTender")) %>">
                                                            <%# StringUtility.GetStr(Eval("PrjName").ToString(), 25) %>
                                                        </span>
												</asp:HyperLink>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目经理"><ItemTemplate>
												<span class="tooltip" title="<%# Eval("PrjMangerName") %>">
													<%# StringUtility.GetStr(Eval("PrjMangerName").ToString(), 25) %>
												</span>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="权限人员"><ItemTemplate>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="角色名"><ItemTemplate>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目来源"><ItemTemplate>
												<%# ((bool)Eval("IsTender")) ? "投标立项" : "手工立项" %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="立项申请日期"><ItemTemplate>
												<%# Common2.GetTime(Eval("InputDate")) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="开始日期"><ItemTemplate>
												<%# Common2.GetTime(Eval("StartDate")) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结束日期"><ItemTemplate>
												<%# Common2.GetTime(Eval("EndDate")) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申请部门"><ItemTemplate>
												<span title="<%# Eval("ProjPeopleDep") %>" class="tooltip">
													<%# StringUtility.GetStr(Eval("ProjPeopleDep").ToString(), 25) %>
												</span>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目属性"><ItemTemplate>
												<span title="<%# Eval("prjPropertyName") %>" class="tooltip">
													<%# StringUtility.GetStr(Eval("prjPropertyName").ToString(), 25) %>
												</span>
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
	<asp:HiddenField ID="hfldCheckedIds" runat="server" />
	<div id="divFramPerson" title="选择人员" style="display: none">
		<iframe id="IframePerson" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<div id="divFramRole" title="" style="display: none">
		<iframe id="iframeRole" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	</form>
</body>
</html>
