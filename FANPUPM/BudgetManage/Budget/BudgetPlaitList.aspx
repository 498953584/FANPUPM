<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudgetPlaitList.aspx.cs" Inherits="BudgetManage_Budget_BudgetPlaitList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>WBS分解</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/jquery.easyui.extension.css" />
<link Href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../Script/jquery-budgetpait.js"></script>
	<script type="text/javascript" src="/Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript" src="../../Script/HideButtons.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$('#gvBudget').treetable(true, 'BudTask');
			if ($('#hfldIsWBSRelevance').val() == '1') {
				$('#btnTemImport').show();
				$('#btnSaveTemplate').show();
				$('#btnShowRes').hide();
				$('#linkADownLoad').attr('href', '../../Download/目标成本模板.xls');
			} else {
				$('#btnTemImport').hide();
				$('#btnSaveTemplate').hide();
				$('#btnShowRes').show();
				$('#linkADownLoad').attr('href', '../../Download/NewWBSTemplate/目标成本模板.xls');
			}

			$('#gvBudget').delegate(':checkbox:gt(0)', 'click', checkSingle);
			$('#gvBudget').delegate(':checkbox:eq(0)', 'click', checkAll);
			CheckPrjects();
			setControlButton('hfldCheckedIds', 'btnAdd', 'btnEdit', 'btnDel', 'btnExcelImport', 'btnTemImport', 'btnSaveTemplate', undefined, undefined, 'btnMoveUp', 'btnMoveDown');
			setHiddenId('hfldCurrentVersion', 'hfldPrjId', 'hfldInputUser', 'hfldIsLocked', 'hfldEditOrAdd');
			//无任务节点不显示汇总信息
			if ($('#gvBudget').get(0) != undefined) {
				var tb = document.getElementById('gvBudget');
				if (tb != null) {
					showCostAccounting();
				}
			}


			setWFButtonState($('#hfldTaskChangeId').val(), $('#hfldPrjId').val(), $('#hfldFlowstate').val(), true);
			if ($('#hfldIsLocked').val() == 'False') {
				if ($('#hfldFlowstate').val() == '-1' || $('#hfldFlowstate').val() == '-3') {
					$('#btnAdd').removeAttr('disabled');
					$('#btnExcelImport').removeAttr('disabled');
					$('#ddlPriceType').removeAttr('disabled');
				} else if ($('#hfldFlowstate').val() == '') {
					$('#btnAdd').removeAttr('disabled');
				} else {
					$('#btnAdd').attr('disabled', 'disabled');
					$('#btnExcelImport').attr('disabled', 'disabled');
					$('#ddlPriceType').attr('disabled', 'disabled');
				}
			} else {
				$('#btnAdd').attr('disabled', 'disabled');
				$('#btnExcelImport').attr('disabled', 'disabled');
				$('#ddlPriceType').attr('disabled', 'disabled');
				$('#btnStartWF').attr('disabled', 'disabled');
				$('#CancelBt').attr('disabled', 'disabled');
				if ($('#hfldIsAllowWithdraw').val() == "True") {
					$('#CancelBt').removeAttr('disabled');
				}
			}
			//当没有数据时，禁用“提交审核”“编辑”“删除”按钮
			if ($('#gvBudget').length == 0) {
				$('#btnStartWF').attr('disabled', 'disabled');
				$('#btnEdit').attr('disabled', 'disabled');
				$('#btnDel').attr('disabled', 'disabled');
			}
			//隐藏按钮
			hideButtons('td_buttons');
			if ($('#hfldPrjId').val() == '') {
				$('#btnAdd').attr('disabled', 'disabled');
				$('#btnExcelImport').attr('disabled', 'disabled');
			}
		});

		//Excel导入
		function excelImport() {
			var isaffirm = true;
			var taskId = $("#hfldCheckedIds").val();
			if (taskId != '') {
				if (isHaveResource(taskId) == 1) {
					alert("系统提示：\n此节点已经配置资源，无法在此节点下进行Excel导入！");
					return;
				}
			}
			var count = $('#gvBudget').get(0) == undefined ? 0 : 1;

			if (taskId == '' && count != '0') {
				if ($('#hfldIsWBSRelevance').val() == '1') {
					isaffirm = confirm("系统提示：\n没有选择导入Excel的根节点，将会覆盖当前版本！\n是否继续导入Excel？");
				} else {
					isaffirm = confirm("系统提示：\n没有选择导入Excel的根节点，将会覆盖当前版本的分部分项和资源！\n是否继续导入Excel？");
				}
			}
			if (isaffirm) {
				var selectYear = jw.getRequestParam('year');
				var prjid = $("#hfldPrjId").val();
				var url = "/BudgetManage/Budget/ExcelImport.aspx?taskId=" + taskId + "&year=" + selectYear + "&prjId=" + prjid + "&version=1";
				top.ui._BudgetPlaitList = window;
				toolbox_oncommand(url, "Excel导入");
			}
		}
		//保存为模板
		function saveTem() {
			top.ui._BudgetPlaitListSave = window;
			var url = "/BudgetManage/Template/SelectTemplate.aspx";
			toolbox_oncommand(url, "保存为模板");
		}
		//模板导入
		function temImport() {
			var isaffirm = true;
			var count = $('#gvBudget').get(0) == undefined ? 0 : 1;
			var taskId = $("#hfldCheckedIds").val();
			if (taskId != '') {
				if (isHaveResource(taskId) == 1) {
					alert("系统提示：\n此节点已经配置资源，无法在此节点下进行模板导入！");
					return;
				}
			}
			if (taskId == '' && count != '0') {
				isaffirm = confirm("系统提示：\n没有选择导入模板的根节点，将会覆盖当前版本！\n是否继续导入模板？");
			}
			if (isaffirm) {
				top.ui._BudgetPlaitList = window;
				var selectYear = jw.getRequestParam('year');
				var prjid = $("#hfldPrjId").val();
				var url = "/BudgetManage/Template/TemplateImport.aspx?taskId=" + taskId + "&year=" + selectYear + "&prjId=" + prjid + "&version=1";
				toolbox_oncommand(url, "模板导入");
			}
		}
		//获取TR
		function getTrByChk(chk) {
			if (chk.parentNode.parentNode.nodeName == 'TR') {
				return chk.parentNode.parentNode;
			}
			if (chk.parentNode.parentNode.parentNode.nodeName == 'TR') {
				return chk.parentNode.parentNode.parentNode;
			}
			return null;
		}

		//检索是否存在项目
		function CheckPrjects() {
			var prjCount = $('#hfldProjectCount').val();
			if (prjCount != '') {
				if (parseInt(prjCount) == 0) {
					$('#btnAdd').attr('disabled', 'disabled');
					$('#btnExcelImport').attr('disabled', 'disabled');
					$('#btnTemImport').attr('disabled', 'disabled');
				} else {
					if ($('#hfldIsLocked').val() == 'False') {
						$('#btnAdd').removeAttr('disabled');
						$('#btnExcelImport').removeAttr('disabled');
						$('#btnTemImport').removeAttr('disabled');
					}
				}
			}
		}

		//显示汇总信息
		function showCostAccounting() {
			var tab_Resource = null;
			$.ajax({
				type: 'GET',
				async: false,
				url: '/BudgetManage/Handler/ReturnConResource.ashx?' + new Date().getTime() + '&type=budtask&prjId=' +
                $('#hfldPrjId').val() + '&version=1&priceType=' + $('#hfldPriceType').val(),
				success: function (data) {
					tab_Resource = data;
				}
			});
			$('#statisticsTable').html(tab_Resource);
		}
		//资源列表
		function showResList() {
			var isAllowEditRes = '0';
			if ($('#hfldIsLocked').val() == 'False' && ($('#hfldFlowstate').val() == '-1' || $('#hfldFlowstate').val() == '-3')) {
				isAllowEditRes = '1';
			}
			var year = jw.getRequestParam('year');
			top.ui._ResourceList = window;
			var url = '/BudgetManage/Budget/ResourceList.aspx?' + new Date().getTime() + '&prjId=' + $('#hfldPrjId').val() + '&version=1&year='
					+ year + '&isAllowEditRes=' + isAllowEditRes;
			toolbox_oncommand(url, "资源列表");
		}
	</script>
	<style type="text/css">
		.descTd
		{
			text-align: right;
		}
		#btnAdd
		{
			width: 43px;
		}
		#btnEdit
		{
			width: 43px;
		}
		#btnMoveUp
		{
			width: 43px;
		}
		#btnMoveDown
		{
			width: 43px;
		}
	</style>
