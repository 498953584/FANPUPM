<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayoutContractMain.aspx.cs" Inherits="ContractManage_PayoutContract_PayoutContractMain" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>支出合同列表</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />
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
			$("#btnSearch")[0].onclick = function () {
				if (!$('#form1').form('validate')) {
				    return false;
                }
			}
			setSp('hfldContract');
			setbkImg(getRequestParam("spId"), 'hfldContract');
			replaceEmptyTable('emptyContractType', 'gvwContract');
			var contractTable = new JustWinTable('gvwContract');
			contractTable.registClickTrListener(function () {
				if (this.id != "") {
					document.getElementById("hfldContract").value = this.id;
					setbkImg(getRequestParam("spId"), 'hfldContract');
				}
			});
			showTooltip('tooltip', 20);

			// 显示合同与补充协议直接的折线
			formateTable('gvwContract', 2);

			// trFrame 为存放Frame的TR
			// 必需将整个Table的高度设置为100%，且第二个TR的高度为1px
			var trWidth = document.getElementById('trFrame').offsetHeight;
			//            document.getElementById('framContract').style.height = (trWidth - 34) + 'px';

			//默认选中第一行  根据胡经理的意思修改
			$('#gvwContract tr[id]:eq(0)').trigger('click');
		});

		function Change(SCheckBox) {
			var objs = document.getElementsByTagName('input');
			for (var i = 0; i < objs.length; i++) {
				if (objs[i].type.toLowerCase() == 'checkbox') {
					if (objs[i].id == SCheckBox.id)
						objs[i].checked = true;
					else
						objs[i].checked = false;
				}
			}
			document.getElementById("hfldContract").value = SCheckBox.parentNode.parentNode.id;
			setbkImg(getRequestParam("spId"), 'hfldContract');
		}

		function pickCorp() {
			jw.selectOneCorp({ idinput: 'hfldBName', nameinput: 'txtBName' });
		}
		// 选择签订人
		function selectSignPerson() {
			jw.selectOneUser({ nameinput: 'txtSignPersonName' });
		}

		// 选择项目
		function openProjPicker() {
			jw.selectOneProject({nameinput: 'txtProject' });
		}

		function rowQueryA(cid) {
			parent.desktop.AddIncometContract = window;
			var url = "/ContractManage/IncometContract/AddIncometContract.aspx?t=1&id=" + cid;
			toolbox_oncommand(url, "查看支出合同");
		}

		function viewopen_n(url) {
			viewopen(url);
		}

		// 选择合同类型
		function selectConType() {
			jw.selectOneConType({ nameinput: 'txtConType' });
		}
	</script>
</head>
<body scroll="auto">
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;
		height: 97%; vertical-align: top;">
		<tr style="height: 1px;">
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
							<asp:TextBox ID="txtStartMoney" Width="120px" CssClass="decimal_input" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							结束金额
						</td>
						<td>
							<asp:TextBox ID="txtEndMoney" Width="120px" CssClass="decimal_input" runat="server"></asp:TextBox>
						</td>
					</tr>
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
								<img alt="选择类型" onclick="selectConType();" src="../../images/icon.bmp" style="float: right;" />
							</span>
						</td>
						<td class="descTd">
							项目
						</td>
						<td>
							<asp:TextBox ID="txtProject" CssClass="select_input" Width="120px" imgclick="openProjPicker" runat="server"></asp:TextBox>
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
							<span class="spanSelect" style="width: 122px">
								<asp:TextBox ID="txtBName" Style="width: 97px; height: 15px; border: none;
									line-height: 16px; margin: 1px 0px 1px 2px;" CssClass="easyui-validatebox" data-options="required:false, validType:'validChars[50]'" runat="server"></asp:TextBox>
								<img alt="选择类型" onclick="pickCorp();" src="../../images/icon.bmp" style="float: right;" />
							</span>
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
			<td style="height: 175px; width: 100%;">
				<table class="tab">
					<tr>
						<td style="height: 100%; vertical-align: top;">
							<div>
								<asp:GridView ID="gvwContract" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwContract_RowDataBound" DataKeyNames="ContractID,IsMainContract" runat="server">
<EmptyDataTemplate>
										<table id="emptyContractType" class="gvdata">
											<tr class="header">
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
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
												<%# StringUtility.GetStr(Eval("Num").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:BoundField DataField="ContractCode" HeaderText="合同编号" /><asp:TemplateField HeaderText="合同名称"><ItemTemplate>
												<span class="link tooltip" title='<%# Eval("ContractName").ToString() %>' onclick="viewopen_n ('/ContractManage/PayoutContract/ParyoutContractQuery.aspx?bc=081&bcl=001&ic=<%# Eval("ContractID") %>', '支出合同查看')">
													<%# StringUtility.GetStr(Eval("ContractName").ToString(), 20) %>
												</span>
											</ItemTemplate></asp:TemplateField><asp:BoundField DataField="ModifiedMoney" ItemStyle-HorizontalAlign="Right" HeaderText="最终金额" ItemStyle-CssClass="decimal_input" /><asp:TemplateField HeaderText="合同类型"><ItemTemplate>
												<%# StringUtility.GetStr(Eval("TypeName").ToString()) %>
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
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
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
		<tr id="trFrame">
			<td style="height: 45%; width: 100%; vertical-align: top; padding-top: 10px;">
				<div id="div">
					<span id="spPayoutModify" style="margin-left: 0px;" class="xxk" runat="server">合同变更</span>
					<span id="spPayoutBalance" class="xxk" runat="server">合同结算 </span><span id="spPayoutPayment" class="xxk" runat="server">合同付款 </span><span id="spPayoutInvoice" class="xxk" runat="server">合同发票</span> <span id="spPayoutCalc" class="xxk" runat="server">合同计量</span>
				</div>
				<iframe id="framContract" frameborder="0" style="width: 100%; height: 100%;" src="/ContractManage/PayoutModify/PayoutModify.aspx?ContractID=''" runat="server"></iframe>
			</td>
		</tr>
		<tr>
		</tr>
	</table>
	<div id="divFram" title="" style="display: none">
		<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<asp:HiddenField ID="hfldContract" runat="server" />
	<asp:HiddenField ID="hdframsrc" runat="server" />
	<asp:HiddenField ID="hdSpId" runat="server" />
	</form>
</body>
</html>
