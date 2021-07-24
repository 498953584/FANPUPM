<%@ Page Language="C#" AutoEventWireup="true" CodeFile="progressplanlist.aspx.cs" Inherits="ProgressPlanList_" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>ProgressPlanList </title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/ecmascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var purchasePlanTable = new JustWinTable('dgList');
			//如果只有一页的数据,隐藏页码
			hideFirstPageNo();
		});

		function DisplayBtn() {
			document.getElementById("btnShow").style = "none";
		}

		function ClickRow(obj, tbName, planId, isAbled) {
			//doClick(obj,tbName);
			document.getElementById("hidPlanId").value = planId;
			if (isAbled != "") {
				if (document.getElementById("btnEdit") != null)
					document.getElementById("btnEdit").disabled = true;

				if (document.getElementById("btnDel") != null)
					document.getElementById("btnDel").disabled = true;

				if (document.getElementById("btnAudit") != null)
					document.getElementById("btnAudit").disabled = true;

				//if(document.getElementById("btnPromiss") != null)
				//	document.getElementById("btnPromiss").disabled = true;
			}
			else {
				if (document.getElementById("btnEdit") != null)
					document.getElementById("btnEdit").disabled = false;

				if (document.getElementById("btnDel") != null)
					document.getElementById("btnDel").disabled = false;

				if (document.getElementById("btnAudit") != null)
					document.getElementById("btnAudit").disabled = false;

				//if(document.getElementById("btnPromiss") != null)
				//	document.getElementById("btnPromiss").disabled = false;
			}
		}
		function doEdit(target) {
			var url = "/EPC/17/Ppm/ScienceInnovate/ProgressPlanOneEdit.aspx";
			var title = "";
			if (target == "Edit") {
				title = "编辑进步计划"
				if (document.getElementById("hidPlanId").value == "") {
					top.ui.alert("请选择计划！");
					return false;
				}
				else {
					url += "?prjCode=" + document.getElementById("hidPrjCode").value;
					url += "&planId=" + document.getElementById("hidPlanId").value;
				}
			}
			else {
				title = "新增进步计划";
				url += "?prjCode=" + document.getElementById("hidPrjCode").value;
			}
			top.ui._progressplanlist = window;
			toolbox_oncommand(url, title)
		}
		function dodoAudit() {
			var url = "/EPC/17/Entpm/ScienceInnovate/ProgressPlanPermiss.aspx";
			if (document.getElementById("hidPlanId").value == "") {
				top.ui.alert("请选择计划！");
				return false;
			}
			else {
				url += "?prjCode=" + document.getElementById("hidPrjCode").value;
				url += "&planId=" + document.getElementById("hidPlanId").value;
			}
			top.ui._progressplanlist = window;
			toolbox_oncommand(url, "项目部审核")
			//return window.showModalDialog(url,window,"dialogHeight:400px;dialogWidth:400px;center:1;help:0;status:0;");
		}
		function doDel() {
			if (document.getElementById("hidPlanId").value == "") {
				top.ui.alert("请选择计划！");
				return false;
			}
			else {
				if (window.confirm("确定删除?")) {
					return true;
				}
				else {
					return false;
				}
			}
		}
		function doPrjAudit() {
			var url = "/EPC/17/ppm/ScienceInnovate/ProgressPlanPermiss.aspx";
			if (document.getElementById("hidPlanId").value == "") {
				top.ui.alert("请选择计划！");
				return false;
			}
			else {
				url += "?planId=" + document.getElementById("hidPlanId").value;
				top.ui._progressplanlist = window;
				toolbox_oncommand(url, "项目部审核")
			}
		}
		function doShowPrjAudit() {
			var url = "/EPC/17/ppm/ScienceInnovate/ProgressPlanPermiss.aspx";
			if (document.getElementById("hidPlanId").value == "") {
				top.ui.alert("请选择计划！");
				return false;
			}
			else {
				url += "?planId=" + document.getElementById("hidPlanId").value;
				url += "&target=view";
				top.ui._progressplanlist = window;
				toolbox_oncommand(url, "项目部审核")
			}
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
	<font face="宋体">
		<table id="Table1" cellspacing="0" cellpadding="0" width="100%">
			
			<tr>
				<td class="divFooter" style="text-align: left">
					<input id="hidPrjCode" type="hidden" runat="server" />
<input id="hidPlanId" type="hidden" runat="server" />

					<asp:Button ID="btnNew" Text="新 增" OnClick="btnNew_Click" runat="server" />
					<asp:Button ID="btnEdit" Text="编  辑" disabled="disabled" OnClick="btnEdit_Click" runat="server" /><asp:Button ID="btnPromiss" Text="审  批" CssClass="button-normal" OnClick="btnPromiss_Click" runat="server" />
					<asp:Button ID="btnDel" Text="删  除" disabled="disabled" OnClick="btnDel_Click" runat="server" />
					<input id="btnShow" style="width: 120px;" onclick="doShowPrjAudit()" type="button"
						value="项目部审核情况" />
					<asp:Button ID="btnAudit" Text="审  核" OnClick="btnAudit_Click" runat="server" />
				</td>
			</tr>
			<tr>
				<td>
					<asp:DataGrid ID="dgList" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" DataKeyField="PlanId" AllowPaging="true" PageSize="25" OnPageIndexChanged="dgList_PageIndexChanged" runat="server"><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><ItemStyle CssClass="rowa"></ItemStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle><Columns><asp:TemplateColumn HeaderText="编号"><ItemTemplate>
									<asp:Label Text='<%# Convert.ToString(Eval("PlanCode")) %>' runat="server"></asp:Label>
									<input type="hidden" id="hidAuditState" value='<%# Convert.ToString(Eval("AuditState")) %>' runat="server" />

								</ItemTemplate></asp:TemplateColumn><asp:BoundColumn DataField="PrjName" HeaderText="填报单位"></asp:BoundColumn><asp:BoundColumn DataField="ResultsName" HeaderText="成果名称"></asp:BoundColumn><asp:BoundColumn DataField="ResultsHolders" HeaderText="执行成果单位/人"></asp:BoundColumn><asp:BoundColumn DataField="ApplicationName" HeaderText="应用工程名称"></asp:BoundColumn><asp:BoundColumn DataField="PlanSort" HeaderText="类别"></asp:BoundColumn><asp:BoundColumn DataField="CompletedDate" HeaderText="完成时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="strAuditState" HeaderText="流程状态"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid>
					<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
				</td>
			</tr>
		</table>
		<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	</font>
	</form>
</body>
</html>
