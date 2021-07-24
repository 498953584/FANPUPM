<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddIncometBalance.aspx.cs" Inherits="ContractManage_IncometBalance_AddIncometBalance" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>收入合同结算</title><link type="text/css" rel="Stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$('#hfldBtnMeasure').hide();
			$('#hfldBtnConBalanceItem').hide();
			$('#BtnDel').attr('disabled', 'disabled');
			if (getRequestParam('t') == '1') {
				setAllInputDisabled();
			}
			var rptTable = new JustWinTable('gvContractRpt');
			replaceEmptyTable('emptyBudget', 'gvContractRpt');
			//hfldBtnDelMeasureIds
			rptTable.registClickTrListener(function () {
				$('#hfldBtnDelMeasureIds').val($(this).attr('id'));
				$('#BtnDel').removeAttr('disabled');
				if (getRequestParam('t') == '1') {
					$('#BtnDel').attr('disabled', 'disabled');
				}
			});
			rptTable.registClickSingleCHKListener(function () {
				var checkCHK = rptTable.getCheckedChk();
				$('#hfldBtnDelMeasureIds').val(rptTable.getCheckedChkIdJson(checkCHK));
				if (checkCHK.length > 0) {
					$('#BtnDel').removeAttr('disabled');
					if (getRequestParam('t') == '1') {
						$('#BtnDel').attr('disabled', 'disabled');
					}
				} else {
					$('#BtnDel').attr('disabled', 'disabled');
				}
			});
			rptTable.registClickAllCHKLitener(function () {
				var checkCHK = rptTable.getAllChk();
				$('#hfldBtnDelMeasureIds').val(rptTable.getCheckedChkIdJson(checkCHK));
				if ($(this).attr('checked')) {
					$('#BtnDel').removeAttr('disabled');
					if (getRequestParam('t') == '1') {
						$('#BtnDel').attr('disabled', 'disabled');
					}
				} else {
					$('#BtnDel').attr('disabled', 'disabled');
				}
			});
			var consTaskTable = new JustWinTable('gvConsTask');
			replaceEmptyTable('emptyConsTask', 'gvConsTask');
			Val.validate('form1', 'btnSave');

			// 管理扣项
			var itemTable = new JustWinTable('gvIncometItem');
			replaceEmptyTable('EmptyItem', 'gvIncometItem');
			itemTable.registClickTrListener(function () {
				$('#hfldInItemIds').val($(this).attr('id'));
				$('#btnDelItem').removeAttr('disabled');
				$('#btnEdit').removeAttr('disabled');
			});
			itemTable.registClickSingleCHKListener(function () {
				var checkCHK = itemTable.getCheckedChk();
				if (checkCHK.length > 0) {
					$('#btnDelItem').removeAttr('disabled');
					$('#hfldInItemIds').val(itemTable.getCheckedChkIdJson(checkCHK));
				} else {
					$('#btnDelItem').attr('disabled', 'disabled');
				}
				if (checkCHK.length == '1') {
					$('#btnEdit').removeAttr('disabled');
				} else {
					$('#btnEdit').add('disabled', 'disabled');
				}
			});
			itemTable.registClickAllCHKLitener(function () {
				var checkCHK = itemTable.getAllChk();
				$('#btnEdit').attr('disabled', 'disabled');
				if ($(this).attr('checked')) {
					$('#btnDelItem').removeAttr('disabled');
					$('#hfldInItemIds').val(itemTable.getCheckedChkIdJson(checkCHK));
				} else {
					$('#btnDelItem').attr('disabled', 'disabled');
				}
			});
			$('#gvIncometItem tr').live('click', function () {
				if (getRequestParam('t') == '1') {
					$('#btnEdit').attr('disabled', 'disabled');
					$('#btnDelItem').attr('disabled', 'disabled');
				}
			});
			// 计算完成百分比
			calcCompletePercent();
			// 完成量添加Keyup事件
			$('#gvConsTask input[id$=txtCompleteAmount]').keyup(completeAmountKeyupEvent);
			// 完成百分比添加Keyup事件
			$('#gvConsTask input[id$=txtCompletePercent]').keyup(completePercentKeyupEvent);
		});

		function calBalanceThisItemTotal() {
			var $tr = $(this).parent().parent().parent();
			var total;
			var amount = $tr.find('input[id$=txtQty]').val().replace(/\,/g, '');   // 数量
			var price = $tr.find('input[id$=txtUnitPrice]').val().replace(/\,/g, ''); // $tr.find('input[id$=txtUnitPrice]').val().replace(/\,/g, '');   // 单价
			if (!amount || !price) {
				total = 0.0;
			}
			else if (isNaN(amount) && isNaN(price)) {
				total = 0.0;
			}
			else {
				total = amount * price;
			}                      // 小计
			$tr.find('input[id$=txtTotal]').val(total);
		}

		// 计算完成百分比
		function calcCompletePercent() {
			$('#gvConsTask tr[id]').each(function () {
				var amount = $(this).find('input[id$=txtCompleteAmount]').val().replace(/\,/g, '');   // 完成量
				var total = $(this).find('.total').text().replace(/\,/g, '');                         // 小计
				var percent = getCompletePercent(amount, total);                                    // 完成百分比
				if (isNaN(percent)) {
					percent = 0;
				}
				$(this).find('input[id$=txtCompletePercent]').val(percent * 100);
			});
		}

		// 计算完成百分比
		function getCompletePercent(amount, total) {
			if (!amount || !total) { return 0.0; }
			if (isNaN(amount) && isNaN(total)) { return 0.0; }
			if (parseInt(total) == 0) { return 0.0; }

			return amount / total;
		}

		// 完成量的Keyup事件
		function completeAmountKeyupEvent() {
			var $tr = $(this).parent().parent();
			var amount = $(this).val().replace(/\,/g, '');
			var total = $tr.find('.total').text().replace(/\,/g, '');
			var percent = getCompletePercent(amount, total);
			if (isNaN(percent)) {
				percent = 0;
			}
			var percentFormat = _formatDecimal((percent * 100) + '');
			$tr.find('input[id$=txtCompletePercent]').val(percentFormat);
		}

		// 完成百分比的Keyup事件
		function completePercentKeyupEvent() {
			var $tr = $(this).parent().parent();
			var total = $tr.find('.total').text().replace(/\,/g, '');
			var percent = $(this).val().replace(/\,/g, '');
			var amount = (percent * total) / 100;
			if (isNaN(amount)) {
				amount = 0;
			}
			$tr.find('input[id$=txtCompleteAmount]').val(_formatDecimal(amount));
		}

		// 添加合同计量
		function addContractConsReport() {
			top.ui._ConInMeasureList = window;
			var url = '/ContractManage/IncometBalance/ConInMeasureList.aspx?contractId=' + $('#hfldContractId').val() + '&BalanceId=' + $('#hdGuid').val();
			top.ui.openWin({ title: '添加合同计量', url: url });
			top.ui.callback = function (obj) {
				if (obj.id != null) {
					$('#hfldMeasureIds').val(obj.id);
					$('#hfldBtnMeasure').click();
				}
			}
		}

		function AddItem() {
			top.ui._AddBalanceItem = window;
			var title = '新增管理扣项';
			var url = '/ContractManage/IncometBalance/AddBalanceItem.aspx?action=Add&balanceId=' + $('#hdGuid').val();
			top.ui.callback = function () {
				$('#hfldBtnConBalanceItem').click();
			}
			top.ui.openWin({ title: title, url: url, width: 650, height: 450 });
		}

		function EditItem() {
			top.ui._AddBalanceItem = window;
			var title = '新增管理扣项';
			var url = '/ContractManage/IncometBalance/AddBalanceItem.aspx?action=Edit&balanceId=' + $('#hdGuid').val();
			top.ui.callback = function () {
				$('#hfldBtnConBalanceItem').click();
			}
			top.ui.openWin({ title: title, url: url, width: 650, height: 450 });
		}
	</script>
	<style type="text/css">
		#FileLink1_But_UpFile
		{
			width: auto;
		}
		#FileLink1_Btn_Upload
		{
			width: auto;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divHeader2">
		<table class="tableHeader">
			<tr>
				<td>
					<asp:Label ID="lblTitle" Font-Bold="true" Text="" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<div class="divContent2">
		<table class="tableContent2" cellpadding="5px" cellspacing="0">
			<tr>
				<td colspan="4" class="groupInfo">
					合同基本信息
				</td>
			</tr>
			<tr>
				<td class="word">
					合同编号
				</td>
				<td class="txt readonly">
					<asp:TextBox ID="txtContractCode" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					合同名称
				</td>
				<td class="txt readonly">
					<asp:TextBox ID="txtContractName" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					合同金额
				</td>
				<td class="txt readonly">
					<asp:TextBox ID="txtContractPrice" CssClass="easyui-numberbox" data-options="min:-99999999999,max:99999999999,precision:3,groupSeparator:','" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					签订时间
				</td>
				<td class="txt readonly">
					<asp:TextBox ID="txtSignedTime" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td colspan="4" class="groupInfo">
					结算单信息
				</td>
			</tr>
			<tr>
				<td class="word">
					结算累计
				</td>
				<td class="txt readonly">
					<asp:TextBox ID="txtSumClearing" CssClass="easyui-numberbox" data-options="min:-99999999999,max:99999999999,precision:3,groupSeparator:','" ReadOnly="true" Width="100%" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					差额
				</td>
				<td class="txt readonly">
					<asp:TextBox ID="txtDiffAmount" CssClass="easyui-numberbox" data-options="min:-99999999999,max:99999999999,precision:3,groupSeparator:','" ReadOnly="true" Width="100%" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					结算编号
				</td>
				<td class="txt mustInput">
					<asp:TextBox Height="15px" Width="100%" CssClass="{required:true, messages:{required:'结算编号必须输入'}}" ID="txtClearingNumber" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					结算金额
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtClearingPrice" Height="15px" Width="100%" CssClass="easyui-numberbox  {required:true,number:true, messages:{required:'结算金额必须输入',number:'结算金额格式错误'}}" data-options="min:-99999999999,max:99999999999,precision:3,groupSeparator:','" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					结算方式
				</td>
				<td class="txt mustInput">
					<asp:DropDownList Width="100%" Height="19px" CssClass="{required:true, messages:{required:'请选择结算方式'}}" ID="ddlBalanceMode" DataValueField="NoteID" DataTextField="CodeName" runat="server"></asp:DropDownList>
				</td>
				<td class="word">
					付款方式
				</td>
				<td class="txt mustInput">
					<asp:DropDownList Width="100%" Height="19px" CssClass="{required:true, messages:{required:'请选择付款方式'}}" ID="ddlPayMode" DataValueField="NoteID" DataTextField="CodeName" runat="server"></asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td class="word">
					结算对象
				</td>
				<td class="txt">
					<asp:TextBox ID="txtBalanceObj" Height="15px" Width="100%" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					结算日期
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtClearingTime" onClick="WdatePicker()" CssClass="{required:true, messages:{required:'结算日期必须输入'}}" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					结算人
				</td>
				<td colspan="3" class="txt mustInput">
					<asp:TextBox ID="txtClearingUser" Height="15px" Width="100%" CssClass="{required:true, messages:{required:'结算人必须输入'}}" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td align="right">
					备注
				</td>
				<td colspan="3">
					<asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" id="td1">
					附件
				</td>
				<td colspan="3" style="text-align: left; padding-right: 0px;">
					<MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
				</td>
			</tr>
			<tr>
				<td class="word">
					录入人
				</td>
				<td class="txt readonly">
					<asp:TextBox ID="txtInputPerson" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					录入时间
				</td>
				<td class="txt readonly">
					<asp:TextBox Width="100%" ReadOnly="true" ID="txtInputDate" runat="server"></asp:TextBox>
				</td>
			</tr>
		</table>
		<table class="tableContent2" cellpadding="5px" cellspacing="0">
			<tr>
				<td class="groupInfo">
					管理扣费
				</td>
			</tr>
			<tr>
				<td>
					<table width="100%" cellpadding="1px" cellspacing="0">
						<tr style="vertical-align: top;">
							<td style="text-align: left;">
								<input id="btnAddItem" type="button" value="新增" onclick="AddItem();" runat="server" />

								<input id="btnEdit" type="button" value="编辑" disabled="true" OnServerClick="btnEdit_click" runat="server" />

								<asp:Button Text="删除" ID="btnDelItem" OnClientClick="return confirm('您确定要删除吗？')" disabled="disabled" OnClick="btnDelItem_Click" runat="server" />
							</td>
						</tr>
						<tr>
							<td>
								<asp:GridView ID="gvIncometItem" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvgvIncometItem_RowDataBound" DataKeyNames="Id,Type,Qty,UnitPrice" runat="server">
<EmptyDataTemplate>
										<table id="EmptyItem" class="tab" width="100%">
											<tr class="header">
												<td style="width: 20px">
													<asp:CheckBox ID="chkAll" runat="server" />
												</td>
												<td style="width: 25px;">
													序号
												</td>
												<td align="center">
													扣费项目名称
												</td>
												<td align="center">
													扣费数量
												</td>
												<td align="center">
													单价
												</td>
												<td align="center" style="width: 50px;">
													小计
												</td>
												<td>
													扣费类型
												</td>
												<td align="center">
													备注
												</td>
												<td align="center">
													附件
												</td>
											</tr>
										</table>
									</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
												<asp:CheckBox ID="cbAllBox" runat="server" />
											</HeaderTemplate><ItemTemplate>
												<asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("Id")) %>' runat="server" />
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="扣费项目名称"><ItemTemplate>
												<%# Eval("Name") %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量">
