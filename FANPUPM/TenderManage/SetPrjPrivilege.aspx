<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="SetPrjPrivilege.aspx.cs" Inherits="TenderManage_SetPrjPrivilege" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>投标项目设置权限</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$('#btnQuery')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
			replaceEmptyTable('emptyTable', 'gvDataInfo');
			var jwTable = new JustWinTable('gvDataInfo');
			showTooltip('tooltip', 25);

			var btnAllocUser = document.getElementById('btnAllocUser');
			var btnAllocRole = document.getElementById('btnAllocRole');
			jwTable.registClickTrListener(function () {
				document.getElementById('hfldCheckedIds').value = this.id;
				if (this.id != '') {
					btnAllocUser.removeAttribute('disabled');
					btnAllocRole.removeAttribute('disabled');
				}
			});

			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				if (checkedChk.length == 1) {
					btnAllocUser.removeAttribute('disabled');
					btnAllocRole.removeAttribute('disabled');
					var tr1 = getFirstParentElement(checkedChk[0].parentNode, 'TR');
					document.getElementById('hfldCheckedIds').value = tr1.id;
				}
				else if (checkedChk.length > 1) {
					var idArr = new Array();
					var ids = "";
					for (var i = 0; i < checkedChk.length; i++) {
						var tr = jw.getParentUntil(checkedChk[i], 'TR');
						idArr.push(tr.id);
					}

					$('#hfldCheckedIds').val(jw.array1dToJson(idArr));
					btnAllocUser.removeAttribute('disabled');
					btnAllocRole.removeAttribute('disabled');
				}
				else {
					btnAllocUser.setAttribute('disabled', 'disabled');
					btnAllocRole.setAttribute('disabled', 'disabled');
				}
			});

			jwTable.registClickAllCHKLitener(function () {
				if (jwTable.isCheckedAll()) {
					var idArr = new Array();
					var checkedChk = jwTable.getAllChk();
					for (var i = 0; i < checkedChk.length; i++) {
						var tr = jw.getParentUntil(checkedChk[i], 'TR');
						idArr.push(tr.id);
					}
					$('#hfldCheckedIds').val(jw.array1dToJson(idArr));

					btnAllocUser.removeAttribute('disabled');
					btnAllocRole.removeAttribute('disabled');
				}
				else {
					btnAllocUser.setAttribute('disabled', 'disabled');
					btnAllocRole.setAttribute('disabled', 'disabled');
				}
			});

		});

		function allocUser() {

			var url = '/Common/AllocUser.aspx?type=tender&idJson=' + $('#hfldCheckedIds').val();
			top.ui.openWin({ title: '设置权限', url: url, width: 650, height: 500 });
		}

		function allocRole() {
			var url = '/Common/AllocRole.aspx?tableName=PT_PrjInfo_ZTB_Detail&dataId=' + $('#hfldCheckedIds').val();
			top.ui.openWin({ title: '选择角色', url: url });
		}


		//选择人员
		function selectUser(id, name) {
			jw.selectOneUser({ nameinput: name, codeinput: id });
		}
		//选择部门
		function SelDept(id, name) {
			jw.selectOneDep({ nameinput: name, idinput: id });
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
						<td>
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
					</tr>
					<tr>
						<td class="descTd" style="white-space: nowrap;">
							项目属性
						</td>
						<td>
							<asp:DropDownList ID="dropProperty" Width="100%" runat="server"></asp:DropDownList>
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
							<input type="button" value="分配人员" id="btnAllocUser" onclick="allocUser();" style="width: 70px;"
								disabled="disabled" />
							<input type="button" value="分配角色" id="btnAllocRole" onclick="allocRole();" style="width: 70px;"
								disabled="disabled" />
						</td>
					</tr>
					<tr>
						<td>
							<div>
								<asp:GridView ID="gvDataInfo" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvDataInfo_RowDataBound" DataKeyNames="PrjGuid" runat="server">
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
													角色名
												</th>
												<th class="header">
													立项申请日期
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
												<%# Eval("Num") %>
											</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="项目状态" DataField="StateText" HeaderStyle-Width="50px" /><asp:BoundField HeaderText="项目经理" DataField="PrjMangerName" HeaderStyle-Width="50px" /><asp:TemplateField HeaderText="项目编号" HeaderStyle-Width="130px"><ItemTemplate>
												<%# Eval("PrjCode") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
												<asp:HyperLink ID="hlinkShowName" CssClass="tooltip" Style="text-decoration: none;
													color: Black;" ToolTip='<%# System.Convert.ToString(Eval("PrjName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server">
													<span class="link" onclick="viewopen('/TenderManage/InfoView.aspx?&&ic=<%# Eval("PrjGuid") %>', '项目信息查看')">
													<%# StringUtility.GetStr(Eval("PrjName").ToString(), 25) %>
												   </span>
												</asp:HyperLink>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="权限人员"><ItemTemplate>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="角色名"><ItemTemplate>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="立项申请日期" HeaderStyle-Width="60px"><ItemTemplate>
												<%# Common2.GetTime(Eval("InputDate")) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申请部门"><ItemTemplate>
												<span title="<%# Eval("ProjPeopleDep") %>" class="tooltip">
													<%# StringUtility.GetStr(Eval("ProjPeopleDep").ToString(), 25) %>
												</span>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目属性" HeaderStyle-Width="60px"><ItemTemplate>
												<span title="<%# Eval("PropertyName") %>" class="tooltip">
													<%# StringUtility.GetStr(Eval("PropertyName").ToString(), 25) %>
												</span>
											</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
	<asp:HiddenField ID="hfldCheckedIds" runat="server" />
	</form>
</body>
</html>
