<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryReportList.aspx.cs" Inherits="StartStopReturnWork_QueryReportList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>查看报告</title><link href="../Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="../Script/jquery.js"></script>
	<script type="text/javascript" src="../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var gvReportList = new JustWinTable('gvReportList');
			replaceEmptyTable('emptyReportList', 'gvReportList');
			setButton(gvReportList, 'btnDel', 'btnEdit', 'btnQuery', 'hfldReportChecked');
			setWidthAndHeight();
			showTooltip('tooltip', 25);
			if (arr = document.cookie.match(/scrollTop=([^;]+)(;|$)/))
				document.getElementById('div_project').scrollTop = parseInt(arr[1]);
			clickTree('tvProject', 'hfldPrjId');
			gvReportList.registClickTrListener(function () {
				$('#hfldTypeId').val($(this).attr('typeId'));
			})
		});


		//设置div高度和宽度
		function setWidthAndHeight() {
			$('#divBudget').height($(this).height() - $('#divTop').height() - 57);
			$('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 8);
			$('#div_project').height($(this).height() - $('#ddlYear').height() - 4);
		}
		//以标签页形式弹出
		function Query(reportId, typeId) {
		    var url = GetUrl(reportId, typeId);
			
			toolbox_oncommand(url, "查看报告 ");
		}
		//弹出层弹出
		function openQuery() {
			var reportId = $('#hfldReportChecked').val();
			var typeId = $('#hfldTypeId').val();
			var url = GetUrl(reportId, typeId);
			viewopen(url, '查看报告 ');
		}
		function GetUrl(reportId, typeId) {
			if (typeId == '1') {
				return '/StartStopReturnWork/StartWorkReportView.aspx?prjId=' + $('#hfldPrjId').val() + '&prjState=&reportId=' + reportId + '&ic=' + reportId;
			} else if (typeId == '2') {
				return '/StartStopReturnWork/StopMsgView.aspx?stopMsgId=' + reportId + '&ic=' + reportId;
			} else if (typeId == '3') {
				return '/StartStopReturnWork/RetMsgView.aspx?retMsgId=' + reportId + '&ic=' + reportId;
			}

		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
		<table style="width: 100%; height: 100%; overflow: hidden;">
			<tr>
				<td style="width: 100%; height: 100%;">
					<table style="width: 100%; height: 100%;">
						<tr>
							<td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
								<div id="div1" class="divContent2" style="width: 100%; height: 100%;overflow:hidden">
									<table style="width: 100%; height: 88%;">
										<tr id="header">
											<td>
												<asp:Label ID="lblProject" runat="server"></asp:Label>
											</td>
										</tr>
										<tr>
											<td>
												<table class="queryTable" id="queryTable" cellpadding="3px" cellspacing="0px">
													<tr>
														<td class="descTd">
															报告状态
														</td>
														<td>
															<asp:DropDownList ID="dropReportType" Width="100%" runat="server"><asp:ListItem Value="" /><asp:ListItem Value="1" Text="开工报告" /><asp:ListItem Value="2" Text="停工通知单" /><asp:ListItem Value="3" Text="复工通知单" /></asp:DropDownList>
														</td>
														<td>
															<asp:Button ID="brnQuery" Text="查询" OnClick="brnQuery_Click" runat="server" />
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr id="divTop">
											<td class="divFooter" style="text-align: left; width: 100%;">
												<input type="button" id="btnQuery" value="查看" disabled="disabled" onclick="openQuery();" />
											</td>
										</tr>
										<tr>
											<td style="width: 100%; height: 100%; vertical-align: top">
												<div id="divBudget" class="pagediv" style="overflow:auto;">
													<asp:GridView ID="gvReportList" Width="100%" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvReportList_RowDataBound" DataKeyNames="ReportId,ReportTypeId" runat="server">
<EmptyDataTemplate>
															<table class="gvdata" cellspacing="0" id="emptyReportList" rules="all" border="1"
																style="width: 100%; border-collapse: collapse;">
																<tr>
																	<th class="header">
																		序号
																	</th>
																	<th class="header">
																		报告类型
																	</th>
																	<th class="header">
																		日期
																	</th>
																	<th class="header">
																		施工单位
																	</th>
																	<th class="header">
																		录入人
																	</th>
																	<th class="header">
																		录入日期
																	</th>
																	<th class="header">
																		流程状态
																	</th>
																</tr>
															</table>
														</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="25px">
<HeaderTemplate>
																	序号
																</HeaderTemplate>

<ItemTemplate>
																	<%# Container.DataItemIndex + 1 %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="100px"><HeaderTemplate>
																	报告类型
																</HeaderTemplate><ItemTemplate>
																	<span class="link" onclick="Query('<%# Eval("ReportId") %>','<%# Eval("ReportTypeId") %>');">
																		<%# Eval("ReportTypeName") %>
																	</span>
																</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="200px">
<HeaderTemplate>
																	日期
																</HeaderTemplate>

<ItemTemplate>
																	<%# Common2.GetTime(Eval("ReportDate")) %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="200px">
<HeaderTemplate>
																	施工单位
																</HeaderTemplate>

<ItemTemplate>
																	<span class="tooltip" title="<%# Eval("ConstUnit") %>">
																		<%# StringUtility.GetStr(Eval("ConstUnit").ToString(), 25) %>
																	</span>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="200px">
<HeaderTemplate>
																	录入人
																</HeaderTemplate>

<ItemTemplate>
																	<%# WebUtil.GetUserNames(Eval("InputUser").ToString()) %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="200px">
<HeaderTemplate>
																	录入日期
																</HeaderTemplate>

<ItemTemplate>
																	<%# Common2.GetTime(Eval("InputDate")) %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
																	流程状态
																</HeaderTemplate>

<ItemTemplate>
																	<%# Common2.GetState(Eval("FlowState").ToString()) %>
																</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
												</div>
											</td>
										</tr>
									</table>
								</div>
								<asp:HiddenField ID="hfldPrjId" runat="server" />
								<asp:HiddenField ID="hfldPrjState" runat="server" />
								<asp:HiddenField ID="hfldReportChecked" runat="server" />
								<asp:HiddenField ID="hfldTypeId" runat="server" />
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
