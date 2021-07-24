<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VehilcleName.aspx.cs" Inherits="oa_Vehicle_VehicleUserControl_VehilcleName" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title>
	<script type="text/javascript" src="../../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.ui/jquery.ui.core.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.ui/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.ui/jquery.ui.mouse.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.ui/jquery.ui.draggable.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.ui/jquery.ui.position.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.ui/jquery.ui.resizable.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.ui/jquery.ui.dialog.js"></script>
	<script src="../../../SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
	<script src="../../../StockManage/script/Config2.js" type="text/javascript"></script>
	<script src="../../../StockManage/script/JustWinTable.js" type="text/javascript"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			// hideControls(); 
			var typeTable = new JustWinTable('VehicleType');
			//setButton(typeTable, 'btnDelete', 'btnUpdate', 'btnQuery', 'hfldContractTypeGuid')
			typeTable.setBtnStateByJustwinTable('btnUserCodes');
			replaceEmptyTable();
		});
		function replaceEmptyTable() {
			//当数据量为空时，修改样式
			if (!document.getElementById('VehicleType')) return;
			if (!document.getElementById('emptyContractType')) return;
			var VehicleType = document.getElementById('VehicleType');
			var emptyContractType = document.getElementById('emptyContractType');
			if (VehicleType.firstChild.childNodes.length == 1) {
				VehicleType.replaceChild(emptyContractType.firstChild, VehicleType.firstChild);
			}
		}
		function save_VehicleName() {
			$(parent.document).find('#VehilcleName1_txtVehicleName').val($('#hildSelectVal').val());
			$(parent.document).find('.ui-icon-closethick').each(function () {
				this.click();
			});
		}
		function GET_VehicleName(s) {
			$("#hildSelectVal").val(s);
			//save_VehicleName();
		}
		function get_VehicleNameDoubleClick(s) {
			$("#hildSelectVal").val(s);
			save_VehicleName();
		}
	</script>
	<link href="/StockManage/skins/sm4.css" rel="stylesheet" type="text/css" />
<link href="/StyleCss/Common.css" rel="stylesheet" type="text/css" />

	<style type="text/css">
		#container
		{
			margin: 0 auto;
			width: 100%;
		}
		#sidebar
		{
			float: left;
			width: 300px;
			height: 370px;
		}
		* html #sidebar
		{
			margin-right: -3px;
		}
		#content
		{
			height: 370px;
		}
		a
		{
			text-decoration: none;
		}
		a:hover
		{
			color: #0000FF;
			text-decoration: underline;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<div id="container">
		<div id="sidebar">
			
			<asp:TreeView ID="tvDept" ShowLines="true" ExpandDepth="1" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
		</div>
		<div id="content">
			<table width="400px;">
				<tr>
					<td>
						<asp:GridView ID="VehicleType" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" DataKeyNames="v_yhdm" runat="server">
<EmptyDataTemplate>
								<table id="emptyContractType" class="gvdata">
									<tr class="header">
										<th scope="col" style="width: 25px;">
											序号
										</th>
										<th scope="col">
											编码
										</th>
										<th scope="col">
											姓名
										</th>
									</tr>
								</table>
							</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="编码"><ItemTemplate>
										<%# Eval("v_yhdm") %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="名称"><ItemTemplate>
										<a href="javascript:void(0);" ondblclick="get_VehicleNameDoubleClick('<%# Eval("v_xm") %>')"
											onclick="GET_VehicleName('<%# Eval("v_xm") %>')">
											<%# Eval("v_xm") %>
										</a>
									</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					</td>
				</tr>
			</table>
		</div>
	</div>
	<asp:HiddenField ID="hfldTypeName" Value="" runat="server" />
	<asp:HiddenField ID="hfldTypeId" Value="" runat="server" />
	<asp:HiddenField ID="hildSelectVal" Value="" runat="server" />
	<div id="divselectContractUser" style="text-align: right; margin-bottom: 1px; width: 100%;">
		<input id="btnSave" type="button" onclick="save_VehicleName()" value="保存" />
	</div>
	</form>
</body>
</html>
