<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayoutConReport.aspx.cs" Inherits="ContractManage_ContractForm_PayoutConReport" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>支出合同明细</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			replaceEmptyTable('emptyPayoutCon', 'gvwPayoutCon');
			var table = new JustWinTable('gvwPayoutCon');
			showTooltip('tooltip', 10);
		})

		// 选择合同类型
		function selectConType() {
			jw.selectOneConType({ nameinput: 'txtConType', idinput: 'hfldConType' });
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		<tr>
			<td>
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							合同编号
						</td>
						<td>
							<asp:TextBox ID="txtContractCode" Width="120px" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							合同名称
						</td>
						<td>
							<asp:TextBox ID="txtContractName" Width="120px" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							合同类型
						</td>
						<td>
							<asp:TextBox ID="txtConType" Width="120px" CssClass="select_input" imgclick="selectConType" runat="server"></asp:TextBox>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td class="divFooter" style="text-align: left;">
				<asp:Button ID="btnSearch" Text="查询" Style="cursor: pointer;" OnClick="btnSearch_Click" runat="server" />
				<asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
			</td>
		</tr>
		<tr>
			<td style="vertical-align: top;">
				<div class="pagediv">
					<asp:GridView ID="gvwPayoutCon" CssClass="gvdata" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="gvwPayoutCon_RowDataBound" DataKeyNames="PayoutConId" runat="server">
<EmptyDataTemplate>
							<table id="emptyPayoutCon" class="gvdata">
								<tr class="header">
									<th scope="col" style="width: 25px;">
										序号
									</th>
									<th scope="col">
										合同编号
									</th>
									<th scope="col">
										合同名称
									</th>
									<th scope="col">
										合同类型
									</th>
									<th scope="col">
										原始金额
									</th>
									<th scope="col">
										最终金额
									</th>
									<th scope="col">
										开累结算
									</th>
									<th scope="col">
										开累支付
									</th>
								</tr>
							</table>
						</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
									<%# StringUtility.GetStr(Eval("Num").ToString()) %>
								</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="ContractCode" HeaderText="合同编号" HeaderStyle-Width="100px" /><asp:TemplateField HeaderText="合同名称" HeaderStyle-Width="100px">
<ItemTemplate>
									<span class="tooltip" title='<%# Eval("ContractName").ToString() %>'>
										<%# StringUtility.GetStr(Eval("ContractName").ToString(), 10) %>
									</span>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合同类型" HeaderStyle-Width="100px">
<ItemTemplate>
									<span class="tooltip" title='<%# Eval("TypeName").ToString() %>'>
										<%# StringUtility.GetStr(Eval("TypeName").ToString(), 10) %>
									</span>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="原始金额" HeaderStyle-Width="80px">
<ItemTemplate>
									<%# Eval("ContractMoney") %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="最终金额" HeaderStyle-Width="80px">
<ItemTemplate>
									<%# Eval("PayoutModifiedMoney") %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="开累结算" HeaderStyle-Width="80px">
<ItemTemplate>
									<%# Eval("PayoutBalanceMoney") %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="开累支付" HeaderStyle-Width="80px">
<ItemTemplate>
									<%# Eval("PayoutPaymentMoney") %>
								</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
					</webdiyer:AspNetPager>
				</div>
			</td>
		</tr>
	</table>
	<div id="divFram" title="" style="display: none">
		<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	</form>
</body>
</html>
