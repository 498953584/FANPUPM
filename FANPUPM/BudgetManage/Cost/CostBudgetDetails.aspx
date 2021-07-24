<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostBudgetDetails.aspx.cs" Inherits="BudgetManage_Cost_CostBudgetDetails" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>费用预算申请查看</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        //合计
        function fillTotalAmount(total) {
            var trText = '<tr><td colspan="3" align="center">合计</td><td colspan="4">' + total + '</td></tr>';
            if ($('#gvDiaryDetails')) {
                var tab = $('#gvDiaryDetails').get(0);
                var lastRowId = tab.rows[tab.rows.length - 1].id;
                $('#' + lastRowId).after(trText);
            }
        }
        
	</script>
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
				费用预算申请查看
                <input type="button" style="float: right;" class="noprint" onclick="winPrint()" value=" 打 印 " />
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
							<asp:Label ID="lblPrjNameTitle" Text="项目" runat="server"></asp:Label>
						</td>
						<td class="elemTd">
							<asp:Label ID="lblPrjName" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							费用名称
						</td>
						<td class="elemTd">
							<asp:Label ID="lblName" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							<asp:Label ID="lblPrjCodeTitle" Text="项目编号" runat="server"></asp:Label>
						</td>
						<td class="elemTd">
							<asp:Label ID="lblPrjCode" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							费用编号
						</td>
						<td class="elemTd">
							<asp:Label ID="lblCode" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							申请时间
						</td>
						<td class="elemTd">
							<asp:Label ID="lblInputDate" runat="server"></asp:Label>
						</td>
						<td class="descTd">
							填报人
						</td>
						<td class="elemTd">
							<asp:Label ID="lblInputUser" runat="server"></asp:Label>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr style="vertical-align: top; height: 1px;">
			<td style="vertical-align: top;">
				<div class="groupInfo" style="text-align: center; margin-bottom: 5px; margin-top: 5px;">
					费用预算申请明细
				</div>
				<asp:GridView CssClass="gvdata" ID="gvDiaryDetails" AutoGenerateColumns="false" OnRowDataBound="gvDiaryDetails_RowDataBound" DataKeyNames="Id,CBSCode" runat="server">
<EmptyDataTemplate>
						<table id="gvEmpty" class="gvdata">
							<tr class="header">
								<th scope="col" style="width: 20px;">
									序号
								</th>
								<th scope="col">
									名称
								</th>
								<th scope="col">
									成本科目
								</th>
                                <th scope="col">
									金额
								</th>
								<th scope="col">
									备注
								</th>
							</tr>
						</table>
					</EmptyDataTemplate>
<EmptyDataRowStyle CssClass="header"></EmptyDataRowStyle><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="20px">
<ItemTemplate>
								<%# Container.DataItemIndex + 1 %>
							</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="名称" DataField="Name" /><asp:TemplateField HeaderText="成本科目"><ItemTemplate>
								<%# CBSName(Eval("CBSCode").ToString()) %>
							</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="金额" DataField="Cost" /><asp:TemplateField HeaderText="备注"><ItemTemplate>
								<%# Eval("Note") %>
							</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
			</td>
		</tr>
		<tr style="vertical-align: top; height: 1px;">
			<td style="vertical-align: top;">
				<div class="groupInfo" style="text-align: center; margin-bottom: 5px; margin-top: 5px;">
					费用预算申请对比表
				</div>
				<asp:Repeater ID="rptContrast" OnItemDataBound="rptContrast_ItemDataBound" runat="server">
<HeaderTemplate>
						<table class="tbrep" id="rptContrast" cellpadding="4" cellspacing="0">
							<tr class="header headerColor">
								<td style="width: 30px;">
									序号
								</td>
								<td>
									名称
								</td>
								<td>
									成本科目
								</td>
								<td>
									总预算金额
								</td>
								<td>
									月度预算金额
								</td>
								<td>
									已发生金额
								</td>
								<td>
									本次申请金额
								</td>
							</tr>
					</HeaderTemplate>

<ItemTemplate>
						<tr class="rowa" id="<%# Container.ItemIndex + 1 %>">
							<td style="width: 20px; text-align: right;">
								<%# Container.ItemIndex + 1 %>
							</td>
							<td>
								<%# Eval("Name") %>
							</td>
							<td>
								<%# Eval("CBSName") %>
							</td>
							<td>
								<%# Eval("PrjAmount") %>
							</td>
							<td>
								<%# Eval("MonthAmount") %>
							</td>
							<td>
								<%# Eval("PrjAlreadyAmount") %>
							</td>
							<td>
								<%# Eval("ThisAmount") %>
							</td>
						</tr>
					</ItemTemplate>

<AlternatingItemTemplate>
						<tr class="rowb" id="<%# Container.ItemIndex + 1 %>">
							<td style="width: 20px; text-align: right;">
								<%# Container.ItemIndex + 1 %>
							</td>
							<td>
								<%# Eval("Name") %>
							</td>
							<td>
								<%# Eval("CBSName") %>
							</td>
							<td>
								<%# Eval("PrjAmount") %>
							</td>
							<td>
								<%# Eval("MonthAmount") %>
							</td>
							<td>
								<%# Eval("PrjAlreadyAmount") %>
							</td>
							<td>
								<%# Eval("ThisAmount") %>
							</td>
						</tr>
					</AlternatingItemTemplate>

<FooterTemplate>
						<tr>
							<td colspan="3" align="center">
								合计
							</td>
							<td>
								<asp:Label ID="lblPrjAmount" Text="aa" runat="server"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblMonthAmount" runat="server"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblPrjAlreadyAmount" runat="server"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblThisAmount" runat="server"></asp:Label>
							</td>
						</tr>
						</table>
					</FooterTemplate>
</asp:Repeater>
			</td>
		</tr>
		<tr id="trAudit" style="vertical-align: top;">
			<td>
				<MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="168" BusiClass="001" runat="server" />
			</td>
		</tr>
	</table>
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    <asp:HiddenField ID="hfldPrjId" runat="server" />
	<asp:HiddenField ID="hfldYear" runat="server" />
	<asp:HiddenField ID="hfldPrjName" runat="server" />
    </form>
</body>
</html>
