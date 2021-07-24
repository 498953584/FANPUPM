<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanDetail.aspx.cs" Inherits="ProgressManagement_Plan_PlanDetail" %>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>计划编制WBS分解</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			setWidthHeight();
			setIfFrameSrc();
			if ($('#setDate').css("display") != "none")
				Val.validate('form1', 'btnSave');
		});

		function setWidthHeight() {
			$('#divBudget').height($(this).height());
			$('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 7);
			$('#div_project').height($(this).height() - 20);
		}

		function setIfFrameSrc() {

			var prjId = getRequestParam('prjId');
			var isLoadGantt = false;
			var start;
			var end;

			if (prjId != '') {
				$.ajax({
					type: "GET",
					async: false,
					url: "/ProgressManage/Handler/GetStarEndDate.ashx?" + new Date().getTime() + "&prjId=" + prjId,
					dataType: "json",
					success: function (data) {
						if (data == "0") {
							alert("系统提示：\n\n项目不存在，请刷新");
							return;
						}
						else {
							start = data[0].StartDate;
							end = data[0].EndDate;
							isLoadGantt = compareDate(start, end);
						}
					}
				});

			}
			if (prjId == "") {
				$('#noProject').css("display", "");
				$('#setDate').css("display", "none");
			}
			else {
				if (!isLoadGantt) {
					$('#setDate').css("display", "");
					$('#noProject').css("display", "none");
					$('#txtStartDate').val(start);
					$('#txtEndDate').val(end);
				}
			}

			if (isLoadGantt) {
				var progressVerId = getRequestParam('verId');
				loadGantt(progressVerId);
			}
		}

		//加载甘特图页面
		function loadGantt(progressVerId) {
			var url = "../../ProgressManagement/Gantt/PlanDetail.htm?&id=" + progressVerId;
			$('#ifPlusGantt').attr("src", url);

		}

		//比较项目的开始时间和结束时间,当开始时间<结束时间返回true
		function compareDate(start, end) {
			var isAllow = true;

			if (start == null || end == null || start == "" || end == "") {
				isAllow = false;
			}
			else {

				var startArray = start.split("-");
				var endArray = end.split("-");
				var startTime = new Date(startArray[0], startArray[1] - 1, startArray[2]);
				var endTime = new Date(endArray[0], endArray[1] - 1, endArray[2]);

				if (startTime >= endTime) {
					isAllow = false;
				}
			}
			return isAllow;
		}



		//设置时间
		function setPrjDate() {
			var url = '/ProgressManagement/Plan/SetDate.aspx?prjId=' + jw.getRequestParam('prjId');
			top.ui._plandetail = window;
			top.ui.openWin({ title: '设置时间', url: url, width: 400, height: 270 });
		}


	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divBudget" class="pagediv" style="overflow: hidden;">
		<span id="noProject" style="color: Red; display: none;">没有项目可以加载</span> <span id="setDate"
			style="color: Red; display: none;">项目的结束时间必须大于开始时间，请 <a onclick="setPrjDate()" class="link">
				设置时间</a> </span>
		<iframe id="ifPlusGantt" style="width: 100%; height: 100%;" frameborder="0" scrolling="auto">
		</iframe>
	</div>
	<asp:HiddenField ID="hfldProgressVerId" runat="server" />
	</form>
</body>
</html>
