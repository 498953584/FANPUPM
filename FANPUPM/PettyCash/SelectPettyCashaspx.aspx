<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectPettyCashaspx.aspx.cs" Inherits="PettyCash_SelectPettyCashaspx" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>选择备用金</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<style type="text/css">
		.query-tbl td
		{
			padding-left: 10px;
		}
	</style>
	<script type="text/javascript">
		$(document).ready(function () {
			var pcJtbl = new JustWinTable('gvwPettyCash');
			pcJtbl.registClickTrListener(function () { $('#hfldPettyCashId').val(this.id) });
			pcJtbl.registDbClickListener(success);

		});

		function success() {
			var id = $('#hfldPettyCashId').val();
			var matter = $('#' + id).find('td:eq(0) span').attr('title');
			if (typeof top.ui.callback == 'function') {
				top.ui.callback({ Id: id, Matter: matter });
			}

			top.ui.closeWin();
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<table class="query-tbl">
			<tr>
				<td>
					申请事项
				</td>
				<td>
					<asp:TextBox ID="txtMatter" runat="server"></asp:TextBox>
				</td>
				<td>
					申请日期
				</td>
				<td>
					<asp:TextBox ID="txtApplicationDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
				</td>
				<td>
					<asp:Button ID="btnSelect" Text="查询" OnClick="btnSelect_Click" runat="server" />
				</td>
			</tr>
		</table>
	</div>
	<div style="height: 400px;">
		<asp:GridView ID="gvwPettyCash" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwPettyCash_RowDataBound" DataKeyNames="Id" runat="server"><Columns><asp:TemplateField HeaderText="申请事项" HeaderStyle-Width="490px"><ItemTemplate>
						<span title='<%# Eval("Matter").ToString() %>'>
							<%# StringUtility.GetStr(Eval("Matter").ToString(), 35) %>
						</span>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申请日期" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
						<%# System.Convert.ToDateTime(Eval("ApplicationDate")).ToString("yyyy-MM-dd") %>
					</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
		<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
		</webdiyer:AspNetPager>
	</div>
	<div style="text-align: right;">
		<input type="button" id="btnOk" value="确定" onclick="success();" />
		<input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
	</div>
	<asp:HiddenField ID="hfldPettyCashId" runat="server" />
	</form>
</body>
</html>
