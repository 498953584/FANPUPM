<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectRP.aspx.cs" Inherits="Fund_AccountPayout_SelectRP" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>项目合同支付</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			Val.validate('form1', 'btnSearch');
			//            $('#btnSearch').click(save);
			setDivDiaries();
		});

		$(document).ready(function () {
			replaceEmptyTable('emptyPayment', 'gvBudget');
			var jwTable = new JustWinTable('gvBudget');
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
				var url = '/ContractManage/PayoutPayment/PaymentQuery.aspx?Action=Query&PaymentId=' + id + "&ic=" + id;
				var title = "";
				title = "项目合同支付";
				parent.desktop.RequestPaymentEdit = window;
				title += '明细';
				toolbox_oncommand(url, title);
			}
		}
		function dbClickRow(Guid) {
			//alert(Guid);
			//            $(parent.document).find('#hdnRPUID').val(Guid);
			//            $(parent.document).find('.ui-icon-closethick').each(function () {
			//                this.click();
			//            });
			if (typeof top.ui.callback == 'function') {
				top.ui.callback(Guid);
				top.ui.callback = null;
			}
			top.ui.closeWin();
			//parent.document.getElementById('btnPlan').click();
		}
		function setDivDiaries() {
			$('#divBudget').width($('#divContent').width() - 2);
			$('#divBudget').height($('#divContent').height() - $('#title').height() - $('#header').height() - 2);


		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
		<table style="width: 100%; height: 100%;">
			<tr>
				<td style="width: 100%; vertical-align: top; height: 100%;">
					<table class="tab" style="height: 100%;">
						<tr id="title">
							<td>
								支付编号：<asp:TextBox ID="prjcode" Width="120px" runat="server"></asp:TextBox>&nbsp;
								入账率(%)：<asp:TextBox ID="zfstart" Width="50px" CssClass="{number:true, messages:{number:'入账率必须为数字!'}}" runat="server"></asp:TextBox>—<asp:TextBox ID="zfend" Width="50px" CssClass="{number:true, messages:{number:'入账率必须为数字!'}}" runat="server"></asp:TextBox>
								<asp:Button ID="btnSearch" Text="查询" runat="server" />
							</td>
						</tr>
						<tr id="header">
							<td>
								<asp:Label ID="lblTitle" Text="项目请款" runat="server"></asp:Label>
							</td>
						</tr>
						<tr>
							<td id="tdContent" style="vertical-align: top; height: 100%; width: 100%;">
								<div id="divBudget" style="overflow: auto; height: 100%; width: 100%;">
									<asp:GridView ID="gvBudget" AllowPaging="true" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvBudget_RowDataBound" OnPageIndexChanging="gvBudget_PageIndexChanging" DataKeyNames="ID" runat="server">
<EmptyDataTemplate>
											<table id="emptyPayment" class="tab">
												<tr class="header">
													<th scope="col" style="width: 25px;">
														序号 </td>
														<th scope="col">
															支付编号
														</th>
														<th scope="col">
															合同名称
														</th>
														<th scope="col">
															支付金额
														</th>
														<th scope="col">
															入账率
														</th>
														<th scope="col">
															支付时间
														</th>
														<th scope="col">
															支付人
														</th>
														<th scope="col">
															流程状态
														</th>
														<th scope="col">
															备注</td>
															<th scope="col">
													附件</td>
												</tr>
											</table>
										</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
													<%# Container.DataItemIndex + 1 %>
												</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="支付编号">
<ItemTemplate>
													<span class="al" onclick="viewopen('/ContractManage/PayoutPayment/PaymentQuery.aspx?Action=Query&PaymentId=<%# Eval("ID") %>&ic=<%# Eval("ID") %>')">
														<%# Eval("PaymentCode") %>
													</span>
												</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合同名称">
<ItemTemplate>
													<%# Eval("ContractName") %>
												</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="支付金额" DataField="PaymentMoney" DataFormatString="{0:F2}" /><asp:TemplateField HeaderText="入账率">
<ItemTemplate>
													<%# PayOutp(Eval("PaymentMoney").ToString(), Eval("pidcont").ToString()) %>
												</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="支付时间" DataField="paymentdate" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" /><asp:BoundField HeaderText="支付人" DataField="paymentPerson" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" /><asp:TemplateField HeaderText="流程状态" HeaderStyle-Width="80px">
<ItemTemplate>
													<%# Common2.GetState(Eval("FlowState").ToString()) %>
												</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="备注" DataField="notes" /><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
													<%# GetAnnx(Convert.ToString(Eval("ID"))) %>
												</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	<asp:HiddenField ID="hfldYear" runat="server" />
	<asp:HiddenField ID="hfplantype" runat="server" />
	<asp:HiddenField ID="hfPrjName" runat="server" />
	<asp:HiddenField ID="hfldMonthPlanID" runat="server" />
	</form>
</body>
</html>
