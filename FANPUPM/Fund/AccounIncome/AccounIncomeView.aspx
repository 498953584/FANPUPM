<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccounIncomeView.aspx.cs" Inherits="Fund_AccounIncome_AccounIncomeView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>入账查看</title>
	<style type="text/css" media="print">
		.noprint
		{
			display: none;
		}
	</style>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			var Value = document.getElementById("hdnsubject").value;
			if (Value != "0") {
				document.getElementById("td_word").style.display = "none";
				document.getElementById("td_txt").style.display = "none";
				document.getElementById("TR_Cont").style.display = "none";
				document.getElementById("TR_Money").style.display = "none";
			}
		});
	</script>
</head>
<body id="print">
	<form id="form1" runat="server">
	<table class="tab" style="vertical-align: top;">
		<tr>
			<td class="divHeader">
				入账查看
				<div style="height: 20px; width: 100%; left: 0px; top: 0px; text-align: right; position: absolute;">
					<input type="button" style="float: right;" class="noprint" onclick="winPrint()" value=" 打 印 " />
				</div>
			</td>
		</tr>
		<tr style="height: 1px;">
			<td>
				<table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style: none;"
					class="viewTable">
					<tr>
						<td style="border-style: none;">
							制单人:&nbsp;&nbsp;<asp:Label ID="lblPrintPeople" runat="server"></asp:Label>
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
				<table border="0px" cellpadding="0" cellspacing="0" class="viewTable">
					<tr>
						<td class="descTd">
							入账单编号
						</td>
						<td class="elemTd">
							<asp:Label ID="lblcode" Text="" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							项目
						</td>
						<td class="elemTd">
							<asp:Label ID="lblProject" Text="" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							入账类型
						</td>
						<td class="elemTd">
							<asp:Label ID="lblsubject" Text="" runat="server"></asp:Label>
							<asp:HiddenField ID="hdnsubject" runat="server" />
						</td>
						<td class="descTd">
							<div id="td_word">
								所属合同收款</div>
						</td>
						<td class="elemTd">
							<div id="td_txt">
								<asp:Label ID="lblContName" Text="" runat="server"></asp:Label></div>
						</td>
					</tr>
					<tr id="TR_Cont">
						<td class="descTd">
							所属计划
						</td>
						<td class="elemTd">
							<asp:Label ID="lblPlan" Text="" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							计划金额
						</td>
						<td class="elemTd">
							<asp:Label ID="lblPlanMoney" Text="" runat="server"></asp:Label>
						</td>
					</tr>
					<tr id="TR_Money">
						<td class="descTd">
							回款金额
						</td>
						<td class="elemTd">
							<asp:Label ID="lblGetMoney" Text="" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							所属合同
						</td>
						<td class="elemTd">
							<asp:Label ID="lblContractName" Text="" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							入账日期
						</td>
						<td class="elemTd">
							<asp:Label ID="DateGetTime" Text="" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							入帐金额
						</td>
						<td class="elemTd">
							<asp:Label ID="lblInMoney" Text="" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							附件
						</td>
						<td class="elemTd" id="upload" colspan="3" runat="server">
						</td>
					</tr>
					<tr>
						<td class="descTd">
							入账人
						</td>
						<td class="elemTd">
							<asp:Label ID="lblInPeople" Text="" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							入账日期
						</td>
						<td class="elemTd">
							<asp:Label ID="lblInDate" Text="" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							备注
						</td>
						<td colspan="3" style="word-break: break-all;">
							<asp:Label ID="lblRemark" runat="server"></asp:Label>
						</td>
					</tr>
				</table>
				<table style="width: 100%; height: 50%; margin-top: 10px;" border="0">
					<tr id="trAudit" style="vertical-align: top;">
						<td>
							<MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="093" BusiClass="001" runat="server" />
						</td>
					</tr>
				</table>
				<input id="hdnCode" type="hidden" runat="server" />

			</td>
		</tr>
		<tr>
			<td>
			</td>
		</tr>
	</table>
	</form>
	<input type="hidden" id="hdnAccountID" runat="server" />

</body>
</html>
