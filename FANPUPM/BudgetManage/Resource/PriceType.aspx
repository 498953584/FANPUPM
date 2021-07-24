<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PriceType.aspx.cs" Inherits="BudgetManage_PriceType" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>价格类型</title><link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			var priceType = new JustWinTable('gvwPriceType');
			registerAddPriceTypeEvent();
			registerUpdatePriceTypeEvent();
			registerDeletePriceTypeEvent();
			setButton(priceType, 'btnDelete', 'btnUpdate', 'btnQuery', 'hfldPriceTypeId');
			replaceEmptyTable('emptyContractType', 'gvwPriceType');
			$('#btnQuery').css('display', 'none');
			//控制权限按钮
			priceType.registClickTrListener(function () {
				document.getElementById('btnPrivilege').removeAttribute('disabled', 'disabled');
			});
			priceType.registClickSingleCHKListener(function () {
				if (priceType.getCheckedChk().length == 1) {
					document.getElementById('btnPrivilege').removeAttribute('disabled', 'disabled');
				}
				else {
					document.getElementById('btnPrivilege').setAttribute('disabled', 'disabled');
				}
			});
			priceType.registClickAllCHKLitener(function () {
				document.getElementById('btnPrivilege').setAttribute('disabled', 'disabled');
			});

		});

		function registerAddPriceTypeEvent() {
			addEvent(document.getElementById('btnAdd'), 'click', function () {
				var url = '/BudgetManage/Resource/PriceTypeEdit.aspx';
				top.ui.openWin({ title: '价格类型', url: url, width: 500, height: 230 });
			});
		}
		function registerUpdatePriceTypeEvent() {
		    addEvent(document.getElementById('btnUpdate'), 'click', function () {
		        var priceTypeIds = document.getElementById('hfldPriceTypeId').value;
		        var url = '/BudgetManage/Resource/PriceTypeEdit.aspx?Action=Update&PriceTypeId=' + priceTypeIds
		        top.ui.openWin({ title: '价格类型', url: url, width: 500, height: 230 });
		    });
		}
		function registerDeletePriceTypeEvent() {
			document.getElementById('btnDelete').onclick = function () {
				if (!confirm("确定要删除吗？")) {
					return false;
				}
			}
		}

		function allocUser() {
			//			var url = '/Common/AllocUser.aspx?type=basicPriv&tableName=Res_PriceType&idJson=' + $('#hfldPriceTypeId').val();
			var url = "/Common/DivSetRole.aspx?tbName=Res_PriceType&idName=PriceTypeId&field=UserCodes&id=" + $('#hfldPriceTypeId').val();
			top.ui.openWin({ title: '选择人员', url: url });
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divHeader">
		<table class="tableHeader">
			<tr>
				<td>
					价格类型
				</td>
			</tr>
		</table>
	</div>
	<table cellpadding="0px" cellspacing="0px" style="width: 100%; vertical-align: top;">
		<tr>
			<td class="divFooter" style="text-align: left;">
				<input type="button" id="btnAdd" value="新增" />
				<input type="button" id="btnUpdate" value="编辑" disabled="disabled" />
				<asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
				<input type="button" id="btnQuery" disabled="disabled" value="查看" />
				<input type="button" id="btnPrivilege" disabled="disabled" value="权限" onclick="allocUser();" />
			</td>
		</tr>
		<tr style="vertical-align: top; height: 70%">
			<td>
				<asp:GridView ID="gvwPriceType" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwPriceType_RowDataBound" DataKeyNames="PriceTypeId" runat="server">
<EmptyDataTemplate>
						<table id="emptyContractType" class="gvdata">
							<tr class="header">
								<th scope="col" style="width: 20px;">
									<input id="chk1" type="checkbox" />
								</th>
								<th scope="col" style="width: 25px;">
									序号
								</th>
								<th scope="col" style="width: 300px">
									类型名称
								</th>
								<th scope="col">
									备注
								</th>
							</tr>
						</table>
					</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
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
</asp:TemplateField><asp:TemplateField HeaderText="类型名称" HeaderStyle-Width="300px"><ItemTemplate>
								<%# StringUtility.GetStr(Eval("PriceTypeName").ToString()) %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
								<%# StringUtility.GetStr(Eval("Note"), 60) %>
							</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
			</td>
		</tr>
	</table>
	<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="true" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
	</webdiyer:AspNetPager>
	<asp:HiddenField ID="hfldPriceTypeId" runat="server" />
	<div id="divFramRole" title="" style="display: none">
		<iframe id="iframeRole" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	</form>
</body>
</html>
