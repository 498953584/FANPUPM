<%@ Page Language="C#" AutoEventWireup="true" CodeFile="progsortlist.aspx.cs" Inherits="ProgSortList" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>ProgSortList</title>
	<script type="text/javascript" src="../../../../Script/jquery.js"></script>
	<script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
	<script type="text/ecmascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script language="javascript" type="text/javascript">
		var pk = null;
		$(document).ready(function () {
			var purchasePlanTable = new JustWinTable('DataGrid1');
			// 隐藏一个不知道有用还是没用的文本框	Bery
			document.getElementById('TextBox_pk').style.display = 'none';
			hideFirstPageNo();
		});

		function setvalue(obj) {
			pk = obj;
			document.getElementById("TextBox_pk").value = obj;
			document.getElementById("Button_edit").disabled = false;
			document.getElementById("Button_del").disabled = false;
		}
		function ShowInsertWindow() {
			var url = "/EPC/17/Ppm/Prog/ProgManage.aspx";
			top.ui._progsortlist = window;
			toolbox_oncommand(url, "新增奖罚类别");
		}
		function ShowEditWindow() {
			if (pk == null) {
				window.alert('请选择记录！');
			}
			else {
				var url = "/EPC/17/Ppm/Prog/ProgManage.aspx?pk=" + pk + "";
				top.ui._progsortlist = window;
				toolbox_oncommand(url, "编辑奖罚类别");
			}
		}
	</script>
	<style type="text/css">
		.dgheader
		{
			background-color: #EEF2F5;
			white-space: nowrap;
			overflow: hidden;
			font-weight: normal;
			text-align: center;
			border-color: #CADEED;
			color: #727FAA;
			padding-left: 6px;
			padding-right: 6px;
		}
	</style>
</head>
<body>
	<form id="Form1" method="post" runat="server">
	<table cellspacing="0" cellpadding="0" width="100%" border="0">
		
		<tr>
			<td class="divFooter" style="text-align: left;">
				<asp:Button ID="Button_add" Text="添加" runat="server" />
				<asp:Button ID="Button_edit" Text="编辑" Enabled="false" runat="server" />
				<asp:Button ID="Button_del" Text="删除" Enabled="false" OnClick="Button_del_Click" runat="server" />
				<asp:TextBox ID="TextBox_pk" Width="0px" BorderColor="Window" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td valign="top">
				<div style="width: 100%; height: 100%">
					<asp:DataGrid ID="DataGrid1" Width="100%" CssClass="gvdata" AutoGenerateColumns="false" AllowPaging="true" PageSize="15" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server"><SelectedItemStyle HorizontalAlign="Left"></SelectedItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><ItemStyle CssClass="rowa"></ItemStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="25px"><ItemTemplate>
									<%# Container.ItemIndex + 1 + this.DataGrid1.PageSize * this.DataGrid1.CurrentPageIndex %>
								</ItemTemplate></asp:TemplateColumn><asp:BoundColumn DataField="ProgSortName" HeaderText="奖罚类别名称"></asp:BoundColumn><asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid></div>
			</td>
		</tr>
	</table>
	<JWControl:JavaScriptControl ID="JavaScriptControl1" runat="server"></JWControl:JavaScriptControl>
	</form>
</body>
</html>
