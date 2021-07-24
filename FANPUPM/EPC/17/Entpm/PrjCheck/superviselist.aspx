<%@ Page Language="C#" AutoEventWireup="true" CodeFile="superviselist.aspx.cs" Inherits="SuperviseList" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>SuperviseList</title>
	<script type="text/javascript" src="../../../../Script/jquery.js"></script>
	<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
	<script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
	<script type="text/ecmascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script language="javascript" type="text/javascript">
		$(document).ready(function () {
			var purchasePlanTable = new JustWinTable('DataGrid1');
		});

		var pk = null;
		function setvalue(obj, CanEdit) {
			pk = obj;
			document.getElementById("TextBox_pk").value = obj;
			if (CanEdit == 1) {
				if (document.getElementById("Button_edit") != null)
					document.getElementById("Button_edit").disabled = true;
				if (document.getElementById("Button_del") != null)
					document.getElementById("Button_del").disabled = true;

				if (document.getElementById("Button_sp") != null)
					document.getElementById("Button_sp").disabled = true;
				if (document.getElementById("Button_view") != null)
					document.getElementById("Button_view").disabled = false;

			}
			else {
				if (document.getElementById("Button_edit") != null)
					document.getElementById("Button_edit").disabled = false;
				if (document.getElementById("Button_del") != null)
					document.getElementById("Button_del").disabled = false;
				if (document.getElementById("Button_sp") != null)
					document.getElementById("Button_sp").disabled = false;
				if (document.getElementById("Button_view") != null)
					document.getElementById("Button_view").disabled = false;
			}
		}
		function ShowInsertWindow(pc) {
			var url = "/EPC/17/Entpm/PrjCheck/SuperviseManage.aspx?prjcode=" + pc;
			top.ui._superviselist = window;
			toolbox_oncommand(url, "新增项目监察"); //引用js
		}
		function ShowInfo() {
			var prjId = jw.getRequestParam('PrjCode');
			top.ui._superviselist = window;
			var url = "/EPC/17/Entpm/PrjCheck/SuperviseManage.aspx?Type=View&prjcode=" + prjId + "&pk=" + pk + "";
			toolbox_oncommand(url, "查看项目监察"); //引用js	
		}
		function ShowEditWindow() {
			if (pk == null) {
				window.alert('请选择记录！');
			}
			else {
				top.ui._superviselist = window;
				var url = "/EPC/17/Entpm/PrjCheck/SuperviseManage.aspx?pk=" + pk + "";
				toolbox_oncommand(url, "编辑项目监察"); //引用js
			}
		}
		function ShowSpWindow() {
			if (pk == null) {
				window.alert('请选择记录！');
			}
			else {
				var url = "/EPC/17/Entpm/PrjCheck/SuperviseSp.aspx?pk=" + pk + "";
				top.ui._superviselist = window;
				toolbox_oncommand(url, "项目监察审核"); //引用js
			}
		}
		function ShowReportWindow(pk) {
			window.open("/ModuleSet/common/common/print.asp?Floder=PrjCheck&Report=ItemSupervise&sql=exec prj_sp_ItemSupervise " + pk, "newwindow", "toolbar=no, menubar=no,location=no,resizable=yes,  status=no");
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
<body scroll="no">
	<form id="Form1" method="post" runat="server">
	<table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
		<tr>
			<td class="divHeader" colspan="2">
				<asp:Literal ID="Literal1" runat="server"></asp:Literal>
			</td>
		</tr>
		<tr>
			<td valign="top" style="height: 25px">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							<asp:Literal ID="Literal2" Text="立项单位:" runat="server"></asp:Literal>
							<asp:TextBox ID="TextBox_lxdw" Width="120px" runat="server"></asp:TextBox>
							<asp:Button ID="btn_Search" Text="查询" OnClick="Button_query_Click" runat="server" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td class="divFooter" style="text-align: left" valign="top">
				<asp:TextBox ID="TextBox_pk" Width="0px" BorderColor="White" runat="server"></asp:TextBox>
				<asp:Button ID="Button_add" Text="新 增" OnClick="Button_add_Click" runat="server" />
				<asp:Button ID="Button_edit" Text="编  辑" Enabled="false" runat="server" />
				<asp:Button ID="Button_del" Text="删  除" Enabled="false" OnClick="Button_del_Click" runat="server" />
				<asp:Button ID="Button_sp" Text="审  批" Enabled="false" runat="server" />
				<input id="Button_view" type="button" value="查  看" onclick="ShowInfo();" disabled="disabled">
			</td>
		</tr>
		<tr>
			<td valign="top" colspan="2">
				<div style="width: 100%; height: 100%">
					<asp:DataGrid ID="DataGrid1" Width="100%" CssClass="gvdata" AutoGenerateColumns="false" AllowCustomPaging="true" AllowPaging="true" PageSize="20" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server"><SelectedItemStyle HorizontalAlign="Center"></SelectedItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><ItemStyle CssClass="rowa"></ItemStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><Columns><asp:BoundColumn Visible="false" DataField="ID" ReadOnly="true" HeaderText="编号"></asp:BoundColumn><asp:BoundColumn DataField="StandNapeUnit" HeaderText="立项单位"></asp:BoundColumn><asp:BoundColumn DataField="StandNapeDate" HeaderText="立项时间" DataFormatString="{0:d}"></asp:BoundColumn><asp:HyperLinkColumn DataTextField="StandNapeName" HeaderText="立项名称"></asp:HyperLinkColumn><asp:BoundColumn DataField="StandItemRecord" HeaderText="立项案由"></asp:BoundColumn><asp:BoundColumn DataField="KnotItemDate" HeaderText="结项时间" DataFormatString="{0:d}"></asp:BoundColumn><asp:BoundColumn DataField="SuperviseEffciency" HeaderText="监察效能"></asp:BoundColumn><asp:BoundColumn DataField="ApprovePerson" HeaderText="审核人"></asp:BoundColumn><asp:BoundColumn HeaderText="审核结果"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="ApproveResult" ReadOnly="true" HeaderText="审核结果"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid></div>
			</td>
		</tr>
	</table>
	<JWControl:JavaScriptControl ID="JavaScriptControl1" runat="server"></JWControl:JavaScriptControl>
	<asp:HiddenField ID="hfldPrjName" runat="server" />
	</form>
</body>
</html>
