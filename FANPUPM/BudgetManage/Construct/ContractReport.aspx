<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="ContractReport.aspx.cs" Inherits="BudgetManage_Construct_ContractReport" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>中标预算施工报量</title>
	<style type="text/css">
		
	</style>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			//			// 解决第一个节点永远处于选中状态的问题
			//			$('.trvw_select').removeClass('trvw_select');

			//			// 项目点击事件
			//			$('#tvBudget a').click(function () {
			//				var prjId = $(this).attr('title');
			//				var url = 'ContractReportDetail.aspx?prjId=' + prjId;
			//				$('#ifrmReport').attr('src', url);
			//				return false;
			//			});

			//			// 默认选择第一个节点
			//			$('#tvBudget a[title]')[0].click();

			var prjId = jw.getRequestParam('prjId');
			var url = 'ContractReportDetail.aspx?prjId=' + prjId;
			$('#ifrmReport').attr('src', url);
		});
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
		<table style="width: 100%; height: 100%;">
			<tr>
				
				<td style="vertical-align: top; width: 100%; height: 100%;">
					<iframe id="ifrmReport" frameborder="0" width="100%" height="100%" scrolling="no" runat="server"></iframe>
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
