<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractType.aspx.cs" Inherits="ContractManage_ContractType" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="cn.justwin.contractDAL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>合同类型</title><link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="../../Script/Budget/BudgetPait.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../Script/jquery.blockUI.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jquery.external/jquery.bgiframe-2.1.2.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			hideControls();
			var typeTable = new JustWinTable('gvwContractType');
			setButton(typeTable, 'btnDelete', 'btnUpdate', 'btnQuery', 'hfldContractTypeGuid')
			replaceEmptyTable();
			registerBtnAddEvent();
			registerBtnUpdateEvent();
			registerBtnDeleteEvent();
			registerBtnQueryEvent();
			registerBtnManageEvent();
			jw.tooltip();
		});

		function registerBtnAddEvent() {
			$('#btnAdd').click(function () {
				var url = '/ContractManage/ContractType/ContractTypeEdit.aspx?Action=Add';
				top.ui._ContractTypeEdit = window;
				toolbox_oncommand(url, "新增合同类型");
			});
		}

		function registerBtnUpdateEvent() {
			$('#btnUpdate').click(function () {
				var url = '/ContractManage/ContractType/ContractTypeEdit.aspx?Action=Update&TypeID=' + $('#hfldContractTypeGuid').val();
				top.ui._ContractTypeEdit = window;
				toolbox_oncommand(url, "编辑合同类型");
			});
		}

		function registerBtnDeleteEvent() {
			if (!document.getElementById('btnDelete')) return;
			var btnDelete = document.getElementById('btnDelete');
			btnDelete.onclick = function () {
				if (!confirm('系统提示：\n\n确定要删除吗？')) {
					return false;
				}
			}
		}

		function registerBtnQueryEvent() {
			if (!document.getElementById('btnQuery')) return;
			var btnQuery = document.getElementById('btnQuery');
			addEvent(btnQuery, 'click', function () {
				parent.desktop.ContractTypeEdit = window;
				var url = '/ContractManage/ContractType/ContractTypeEdit.aspx?Action=Query&TypeID=' + document.getElementById('hfldContractTypeGuid').value;
				toolbox_oncommand(url, "查看合同类型");
			})
		}

		// 管理
		function registerBtnManageEvent() {
			$('#btnManage').click(function () {
				var url = '/ContractManage/ContractType/ManageConType.aspx';
				top.ui.openWin({ title: '管理合同类型', url: url, width: 600, height: 485 });
			});
		}

		function CheckType(typeId) {
			parent.desktop.ContractTypeEdit = window;
			var url = '/ContractManage/ContractType/ContractTypeEdit.aspx?Action=Query&TypeID=' + typeId;
			toolbox_oncommand(url, "查看合同类型");
		}

		function replaceEmptyTable() {
			//当数据量为空时，修改样式
			if (!document.getElementById('gvwContractType')) return;
			if (!document.getElementById('emptyContractType')) return;
			var gvwContractType = document.getElementById('gvwContractType');
			var emptyContractType = document.getElementById('emptyContractType');
			if (gvwContractType.firstChild.childNodes.length == 1) {
				gvwContractType.replaceChild(emptyContractType.firstChild, gvwContractType.firstChild);
			}
		}

		function hideControls() {
			if (!document.getElementById('btnDataBind')) return;
			document.getElementById('btnDataBind').style.display = 'none';
		}
        
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table style="vertical-align: top; width: 100%; height: 100%;">
		<tr>
			<td style="width: 100%; vertical-align: top;">
				<table style="width: 100%">
					<tr>
						<td>
							<table class="queryTable" cellpadding="3px" cellspacing="0px">
								<tr>
									<td class="descTd">
										类型编码
									</td>
									<td>
										<asp:TextBox ID="txtTypeCode" Width="120px" runat="server"></asp:TextBox>
									</td>
									<td class="descTd">
										类型名称
									</td>
									<td>
										<asp:TextBox ID="txtTypeName" Width="120px" runat="server"></asp:TextBox>
									</td>
									<td>
										<asp:Button ID="btnSelect" Text="查询" OnClick="btnSelect_Click" runat="server" />
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr style="vertical-align: top;">
						<td class="divFooter" style="text-align: left;">
							<input type="button" id="btnAdd" value="新增" runat="server" />

							<input type="button" id="btnUpdate" value="编辑" disabled="disabled" />
							<asp:Button ID="btnDelete" Text="删除" Enabled="false" OnClick="btnDelete_Click" runat="server" />
							<input type="button" id="btnQuery" value="查看" disabled="disabled" />
							<input type="button" id="btnManage" value="管理" />
						</td>
					</tr>
					<tr>
						<td>
							<asp:GridView ID="gvwContractType" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" OnRowDataBound="gvwContractType_RowDataBound" DataKeyNames="TypeID" runat="server">
