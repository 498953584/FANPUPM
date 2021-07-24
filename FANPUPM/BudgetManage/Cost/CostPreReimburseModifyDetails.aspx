<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostPreReimburseModifyDetails.aspx.cs" Inherits="BudgetManage_Cost_CostPreReimburseModifyDetails" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>预报销申请查看</title>
     <script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        function fillTotalAmount(begintotal, aftertotal) {
            var trText = '<tr><td colspan="3" align="center">合计</td><td>' + begintotal + '</td><td colspan="3">' + aftertotal + '</td></tr>';
            if ($('#gvModifyDetails')) {
                var tab = $('#gvModifyDetails').get(0);
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
				预报销变更申请查看
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
					预报销变更申请明细
				</div>
				<asp:GridView CssClass="gvdata" ID="gvModifyDetails" AutoGenerateColumns="false" OnRowDataBound="gvModifyDetails_RowDataBound" DataKeyNames="Id,CBSCode" runat="server">
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
									预算金额
								</th>
                                <th scope="col">
									变更金额
								</th>
                                 <th scope="col">
									变更原因
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
</asp:TemplateField><asp:TemplateField HeaderText="名称"><ItemTemplate>
								<%# Eval("Name").ToString() %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="成本科目"><ItemTemplate>
								<%# CBSName(Eval("CBSCode").ToString()) %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="预算金额"><ItemTemplate>
								<%# Eval("BeginCost").ToString() %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="变更金额"><ItemTemplate>
								<%# Eval("AfterCost").ToString() %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="变更原因"><ItemTemplate>
								<%# Eval("ModifyReason").ToString() %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
								<%# Eval("Note") %>
							</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
			</td>
		</tr>
		<tr id="trAudit" style="vertical-align: top;">
			<td>
				<MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="170" BusiClass="001" runat="server" />
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
