<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConstructReport.aspx.cs" Inherits="BudgetManage_Construct_ConstructReport" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>施工报量</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script src="/Script/jquery.js" type="text/javascript"></script>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 若该项目的预算没有审核通过，则不能进行施工报量
			if ($('#hfldBudFlowState').val() == '1') {
				var gvConstruct = new JustWinTable('gvConstruct');
				setButton(gvConstruct, 'btnDel', 'btnEdit', 'btnQuery', 'hfldPurchaseChecked');
				setWfButtonState2(gvConstruct, 'WF1');
			} else {
				$('#btnAdd').attr('disabled', true);
				top.ui.show('该项目的预算没有审核通过，不能进行施工报量。');
			}
			setWidthAndHeight();
			showTooltip('tooltip', 25);
			clickTree('tvBudget', 'hfldPrjId');
		});

		// 设置div高度和宽度
		function setWidthAndHeight() {
			$('#divBudget').height($(this).height() - $('#divTop').height() - 2);
			$('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 8);
			$('#div_project').height($(this).height() - $('#ddlYear').height() - 4);
		}

		function add() {
			top.ui._ConstructTask = window;
			var url = '/BudgetManage/Construct/ConstructTask.aspx?type=add&prjId=' + document.getElementById("hfldPrjId").value + '&year=' + document.getElementById("hfldYear").value;
			toolbox_oncommand(url, "新增施工报量");
		}
		function edit() {
			top.ui._ConstructTask = window;
			var url = '/BudgetManage/Construct/ConstructTask.aspx?type=edit&prjId=' + document.getElementById("hfldPrjId").value + '&conId=' + document.getElementById("hfldPurchaseChecked").value + '&year=' + document.getElementById("hfldYear").value;
			toolbox_oncommand(url, "编辑施工报量");
		}
		function Query() {
			var url = '/BudgetManage/Construct/QueryConstructTask.aspx?type=edit&prjId=' + document.getElementById("hfldPrjId").value + '&conId=' + document.getElementById("hfldPurchaseChecked").value + '&year=' + document.getElementById("hfldYear").value;
			toolbox_oncommand(url, "查看施工报量");
		}

		//添加行进行显示资源信息
		var prevId;
		function showInfo(id, state) {
			var tab_Resource = null;
			$.ajax({
				type: 'GET',
				async: false,
				url: '/BudgetManage/Handler/ReturnConsRep.ashx?' + new Date().getTime() + '&consId=' + id,
				success: function (data) {
					tab_Resource = data;
				}
			});
			var isDisplay = $('#' + id + '1').get(0);
			if (isDisplay == undefined) {
				$('#' + id).after('<tr id="' + id + '1"><td align="center" colspan="12" style="border:solid 1px #CADEED;">' + tab_Resource + '</td></tr>');
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
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
		<table style="width: 100%; height: 88%;">
			<tr id="divTop">
				<td class="divFooter" style="text-align: left; width: 100%;">
					<input type="button" id="btnAdd" value="新增" onclick="add();" />
					<input type="button" id="btnEdit" value="编辑" disabled="disabled" onclick="edit();" />
					<asp:Button ID="btnDel" Text="删除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗?')" OnClick="btnDelete_Click" runat="server" />
					<input type="button" id="btnQuery" value="查看" disabled="disabled" onclick="Query();" />
					<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="123" BusiClass="001" runat="server" />
				</td>
			</tr>
			<tr>
				<td style="width: 100%; height: 100%; vertical-align: top">
					<div id="divBudget" class="pagediv" style=" overflow:auto;">
						<asp:GridView ID="gvConstruct" Width="100%" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvConstruct_RowDataBound" DataKeyNames="id,state,ProjectId" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
										<asp:CheckBox ID="cbAllBox" runat="server" />
									</HeaderTemplate>

<ItemTemplate>
										<asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Eval("Id"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="50px">
<HeaderTemplate>
										序号
									</HeaderTemplate>

<ItemTemplate>
										<%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="200px">
<HeaderTemplate>
										上报日期
									</HeaderTemplate>

<ItemTemplate>
										<%# System.Convert.ToDateTime(Eval("InputDate")).ToString("yyyy-MM-dd") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="200px">
<HeaderTemplate>
										记录人
									</HeaderTemplate>

<ItemTemplate>
										<%# Eval("InputUser") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
										安全工作纪录
									</HeaderTemplate>

<ItemTemplate>
										<asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(StringUtility.StripTagsCharArray(Eval("WorkCard").ToString()), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(StringUtility.StripTagsCharArray(Eval("WorkCard").ToString()), 25) %></asp:HyperLink>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
										流程状态
									</HeaderTemplate>

<ItemTemplate>
										<%# Common2.GetState(Eval("FlowState").ToString()) %>
									</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
						<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
						</webdiyer:AspNetPager>
					</div>
				</td>
			</tr>
		</table>
		<asp:HiddenField ID="hfldPrjId" runat="server" />
		<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
		<asp:HiddenField ID="hfldYear" runat="server" />
		<asp:HiddenField ID="hfldBudFlowState" runat="server" />
	</div>
	</form>
</body>
</html>
