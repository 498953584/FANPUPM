<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjAccount.aspx.cs" Inherits="PrjAccount" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>资金管理-资金计划</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="../../UserControl/FileUpload/GetFiles.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
	<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />

	<script type="text/javascript">
		$(document).ready(function () {
			setWidthHeight();
			var jwTable = new JustWinTable('gvBudget');
			replaceEmptyTable('emptyContractType', 'gvBudget');
			setButton(jwTable, 'btnDel', 'btnEdit', 'btnQuery', 'hfldPurchaseChecked');
			deleteClick();
			btnActivationClick();
			displayLookAdjuncts();

			jwTable.registClickTrListener(function () {
				var btnSelectPerson = document.getElementById('btnSelectPerson');
				btnSelectPerson.removeAttribute('disabled');
			});
			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				var btnSelectPerson = document.getElementById('btnSelectPerson');
				if (checkedChk.length == "1") {
					btnSelectPerson.removeAttribute('disabled');
				}
				else {
					document.getElementById('btnSelectPerson').setAttribute('disabled', 'disabled');
				}

			});

			jwTable.registClickAllCHKLitener(function () {
				document.getElementById('btnSelectPerson').setAttribute('disabled', 'disabled');
			});

		});
		function deleteClick() {
			var btnDelete = document.getElementById('btnDel');
			btnDelete.onclick = function () {
				if (!window.confirm('系统提示：\n\n确定要删除吗？')) {
					return false;
				}
			}
		}
		function btnActivationClick() {
			$("#btnActivation").click(function () {
				if ($("#HD").val() == "1") {
					if (confirm('系统提示：\n你确实要注销该账户吗，账户注销将无法使用！')) {
						return true;
					} else {
						return false;
					}
				}
			});
		}
		function setWidthHeight() {
			$('#divBudget').height($(this).height() - 25);
			$('#div_project').height($(this).height() - 20);
		}

		function clickRows(temAID, temtr) {
			if (temAID != "") {
				$("#hfAccountID").val(temAID);
				var nextID = $(temtr).children().eq(5).children().attr("ID");
				var firstID = $(temtr).children().eq(4).children().attr("ID");
				if (firstID == "1") {
					if (nextID == "0") {
						$("#btnActivation").removeAttr("disabled");
						$("#btnActivation").val("激活");
						$("#HD").val("0");
					} else if (nextID == "1") {
						$("#btnActivation").removeAttr("disabled");
						$("#btnActivation").val("注销");
						$("#HD").val("1");
					} else if (nextID == "2") {
						$("#btnActivation").attr("disabled", "disabled");
						$("#btnActivation").val("注销");
						$("#HD").val("1");
					} else {
						$("#btnActivation").attr("disabled", "disabled");
					}
				} else {
					$("#btnActivation").val("激活");
					$("#btnActivation").attr("disabled", "disabled");
				}
				$("#btnSelectPerson").removeAttr("disabled");
			}
		}
		//编辑
		function btnEdit_onclick() {
			top.ui._prjaccount = window;
			var titleText = '编辑项目账户';
			var accid = $("#hfAccountID").val();
			var _PrjId = $("#hfldPrjId").val();
			var prjname = $("#hfPrjName").val();
			if (accid != "") {
				var url = "/Fund/Account/PrjAccountEdit.aspx?Action=edit&ic=" + accid + "&PrjCode=" + _PrjId + "&PrjName=" + escape(prjname);
				toolbox_oncommand(url, titleText);
			}
		}
		//查看
		function btnQuery_onclick() {
			var titleText = '查看项目账户';
			var accid = $("#hfAccountID").val();
			var _PrjId = $("#hfldPrjId").val();
			var prjname = $("#hfPrjName").val();
			if (accid != "") {
				url = "/Fund/Account/PrjAccountView.aspx?Action=query&ic=" + accid + "&PrjCode=" + _PrjId + "&PrjName=" + escape(prjname);
				viewopen(url);
			}
		}
		//新增
		function btnAdd_onclick() {
			top.ui._prjaccount = window;
			var titleText = '新增项目账户';
			url = "/Fund/Account/PrjAccountEdit.aspx?Action=add";
			toolbox_oncommand(url, titleText);
		}
		function CheckForm() {

		}
		function registerPrivilegeEvent() {
			var url = '/Common/AllocUser.aspx?type=projectAccount&idJson=' + $('#hfldPurchaseChecked').val();
			top.ui.openWin({ title: '设置权限', url: url, width: 650, height: 500 });
		}

		//查看附件
		function queryAdjunct(id) {
			var path = $('#hfldAdjunctPath').val() + id;
			showFiles(path, 'divOpenAdjunct');
		}
		function showFiles(folder, divId) {
			$('#' + divId + ' table tr:gt(0)').remove();
			$.getJSON('/UserControl/FileUpload/GetFiles.ashx?' + new Date().getTime(), { Path: folder }, function (data) {
				$('#' + divId + ' table').empty();
				var content;
				//table 头
				var $thName = "<td class=tdStyle style=' width:280px; height:20px;border:1px solid rgb(202,222,222);text-align:center;background-color:rgb(238,242,242)'>文件名称</td>";
				var $thLength = "<td  style=' width:140px; height:20px;border:1px solid rgb(202,222,222);text-align:center;background-color:rgb(238,242,242)' >文件大小</td>"
				var $trTitle = "<tr class style='width:420px;border:1px solid rgb(202,204,204)；'>" + $thName + $thLength + "</tr>";
				content = $trTitle;
				$.each(data, function (index, annex) {
					var style;
					var $a = "<a target='_blank' href=" + jw.downloadUrl + "?path=" + folder + '/' + annex.Name + " style='cursor:pointer' class='linkAnnex'>" + decodeURIComponent(annex.Name) + "</a>";
					if (index % 2 == 0) {
						style = 'rowa';
					} else {
						style = 'rowb';
					}
					var $tdName = "<td class=tdStyle style=' width:280px; height:20px;border:1px solid rgb(202,222,222);text-align:center;'>" + $a + "</td>";
					var $tdLength = "<td  style=' width:140px; height:20px;border:1px solid rgb(202,222,222);text-align:center;' >" + annex.Length + "</td>"
					var $tr = "<tr class=" + style + " style='width:420px;border:1px solid rgb(202,222,222)'>" + $tdName + $tdLength + "</tr>";
					content += $tr;
				});
				var table = $("<div style='width:420px;height:200px;padding:0px;margin:0px auto;text-align:center;'><table id=" + divId + " style=' text-align:center; border-collapse:collapse; width:418px; height:auto; padding:0px; margin:1px; border:1px solid rgb(202,222,222);border-bottom:0px;'>" + content + "</table></div>");
				$(table).dialog({ width: 434, height: 300, modal: true, title: '附件' });
			});
		}
		//附件显示 
		function displayLookAdjuncts() {
			var rows = $("#gvBudget").find("tr");
			if (rows.length > 0) {
				for (var i = 1; i < rows.length - 2; i++) {
					var id = rows.eq(i).attr("id");
					var path = $('#hfldAdjunctPath').val() + '/' + id;
					var showCount = getFilesCount(path);
					if (showCount == 0) {
						rows.eq(i).find("td").eq(8).text("");
					}
				}
			}
		}
		function getFilesCount(path) {
			var count = 1;
			$.ajax({
				type: "GET",
				url: "/UserControl/FileUpload/GetFiles.ashx?" + new Date().getTime() + '&Path=' + path,
				async: false,
				dataType: "JSON",
				success: function (data) {
					count = data.length;
				}
			});
			return count;
		}

	</script>
