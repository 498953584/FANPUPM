<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BalanceQuery.aspx.cs" Inherits="ContractManage_PayoutBalance_BalanceQuery" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
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
		#print
		{
			max-width: 670px;
			margin: 0 auto;
		}
	</style>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			registerChkDiffEvent();
		});
		function registerChkDiffEvent() {
			if (!document.getElementById('chkDiff')) return false;
			if (!document.getElementById('trSate')) return false;
			addEvent(document.getElementById('chkDiff'), 'click', function () {
				if (this.checked) {
					document.getElementById('trSate').className = '';
				}
				else {
					document.getElementById('trSate').className = 'noprint';
				}
			});
		}
	</script>
</head>
<body id="print1">
	<form id="form1" style="vertical-align: top; min-width: 800px;" runat="server">
	<table cellpadding="0" cellspacing="0" class="tab" style="vertical-align: top;">
		<tr>
			<td class="divHeader">
				支出合同结算
				<input type="button" id="Button1" style="float: right;" class="noprint" onclick="winPrint()"
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
				<table border="0px" cellpadding="0" cellspacing="0" class="viewTable" style="width: 100%;
					margin: auto;">
					<tr>
						<td colspan="4" class="groupInfo">
							合同基本信息
						</td>
					</tr>
					<tr>
						<td class="descTd">
							合同编号
						</td>
						<td class="elemTd">
							<asp:Label ID="lblContractCode" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							合同名称
						</td>
						<td class="elemTd">
							<asp:Label ID="lblContractName" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							合同类型
						</td>
						<td class="elemTd">
							<asp:Label ID="LblContractType" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							合同最终金额
						</td>
						<td class="elemTd">
							<asp:Label ID="lblContractMoney" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							甲方
						</td>
						<td class="elemTd">
							<asp:Label ID="LblAPart" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							乙方
						</td>
						<td class="elemTd">
							<asp:Label ID="lblBName" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							签订时间
						</td>
						<td class="elemTd">
							<asp:Label ID="lblSignedDate" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							签订地点
						</td>
						<td class="elemTd">
							<asp:Label ID="lblAddress" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td colspan="4" class="groupInfo">
							结算单信息
						</td>
					</tr>
					<tr>
						<td class="descTd">
							结算编号
						</td>
						<td class="elemTd">
							<asp:Label ID="lblBalanceCode" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							结算金额
						</td>
						<td class="elemTd">
							<asp:Label ID="lblBalanceMoney" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							结算方式
						</td>
						<td class="elemTd">
							<asp:Label ID="lblBalanceMode" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							差额
						</td>
						<td class="elemTd">
							<asp:Label ID="lblDiffAmount" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							结算对象
						</td>
						<td class="elemTd">
							<asp:Label ID="lblBalanceObj" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							付款方式
						</td>
						<td class="elemTd">
							<asp:Label ID="lblPayMode" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							结算人
						</td>
						<td class="elemTd">
							<asp:Label ID="lblBalancePerson" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							结算日期
						</td>
						<td class="elemTd">
							<asp:Label ID="lblBalanceDate" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							备注
						</td>
						<td colspan="3">
							<asp:Label ID="lblNotes" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							录入人
						</td>
						<td class="elemTd">
							<asp:Label ID="lblInputPerson" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							录入时间
						</td>
						<td class="elemTd">
							<asp:Label ID="lblInputDate" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							附件
						</td>
						<td colspan="3" style="padding-right: 0px;">
							<asp:Literal ID="Literal1" runat="server"></asp:Literal>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr id="trPurchase" style="vertical-align: top; height: 1px;">
			<td style="padding-top: 10px;">
				<div style="font-size: 13px; font-weight: bold; text-align: center;">
					<asp:Label ID="lblTitalPurchase" Font-Size="13px" Font-Bold="true" runat="server"></asp:Label></div>
				
				<asp:GridView ID="gvwPurchaseplanStock" AutoGenerateColumns="false" CssClass="viewTable" OnRowDataBound="gvwPurchaseplanStock_RowDataBound" DataKeyNames="ResourceCode" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="40px"><ItemTemplate>
								<%# Container.DataItemIndex + 1 %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资编号" HeaderStyle-Width="80px">
<ItemTemplate>
								<%# Eval("ResourceCode") %>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="物资名称">
<ItemTemplate>
								<%# Eval("ResourceName") %>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="规格" HeaderStyle-Width="40px">
<ItemTemplate>
								<%# Eval("Specification") %>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="40px">