</head>
<body class="easyui-layout">
	<form id="form1" runat="server">
	<div id="divContent2" class="divContent2" data-options="region:'center',title:''">
		<table style="width: 100%; height: 100%; vertical-align: top;" border="0" class="tab">
			<tr id="tr_Buttons">
				<td colspan="3" class="divFooter" style="text-align: left;" id="td_buttons">
					<input type="button" id="btnAdd" value="新增" disabled="true" runat="server" />

					<input type="button" id="btnEdit" value="编辑" disabled="disabled" />
					<asp:Button ID="btnGenerage" Text="自动生成" Width="80px" ToolTip="根据主项目的预算自动生成子项目的预算" CssClass="tooltip" Visible="false" OnClick="btnGenerage_Click" runat="server" />
					<asp:Button ID="btnDel" Text="删除" disabled="disabled" OnClientClick="return confirm('确定要删除吗？')" Width="43px" OnClick="btnDel_Click" runat="server" />
					<input type="button" id="btnMoveUp" value="上移" disabled="disabled" />
					<input type="button" id="btnMoveDown" value="下移" disabled="disabled" />
					<asp:DropDownList ID="ddlPriceType" AutoPostBack="true" ToolTip="单价类型" OnSelectedIndexChanged="ddlPriceType_SelectedIndexChanged" runat="server"><asp:ListItem Selected="true" Value="0" Text="--请选择--" /></asp:DropDownList>
					<input type="button" id="btnExcelImport" value="Excel导入" onclick="excelImport()" style="width: 70px" runat="server" />

					<input type="button" id="btnTemImport" value="模板导入" onclick="temImport()" style="width: 65px;" disabled="true" runat="server" />

					<asp:Button ID="btnSaveTemplate" Text="保存为模板" Enabled="false" Width="75px" OnClick="btnSaveTemplate_Click" runat="server" />
					<input type="button" id="btnShowRes" value="资源列表" onclick="showResList()" style="width: 70px" runat="server" />

					<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="109" BusiClass="001" runat="server" />
					<asp:Label ID="lblLockPrjMessage" Font-Bold="true" Text="(未锁定!)" runat="server"></asp:Label>
					<a id="linkADownLoad" class="link" style="margin-top: 5px; margin-right: 5px; white-space: nowrap;"
						href="../../Download/目标成本模板.xls">目标成本模板</a>
				</td>
			</tr>
			<tr>
				<td colspan="3" style="vertical-align: top;">
					<div id="divBudget" class="pagediv" style="overflow: hidden; vertical-align: top;
						margin: 0px;">
						<asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvBudget_RowDataBound" DataKeyNames="TaskId,SubCount,OrderNumber" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
										<asp:CheckBox ID="cbAllBox" runat="server" />
									</HeaderTemplate><ItemTemplate>
										<asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Eval("TaskId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
										<%# Eval("No") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="清单编码"><ItemTemplate>
										<%# Eval("TaskCode") %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
										<span style="vertical-align: middle;">
											<%# Eval("TaskName") %>
										</span>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目特征描述" HeaderStyle-Width="100px"><ItemTemplate>
										<span class="tooltip" title='<%# Eval("FeatureDescription").ToString() %>'>
											<%# StringUtility.GetStr(Eval("FeatureDescription"), 30) %>
										</span>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型" HeaderStyle-Width="50px"><ItemTemplate>
										<%# Eval("TypeName") %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px"><ItemTemplate>
										<%# Eval("Unit") %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程量" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
										<%# Eval("Quantity") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="综合单价" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
										<%# System.Convert.ToDecimal(Eval("UnitPrice")).ToString() %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合价" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
										<%# System.Convert.ToDecimal(Eval("Total2")).ToString() %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="50px"><ItemTemplate>
										<%# Common2.GetTime(Eval("StartDate")) %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="50px"><ItemTemplate>
										<%# Common2.GetTime(Eval("EndDate")) %>
									</ItemTemplate></asp:TemplateField><asp:BoundField DataField="ConstructionPeriod" HeaderText="工期(天)" HeaderStyle-Width="50px" /><asp:TemplateField HeaderText="备注"><ItemTemplate>
										<asp:HyperLink ID="hlinkShowNote" Style="text-decoration: none; color: Black;" ToolTip='<%# System.Convert.ToString(Eval("Note").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(Eval("Note").ToString()) %></asp:HyperLink>
									</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					</div>
				</td>
			</tr>
		</table>
	</div>
	<div data-options="region:'south',title:'',split:true" style="height: 60px;">
		<table style="width: 100%;" border="0">
			<tr>
				<td id="statisticsTable">
				</td>
			</tr>
		</table>
	</div>
	<div id="editTask" title="新增/编辑节点" style="display: none">
		<iframe id="editTaskFrame" frameborder="0" width="100%" height="100%"></iframe>
	</div>
	
	<asp:HiddenField ID="hfldCheckedIds" runat="server" />
	
	<asp:HiddenField ID="hfldIsLocked" runat="server" />
	
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	
	<asp:HiddenField ID="hfldCurrentVersion" runat="server" />
	
	<asp:HiddenField ID="hfldProjectCount" runat="server" />
	
	<asp:HiddenField ID="hfldEditOrAdd" runat="server" />
	
	<asp:HiddenField ID="hfldInputUser" runat="server" />
	
	<asp:HiddenField ID="hfldFlowstate" runat="server" />
	
	<asp:HiddenField ID="hfldTaskChangeId" runat="server" />
	
	<asp:HiddenField ID="hfldIsAllowWithdraw" Value="False" runat="server" />
	
	<asp:HiddenField ID="hfldPriceType" runat="server" />
	
	<asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
	</form>
	<script type="text/javascript" src="../../Script/jquery.treetable.js"></script>
	<script type="text/javascript" src="../../Script/jquery.blockUI.js"></script>
</body>
</html>