</head>
<body>
	<form id="form1" onsubmit="return CheckForm();" runat="server">
	<asp:HiddenField ID="HD" runat="server" />
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
		<table style="width: 100%; height: 100%;">
			<tr>
				<td style="width: 100%; vertical-align: top; height: 100%;">
					<div id="header">
						<asp:Label ID="lblTitle" Text="项目账户管理" runat="server"></asp:Label>
					</div>
					<table class="queryTable" cellpadding="5px" cellspacing="0px" style="display: none">
						
						<tr>
							<td class="">
								&nbsp; 账户编号&nbsp;&nbsp;
							</td>
							<td>
								<asp:TextBox ID="txtaccountNum" runat="server"></asp:TextBox>
							</td>
							<td class="">
								&nbsp;&nbsp; 账户名称&nbsp;&nbsp;
							</td>
							<td>
								<asp:TextBox ID="txtacountName" runat="server"></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="btnQueryList" Text="查询" OnClick="btnQuery_Click" runat="server" />
							</td>
						</tr>
					</table>
					<table class="tab" style="vertical-align: top;">
						<tr>
							<td class="divFooter" style="text-align: left;">
								<input type="button" value="新增" id="btnAdd" onclick="return btnAdd_onclick()" runat="server" />

								<input id="btnEdit" type="button" disabled="disabled" value="编辑" onclick="return btnEdit_onclick()" />
								<asp:Button ID="btnDel" Text="删除" Enabled="false" OnClick="btnDel_Click" runat="server" />
								<input type="button" id="btnQuery" disabled="disabled" value="查看" onclick="return btnQuery_onclick()" />
								<asp:Button ID="btnActivation" Text="激活" Enabled="false" Visible="false" OnClientClick="btnActivationClick()" OnClick="btnActivation_Click" runat="server" />
								<input id="btnSelectPerson" type="button" value="账户权限" style="width: 75px;" onclick="registerPrivilegeEvent()"
									disabled="disabled" />
								<asp:Button ID="btnQxian" Text="账户权限" OnClientClick="registerPrivilegeEvent()" Visible="false" runat="server" />
							</td>
						</tr>
						<tr>
							<td style="vertical-align: top; height: 100%;">
								<div id="divBudget" style="overflow: hidden;">
									<div id="divDiaries" style="overflow: auto;">
										<asp:GridView ID="gvBudget" CssClass="gvdata" AllowPaging="true" AutoGenerateColumns="false" OnRowDataBound="gvBudget_RowDataBound" OnPageIndexChanging="gvBudget_PageIndexChanging" DataKeyNames="AccountID" runat="server">
