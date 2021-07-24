<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractTaskQuery.aspx.cs" Inherits="BudgetManage_Budget_ContractTaskQuery" EnableEventValidation="false" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>合同预算查询</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			setWidthHeight();
			showMoreNote();
			//			clickTree('tvBudget', 'hfldPrjId');


			$('#dropTaskType').change(typeChangeEvent);
			$('#dropYear').change(yearChangeEvent);
			$('#dropMonth').change(monthChangeEvent);

			// 解决第一个节点永远处于选中状态的问题
			//			$('.trvw_select').removeClass('trvw_select');

			//			$('#tvBudget a').click(function () {
			//				var prjId = $(this).attr('title');
			//				var url = 'ContractTaskQuery2.aspx?prjId=' + prjId;
			//				$('#ifrmConTask').attr('src', url);

			//				// 绑定年份
			//				bindYear(prjId);
			//				// 保存项目ID
			//				$('#hfldPrjId').val(prjId)
			//				return false;
			//			});

			//			$('#tvBudget a[title]')[0].click();

			var prjId = jw.getRequestParam('prjId');
			var url = 'ContractTaskQuery2.aspx?prjId=' + prjId;
			$('#ifrmConTask').attr('src', url);
			$('#hfldPrjId').val(prjId)
			bindYear(prjId);
		});

		// 备注信息提示
		function showMoreNote() {
			var tab = document.getElementById('gvBudget');
			if (tab != null) {
				var noteIndex = 11;
				for (i = 1; i < tab.rows.length; i++) {
					var cells = tab.rows[i].cells;
					if (cells.length == 1) return;
					var imgId = cells[noteIndex].children[0].id;
					var altLength = document.getElementById(imgId).title.length;
					if (altLength > 15) {
						$('#' + imgId).css("display", "");
						$('#' + imgId).tooltip({
							track: true,
							delay: 0,
							showURL: false,
							fade: true,
							showBody: " - ",
							extraClass: "solid 1px blue",
							fixPNG: true,
							left: -200
						});
					} else {
						document.getElementById(imgId).title = '';
					}
				}
			}
		}
		function setWidthHeight() {
			$('#ifrmConTask').width($('#divContent').width() - 10);
			$('#ifrmConTask').height($(this).height() - 30);
		}


		// 绑定年份
		function bindYear(prjId) {
			var url = '../../BudContractTaskService.svc/GetYears/' + prjId;
			$.ajax({
				url: url,
				contenttype: 'application/json',
				success: function (data) {
					$('#dropYear option:gt(0)').remove();
					for (var i in data) {
						var option = jw.format('<option value="{0}">{0}年</option>', data[i]);
						$('#dropYear').append(option);
					}
				} //,
				//error: function (ex) { alert('err'); }
			});
		}

		// 选择预算类型
		function typeChangeEvent() {
			var prjId = $('#hfldPrjId').val();
			var type = $('#dropTaskType option:selected').val();
			var year = $('#dropYear option:selected').val();
			var month = $('#dropMonth option:selected').val();
			showTask(prjId, type, year, month);

			if (type == '') {
				// 还原年份和月份
				$('#dropYear').val('');
				$('#dropMonth').val('');
			}
		}

		// 选择年份
		function yearChangeEvent() {
			var prjId = $('#hfldPrjId').val();
			var type = $('#dropTaskType option:selected').val();
			var year = $('#dropYear option:selected').val();
			var month = $('#dropMonth option:selected').val();
			if (!year) return;
			$('#dropMonth option:gt(0)').remove();
			var url = '../../BudContractTaskService.svc/GetMonths/' + prjId + '/' + year;
			$.ajax({
				url: url,
				contenttype: 'application/json',
				success: function (data) {
					for (var i in data) {
						var option = jw.format('<option value="{0}">{0}月</option>', data[i]);
						$('#dropMonth').append(option);
					}
					showTask(prjId, type, year, month)
				} //,
				//error: function (ex) { alert('err'); }
			});
		}

		// 选择月份
		function monthChangeEvent() {
			var prjId = $('#hfldPrjId').val();
			var type = $('#dropTaskType option:selected').val();
			var year = $('#dropYear option:selected').val();
			var month = $('#dropMonth option:selected').val();
			showTask(prjId, type, year, month)
		}

		// 显示预算
		function showTask(prjId, type, year, month) {
			if (!prjId) return;
			var url = 'ContractTaskQuery2.aspx?prjId=' + prjId;
			if (type == 'Y' && year != '') {
				// 年度预算
				url += '&type=Y&year=' + year;
			} else if (type == 'M' && year != '' && month != '') {
				// 月度预算
				url += '&type=M&year=' + year + '&month=' + month;
			}
			$('#ifrmConTask').attr('src', url);
		}
	</script>
	<style type="text/css">
		.descTd
		{
			text-align: right;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%;">
		<table style="width: 100%;">
			<tr>
				<td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 1px #CADEED;">
					<table>
						<tr>
							<td>
								<div class="divHeader" style="text-align: left;">
									<asp:DropDownList ID="dropTaskType" runat="server"><asp:ListItem Value="" Text="总预算" /><asp:ListItem Value="Y" Text="年度预算" /><asp:ListItem Value="M" Text="月度预算" /></asp:DropDownList>
									<asp:DropDownList ID="dropYear" runat="server"><asp:ListItem Value="" Text="选择年份" /></asp:DropDownList>
									<asp:DropDownList ID="dropMonth" runat="server"><asp:ListItem Value="" Text="选择月份" /></asp:DropDownList>
								</div>
							</td>
						</tr>
						<tr>
							<td style="vertical-align: top;">
								<iframe id="ifrmConTask" name="ifrmConTask" frameborder="0" style="height: 100%;" scrolling="yes" runat="server"></iframe>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		<asp:HiddenField ID="hfldPrjId" runat="server" />
	</div>
	</form>
</body>
</html>
