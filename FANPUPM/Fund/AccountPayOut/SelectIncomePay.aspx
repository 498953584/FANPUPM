<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectIncomePay.aspx.cs" Inherits="Fund_AccountPayout_SelectRP" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>项目合同收入</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			replaceEmptyTable('emptyPayment', 'gvBudget');
			var jwTable = new JustWinTable('gvBudget');

			$('#gvBudget TR[id]').dblclick(function () {
				if (typeof top.ui.callback == 'function') {
					top.ui.callback({ id: this.id, code: $(this).attr('code') });
					top.ui.callback = null;
				}
				top.ui.closeWin();
			});
			setWidthHeight();
		});

		//查看附件
		function queryAdjunct(path) {
			alert(path);
			showFiles(path, 'divOpenAdjunct');
		}
		//查看
		function openQuery() {
			var id = document.getElementById('hfldPurchaseChecked').value;
			if (id != "") {
				var url = '/ContractManage/IncometPayment/AddIncometPayment.aspx?t=1&id=' + id + "&ContractID=" + document.getElementById("hdContractID").value;
				var title = "";
				title = "项目合同支付";
				parent.desktop.RequestPaymentEdit = window;
				title += '明细';
				toolbox_oncommand(url, title);
			}
		}
		function setWidthHeight() {
			$('#divBudget').height($(this).height() - $('#header').height() - 2);
			$('#divBudget').width($('#divContent').width()-2);
        }
	</script>
</head>
<body >
	<form id="form1" runat="server">
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow:Hidden; ">
		<table style="width: 100%; height: 100%;">
			<tr>
				<td style="width: 100%; vertical-align: top; height: 100%;">
					<table class="tab" style="height: 100%;">
						<tr id="header">
							<td>
								<asp:Label ID="lblTitle" Text="项目请款" runat="server"></asp:Label>
							</td>
						</tr>
						<tr>
							<td style="vertical-align: top; height: 100%;width: 100%;  ">
								<div id="divBudget" style="overflow:auto; ">
								 <asp:GridView ID="gvBudget" AllowPaging="true" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvBudget_RowDataBound" OnPageIndexChanging="gvBudget_PageIndexChanging" DataKeyNames="ID,CllectionCode" runat="server">
<EmptyDataTemplate>
												<table id="emptyPayment" class="tab">
													<tr class="header">
														<th scope="col" style="width: 25px;">
															序号 </td>
															<th scope="col">
																收款编号
															</th>
															<th scope="col">
																合同名称
															</th>
															<th scope="col">
																收款日期
															</th>
															<th scope="col">
																收款人
															</th>
															<th scope="col">
																金额
															</th>
															<th scope="col">
																收款累计</td>
																<th scope="col">
																	入账率</td>
																	<th scope="col">
														附件</td>
													</tr>
												</table>
											</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
														<%# Container.DataItemIndex + 1 %>
													</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="收款编号"><ItemTemplate>
														<a href="#" class="al" onclick="viewopen('/ContractManage/IncometPayment/AddIncometPayment.aspx?t=1&id=<%# Eval("ID") %>&contractId=<%# Eval("ContractId") %>',750)">
															<%# Eval("CllectionCode") %></a>
													</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同名称">
<ItemTemplate>
														<%# GetContName(Eval("ContractID").ToString()) %>
													</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="收款日期"><ItemTemplate>
														<%# Common2.GetTime(Eval("CllectionTime").ToString()) %>
													</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="收款人"><ItemTemplate>
														<%# Eval("CllectionUser") %>
													</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="收款金额" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
														<%# Eval("CllectionPrice") %>
													</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="入账累计" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
														 <%# Eval("Cinmoney").ToString() %>
													</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="入账率" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
														<%# PayOutp(Eval("Cinmoney").ToString(), Eval("CllectionPrice").ToString()) %>
                                                        
													</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
														<%# GetAnnx(Convert.ToString(Eval("ID"))) %>
													</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="" Visible="false"><ItemTemplate>
														</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
								</div>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	<div id="divOpenAdjunct" title="查看附件" style="display: none;">
		<table id="annexTable" class="gvdata" style="width: 100%;" runat="server"><tr class="header" runat="server"><td style="width: 60%" runat="server">
						名称
					</td><td style="width: 30%" runat="server">
						大小
					</td><td runat="server">
					</td></tr></table>
	</div>
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	<asp:HiddenField ID="hdContractID" runat="server" />
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	<asp:HiddenField ID="hfldYear" runat="server" />
	<asp:HiddenField ID="hfplantype" runat="server" />
	<asp:HiddenField ID="hfPrjName" runat="server" />
	<asp:HiddenField ID="hfldMonthPlanID" runat="server" />
	</form>
</body>
</html>
