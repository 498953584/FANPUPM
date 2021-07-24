<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayoutCalcList.aspx.cs" Inherits="ContractManage_PayoutPayment_PayoutCalcList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>支出合同计量</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var type = jw.getRequestParam('type');
			if (type == 'view') {
				$('#divTop').hide();
			}
			var tab = new JustWinTable('gvConstruct');
			setButton(tab, 'btnDel', 'btnEdit', 'btnQuery', 'hfldRptId');
			setWfButtonState2(tab, 'WF1');
		});

		// 添加合同计量
		function addPayoutCalc() {
			top.ui._ConstructTask = window;
			var url = '/BudgetManage/Construct/ConstructTask.aspx?type=add'
				+ '&prjId=' + $('#hfldPrjId').val()
				+ '&contractId=' + jw.getRequestParam('ContractID');
			top.ui.openTab({ title: '新增合同计量', url: url });
		}

		// 编辑合同计量
		function editPayoutCalc() {
			top.ui._ConstructTask = window;
			var url = '/BudgetManage/Construct/ConstructTask.aspx?type=edit'
				+ '&prjId=' + $('#hfldPrjId').val()
				+ '&contractId=' + jw.getRequestParam('ContractID')
				+ '&conId=' + $('#hfldRptId').val();

			top.ui.openTab({ title: '编辑合同计量', url: url });
		}

		// 查看施工报量
		function viewPayoutCalc() {
			var url = '/BudgetManage/Construct/QueryConstructTask.aspx?type=edit'
				+ '&prjId=' + $('#hfldPrjId').val()
				+ '&contractId=' + jw.getRequestParam('ContractID')
				+ '&conId=' + $('#hfldRptId').val();
			top.ui.openTab({ title: '查看合同计量', url: url });
		}

	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
		<table style="width: 100%; height: 88%;">
			<tr id="divTop">
				<td class="divFooter" style="text-align: left; width: 100%;">
					<input type="button" id="btnAdd" value="新增" onclick="addPayoutCalc();" />
					<input type="button" id="btnEdit" value="编辑" onclick="editPayoutCalc();"disabled="disabled" />
					
					
					
					<asp:Button ID="btnDel" Text="删除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗?')" OnClick="btnDelete_Click" runat="server" />
					<input type="button" id="btnQuery" value="查看" disabled="disabled" onclick="viewPayoutCalc();" />
					<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="123" BusiClass="001" runat="server" />
				</td>
			</tr>
			<tr>
				<td style="width: 100%; height: 100%; vertical-align: top">
					<div id="divBudget" class="pagediv" style="overflow: auto;">
						<asp:GridView ID="gvConstruct" Width="100%" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvConstruct_RowDataBound" DataKeyNames="ConsReportId,State,PrjId" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
										<asp:CheckBox ID="cbAllBox" runat="server" />
									</HeaderTemplate>

<ItemTemplate>
										<asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("ConsReportId")) %>' runat="server" />
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="25px">
<HeaderTemplate>
										序号
									</HeaderTemplate>

<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="200px">
<HeaderTemplate>
										上报日期
									</HeaderTemplate>

<ItemTemplate>
										<%# Convert.ToDateTime(Eval("InputDate")).ToString("yyyy-MM-dd") %>
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
										<asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# Convert.ToString(StringUtility.StripTagsCharArray(Eval("WorkCard").ToString())) %>' runat="server"><%# StringUtility.GetStr(StringUtility.StripTagsCharArray(Eval("WorkCard").ToString()), 25) %></asp:HyperLink>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
										流程状态
									</HeaderTemplate>

<ItemTemplate>
										<%# Common2.GetState(Eval("FlowState").ToString()) %>
									</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
						
					</div>
				</td>
			</tr>
		</table>
		<asp:HiddenField ID="hfldYear" runat="server" />
		<asp:HiddenField ID="hfldBudFlowState" runat="server" />
	</div>
	<!-- 所选择的计量Id-->
	<asp:HiddenField ID="hfldRptId" runat="server" />
	<!-- 项目Id-->
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	</form>
</body>
</html>
