<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayoutContractSum.aspx.cs" Inherits="ContractManage_ContractReport_PayoutContractSum" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>支出合同汇总表</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			replaceEmptyTable('emptyContract', 'gvwContract');
			var table = new JustWinTable('gvwContract');
			formateTable('gvwContract', 2, true);
			addPregressBar('percent');
			//document.getElementById('txtProject').setAttribute('readonly', 'readonly');
			showTooltip('tooltip', 10);
		})
		//选择项目
		function openProjPicker() {
			jw.selectOneProject({ idinput: 'hdnProjectCode', nameinput: 'txtProject' });
		}
		function viewopen_n(url) {
			viewopen(url);
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
		<tr id="header">
			<td>
				支出合同汇总表
			</td>
		</tr>
		<tr>
			<td>
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							合同类型:
						</td>
						<td>
							<asp:TextBox ID="txtConType" Width="120px" CssClass="select_input" imgclick="selectConType" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							项目名称:
						</td>
						<td>
							<asp:TextBox ID="txtProject" Style="width: 120px;" class="select_input" imgclick="openProjPicker" runat="server"></asp:TextBox>
							<input id="hdnProjectCode" type="hidden" name="hdnProjectCode" runat="server" />

						</td>
						<td class="descTd" align="center">
							合同编码:
						</td>
						<td>
							<asp:TextBox ID="txtContractCode" Text="" runat="server"></asp:TextBox>
						</td>
						<td class="descTd" align="center">
							合同名称:
						</td>
						<td>
							<asp:TextBox ID="txtContractName" Text="" runat="server"></asp:TextBox>
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
				<div class="pagediv" style="width: 100%">
					<asp:GridView ID="gvwContract" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwContract_RowDataBound" DataKeyNames="ContractCode,IsMainContract" runat="server">
<EmptyDataTemplate>
							<table id="emptyContract" class="gvdata">
								<tr class="header">
									<th scope="col" style="width: 5%">
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
										项目名称
									</th>
									<th scope="col">
										合同金额
									</th>
									<th scope="col">
										累计结算金额
									</th>
									<th scope="col">
										累计已付款金额
									</th>
									<th scope="col">
										应付未付金额
									</th>
								</tr>
							</table>
						</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="5%"><ItemTemplate>
									<%# StringUtility.GetStr(Eval("Num").ToString()) %>
								</ItemTemplate></asp:TemplateField><asp:BoundField DataField="ContractCode" ItemStyle-HorizontalAlign="Left" HeaderText="合同编号" ItemStyle-Width="10%" /><asp:TemplateField HeaderText="合同名称" HeaderStyle-Width="100px"><ItemTemplate>
									<span class="link tooltip" title='<%# Eval("ContractName").ToString() %>' onclick="viewopen_n ('/ContractManage/PayoutContract/ParyoutContractQuery.aspx?bc=081&bcl=001&ic=<%# Eval("ContractID") %>', '支出合同查看')">
										<%# StringUtility.GetStr(Eval("ContractName").ToString(), 10) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同类型"><ItemTemplate>
									<span class="tooltip" title='<%# Eval("TypeName").ToString() %>'>
										<%# StringUtility.GetStr(Eval("TypeName").ToString(), 10) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
									<span class="tooltip" title='<%# Eval("PrjName").ToString() %>'>
										<%# StringUtility.GetStr(Eval("PrjName").ToString(), 10) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:BoundField DataField="ModifiedMoney" ItemStyle-HorizontalAlign="Right" HeaderText="合同金额" ItemStyle-Width="15%" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="BalanceMoney" ItemStyle-HorizontalAlign="Right" HeaderText="累计结算金额" ItemStyle-Width="15%" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="PaymentMoney" ItemStyle-HorizontalAlign="Right" HeaderText="累计已付款金额" ItemStyle-Width="15%" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="NoPaymentMoney" ItemStyle-HorizontalAlign="Right" HeaderText="应付未付金额" ItemStyle-Width="15%" ItemStyle-CssClass="decimal_input" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
