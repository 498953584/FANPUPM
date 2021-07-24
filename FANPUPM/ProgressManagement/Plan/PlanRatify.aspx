<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanRatify.aspx.cs" Inherits="ProgressManagement_Plan_PlanRatify" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>计划审核</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<link rel="Stylesheet" type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var jwTable = new JustWinTable('gvwPlans');
			replaceEmptyTable('emptyTable', 'gvwPlans');
			setButton(jwTable, 'hfldChecked');
			setWidthHeight();
			showTooltip('tooltip', 25);
		});

		function setWidthHeight() {
			$('#divBudget').height($(this).height());
			$('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 7);
			$('#div_project').height($(this).height() - 20);
		}

		// 审核
		function audit() {
			var ic = $('#hfldChecked').val();
			$('#' + ic).find('td:eq(1)').find('a').click();
		}

		// 审核记录
		function procedure() {
			var ic = $('#hfldChecked').val();
			var url = $('#' + ic).attr('procedureURL');
			toolbox_oncommand(url, '审核记录');
		}

		// 流程状态
		function flowState() {
			var ic = $('#hfldChecked').val();
			var url = $('#' + ic).attr('flowStateURL');
			toolbox_oncommand(url, '查看流程状态');
		}

		// 查看进度
		function look() {
			var progressId = $('#hfldChecked').val();
			var url = '/ProgressManagement/Plan/PlanView.aspx?ic=' + progressId;
			var titleText = '查看进度计划';
			toolbox_oncommand(url, titleText);
		}

		// 复选框选择
		function setButton(jwTable, hdChkId) {
			if (!jwTable.table) return;
			if (jwTable.table.getElementsByTagName('TR').length == 1) return;

			jwTable.registClickTrListener(function () {
				if (hdChkId != '') {
					$('#hfldChecked').val(this.id);
				}
				// 判断是否可以审核
				if ($('#' + this.id).find('td:eq(7)').find('span').attr('state') == '0')
					$('#btnAudit').removeAttr('disabled');
				else
					$('#btnAudit').attr('disabled', 'disabled');

				// 启用所有按钮
				$('#btnLook').removeAttr('disabled');
				$('#btnProcedure').removeAttr('disabled');
				$('#btnFlowState').removeAttr('disabled');
			});
		};
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
								<table cellpadding="3" cellspacing="3">
									<tr>
										<td>
											计划名称
										</td>
										<td>
											<asp:TextBox ID="txtVerName" runat="server"></asp:TextBox>
										</td>
										<td>
											版本号
										</td>
										<td>
											<asp:TextBox ID="txtVerCode" runat="server"></asp:TextBox>
										</td>
									</tr>
									<tr>
										<td>
											发起人
										</td>
										<td>
											<asp:TextBox ID="txtOrgName" runat="server"></asp:TextBox>
										</td>
										<td>
											审核状态
										</td>
										<td>
											<asp:DropDownList ID="dropFlow" runat="server"><asp:ListItem Text="所有" Value="" Selected="true" /><asp:ListItem Text="待审核" Value="0" /><asp:ListItem Text="已审核" Value="1" /></asp:DropDownList>
										</td>
										<td>
											<asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
										</td>
									</tr>
								</table>
								<table class="tab">
									<tr class="divFooter" style="text-align: left;">
										<td>
											<input type="button" id="btnAudit" value="审核" disabled="disabled" onclick="audit()" />
											<input type="button" id="btnProcedure" value="审核记录" style="width: auto;" disabled="disabled"
												onclick="procedure()" />
											<input type="button" id="btnFlowState" value="流程状态" style="width: auto;" disabled="disabled"
												onclick="flowState()" />
											<input type="button" id="btnLook" value="查看进度" style="width: auto;" disabled="disabled"
												onclick="look()" />
										</td>
									</tr>
									<tr>
										<td style="vertical-align: top;" colspan="3">
											<div id="divBudget" class="pagediv" style="overflow: hidden;">
												<asp:GridView ID="gvwPlans" AutoGenerateColumns="false" Width="100%" CssClass="gvdata" AllowPaging="false" OnRowDataBound="gvwPlans_RowDataBound" DataKeyNames="NoteID" runat="server">
<EmptyDataTemplate>
														<table id="emptyTable">
															<tr class="header">
																<th scope="col" width="20">
																	序号
																</th>
																<th scope="col">
																	计划名称
																</th>
																<th scope="col">
																	版本号
																</th>
																<th scope="col">
																	主计划
																</th>
																<th scope="col">
																	发起人
																</th>
																<th scope="col">
																	参与审核人
																</th>
																<th scope="col">
																	提交时间
																</th>
																<th scope="col">
																	流程状态
																</th>
															</tr>
														</table>
													</EmptyDataTemplate>
<Columns><asp:BoundField HeaderText="序号" DataField="No" HeaderStyle-Width="20px" /><asp:ButtonField HeaderText="计划名称" DataTextField="VersionName" /><asp:BoundField HeaderText="版本号" DataField="VersionCode" /><asp:BoundField HeaderText="主计划" HeaderStyle-Width="30px" DataField="Main" /><asp:BoundField HeaderText="发起人" DataField="OrganigerName" /><asp:BoundField HeaderText="参与审核人" /><asp:BoundField DataField="ArriveTime" HeaderText="提交时间" HeaderStyle-Width="100px" /><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
																<%# Common2.GetState(Eval("FlowState").ToString()) %>
															</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><RowStyle CssClass="rowb"></RowStyle><HeaderStyle CssClass="header" Font-Bold="false"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
	</form>
</body>
</html>
