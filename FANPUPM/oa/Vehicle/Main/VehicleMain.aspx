<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VehicleMain.aspx.cs" Inherits="oa_Vehicle_Main_Default" %>
<%@ Register TagPrefix="MyUserControl" TagName="oa_vehicle_vehicleusercontrol_typecontrol_ascx" Src="~/oa/Vehicle/VehicleUserControl/TypeControl.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="oa_vehicle_vehicleusercontrol_vehilclename_ascx" Src="~/oa/Vehicle/VehicleUserControl/VehilcleName.ascx" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>车辆信息</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script src="/StockManage/script/Config2.js" type="text/javascript"></script>
	<script src="../../../StockManage/script/JustWinTable.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			replaceEmptyTable();
			var contractTable = new JustWinTable('gvwContract');
			setButton(contractTable, 'btnDelete', 'btnUpdate', 'btnQuery', 'hfldContract');
			registerUpdateVehicleItem();
			registerQueryVehicleItem();
		});

		// 新增
		function addVehicls() {
			//parent.desktop.flowclass = window;
			top.ui._VehicleMain = window;
			var url = "/oa/Vehicle/Main/Default.aspx?Action=Add&Random=" + new Date().getTime();
			toolbox_oncommand(url, "新增车辆信息");
		}

		// 删除
		function registerDeleteVehicleItem() {
			var btnDelete = document.getElementById('btnDelete');
			btnDelete.onclick = function () {
				if (!window.confirm('系统提示：\n\n确定要删除吗？')) {
					return false;
				}
			}
		}

		// 编辑
		function registerUpdateVehicleItem() {
			var btnUpdate = document.getElementById('btnUpdate');
			var hfldContract = document.getElementById('hfldContract');
			addEvent(btnUpdate, 'click', function () {
				//parent.desktop.flowclass = window;
				top.ui._VehicleMain = window;
				var url = "/oa/Vehicle/Main/Default.aspx?Action=Update&VehicletId=" + hfldContract.value + '&Random=' + new Date().getTime();
				toolbox_oncommand(url, "编辑车辆信息");
			});
		}

		// 查看
		function registerQueryVehicleItem() {
			var btnQuery = document.getElementById('btnQuery');
			var hfldContract = document.getElementById('hfldContract');
			addEvent(btnQuery, 'click', function () {
				//parent.desktop.flowclass = window;
				top.ui._VehicleMain = window;
				var url = "/oa/Vehicle/Main/Default.aspx?Action=Query&bc=081&bcl=001&ic=" + hfldContract.value + "&VehicletId=" + hfldContract.value + '&Random=' + new Date().getTime();
				toolbox_oncommand(url, "查看车辆信息");
			});
		}

		// 当数据量为空时，修改样式
		function replaceEmptyTable() {
			if (!document.getElementById('gvwContract')) return;
			if (!document.getElementById('emptyContractType')) return;
			var gvwContractType = document.getElementById('gvwContract');
			var emptyContractType = document.getElementById('emptyContractType');
			if (gvwContractType.firstChild.childNodes.length == 1) {
				gvwContractType.replaceChild(emptyContractType.firstChild, gvwContractType.firstChild);
			}
		}

		function rowQueryVehicle(path) {
			viewopen(path, "查看车辆信息");
		}

		function viewopen_n(url) {
			toolbox_oncommand(url, "查看车辆信息");
		}

		// 选择车辆户主
		function selectVehicleName() {
			jw.selectOneUser({ nameinput: 'txtVehicleName', codeinput: 'hfldVehicleNameID' });
		}

		// 选择车辆
		function selectVehicle() {
			var url = '/oa/Vehicle/DivSelectVehicle.aspx';
			top.ui.callback = function (c) {
				$('#hdnVehiclesCode').val(c.code);
				$('#txtVehicleCode').val(c.name);
			};
			top.ui.openWin({ title: '选择车辆', url: url });
		}

		$(function () {
			$(".queryTable td").attr("style", "white-space:nowrap;");
		});
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		<tr id="header">
			<td style="line-height: 28px;">
				车辆信息
			</td>
		</tr>
		<tr>
			<td style="vertical-align: top;">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							车辆类型
						</td>
						<td>
							<MyUserControl:oa_vehicle_vehicleusercontrol_typecontrol_ascx ID="TypeControl1" Width="120px" runat="server" />
						</td>
						<td class="descTd">
							车牌号码
						</td>
						<td>
							<span id="spanPrj" class="spanSelect" style="width: 122px;">
								<asp:TextBox Style="width: 96px; height: 15px; border: none; line-height: 16px; margin: 1px 2px" ID="txtVehicleCode" runat="server"></asp:TextBox>
								<img style="float: right;" src="../../../images/icon.bmp" alt="选择车辆" id="imgPrj"
									onclick="selectVehicle();" />
							</span>
							<asp:HiddenField ID="hdnVehiclesCode" runat="server" />
						</td>
						<td class="descTd">
							购买日期
						</td>
						<td>
							<asp:TextBox ID="txtBeinTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							至
						</td>
						<td>
							<asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							车辆户主
						</td>
						<td>
							<span id="spanVehicleName" style="width: 118px;" class="spanSelect" runat="server">
								<asp:TextBox ID="txtVehicleName" Style="height: 15px; border: none; line-height: 16px;
									width: 91px; margin: 1px 2px;" CssClass="{required:true, messages:{required:'车辆户主必须输入'}}" runat="server"></asp:TextBox>
								<img style="float: right;" src="../../../images/icon.bmp" id="img1" alt="请选择车辆户主" onclick="selectVehicleName();" runat="server" />

								<asp:HiddenField ID="hfldVehicleNameID" runat="server" />
							</span>
							
						</td>
						<td class="descTd">
							识别号码
						</td>
						<td>
							<asp:TextBox ID="txtVehicleIdentify" Width="120px" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							归属地
						</td>
						<td>
							<asp:TextBox ID="txtAddress" Width="120px" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							是否为入股
						</td>
						<td>
							<asp:DropDownList ID="DropDownList1" runat="server"><asp:ListItem Text="--请选择--" Value="" /><asp:ListItem Text="是" Value="1" /><asp:ListItem Text="否" Value="0" /></asp:DropDownList>
						</td>
						<td>
							<asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
						</td>
						<td>
							&nbsp;
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td style="vertical-align: top;">
				<div style="line-height: 1px;">
					&nbsp;</div>
				<table class="tab" style="vertical-align: top;">
					<tr>
						<td class="divFooter" style="text-align: left">
							<input type="button" id="btnAdd" onclick="addVehicls();" value="新增" />
							<input type="button" id="btnUpdate" value="编辑" disabled="disabled" />
							<asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
							<input type="button" id="btnQuery" disabled="disabled" value="查看" />
						</td>
					</tr>
					<tr>
						<td>
							<div>
								<asp:GridView ID="gvwContract" CssClass="gvdata" AllowPaging="true" AutoGenerateColumns="false" OnRowDataBound="gvwContract_RowDataBound" OnPageIndexChanging="gvwContract_PageIndexChanging" DataKeyNames="guid,VehicleCode" runat="server">
