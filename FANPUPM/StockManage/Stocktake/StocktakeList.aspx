<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StocktakeList.aspx.cs" Inherits="StockManage_Stocktake_StocktakeList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>盘点结存</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/jquery.easyui.extension.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../script/JustWinTable.js"></script>
	<script type="text/javascript" src="../script/Config2.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/jwJson.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 设置只读
			$('#txtEndDate').attr('readonly', 'readonly');
			addEvent(document.getElementById('btnStocktake'), 'click', Stocktake);
			addEvent(document.getElementById('btnUpdate'), 'click', updateStorage);
			addEvent(document.getElementById('btnQuery'), 'click', queryStorage);

			var storageTable = new JustWinTable('gvwStocktake');
			setButton(storageTable, 'btnDelete', 'btnUpdate', 'btnQuery', 'hfldStocktake');
			var state = document.getElementById('hfldState').value
			if (state == '-1' || state == '') {
				document.getElementById('btnStocktake').removeAttribute('disabled', 'disabled');
			} else {
				document.getElementById('btnStocktake').setAttribute('disabled', 'disabled');
			}

			setWfButtonState2(storageTable, 'WF_1');
		});

		// 盘点
		function Stocktake() {
			if (document.getElementById('hfSelectValue').value != '') {
				if (document.getElementById('hfSelectValue').value != '0') {
					var IsFirst = document.getElementById('hfldIsFirst').value;
					var IsAdd = document.getElementById('hfldIsAdd').value;
					if (IsAdd == 'True') {
						if (document.getElementById('hfldLastStocktakeDate').value == 'true') {
							alert('这个月的盘点已经结束，不能重复盘点.');
						} else {
							$('#divEndDate').show().window('open');
						}
					} else {
						parent.desktop.StocktakeEdit = window;
						var url = '/StockManage/Stocktake/StocktakeEdit.aspx?Action=Update&TCode=' + document.getElementById('hfSelectValue').value + '&TName=' + encodeURI(document.getElementById('hfSelectName').value);
						toolbox_oncommand(url, "编辑盘点结存");
					}
				} else {
					alert('此仓库不能进行盘点！');
				}
			} else {
				alert('请选择仓库！');
			}
		}

		function ToStocktakeEdit() {
			///判断选择的日期是否超过今天或者今天以后的日期
			var strDateStart = document.getElementById('txtEndDate').value;
			var strDateEnd = document.getElementById('hfldToday').value;
			var strSeparator = "-"; //日期分隔符
			var strDateArrayStart;
			var strDateArrayEnd;
			var intDay;
			strDateArrayStart = strDateStart.split(strSeparator);
			strDateArrayEnd = strDateEnd.split(strSeparator);
			var strDateS = new Date(strDateArrayStart[0] + "/" + strDateArrayStart[1] + "/" + strDateArrayStart[2]);
			var strDateE = new Date(strDateArrayEnd[0] + "/" + strDateArrayEnd[1] + "/" + strDateArrayEnd[2]);
			intDay = (strDateE - strDateS) / (1000 * 3600 * 24);

			if (document.getElementById('txtEndDate').value == "") {
				alert('请选择盘点截止日期.');
			} else if (intDay <= 0) {
				alert('截止日期不能选择今天或者今天以后的日期.');
			} else {
				$('#divEndDate').window('close');
				parent.desktop.StocktakeEdit = window;
				var url = '/StockManage/Stocktake/StocktakeEdit.aspx?Action=Add&IsFirst=' + document.getElementById('hfldIsFirst').value + '&endDate=' + document.getElementById('txtEndDate').value + '&TCode=' + document.getElementById('hfSelectValue').value + '&TName=' + encodeURI(document.getElementById('hfSelectName').value);
				toolbox_oncommand(url, "增加盘点结存");
			}
		}

		function updateStorage() {
			parent.desktop.StorageEdit = window;
			var url = '/StockManage/Storage/StorageEdit.aspx?Action=Update&StorageCode=' + encodeURI(document.getElementById('hfldStorageCode').value);
			toolbox_oncommand(url, "编辑入库单");
		}

		function LockStocktake() {
			if (!confirm("确定要锁定吗？")) {
				return false;
			}
		}

		function queryStorage() {
			viewopen('StocktakeView.aspx?stocktakeId=' + document.getElementById('hfldStocktake').value + '&ic=' + document.getElementById('hfldStocktake').value, 900, 500);
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table class="tab">
		<tr style="height: 100%">
			<td style="width: 220px; vertical-align: top; height: 100%">
				<div class="pagediv" style="width: 220px;">
					<asp:TreeView ID="tvTreasury" ShowLines="true" OnSelectedNodeChanged="TreeView_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
				</div>
			</td>
			<td style="vertical-align: top; border-left: solid 2px #CADEED; height: 100%;">
				<table border="0" cellpadding="0" cellspacing="0" width="100%">
					<tr>
						<td style="height: 96%;">
							<table class="tab" style="vertical-align: top;">
								<tr style="vertical-align: top;">
									<td class="divFooter" style="text-align: left;">
										<input type="button" id="btnUpdate" value="编辑" style="display: none;" />
										<asp:Button ID="btnDelete" Style="display: none;" Text="删除" disabled="disabled" runat="server" />
										<input type="button" id="btnStocktake" value="盘点" />
										<asp:Button ID="btnLock" Text="锁定" Visible="false" disabled="disabled" OnClientClick="return confirm('您确定要锁定吗?')" OnClick="btnLock_Click" runat="server" />
										<asp:Button ID="btnOverrule" Text="驳回" Visible="false" disabled="disabled" OnClientClick="return confirm('您确定要驳回吗?')" OnClick="btnOverrule_Click" runat="server" />
										<input type="button" id="btnQuery" disabled="disabled" value="查看" />&nbsp;
										<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="115" BusiClass="001" runat="server" />
									</td>
								</tr>
								<tr style="height: 100%; vertical-align: top">
									<td>
										<div class="">
											<asp:GridView ID="gvwStocktake" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" AllowPaging="true" OnRowDataBound="gvwStocktake_RowDataBound" OnPageIndexChanging="gvwStorage_PageIndexChanging" DataKeyNames="Id" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
															<%# Container.DataItemIndex + 1 %>
														</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="盘点编号"><ItemTemplate>
															<span class="al" onclick="viewopen('StocktakeView.aspx?stocktakeId=<%# Eval("Id") %>&ic=<%# Eval("Id") %>',900,500);">
																<%# Eval("Code") %>
															</span>
														</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="盘点名称"><ItemTemplate>
															<%# Eval("Name") %>
														</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="仓库名称"><ItemTemplate>
															<%# Eval("TreasuryName") %>
														</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="盘点人"><ItemTemplate>
															<%# Eval("InputUser") %>
														</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="盘点所属年月"><ItemTemplate>
															<asp:Label ID="lblStocktakeDate" Text='<%# Convert.ToString(Eval("StocktakeDate")) %>' runat="server"></asp:Label>
														</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="盘点时间"><ItemTemplate>
															<%# Convert.ToDateTime(Eval("InputDate")).ToString("yyyy-MM-dd HH:mm:ss") %>
														</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
															<%# Common2.GetState(Eval("FlowState").ToString()) %>
														</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
															<%# StringUtility.GetStr(Eval("Note").ToString()) %>
														</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
										</div>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<div id="divFram" title="" style="display: none">
		<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<div id="divEndDate" class="divContent2 easyui-window" title="设置时间" style="display: none;
		width: 300px; height: 400px
		text-align: center;" data-options="modal:true,closed:true,iconCls:'icon-save'">
		<table style="width: 100%; margin: auto;" cellpadding="3px" cellspacing="0px">
			<tr>
				<td class="descTd">
					盘点截止时间
				</td>
				<td>
					<asp:TextBox ID="txtEndDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td colspan="2">
					<font color="red">(盘点截止时间选择之后不能修改！)</font>
				</td>
			</tr>
			<tr>
				<td colspan="2" align="right">
					<input type="button" id="btnOk" value="确定" onclick="ToStocktakeEdit();" />
				</td>
			</tr>
	</div>
	<asp:HiddenField ID="hfldStocktake" runat="server" />
	<asp:HiddenField ID="hfSelectValue" runat="server" />
	<asp:HiddenField ID="hfSelectName" runat="server" />
	<asp:HiddenField ID="hfldIsFirst" runat="server" />
	<asp:HiddenField ID="hfldIsAdd" runat="server" />
	<asp:HiddenField ID="hfldToday" runat="server" />
	<!-- 保存正在盘点信息的流程状态-->
	<asp:HiddenField ID="hfldState" runat="server" />
	
	<asp:HiddenField ID="hfldLastStocktakeDate" Value="false" runat="server" />
	</form>
</body>
</html>
