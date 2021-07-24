<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReimbursementManage.aspx.cs" Inherits="oa_Vehicle_Reimbursement_ReimbursementManage" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>司机报销</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/ecmascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<link href="/StockManage/skins/sm4.css" rel="stylesheet" type="text/css" />
<link href="/StyleCss/Common.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$("#btn_Search")[0].onclick = function () {
				if (!$('#form1').form('validate')) {
					return false;
				}
			}
			replaceEmptyTable('gvTable', 'GvReimbursement');
			var purchasePlanTable = new JustWinTable('GvReimbursement');
			formateTable('GvReimbursement', 2, true);
			setButton(purchasePlanTable, 'btnDel', 'btnEdit', 'btnLook', 'hfldPurchaseChecked');
			addEvent(document.getElementById('btnEdit'), 'click', rowEdit);
			addEvent(document.getElementById('btnLook'), 'click', rowQuery);
			addEvent(document.getElementById('btnAdd'), 'click', rowAdd);
		});
		function rowAdd() {
			top.ui._ReimbursementManage = window;
			var url = "/oa/Vehicle/Reimbursement/ReimbursementEdit.aspx?type=add";
			toolbox_oncommand(url, "新增司机报销单");
		}
		function rowEdit() {
			top.ui._ReimbursementManage = window;
			var url = "/oa/Vehicle/Reimbursement/ReimbursementEdit.aspx?type=edit&id=" + document.getElementById("hfldPurchaseChecked").value;
			toolbox_oncommand(url, "编辑司机报销单");
		}
		function rowQuery() {
			top.ui._ReimbursementManage = window;
			var url = "/oa/Vehicle/Reimbursement/ReimbursementEdit.aspx?type=view&id=" + document.getElementById("hfldPurchaseChecked").value;
			toolbox_oncommand(url, "查看司机报销单");
		}


		// 车辆信息超链接
		function viewopen_n(url) {
			toolbox_oncommand(url, "查看车辆信息");
		}

		// 选择人员
		function selectUser() {
			jw.selectOneUser({ nameinput: 'txtPeople', codeinput: 'hdnPeople' });
		}

		// 选择车辆
		function selectVehicle() {
			top.ui.callback = function (obj) {
				$('#hdnVehiclesCode').val(obj.code);
				$('#txtVehiclesCode').val(obj.name);
			}
			top.ui.openWin({ title: '选择车辆', url: '/oa/Vehicle/DivSelectVehicle.aspx' });
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
	<form id="form1" runat="server">
	<div>
		<table border="0" cellpadding="0" cellspacing="0" width="105%">
			<tr style="height: 20px;">
				<td class="divHeader">
					司机报销
				</td>
			</tr>
			<tr>
				<td style="width: 100%; vertical-align: top;">
					<table class="queryTable" cellpadding="3px" cellspacing="0px">
						<tr>
							<td class="descTd">
								报销日期开始
							</td>
							<td>
								<asp:TextBox ID="txtStartTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
							</td>
							<td class="descTd">
								报销日期结束
							</td>
							<td>
								<asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
							</td>
							<td class="descTd">
								车牌号码
							</td>
							<td>
								<span id="spanPrj" class="spanSelect" style="width: 122px;">
									<asp:TextBox ID="txtVehiclesCode" CssClass="easyui-validatebox" data-options="required:false, validType:'validChars[50]'" Style="width: 97px; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px" runat="server"></asp:TextBox>
									<img src="../../../images/icon.bmp" style="float: right;" alt="选择车辆" id="imgPrj"
										onclick="selectVehicle();" />
								</span>
								<asp:HiddenField ID="hdnVehiclesCode" runat="server" />
							</td>
							<td class="descTd">
								司机姓名
							</td>
							<td>
								<span id="span1" class="spanSelect" style="width: 122px;">
									<asp:TextBox ID="txtPeople" CssClass="easyui-validatebox" data-options="required:false, validType:'validChars[50]'" Style="width: 97px; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px" runat="server"></asp:TextBox>
									<img src="../../../images/icon.bmp" style="float: right;" alt="请双击此处选择" id="img1"
										onclick="selectUser();" />
								</span>
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
							</td>
						</tr>
						<tr>
							<td>
								<div class="" style="width: 100%;">
									<asp:GridView ID="GvReimbursement" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" PageSize="14" ShowFooter="true" OnRowDataBound="GvReimbursement_RowDataBound" OnPageIndexChanging="GvReimbursement_PageIndexChanging" DataKeyNames="guid" runat="server">
<EmptyDataTemplate>
											<table id="gvTable" class="gvdata" style="width: 100%; text-align: center;">
												<tr class="header">
													<th style="width: 20px;">
														<asp:CheckBox ID="cbAllBox" runat="server" />
													</th>
													<th style="width: 25px;">
														序号
													</th>
													<th>
														车牌号码
													</th>
													<th>
														司机姓名
													</th>
													<th>
														报销日期
													</th>
													<th>
														过路费
													</th>
													<th>
														油费
													</th>
													<th>
														修理费
													</th>
													<th>
														保养费
													</th>
													<th>
														小计
													</th>
													<th>
														备注
													</th>
												</tr>
											</table>
										</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
													<asp:CheckBox ID="cbAllBox" runat="server" />
												</HeaderTemplate>

<ItemTemplate>
													<asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("Guid").ToString()) %>' runat="server" />
												</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
													<%# Container.DataItemIndex + 1 %>
												</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="Guid" Visible="false"><ItemTemplate>
													<%# Eval("Guid").ToString() %>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="车牌号码">
<ItemTemplate>
													<span class="link" onclick="viewopen_n('/oa/Vehicle/Main/Default.aspx?Action=Query&q=alert&VehicletId=<%# Eval("VehicleGuid") %>')">
														<%# Eval("VehicleCode").ToString() %></span>
												</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="司机姓名">
<ItemTemplate>
													<%# Eval("UserName").ToString() %>
												</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="报销日期">
<ItemTemplate>
													<%# Convert.ToDateTime(Eval("Date")).ToString("yyyy-MM-dd") %>
												</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="过路费" DataField="Tolls" /><asp:BoundField HeaderText="油费" DataField="Repairs" /><asp:BoundField HeaderText="修理费" DataField="FuelCosts" /><asp:BoundField HeaderText="保养费" DataField="MaintenanceCosts" /><asp:TemplateField HeaderText="小计"></asp:TemplateField><asp:BoundField HeaderText="备注" DataField="remark" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
	</div>
	</form>
</body>
</html>
