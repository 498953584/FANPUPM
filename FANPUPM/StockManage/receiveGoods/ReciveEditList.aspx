<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReciveEditList.aspx.cs" Inherits="StockManage_receiveGoods_ReciveEditList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>发货通知单列表</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../script/Config2.js"></script>
	<script type="text/javascript" src="../script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/jwJson.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var purchasePlanTable = new JustWinTable('gvPurchaseplan');

			addEvent(document.getElementById('btnEdit'), 'click', rowEdit);
			addEvent(document.getElementById('btnLook'), 'click', rowQuery);

			setButton(purchasePlanTable, 'hdfDel', 'btnEdit', 'btnLook', 'hfldPurchaseChecked');
			setWfButtonState2(purchasePlanTable, 'WF1');

			replaceEmptyTable('emptyPurchaseplan', 'gvPurchaseplan');
		});

		function rowEdit() {
			top.ui._addReceiveNote = window;
			var url = "/StockManage/receiveGoods/AddReceiveNote.aspx?type=edit&id=" + document.getElementById("hfldPurchaseChecked").value;
			toolbox_oncommand(url, "收货验收");
		}
		function rowQuery() {
			var url = "ViewReceiveNote.aspx?t=1&ic=" + document.getElementById("hfldPurchaseChecked").value;
			viewopen(url, 820, 500);
		}

		function ClickRow(FlowGuid, AuditState) {
			document.getElementById('hfldPurchaseChecked').value = FlowGuid;
		}

		//选择人员
		function selectUser() {
			jw.selectOneUser({ nameinput: 'txtPeople', codeinput: 'hdnPeople' });
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
							起始时间
						</td>
						<td>
							<asp:TextBox ID="txtStartTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							结束时间
						</td>
						<td>
							<asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							收货通知单号
						</td>
						<td>
							<asp:TextBox ID="txtsnCode" Height="15px" Width="120px" runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr style="height: 25px;">
						<td class="descTd">
							收 &nbsp; 货 人
						</td>
						<td>
							<input type="text" style="width: 120px;" id="txtPeople" class="select_input" imgclick="selectUser" runat="server" />

							<asp:HiddenField ID="hdnPeople" runat="server" />
						</td>
						<td colspan="4">
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
							<input id="btnEdit" type="button" value="修改验收单" style="width: 80px;" disabled="disabled" />
							<input type="button" id="btnLook" value="查看" disabled="disabled" />
							<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiClass="001" BusiCode="096" runat="server" />
							<asp:HiddenField ID="hdfDel" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							<div class="">
								<asp:GridView ID="gvPurchaseplan" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvPurchaseplan_PageIndexChanging" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="rnId,AuditState" runat="server">
<EmptyDataTemplate>
										<table id="emptyPurchaseplan" class="rowa" style="width: 100%; text-align: center;">
											<tr class="header">
												<th scope="col" style="width: 20px;">
													<asp:CheckBox ID="cbAllBox" runat="server" />
												</th>
												<th scope="col" style="width: 25px;">
													序号
												</th>
												<th scope="col">
													发货通知单
												</th>
												<th scope="col">
													收货人
												</th>
												<th scope="col">
													收货时间
												</th>
												<th scope="col">
													流程状态
												</th>
												<th scope="col">
													收货情况
												</th>
												<th scope="col">
													发货通知单号
												</th>
												<th scope="col">
													发往项目
												</th>
											</tr>
										</table>
									</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
												<asp:CheckBox ID="cbAllBox" runat="server" />
											</HeaderTemplate><ItemTemplate>
												<asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("snId")) %>' runat="server" />
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="收货通知单"><ItemTemplate>
												<span class="al" onclick="viewopen('ViewReceiveNote.aspx?t=1&ic=<%# Eval("rnId") %>',820,500)">
													<%# Eval("rnCode") %>
												</span>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="收货人"><ItemTemplate>
												<asp:Label ID="labUser" Text='<%# Convert.ToString(Eval("rnUser")) %>' runat="server"></asp:Label>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="收货时间"><ItemTemplate>
												<%# Common2.GetTime(Eval("rnTime").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
												<%# Common2.GetState(Eval("AuditState").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="收货情况"><ItemTemplate>
												<%# StringUtility.GetStr(Convert.ToString(Eval("explain"))) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="发货通知单号"><ItemTemplate>
												<%# Eval("snCode") %>
											</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="发往项目" DataField="prjName" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	<asp:HiddenField ID="hdfState" runat="server" />
	</form>
</body>
</html>
