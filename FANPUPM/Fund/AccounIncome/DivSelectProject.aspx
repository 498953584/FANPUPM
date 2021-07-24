<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivSelectProject.aspx.cs" Inherits="Common_DivSelectProject" %>
<%@ Import Namespace="cn.justwin.BLL"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>项目信息</title>
	<script src="/Web_Client/Tree.js" type="text/javascript"></script>
	<script src="/Script/jquery.js" type="text/javascript"></script>
	<script src="/StockManage/script/JustWinTable.js" type="text/javascript"></script>
	<script src="/StockManage/script/Config2.js" type="text/javascript"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var aa = new JustWinTable('grdModules');
		});
		function clickRow(obj, moduleCode, isLeaf, theCode, theName) {
			$("#hdName").val(theName);
			$("#hdCode").val(theCode);
			$("#btnSave").removeAttr('disabled');
		}

		function dbClickRow(obj, theCode, theName, isLeaf) {
			btnOk();
		}
		//点确定
		function btnOk() {
			if (typeof top.ui.callback == 'function') {
				top.ui.callback({ id: $('#hdCode').val(), name: $('#hdName').val() });
			}
			top.ui.closeWin();

			return false;
			if ($("#hdCode").val() != null && $("#hdName").val() != null) {
				var method = getRequestParam('Method'); //方法
				parent[method]($("#hdCode").val(), $("#hdName").val());
			}
			divClose(parent);
		}
				
	</script>
	<style type="text/css">
		
	</style>
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0" scroll="no">
	<form id="Form1" method="post" runat="server">
	<table id="Table2" class="table-nomal" cellspacing="0" cellpadding="0" style="vertical-align: top;"
		width="680px">
		<tr>
			<td style="text-align: left; border: solid 0px red; height: 25px;">
				项目编号：<asp:TextBox ID="prjcode" Width="120px" runat="server"></asp:TextBox>&nbsp;
				项目名称：<asp:TextBox ID="prjname" Width="120px" runat="server"></asp:TextBox>&nbsp;
				<asp:Button ID="SearchBt" Text="查 询" OnClick="SearchBt_Click" runat="server" />
			</td>
		</tr>
		<tr>
			<td valign="top" align="center" style="height: 320px; padding-top: 10px;">
				<div id="pagediv" style="width: 100%; border: solid 0px red; text-align: left;" align="center">
					<asp:DataGrid ID="grdModules" DataKeyField="PrjCode" AutoGenerateColumns="false" CellPadding="0" Width="100%" CssClass="gvdata" AllowPaging="true" OnPageIndexChanged="grdModules_PageIndexChanged" runat="server"><ItemStyle CssClass="rowa"></ItemStyle><PagerStyle Mode="NumericPages"></PagerStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle><Columns><asp:TemplateColumn HeaderText="项目编号">
<ItemTemplate>
									<span style="vertical-align: middle;">
										<%# Eval("PrjCode") %>
									</span>
								</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="TypeCode" HeaderText="序号"></asp:BoundColumn><asp:TemplateColumn HeaderText="项目名称" ItemStyle-Width="170px"><ItemTemplate>
									<%# StringUtility.GetStr(Eval("prjName"), 20) %>
								</ItemTemplate></asp:TemplateColumn><asp:BoundColumn DataField="Owner" HeaderText="建设单位"></asp:BoundColumn><asp:BoundColumn DataField="PrjCost" HeaderText="造价(万元)" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="startdate" HeaderText="开始时间" DataFormatString="{0:yyyy-MM-dd}" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="EndDate" HeaderText="结束时间" DataFormatString="{0:yyyy-MM-dd}" Visible="false"></asp:BoundColumn><asp:BoundColumn HeaderText="项目地点" DataField="PrjPlace"></asp:BoundColumn><asp:BoundColumn DataField="PrjState" HeaderText="状态"></asp:BoundColumn></Columns></asp:DataGrid>
				</div>
			</td>
		</tr>
		<tr class="td-submit">
			<td align="right" style="border: solid 0px red;">
				<input type="hidden" id="hdn1" name="hdn1" style="width: 10px" runat="server" />

				<input type="hidden" id="hdn2" name="hdn2" style="width: 10px" runat="server" />

				<input type="hidden" id="hdn3" name="hdn3" style="width: 10px" runat="server" />

				<input id="btnSave" type="button" value="确 定" onclick="btnOk();" disabled="disabled" />
				<input id="btnCancel" type="button" value="取 消" onclick="top.ui.closeWin();" />
			</td>
		</tr>
	</table>
	<input id="hdnModuleCodeList" style="width: 10px" type="hidden" name="hdnModuleCodeList" runat="server" />

	<asp:HiddenField ID="hdName" runat="server" />
	<asp:HiddenField ID="hdCode" runat="server" />
	</form>
</body>
</html>
