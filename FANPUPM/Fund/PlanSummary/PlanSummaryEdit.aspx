<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanSummaryEdit.aspx.cs" Inherits="Fund_AccounIncome_AccounIncomeEdit" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="com.jwsoft.pm.entpm"%>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>入账编辑</title>
	<script src="/Web_Client/TreeNew.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/DecimalInput.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			replaceEmptyTable('emptyDetail', 'gvwDetail');
			var accTable = new JustWinTable('gvwDetail');
			registerCancelEvent();
			document.getElementById('btnSave').onclick = btnSaveClick;
			document.getElementById('BtnPlanSummary').onclick = btnSaveClick;
		});

		function ClickRow(Guid, state) {
			document.getElementById("HdnPlanGuid").value = Guid;
			if (state == 0)
				document.getElementById("BtnChangeState").setAttribute("disabled", "disabled");
			if (state == 1)
				document.getElementById("BtnChangeState").removeAttribute("disabled");
		}

		function btnSaveClick() {
			if (!validate()) {
				return false;
			}
		}

		function validate() {
			if (!document.getElementById('txtDate').value) {
				alert('系统提示：\n\n计划月份不能为空！');
				return false;
			}
			return true;
		}

		function registerCancelEvent() {
			var btnCancel = document.getElementById('btnCancel');
			addEvent(btnCancel, 'click', function () {
				winclose('ModifyEdit', 'PlanSummary.aspx', false)
			})
		}

	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divHeader2">
		<table class="tableHeader2">
			<tr>
				<td>
					<asp:Label ID="lblTitle" Font-Bold="true" Text="资金计划上报" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<div id="divContent2" class="divContent2">
		<table id="tableContent" class="tableContent2" cellpadding="5px" cellspacing="0">
			<tr>
				<td class="word">
					计划月份
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtDate" Width="120px" onclick="WdatePicker({dateFmt:'yyyy-MM'})" runat="server"></asp:TextBox>
				</td>
				<td class="word">
				</td>
				<td class="txt">
				</td>
			</tr>
			<tr>
				<td class="word">
					附件
				</td>
				<td colspan="3" style="padding-right: 0px;">
					<MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="附件" runat="server" />
				</td>
			</tr>
			<tr>
				<td class="word">
					备注
				</td>
				<td colspan="3">
					<asp:TextBox ID="txtre" TextMode="MultiLine" Height="50px" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					上报人
				</td>
				<td class="txt">
					<asp:TextBox ID="txtInPeople" ContentEditable="false" Height="15px" Width="100%" runat="server"></asp:TextBox>
					<input type="hidden" id="hdnPeopleCode" runat="server" />

				</td>
				<td class="word">
					上报日期
				</td>
				<td class="txt">
					<asp:TextBox ID="txtInDate" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td colspan="4">
					<table width="100%" cellpadding="5px" cellspacing="0">
						<tr>
							<td>
								<asp:Button ID="BtnPlanSummary" Text="按提交的计划生成" Width="150px" OnClick="BtnPlanSummary_Click" runat="server" />
								<asp:Button ID="BtnChangeState" Text="驳回重报" disabled="disabled" Width="100px" OnClick="BtnChangeState_Click" runat="server" />
							</td>
						</tr>
						<tr style="vertical-align: top">
							<td style="width: 100%;">
								<asp:GridView ID="gvwDetail" AutoGenerateColumns="false" CssClass="gvdata" ShowFooter="true" OnRowDataBound="gvwDetail_RowDataBound" DataKeyNames="MonthPlanID" runat="server">
<EmptyDataTemplate>
										<table id="emptyDetail" class="tab">
											<tr class="header">
												<th scope="col" style="width: 25px;">
													序号
												</th>
												<th scope="col">
													项目
												</th>
												<th scope="col">
													上期计划金额
												</th>
												<th scope="col">
													上期实际发生金额
												</th>
												<th scope="col">
													上期计划执行完成情况
												</th>
												<th scope="col">
													本期计划金额
												</th>
												<th scope="col">
													流程状态
												</th>
												<th scope="col">
													填报人</td>
													<th scope="col">
														填报日期
													</th>
													<th scope="col">
														备注
													</th>
											</tr>
										</table>
									</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目" HeaderStyle-Width="100px">
<ItemTemplate>
												<span class="link" onclick="viewopen('/Fund/MonthPlan/MonthPlanView.aspx?ic=<%# Eval("MonthPlanID") %>')">
													<%# Eval("PrjName") %>
												</span>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上期计划金额" HeaderStyle-Width="70px">
<ItemTemplate>
												<%# Eval("LastPlanMoney").ToString() %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上期实际发生金额" HeaderStyle-Width="70px">
<ItemTemplate>
												<%# Eval("LastActualMoney").ToString() %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上期计划执行完成情况" HeaderStyle-Width="70px">
<HeaderTemplate>
												上期计划执行完成情况<img title="上期计划执行完成情况=上期实际发生金额/上期计划金额" style="cursor: pointer;" src="/images/help.jpg" />
											</HeaderTemplate>

<ItemTemplate>
												<%# Convert.ToDecimal(Eval("BL")).ToString("0.00") %>%
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="本期计划金额" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
												<%# Eval("CurrPlanMoney").ToString() %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态" HeaderStyle-Width="70px">
<ItemTemplate>
												<%# Common2.GetState(Eval("FlowState").ToString()) %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="填报人" HeaderStyle-Width="60px">
<ItemTemplate>
												<%# PageHelper.QueryUser(this, Eval("OperatorCode").ToString()) %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="填报日期" HeaderStyle-Width="70px">
<ItemTemplate>
												<%# Common2.GetTime(Eval("OperateTime").ToString()) %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注">
<ItemTemplate>
												<asp:TextBox ID="txtRemark" Text='<%# Convert.ToString(Eval("Remark")) %>' runat="server"></asp:TextBox>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="" Visible="false">
<ItemTemplate>
												<asp:Label ID="lblstate" Text='<%# Convert.ToString(Eval("FlowState")) %>' runat="server"></asp:Label>
											</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	<div id="divFooter" class="divFooter2">
		<table class="tableFooter2">
			<tr>
				<td>
					<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
					<input type="button" id="btnCancel" value="取消" />
				</td>
			</tr>
		</table>
	</div>
	<div id="divSelectFromPlan" title="从计划中选择" style="display: none">
		<iframe id="ifSelectFromPlan" frameborder="0" width="100%" height="100%" runat="server">
		</iframe>
	</div>
	<div id="divFram" title="" style="display: none">
		<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<asp:HiddenField ID="hdnAccountID" runat="server" />
	<asp:HiddenField ID="HdnPlanType" runat="server" />
	<asp:HiddenField ID="HdnPlanGuid" runat="server" />
	</form>
</body>
</html>