<ItemTemplate>
												<%# Eval("Qty") %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单价">
<ItemTemplate>
												<%# Eval("UnitPrice") %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="小计" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="扣费类型">
<ItemTemplate>
												<%# getTypeName(Eval("Type").ToString()) %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
												<span class="tooltip" title="<%# Eval("Note") %>">
													<%# StringUtility.GetStr(Eval("Note").ToString(), 25) %>
												</span>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
												<%# StringUtility.FilesBind(Eval("Id").ToString(), "ContractBudBalance") %>
											</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		<div id='divTarget'>
			<table class="tableContent2" cellpadding="5px" cellspacing="0">
				<tr>
					<td>
						<input type="button" value="添加合同计量" style="width: 90px;" onclick="addContractConsReport();" />
						<asp:Button ID="BtnDel" Width="90px" Text="删除合同计量" OnClick="delContractConsReport" runat="server" />
					</td>
				</tr>
				<tr>
					<td>
						<asp:GridView ID="gvContractRpt" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvContractRpt_RowDataBound" DataKeyNames="RptId,PrjId,FlowState,BalanceId,ContractId" runat="server">
<EmptyDataTemplate>
								<table id="emptyBudget" class="tab">
									<tr class="header">
										<th scope="col" style="width: 20px;">
											<input id="chk1" type="checkbox" />
										</th>
										<th scope="col" style="width: 25px;">
											序号
										</th>
										<th scope="col">
											录入人
										</th>
										<th scope="col">
											上报时间
										</th>
										<th scope="col">
											安全工作记录
										</th>
									</tr>
								</table>
							</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
										<asp:CheckBox ID="cbAllBox" runat="server" />
									</HeaderTemplate><ItemTemplate>
										<asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Container.DataItemIndex + 1) %>' runat="server" />
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="录入人">
<ItemTemplate>
										<%# Eval("InputUser") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上报时间">
