<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivSelectProject2.aspx.cs" Inherits="Common_DivSelectProject2" %>
<%@ Import Namespace="cn.justwin.BLL"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>选择项目</title>
	<base target="_self" />
	<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script src="/Web_Client/Tree.js" type="text/javascript"></script>
	<script src="/Script/jquery.js" type="text/javascript"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script src="/StockManage/script/JustWinTable.js" type="text/javascript"></script>
	<script src="/StockManage/script/Config2.js" type="text/javascript"></script>
	<script type="text/javascript" src="../Script/json2.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script language="JavaScript" type="text/javascript">
		$(document).ready(function () {
			jw.tooltip();
			var prjTab = new JustWinTable('gvwPrj');
			formateTable('gvwPrj', 1);

			clickTr();
			dbclickTr();
		});

		function clickTr() {
			$('#gvwPrj TR[id]').click(function () {
				var prjObj = {
					prjId: $(this).attr('id'),
					prjName: $(this).attr('name'),
					typeCode: $(this).attr('typeCode')
				};

				$('#hdPrjInfo').val(JSON.stringify(prjObj));

				$("#btnSave").removeAttr('disabled');
			});
		}

		function dbclickTr() {
			$('#gvwPrj TR[id]').dblclick(function () {
				okEvent();
			});
		}

		// 确定事件
		function okEvent() {
			if (top.ui.callback != null) {
				var prj = JSON.parse($('#hdPrjInfo').val());
				top.ui.callback(prj);
			}
			top.ui.closeWin({ winNo: getRequestParam('winNo') });
		}
				
	</script>
</head>
<body>
	<form id="Form1" method="post" runat="server">
	<table id="Table2" class="table-nomal" height="85%" cellspacing="0" cellpadding="0"
		width="100%">
		<tr>
			<td style="text-align: left; height: 25px;">
				项目编号：<asp:TextBox ID="prjcode" Width="120px" runat="server"></asp:TextBox>&nbsp;
				项目名称：<asp:TextBox ID="prjname" Width="120px" runat="server"></asp:TextBox>&nbsp;
				<asp:Button ID="SearchBt" Text="查 询" OnClick="SearchBt_Click" runat="server" />
			</td>
		</tr>
		<tr>
			<td valign="top" style="height: 100%; padding-top: 2px; vertical-align: top; text-align: center;">
				<div id="Div1" style="width: 100%; text-align: left;" align="center">
					<asp:GridView ID="gvwPrj" CellPadding="0" Width="100%" CssClass="gvdata" AutoGenerateColumns="false" AllowPaging="true" PageSize="15" OnPageIndexChanging="gvwPrj_PageIndexChanging" OnRowDataBound="gvwPrj_RowDataBound" DataKeyNames="Id,Code,Name,State,IsParent,TypeCode" runat="server"><Columns><asp:TemplateField HeaderText="项目编号"><ItemTemplate>
									<%# Eval("Code") %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称" ItemStyle-VerticalAlign="Middle"><ItemTemplate>
									<span class="tooltip" title='<%# Eval("Name") %>'>
										<%# StringUtility.GetStr(Eval("Name"), 15) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="建设单位"><ItemTemplate>
									<span class="tooltip" title='<%# GetOwnerName(Eval("OwnerCode")) %>'>
										<%# StringUtility.GetStr(base.GetOwnerName(Eval("OwnerCode")), 10) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目地点"><ItemTemplate>
									<span class="tooltip" title='<%# Eval("Place").ToString() %>'>
										<%# StringUtility.GetStr(Eval("Place"), 15) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="状态"><ItemTemplate>
									<%# ProjectStateHelper.GetName(Eval("State")) %>
								</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				</div>
			</td>
		</tr>
	</table>
	<div style="text-align: right">
		<input id="btnSave" type="button" value="确 定" onclick="okEvent();" disabled="disabled" />
		<input id="btnCancel" type="button" value="取 消" onclick="top.ui.closeWin({ winNo: getRequestParam('winNo') });" />
	</div>
	<input id="hdnModuleCodeList" style="width: 10px" type="hidden" name="hdnModuleCodeList" runat="server" />

	<asp:HiddenField ID="hdName" runat="server" />
	<asp:HiddenField ID="hdCode" runat="server" />
	<asp:HiddenField ID="hdPrjCode" runat="server" />
	<asp:HiddenField ID="hdPrjInfo" runat="server" />
	<asp:HiddenField ID="hdCodeName" runat="server" />
	</form>
</body>
</html>
