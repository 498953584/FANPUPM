<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="oa_Vehicle_Main_VehicleMainEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>车辆信息</title><link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

	<script src="/Script/jquery.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script src="/StockManage/script/Config2.js" type="text/javascript"></script>
	<script src="/StockManage/script/JustWinTable.js" type="text/javascript"></script>
	<script src="/StockManage/script/Validation.js" type="text/javascript"></script>
	<link href="../../../StockManage/skins/sm4.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var gvBudget = new JustWinTable('gvBudget');
			if (getRequestParam('Action') == 'Query' || !getRequestParam('Action')) {
				setAllInputDisabled();
			}
			registerBtnCancelEvent();
			Val.validate('form1');
		});

		function registerBtnCancelEvent() {
			var btnCancel = document.getElementById('btnCancel');
			addEvent(btnCancel, 'click', function () {
				winclose('PayoutContractEdit', 'PayoutContract.aspx', false)
			});
		}

		// 选择购买人员
		function selectPurchaser() {
			jw.selectOneUser({ nameinput: 'txtPurchaser', codeinput: 'hfldPurchaserTypeID' });
		}

		function returnPurchaser(id, name) {
			document.getElementById('').value = id;
			document.getElementById('').value = name;
		}

		// 选择车辆户主
		function selectVehicleName() {
			jw.selectOneUser({ nameinput: 'txtVehicleName', codeinput: 'HiddenField1' });
		}

		// 选择车辆类型
		function selectVehicleType() {
			var url = '/oa/Vehicle/VehicleUserControl/VehicleType.aspx';
			top.ui.callback = function (t) {
				$('#txtVehicleType').val(t.name);
				$('#hfldVehicleType').val(t.code);
			}
			top.ui.openWin({ title: '选择车辆类型', url: url, width: 350, height: 350 });
		}

		// 选择车辆状态
		function selectVehicleState() {
			var url = '/oa/Vehicle/VehicleUserControl/VehicleState.aspx';
			top.ui.callback = function (t) {
				$('#hfldVehicleState').val(t.code);
				$('#txtVehicleState').val(t.name);
			}
			top.ui.openWin({ title: '选择车辆状态', url: url });
		}

	</script>
	<style type="text/css">
		.word
		{
			width: 18%;
			text-align: right;
		}
		.txt
		{
			width: 40%;
			text-align: left;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divHeader2">
		<table class="tableHeader">
			<tr>
				<td>
					<asp:Label ID="lblTitle" Font-Bold="true" Text="车辆信息" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<div style="overflow: auto; margin: auto; height: 95%;">
		<table style="width: 80%; margin: auto;" cellspacing="0" cellpadding="5px">
			<tr>
				<td class="word">
					车牌号码
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtVehicleCode" Height="15px" Width="99%" CssClass="{required:true, messages:{required:'车牌号码必须输入'}}" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					车辆户主
				</td>
				<td class="txt ">
					<span id="span1" style="width: 99%; background-color: #FEFEF4;" class="spanSelect" runat="server">
						<asp:TextBox ID="txtVehicleName" Style="height: 15px; border: none; line-height: 16px;
							width: 89%; margin: 1px 2px;" runat="server"></asp:TextBox>
						<img style="float: right;" src="../../../images/icon.bmp" id="img1" alt="请选择车辆户主" onclick="selectVehicleName();" runat="server" />

						<asp:HiddenField ID="HiddenField1" runat="server" />
					</span>
				</td>
			</tr>
			<tr>
				<td class="word">
					识别号码
				</td>
				<td class="txt ">
					<asp:TextBox ID="txtVehicleIdentify" Height="15px" Width="99%" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					发动机号
				</td>
				<td class="txt ">
					<asp:TextBox ID="txtEngineCode" Height="15px" Width="99%" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					型号名称
				</td>
				<td class="txt ">
					<asp:TextBox ID="txtSpecification" Height="15px" Width="99%" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					归属地
				</td>
				<td class="txt ">
					<asp:TextBox ID="txtAddress" Height="15px" Width="99%" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					购买日期
				</td>
				<td class="txt ">
					<asp:TextBox ID="txtPurchaseDate" Height="15px" Width="99%" onclick="WdatePicker()" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					上户日期
				</td>
				<td class="txt ">
					<asp:TextBox ID="txtOnHouserDate" Height="15px" Width="99%" onclick="WdatePicker()" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					年检日期
				</td>
				<td class="txt ">
					<asp:TextBox ID="txtInspectionDate" Height="15px" Width="99%" onclick="WdatePicker()" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					保险日期
				</td>
				<td class="txt ">
					<asp:TextBox ID="txtInsuranceDate" Height="15px" Width="99%" onclick="WdatePicker()" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					备用钥匙
				</td>
				<td class="txt ">
					<asp:TextBox ID="txtSparekey" Height="15px" Width="99%" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					能力
				</td>
				<td class="txt ">
					<asp:TextBox ID="txtAbility" Height="15px" Width="99%" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					发票价格
				</td>
				<td class="txt ">
					<asp:TextBox ID="txtFatfare" Height="15px" Width="99%" CssClass="{number:true, messages:{number:'发票价格格式错误'}}" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					入账价格
				</td>
				<td class="txt ">
					<asp:TextBox ID="txtRecordedprice" CssClass="{number:true,messages:{number:'入账价格格式错误'}}" Height="15px" Width="99%" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					出厂日期
				</td>
				<td class="txt ">
					<asp:TextBox ID="txtManufactureDate" Height="15px" Width="99%" onclick="WdatePicker()" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					月折旧率
				</td>
				<td class="txt ">
					<asp:TextBox ID="txtDepreciationRate" Height="15px" Width="99%" CssClass="{number:true,messages:{number:'月折旧率格式错误'}}" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					车辆类型
				</td>
				<td class="txt mustInput">
					<span id="spanVehicleType" style="width: 99%; background-color: #FEFEF4;" class="spanSelect" runat="server">
						<asp:TextBox ID="txtVehicleType" Style="height: 15px; border: none; line-height: 16px;
							width: 89%; margin: 1px 2px;" CssClass="{required:true, messages:{required:'请选择车辆类型'}}" runat="server"></asp:TextBox></asp:TextBox>
						<img style="float: right;" src="../../../images/icon.bmp" id="img2" 
							alt="请选择车辆类型" <%=this.selectVehicle %> />
						<asp:HiddenField ID="hfldVehicleType" runat="server" />
					</span>
				</td>
				<td class="word">
					购买人
				</td>
				<td class="txt ">
					<span id="spanContractType" style="width: 99%; background-color: #FEFEF4;" class="spanSelect" runat="server">
						<asp:TextBox ID="txtPurchaser" Style="height: 15px; border: none; line-height: 16px;
							width: 89%; margin: 1px 2px;" runat="server"></asp:TextBox>
						<img style="float: right;" src="../../../images/icon.bmp" id="img" 
							alt="请选择车辆购买人" <%=this.selectPurchaser %> />
						<asp:HiddenField ID="hfldPurchaserTypeID" runat="server" />
					</span>
				</td>
			</tr>
			<tr>
				<td class="word">
					车辆状态
				</td>
				<td class="txt mustInput">
					<span id="spanVehicleState" style="width: 99%; background-color: #FEFEF4;" class="spanSelect" runat="server">
						<asp:TextBox ID="txtVehicleState" Style="height: 15px; border: none; line-height: 16px;
							width: 89%; margin: 1px 2px;" CssClass="{required:true, messages:{required:'请选择车辆类型'}}" runat="server"></asp:TextBox></asp:TextBox>
						<img style="float: right;" src="../../../images/icon.bmp" id="img3" 
							alt="请选择车辆类型" <%=this.selectVehicleState %> />
						<asp:HiddenField ID="hfldVehicleState" runat="server" />
					</span>
				</td>
				<td class="word">
					入股标示
				</td>
				<td>
					<asp:CheckBox ID="RadButIsShare" runat="server" />
				</td>
			</tr>
			<tr>
				<td class="word">
					备注
				</td>
				<td class="txt" colspan="3">
					<asp:TextBox ID="txtRemark" Rows="3" Height="50px" Width="99%" TextMode="MultiLine" runat="server"></asp:TextBox>
				</td>
			</tr>
		</table>
	</div>
	<div id="divFooter" class="divFooter2">
		<table class="tableFooter2">
			<tr>
				<td>
					<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
					<input type="button" id="btnCancel" value="取消" />
				</td>
			</tr>
		</table>
	</div>
	<div id="divFramPrj" title="" style="display: none">
		<iframe id="ifFramPrj" frameborder="0" width="99%" height="99%" src=""></iframe>
	</div>
	<asp:HiddenField ID="vehicle_Code_Number_session" runat="server" />
	</form>
</body>
</html>
