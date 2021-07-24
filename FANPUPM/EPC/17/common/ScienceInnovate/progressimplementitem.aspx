<%@ Page Language="C#" AutoEventWireup="true" CodeFile="progressimplementitem.aspx.cs" Inherits="ProgressImplementItem" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>ProgressImplementItem</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/ecmascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" language="javascript">
		$(document).ready(function () {
			var purchasePlanTable = new JustWinTable('dgList');
			setButton(purchasePlanTable, 'btnDel', 'btnEdit', 'btnAppraise', 'hfldPurchaseChecked');
			//如果只有一页的数据,隐藏页码
			hideFirstPageNo();
		});

		function ClickRow(obj, tbName, MainId) {
			document.getElementById("hidMainId").value = MainId;
		}
		function CheckId() {
			if (document.getElementById("hidPlanId").value == "") {
				top.ui.alert("请选择计划记录！");
				return false;
			}
			else
				if (document.getElementById("hidMainId").value == "") {
					top.ui.alert("请选择实施记录！");
					return false;
				}
				else {
					return true;
				}
		}
		function doEdit(target) {
			var url = "/EPC/17/ppm/ScienceInnovate/ProgressImplementReport.aspx";
			if (target == "new") {
				if (document.getElementById("hidPlanId").value != "")
					url += "?planId=" + document.getElementById("hidPlanId").value;
				else {
					top.ui.alert("请选择计划记录!");
					return false;
				}
			}
			else if (CheckId()) {
				url += "?planId=" + document.getElementById("hidPlanId").value;
				url += "&MainId=" + document.getElementById("hidMainId").value;
			}
			else {
				return false;
			}
			//			
			top.ui._progressimplementitem = window;
			toolbox_oncommand(url, "实施情况")
		}
		function doDel() {
			if (CheckId()) {
				if (window.confirm("确定删除:包括该实施情况对应的评论记录?"))
					return true;
				else
					return false;
			}
			else {
				return false;
			}
		}
		function doAppraise() {
			var url = "/EPC/17/entpm/ScienceInnovate/ProgressImplementEvaluate.aspx";
			if (CheckId()) {
				url += "?MainId=" + document.getElementById("hidMainId").value;
			}
			else {
				return false;
			}
			top.ui._progressimplementitem = window;
			toolbox_oncommand(url, "实施评价")
		}
		function doShow() {
			var url = "/EPC/17/common/ScienceInnovate/ProgressImplementEvaluateAll.aspx";
			if (CheckId()) {
				url += "?MainId=" + document.getElementById("hidMainId").value;
			}
			else {
				return false;
			}
			top.ui._progressimplementitem = window;
			toolbox_oncommand(url, "实施评价")
		}
		
	</script>
	<style type="text/css">
		.dgheader
		{
			background-color: #EEF2F5;
			white-space: nowrap;
			overflow: hidden;
			font-weight: normal;
			text-align: center;
			border-color: #CADEED;
			color: #727FAA;
			padding-left: 6px;
			padding-right: 6px;
		}
	</style>
</head>
<body>
	<form id="Form1" method="post" runat="server">
	<table id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%">
		<tr class="divFooter" style="text-align: left">
			<td>
				<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
				<input id="hidPlanId" type="hidden" name="Hidden1" runat="server" />

				<input id="hidMainId" type="hidden" name="Hidden1" runat="server" />

				<asp:Button ID="btnNew" Text="新 增" OnClick="btnNew_Click" runat="server" />&nbsp;
				<asp:Button ID="btnEdit" Text="编  辑" OnClick="btnEdit_Click" runat="server" />
				<asp:Button ID="btnAppraise" Text="评  价" OnClick="btnAppraise_Click" runat="server" />&nbsp;
				<asp:Button ID="btnDel" Text="删  除" OnClick="btnDel_Click" runat="server" />&nbsp;
				<input type="button" style="width: 70px" value="公司评价" onclick="doShow();" />
			</td>
		</tr>
		<tr height="98%">
			<td valign="top">
				<div style="overflow: auto; width: 100%; height: 100%">
					<asp:DataGrid ID="dgList" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" DataKeyField="MainId" AllowPaging="true" PageSize="10" OnPageIndexChanged="dgList_PageIndexChanged" runat="server"><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><ItemStyle CssClass="rowa"></ItemStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle><Columns><asp:BoundColumn DataField="FillPeople" HeaderText="填报人"></asp:BoundColumn><asp:BoundColumn DataField="FillTime" HeaderText="填报日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="ExaminePeople" HeaderText="审核人"></asp:BoundColumn><asp:BoundColumn DataField="ExamineTime" HeaderText="审核日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="ActualizeCircs" HeaderText="实施情况"></asp:BoundColumn><asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid></div>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	</form>
</body>
</html>