<ItemTemplate>
										<%# Common2.GetTime(Eval("InputDate")) %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="安全工作记录">
<ItemTemplate>
										<%# Eval("Note") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="添加方式">
<ItemTemplate>
										<%# (Eval("Type").ToString() == "0") ? "手动录入" : "变更录入" %>
									</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					</td>
				</tr>
				<tr>
					<td>
						<div style="padding: 2px; width: 75px; margin-top: 5px;">
							合同计量汇总</div>
						<asp:GridView ID="gvConsTask" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwConsTask_RowDataBound" DataKeyNames="TaskId" runat="server">
<EmptyDataTemplate>
								<table id="emptyConsTask" class="tab">
									<tr class="header">
										<th scope="col" style="width: 25px;">
											序号
										</th>
										<th scope="col">
											任务编码
										</th>
										<th scope="col">
											分项工作
										</th>
										<th scope="col">
											单位
										</th>
										<th scope="col">
											小计
										</th>
										<th scope="col">
											申报量
										</th>
										<th scope="col">
											完成量 / 总工作量
										</th>
										<th scope="col">
											核准量
										</th>
									</tr>
								</table>
							</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderText="序号"><ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="任务编码" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
										<%# Eval("TaskCode") %>
										<asp:HiddenField ID="hfldTaskId" Value='<%# Convert.ToString(Eval("TaskId")) %>' runat="server" />
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="分项工作 " ItemStyle-HorizontalAlign="Left"><ItemTemplate>
										<%# Eval("TaskName") %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
										<%# Eval("Unit") %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="小计" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
										<span class="decimal_input total">
											<%# Eval("Total") %></span>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申报量" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
										<asp:TextBox ID="txtCompleteAmount" Style="text-align: right;" CssClass="easyui-numberbox" data-options="min:-99999999999,max:99999999999,precision:3,groupSeparator:','" Text='<%# Convert.ToString(Eval("Amount")) %>' runat="server"></asp:TextBox>
										<asp:HiddenField ID="hfldCompleteAmount" Value='<%# Convert.ToString(Eval("Amount")) %>' runat="server" />
									</ItemTemplate></asp:TemplateField><asp:TemplateField ItemStyle-HorizontalAlign="Left"><HeaderTemplate>
										<span title="完成量 / 总工作量">完成百分比</span>
									</HeaderTemplate><ItemTemplate>
										<asp:TextBox ID="txtCompletePercent" Style="text-align: right; width: 90%;" CssClass="decimal_input" runat="server"></asp:TextBox>%
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="核准量" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
										<asp:TextBox ID="txtApproveAmount" Style="text-align: right;" CssClass="easyui-numberbox" data-options="min:-99999999999,max:99999999999,precision:3,groupSeparator:','" Text='<%# Convert.ToString(Eval("ApproveAmount")) %>' runat="server"></asp:TextBox>
										<asp:HiddenField ID="hfldApproveAmount" Value='<%# Convert.ToString(Eval("ApproveAmount")) %>' runat="server" />
									</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					</td>
				</tr>
			</table>
		</div>
	</div>
	<div class="divFooter2">
		<table class="tableFooter2">
			<tr>
				<td>
					<asp:Button ID="btnSave" Text="保存" OnClientClick="return valForm()" OnClick="btnSave_Click" runat="server" />
					<input type="button" id="btnCancel" value="取消" onclick="winclose('AddIncometBalance','ShowBalanceList.aspx',false)" />
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hdGuid" runat="server" />
	
	<asp:HiddenField ID="hfldPrjid" runat="server" />
	<asp:HiddenField ID="hdClearingNumber" runat="server" />
	
	<asp:HiddenField ID="hfldContractId" runat="server" />
	
	<asp:HiddenField ID="hfldMeasureIds" runat="server" />
	
	<asp:Button ID="hfldBtnMeasure" OnClick="hfldBtnMeasure_Click" runat="server" />
	
	<asp:HiddenField ID="hfldBtnDelMeasureIds" runat="server" />
	
	<asp:HiddenField ID="hfldRptIDs" runat="server" />
	
	<asp:HiddenField ID="hfldPrjKindJson" runat="server" />
	
	<asp:HiddenField ID="hfldInItemIds" runat="server" />
	<asp:Button ID="hfldBtnConBalanceItem" OnClick="hfldBtnConBalanceItem_Click" runat="server" />
	
	<asp:HiddenField ID="hfldconBalanceItem" runat="server" />
	</form>
</body>
</html>
