<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryItemList.aspx.cs" Inherits="Salary2_SalaryItemList" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>工资项</title><link type="text/css" rel="Stylesheet" href="../Script/jquery.tooltip/jquery.tooltip.css" />
<link type="text/css" rel="Stylesheet" href="../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../Script/jquery.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../Script/jw.js"></script>
	<script type="text/javascript" src="Script/SalaryItemList.js"></script>
	<script type="text/javascript">
		function upAdminPrivilege() { }
	</script>
	<style type="text/css">
		.descTd
		{
			width: 40px;
		}
		.descTd span
		{
			margin: 0px;
			color: Red;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<table style="width: 100%; height: 100%;">
			<tr>
				<td class="divFooter" style="text-align: left;">
					<input type="button" id="btnAdd" value="新增" onclick="edit('add');" />
					<input type="button" id="btnEdit" value="编辑" disabled="disabled" onclick="edit('edit');" />
					<asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
					<input type="button" id="btnUp" value="上移" disabled="disabled" onclick="MoveTREvent('Up');" />
					<input type="button" id="btnDown" value="下移" disabled="disabled" onclick="MoveTREvent('Down');" />
				</td>
			</tr>
			<tr>
				<td>
					<asp:GridView ID="gvSalaryItem" CssClass="gvdata" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvSalaryItem_RowDataBound" DataKeyNames="Id,No,IsAllowDel" runat="server">
<EmptyDataTemplate>
							<table class="gvdata" id="emptySalaryItem" cellspacing="0" rules="all" border="1"
								style="width: 100%; border-collapse: collapse;">
								<tr class="header">
									<td scope="col" style="width: 20px;">
										<input type="checkbox" />
									</td>
									<td scope="col" style="width: 25px;">
										序号
									</td>
									<td scope="col" style="width: 400px;">
										名称
									</td>
									<td scope="col">
										备注
									</td>
								</tr>
							</table>
						</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
									<input type="checkbox" id="chkAll" />
								</HeaderTemplate><ItemTemplate>
									<input type="checkbox" id="chkSelectSingle" />
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Right"><HeaderTemplate>
									序号
								</HeaderTemplate><ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="400px"><HeaderTemplate>
									工资项名称
								</HeaderTemplate><ItemTemplate>
									<span class="tooltip" title="<%# Eval("Name") %>">
										<%# StringUtility.GetStr(Eval("Name").ToString(), 25) %>
									</span>
									<asp:HiddenField ID="hfldNum" Value='<%# System.Convert.ToString(Container.DataItemIndex + 1, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
								</ItemTemplate></asp:TemplateField><asp:TemplateField><HeaderTemplate>
									备注
								</HeaderTemplate><ItemTemplate>
									<span class="tooltip" title="<%# Eval("Note") %>">
										<%# StringUtility.GetStr(Eval("Note"), 25) %>
									</span>
								</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				</td>
			</tr>
		</table>
	</div>
	<div id="divSalaryItemEdit" style="display: none" runat="server">
		<table class="tableContent2" cellpadding="5px" cellspacing="0">
			<tr>
				<td class="descTd">
					<span>* </span>名称
				</td>
				<td>
					<asp:TextBox ID="txtSalaryName" CssClass="easyui-validatebox" data-options="required:true, validType:'validChars[15]'" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="descTd">
					&nbsp;&nbsp;&nbsp;备注
				</td>
				<td>
					<asp:TextBox ID="txtNote" TextMode="MultiLine" Height="50px" CssClass="easyui-validatebox" data-options="validType:'validChars[4000]'" Rows="3" runat="server"></asp:TextBox>
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hfldCheckedId" runat="server" />
	<asp:HiddenField ID="hfldType" runat="server" />
	<asp:Button ID="btnSaveData" Style="display: none;" OnClick="btnSaveData_Click" runat="server" />
	<link type="text/css" rel="Stylesheet" href="../Script/jquery.easyui/themes/default/easyui.css" />
	<link type="text/css" rel="Stylesheet" href="../Script/jquery.easyui/themes/icon.css" />
	</form>
</body>
</html>
