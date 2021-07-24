<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Permission.aspx.cs" Inherits="ContractManage_IncometContract_Permission" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>收入合同权限分配</title><link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var jwTable = new JustWinTable('gvConract');
			setBtn(jwTable, 'btnAleave');
			formateTable('gvConract', 3);
			showTooltip('tooltip', 25);
		});

		// 权限
		function rowAleave() {
			var url = "/Common/DivSetRole.aspx?tbName=Con_Incomet_Contract&idName=ContractID&field=UserCodes&id=" +
                $("#hfldPurchaseChecked").val();
			top.ui.openWin({ title: '设置权限', url: url, width: 650, height: 500 });
			//            document.getElementById("divFramRole").title = "设置权限";
			//            document.getElementById("iframeRole").src = url;
			//            selectnEvent('divFramRole', "650", "500");
		}

		// 选择项目
		function openProjPicker() {
			jw.selectOneProject({ idinput: 'hdnProjectCode', nameinput: 'txtProject' });
		}

		// 设置按钮权限
		function setBtn(jwTable, btnAleave) {
			if (jwTable.table.firstChild.childNodes.length == 1) {
				//table中没有数据
				return;
			}
			jwTable.registClickTrListener(function () {
				if (this.id) {
					document.getElementById(btnAleave).removeAttribute('disabled');
					$('#hfldPurchaseChecked').val(this.id);
				}
			});
			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				if (checkedChk.length == 0) {
					document.getElementById(btnAleave).setAttribute('disabled', 'disabled');
				}
				else
					document.getElementById(btnAleave).removeAttribute('disabled');
				$('#hfldPurchaseChecked').val(jwTable.getCheckedChkIdJson(checkedChk));
			});

			jwTable.registClickAllCHKLitener(function () {
				document.getElementById(btnAleave).removeAttribute('disabled');
				if (this.checked) {
					var checkChk = jwTable.getAllChk();
					if (checkChk.length == 1) {
						var value = jwTable.getCheckedChkIdJson(checkChk);
						$('#hfldPurchaseChecked').val(value);
					}
					else {
						var value = jwTable.getCheckedChkIdJson(checkChk);
						if (value.toString().indexOf('[') == 0) {
							$('#hfldPurchaseChecked').val(value);
						}
					}
				}
			});
		}

		// 合同明细
		function viewopen_n(url) {
			viewopen(url);
		}

		// 选择人员
		function selectUser() {
			jw.selectOneUser({ codeinput: 'hfldSignPeople', nameinput: 'txtSignPeople' });
		}

		// 往来单位
		function pickCorp() {
			jw.selectOneCorp({ idinput: 'hdParty', nameinput: 'txtParty' });
		}

		// 选择合同类型
		function selectConType() {
			jw.selectOneConType({ nameinput: 'txtConType', idinput: 'hfldConType' });
		}
	</script>
	<style type="text/css">
		#spanContractType
		{
			width: 122px;
			margin-left: 3px;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		
		<tr>
			<td style="vertical-align: top;">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
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
							项 目
						</td>
						<td colspan="2" align="left" style="border: solid 0px red;">
							<asp:TextBox ID="txtProject" CssClass="select_input txtreadonly" Style="width: 120px;" imgclick="openProjPicker" runat="server"></asp:TextBox>
							<asp:HiddenField ID="hdnProjectCode" runat="server" />
						</td>
					</tr>
					<tr>
						<td class="descTd">
							起始金额
						</td>
						<td>
							<asp:TextBox ID="txtStartContractPrice" CssClass="decimal_input" Width="120px" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							结束金额
						</td>
						<td>
							<asp:TextBox ID="txtEndContractPrice" CssClass="decimal_input" Width="120px" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							签约日期
						</td>
						<td>
							<asp:TextBox ID="txtStartSignedTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							至
						</td>
						<td>
							<asp:TextBox ID="txtEndSignedTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							签订人
						</td>
						<td>
							<input type="text" style="width: 120px;" id="txtSignPeople" class="select_input" imgclick="selectUser" runat="server" />

							<input id="hfldSignPeople" type="hidden" style="width: 1px" runat="server" />

						</td>
						<td class="descTd">
							甲 方
						</td>
						<td>
							<input type="text" style="width: 120px;" id="txtParty" class="select_input" imgclick="pickCorp" runat="server" />

							<asp:HiddenField ID="hdParty" runat="server" />
						</td>
						<td style="border: solid 0px red;" colspan="2">
							<asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
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
							<input type="button" id="btnAleave" disabled="disabled" value="权限" onclick="rowAleave()" />
						</td>
					</tr>
					<tr>
						<td>
							<div class="">
								<asp:GridView ID="gvConract" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvPurchaseplan_PageIndexChanging" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="ContractId,isFContract,FlowState,Project" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
												<asp:CheckBox ID="cbAllBox" runat="server" />
											</HeaderTemplate><ItemTemplate>
												<asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Eval("ContractId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
												<%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同编号"><ItemTemplate>
												<%# Eval("ContractCode") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同名称"><ItemTemplate>
												<span class="link tooltip" title='<%# Eval("ContractName").ToString() %>' onclick="viewopen_n ('/ContractManage/IncometContract/IncometContractQuery.aspx?ic=<%# Eval("ContractId") %>', '收入合同查看')">
													<%# StringUtility.GetStr(Eval("ContractName").ToString(), 25) %>
												</span>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同金额" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
												<%# WebUtil.GetEnPrice(Eval("ContractPrice").ToString(), Eval("ContractId").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同类型"><ItemTemplate>
												<%# StringUtility.GetStr(Eval("TypeName").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签订类型" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
												<asp:Label ID="labsign" Text='<%# System.Convert.ToString((Eval("sign").ToString() == "1") ? "已签订" : "未签订", System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="甲方"><ItemTemplate>
												<%# StringUtility.GetStr(base.GetParty(Eval("Party").ToString())) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结算方式"><ItemTemplate>
												<%# StringUtility.GetStr(Eval("BMode").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="付款方式"><ItemTemplate>
												<%# StringUtility.GetStr(Eval("PMode").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同状态" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
												<%# WebUtil.GetConState(Eval("conState").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签订人"><ItemTemplate>
												<%# Eval("SignPeopleName") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签约日期"><ItemTemplate>
												<%# Common2.GetTime(Eval("SignedTime").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目"><ItemTemplate>
												<%# StringUtility.GetStr(Eval("prjName").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
												<%# GetAnnx(System.Convert.ToString(Eval("ContractId"))) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
												<%# Common2.GetState(Eval("FlowState").ToString()) %>
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
	<asp:HiddenField ID="hdReturnVal" runat="server" />
	<asp:HiddenField ID="hldfIsExamineApprove" runat="server" />
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	</form>
</body>
</html>
