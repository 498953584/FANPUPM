<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccounViewList.aspx.cs" Inherits="Fund_AccounDetailList" %>
<%@ Import Namespace="com.jwsoft.pm.entpm"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>明细</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
	<script type="text/javascript" src="/Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
	<script type="text/javascript" src="/UserControl/FileUpload/GetFiles.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script src="../Script/DecimalInput.js" type="text/javascript"></script>

	<style type="text/css">
		.newdiv
		{
			font-family: 宋体, Arial, Helvetica, sans-serif;
			font-size: 12px;
			font-weight: bold;
			line-height: 28px;
			text-align: left;
			padding-top: 5px;
		}
	</style>
	<script language="javascript" type="text/javascript">
		$(function () {
			//当数据量为空时，修改样式
			replaceEmptyTable('emptyContractType1', 'GridView1');
			replaceEmptyTable('emptyContractType2', 'GridView2');
			replaceEmptyTable('emptyContractType3', 'GridView3');
			replaceEmptyTable('emptyContractType4', 'GridView4');
			replaceEmptyTable('emptyContractType5', 'GridView5');
			replaceEmptyTable('emptyContractType6', 'GridView6');
			replaceEmptyTable('emptyContractType7', 'GridView7');
			replaceEmptyTable('emptyContractType8', 'GridView8');
			replaceEmptyTable('emptyContractType9', 'GridView9');
		});
		//选择项目
		function openProjPicker() {
			var vaic = $("#HiddenField1").val();
			if (vaic != "") {
				document.getElementById("divFram").title = "选择项目";
				document.getElementById("ifFram").src = "/Fund/SelectPrjName.aspx?Method=returnPrj&ic=" + vaic;
				//selectnEvent('divFram');
				$('#divFram').dialog({ width: 300, height: 200, modal: false });
			}
		}
		//选择项目返回值
		function returnPrj(id, name) {
			document.getElementById('hdnProjectCode').value = id;
			document.getElementById('txtPrjName').value = name;
		}
	</script>
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
</head>
<body>
	<form id="form1" runat="server">
	<asp:HiddenField ID="HiddenField1" runat="server" />
	<asp:HiddenField ID="hdnProjectCode" runat="server" />
	<div id="divFram" title="" style="display: none">
		<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<table class="tab noprint" style="vertical-align: top; height: 28px;">
		<tr>
			<td class="divHeader">
				账户明细
				
				<input type="button" id="btnDy" style="float: right;" class="noprint" onclick="winPrint()"
					value=" 打 印 " />
				
			</td>
		</tr>
	</table>
	<div style="padding-bottom: 10px; margin-left: 10px; margin-top: 10px; margin-right: 10px;">
		<div class="noprint">
			<table class="queryTable noprint" cellpadding="3px" cellspacing="0px">
				<tr>
					<td class="descTd">
						<div style="width: 48px;">
							发生日期</div>
					</td>
					<td style="white-space: nowrap">
						<asp:TextBox ID="txtBeginDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
					</td>
					<td class="descTd">
						至
					</td>
					<td>
						<asp:TextBox ID="txtEndDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
					</td>
					<td class="descTd" style="display: none;">
						<div style="width: 48px;">
							项目</div>
					</td>
					<td style="white-space: nowrap; display: none;">
						<span id="span1" class="spanSelect" style="width: 150px; background-color: #FEFEF4;">
							<input type="text" style="width: 79%; background-color: #FEFEF4; height: 15px; border: none;
								line-height: 16px; margin: 1px 2px" id="txtPrjName" runat="server" />

							<img id="Img1" src="~/images/icon.bmp" style="float: right;" onclick="openProjPicker();" alt="" runat="server" />

							<input id="hdnPrjCode" type="hidden" name="hdnPrjCode" runat="server" />

						</span>
					</td>
					<td>
						<asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
					</td>
				</tr>
			</table>
		</div>
		<div>
			<div class="newdiv">
				启动资金
			</div>
			<asp:GridView ID="GridView1" CssClass="gvdata" AutoGenerateColumns="false" runat="server">
