<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudTemplateList.aspx.cs" Inherits="BudgetManage_Template_BudTemplateList" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>模板维护</title>
	<style type="text/css">
		.edit_template input, select
		{
			width: 200px;
		}
	</style>
	<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/jquery.treetable.js"></script>
	<script type="text/javascript" src="../../Script/jquery.blockUI.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="../../Script/jquery-budgetpait.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$('#gvBudget').treetable(true, 'TempleateItems');
			$('#gvBudget').delegate(':checkbox:gt(0)', 'click', checkSingle);
			$('#gvBudget').delegate(':checkbox:eq(0)', 'click', checkAll);
			setControlButton('hfldCheckedIds', 'btnAdd', 'btnEdit', 'btnDel', undefined, undefined, undefined, 'btnResDeploy', undefined, 'btnMoveUp', 'btnMoveDown');
			setHiddenId(undefined, 'hfldTempId', undefined, undefined, 'hfldEditOrAdd');
			addEvent(document.getElementById('btnResDeploy'), 'click', add);
			setWidthHeight();
		});

		function setWidthHeight() {
			$("#divBudget").height($(this).height() - 80);
			$("#divContent2").height($(this).height() - 20);
			$('#div_project').height($(this).height() - 80);
			$("#divBudget").width($(this).width() - $("#td_Left").width() - 8);
		}
		//资源配置
		function add() {
			parent.desktop.ResourceList = window;
			var url = "/BudgetManage/Template/ResourceList.aspx?itemId=" + document.getElementById("hfldCheckedIds").value + "&tempId=" + document.getElementById("hfldTempId").value + "&tempType=" + document.getElementById("hfldTemTypeId").value;
			toolbox_oncommand(url, "资源清单");
		}
	</script>
	<style type="text/css">
		.descTd
		{
			text-align: right;
		}
	</style>
</head>
<body style="margin: 0;">
	<form id="form1" runat="server">
	<div class="divHeader" style="display: none;">
		<table class="tableHeader">
			<tr>
				<td>
					<asp:Label ID="lblTitle" Font-Bold="true" Text="模板维护" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<div id="divContent2" class="divContent2" style="padding: 0px;">
		<table style="">
			<tr>
				<td id="td_Left" style="width: 195px; vertical-align: top; height: 100%;">
					<asp:DropDownList ID="ddlType" AutoPostBack="true" Width="194px" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" runat="server"></asp:DropDownList>
					<div id="div_project" class="pagediv" style="width: 195px; height: 100%;">
						<font face="宋体">
							<asp:TreeView ID="tvBudget" Font-Size="12px" ShowLines="true" OnSelectedNodeChanged="TreeView_SelectedNodeChanged" runat="server"><SelectedNodeStyle BackColor="#3399FF" Height="12px" ForeColor="White" /></asp:TreeView>
						</font>
					</div>
				</td>
				<td style="vertical-align: top;">
					<table border="0" class="tab">
						<tr>
							<td class="divFooter" style="text-align: left;">
								<input type="button" id="btnAdd" value="新增" runat="server" />

								<input type="button" id="btnEdit" value="编辑" disabled="disabled" />
								<asp:Button ID="btnDel" Text="删除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗??')" OnClick="btnDel_Click" runat="server" />
								<input type="button" id="btnMoveUp" value="上移" disabled="disabled" />
								<input type="button" id="btnMoveDown" value="下移" disabled="disabled" />
								<input type="button" style="width: 80px;" id="btnResDeploy" disabled="true" value="资源配置" runat="server" />

							</td>
						</tr>
						<tr>
							<td style="vertical-align: top; height: 100%; width: 100%;">
								<div id="divBudget">
									<asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="TemplateItemId,OrderNumber,SubCount" runat="server"><Columns><asp:TemplateField>
<HeaderTemplate>
													<asp:CheckBox ID="cbAllBox" runat="server" />
												</HeaderTemplate>

<ItemTemplate>
													<asp:CheckBox ID="cbBox" runat="server" />
												</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
													<span style="cursor: default;">
														<%# Eval("No") %>
													</span>
												</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="清单编码"><ItemTemplate>
													<span style="cursor: default;">
														<%# Eval("ItemCode") %>
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
													<span style="vertical-align: middle; cursor: default;">
														<%# Eval("ItemName") %>
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目特征描述" HeaderStyle-Width="100px"><ItemTemplate>
													<span class="tooltip" title='<%# Eval("FeatureDescription").ToString() %>'>
														<%# StringUtility.GetStr(Eval("FeatureDescription"), 30) %>
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型" HeaderStyle-Width="50px"><ItemTemplate>
													<span style="cursor: default;">
														<%# Eval("TypeName") %>
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px"><ItemTemplate>
													<span style="cursor: default;">
														<%# Eval("Unit") %>
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程量" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
													<%# Eval("Quantity") %>
												</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="综合单价" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
													<%# System.Convert.ToDecimal(Eval("UnitPrice")).ToString() %>
												</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合价" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
													<%# System.Convert.ToDecimal(Eval("Total")).ToString() %>
												</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
													<asp:HyperLink ID="hlinkShowNote" Style="text-decoration: none; color: Black; cursor: default;" ToolTip='<%# System.Convert.ToString(Eval("Note").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(Eval("Note").ToString()) %></asp:HyperLink>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="排序" Visible="false"><ItemTemplate>
													<%# StringUtility.GetStr(System.Convert.ToString(Eval("OrderNumber"))) %>
												</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
								</div>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	<div id="editTask" title="新增/编辑节点" style="display: none">
		<iframe id="editTaskFrame" frameborder="0" width="100%" height="100%"></iframe>
	</div>
	
	<asp:HiddenField ID="hfldCheckedIds" runat="server" />
	
	<asp:HiddenField ID="hfldTempId" runat="server" />
	
	<asp:HiddenField ID="hfldTemTypeId" runat="server" />
	
	<asp:HiddenField ID="hfldOrderNumber" runat="server" />
	
	<asp:HiddenField ID="hfldEditOrAdd" runat="server" />
	</form>
</body>
</html>
