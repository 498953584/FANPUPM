<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RetMsgList.aspx.cs" Inherits="StartStopReturnWork_RetMsgList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>复工报告</title><link href="../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="../Script/jquery.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../StockManage/script/ValidateInput.js"></script>
	<script type="text/javascript" src="../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../Script/Budget/BudgetPait.js"></script>
	<script type="text/javascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../Script/jw.js"></script>
	<script type="text/javascript" src="../Script/wf.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var gvRetMsg = new JustWinTable('gvRetMsg');
			setButton(gvRetMsg, 'btnDel', 'btnEdit', 'btnQuery', 'hfldRetMsgChecked');
			setWidthAndHeight();
			showTooltip('tooltip', 25);
			if (arr = document.cookie.match(/scrollTop=([^;]+)(;|$)/))
				document.getElementById('div_project').scrollTop = parseInt(arr[1]);
			clickTree('tvProject', 'hfldPrjId');
			setWfButtonState2(gvRetMsg, 'WF1');
			$('#BtnWFDel').hide();
			//是否允许增加复工报告
			if ($('#hfldPrjState').val() == "8") {
				if ($('#hfldIsAllowAdd').val() == "1") {
					$('#btnAdd').removeAttr('disabled');
				} else {
					$('#btnAdd').attr('disabled', 'disabled');
				}
			} else {
				$('#btnAdd').attr('disabled', 'disabled');
			}
		});

		//设置div高度和宽度
		function setWidthAndHeight() {
			$('#divBudget').height($(this).height() - $('#divTop').height() - 25);
			$('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 8);
			$('#div_project').height($(this).height() - $('#ddlYear').height() - 4);
		}

		function add() {
			top.ui._RetMsgEdit = window;
			var url = '/StartStopReturnWork/RetMsgEdit.aspx?type=add&prjId=' + document.getElementById("hfldPrjId").value + '&year=' + document.getElementById("hfldYear").value;
			toolbox_oncommand(url, "新增复工通知单");
		}
		function edit() {
			top.ui._RetMsgEdit = window;
			var url = '/StartStopReturnWork/RetMsgEdit.aspx?type=edit&prjId=' + document.getElementById("hfldPrjId").value + '&retMsgId=' + document.getElementById("hfldRetMsgChecked").value + '&year=' + document.getElementById("hfldYear").value;
			toolbox_oncommand(url, "编辑复工通知单");
		}
		function Query(retMsgId) {
			var url = '/StartStopReturnWork/RetMsgView.aspx?retMsgId=' + retMsgId + '&ic=' + retMsgId;
			toolbox_oncommand(url, "查看复工通知单");
		}
		function openQuery() {
			var retMsgId = document.getElementById("hfldRetMsgChecked").value;
			viewopen('/StartStopReturnWork/RetMsgView.aspx?retMsgId=' + retMsgId + '&ic=' + retMsgId + '', '查看复工通知单');
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
		<table>
			<tr>
				<td style="width: 100%; height: 100%;">
					<table style="width: 100%; height: 100%;">
						<tr>
							
							<td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
								<div id="div1" class="divContent2" style="width: 100%; height: 99%; overflow: hidden;">
									<table style="width: 100%; height: 88%;">
										<tr id="header">
											<td>
												<asp:Label ID="lblProject" runat="server"></asp:Label>
											</td>
										</tr>
										<tr id="divTop">
											<td class="divFooter" style="text-align: left; width: 100%;">
												<input type="button" id="btnAdd" value="新增" onclick="add();" />
												<input type="button" id="btnEdit" value="编辑" disabled="disabled" onclick="edit();" />
												<asp:Button ID="btnDel" Text="删除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗?')" OnClick="btnDelete_Click" runat="server" />
												<input type="button" id="btnQuery" value="查看" disabled="disabled" onclick="openQuery();" />
												<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="128" BusiClass="001" runat="server" />
											</td>
										</tr>
										<tr>
											<td style="width: 100%; height: 100%; vertical-align: top ;">
												<div id="divBudget" class="pagediv" style="overflow:auto;">
													<asp:GridView ID="gvRetMsg" Width="100%" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvRetMsg_RowDataBound" DataKeyNames="RetMsgId" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="50px">
<HeaderTemplate>
																	序号
																</HeaderTemplate>

<ItemTemplate>
																	<%# Container.DataItemIndex + 1 %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="200px">
<HeaderTemplate>
																	复工日期
																</HeaderTemplate>

<ItemTemplate>
																	<span class="link" onclick="Query('<%# Eval("RetMsgId") %>');">
																		<%# System.Convert.ToDateTime(Eval("RetDate")).ToString("yyyy-MM-dd") %>
																	</span>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="200px">
<HeaderTemplate>
																	施工单位
																</HeaderTemplate>

<ItemTemplate>
																	<span class="tooltip" title="<%# Eval("ConstUnit") %>">
																		<%# StringUtility.GetStr(Eval("ConstUnit").ToString(), 25) %>
																	</span>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="200px">
<HeaderTemplate>
																	录入人
																</HeaderTemplate>

<ItemTemplate>
																	<%# WebUtil.GetUserNames(Eval("InputUser").ToString()) %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="200px">
<HeaderTemplate>
																	录入日期
																</HeaderTemplate>

<ItemTemplate>
																	<%# Common2.GetTime(Eval("InputDate")) %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
																	流程状态
																</HeaderTemplate>

<ItemTemplate>
																	<%# Common2.GetState(Eval("FlowState").ToString()) %>
																</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
												</div>
											</td>
										</tr>
									</table>
								</div>
								<asp:HiddenField ID="hfldPrjId" runat="server" />
								<asp:HiddenField ID="hfldRetMsgChecked" runat="server" />
								<asp:HiddenField ID="hfldYear" runat="server" />
								<asp:HiddenField ID="hfldIsAllowAdd" runat="server" />
								<asp:HiddenField ID="hfldPrjState" runat="server" />
								
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	<link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/default/easyui.css" />
	<link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/icon.css" />
	</form>
</body>
</html>
