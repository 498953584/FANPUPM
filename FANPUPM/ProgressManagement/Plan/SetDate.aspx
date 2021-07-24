<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetDate.aspx.cs" Inherits="ProgressManagement_Plan_SetDate" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>设置时间</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$('#btnSave')[0].onclick = controlDate;
		});

		//管理时间
		function controlDate() {
			var startDates = document.getElementById('txtStartDate').value;
			var startDateList = startDates.split('-');
			var startDate = new Date(startDateList[0], startDateList[1] - 1, startDateList[2]);
			var endDates = document.getElementById('txtEndDate').value;

			if (startDates != "" && endDates != "") {
				var endDatesList = endDates.split('-');
				var endDate = new Date(endDatesList[0], endDatesList[1] - 1, endDatesList[2]);
				if (endDate - startDate <= 0) {
					top.ui.show('结束时间必须大于开始时间！请重新选择时间！');
					return false;
				}
			}
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="dvSetPrjDate" style="height: 220px; width: 100%;">
		<table style="text-align: center; width: 90%; height: 75px; vertical-align: top;">
			<tr style="">
				<td>
					开始时间
				</td>
				<td class="elemTd mustInput" align="left">
					<asp:TextBox ID="txtStartDate" Width="180px" CssClass="{required:true, messages:{required:'开始时间必须输入'}}" onclick="WdatePicker()" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td>
					结束时间
				</td>
				<td class="elemTd mustInput" align="left">
					<asp:TextBox ID="txtEndDate" Width="180px" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'结束时间必须输入'}}" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
			</tr>
		</table>
		<div style="background-color: Blue; bottom: 10px; right: 10px; position: absolute;">
			<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
			<input type="button" value="取消" onclick="top.ui.closeWin();" />
		</div>
	</div>
	<asp:HiddenField ID="hfldProgressVerId" runat="server" />
	</form>
</body>
</html>
