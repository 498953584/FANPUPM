<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PettyCashClearDetail.aspx.cs" Inherits="PettyCash_PettyCashClearDetail" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>备用金清理</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <style type="text/css" media="print">
		.noprint
		{
			display: none;
		}
	</style>
	<style type="text/css">
		.tdLeft
		{
			text-align: left;
		}
		.tbrep
		{
			width: 100%;
		}
		.tbrep tr td
		{
			border: solid 1px Black;
			height: 22px;
		}
		.headerColor
		{
			color: Black;
			text-align: center;
			white-space: nowrap;
		}
	</style>
</head>
<body id="print">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top; height: auto;">
		<tr>
			<td class="divHeader">
				备用金还款
				<div style="height: 20px; width: 100%; left: 0px; top: 0px; text-align: right; position: absolute;">
					<input type="button" id="btnPrint" style="float: right;" class="noprint" onclick="winPrint()" value=" 打 印 "  />
				</div>
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
				<table cellpadding="0" cellspacing="0" class="viewTable">
					<tr>
						<td class="descTd">
							 申请日期
						</td>
						<td class="elemTd">
							<asp:Label ID="lblDatetime" ReadOnly="true" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							  申请金额
						</td>
						<td class="elemTd">
							<asp:Label ID="lblMoney" ReadOnly="true" runat="server"></asp:Label>     
						</td>
					</tr>
					<tr>
						<td class="descTd">
							   事项
						</td>
						<td class="elemTd">
							<asp:Label ID="lblMatter" ReadOnly="true" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							  用款日期
						</td>
						<td class="elemTd">
							
                              <asp:Label ID="lblCashDate" ReadOnly="true" runat="server"></asp:Label>       
						</td>
					</tr>
					<tr>
						<td class="descTd">
							  预报金额
						</td>
						<td class="elemTd">
							 <asp:Label ID="lblAmount" ReadOnly="true" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							未报金额
						</td>
						<td class="elemTd">
							<asp:Label ID="lblReportMoney" ReadOnly="true" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							核销金额
						</td>
						<td class="elemTd">
							<asp:Label ID="lblAuditAmount" ReadOnly="true" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							 欠款金额
						</td>
						<td class="elemTd">
							<asp:Label ID="lblOweMoney" ReadOnly="true" runat="server"></asp:Label>
						</td>
					</tr>
                    <tr>
						<td class="descTd">
							还款金额
						</td>
						<td class="elemTd">
							 <asp:Label ID="lblReturnMoney" ReadOnly="true" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							项目
						</td>
						<td class="elemTd">
							<asp:Label ID="lblProject" ReadOnly="true" runat="server"></asp:Label>
						</td>
					</tr>
				</table>
			</td>
		</tr>
        </table>
	</form>
</body>
</html>
