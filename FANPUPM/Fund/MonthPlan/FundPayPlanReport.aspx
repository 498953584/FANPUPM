<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FundPayPlanReport.aspx.cs" Inherits="Fund_MonthPlan_FundPayPlanReport" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_querydatectrl_ascx" Src="~/UserControl/QueryDateCtrl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>支出合同结算报表</title><link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script src="../../Web_Client/TreeNew.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var ReturnTable = new JustWinTable('gvPayoutPlan');
		});

		//选择合同
		function selectContract() {
			jw.selectOutCon({ idinput: 'hfldContract', nameinput: 'txtContract' })
		}
		//选择项目
		function openProjPicker() {
			selectOneProject({ nameinput: 'txtProject' });
		}
		// 单选项目 
		function selectOneProject(option) {
			option = option || {};
			var url = '/Common/DivSelectProject2.aspx';
			var winNo = option.winNo || 1;
			var _callback = null;
			if (winNo == 2) {
				_callback = top.ui.callback;
			}
			top.ui.callback = function (prj) {
				if (option.nameinput) {
					$('#' + option.nameinput).val(prj.prjName);
					
				}
				if (option.idinput)
					$('#' + option.idinput).val(prj.prjId);

				if (typeof option.func == 'function')
					option.func(prj);

				if (winNo == 2)
					top.ui.callback = _callback;
			}
			top.ui.openWin({ title: '选择项目', url: url, winNo: winNo, width: 700 });
		}
	</script>
	<style type="text/css">
		.style2
		{
			height: 18px;
		}
		.style3
		{
			width: 100%;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: auto;">
		<table border="0" cellpadding="0" cellspacing="0" width="100%">
			<tr id="header">
				<td>
					<asp:Label ID="labTitle" Text="" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td>
					<table class="queryTable" cellpadding="3px" cellspacing="0px">
						<tr>
							<td class="descTd">
								项目
							</td>
							<td>
								<span class="spanSelect" style="width: 122px">
									<asp:TextBox ID="txtProject" Style="width: 97px; height: 15px; border: none; line-height: 16px;
										margin: 1px 0px 1px 2px" runat="server"></asp:TextBox>
									<img alt="选择录项目" onclick="openProjPicker();" src="../../images/icon.bmp" style="float: right;" />
								</span>
							</td>
							<td class="descTd" style="display: none;">
								合同
							</td>
							<td style="display: none;">
								<span class="spanSelect" style="width: 122px">
									<asp:TextBox ID="txtContract" Style="width: 97px; height: 15px; border: none; line-height: 16px;
										margin: 1px 0px 1px 2px" ReadOnly="true" runat="server"></asp:TextBox>
									<img src="../../images/icon.bmp" style="float: right;" alt="选择合同" id="img1" onclick="selectContract();" />
									<asp:HiddenField ID="hfldContract" runat="server" />
								</span>
							</td>
							<td>
								月份
							</td>
							<td class="style2">
								<asp:TextBox ID="txtDate" Width="120px" onclick="WdatePicker({dateFmt:'yyyy-MM'})" runat="server"></asp:TextBox>
							</td>
							<td class="style2">
								<asp:Button ID="btn_Search" Text="查询" OnClick="btn_Search_Click" runat="server" />
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td class="divFooter" style="text-align: left;">
					<asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
					<asp:HiddenField ID="hdfRow" runat="server" />
				</td>
			</tr>
			<tr>
				<td style="vertical-align: top;">
					<div class="pagediv">
						<asp:GridView ID="gvPayoutPlan" CssClass="gvdata" Width="100%" AllowPaging="true" AutoGenerateColumns="false" PageSize="15" OnPageIndexChanging="gvPayoutPlan_PageIndexChanging" OnDataBound="gvPayoutPlan_DataBound" OnRowDataBound="gvPayoutPlan_RowDataBound" DataKeyNames="prjGuid" runat="server">
<EmptyDataTemplate>
								<table class="style3 header">
									<tr>
										<td width="20">
											序号
										</td>
										<td>
											项目
										</td>
										<td width="200">
											合同
										</td>
										<td width="90">
											上期计划金额
										</td>
										<td width="90">
											本期计划金额
										</td>
										<td width="90">
											上期实际发生
										</td>
										<td width="120">
											计划执行完成情况
										</td>
										<td width="90">
											计划执行差异
										</td>
										<td width="200">
											说明
										</td>
									</tr>
								</table>
							</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="项目">
<ItemTemplate>
										<asp:Label ID="labPrjName" Text='<%# Convert.ToString(StringUtility.GetStr(Eval("PrjName").ToString(), 15)) %>' runat="server"></asp:Label>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合同">
<ItemTemplate>
										<%# StringUtility.GetStr(Eval("ContractName").ToString(), 15) %>
									</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="上期计划金额" DataField="beforePlanMoney" /><asp:BoundField HeaderText="本期计划金额" DataField="PlanMoney" ItemStyle-CssClass="decimal_input" /><asp:TemplateField HeaderText="上期实际发生金额" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
										<%# Eval("conPayMoney") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上期计划执行完成情况">
<ItemTemplate>
										<%# Eval("ExecuteRatio") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上期计划执行差异" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
										<%# Eval("ExecuteVariation") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="说明">
<ItemTemplate>
										<%# StringUtility.GetStr(Eval("ReMark").ToString(), 15) %>
									</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					</div>
				</td>
			</tr>
		</table>
	</div>
	<div id="divFram" title="" style="display: none">
		<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<div id="divSelectContract" title="选择合同" style="display: none">
		<iframe id="ifrmSelectContract" frameborder="0" width="100%" height="100%" src="../../StockManage/Purchase/PurchaseplanList.aspx">
		</iframe>
	</div>
	<asp:HiddenField ID="hfldPrjName" runat="server" />
	</form>
</body>
</html>
