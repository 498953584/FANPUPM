<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanSummary.aspx.cs" Inherits="Fund_PlanSummary_PlanSummary" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="com.jwsoft.pm.entpm"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>计划汇总</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
	<script type="text/javascript" src="/Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
	<script type="text/javascript" src="/UserControl/FileUpload/GetFiles.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript">
	    addEvent(window, 'load', function () {
		    replaceEmptyTable('emptyAccount', 'gvwAccount');
		    $('.pagediv').css('overflow', 'hidden');

			var accTable = new JustWinTable('gvwAccount');
			if (document.getElementById('HdnState').value == "Manage") {
				setButton(accTable, 'btnDelete', 'btnUpdate', 'btnQuery', 'HdnAccountID');
				setWfButtonState2(accTable, 'WF1');
				registerAddEvent();
				registerDeleteEvent();
				registerUpdateEvent();
				registerQueryEvent();
			}
			else {
				document.getElementById('TrButn').style.display = "none";
			}
			displayLookAdjuncts();
		});

		function registerAddEvent() {
			var btnAdd = document.getElementById('btnAdd');
			addEvent(btnAdd, 'click', function () {
				var PlanType = document.getElementById('HdnPlanType').value;
				parent.desktop.ModifyEdit = window;
				var url = "/Fund/PlanSummary/PlanSummaryEdit.aspx?Action=Add&PlanType=" + PlanType;
				toolbox_oncommand(url, "新增计划汇总单");
			});
		}

		function registerDeleteEvent() {
			var btnDelete = document.getElementById('btnDelete');
			btnDelete.onclick = function () {
				if (!confirm("确定要删除吗")) {
					return false;
				}
			}
		}

		function registerUpdateEvent() {
			var btnUpdate = document.getElementById('btnUpdate');
			addEvent(btnUpdate, 'click', function () {
				var AccountID = document.getElementById('HdnAccountID').value;
				var PlanType = document.getElementById('HdnPlanType').value;
				parent.desktop.ModifyEdit = window;
				var url = "/Fund/PlanSummary/PlanSummaryEdit.aspx?Action=Update&AccountID=" + AccountID + "&PlanType=" + PlanType;
				toolbox_oncommand(url, "编辑计划汇总单");

			});
		}

		function registerQueryEvent() {
			var btnQuery = document.getElementById('btnQuery');
			addEvent(btnQuery, 'click', function () {
				var AccountID = document.getElementById('HdnAccountID').value;
				parent.desktop.ModifyEdit = window;
				var url = "/Fund/PlanSummary/PlanSummaryView.aspx?ic=" + AccountID;
				//toolbox_oncommand(url, "查看入账单");
				viewopen(url);
			});
		}
		//查看附件
		function queryAdjunct(id) {
			var path = $('#hfldAdjunctPath').val() + '/' + id;
			window.showFiles(path, 'divOpenAdjunct');
		}
		//附件显示
		function displayLookAdjuncts() {
			var tab = document.getElementById('gvwAccount');
			if (tab.rows.length > 0) {
				for (i = 1; i < tab.rows.length; i++) {
					var id = tab.rows[i].id;
					var path = $('#hfldAdjunctPath').val() + '/' + id;
					var showCount = getFilesCount(path);
					if (showCount == 0)
						tab.rows[i].cells[8].innerText = '';
				}
			}
		}
		function getFilesCount(path) {
			var count = 0;
			$.ajax({
				type: "GET",
				url: "/UserControl/FileUpload/GetFiles.ashx?" + new Date().getTime() + '&Path=' + path,
				async: false,
				dataType: "JSON",
				success: function (data) {
					count = data.length;
				}
			});
			return count;
}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table class="tab">
		<tr id="header">
			<td>
				<asp:Label ID="lblTitle" Text="计划汇总列表" runat="server"></asp:Label>
			</td>
		</tr>
		
		<tr style="vertical-align: top;" id="TrButn">
			<td class="divFooter" style="text-align: left;">
				<input type="button" id="btnAdd" value="新增" />
				<input type="button" id="btnUpdate" value="编辑" disabled="disabled" />
				<asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
				<input type="button" id="btnQuery" disabled="disabled" value="查看" />
				<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiClass="001" runat="server" />
			</td>
		</tr>
		<tr>
			<td style="vertical-align: top;">
				<div class="pagediv">
					<asp:GridView ID="gvwAccount" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwAccount_RowDataBound" DataKeyNames="MID" runat="server">
<EmptyDataTemplate>
							<table id="emptyAccount" class="gvdata">
								<tr class="header">
									<th scope="col" style="width: 20px;">
										<input id="chk1" type="checkbox" />
									</th>
									<th scope="col" style="width: 25px;">
										序号
									</th>
									<th scope="col">
										计划月份
									</th>
									<th scope="col">
										计划金额
									</th>
									<th scope="col">
										上报人
									</th>
									<th scope="col">
										上报日期
									</th>
									<th scope="col">
										流程状态
									</th>
									<th scope="col">
										备注
									</th>
									<th scope="col">
										附件
									</th>
								</tr>
							</table>
						</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
									<asp:CheckBox ID="chkAll" runat="server" />
								</HeaderTemplate>

<ItemTemplate>
									<asp:CheckBox ID="chk" runat="server" />
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="计划月份">
<ItemTemplate>
									<span class="link" onclick="viewopen('/Fund/PlanSummary/PlanSummaryView.aspx?ic=<%# Eval("MID") %>')">
										<%# Eval("PlanName") %>
									</span>
								</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="TotalMoney" HeaderText="计划金额" /><asp:TemplateField HeaderText="上报人">
<ItemTemplate>
									<%# PageHelper.QueryUser(this, Eval("Reporter").ToString()) %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上报日期">
<ItemTemplate>
									<%# Common2.GetTime(Eval("ReportTime").ToString()) %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="流程状态">
<ItemTemplate>
									<%# Common2.GetState(Eval("FlowState").ToString()) %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注">
<ItemTemplate>
									<%# Eval("remark") %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="附件">
<ItemTemplate>
									<span class="link" onclick="queryAdjunct('<%# Eval("MID") %>')">
										<img src='/images1/icon_att0b3dfa.gif' style='cursor: pointer; border-style: none' />
									</span>
								</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				</div>
			</td>
		</tr>
	</table>
	<div id="divOpenAdjunct" title="查看附件" style="display: none;">
		<table id="annexTable" class="gvdata" style="width: 100%;" runat="server"><tr class="header" runat="server"><td style="width: 60%" runat="server">
						名称
					</td><td style="width: 30%" runat="server">
						大小
					</td><td runat="server">
					</td></tr></table>
	</div>
	<asp:HiddenField ID="HdnAccountID" runat="server" />
	<asp:HiddenField ID="hfldAdjunctPath" runat="server" />
	<asp:HiddenField ID="HdnPlanType" runat="server" />
	<asp:HiddenField ID="HdnState" runat="server" />
	</form>
</body>
</html>
