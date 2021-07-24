<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Privilege.aspx.cs" Inherits="ProgressManagement_Privilege_Privilege" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>进度计划权限</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<link href="../../Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="../../Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.core.js"></script>
	<script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.mouse.js"></script>
	<script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.draggable.js"></script>
	<script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.position.js"></script>
	<script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.resizable.js"></script>
	<script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.dialog.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
	<link rel="Stylesheet" type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var jwTable = new JustWinTable('gvwPlans');
			setWidthHeight();
			showTooltip("tooltip", 25);
			replaceEmptyTable('tblEmpty', 'gvwPlans');
			setButton(jwTable, 'btnLimit', 'btnLook', '', 'hfldChecked');
		});

		function setWidthHeight() {
			$('#divBudget').height($(this).height());
			$('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 7);
			$('#div_project').height($(this).height() - 20);
		}

		//更新权限人员
		function returnUser(id, name) {
			$('#hfldUserCodes').val(id);
			//执行权限分配
			$('#btnUpdateUserCodes').click();
		}

		//选择人员共享
		function selectUser() {
			var progressId = $('#hfldChecked').val();
			var url = "/Common/DivSelectAllUser.aspx?Method=returnUser&showSel=true&showType=3&progressId=" + progressId;
			document.getElementById("ifLimit").src = url;
			$('#btnUpdateUserCodes').appendTo($('#divLimit'));
			$('#divLimit').dialog({
				open: function () {
					$(this).parent().appendTo("form:first");
				},
				modal: true,
				title: '计划权限分配',
				width: 610,
				height: 485
			});
		}
		//查看进度
		function look() {
			var progressId = $('#' + $('#hfldChecked').val()).attr("strguid");
			var url = '/ProgressManagement/Modify/ApplyView.aspx?ic=' + progressId;
			var titleText = '查看进度计划';
			toolbox_oncommand(url, titleText);
		}

		function allocUser() {
			top.ui._progresspriv = window;
			var url = '/Common/AllocUser.aspx?type=progressPlanUser&idJson=' + $('#hfldChecked').val() + '&parentName=_progresspriv';
			top.ui.openWin({ title: '设置权限', url: url, width: 650, height: 500 });
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
											<input type="button" id="btnLimit" value="权限" onclick="allocUser()" disabled="disabled" />
											<input type="button" id="btnLook" value="查看进度" style="width: auto;" onclick="look()"
												disabled="disabled" />
										</td>
									</tr>
									<tr>
										<td style="vertical-align: top;" colspan="3">
											<div id="divBudget" class="pagediv" style="overflow: hidden;">
												<asp:GridView ID="gvwPlans" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwPlans_RowDataBound" DataKeyNames="ProgressVersionId,ProgressId" runat="server">
<EmptyDataTemplate>
														<table id="tblEmpty" class="gvdata">
															<tr class="header">
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
																	主计划
																</th>
																<th scope="col">
																	计划权限
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
																<asp:CheckBox ID="cbAllBox" runat="server" />
															</HeaderTemplate><ItemTemplate>
																<asp:CheckBox ID="cbBox" runat="server" />
															</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="序号" DataField="No" HeaderStyle-Width="20px" /><asp:TemplateField HeaderText="计划名称" HeaderStyle-Width="320px"><ItemTemplate>
																<a class="link tooltip" title='<%# Eval("VersionName") %>' onclick="look()">
																	<%# StringUtility.GetStr(Eval("VersionName"), 25) %></a>
															</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="版本号" DataField="VersionCode" /><asp:BoundField HeaderText="执行" DataField="Latest" HeaderStyle-Width="30px" /><asp:BoundField HeaderText="主计划" DataField="Main" HeaderStyle-Width="30px" /><asp:TemplateField HeaderText="计划权限"><ItemTemplate>
																<a class="tooltip" style="text-decoration: none; color: Black;" title='<%# Eval("Limits") %>'>
																	<%# StringUtility.GetStr(Eval("Limits").ToString(), 25) %>
																</a>
															</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="计划编制人" DataField="UserName" /><asp:BoundField HeaderText="计划编制日期" DataField="InputDate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
	<div id="divLimit" style="display: none;">
		<asp:Button ID="btnUpdateUserCodes" Style="display: none;" OnClick="btnUpdateUserCodes_Click" runat="server" />
		<asp:HiddenField ID="hfldUserCodes" runat="server" />
		<iframe id="ifLimit" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<asp:HiddenField ID="hfldChecked" runat="server" />
	</form>
</body>
</html>
