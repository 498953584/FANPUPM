<%@ Page Language="C#" AutoEventWireup="true" CodeFile="itemproglist.aspx.cs" Inherits="ItemProgList" %>
<%@ Import Namespace="cn.justwin.BLL"%>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>ItemProgList</title>
	<script src="../../../../Script/jquery.js" type="text/javascript"></script>
	<script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
	<script type="text/ecmascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
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
	<script language="javascript" type="text/javascript">
		var img_src = "EPC/QuaitySafety/";
		$(document).ready(function () {
			var purchasePlanTable = new JustWinTable('DataGrid1');
			setWfButtonState2(purchasePlanTable, 'WF1');
			setButton(purchasePlanTable, 'Button_del', 'Button_edit', 'Button_view', 'hidItemId');
			hideFirstPageNo();
			if (getRequestParam('Type').toLowerCase() != 'Edit'.toLowerCase()) {
				$('input[id^=WF]').css('display', 'none');
				$('#btnStartWF').css('display', 'none'); //提交审核
				$('#CancelBt').css('display', 'none');
			}

			if (getRequestParam('Levels') == '1') {
				$('#ba').find('th').eq(0).hide();
				$('#td_Btn').hide();
				$('.divFooter').hide();
			}
			if (getRequestParam('Type') == 'Edit') {
				$('.divHeader').hide();
			}
			$('#DataGrid1 tr').each(function () {
				var _markTem = $(this).attr('mark');
				if (_markTem != 'undefined' && _markTem != '' && _markTem != null) {
					if (_markTem == '1') {
						$(this).find('td').eq(1).find('img').attr('src', '/images/over.gif');
					} else
						if (_markTem == '2') {
							$(this).find('td').eq(1).find('img').attr('src', '/images/Edit.gif');
						} else
							if (_markTem == '3') {
								$(this).find('td').eq(1).find('img').attr('src', '/images/Process.gif');
							}
				}
			});


			$('#DataGrid1 tr').each(function () {
				if ($(this).attr("class") == "header") {
				} else {
					$(this).live('click', function () {
						$("#Button_view").removeAttr("disabled");  // Button_add, Button_edit,Button_del,Button_view,btnFiles
						//FlowState：-1:未提交，0：审核中，1：已审核，-2：驳回，-3：重报。
						var flowstate = $(this).attr('flowState');
						//mark:  1：已归档，3：作为归档资料，2：未作为归档资料。
						var mark = $(this).attr('mark');
						if (getRequestParam('Type').toLowerCase() == 'Edit'.toLowerCase()) {
							$('#Button_add').removeAttr('disabled');
							if (mark == '3') {
								if (flowstate == '1') {
									$('#btnFiles').removeAttr('disabled');
									$('#Button_del').attr('disabled', 'disabled');
									$('#Button_edit').attr('disabled', 'disabled');
								} else if (flowstate == '-1' || flowstate == '-3') {
									$('#btnFiles').attr('disabled', 'disabled');
									$('#Button_del').removeAttr('disabled');
									$('#Button_edit').removeAttr('disabled');
								} else {
									$('#btnFiles').attr('disabled', 'disabled');
									$('#Button_del').attr('disabled', 'disabled');
									$('#Button_edit').attr('disabled', 'disabled');
								}
							} else {
								$('#btnFiles').attr('disabled', 'disabled');
								if (flowstate == '-1' || flowstate == '-3') {
									$('#Button_edit').removeAttr('disabled');
								}
							}
							if (mark == '1') {
								$('#Button_edit').attr('disabled', 'disabled');
								$('#Button_del').attr('disabled', 'disabled');
							}
						}
						else if (getRequestParam('Type') == 'List') {
							if (mark == '3') {
								$('#btnFiles').removeAttr('disabled');
							} else {
								$('#btnFiles').attr('disabled', 'disabled');
							}
						} else if (getRequestParam('Type') == 'View') {
							if (mark == '3') {
								$('#btnFiles').removeAttr('disabled');
							} else {
								$('#btnFiles').attr('disabled', 'disabled');
							}
						}
						$("#HiddenField_ID").val($(this).attr("id"));
					});
				}
			});

		});

		var pk = null;
		function setvalue(obj, isAction) {
			pk = obj;

			if (document.getElementById('TextBox_pk') != null) {
				document.getElementById('TextBox_pk').value = obj;
			}

			if (isAction == '1') {

				if (document.getElementById('Button_edit') != null) {
					document.getElementById('Button_edit').disabled = true;
				}
				if (document.getElementById('Button_del') != null) {
					document.getElementById('Button_del').disabled = true;
				}
				if (document.getElementById('Button_sp') != null) {
					document.getElementById('Button_sp').disabled = true;
				}
				if (document.getElementById('btnCanCel') != null) {
					document.getElementById('btnCanCel').disabled = true;
				}
				if (document.getElementById('btnAction') != null) {
					document.getElementById('btnAction').disabled = false;
				}
				if (document.getElementById('Button_add') != null) {
					document.getElementById('Button_add').disabled = true;
				}

			}
			else {
				if (document.getElementById('Button_edit') != null) {
					document.getElementById('Button_edit').disabled = false;
				}

				if (document.getElementById('Button_del') != null) {
					document.getElementById('Button_del').disabled = false;
				}

				if (document.getElementById('Button_sp') != null) {
					document.getElementById('Button_sp').disabled = false;
				}

				if (document.getElementById('btnCanCel') != null) {
					document.getElementById('btnCanCel').disabled = false;
				}

				if (document.getElementById('btnAction') != null) {
					document.getElementById('btnAction').disabled = true;
				}

				if (document.getElementById('Button_add') != null) {
					document.getElementById('Button_add').disabled = false;
				}
			}
			if (document.getElementById("Button_view") != null)
				document.getElementById("Button_view").disabled = false;
		}
		function ShowInsertWindow() {
			if (getRequestParam('PrjCode') == null) {
				window.alert('请选择项目！')
			} else {
				var url = "/EPC/17/Ppm/Prog/ItemProgManage.aspx?prjcode=" + getRequestParam('PrjCode') + "&Levels=" + getRequestParam('Levels');
				top.ui._ItemProgManage = window;
				toolbox_oncommand(url, "新增项目奖罚"); //引用js
			}
		}
		function clickRow(sc) {
		}
		function ShowEditWindow() {
			if (pk == null) {
				window.alert('请选择记录！');
			}
			else {
				var url = "/EPC/17/Ppm/Prog/ItemProgManage.aspx?pk=" + pk + "";
				top.ui._ItemProgManage = window;
				toolbox_oncommand(url, "编辑项目奖罚"); //引用js
			}
		}
		function ShowInfo() {
			if (pk == null) {
				window.alert('请选择记录！');
			}
			else {
				var url = "/EPC/17/Ppm/Prog/ItemProgManage.aspx?Type=View&pk=" + pk + "";
				toolbox_oncommand(url, "查看项目奖罚"); //引用js
			}
		}
		function ShowSpWindow() {
			if (pk == null) {
				window.alert('请选择记录！');
			}
			else {
				var url = "/EPC/17/Ppm/Prog/ItemProgSp.aspx?pk=" + pk + "";
				top.ui._ItemProgSp = window;
				toolbox_oncommand(url, "审核项目奖罚"); //引用js
			}
		}
		function ActionRecord() {
			if (pk == null) {
				window.alert('请选择记录！');
				return false;
			}
			else {
				document.getElementById("hidItemId").value = pk;
				return true;
			}
		}
	</script>