<EmptyDataTemplate>
					<table class="gvdata" id="emptyContractType">
						<tr class="header">
							<th scope="col" style="width: 25px;">
								序号
							</th>
							<th scope="col" style="width: 110px;">
								项目
							</th>
							<th scope="col" style="width: 70px;">
								发生日期
							</th>
							<th scope="col" style="width: 50px;">
								经手人
							</th>
							<th scope="col" style="width: 70px;">
								金额
							</th>
							<th scope="col">
								备注
							</th>
						</tr>
					</table>
				</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
							<%# Container.DataItemIndex + 1 %>
						</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目" ItemStyle-Width="110px"><ItemTemplate>
							<%# Eval("PrjName") %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="发生日期" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
							<%# (Eval("OccurredDate") != null) ? DataBinder.Eval(Container.DataItem, "OccurredDate", "{0:yyyy-MM-dd}") : "" %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="经手人" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
							<%# Eval("v_xm") %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="70px" ItemStyle-CssClass="decimal_input"><ItemTemplate>
							<%# (Eval("INMoney") != null) ? DataBinder.Eval(Container.DataItem, "INMoney", "{0:F3}") : "" %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
							<%# Eval("RemarkText") %>
						</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
			<div class="newdiv">
				其他收入
			</div>
			<asp:GridView ID="GridView2" CssClass="gvdata" AutoGenerateColumns="false" runat="server">
<EmptyDataTemplate>
					<table class="gvdata" id="emptyContractType2">
						<tr class="header">
							<th scope="col" style="width: 25px;">
								序号
							</th>
							<th scope="col" style="width: 110px;">
								项目
							</th>
							<th scope="col" style="width: 70px;">
								发生日期
							</th>
							<th scope="col" style="width: 50px;">
								经手人
							</th>
							<th scope="col" style="width: 70px;">
								金额
							</th>
							<th scope="col">
								备注
							</th>
						</tr>
					</table>
				</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
							<%# Container.DataItemIndex + 1 %>
						</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目" HeaderStyle-Width="110px"><ItemTemplate>
							<%# Eval("PrjName") %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="发生日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px"><ItemTemplate>
							<%# (Eval("OccurredDate") != null) ? DataBinder.Eval(Container.DataItem, "OccurredDate", "{0:yyyy-MM-dd}") : "" %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="经手人" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
							<%# Eval("v_xm") %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="70px"><ItemTemplate>
							<%# (Eval("INMoney") != null) ? DataBinder.Eval(Container.DataItem, "INMoney", "{0:n}") : "" %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
							<%# Eval("RemarkText") %>
						</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
			<div class="newdiv">
				合同回款入账
			</div>
			<asp:GridView ID="GridView3" CssClass="gvdata" AutoGenerateColumns="false" runat="server">
<EmptyDataTemplate>
					<table class="gvdata" id="emptyContractType3">
						<tr class="header">
							<th scope="col" style="width: 25px;">
								序号
							</th>
							<th scope="col" style="width: 110px;">
								项目
							</th>
							<th scope="col" style="width: 70px;">
								发生日期
							</th>
							<th scope="col" style="width: 50px;">
								经手人
							</th>
							<th scope="col" style="width: 70px;">
								回款入账额
							</th>
							<th scope="col" style="width: 100px;">
								回款金额
							</th>
							<th scope="col" style="width: 100px;">
								合同
							</th>
							<th scope="col">
								备注
							</th>
						</tr>
					</table>
				</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
							<%# Container.DataItemIndex + 1 %>
						</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目" HeaderStyle-Width="110px"><ItemTemplate>
							<%# Eval("PrjName") %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="发生日期" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
							<%# (Eval("OccurredDate") != null) ? DataBinder.Eval(Container.DataItem, "OccurredDate", "{0:yyyy-MM-dd}") : "" %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="经手人" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
							<%# PageHelper.QueryUser(this, Eval("inpeople").ToString()) %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="回款入账额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="70px"><ItemTemplate>
							<%# (Eval("INMoney") != null) ? DataBinder.Eval(Container.DataItem, "INMoney", "{0:F3}") : "" %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="回款金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px"><ItemTemplate>
							<%# (Eval("Inm") != null) ? DataBinder.Eval(Container.DataItem, "Inm", "{0:F3}") : "" %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同" HeaderStyle-Width="100px"><ItemTemplate>
							<%# Eval("ContName") %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
							<%# Eval("Remark") %>
						</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
			<div class="newdiv">
				项目借款
			</div>
			<asp:GridView ID="GridView4" CssClass="gvdata" AutoGenerateColumns="false" runat="server">
