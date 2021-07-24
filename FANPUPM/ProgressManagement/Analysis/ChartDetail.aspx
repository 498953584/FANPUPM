<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChartDetail.aspx.cs" Inherits="ProgressManagement_Analysis_ChartDetail" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>进度柱状图明细</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<link rel="Stylesheet" type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			setWidthHeight();
		});

		function setWidthHeight() {
			$('#div_project').height($(this).height() - 20);
			$('#ifChartDetail').height($(this).height());
			$('#ifChartDetail').width($('#divContent').width() - $('#td_Left').width() - 7);
		}

		function setIframe(prjName) {
			var src = "/ProgressManagement/Analysis/AnalysisDetail.aspx?region=" + encodeURIComponent(prjName);
			$('#ifChartDetail').attr('src', src);
		}
	</script>
	<style type="text/css">
		.style1
		{
			height: 34px;
		}
	</style>
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
								<iframe id="ifChartDetail" scrolling="auto" frameborder="0" src=""></iframe>
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
