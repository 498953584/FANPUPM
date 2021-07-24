<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="StartWorkReportList.aspx.cs" Inherits="StartStopReturnWork_StartWorkReportList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>开工报告</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../Script/jquery.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/ecmascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../Script/wf.js"></script>
	<script type="text/javascript" src="../Script/jw.js"></script>
	<script type="text/javascript" src="../Script/jwJson.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			replaceEmptyTable('emptyProject', 'gvwPrjInfo');
			var table = new JustWinTable('gvwPrjInfo');
			formateTable('gvwPrjInfo', 3);
			setButton2(table, 'hfldCheckedIds');
			setWfButtonState2(table, 'WF1');
			showTooltip('tooltip', 25);
			$('#BtnWFDel').hide();
			$('#txtStartTime').blur(function () {
				controlDate(this);
			})
			$('#txtEndTime').blur(function () {
				controlDate(this);
			})
		});

		//设置按钮
		function setButton2(jwTable, hdChkId) {
			if (!jwTable.table) return;
			if (jwTable.table.getElementsByTagName('TR').length == 1) {
				//table中没有数据
				return;
			}
			jwTable.registClickTrListener(function () {
				var prjState = $(this).attr('prjState');
				var primit = $(this).attr('primit');
				var flowState = $(this).attr('flowState');
				$('#hfldCheckedIds').val(this.id);
				$('#hfldStartReportId').val($(this).attr('reportId'));
				$('#hfldPrjState').val(prjState);
				if (primit != "0" && (prjState == "5" || prjState == "17")) {
					if (flowState == "" || flowState == -1 || flowState == -3) {
						$('#btnStartWork').removeAttr('disabled');
					} else {
						$('#btnStartWork').attr('disabled', 'disabled');
						$('#btnStartWF').attr('disabled', 'disabled');
					}
					if (flowState == "") {
						$('#btnStartWF').attr('disabled', 'disabled');
						$('#CancelBt').attr('disabled', 'disabled');
						$('#WF1_btnViewWF').attr('disabled', 'disabled');
						$('#WF1_btnWFPrint').attr('disabled', 'disabled');
					}
				} else if (primit != "0" && flowState == "1") {
					$('#btnStartWork').attr('disabled', 'disabled');
					$('#btnStartWF').attr('disabled', 'disabled');
					$('#CancelBt').attr('disabled', 'disabled');
					$('#WF1_btnViewWF').removeAttr('disabled');
					$('#WF1_btnWFPrint').removeAttr('disabled');
				} else {
					$('#btnStartWork').attr('disabled', 'disabled');
					$('#btnStartWF').attr('disabled', 'disabled');
					$('#CancelBt').attr('disabled', 'disabled');
					$('#WF1_btnViewWF').attr('disabled', 'disabled');
					$('#WF1_btnWFPrint').attr('disabled', 'disabled');
				}
				$('#btnQuery').removeAttr('disabled');
				var userCode = getCookie('UserCode');
				//管理员可以编辑审核通过的开工报告
				if (userCode == '00000000') {
					$('#btnStartWork').removeAttr('disabled');
				}
			});
		}

		//选择人员
		function selectUser(id, name) {
			jw.selectOneUser({ nameinput: name, codeinput: id });
		}

		//编辑开工报告
		function EidtStartWorkReport() {
			parent.desktop.StartWorkReportEdit = window;
			var id = $('#hfldCheckedIds').val();
			var prjState = $('#hfldPrjState').val();
			var reportId = $('#hfldStartReportId').val();
			var url = "/StartStopReturnWork/StartWorkReportEdit.aspx?prjId=" + id + "&prjState=" + prjState + "&reportId=" + reportId;
			toolbox_oncommand(url, "编辑开工报告");
		}
		//查看开工报告
		function openQuery(prjId, prjState, reportId) {
			if (prjId == undefined) {
				prjId = $('#hfldCheckedIds').val();
				prjState = $('#hfldPrjState').val();
				reportId = $('#hfldStartReportId').val();
			}
			if (reportId == "") {
				reportId = prjId;
			}
			viewopen('/StartStopReturnWork/StartWorkReportView.aspx?prjId=' + prjId + '&prjState=' + prjState + '&reportId=' + reportId + '&ic=' + reportId, '查看开工报告');
		}
		//标签页查看
		function Query(prjId, prjState, reportId) {
			parent.desktop.StartWorkReportView = window;
			if (reportId == "") {
				reportId = prjId;
			}
			var url = '/StartStopReturnWork/StartWorkReportView.aspx?prjId=' + prjId + '&prjState=' + prjState + '&reportId=' + reportId + '&ic=' + reportId;
			toolbox_oncommand(url, "查看开工报告");
		}
		//管理日期
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
	</script>
	<style type="text/css">
		#queryTable tr td
		{
			white-space: nowrap;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<table width="100%" cellpadding="0" cellspacing="0">
		<tr>
			<td>
				<table class="queryTable" id="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							项目编号
						</td>
						<td>
							<asp:TextBox ID="txtPrjCode" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							项目名称
						</td>
						<td>
							<asp:TextBox ID="txtPrjName" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							项目经理
						</td>
						<td>
							<span class="spanSelect" style="width: 122px;">
								<asp:TextBox ID="txtPrjManager" Style="width: 97px; height: 15px;
									border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
								<img src="../../images/icon.bmp" style="float: right; height: 18px;" alt="选择" id="img2"
									onclick="selectUser('hfldPrjManager','txtPrjManager');" />
							</span>
							<input id="hfldPrjManager" type="hidden" style="width: 1px" runat="server" />

						</td>
						<td class="descTd">
							流程状态
						</td>
						<td>
							<asp:DropDownList ID="dropWFState" Width="100px" runat="server"></asp:DropDownList>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							立项申请日期
						</td>
						<td>
							<asp:TextBox ID="txtStartTime" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							至
						</td>
						<td>
							<asp:TextBox ID="txtEndTime" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							项目状态
						</td>
						<td>
							<asp:DropDownList ID="dropPrjState" Width="100%" runat="server"></asp:DropDownList>
						</td>
						<td colspan="2">
							<asp:Button ID="brnQuery" Text="查询" OnClick="brnQuery_Click" runat="server" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td class="divFooter" style="text-align: left; padding-left: 2px;">
				<input type="button" id="btnStartWork" value="开工报告" disabled="disabled" onclick="EidtStartWorkReport();"
					style="width: auto;" />
				<input type="button" id="btnQuery" value="查看" disabled="disabled" onclick="openQuery()" />
				<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="126" BusiClass="001" runat="server" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:GridView ID="gvwPrjInfo" CssClass="gvdata" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="gvwPrjInfo_RowDataBound" DataKeyNames="PrjGuid,TypeCode,Primit,PrjState,FlowState,ReportId" runat="server">
<EmptyDataTemplate>
						<table class="gvdata" cellspacing="0" id="emptyProject" rules="all" border="1" style="width: 100%;
							border-collapse: collapse;">
							<tr>
								<th class="header">
									序号
								</th>
								<th class="header">
									项目状态
								</th>
								<th class="header">
									项目编号
								</th>
								<th class="header">
									项目名称
								</th>
								<th class="header">
									项目经理
								</th>
								<th class="header">
									流程状态
								</th>
								<th class="header">
									立项申请日期
								</th>
							</tr>
						</table>
					</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
								<%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
							</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="项目状态" DataField="ItemName" HeaderStyle-Width="150px" /><asp:BoundField DataField="PrjCode" HeaderText="项目编号" HeaderStyle-Width="150px" /><asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="300px"><ItemTemplate>
								<span class="tooltip link" title="<%# Eval("PrjName") %>" onclick="Query('<%# Eval("PrjGuid") %>','<%# Eval("PrjState") %>','<%# Eval("ReportId") %>');">
									<%# StringUtility.GetStr(Eval("PrjName").ToString(), 25) %>
								</span>
							</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="项目经理" DataField="person" /><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
								<%# Common2.GetStateNullString(Eval("FlowState").ToString()) %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="立项申请日期" HeaderStyle-Width="80px"><ItemTemplate>
								<%# Common2.GetTime(Eval("InputDate")) %>
							</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
				</webdiyer:AspNetPager>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hfldCheckedIds" runat="server" />
	<asp:HiddenField ID="hfldStartReportId" runat="server" />
	<asp:HiddenField ID="hfldPrjState" runat="server" />
	<link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/default/easyui.css" />
	<link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/icon.css" />
	</form>
</body>
</html>
