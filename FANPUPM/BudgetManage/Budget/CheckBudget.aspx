<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckBudget.aspx.cs" Inherits="BudgetManage_Budget_checkBudget" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>资源配置</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/jquery.treetable.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../Script/jquery.blockUI.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$('#gvBudget').treetable(false, 'ColorRed');
			//无任务节点不显示汇总信息
			if ($('#gvBudget').get(0) != undefined) {
				var tb = document.getElementById('gvBudget');
				if (tb != null) {
					showCostAccounting();
				}
			}
			setWidthHeight();
			if ($('#hfldIsWBSRelevance').val() == '1') {
				$('#btnRes').hide();
			} else {
				$('#btnRes').show();
				$('#btnRes').click(showResList);
			}
		});
		//显示汇总信息
		function showCostAccounting() {
			var tab_Resource = null;
			$.ajax({
				type: 'GET',
				async: false,
				url: '/BudgetManage/Handler/ReturnConResource.ashx?' + new Date().getTime() + '&type=budtask&prjId=' + $('#hfldPrjId').val() + '&version=1&prjType=',
				success: function (data) {
					tab_Resource = data;
				}
			});
			$('#statisticsTable').html(tab_Resource);
		}
		function setWidthHeight() {
			$('#divBudget').height($(this).height() - $('#trBudgetButtons').height() -60);
			$('#div_project').height($(this).height() - $('#ddlYear').height() - 4);
			$('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 7);
		}
		//添加行进行显示资源信息
		var prevId;
		function showInfo(id) {
			var tab_Resource = null;
			$.ajax({
				type: 'GET',
				async: false,
				url: '/BudgetManage/Handler/ReturnResource.ashx?' + new Date().getTime() + '&taskId=' + id + '&type=check',
				success: function (data) {
					tab_Resource = data;
				}
			});
			var isDisplay = $('#' + id + '1').get(0);
			if (isDisplay == undefined) {
				$('#' + id).after('<tr id="' + id + '1"><td align="center" colspan="13" style="border:solid 1px #CADEED;">' + tab_Resource + '</td></tr>');
				if (prevId != undefined && prevId != id) {
					$('#' + prevId + '1').remove();
				}
				prevId = id;

			}
			else {
				$('#' + id + '1').remove();
				isDisplay = undefined;
			}
		}

		//资源列表
		function showResList() {
			var url = '/BudgetManage/Budget/ResourceList.aspx?' + new Date().getTime() + '&prjId=' + $('#hfldPrjId').val() + '&version=1&year=' 
					+ $('#hfldYear').val() + '&isAllowEditRes=0&pageURl=check';
			toolbox_oncommand(url, "查看资源");
		}    
	</script>
</head>
<body class="easyui-layout">
	<form id="form1" runat="server">
	<div class="divHeader" style="display: none;">
		<table class="tableHeader">
			<tr>
				<td>
					<asp:Label ID="lblTitle" Font-Bold="true" Text="预算编制" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<div id="divContent" class="divContent2" data-options="region:'center',title:''" style="overflow:hidden;">
		<table class="tab">
			<tr id="trBudgetButtons">
				<td class="divFooter" style="text-align: left;">
					<asp:DropDownList ID="dropTaskType" AutoPostBack="true" OnSelectedIndexChanged="dropTaskType_SelectedIndexChanged" runat="server"><asp:ListItem Value="" Text="总预算" /><asp:ListItem Value="Y" Text="年度预算" /><asp:ListItem Value="M" Text="月度预算" /></asp:DropDownList>
					<asp:DropDownList ID="dropYear" AutoPostBack="true" OnSelectedIndexChanged="dropYear_SelectedIndexChanged" runat="server"><asp:ListItem Value="" Text="选择年份" /></asp:DropDownList>
					<asp:DropDownList ID="dropMonth" AutoPostBack="true" OnSelectedIndexChanged="dropMonth_SelectedIndexChanged" runat="server"><asp:ListItem Value="" Text="选择月份" /></asp:DropDownList>
					<asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
					<asp:Button ID="btnRes" Text="查看资源" Style="width: 80px" runat="server" />
				</td>
				<td class="divFooter">
				</td>
				<td class="divFooter">
				</td>
			</tr>
			<tr>
				<td style="vertical-align: top;" colspan="3">
					<div id="divBudget" class="pagediv" style="overflow:auto;">
						<asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvBudget_RowDataBound" DataKeyNames="TaskId,SubCount,OrderNumber,Modified" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
										<%# Eval("No") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="清单编码"><ItemTemplate>
										<span style="vertical-align: middle; margin-right: 25px;">
											<%# Eval("TaskCode") %>
										</span>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
										<span style="vertical-align: middle;">
											<%# Eval("TaskName") %>
										</span>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目特征描述" HeaderStyle-Width="100px"><ItemTemplate>
										<span class="tooltip" title='<%# Eval("FeatureDescription").ToString() %>'>
											<%# StringUtility.GetStr(Eval("FeatureDescription"), 30) %>
										</span>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型"><ItemTemplate>
										<%# Eval("TypeName") %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位"><ItemTemplate>
										<%# Eval("unit") %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程量" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
										<%# Eval("Quantity") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="综合单价" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
										<%# System.Convert.ToDecimal(Eval("UnitPrice")).ToString() %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合价" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
										<%# System.Convert.ToDecimal(Eval("Total2")).ToString() %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="开始时间"><ItemTemplate>
										<%# Common2.GetTime(Eval("StartDate")) %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结束时间"><ItemTemplate>
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
	<asp:HiddenField ID="hfldVersion" runat="server" />
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	
	<asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
	</form>
</body>
</html>