<ItemTemplate>
								<%# Eval("Name") %>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="库存量" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
								<%# WebUtil.GetStockNumberByCode(Eval("ResourceCode").ToString()).ToString() %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="采购<br>数量" HeaderStyle-Width="40px">
<ItemTemplate>
								<%# Convert.ToDecimal(Eval("ContractNumber")).ToString("0.000") %>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="采购<br>价格" HeaderStyle-Width="40px">
<ItemTemplate>
								<%# Convert.ToDecimal(Eval("sprice")).ToString("0.000") %>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="本次结<br/>算数量" HeaderStyle-Width="35px">
<ItemTemplate>
                                <%# (Eval("ThisTimeArrivaledQuantity").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("ThisTimeArrivaledQuantity")).ToString("0.000") %>
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="本次结<br/>算金额" HeaderStyle-Width="35px">
<ItemTemplate>
                                <%# (Eval("ThisTimeTotal").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("ThisTimeTotal")).ToString("0.000") %>
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="已结<br/>算数量" HeaderStyle-Width="35px">
<ItemTemplate>
                                <%# (Eval("AllAlreadyBalanceQuantity").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("AllAlreadyBalanceQuantity")).ToString("0.000") %>
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="已结<br/>算金额" HeaderStyle-Width="35px">
<ItemTemplate>
                                <%# (Eval("AlreadyTotal").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("AlreadyTotal")).ToString("0.000") %>
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="应到而<br/>未到数量" HeaderStyle-Width="35px">
<ItemTemplate>
                                <%# (Eval("NoArrivaledQuantity").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("NoArrivaledQuantity")).ToString("0.000") %>
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="供应商" HeaderStyle-Width="100px">
<ItemTemplate>
								<%# Eval("CorpName") %>
								<asp:HiddenField ID="hfldCorp" Value='<%# Convert.ToString(Eval("corp")) %>' runat="server" />
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="型号" HeaderStyle-Width="40px">
<ItemTemplate>
								<%# Eval("ModelNumber") %>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="品牌" HeaderStyle-Width="40px">
<ItemTemplate>
								<%# Eval("brand") %>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数">
<ItemTemplate>
								<%# Eval("TechnicalParameter") %>
							</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="采购单号" HeaderStyle-Width="50px">
<ItemTemplate>
								<div style="width: 100%; word-break: break-all;">
									<%# Eval("pscode").ToString() %>
								</div>
							</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
			</td>
		</tr>
		<tr id="trSate" style="height: 1px;" runat="server"><td colspan="4" runat="server">
				<div>
					<div id="diffTitle" style="position: relative;" runat="server">
						指标对比表
						<div class="noprint" style="height: 20px; width: 100%; left: 0px; bottom: 0px; text-align: right;
							position: absolute; font-weight: normal;">
							<asp:CheckBox ID="chkDiff" Style="float: right;" Checked="true" Text="打印" runat="server" />
						</div>
					</div>
					<table cellpadding="0" cellspacing="0" class="viewTable" style="width: 100%; margin: auto;">
						<tr class="header">
							<td align="center">
								状态
							</td>
							<td align="center">
								最终合同额
							</td>
							<td align="center">
								已结算金额
							</td>
							<td align="center">
								本次结算金额
							</td>
							<td align="center">
								差额比例
							</td>
						</tr>
						<tr>
							<td align="center">
								<asp:Label ID="lblState" CssClass="alarm" Text="" runat="server"></asp:Label>
							</td>
							<td align="center">
								<asp:Label ID="lblModifedAmount" CssClass="alarm" Text="" runat="server"></asp:Label>
							</td>
							<td align="center">
								<asp:Label ID="lblBalancedAmount" CssClass="alarm" Text="" runat="server"></asp:Label>
							</td>
							<td align="center">
								<asp:Label ID="lblBalanceAmount" CssClass="alarm" Text="" runat="server"></asp:Label>
							</td>
							<td align="center">
								<asp:Label ID="lblRate" CssClass="alarm" Text="" runat="server"></asp:Label>
							</td>
						</tr>
						<tr>
							<td colspan="5" style="text-align: left">
								<div style="width: 12%; float: left">
									预警级别：
								</div>
								<div style="width: 88%; float: left">
									高：红色字体，表示已超；中：紫色字体；低：蓝色字体；
									<br />
									普通：黑色字体，表示未达到预警阀值，正常。
								</div>
							</td>
						</tr>
					</table>
				</div>
			</td></tr>
		<tr id="trAudit" style="vertical-align: top;">
			<td colspan="4">
				<div>
					<MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="083" BusiClass="001" runat="server" />
				</div>
			</td>
		</tr>
		<tr>
		</tr>
	</table>
	</form>
</body>
</html>
