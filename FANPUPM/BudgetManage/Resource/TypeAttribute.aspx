<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TypeAttribute.aspx.cs" Inherits="BudgetManage_Resource_TypeAttribute" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>类别属性</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var typeAttribute = new JustWinTable('gvwTypeAttribute');
			setButton(typeAttribute, 'btnDelete', 'btnUpdate', 'btnQuery', 'hfldAttributeId');
			registerAddAttributeEvent();
			registerUpdateAttributeEvent();
			registerDeleteAttributeEvent();
		});

		function registerAddAttributeEvent() {
			addEvent(document.getElementById('btnAdd'), 'click', function () {
				var typeId = jw.getRequestParam('ResourceTypeId');
				var url = '/BudgetManage/Resource/TypeAttributeEdit.aspx?Action=Add&ResourceTypeId=' + typeId;
				top.ui.openWin({ title: '资源属性', url: url, width: 400, height: 220 });
			});
		}
		function registerUpdateAttributeEvent() {
			addEvent(document.getElementById('btnUpdate'), 'click', function () {
				var attributeId = document.getElementById('hfldAttributeId').value;
				var typeId = jw.getRequestParam('ResourceTypeId');

				var url = '/BudgetManage/Resource/TypeAttributeEdit.aspx?Action=Update&AttributeId=' +
                    attributeId + '&ResourceTypeId=' + typeId;
				top.ui.openWin({ title: '资源属性', url: url, width: 400, height: 220 });
			});

		}
		function registerDeleteAttributeEvent() {
			document.getElementById('btnDelete').onclick = function () {
				if (!confirm("确定要删除吗？")) {
					return false;
				}
			}
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divHeader">
		<table class="tableHeader">
			<tr>
				<td>
					类别属性
				</td>
			</tr>
		</table>
	</div>
	<table cellpadding="0" cellspacing="0" style="width: 100%; height: 95%; border-style: none;">
		<tr style="vertical-align: top;">
			<td style="width: 220px; vertical-align: top; height: 100%;">
				<asp:TreeView ID="trvwResourceType" ShowLines="true" OnSelectedNodeChanged="trvwResourceType_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
			</td>
			<td style="border-left: solid 2px #CADEED; padding-left: 1px;">
				<table cellpadding="0px" cellspacing="0px" style="width: 100%; vertical-align: top;">
					<tr>
						<td class="divFooter" style="text-align: left;">
							<input type="button" id="btnAdd" value="新增" />
							<input type="button" id="btnUpdate" value="编辑" disabled="disabled" />
							<asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
							<input type="button" id="btnQuery" style="display: none" disabled="disabled" value="查看" />
						</td>
					</tr>
					<tr>
						<td>
							<asp:GridView ID="gvwTypeAttribute" CssClass="gvdata" AutoGenerateColumns="false" Height="100%" OnRowDataBound="gvwTypeAttribute_RowDataBound" DataKeyNames="AttributeId" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
											<asp:CheckBox ID="chkSelectAll" runat="server" />
										</HeaderTemplate>

<ItemTemplate>
											<asp:CheckBox ID="chkSelectSingle" runat="server" />
										</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
											<%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
										</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="名称"><ItemTemplate>
											<%# StringUtility.GetStr(Eval("AttributeName").ToString(), 80) %>
										</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="true" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
							</webdiyer:AspNetPager>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hfldAttributeId" runat="server" />
	</form>
</body>
</html>