<EmptyDataTemplate>
												<table id="emptyContractType" class="gvdata" width="100%" border="0">
													<tr class="header">
														<th scope="col" style="width: 20px;">
															<asp:CheckBox ID="chkSelectAll" runat="server" />
														</th>
														<th scope="col" width="25px">
															序号
														</th>
														<th scope="col">
															账户编号
														</th>
														<th scope="col">
															账户名称
														</th>
														<th scope="col" width="70px">
															启动金额
														</th>
														<th scope="col" width="70px">
															创建人
														</th>
														<th scope="col" width="70px">
															创建日期
														</th>
														<th scope="col">
															项目
														</th>
														<th scope="col" width="25px">
															附件
														</th>
													</tr>
												</table>
											</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
														<asp:CheckBox ID="cbBoxAll" runat="server" />
													</HeaderTemplate>

<ItemTemplate>
														<asp:CheckBox ID="cbBox" runat="server" />
													</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
														<%# Container.DataItemIndex + 1 %>
													</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="账户编号">
<ItemTemplate>
														<span class="al" onclick="viewopen('PrjAccountView.aspx?ic=<%# Eval("AccountID") %>')">
															<%# Eval("accountNum") %>
														</span>
													</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="账户名称">
<ItemTemplate>
														<%# Eval("acountName") %>
													</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="启动金额">
<HeaderTemplate>
														<asp:Label ID="Label1" Text="启动金额" title="账户初始启动资金,需要走流程审核的请【从收入记账】录入" runat="server"></asp:Label>
													</HeaderTemplate>

<ItemTemplate>
														<%# Eval("initialFund") %>
													</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="创建人">
<ItemTemplate>
														<%# Eval("createMan") %>
													</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="创建日期" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "creatDate", "{0:yyyy-MM-dd}") %>
													</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目"><ItemTemplate>
														<asp:Label ID="lblPrjName" Text='<%# Convert.ToString(this.AL.getPrjName(Convert.ToString(Eval("PrjGuid")))) %>' runat="server"></asp:Label>
													</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="25px">
<ItemTemplate>
														<span class="link" onclick="queryAdjunct('<%# Eval("AccountID") %>')">
															<img src='/images1/icon_att0b3dfa.gif' style='cursor: pointer; border-style: none' />
														</span>
													</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
									</div>
								</div>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	<div id="divOpenAdjunct" title="查看附件" style="display: none;">
		<table id="annexTable" class="gvdata" style="width: 100%;" runat="server"><tr class="header" runat="server"><td style="width: 60%" runat="server">
						名称
					</td><td style="width: 30%" runat="server">
						大小
					</td><td runat="server">
					</td></tr></table>
	</div>
	<div id="divFram" title="" style="display: none">
		<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	<asp:HiddenField ID="hfAccountID" runat="server" />
	<asp:HiddenField ID="hfplantype" runat="server" />
	<asp:HiddenField ID="hfPrjName" runat="server" />
	<asp:HiddenField ID="hfldMonthPlanID" runat="server" />
	<asp:HiddenField ID="hfldAdjunctPath" runat="server" />
	</form>
</body>
</html>