<EmptyDataTemplate>
					<table class="gvdata" id="emptyContractType4">
						<tr class="header">
							<th scope="col" style="width: 25px;">
								序号
							</th>
							<th scope="col" style="width: 110px;">
								项目
							</th>
							<th scope="col" style="width: 70px;">
								发生日期
							</th>
							<th scope="col" style="width: 50px;">
								经手人
							</th>
							<th scope="col" style="width: 70px;">
								金额
							</th>
							<th scope="col" style="width: 100px;">
								是否还清
							</th>
							<th scope="col" style="width: 100px;">
								借款单号
							</th>
							<th scope="col">
								备注
							</th>
						</tr>
					</table>
				</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
							<%# Container.DataItemIndex + 1 %>
						</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目" ItemStyle-Width="110px"><ItemTemplate>
							<%# Eval("PrjName") %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="发生日期" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
							<%# (Eval("OccurredDate") != null) ? DataBinder.Eval(Container.DataItem, "OccurredDate", "{0:yyyy-MM-dd}") : "" %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="经手人" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
							<%# Eval("v_xm") %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="70px"><ItemTemplate>
							<%# (Eval("LoanFund") != null) ? DataBinder.Eval(Container.DataItem, "LoanFund", "{0:F3}") : "" %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="是否还清" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
							<%# (Eval("ReturnState").ToString() == "0") ? "未还清" : "已还清" %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="借款单号" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
							<%# Eval("LoanCode") %>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
							<%# Eval("Remark") %>
						</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
		</div>
		<div class="newdiv">
			项目还款
		</div>
		<asp:GridView ID="GridView5" CssClass="gvdata" AutoGenerateColumns="false" runat="server">
<EmptyDataTemplate>
				<table class="gvdata" id="emptyContractType5">
					<tr class="header">
						<th scope="col" style="width: 25px;">
							序号
						</th>
						<th scope="col" style="width: 110px;">
							项目
						</th>
						<th scope="col" style="width: 70px;">
							发生日期
						</th>
						<th scope="col" style="width: 50px;">
							经手人
						</th>
						<th scope="col" style="width: 70px;">
							归还本金
						</th>
						<th scope="col" style="width: 100px;">
							归还利息及其他
						</th>
						<th scope="col" style="width: 100px;">
							借款单号
						</th>
						<th scope="col">
							备注
						</th>
					</tr>
				</table>
			</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
						<%# Container.DataItemIndex + 1 %>
					</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目" ItemStyle-Width="110px"><ItemTemplate>
						<%# Eval("PrjName") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="发生日期" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
						<%# (Eval("FR_Time") != null) ? DataBinder.Eval(Container.DataItem, "FR_Time", "{0:yyyy-MM-dd}") : "" %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="经手人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px"><ItemTemplate>
						<%# Eval("v_xm") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="归还本金" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="70px"><ItemTemplate>
						<%# (Eval("FR_Money") != null) ? DataBinder.Eval(Container.DataItem, "FR_Money", "{0:F3}") : "" %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="归还利息及其他" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
						<%# (Convert.ToDecimal(Eval("FR_interest").ToString()) + Convert.ToDecimal(Eval("FR_deduct").ToString())).ToString() %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="借款单号" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px"><ItemTemplate>
						<%# Eval("LoanCode") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
						<%# Eval("FR_remark") %>
					</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
		<div class="newdiv">
			应支合同额
		</div>
		<asp:GridView ID="GridView6" CssClass="gvdata" AutoGenerateColumns="false" runat="server">
<EmptyDataTemplate>
				<table class="gvdata" id="emptyContractType6">
					<tr class="header">
						<th scope="col" style="width: 25px;">
							序号
						</th>
						<th scope="col" style="width: 110px;">
							项目
						</th>
						<th scope="col" style="width: 70px;">
							发生日期
						</th>
						<th scope="col" style="width: 50px;">
							经手人
						</th>
						<th scope="col" style="width: 70px;">
							金额
						</th>
						<th scope="col" style="width: 100px;">
							合同
						</th>
						<th scope="col">
							备注
						</th>
					</tr>
				</table>
			</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
						<%# Container.DataItemIndex + 1 %>
					</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目" ItemStyle-Width="110px"><ItemTemplate>
						<%# Eval("PrjName") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="发生日期" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
						<%# (Eval("OccurredDate") != null) ? DataBinder.Eval(Container.DataItem, "OccurredDate", "{0:yyyy-MM-dd}") : "" %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="经手人" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
						<%# Eval("v_xm") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="70px"><ItemTemplate>
						<%# (Eval("PaymentMoney") != null) ? DataBinder.Eval(Container.DataItem, "PaymentMoney", "{0:F3}") : "" %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
						<%# Eval("ContractName") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
						<%# Eval("Notes") %>
					</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
		<div class="newdiv">
			应支间接费用
		</div>
		<asp:GridView ID="GridView7" CssClass="gvdata" AutoGenerateColumns="false" runat="server">
