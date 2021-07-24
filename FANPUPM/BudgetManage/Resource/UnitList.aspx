<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnitList.aspx.cs" Inherits="BudgetManage_Resource_UnitList" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>资源单位设置</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var jwTable = new JustWinTable('gvResource');
			setButton(jwTable, 'btnDel', 'btnEdit', 'btnLook', 'hfldPurchaseChecked');
			addEvent(document.getElementById('btnEdit'), 'click', rowEdit);
			addEvent(document.getElementById('btnLook'), 'click', rowQuery);
			addEvent(document.getElementById('btnAdd'), 'click', rowAdd);

			function rowEdit() {
				var url = "/BudgetManage/Resource/UnitEdit.aspx?id=" + document.getElementById("hfldPurchaseChecked").value;
				top.ui.openWin({ title: '资源单位', url: url, width: 400, height: 200 });
			}
			function rowAdd() {
				var url = "/BudgetManage/Resource/UnitEdit.aspx";
				top.ui.openWin({ title: '资源单位', url: url, width: 400, height: 200 });
			}
			function rowQuery() {
				var url = "/BudgetManage/Resource/AddIncometBalance.aspx?t=1&id=" + document.getElementById("hfldPurchaseChecked").value;
				winopen(url)
			}
		});
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divHeader">
		<table class="tableHeader">
			<tr>
				<td>
					<asp:Label ID="lblTitle" Font-Bold="true" Text="资源单位设置" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<table style="width: 100%;">
		<tr>
			<td style="vertical-align: top;">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							单位名称
						</td>
						<td>
							<asp:TextBox ID="txtName" Width="120px" runat="server"></asp:TextBox>
						</td>
						<td>
							<asp:Button ID="btnSearch" Style="cursor: pointer;" Text="查询" OnClick="btnSearch_Click" runat="server" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td style="width: 100%; vertical-align: top;">
				<table border="0" class="tab">
					<td style="height: 20px;" class="bottonrow">
						<div class="divFooter" style="text-align: left;">
							<input type="button" id="btnAdd" value="新增" />
							<input type="button" id="btnEdit" disabled="disabled" value="编辑" />
							<asp:Button ID="btnDel" Text="删  除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗??')" OnClick="btnDel_Click" runat="server" />
							<input type="button" id="btnLook" style="display: none;" disabled="disabled" value="查看" />
						</div>
					</td>
		</tr>
		<tr>
			<td style="height: 100%; vertical-align: top;">
				<div>
					<asp:GridView ID="gvResource" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="UnitId" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
									<asp:CheckBox ID="cbAllBox" runat="server" />
								</HeaderTemplate><ItemTemplate>
									<asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Eval("UnitId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
									<%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位名称"><ItemTemplate>
									<%# Eval("name") %>
								</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="true" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
					</webdiyer:AspNetPager>
				</div>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	<asp:HiddenField ID="hdContractID" runat="server" />
	</form>
</body>
</html>
