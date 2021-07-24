<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncometConReport.aspx.cs" Inherits="ContractManage_ContractForm_IncometConReport" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>收入合同明细</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			replaceEmptyTable('emptyIncometCon', 'gvwIncometCon');
			var table = new JustWinTable('gvwIncometCon');
			showTooltip('tooltip', 10);
		})

		function viewopen(IncometConId) {
			parent.desktop.PayoutConReport = window;
			var url = "/ContractManage/ContractForm/PayoutConReport.aspx?IncometConId=" + IncometConId;
			toolbox_oncommand(url, "支出合同明细");
		}


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
					<asp:GridView ID="gvwIncometCon" CssClass="gvdata" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="gvwIncometCon_RowDataBound" DataKeyNames="ContractId" runat="server">
<EmptyDataTemplate>
							<table id="emptyIncometCon" class="gvdata">
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
										最终金额
									</th>
									<th scope="col">
										开累结算
									</th>
									<th scope="col">
										开累回款
									</th>
									<th scope="col">
										挂靠项目</br>开累付款
									</th>
									<th scope="col">
										支出合同</br>最终金额
									</th>
									<th scope="col">
										支出合同</br>开累结算
									</th>
									<th scope="col">
										开累支付
									</th>
									<th scope="col">
										合同差额
									</th>
									<th scope="col">
										结算差额
									</th>
									<th scope="col">
										支付差额
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
									<%# Eval("ContractPrice") %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="最终金额" HeaderStyle-Width="80px">
<ItemTemplate>
									<%# Eval("InCometContractMoney") %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="开累结算" HeaderStyle-Width="80px">
<ItemTemplate>
									<%# Eval("IncometBalanceMoney") %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="开累回款" HeaderStyle-Width="80px">
<ItemTemplate>
									<%# Eval("IncometPaymentMoney") %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="挂靠项目</br>开累付款" HeaderStyle-Width="80px">
<ItemTemplate>
									<%# Eval("IncometApplyMoney") %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="支出合同</br>最终金额" HeaderStyle-Width="80px">
<ItemTemplate>
									<span class="link" onclick="viewopen('<%# Eval("ContractId") %>');">
										<%# Eval("PayoutModifiedMoney") %>
									</span>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="支出合同</br>开累结算" HeaderStyle-Width="80px">
<ItemTemplate>
									<%# Eval("PayoutBalanceMoney") %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="开累支付" HeaderStyle-Width="80px">
<ItemTemplate>
									<%# Eval("PayoutPaymentMoney") %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
									<span class="tooltip" title="收入合同最终金额-支出合同最终金额">合同差额 </span>
								</HeaderTemplate>

<ItemTemplate>
									<%# Eval("SpreadConMoney") %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
									<span class="tooltip" title="收入合同开累结算-支出合同开累结算">结算差额 </span>
								</HeaderTemplate>

<ItemTemplate>
									<%# Eval("SpreadBalanceMoney") %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
									<span class="tooltip" title=" 开累回款-开累支付 ">支付差额</span>
								</HeaderTemplate>

<ItemTemplate>
									<%# Eval("SpreadPaymentMoney") %>
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
