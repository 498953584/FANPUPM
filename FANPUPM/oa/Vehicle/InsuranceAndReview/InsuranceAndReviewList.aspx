<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsuranceAndReviewList.aspx.cs" Inherits="oa_Vehicle_InsuranceAndReview_InsuranceAndReviewList" %>
<%@ Import Namespace="cn.justwin.BLL"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script src="/StockManage/script/Config2.js" type="text/javascript"></script>
	<script src="/StockManage/script/JustWinTable.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			replaceEmptyTable();
			var contractTable = new JustWinTable('IARgridView');
		});

		function viewopen_n(url) {
			toolbox_oncommand(url, "查看车辆信息");
		}


		function queryItem(url) {
			parent.desktop.flowclass = window;
			toolbox_oncommand(url, "查看车辆交强险与审车");
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

		// 选择车辆
		function selectVehicle() {
			top.ui.callback = function (obj) {
				$('#hdnVehiclesCode').val(obj.code);
				$('#txtVehicleCode').val(obj.name);
			}
			top.ui.openWin({ title: '选择车辆', url: '/oa/Vehicle/DivSelectVehicle.aspx' });
		}

	</script>
	<style type="text/css">
		.newCon
		{
			text-align: center;
		}
	</style>
</head>
<body style="scroll: auto;">
	<form id="form1" runat="server">
	<div>
		<div id="divFram" title="" style="display: none">
			<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
		</div>
		<table border="0" cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td class="divHeader">
					交强险与年审
				</td>
			</tr>
			<tr>
				<td style="vertical-align: top; width: 100%;">
					<table class="queryTable" cellpadding="3px" cellspacing="0px">
						<tr>
							<td class="descTd">
								<div style="width: 25px;">
									编码
								</div>
							</td>
							<td>
								<asp:TextBox ID="txtCode" Width="120px" runat="server"></asp:TextBox>
							</td>
							<td class="descTd">
								<div style="width: 48px;">
									车牌号码</div>
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
									开始日期</div>
							</td>
							<td>
								<asp:TextBox ID="txtBeinTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
							</td>
							<td class="descTd">
								<div style="width: 48px;">
									结束日期</div>
							</td>
							<td>
								<asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
							</td>
							<td class="descTd">
								<div style="width: 68px;">
									交强险/审车</div>
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
							<td>
								<asp:GridView ID="IARgridView" CssClass="gvdata" Width="100%" AllowPaging="true" AutoGenerateColumns="false" OnRowDataBound="IARgridViewType_RowDataBound" OnPageIndexChanging="IARgridView_PageIndexChanging" DataKeyNames="guid" runat="server">
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
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="编码"><ItemTemplate>
												<%# Eval("code") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="车牌号码"><ItemTemplate>
												<span class="link" onclick="viewopen_n('/oa/Vehicle/Main/Default.aspx?Action=Query&VehicletId=<%# Eval("VehicleCode") %>')">
													<%# getVehicleCode((Eval("VehicleCode") == null) ? "" : Eval("VehicleCode").ToString()) %>
												</span>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="交强险/审车"><ItemTemplate>
												<%# (Eval("Type") == null) ? "" : ((Eval("Type").ToString() == "0") ? "交强险" : "审车") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="日期"><ItemTemplate>
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
