<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActualReport.aspx.cs" Inherits="ProgressManagement_Actual_Report" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>实际进度上报</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<link rel="Stylesheet" type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var jwTable = new JustWinTable('gvwPlans');
			setWidthHeight();
			showTooltip("tooltip", 25);
			setButton(jwTable, 'btnEmpty', 'btnEmpty', 'btnEmpty', 'hfldChecked');
			replaceEmptyTable('tblEmpty', 'gvwPlans');
			//首次默认第一个计划
			if ($('#gvwPlans')[0].rows.length > 1) {
				$('#gvwPlans')[0].rows[1].click();
			}
		});

		//设置高度和宽度
		function setWidthHeight() {
			$('#divBudget').height($(this).height() - 255);
			$('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 7);
			$('#div_project').height($(this).height() - 20);
			$('#divIfReports').width($('#divContent').width() - $('#td_Left').width() - 7);
			$('#divIfReports').height($(this).height() - $('#divBudget').height() - 5);
		}

		//查看进度计划
		function lookInfo(progressId) {
			var url = '/ProgressManagement/Modify/ApplyView.aspx?ic=' + progressId;
			var titleText = '查看进度计划';
			toolbox_oncommand(url, titleText);
		}

		//点击行
		function clickTr(id) {
			$('#ifReports').attr('src', 'Reports.aspx?' + new Date().getTime() + '&id=' + id);
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
									<tr>
										<td style="vertical-align: top;" colspan="3">
											<div id="divBudget" class="pagediv" style="overflow: hidden;">
												<asp:GridView ID="gvwPlans" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwPlans_RowDataBound" DataKeyNames="ProgressVersionId" runat="server">
<EmptyDataTemplate>
														<table id="tblEmpty">
															<tr class="header">
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
<Columns><asp:BoundField HeaderText="序号" DataField="No" HeaderStyle-Width="20px" /><asp:TemplateField HeaderText="计划名称" HeaderStyle-Width="320px"><ItemTemplate>
																<a class="link tooltip" title='<%# Eval("VersionName") %>' onclick="lookInfo('<%# Eval("ProgressVersionId") %>')">
																	<%# StringUtility.GetStr(Eval("VersionName"), 25) %></a>
															</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="版本号" DataField="VersionCode" /><asp:BoundField HeaderText="主计划" DataField="Main" HeaderStyle-Width="30px" /><asp:BoundField HeaderText="计划编制人" DataField="UserName" /><asp:BoundField HeaderText="计划编制日期" HeaderStyle-Width="80px" DataField="InputDate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
												<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
												</webdiyer:AspNetPager>
											</div>
										</td>
									</tr>
									<tr>
										<td>
											<div id="divIfReports">
												<iframe id="ifReports" src="Reports.aspx" frameborder="0" style="width: 100%; height: 100%;
													border-top: solid #CADEED 2px; overflow: auto;"></iframe>
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
	<input type="button" id="btnEmpty" style="display: none;" />
	</form>
</body>
</html>
