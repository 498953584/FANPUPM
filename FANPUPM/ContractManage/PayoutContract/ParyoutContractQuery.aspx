<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ParyoutContractQuery.aspx.cs" Inherits="ContractManage_PayoutContract_ParyoutContractQuery" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>支出合同</title>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script src="../../Script/jquery.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
	<style type="text/css" media="print">
		.noprint
		{
			display: none;
		}
		table tr
		{
			border-color: Black;
			background-color: Black;
		}
		.fontsize
		{
			font-size: 13px;
		}
	</style>
	<script type="text/javascript">
		$(document).ready(function () {
			if (!document.getElementById('gvBudget')) {
				document.getElementById('trKzzb').style.display = 'none';
			}
			if (!document.getElementById('gvwPurchaseplanStock')) {
				document.getElementById('trPurchase').style.display = 'none';
			}
			registerChkDiffEvent();
		});
		function registerChkDiffEvent() {
			if (!document.getElementById('chkDiff')) return false;
			if (!document.getElementById('trDiff')) return false;
			addEvent(document.getElementById('chkDiff'), 'click', function () {
				if (this.checked) {
					document.getElementById('trDiff').className = '';
				}
				else {
					document.getElementById('trDiff').className = 'noprint';
				}
			});
		}
	</script>
	<style type="text/css">
		#bllProducer
		{
		}
		#bllProducer td
		{
		}
	</style>
</head>
<body id="print1">
	<form id="form1" runat="server">
	<table class="tab" style="vertical-align: top;">
		<tr>
			<td class="divHeader">
				支出合同
				<input type="button" id="btnDy" style="float: right;" class="noprint" onclick="winPrint()"
					value=" 打 印 " />
			</td>
		</tr>
		<tr style="height: 1px;">
			<td>
				<table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style: none;"
					class="viewTable">
					<tr>
						<td style="border-style: none;">
							制单人:&nbsp;&nbsp;<asp:Label ID="lblBllProducer" runat="server"></asp:Label>
						</td>
						<td style="border-style: none; text-align: right">
							打印日期:&nbsp;&nbsp;<asp:Label ID="lblPrintDate" runat="server"></asp:Label>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr style="height: 1px;">
			<td style="vertical-align: top;">
				<table class="viewTable" cellpadding="0px" cellspacing="0">
					<tr>
						<td class="descTd" style="white-space: nowrap;">
							甲方
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtAName" runat="server"></asp:Literal>
						</td>
						<td class="descTd" style="white-space: nowrap;">
							乙方
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtBName" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr style="display: none;">
						<td class="descTd" style="white-space: nowrap;">
							财务项目编号
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtfinanceNumber" runat="server"></asp:Literal>
						</td>
						<td class="descTd" style="white-space: nowrap;">
							财务合同编号
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtfinanceProject" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td class="descTd" style="white-space: nowrap;">
							合同编号
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtContractCode" runat="server"></asp:Literal>
						</td>
						<td class="descTd" style="white-space: nowrap;">
							项目名称
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtProject" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td class="descTd" style="white-space: nowrap;">
							合同名称
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtContractName" runat="server"></asp:Literal>
						</td>
						<td class="descTd" style="white-space: nowrap;">
							合同类型
						</td>
						<td class="elemTd" style="padding-right: 1px;">
							<asp:Literal ID="contractType" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td class="descTd" style="white-space: nowrap;">
							原始金额
						</td>
						<td class="elemTd decimal_input">
							<asp:Literal ID="txtContractMoney" runat="server"></asp:Literal>
						</td>
						<td class="descTd" style="white-space: nowrap;">
							大写
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtCapitalNumber" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td class="descTd" style="white-space: nowrap;">
							最终金额
						</td>
						<td class="elemTd decimal_input">
							<asp:Literal ID="txtModifiedMoney" runat="server"></asp:Literal>
						</td>
						<td class="descTd" style="white-space: nowrap;">
							大写
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtCapitalizationModifiedMoney" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td class="descTd" style="white-space: nowrap;">
							收入合同
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtIncomeContract" runat="server"></asp:Literal>
						</td>
						<td class="descTd" style="white-space: nowrap;">
							签约地点
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtAddress" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							结算方式
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtBalanceMode" runat="server"></asp:Literal>
						</td>
						<td class="descTd">
							签约时间
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtSignDate" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							开始时间
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtStartDate" runat="server"></asp:Literal>
						</td>
						<td class="descTd">
							付款方式
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtdropPayMode" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							结束时间
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtEndDate" runat="server"></asp:Literal>
						</td>
						<td class="descTd">
							合同状态
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtTypeName" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							项目类型
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtPrjType" runat="server"></asp:Literal>
						</td>
						<td class="descTd">
							预付金额
						</td>
						<td class="elemTd decimal_input">
							<asp:Literal ID="txtPreMoney" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							支付条件
						</td>
						<td colspan="3">
							<asp:Literal ID="txtPaymentCondition" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							主要条款
						</td>
						<td colspan="3">
							<asp:Literal ID="txtMainItem" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							备注
						</td>
						<td colspan="3" style="word-break: break-all;">
							<asp:Literal ID="txtNotes" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							附件
						</td>
						<td colspan="3">
							<asp:Literal ID="Literal1" runat="server"></asp:Literal>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr id="trKzzb" style="vertical-align: top; height: 1px;">
			<td style="vertical-align: top; padding-top: 10px;">
				<div style="font-size: 13px; font-weight: bold; text-align: center;">
					<asp:Label ID="lblTitlTarget" Font-Size="13px" Font-Bold="true" runat="server"></asp:Label></div>
				
				<asp:Repeater ID="Repeater1" runat="server">
