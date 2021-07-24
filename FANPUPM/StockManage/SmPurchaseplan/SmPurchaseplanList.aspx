<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SmPurchaseplanList.aspx.cs" Inherits="StockManage_SmPurchaseplan_SmPurchaseplanList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>申请单列表</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../script/Config2.js"></script>
	<script type="text/javascript" src="../script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/jwJson.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var purchasePlanTable = new JustWinTable('gvPurchaseplan');

			// 保存guid
			purchasePlanTable.registClickTrListener(function () {
				$('#btnLook').attr('guid', $(this).attr('guid'));
			});
			purchasePlanTable.registClickSingleCHKListener(function () {
				var tr = jw.getParentUntil('TR')
				$('#btnLook').attr('guid', $(tr).attr('guid'));
			});

			setButton(purchasePlanTable, 'btnDel', 'btnEdit', 'btnLook', 'hfldPurchaseChecked');
			addEvent(document.getElementById('btnEdit'), 'click', rowEdit);
			addEvent(document.getElementById('btnLook'), 'click', rowQuery);
			addEvent(document.getElementById('btnAdd'), 'click', rowAdd);
			setWfButtonState2(purchasePlanTable, 'WF1');
			showTooltip('tooltip', 15);
			function rowAdd() {
				parent.parent.desktop.AddSmPurchaseplan = window;
				var url = "/StockManage/Smpurchaseplan/AddSmpurchaseplan.aspx?prjId=" + document.getElementById("hfldPrjId").value;
				toolbox_oncommand(url, "新增采购计划");
			}
			function rowEdit() {
				parent.parent.desktop.AddSmPurchaseplan = window;
				var url = "/StockManage/Smpurchaseplan/AddSmpurchaseplan.aspx?id=" + document.getElementById("hfldPurchaseChecked").value;
				toolbox_oncommand(url, "编辑采购计划");
			}
			function rowQuery() {
				var guid = $(this).attr('guid');
				var url = "ViewSmPurchaseplan.aspx?t=1&ic=" + guid + "&BusiCode=072&BusiClass=001";
				viewopen(url, 820, 500);
			}
			$('#txtStartTime').blur(function () {
				controlDate(this);
			})
			$('#txtEndTime').blur(function () {
				controlDate(this);
			})
		});

		//选择人员
		function selectUser() {
			jw.selectOneUser({ codeinput: 'hdnPeople', nameinput: 'txtPeople' });
		}

		//管理时间
		function controlDate(para) {
			var startDates = document.getElementById('txtStartTime').value;
			var startDateList = startDates.split('-');
			var startDate = new Date(startDateList[0], startDateList[1] - 1, startDateList[2]);

			var endDates = document.getElementById('txtEndTime').value;
			var endDatesList = endDates.split('-');
			var endDate = new Date(endDatesList[0], endDatesList[1] - 1, endDatesList[2]);
			if (startDates != '') {
				if (startDate == 'NaN') {
					alert('系统提示\n\n起始日期输入时间格式不正确！');
					para.value = '';
					return;
				}
			}
			if (endDates != '') {
				if (endDate == 'NaN') {
					alert('系统提示\n\n结束日期输入时间格式不正确！');
					para.value = '';
					return;
				}
			}
		}
  
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		<tr style="height: 20px;">
			<td class="divHeader">
				<asp:Label ID="lblProject" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td style="width: 100%; vertical-align: top;">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							起始日期
						</td>
						<td>
							<asp:TextBox ID="txtStartTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							结束日期
						</td>
						<td>
							<asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							计划编号
						</td>
						<td>
							<asp:TextBox ID="txtPPcode" Height="15px" Width="120px" runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							录 &nbsp; 入 人
						</td>
						<td>
							<input type="text" style="width: 120px;" id="txtPeople" class="select_input" imgclick="selectUser" runat="server" />

							<asp:HiddenField ID="hdnPeople" runat="server" />
						</td>
						<td>
							<asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td style="vertical-align: top;">
				<table class="tab" style="vertical-align: top;">
					<tr>
						<td class="divFooter" style="text-align: left">
							<input type="button" id="btnAdd" value="新增" />
							<input type="button" id="btnEdit" disabled="disabled" value="编辑" />
							<asp:Button ID="btnDel" Text="删  除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗??')" OnClick="btnDel_Click" runat="server" />
							<input type="button" id="btnLook" disabled="disabled" value="查看" />
							<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="072" BusiClass="001" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							<div class="">
								<asp:GridView ID="gvPurchaseplan" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvPurchaseplan_PageIndexChanging" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="ppcode,ppid,project" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
												<asp:CheckBox ID="cbAllBox" runat="server" />
											</HeaderTemplate><ItemTemplate>
												<asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("ppcode")) %>' runat="server" />
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计划编号"><ItemTemplate>
												<span class="al" onclick="viewopen('ViewSmPurchaseplan.aspx?ic=<%# Eval("ppid") %>',820,500)">
													<%# Eval("ppcode") %>
												</span>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入人"><ItemTemplate>
												<%# Eval("person") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入时间"><ItemTemplate>
												<%# Common2.GetTime(Eval("intime").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
												<%# Common2.GetState(Eval("flowstate").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
												<%# GetAnnx(Convert.ToString(Eval("ppid"))) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="说明"><ItemTemplate>
												<span class="tooltip" title='<%# Eval("explain") %>'>
													<%# StringUtility.GetStr(Convert.ToString(Eval("explain"))) %>
												</span>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称" Visible="false"><ItemTemplate>
												<%# WebUtil.GetPrjNameByproject(Eval("project")) %>
											</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<div id="divFram" title="" style="display: none">
		<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<div id="divFramPrj" title="" style="display: none">
		<iframe id="ifFramPrj" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	</form>
</body>
</html>
