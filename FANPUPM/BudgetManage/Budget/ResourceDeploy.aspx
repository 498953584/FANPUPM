<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceDeploy.aspx.cs" Inherits="BudgetManage_Budget_ResourceDeploy" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>资源配置</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/jquery.treetable.js"></script>
	<script type="text/javascript" src="../../Script/jquery.blockUI.js"></script>
	<script type="text/javascript" src="../../Script/jquery-budgetpait.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript">
	    $(document).ready(function () {
			$('#gvBudget').treetable(true, 'DisplayResource');
			setWidthHeight();
			$('#gvBudget').delegate(':checkbox:gt(0)', 'click', checkSingle);
			$('#gvBudget').delegate(':checkbox:eq(0)', 'click', checkAll);
			$('#gvBudget').delegate('tr:gt(0)', 'click', clickRow);
			setControlButton('hfldCheckedIds', '', '', '', '', '', '', 'btnResDeploy', 'Resource');
			setHiddenId('', '', 'hfldUserCode', 'hfldIsLocked');
			addEvent(document.getElementById('btnResDeploy'), 'click', add);

			//无任务节点不显示汇总信息
			if ($('#gvBudget').get(0) != undefined) {
				var tb = document.getElementById('gvBudget');
				if (tb != null) {
					showCostAccounting();
				}
			}
		});
		function setWidthHeight() {
			$('#divBudget').height($(this).height() - $('#trBudgetButtons').height() -60);
			$('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 7);

		}
		//配置资源
		function add() {
		    top.ui._ResourceDeploy = window;
			var url = "/BudgetManage/Budget/ResourceList.aspx?taskId=" + document.getElementById("hfldCheckedIds").value + "&prjId=" + document.getElementById("hfldPrjId").value + "&year=" + document.getElementById("hfldYear").value;
			toolbox_oncommand(url, "资源清单");

		}
		//显示汇总信息
		function showCostAccounting() {
			var tab_Resource = null;
			$.ajax({
				type: 'GET',
				async: false,
				url: '/BudgetManage/Handler/ReturnConResource.ashx?' + new Date().getTime() + '&type=budtask&prjId=' + $('#hfldPrjId').val() + '&taskId=' + document.getElementById("hfldCheckedIds").value + '&version=' + $('#hfldVersion').val() + '&prjType=',
				success: function (data) {
				}
			});
			$('#statisticsTable').html(tab_Resource);
		}
		function clickRow(e) {
			checkSingle();
			//alert(this.id);
			$('#gvBudget').find(':checkbox').change();
			$('#gvBudget').find(':checkbox').attr('checked', false);
			//revertTable('#gvBudget');
			this.className = 'select_row';
			$(this).find(':checkbox').attr('checked', true);
			//keepTargetState();
			// 取消全选
			$('#gvBudget').find(':checkbox:eq(0)').attr('checked', false);
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
	<div id="divContent" class="divContent2" data-options="region:'center',title:''" style="overflow:hidden;width:100%;">
		<table style="width: 100%; height: 100%; vertical-align: top;" border="0" class="tab">
			<tr id="trBudgetButtons">
				<td class="divFooter" style="text-align: left;">
					<input type="button" style="width: 80px;" id="btnResDeploy" disabled="true" value="资源配置" runat="server" />

				</td>
				<td class="divFooter" style="text-align: left;">
					<asp:Label ID="lblPrjName" Font-Bold="true" Text="" runat="server"></asp:Label>
				</td>
				<td class="divFooter">
				</td>
			</tr>
			<tr>
				<td style="vertical-align: top;" colspan="3">
					<div id="divBudget" class="pagediv" style="overflow: auto; vertical-align: top; margin: 0px;">
						<asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="TaskId,SubCount,OrderNumber,Modified" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
										<asp:CheckBox ID="cbAllBox" runat="server" />
									</HeaderTemplate>

<ItemTemplate>
										<asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Eval("TaskId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
										<%# Eval("No") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="清单编码"><ItemTemplate>
										<span style="vertical-align: middle;">
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
										<%# System.Convert.ToDecimal(Eval("Total")).ToString() %>
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
	<div data-options="region:'south',title:'',split:true" style="height: 60px; overflow: auto;">
		<table style="width: 100%;" border="0">
			<tr>
				<td id="statisticsTable">
				</td>
			</tr>
		</table>
	</div>
	
	
	<asp:HiddenField ID="hfldCheckedIds" runat="server" />
	
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	
	<asp:HiddenField ID="hfldYear" runat="server" />
	
	<asp:HiddenField ID="hfldIsLocked" runat="server" />
	
	<asp:HiddenField ID="hfldVersion" runat="server" />
	
	<asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
	</form>
</body>
</html>