<HeaderTemplate>
						<table class="viewTable" cellspacing="0" rules="all" border="1" id="gvBudget" style="border-collapse: collapse;">
							<tr>
								<th scope="col">
									序号
								</th>
								<th scope="col">
									名称
								</th>
								<th scope="col">
									编码
								</th>
								<th scope="col">
									单位
								</th>
								<th scope="col">
									工程量
								</th>
								<th scope="col">
									开始时间
								</th>
								<th scope="col">
									结束时间
								</th>
								<th scope="col">
									综合单价
								</th>
								<th scope="col">
									小计
								</th>
							</tr>
					</HeaderTemplate>

<ItemTemplate>
						<tr>
							<td>
								<%# Eval("No") %>
							</td>
							<td>
								<%# Eval("TaskName") %>
							</td>
							<td>
								<%# Eval("TaskCode") %>
							</td>
							<td>
								<%# Eval("Unit") %>
							</td>
							<td>
								<%# Eval("Quantity") %>
							</td>
							<td>
								<%# Common2.GetTime(Eval("StartDate")) %>
							</td>
							<td>
								<%# Common2.GetTime(Eval("EndDate")) %>
							</td>
							<td>
								<%# Common2.FormatDecimal(Eval("UnitPrice")) %>
							</td>
							<td>
								<%# Common2.FormatDecimal(Eval("Total2")) %>
							</td>
						</tr>
					</ItemTemplate>

<FooterTemplate>
						</table>
					</FooterTemplate>
</asp:Repeater>
			</td>
		</tr>
		<tr id="trPurchase" style="vertical-align: top; height: 1px;">
			<td style="vertical-align: top; padding-top: 10px;">
				<div style="font-size: 13px; font-weight: bold; text-align: center;">
					<asp:Label ID="lblTitalPurchase" Font-Size="13px" Font-Bold="true" runat="server"></asp:Label></div>
				
				<asp:GridView ID="gvwPurchaseplanStock" Width="100%" AutoGenerateColumns="false" CssClass="viewTable" DataKeyNames="scode" runat="server">
<EmptyDataTemplate>
						<table class="tab">
							<tr class="header">
								<td>
									<asp:CheckBox ID="chkSelectAll" runat="server" />
								</td>
								<td style="width: 20px">
									序号
								</td>
								<td>
									物资编号
								</td>
								<td>
									物资名称
								</td>
								<td>
									规格
								</td>
								<td>
									单位
								</td>
								<td>
									数量
								</td>
								<td>
									采购价格
								</td>
								<td>
									小计
								</td>
								<td>
									供应商
								</td>
								<td>
									型号
								</td>
								<td>
									品牌
								</td>
								<td>
									技术参数
								</td>
							</tr>
						</table>
					</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="20px"><ItemTemplate>
								<%# Container.DataItemIndex + 1 %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资编号"><ItemTemplate>
								<%# Eval("ResourceCode") %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资名称">
<ItemTemplate>
								<%# Eval("ResourceName") %>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="规格"><ItemTemplate>
								<%# Eval("Specification") %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位"><ItemTemplate>
								<%# Eval("Name") %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="库存量" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
								<%# WebUtil.GetStockNumberByCode(Eval("scode").ToString()).ToString() %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
								<%# Eval("number") %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="采购价格" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
								<%# Eval("sprice") %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="小计" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
								<%# System.Convert.ToDecimal(Eval("Total")).ToString("0.000") %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="供应商">
<ItemTemplate>
								<%# Eval("CorpName") %>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="型号">
<ItemTemplate>
								<%# Eval("ModelNumber") %>
								</span>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="品牌">
<ItemTemplate>
								<%# Eval("brand") %>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数">
<ItemTemplate>
								<%# Eval("TechnicalParameter") %>
							</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
			</td>
		</tr>
		<tr id="trDiff" style="vertical-align: top; height: 1px;">
			<td>
				<div id="diffTitle" style="position: relative;" runat="server">
					指标对比表
					<div class="noprint" style="height: 20px; width: 100%; left: 0px; bottom: 0px; text-align: right;
						position: absolute; font-weight: normal;">
						<asp:CheckBox ID="chkDiff" Style="float: right;" Checked="true" Text="打印" runat="server" />
					</div>
				</div>
				<div style="width: 100%;">
					<asp:GridView AutoGenerateColumns="false" Width="100%" ID="gvwDiff" CssClass="viewTable" OnRowDataBound="gvwDiff_RowDataBound" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="2%">
<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="ResourceName" HeaderText="名称" HeaderStyle-Width="15%" /><asp:BoundField DataField="Specification" HeaderText="规格" HeaderStyle-Width="5%" /><asp:BoundField DataField="Brand" HeaderText="品牌" HeaderStyle-Width="5%" /><asp:BoundField DataField="BudQuantity" HeaderText="目标成本预算量" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%" /><asp:BoundField DataField="PurPlanNumber" HeaderText="物资采购审核计划量" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%" /><asp:BoundField DataField="PurNumber" HeaderText="本次采购量" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%" /><asp:BoundField DataField="ConResPrice" HeaderText="合同预算报价" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%" /><asp:BoundField DataField="BudPrice" HeaderText="目标成本预算价" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%" /><asp:BoundField DataField="PuredPrice" HeaderText="以往采购价" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%" /><asp:BoundField DataField="PurPrice" HeaderText="本次采购价" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				</div>
			</td>
		</tr>
		<tr id="trAudit" style="vertical-align: top;">
			<td>
				<MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="081" BusiClass="001" runat="server" />
			</td>
		</tr>
		<tr>
		</tr>
	</table>
	<asp:HiddenField ID="hfldWantPlanGuid" runat="server" />
	
	<asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
	</form>
</body>
</html>
