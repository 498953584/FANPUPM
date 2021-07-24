<%@ Page Language="C#" AutoEventWireup="true" CodeFile="contForm1.aspx.cs" Inherits="ContractManage_ContractForm_contForm1" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>经营情况分析报表</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			replaceEmptyTable('emptyContract', 'gvwContract');
			var gvwContract = new JustWinTable('gvwContract');
			formateTable('gvwContract', 2, true);
			showTooltip('tooltip', 20);
		});

		//选择项目
		function openProjPicker() {
			jw.selectOneProject({ nameinput: 'txtProject' });
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<table width="100%" cellpadding="0" cellspacing="0">
			
			<tr>
				<td>
					<table class="queryTable" cellpadding="3px" cellspacing="0px">
						<tr>
							<td class="descTd">
								项目名称
							</td>
							<td>
								<input type="text" id="txtProject" style="width: 120px;" class="select_input" imgclick="openProjPicker" runat="server" />

							</td>
							<td class="descTd" visible="false" runat="server">
								起始时间
							</td>
							<td visible="false" runat="server">
								<asp:TextBox ID="txtStartDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="brnQuery" Width="80px" Text="查询" OnClick="brnQuery_Click" runat="server" />
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td>
					<asp:GridView ID="gvwContract" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" OnDataBound="gvwContract_DataBound" OnRowDataBound="gvwContract_RowDataBound" runat="server">
<EmptyDataTemplate>
							<table class="gvdata" cellspacing="0" id="emptyContract" rules="all" border="1" id="gvwContract"
								style="width: 100%; border-collapse: collapse;">
								<tr>
									<th class="header" rowspan="2">
										序号
									</th>
									<th class="header" rowspan="2">
										项目名称
									</th>
									<th class="header" colspan="6">
										收入合同
									</th>
									<th class="header" colspan="7">
										支出合同
									</th>
									<th class="header" rowspan="2">
										原合同差额
									</th>
									<th class="header" rowspan="2">
										变更后合同差额
									</th>
									<th class="header" rowspan="2">
										结算差额
									</th>
									<th class="header" rowspan="2">
										支付差额
									</th>
								</tr>
								<tr class='header'>
									</th><th rowspan="1">
										合同编号
									</th>
									<th rowspan="1">
										合同名称
									</th>
									<th rowspan="1">
										原始金额
									</th>
									<th rowspan="1">
										变更后金额
									</th>
									<th rowspan="1">
										开累结算
									</th>
									<th rowspan="1">
										开累回款
									</th>
									<th rowspan="1">
										合同编号
									</th>
									<th rowspan="1">
										合同名称
									</th>
									<th rowspan="1">
										合同类型
									</th>
									<th rowspan="1">
										原始金额
									</th>
									<th rowspan="1">
										变更后金额
									</th>
									<th rowspan="1">
										开累结算
									</th>
									<th rowspan="1">
										开累回款
									</th>
								</tr>
							</table>
						</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<ItemTemplate>
									<%# Container.DataItemIndex + 1 %></ItemTemplate>
</asp:TemplateField><asp:TemplateField><ItemTemplate>
									<asp:Label ID="lblPrjName" CssClass="tooltip" Text='<%# System.Convert.ToString(StringUtility.GetStr(Eval("prjName").ToString(), 20), System.Globalization.CultureInfo.CurrentCulture) %>' ToolTip='<%# System.Convert.ToString(Eval("prjName"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
								</ItemTemplate></asp:TemplateField><asp:BoundField DataField="ContractCode" /><asp:TemplateField><ItemTemplate>
									<asp:Label ID="lblName" CssClass="tooltip" Text='<%# System.Convert.ToString(StringUtility.GetStr(Eval("ContractName").ToString(), 20), System.Globalization.CultureInfo.CurrentCulture) %>' ToolTip='<%# System.Convert.ToString(Eval("ContractName"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
								</ItemTemplate></asp:TemplateField><asp:BoundField DataField="ContractPrice" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="ChangePrice" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="ClearingPrice" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="CllectionPrice" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="ApplyAmount" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="ContractCode1" /><asp:TemplateField><ItemTemplate>
									<span class="tooltip" title='<%# Eval("ContractName1") %>'>
										<%# StringUtility.GetStr(Eval("ContractName1").ToString(), 20) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:BoundField DataField="TypeName" /><asp:BoundField DataField="ContractMoney" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="ModifiedMoney" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="BalanceMoney" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="PaymentMoney" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="ContractChazhi" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="ChangeChazhi" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="ClearingChazhi" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="CllectionChazhi" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
