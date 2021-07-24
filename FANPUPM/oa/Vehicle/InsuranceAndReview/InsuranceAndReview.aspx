<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsuranceAndReview.aspx.cs" Inherits="oa_Vehicle_InsuranceAndReview_InsuranceAndReview" %>
<%@ Import Namespace="cn.justwin.BLL"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

	<script src="/Script/jquery.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script src="/StockManage/script/Config2.js" type="text/javascript"></script>
	<script src="/StockManage/script/JustWinTable.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script language="javascript" type="text/javascript">
		addEvent(window, 'load', function () {
			replaceEmptyTable('emptyContractType', 'IARgridView');
			var contractTable = new JustWinTable('IARgridView');
			formateTable('IARgridView', 2, true);
			setButton(contractTable, 'btnDelete', 'btnUpdate', 'btnQuery', 'hfldIRA');
			registerUpdateIARtEvent();
			registerQueryIAREvent();
			registerDeleteIAREvent()
		});
		function viewopen_n(url) {
			toolbox_oncommand(url, "查看车辆信息");
		}
		//增加
		function addIAR() {
		    top.ui._InsuranceAndReview = window;
			var url = "/oa/Vehicle/InsuranceAndReview/InsuranceAndReviewEdit.aspx?Action=Add&Random=" + new Date().getTime();
			toolbox_oncommand(url, "新增车辆交强险与审车");
		}
		//更新
		function registerUpdateIARtEvent() {
			var btnUpdate = document.getElementById('btnUpdate');
			var hfldIRA = document.getElementById('hfldIRA');
			addEvent(btnUpdate, 'click', function () {
			    top.ui._InsuranceAndReview = window;
				var url = "/oa/Vehicle/InsuranceAndReview/Edit.aspx?Action=Update&IARGuid=" + hfldIRA.value + '&Random=' + new Date().getTime();
				toolbox_oncommand(url, "编辑车辆交强险与审车");
			});
		}
		function queryItem(url) {
		    top.ui._InsuranceAndReview = window;
			toolbox_oncommand(url, "查看车辆交强险与审车");
		}
		//查看
		function registerQueryIAREvent() {
			var btnQuery = document.getElementById('btnQuery');
			var hfldIRA = document.getElementById('hfldIRA');
			addEvent(btnQuery, 'click', function () {
				parent.desktop.flowclass = window;
				var url = "/oa/Vehicle/InsuranceAndReview/Edit.aspx?Action=Query&bc=081&bcl=001&ic=" + hfldIRA.value + "&IARGuid=" + hfldIRA.value + '&Random=' + new Date().getTime();
				toolbox_oncommand(url, "查看车辆交强险与审车");
			});
		}
		//删除
		function registerDeleteIAREvent() {
			var btnDelete = document.getElementById('btnDelete');
			btnDelete.onclick = function () {
				if (!window.confirm('系统提示：\n\n确定要删除吗？')) {
					return false;
				}
			}
		}
		function replaceEmptyTable() {
			if (!document.getElementById('IARgridView')) return;
			if (!document.getElementById('emptyContractType')) return;
			var IARgridViewType = document.getElementById('IARgridView');
			var emptyContractType = document.getElementById('emptyContractType');
			if (IARgridViewType.firstChild.childNodes.length == 1) {
				IARgridViewType.replaceChild(emptyContractType.firstChild, IARgridViewType.firstChild);
			}
		}
		//选择车辆
		function selectVehicle() {
			var url = "/oa/Vehicle/DivSelectVehicle.aspx";
			top.ui.callback = function (o) {
				$('#hdnVehiclesCode').val(o.code);
				$('#txtVehicleCode').val(o.name)
			}
			top.ui.openWin({ title: '选择车辆', url: url });
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div style="width: 100%; overflow: auto; height: 100%;">
		<div>
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td class="divHeader">
						交强险与审车
					</td>
				</tr>
				<tr>
					<td style="vertical-align: top;">
						<table class="queryTable" style="" cellpadding="3px" cellspacing="0px">
							<tr>
								<td class="descTd">
									<div style="width: 30px;">
										编码
									</div>
								</td>
								<td>
									<asp:TextBox ID="txtCode" Width="120px" runat="server"></asp:TextBox>
								</td>
								<td class="descTd">
									<div style="width: 48px;">
										车牌号码
									</div>
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
									<div style="width: 48px;">
										开始日期
									</div>
								</td>
								<td>
									<asp:TextBox ID="txtBeinTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
								</td>
								<td class="descTd">
									<div style="width: 48px;">
										结束日期
									</div>
								</td>
								<td>
									<asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
								</td>
								<td class="descTd">
									<div style="width: 68px;">
										交强险/审车
									</div>
								</td>
								<td>
									<asp:DropDownList ID="DropDownList1" runat="server"><asp:ListItem Text="--请选择--" Value="" /><asp:ListItem Text="交强险" Value="0" /><asp:ListItem Text="审车" Value="1" /></asp:DropDownList>
								</td>
								<td>
									<asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
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
									<input type="button" id="Button1" onclick="addIAR();" value="新增" />
									<input type="button" id="btnUpdate" value="编辑" disabled="disabled" />
									<asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
									<input type="button" id="btnQuery" disabled="disabled" value="查看" />
								</td>
							</tr>
							<tr>
								<td>
									<asp:GridView ID="IARgridView" CssClass="gvdata" Width="100%" AllowPaging="true" AutoGenerateColumns="false" OnRowDataBound="IARgridViewType_RowDataBound" OnPageIndexChanging="IARgridView_PageIndexChanging" DataKeyNames="guid" runat="server">
<EmptyDataTemplate>
											<table id="emptyContractType" cellspacing="0" rules="all" border="1" class="rowa">
												<tr class="header">
													<th scope="col" style="width: 20px;">
														<input id="chk1" type="checkbox" />
													</th>
													<th scope="col" style="width: 25px;">
														序号
													</th>
													<th scope="col">
														类型编码
													</th>
													<th scope="col">
														类型名称
													</th>
													<th scope="col">
														交强险/审车
													</th>
													<th scope="col">
														日期
													</th>
												</tr>
											</table>
										</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
													<asp:CheckBox ID="chkSelectAll" runat="server" />
												</HeaderTemplate><ItemTemplate>
													<asp:CheckBox ID="chkSelectSingle" runat="server" />
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
													<%# Container.DataItemIndex + 1 %>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="编码"><ItemTemplate>
													<%# Eval("code") %>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="车牌号码"><ItemTemplate>
													<span class="link" onclick="viewopen_n('/oa/Vehicle/Main/Default.aspx?Action=Query&VehicletId=<%# Eval("VehicleCode") %>')">
														<%# getVehicleCode((Eval("VehicleCode") == null) ? "" : Eval("VehicleCode").ToString()) %>
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="交强险/审车"><ItemTemplate>
													<%# (Eval("Type") == null) ? "" : ((Eval("Type").ToString() == "0") ? "交强险" : "审车") %>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="日期" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
													<%# Common2.GetTime(Eval("Date")) %>
												</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<asp:HiddenField ID="hfldIRA" runat="server" />
			<asp:HiddenField ID="hfldIRATypeGuid" runat="server" />
			<asp:HiddenField ID="hfldSelectedPrj" runat="server" />
		</div>
	</div>
	</form>
</body>
</html>
<script language="javascript" type="text/javascript">
	$(function () {
		$("#IARgridView tr").each(function (i) {
			$(this).find("td").last().addClass("newCon");
		});
	});
</script>
