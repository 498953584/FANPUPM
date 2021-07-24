<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RefuelApplyView.aspx.cs" Inherits="Equ_ShipOilWear_RefuelApplyView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>加油申请查看</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$('#chkIsEntrustPurchase').attr('disabled', 'disabled');
		});
	</script>
</head>
<body id="print1">
	<form id="form1" runat="server">
	<table class="tab" style="vertical-align: top;">
		<tr style="height: 1px;">
			<td class="divHeader">
				加油申请查看
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
						<td class="descTd">
							申请加油船
						</td>
						<td class="elemTd">
							<asp:Label ID="lblApplyEquCode" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							项目
						</td>
						<td class="elemTd">
							<asp:Label ID="lblPrjName" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							油耗预算
						</td>
						<td class="elemTd">
							<asp:Label ID="lblBudOilWearCode" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							分项工程
						</td>
						<td class="elemTd">
							<asp:Label ID="lblTaskCode" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							至今累计加油数(吨)
						</td>
						<td class="elemTd">
							<asp:Label ID="lblRefuelTotal" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							累计完成工程量(m³)
						</td>
						<td class="elemTd">
							<asp:Label ID="lblCompletedQuantity" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							定额油耗(KG/m³)
						</td>
						<td class="elemTd">
							<asp:Label ID="lblQuotaOilWear" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							至今应该加油数(吨)
						</td>
						<td class="elemTd">
							<asp:Label ID="lblShouldRefuel" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							差异
						</td>
						<td class="elemTd">
							<asp:Label ID="lblDifference" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							现有库存量(吨)
						</td>
						<td class="elemTd">
							<asp:Label ID="lblStockQuantity" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							原因分析
						</td>
						<td colspan="3" style="word-break: break-all;">
							<asp:Label ID="lblReason" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							预计完成工程量(m³)
						</td>
						<td class="elemTd">
							<asp:Label ID="lblBudCompleteQuantity" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							本次申请数量(吨)
						</td>
						<td class="elemTd">
							<asp:Label ID="lblApplyQuantity" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							挖深(m)
						</td>
						<td class="elemTd">
							<asp:Label ID="lblSump" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							油品种类型号
						</td>
						<td class="elemTd">
							<asp:Label ID="lblOilsType" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							委托采购
						</td>
						<td class="elemTd">
							<asp:CheckBox ID="chkIsEntrustPurchase" runat="server" />
						</td>
						<td class="descTd">
							申请船主
						</td>
						<td class="elemTd">
							<asp:Label ID="lblApplyMaster" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							申请加油日期
						</td>
						<td class="elemTd">
							<asp:Label ID="lblApplyRefuelDate" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							申请加油地点
						</td>
						<td class="elemTd">
							<asp:Label ID="lblApplyRefuelPlace" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							施工日期
						</td>
						<td class="elemTd">
							<asp:Label ID="lblConstructionDate" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							累计施工时间
						</td>
						<td class="elemTd">
							<asp:Label ID="lblTotalConstructionDates" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							供油船名
						</td>
						<td class="elemTd">
							<asp:Label ID="lblFueler" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							船东
						</td>
						<td class="elemTd">
							<asp:Label ID="lblFuelerOwner" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							现场负责人
						</td>
						<td class="elemTd">
							<asp:Label ID="lblLocaleLeader" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							现场负责人电话
						</td>
						<td class="elemTd">
							<asp:Label ID="lblLeaderPhone" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							备注
						</td>
						<td colspan="3" style="word-break: break-all;">
							<asp:Label ID="lblNotes" runat="server"></asp:Label>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr id="trAudit" style="vertical-align: top;">
			<td>
				<MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiClass="001" BusiCode="143" runat="server" />
			</td>
		</tr>
		<tr>
		</tr>
	</table>
	</form>
</body>
</html>