<EmptyDataTemplate>
				<table class="gvdata" id="emptyContractType7">
					<tr class="header">
						<th scope="col" style="width: 25px;">
							序号
						</th>
						<th scope="col" style="width: 110px;">
							项目
						</th>
						<th scope="col" style="width: 70px;">
							发生日期
						</th>
						<th scope="col" style="width: 50px;">
							经手人
						</th>
						<th scope="col" style="width: 70px;">
							金额
						</th>
						<th scope="col" style="width: 100px;">
							费用名称
						</th>
						<th scope="col">
							备注
						</th>
					</tr>
				</table>
			</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
						<%# Container.DataItemIndex + 1 %>
					</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目" ItemStyle-Width="110px">
<ItemTemplate>
						<%# Eval("PrjName") %>
					</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="发生日期" ItemStyle-Width="70px" HeaderStyle-HorizontalAlign="Center">
<ItemTemplate>
						<%# (Eval("OccurredDate") != null) ? DataBinder.Eval(Container.DataItem, "OccurredDate", "{0:yyyy-MM-dd}") : "" %>
					</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="经手人" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
						<%# Eval("InputUser") %>
					</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="70px">
<ItemTemplate>
						<%# (Eval("Amount") != null) ? DataBinder.Eval(Container.DataItem, "Amount", "{0:F3}") : "" %>
					</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="费用名称" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
<ItemTemplate>
						<%# Eval("Name") %>
					</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
					</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
		<div class="newdiv">
			支付合同额
		</div>
		<asp:GridView ID="GridView8" CssClass="gvdata" AutoGenerateColumns="false" runat="server">
<EmptyDataTemplate>
				<table class="gvdata" id="emptyContractType8">
					<tr class="header">
						<th scope="col" style="width: 25px;">
							序号
						</th>
						<th scope="col" style="width: 110px;">
							项目
						</th>
						<th scope="col" style="width: 70px;">
							发生日期
						</th>
						<th scope="col" style="width: 50px;">
							经手人
						</th>
						<th scope="col" style="width: 70px;">
							金额
						</th>
						<th scope="col" style="width: 100px;">
							合同
						</th>
						<th scope="col">
							备注
						</th>
					</tr>
				</table>
			</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
						<%# Container.DataItemIndex + 1 %>
					</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目" ItemStyle-Width="110px"><ItemTemplate>
						<%# Eval("PrjName") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="发生日期" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
						<%# (Eval("OccurredDate") != null) ? DataBinder.Eval(Container.DataItem, "OccurredDate", "{0:yyyy-MM-dd}") : "" %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="经手人" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
						<%# PageHelper.QueryUser(this, Eval("PayOutPeople").ToString()) %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="70px"><ItemTemplate>
						<%# (Eval("PayOutMoney") != null) ? DataBinder.Eval(Container.DataItem, "PayOutMoney", "{0:F3}") : "" %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
						<%# Eval("ContName") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
					</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
		<div class="newdiv">
			支付间接费用
		</div>
		<asp:GridView ID="GridView9" CssClass="gvdata" AutoGenerateColumns="false" runat="server">
<EmptyDataTemplate>
				<table class="gvdata" id="emptyContractType9">
					<tr class="header">
						<th scope="col" style="width: 25px;">
							序号
						</th>
						<th scope="col" style="width: 110px;">
							项目
						</th>
						<th scope="col" style="width: 70px;">
							发生日期
						</th>
						<th scope="col" style="width: 50px;">
							经手人
						</th>
						<th scope="col" style="width: 70px;">
							金额
						</th>
						<th scope="col" style="width: 100px;">
							费用名称
						</th>
						<th scope="col">
							备注
						</th>
					</tr>
				</table>
			</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
						<%# Container.DataItemIndex + 1 %>
					</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目" ItemStyle-Width="110px"><ItemTemplate>
						<%# Eval("PrjName") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="发生日期" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
						<%# (Eval("OccurredDate") != null) ? DataBinder.Eval(Container.DataItem, "OccurredDate", "{0:yyyy-MM-dd}") : "" %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="经手人" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
						<%# Eval("InputUser") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="70px"><ItemTemplate>
						<%# (Eval("PayOutMoney") != null) ? DataBinder.Eval(Container.DataItem, "PayOutMoney", "{0:F3}") : "" %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="费用名称" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
						<%# Eval("Name") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
					</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
	</div>
	</form>
</body>
</html>
