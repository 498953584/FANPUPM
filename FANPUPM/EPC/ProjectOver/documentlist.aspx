<%@ Page Language="C#" AutoEventWireup="true" CodeFile="documentlist.aspx.cs" Inherits="DocumentList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>DocumentList</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/ecmascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script language="javascript" type="text/javascript">
		$(document).ready(function () {
			var purchasePlanTable = new JustWinTable('DgdDocument');
			//如果只有一页的数据,隐藏页码
			hideFirstPageNo();

			var prjId = getRequestParam('prjId');
			$('#BtnAdd').click(function () {
				top.ui._documentlist = window;
				var url = '/EPC/ProjectOver/DocumentAdd.aspx?pc=' + prjId + '&allowadd=1';
				toolbox_oncommand(url, "新增文本归档");
			});
			$('#BtnEdit').click(function () {
				top.ui._documentlist = window;
				var url = '/EPC/ProjectOver/DocumentEdit.aspx?dc=' + $('#HdnDocCode').val();
				toolbox_oncommand(url, "编辑文本归档");
			});
		});
		function clickRow(docCode) {
			document.getElementById('HdnDocCode').value = docCode;
			document.getElementById('BtnEdit').disabled = false;
			document.getElementById('BtnDel').disabled = false;
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
<body scroll="yes">
	<form id="Form1" method="post" runat="server">
	<table height="100%" cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td align="left" height="24">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td>
							<asp:DropDownList ID="DDLTerm" Width="120px" runat="server"><asp:ListItem Value="0" Text="请选择" /><asp:ListItem Value="1" Text="按文档名称" /><asp:ListItem Value="2" Text="按文档号" /><asp:ListItem Value="3" Text="按卷标号" /><asp:ListItem Value="4" Text="按存放位置" /></asp:DropDownList>
							<asp:TextBox ID="TxtTerm" Width="120px" runat="server"></asp:TextBox>
							文档类型<JWControl:DropDownTree ID="DDTClass" runat="server"></JWControl:DropDownTree>
							<asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />&nbsp;&nbsp;
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td class="divFooter" style="text-align: left;">
				<input id="HdnDocCode" style="width: 10px" type="hidden" name="HdnDocCode" runat="server" />
&nbsp;
				<asp:Button ID="BtnAdd" Text="新 增" runat="server" />&nbsp;
				<asp:Button ID="BtnEdit" Text="编 辑" Enabled="false" runat="server" />&nbsp;
				<asp:Button ID="BtnDel" Text="删 除" Enabled="false" OnClick="BtnDel_Click" runat="server" />&nbsp;&nbsp;
			</td>
		</tr>
		
		<tr>
			<td valign="top">
				<asp:DataGrid ID="DgdDocument" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="15" OnPageIndexChanged="DgdDocument_PageIndexChanged" runat="server"><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><ItemStyle CssClass="rowa"></ItemStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><Columns><asp:TemplateColumn Visible="false">
<ItemTemplate>
								<input id="SelectRow" type="radio" name="SelectRow">
							</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
								<%# Container.ItemIndex + 1 + this.DgdDocument.PageSize * this.DgdDocument.CurrentPageIndex %>
							</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="FileCode" HeaderText="文档号"></asp:BoundColumn><asp:BoundColumn DataField="RollCode" HeaderText="卷标号"></asp:BoundColumn><asp:BoundColumn DataField="DocumentName" HeaderText="归档名称"></asp:BoundColumn><asp:BoundColumn DataField="Storage" HeaderText="存放位置"></asp:BoundColumn><asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn><asp:BoundColumn DataField="ClassCode" HeaderText="文档类型" Visible="false"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid>
			</td>
		</tr>
	</table>
	<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
	</form>
</body>
</html>