<EmptyDataTemplate>
									<table id="emptyContractType" class="gvdata">
										<tr class="header">
											<th scope="col" style="width: 20px;">
												<input id="chk1" type="checkbox" />
											</th>
											<th scope="col" style="width: 25px;">
												序号
											</th>
											<th scope="col">
												类型编码
											</th>
											<th scope="col">
												类型名称
											</th>
											<th>
												类型简写
											</th>
											<th scope="col">
												权限人员
											</th>
											<th scope="col" style="display: none;">
												成本科目
											</th>
											<th scope="col">
												录入人
											</th>
											<th scope="col">
												录入时间
											</th>
											<th scope="col">
												是否有效
											</th>
											<th scope="col">
												备注
											</th>
										</tr>
									</table>
								</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
											<asp:CheckBox ID="chkSelectAll" runat="server" />
										</HeaderTemplate><ItemTemplate>
											<asp:CheckBox ID="chkSelectSingle" runat="server" />
										</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
											<%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
										</ItemTemplate></asp:TemplateField><asp:BoundField DataField="TypeCode" HeaderText="类型编码" HeaderStyle-Width="100px" /><asp:TemplateField HeaderText="类型名称" HeaderStyle-Width="200px"><ItemTemplate>
											<span class="tooltip link" title='<%# Eval("TypeName") %>' onclick="CheckType('<%# Eval("TypeID") %>')">
												<%# StringUtility.GetStr(Eval("TypeName").ToString()) %>
											</span>
										</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型简写"><ItemTemplate>
											<%# Eval("TypeShort") %>
										</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="权限人员" HeaderStyle-Width="200px"><ItemTemplate>
											<span class="tooltip" title='<%# Eval("UserCodes") %>'>
												<%# StringUtility.GetStr(Eval("UserCodes").ToString()) %>
											</span>
										</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="成本科目" HeaderStyle-Width="100px"><ItemTemplate>
											<%# ContractType.GetCBSCodeName(Eval("CBSCode").ToString()) %>
										</ItemTemplate></asp:TemplateField><asp:BoundField DataField="InputPerson" HeaderText="录入人" HeaderStyle-Width="80px" /><asp:TemplateField HeaderText="录入时间" HeaderStyle-Width="80px"><ItemTemplate>
											<%# Common2.GetTime(Eval("InputDate").ToString()) %>
										</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="是否有效" HeaderStyle-Width="200px"><ItemTemplate>
											<%# Eval("IsValid") %>
										</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
											<span class="tooltip" title='<%# Eval("Notes") %>'>
												<%# StringUtility.GetStr(Eval("Notes").ToString()) %>
											</span>
										</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
							</webdiyer:AspNetPager>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<input type="button" id="btn" value="Button" style="display: none" onclick="Test()" />
	<asp:HiddenField ID="hfldSelectedPrj" runat="server" />
	<asp:HiddenField ID="hfldContractTypeGuid" runat="server" />
	<asp:Button ID="btnDataBind" Text="Button" OnClick="btnDataBind_Click" runat="server" />
	</form>
</body>
</html>
