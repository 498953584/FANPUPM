<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SmWastageList.aspx.cs" Inherits="StockManage_SmWastage_SmWastageList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>报损出库单</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../script/Config2.js"></script>
	<script type="text/javascript" src="../script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/jwJson.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var wastageReserve = new JustWinTable('gvWastage');
			setButton(wastageReserve, 'btnDel', 'btnEdit', 'btnLook', 'hfldWastage');
			addEvent(document.getElementById('btnEdit'), 'click', rowEdit);
			addEvent(document.getElementById('btnLook'), 'click', rowQuery);
			addEvent(document.getElementById('btnAdd'), 'click', rowAdd);
			showTooltip('tooltip', 10);
			setWfButtonState2(wastageReserve, 'WF1');
		});

		function rowAdd() {
			parent.desktop.AddReserve = window;
			var url = "/StockManage/SmWastage/SmWastageEdit.aspx";
			toolbox_oncommand(url, "新增报损出库单");
		}

		function rowEdit() {
			parent.desktop.AddReserve = window;
			var url = "/StockManage/SmWastage/SmWastageEdit.aspx?id=" + document.getElementById("hfldWastage").value;
			toolbox_oncommand(url, "编辑报损出库单");
		}

		function rowQuery() {
			var url = "ViewWastage.aspx?t=1&ic=" + document.getElementById("hfldWastage").value;
			viewopen(url, 820, 500);
		}

		// 选择人员
		function selectUser() {
			jw.selectOneUser({ codeinput: 'hdnPeople', nameinput: 'txtPeople' });
		}

		function selectTrea() {
			jw.selectTreasury({ codeinput: 'hfldTrea', nameinput: 'txtTrea' });
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		
		<tr>
			<td style="vertical-align: top;">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr style="height: 25px;">
						<td class="descTd">
							报损出库单号
						</td>
						<td>
							<asp:TextBox ID="txtWastageCode" Height="15px" Width="120px" runat="server"></asp:TextBox>
						</td>
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
							录 入 &nbsp;人
						</td>
						<td>
							<input type="text" style="width: 120px;" id="txtPeople" class="select_input" imgclick="selectUser" runat="server" />

							<asp:HiddenField ID="hdnPeople" runat="server" />
						</td>
					</tr>
					<tr style="height: 25px;">
						<td class="descTd">
							仓库名称
						</td>
						<td>
							<asp:TextBox ID="txtTrea" CssClass="select_input" Width="120px" imgclick="selectTrea" runat="server"></asp:TextBox>
							<asp:HiddenField ID="hfldTrea" runat="server" />
						</td>
						<td colspan="2">
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
							<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="125" BusiClass="001" runat="server" />
						</td>
					</tr>
					<tr>
						<td style="height: 100%; vertical-align: top;">
							<div class="">
								<asp:GridView ID="gvWastage" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvWastage_RowDataBound" DataKeyNames="WastageId,WastageCode" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
												<asp:CheckBox ID="cbAllBox" runat="server" />
											</HeaderTemplate><ItemTemplate>
												<asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Eval("WastageCode"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="报损出库编号"><ItemTemplate>
												<span class="al" onclick="viewopen('ViewWastage.aspx?ic=<%# Eval("WastageId") %>',820,500);">
													<%# Eval("WastageCode") %>
												</span>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入人"><ItemTemplate>
												<%# Eval("InputPerson") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="仓库名称"><ItemTemplate>
												<%# Eval("TreasuryName") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入时间"><ItemTemplate>
												<%# Common2.GetTime(Eval("InputDate").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
												<%# Common2.GetState(Eval("Flowstate").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="说明">
<ItemTemplate>
												<span class="tooltip" title='<%# Eval("explain").ToString() %>'>
													<%# StringUtility.GetStr(Eval("explain").ToString(), 10) %>
												</span>
											</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
								<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
								</webdiyer:AspNetPager>
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
	<asp:HiddenField ID="hfldWastage" runat="server" />
	</form>
</body>
</html>