</head>
<body>
	<form id="Form1" method="post" runat="server">
	<table cellspacing="0" cellpadding="0" width="100%" border="0">
		<tr>
			<td class="divHeader">
				<asp:Literal ID="Literal4" runat="server"></asp:Literal>
			</td>
		</tr>
		<tr id="TRS" runat="server"><td style="height: 24px" runat="server">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							<asp:Literal ID="Literal1" Text="奖罚类别:" runat="server"></asp:Literal>
							<asp:DropDownList ID="DropDownList_lb" runat="server"></asp:DropDownList>
							<asp:Literal ID="Literal2" Text="奖罚单位:" runat="server"></asp:Literal>
							<asp:TextBox ID="TextBox_cfdw" Width="105px" runat="server"></asp:TextBox>
							<asp:Literal ID="Literal3" Text="被奖罚对象:" runat="server"></asp:Literal>
							<asp:TextBox ID="TextBox_bcfdx" Width="92px" runat="server"></asp:TextBox>
							<asp:Button ID="btn_Search" Text="查询" OnClick="Button_query_Click" runat="server" />
						</td>
					</tr>
				</table>
			</td></tr>
		<tr>
			<td class="divFooter" style="text-align: left;">
				<asp:Button ID="Button_add" Text="新 增" runat="server" />
				<asp:Button ID="Button_edit" Text="编  辑" Enabled="false" runat="server" />
				<asp:Button ID="Button_del" Text="删  除" Enabled="false" OnClick="Button_del_Click" runat="server" />
				<asp:Button ID="Button_sp" Text="审 核" Enabled="false" runat="server" />
				<asp:Button ID="btnAction" Text="执  行" Enabled="false" OnClick="btnAction_Click" runat="server" />
				<asp:Button ID="btnCanCel" Text="取消执行" Enabled="false" Width="70px" OnClick="btnCanCel_Click" runat="server" />
				<input id="Button_view" type="button" value="查  看" onclick="ShowInfo();" disabled="disabled">
				<asp:Button ID="btnFiles" Enabled="false" Text="归档" OnClick="btnFiles_Click" runat="server" />
				<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="120" BusiClass="001" runat="server" />
			</td>
		</tr>
		<tr>
			<td valign="top">
				<div style="width: 100%; height: 100%">
					<asp:DataGrid ID="DataGrid1" Width="100%" CssClass="gvdata" AutoGenerateColumns="false" AllowCustomPaging="false" AllowPaging="true" PageSize="20" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server"><SelectedItemStyle HorizontalAlign="Left"></SelectedItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><ItemStyle CssClass="rowa"></ItemStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><Columns><asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="20px"><HeaderTemplate>
									<input id="chkAll" type="checkbox" /></HeaderTemplate><ItemTemplate>
									<asp:HiddenField ID="HiddenField1" Value='<%# Convert.ToString(Eval("ID")) %>' runat="server" />
									<asp:CheckBox ID="chk" runat="server" />
								</ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="20px"><HeaderTemplate>
									
								</HeaderTemplate><ItemTemplate>
									<asp:Image ID="Image1" Width="12px" Height="12px" runat="server" />
								</ItemTemplate></asp:TemplateColumn><asp:BoundColumn Visible="false" DataField="ID" ReadOnly="true" HeaderText="编号"></asp:BoundColumn><asp:TemplateColumn HeaderText="奖罚单位"><ItemTemplate>
									<asp:Label Text='<%# Convert.ToString(Eval("ProgUnit")) %>' runat="server"></asp:Label>
									<input type="hidden" id="hidIsAction" value='<%# Convert.ToString(Eval("IsAction")) %>' runat="server" />

								</ItemTemplate></asp:TemplateColumn><asp:BoundColumn DataField="ProgCause" HeaderText="奖罚原因"></asp:BoundColumn><asp:BoundColumn DataField="ProgMoney" HeaderText="奖罚金额" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn><asp:BoundColumn DataField="ByProgObject" HeaderText="被奖罚对象"></asp:BoundColumn><asp:BoundColumn DataField="ProgGist" HeaderText="奖罚依据"></asp:BoundColumn><asp:BoundColumn DataField="ProgSortName" HeaderText="奖罚类别"></asp:BoundColumn><asp:BoundColumn DataField="Principal" HeaderText="负责人"></asp:BoundColumn><asp:BoundColumn DataField="ProgDate" HeaderText="奖罚日期" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:D}"></asp:BoundColumn><asp:TemplateColumn HeaderText="流程状态"><ItemTemplate>
									<%# Common2.GetState(Eval("FlowState").ToString()) %>
								</ItemTemplate></asp:TemplateColumn><asp:BoundColumn DataField="IsAction" HeaderText="执行状态" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid></div>
				<asp:TextBox ID="TextBox_pk" Width="0px" BorderColor="White" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				<input id="hidItemId" type="hidden" name="Hidden1" runat="server" />

			</td>
		</tr>
	</table>
	<JWControl:JavaScriptControl ID="JavaScriptControl1" runat="server"></JWControl:JavaScriptControl>
	</form>
</body>
</html>