<EmptyDataTemplate>
										<table id="emptyContractType" class="gvdata">
											<tr class="header">
												<th scope="col" style="width: 20px;">
													<input id="chk1" type="checkbox" />
												</th>
												<th scope="col" style="width: 25px;">
													序号
												</th>
												<th scope="col">
													车辆名称
												</th>
												<th scope="col">
													车牌号码
												</th>
												<th scope="col">
													识别号码
												</th>
												<th scope="col">
													车辆类型
												</th>
												<th scope="col">
													购买日期
												</th>
												<th scope="col">
													年检日期
												</th>
												<th scope="col">
													保险日期
												</th>
												<th scope="col">
													发票价格
												</th>
												<th scope="col">
													入账价格
												</th>
												<th scope="col">
													车辆状态
												</th>
												<th scope="col">
													归属地
												</th>
												<th scope="col">
													备注
												</th>
											</tr>
										</table>
									</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
												<asp:CheckBox ID="chkAll" runat="server" />
											</HeaderTemplate><ItemTemplate>
												<asp:CheckBox ID="chk" runat="server" />
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
											</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="Specification" HeaderText="车辆名称" /><asp:TemplateField HeaderText="车牌号码"><ItemTemplate>
												<span class="link" onclick="viewopen_n('/oa/Vehicle/Main/Default.aspx?Action=Query&q=alert&VehicletId=<%# Eval("guid") %>')">
													<%# Eval("VehicleCode") %></span>
											</ItemTemplate></asp:TemplateField><asp:BoundField DataField="VehicleIdentify" HeaderText="识别号码" /><asp:TemplateField HeaderText="车辆类型" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
												<%# getVehicleName(Eval("VehicleType").ToString()) %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="购买日期" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
												<%# Common2.GetTime(Eval("PurchaseDate")) %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="年检日期" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
												<%# Common2.GetTime(Eval("InspectionDate")) %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="保险日期" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
												<%# Common2.GetTime(Eval("InsuranceDate")) %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="发票价格" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
												<%# (Eval("Fatfare") == null) ? "" : Eval("Fatfare").ToString() %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="入账价格" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
												<%# (Eval("Recordedprice") == null) ? "" : Eval("Recordedprice").ToString() %>
											</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="State" HeaderText="车辆状态" ItemStyle-HorizontalAlign="Center" /><asp:BoundField DataField="Address" HeaderText="归属地" /><asp:TemplateField HeaderText="备注"><ItemTemplate>
												<%# (Eval("Remark") == null) ? "" : StringUtility.GetStr(Eval("Remark"), 10) %>
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
	<div id="divFramRole" title="" style="display: none">
		<iframe id="iframeRole" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<asp:HiddenField ID="hfldContract" runat="server" />
	</form>
</body>
</html>
