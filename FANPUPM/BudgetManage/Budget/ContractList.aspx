<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractList.aspx.cs" Inherits="BudgetManage_Budget_ContractList" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>合同预算清单</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/jquery.treetable.js"></script>
	<script type="text/javascript" src="../../Script/jquery.blockUI.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.extension.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<link type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" />

	<script type="text/javascript">
		$(document).ready(function () {
			$('#gvBudget').treetable(false, 'ConstractList');
			setWidthHeight();
		});

		function setWidthHeight() {
			$('#divBudget').height($(this).height() - 2 - 20);
			$('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 7);
			$('#div_project').height($(this).height() - $('#ddlYear').height() - 4);
		}
	</script>
	<style type="text/css">
		.descTd
		{
			text-align: right;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divHeader" style="display: none;">
		<table class="tableHeader">
			<tr>
				<td>
					<asp:Label ID="lblTitle" Font-Bold="true" Text="合同预算清单" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%;">
		<table>
			<tr>
				<td style="width: 100%; height: 100%;">
					<table style="width: 100%; height: 100%;">
						<tr>
							<td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
								<table class="tab">
									<tr>
										<td style="vertical-align: top; height: 100%;" colspan="3">
											<div id="divBudget" class="pagediv" style="overflow:scroll;">
												<asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="TaskId,SubCount,OrderNumber" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
																<%# Eval("No") %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="清单编码"><ItemTemplate>
																<%# Eval("TaskCode") %>
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
															</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="工程量" DataField="Quantity" DataFormatString="{0:F3}" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px" ItemStyle-CssClass="decimal_input" /><asp:TemplateField HeaderText="综合单价" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
																<%# System.Convert.ToDecimal(Eval("UnitPrice")).ToString() %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="主材" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
																<%# System.Convert.ToDecimal(Eval("MainMaterial")).ToString() %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="辅材" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
																<%# System.Convert.ToDecimal(Eval("SubMaterial")).ToString() %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="人工" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
																<%# System.Convert.ToDecimal(Eval("Labor")).ToString() %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合价" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
																<%# System.Convert.ToDecimal(Eval("Total")).ToString() %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="50px"><ItemTemplate>
																<%# Common2.GetTime(Eval("StartDate")) %>
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="50px"><ItemTemplate>
																<%# Common2.GetTime(Eval("EndDate")) %>
															</ItemTemplate></asp:TemplateField><asp:BoundField DataField="ConstructionPeriod" HeaderText="工期(天)" /><asp:TemplateField HeaderText="备注"><ItemTemplate>
																<span class="tooltip" title="<%# Eval("Note").ToString() %>">
																	<%# StringUtility.GetStr(Eval("Note").ToString()) %></span>
															</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
											</div>
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
