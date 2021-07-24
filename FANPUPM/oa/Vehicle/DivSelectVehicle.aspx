<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivSelectVehicle.aspx.cs" Inherits="Common_DivSelectVehicle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>车辆信息</title>
	<base target="_self" />
	<script src="/Web_Client/Tree.js" type="text/javascript"></script>
	<script src="/Script/jquery.js" type="text/javascript"></script>
	<script src="/StockManage/script/JustWinTable.js" type="text/javascript"></script>
	<script src="/StockManage/script/Config2.js" type="text/javascript"></script>
	<script language="JavaScript" type="text/javascript">
		$(document).ready(function () {
			var aa = new JustWinTable('grdModules');
		});

		function clickRow(obj, theCode, theName) {
			$("#hdName").val(theName);
			$("#hdCode").val(theCode);
			$("#btnSave").removeAttr('disabled');
		}

		function dbClickRow(obj, theCode, theName) {
			successed();
		}

		// 点确定
		function successed() {
			if (top.ui.callback != null) {
				top.ui.callback({ code: $("#hdCode").val(), name: $("#hdName").val() });
				top.ui.callback = null;
			}
			top.ui.closeWin();
		}
		   
	</script>
	<style type="text/css">
		
	</style>
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0" scroll="no">
	<form id="Form1" method="post" runat="server">
	<table id="Table2" class="table-nomal" height="100%" cellspacing="0" cellpadding="0"
		width="100%">
		<tr class="td-title">
			<td width="20" style="border: solid 0px red;">
				<input class="input_hide" id="hdnModuleCode" type="hidden" name="hdnModuleCode" runat="server" />

			</td>
		</tr>
		<tr>
			<td style="text-align: left; border: solid 0px red;">
				车牌号码：<asp:TextBox ID="txtVehicleCode" Width="150px" runat="server"></asp:TextBox>
				车辆户主：<asp:TextBox ID="txtVehicleUser" Width="150px" runat="server"></asp:TextBox>
				<asp:Button ID="SearchBt" Text="查 询" OnClick="SearchBt_Click" runat="server" />
			</td>
		</tr>
		<tr>
			<td valign="top" align="center" style="border: solid 0px red; height: 100%; padding-top: 10px;">
				<div id="pagediv" style="width: 100%; overflow: auto; border: solid 0px red; text-align: left;"
					align="center">
					<asp:GridView ID="gvVehicle" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" OnRowDataBound="gvVehicle_RowDataBound" OnPageIndexChanging="gvVehicle_PageIndexChanging" DataKeyNames="Guid,vehicleCode" runat="server">
<EmptyDataTemplate>
							<table class="rowa" style="width: 100%; text-align: center;">
								<tr class="header">
									<td style="width: 25px;">
										序号
									</td>
									<td>
										车牌号码
									</td>
									<td>
										车辆名称
									</td>
									<td>
										车辆户主
									</td>
									<td>
										识别号码
									</td>
									<td>
										车辆类型
									</td>
									<td>
										车辆状态
									</td>
								</tr>
							</table>
						</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="Guid" DataField="guid" Visible="false" /><asp:BoundField HeaderText="车牌号码" DataField="vehicleCode" /><asp:BoundField HeaderText="车辆名称" DataField="Specification" /><asp:BoundField HeaderText="车辆户主" DataField="VehicleName" /><asp:BoundField HeaderText="识别号码" DataField="EngineCode" /><asp:BoundField HeaderText="车辆类型" DataField="VehicleType" /><asp:BoundField HeaderText="车辆状态" DataField="state" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				</div>
			</td>
		</tr>
		<tr class="td-submit">
			<td align="right" style="border: solid 0px red;">
				<input type="hidden" id="hdn1" name="hdn1" style="width: 10px" runat="server" />

				<input type="hidden" id="hdn2" name="hdn2" style="width: 10px" runat="server" />

				<input type="hidden" id="hdn3" name="hdn3" style="width: 10px" runat="server" />

				<input id="btnSave" type="button" value="确 定" onclick="successed();" disabled="disabled" />
				<input id="btnCancel" type="button" value="取 消" onclick="top.ui.closeWin();" />
			</td>
		</tr>
	</table>
	<input id="hdnModuleCodeList" style="width: 10px" type="hidden" name="hdnModuleCodeList" runat="server" />

	<asp:HiddenField ID="hdName" runat="server" />
	<asp:HiddenField ID="hdCode" runat="server" />
	</form>
</body>
</html>
