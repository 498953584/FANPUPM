<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowContractList.aspx.cs" Inherits="ContractManage_IncometContract_ShowContractList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>收入合同列表</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/StockManage/script/tab.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$("#btn_Search")[0].onclick = function () {
				if (!$('#form1').form('validate')) {
					return false;
				}
			}
			showTooltip('tooltip', 25);
			setSp('hfldPurchaseChecked');
			setbkImg(getRequestParam("spId"), 'hfldPurchaseChecked');
			formateTable('gvConract', 3);
			var jwTable = new JustWinTable('gvConract');
			jwTable.registClickTrListener(function () {
				document.getElementById("hfldPurchaseChecked").value = this.id;
				setbkImg(getRequestParam("spId"), 'hfldPurchaseChecked');
			});
			//trFrame 为存放Frame的TR
			//必需将整个Table的高度设置为100%，且第二个TR的高度为1px
			var trWidth = document.getElementById('trFrame').offsetHeight;
			document.getElementById('framContract').style.height = (trWidth + 10) + 'px';

			//默认选中第一行  根据胡经理的意思修改
			$('#gvConract tr[id]:eq(0)').trigger('click');
		});

		function Change(SCheckBox) {
			var objs = document.getElementsByTagName('input');
			for (var i = 0; i < objs.length; i++) {
				if (objs[i].type.toLowerCase() == 'checkbox') {
					objs[i].checked = false;
				}
			}
			var SelectCheckBoxId = SCheckBox.id;
			document.getElementById(SelectCheckBoxId).checked = true;
			document.getElementById("hfldPurchaseChecked").value = SCheckBox.parentNode.parentNode.id;
			setbkImg(getRequestParam("spId"), 'hfldPurchaseChecked');
		}

		//选择项目
		function openProjPicker() {
			jw.selectOneProject({nameinput: 'txtProject' });
		}

		function rowQueryA(cid) {
			parent.desktop.AddIncometContract = window;
			var url = "/ContractManage/IncometContract/AddIncometContract.aspx?t=1&id=" + cid;
			toolbox_oncommand(url, "查看收入合同");
		}
		function viewopen_n(url) {
			viewopen(url);
		}

		//选择人员
		function selectUser() {
			jw.selectOneUser({ codeinput: 'hfldSignPeople', nameinput: 'txtSignPeople' });
		}

		//往来单位
		function pickCorp() {
			jw.selectOneCorp({ idinput: 'hdParty', nameinput: 'txtParty' });
		}

		// 选择合同类型
		function selectConType() {
			jw.selectOneConType({ nameinput: 'txtConType' });
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 100%;">
		<tr style="height: 20px; display: none;">
			<td class="divHeader">
				<asp:Label ID="lblTitle" runat="server"></asp:Label>
			</td>
		</tr>
		<tr style="height: 80px;">
			<td style="vertical-align: top;">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							合同编号
						</td>
						<td>
							<asp:TextBox ID="txtContractCode" Width="120px" CssClass="easyui-validatebox" data-options="required:false, validType:'validChars[50]'" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							合同名称
						</td>
						<td>
							<asp:TextBox ID="txtContractName" Width="120px" CssClass="easyui-validatebox" data-options="required:false, validType:'validChars[50]'" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							合同类型
						</td>
						<td>
						
								 <span class="spanSelect" style="width: 122px">
								<asp:TextBox ID="txtConType" Style="width: 97px; height: 15px; border: none;
									line-height: 16px; margin: 1px 0px 1px 2px;" CssClass="easyui-validatebox" data-options="required:false, validType:'validChars[50]'" runat="server"></asp:TextBox>
								<img alt="选择合同类型" onclick="selectConType();" src="../../images/icon.bmp" style="float: right;" />
							</span>
						</td>
						<td class="descTd">
							项目
						</td>
						<td colspan="2" align="left">
						
                                <span class="spanSelect" style="width: 122px">
								<asp:TextBox ID="txtProject" Style="width: 97px; height: 15px; border: none;
									line-height: 16px; margin: 1px 0px 1px 2px;" CssClass="easyui-validatebox" data-options="required:false, validType:'validChars[50]'" runat="server"></asp:TextBox>
								<img alt="选择项目" onclick="openProjPicker();" src="../../images/icon.bmp" style="float: right;" />
							</span>
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
							<asp:TextBox ID="txtEndContractPrice" CssClass="decimal_input " Width="120px" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							签约时间
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
							
						<span class="spanSelect" style="width: 122px">
								<asp:TextBox ID="txtSignPeople" Style="width: 97px; height: 15px; border: none;
									line-height: 16px; margin: 1px 0px 1px 2px;" CssClass="easyui-validatebox" data-options="required:false, validType:'validChars[50]'" runat="server"></asp:TextBox>
								<img alt="签订人" onclick="selectUser();" src="../../images/icon.bmp" style="float: right;" />
							</span>


						</td>
						<td class="descTd">
							甲方
						</td>
						<td>
							
								<span class="spanSelect" style="width: 122px">
								<asp:TextBox ID="txtParty" Style="width: 97px; height: 15px; border: none;
									line-height: 16px; margin: 1px 0px 1px 2px;" CssClass="easyui-validatebox" data-options="required:false, validType:'validChars[50]'" runat="server"></asp:TextBox>
								<img alt="签订人" onclick="pickCorp();" src="../../images/icon.bmp" style="float: right;" />
							</span>
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
			<td style="width: 100%; height: 175px; vertical-align: top;">
				<div>
					<asp:GridView ID="gvConract" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="ContractId,isFContract" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
									<asp:CheckBox ID="cbAllBox" runat="server" />
								</HeaderTemplate><ItemTemplate>
									<asp:CheckBox ID="cbBox" runat="server" />
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
									<%# StringUtility.GetStr(Eval("Num").ToString()) %>
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
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签约时间"><ItemTemplate>
									<%# Common2.GetTime(Eval("SignedTime").ToString()) %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目"><ItemTemplate>
									<%# StringUtility.GetStr(Eval("prjName").ToString()) %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
									<%# GetAnnx(System.Convert.ToString(Eval("ContractId"))) %>
								</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
					</webdiyer:AspNetPager>
				</div>
			</td>
		</tr>
		<tr id="trFrame">
			<td style="border: solid 0px red; vertical-align: top;">
				<div>
					<span id="spModify" class="xxk" style="margin-left: 0px;" runat="server">合同变更</span>
					<span id="spBalance" class="xxk" runat="server">合同结算 </span><span id="spPayment" class="xxk" runat="server">合同收款 </span><span id="spPaymentApply" class="xxk" runat="server">
							资金支付 </span><span id="spInvoice" class="xxk" runat="server">合同发票 </span>
				</div>
				<iframe id="framContract" frameborder="0" src="/ContractManage/IncometModify/ViewModifyList.aspx" width="100%" runat="server"></iframe>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hdReturnVal" runat="server" />
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	<asp:HiddenField ID="hldfIsExamineApprove" runat="server" />
	</form>
</body>
</html>
