<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FundOverheadReport.aspx.cs" Inherits="Fund_MonthPlan_FundOverheadReport" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>间接费用报表</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script src="../../Web_Client/TreeNew.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var ReturnTable = new JustWinTable('gvOverhead');
		});

		//选择项目
		function openProjPicker() {
			jw.selectOneProject({ idinput: 'hdnProjectCode', nameinput: 'txtProject'});
		}

	</script>
	<style type="text/css">
		.style1
		{
			width: 100%;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		<tr id="header">
			<td>
				支出计划报表
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
								<img alt="选择录入人" onclick="openProjPicker();" src="../../images/icon.bmp" style="float: right;" />
							</span>
							<input id="hdnProjectCode" type="hidden" name="hdnProjectCode" runat="server" />

						</td>
						<td class="style2">
							计划月份
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
			</td>
		</tr>
		<tr>
			<td style="vertical-align: top;">
				<div class="pagediv">
					<asp:GridView ID="gvOverhead" CssClass="gvdata" Width="100%" AllowPaging="true" AutoGenerateColumns="false" PageSize="16" OnPageIndexChanging="gvPayoutPlan_PageIndexChanging" OnDataBound="gvOverhead_DataBound" OnRowDataBound="gvOverhead_RowDataBound" DataKeyNames="PrjGuid" runat="server">
<EmptyDataTemplate>
							<table class="style3 header">
								<tr>
									<td>
										序号
									</td>
									<td>
										项目
									</td>
									<td>
										所属科目
									</td>
									<td>
										上期计划金额
									</td>
									<td>
										上期实际发生
									</td>
									<td>
										本期计划金额
									</td>
									<td>
										上期计划执行完成情况
									</td>
									<td>
										上期实际数与计划数差异
									</td>
								</tr>
							</table>
						</EmptyDataTemplate>
<Columns><asp:BoundField DataField="prjName" HeaderText="项目" /><asp:TemplateField HeaderText="科目">
<ItemTemplate>
									<%# StringUtility.GetStr(Eval("Name").ToString(), 15) %>
								</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="上期计划金额" DataField="agoAmount" ItemStyle-CssClass="decimal_input" /><asp:BoundField HeaderText="上期实际发生" DataField="payAmount" ItemStyle-CssClass="decimal_input" /><asp:TemplateField HeaderText="本期计划金额" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
									<%# Eval("NewAmount") %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上期计划执行完成情况">
<ItemTemplate>
									<%# Convert.ToDecimal(Eval("ratio")) * 100m %>%
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上期实际数与计划数差异" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
									<%# Eval("balance") %>
								</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				</div>
			</td>
		</tr>
	</table>
	<div id="divFram" title="" style="display: none">
		<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<div id="divSelectContract" title="选择合同" style="display: none">
		<iframe id="ifrmSelectContract" frameborder="0" width="100%" height="100%" src="../../StockManage/Purchase/PurchaseplanList.aspx">
		</iframe>
	</div>
	</form>
</body>
</html>
