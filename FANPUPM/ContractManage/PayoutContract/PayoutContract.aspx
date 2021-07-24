<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayoutContract.aspx.cs" Inherits="ContractManage_PayoutContract_PayoutContract" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>支出合同</title>
	<style type="text/css">
		#spanContractType
		{
			width: 120px;
		}
	</style>
	<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			replaceEmptyTable('emptyContractType', 'gvwContract');
			var contractTable = new JustWinTable('gvwContract');
			setButton(contractTable, 'btnDelete', 'btnUpdate', 'btnQuery', 'hfldContract')
			setWfButtonState2(contractTable, 'WF_1');
			showTooltip('tooltip', 25);

			//绑定参数设置事件
			$('#btnConfig').bind('click', function () {
				parent.desktop.Config = window;
				var url = "/ContractManage/Config.aspx?contype=PayoutContract&type=Update&id=" + $('#hfldContract').val();
				toolbox_oncommand(url, "编辑参数设置");
			});

			contractTable.registClickTrListener(function () {
				var btnAddProtocol = document.getElementById('btnAddProtocol');
				btnAddProtocol.setAttribute('target', this.id);
				//参数设置按钮可用
				document.getElementById('btnConfig').removeAttribute('disabled');
				//Modify Zhang FuJun 2011-8-19
				if (this.childNodes.length <= 10) return;
				//End                
				//Modify Bery 2010-10-14 16:31:27
				//审核通过后也可以修改权限          
				if ($(this).children().eq(10).text().indexOf('已审核') > -1) {
					btnAddProtocol.removeAttribute('disabled');
				}
				else {
					//没有审核通过的不允许添加补充协议
					btnAddProtocol.setAttribute('disabled', 'disabled');
				}

				// 如果是补充协议则不允许再添加补充协议
				if (this.isMainContract == 'False') {
					btnAddProtocol.setAttribute('disabled', 'disabled');
				}
			});

			contractTable.registClickSingleCHKListener(function () {
				var btnAddProtocol = document.getElementById('btnAddProtocol');
				btnAddProtocol.setAttribute('target', this.parentNode.parentNode.id);
				var checkedChk = contractTable.getCheckedChk();
				if (checkedChk.length == 0) {
					btnAddProtocol.setAttribute('disabled', 'disabled');
					//参数设置按钮不可用
					document.getElementById('btnConfig').setAttribute('disabled', 'disabled');
				}
				else {
					setTableState(checkedChk);
				}
			});

			contractTable.registClickAllCHKLitener(function () {
				var btnAddProtocol = document.getElementById('btnAddProtocol');

				document.getElementById('btnConfig').setAttribute('disabled', 'disabled');
				if (!contractTable.isCheckedAll()) {
					btnAddProtocol.setAttribute('disabled', 'disabled');
				}
				else {
					var checkedChk = contractTable.getAllChk();
					setTableState(checkedChk);
				}
			});

			registerAddProtocolEvent();
			registerDeleteContractEvent();
			registerUpdateContractEvent();
			registerPrivilegeEvent();

			formateTable('gvwContract', 3);
		});

		function setTableState(checkedChk) {
			//流程状态的列索引
			var flowIndex = 9;
			var btnAddProtocol = document.getElementById('btnAddProtocol');
			var protocolEnabled = true;
			var privilegeEnabled = true;
			if (checkedChk.length > 1) {
				protocolEnabled = false;
				privilegeEnabled = false;
			}
			for (var i = 0; i < checkedChk.length; i++) {
				var tr = checkedChk[i].parentNode.parentNode;

				if (tr.isMainContract == 'False') {
					//补充协议
					protocolEnabled = false;
				}
			}
			if (protocolEnabled) {
				btnAddProtocol.removeAttribute('disabled');
				document.getElementById('btnConfig').removeAttribute('disabled');
			}
			else {
				btnAddProtocol.setAttribute('disabled', 'disabled');
				document.getElementById('btnConfig').setAttribute('disabled', 'disabled');
			}
		}

		function addContract() {
			parent.desktop.PayoutContractEdit = window;
			var url = "/ContractManage/PayoutContract/PayoutContractEdit.aspx?Action=Add&Random=" + new Date().getTime();
			toolbox_oncommand(url, "新增支出合同");
		}

		function registerAddProtocolEvent() {
			var btnAddProtocol = document.getElementById('btnAddProtocol');
			addEvent(btnAddProtocol, 'click', function () {
				parent.desktop.PayoutContractEdit = window;
				var url = "/ContractManage/PayoutContract/PayoutContractEdit.aspx?Action=AddProtocol&ContractId=" + $(this).attr('target') + '&Random=' + new Date().getTime();
				toolbox_oncommand(url, "新增补充协议");
			});
		}

		function registerDeleteContractEvent() {
			var btnDelete = document.getElementById('btnDelete');
			btnDelete.onclick = function () {
				if (!window.confirm('系统提示：\n\n确定要删除吗？')) {
					return false;
				}
			}
		}
		function registerUpdateContractEvent() {
			var btnUpdate = document.getElementById('btnUpdate');
			var hfldContract = document.getElementById('hfldContract');
			addEvent(btnUpdate, 'click', function () {
				parent.desktop.PayoutContractEdit = window;
				var url = "/ContractManage/PayoutContract/PayoutContractEdit.aspx?Action=Update&ContractId=" + hfldContract.value + '&Random=' + new Date().getTime();
				toolbox_oncommand(url, "编辑支出合同");
			});
		}

		function rowQueryA(path) {
			parent.desktop.PayoutContractEdit = window;
			toolbox_oncommand(path, "查看收入合同");
		}

		function registerPrivilegeEvent() {
			addEvent(document.getElementById('btnPrivilege'), 'click', function () {
				var url = "/Common/DivSetRole.aspx?tbName=Con_Payout_Contract&idName=ContractID&field=UserCodes&id=" + document.getElementById("hfldContract").value;
				document.getElementById("divFramRole").title = "设置权限";
				document.getElementById("iframeRole").src = url;
				selectnEvent('divFramRole');
			});
		}

		// 选择乙方
		function pickCorp() {
			jw.selectOneCorp({ idinput: 'hfldBName', nameinput: 'txtBName' });
		}

		// 选择项目
		function openProjPicker() {
			jw.selectOneProject({nameinput: 'txtProject' });
		}

		// 选择签订人
		function selectSignPerson() {
			jw.selectOneUser({ nameinput: 'txtSignPersonName' });
		}

		function Query() {
			var url = '/ContractManage/PayoutContract/ParyoutContractQuery.aspx?bc=081&bcl=001&ic=' + document.getElementById("hfldContract").value;
			viewopen(url, 1000, 600);
		}

		function viewopen_n(url) {
			viewopen(url, 1000, 600);
		}

		// 选择合同类型
		function selectConType() {
			jw.selectOneConType({ nameinput: 'txtConType' });
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		<tr>
			<td style="vertical-align: top;">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							签约时间
						</td>
						<td>
							<asp:TextBox ID="txtStartTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							至
						</td>
						<td>
							<asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							起始金额
						</td>
						<td>
							<asp:TextBox ID="txtStartMoney" CssClass="decimal_input" Width="120px" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							结束金额
						</td>
						<td>
							<asp:TextBox ID="txtEndMoney" CssClass="decimal_input" Width="120px" runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							合同编号
						</td>
						<td>
							<asp:TextBox ID="txtContractCode" Width="120px" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							合同名称
						</td>
						<td>
							<asp:TextBox ID="txtContractName" Width="120px" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							合同类型
						</td>
						<td>
							<asp:TextBox ID="txtConType" Width="120px" CssClass="select_input" imgclick="selectConType" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							项目
						</td>
						<td>
							<asp:TextBox ID="txtProject" Width="120px" CssClass="select_input" imgclick="openProjPicker" runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							签订人
						</td>
						<td>
							<asp:TextBox ID="txtSignPersonName" Width="120px" CssClass="select_input" imgclick="selectSignPerson" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							乙方:
						</td>
						<td>
							<asp:TextBox ID="txtBName" CssClass="select_input {required:true, messages:{required:'乙方必须输入'}}" Width="120px" imgclick="pickCorp" runat="server"></asp:TextBox>
							<asp:HiddenField ID="hfldBName" runat="server" />
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
							<input type="button" id="btnAdd" onclick="addContract();" value="新增" />
							<input type="button" id="btnAddProtocol" style="width: 100px" value="添加补充协议" disabled="disabled" />
							<input type="button" id="btnUpdate" value="编辑" disabled="disabled" />
							<asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
							<input type="button" id="btnQuery" disabled="disabled" value="查看" onclick="Query();" />
							<input type="button" id="btnConfig" disabled="disabled" value="参数设置" style="width: 100px" />
							<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="081" BusiClass="001" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							<div class="">
								<asp:GridView ID="gvwContract" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwContract_RowDataBound" DataKeyNames="ContractID,IsMainContract,PrjGuid,ContractCode" runat="server">
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
													合同编号
												</th>
												<th scope="col">
													合同名称
												</th>
												<th scope="col">
													最终金额
												</th>
												<th scope="col">
													合同类型
												</th>
												<th scope="col">
													虚拟合同
												</th>
												<th scope="col">
													乙方
												</th>
												<th scope="col">
													结算方式
												</th>
												<th scope="col">
													付款方式
												</th>
												<th scope="col">
													流程状态
												</th>
												<th scope="col">
													合同状态
												</th>
												<th scope="col">
													签约时间
												</th>
												<th scope="col">
													签订人
												</th>
												<th scope="col">
													项目
												</th>
												<th scope="col">
													附件
												</th>
											</tr>
										</table>
									</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
												<asp:CheckBox ID="chkAll" runat="server" />
											</HeaderTemplate><ItemTemplate>
												<asp:CheckBox ID="chk" runat="server" />
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
												<%# StringUtility.GetStr(Eval("Num").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:BoundField DataField="ContractCode" HeaderText="合同编号" /><asp:TemplateField HeaderText="合同名称"><ItemTemplate>
												<span class="link tooltip" title='<%# Eval("ContractName").ToString() %>' onclick="viewopen_n ('/ContractManage/PayoutContract/ParyoutContractQuery.aspx?bc=081&bcl=001&ic=<%# Eval("ContractID") %>', '支出合同查看')">
													<%# StringUtility.GetStr(Eval("ContractName").ToString(), 25) %>
												</span>
											</ItemTemplate></asp:TemplateField><asp:BoundField DataField="ModifiedMoney" ItemStyle-HorizontalAlign="Right" HeaderText="最终金额" ItemStyle-CssClass="decimal_input" /><asp:TemplateField HeaderText="合同类型"><ItemTemplate>
												<%# StringUtility.GetStr(Eval("TypeName").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="虚拟合同" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
												<asp:Label ID="labfictitious" Text='<%# System.Convert.ToString((Eval("fictitious").ToString() == "1") ? "否" : "是", System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="乙方"><ItemTemplate>
												<%# StringUtility.GetStr(Eval("CorpName").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结算方式"><ItemTemplate>
												<%# StringUtility.GetStr(Eval("BalanceModeName").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="付款方式"><ItemTemplate>
												<%# StringUtility.GetStr(Eval("PayModeName").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
												<%# Common2.GetState(Eval("FlowState").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同状态" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
												<%# WebUtil.GetConState(Eval("conState").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签约时间"><ItemTemplate>
												<%# Common2.GetTime(Eval("SignDate").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="签订人" DataField="SignPersonName" /><asp:TemplateField HeaderText="项目"><ItemTemplate>
												<%# StringUtility.GetStr(Eval("PrjName").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
												<%# GetAnnx(System.Convert.ToString(Eval("ContractID"))) %>
											</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
								<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
								</webdiyer:AspNetPager>
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
