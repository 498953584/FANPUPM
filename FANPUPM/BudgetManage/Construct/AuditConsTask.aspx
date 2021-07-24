<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditConsTask.aspx.cs" Inherits="BudgetManage_Construct_AuditConsTask" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_usercontrol_selectrestask_ascx" Src="~/StockManage/UserControl/SelectResTask.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>施工报量审核</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			var gvTask = new JustWinTable('gvTask');
		});

		function theMonenyChange(index, isOnblur) {
			tb = document.getElementById('gvTask');
			var cells = tb.rows[parseFloat(index) + 1].cells;
			var reportQuantity = parseFloat(cells[4].innerText);
			if (isOnblur == '1') {
				var AccountingQuantity = parseFloat(cells[7].children[0].value);
				if (AccountingQuantity < 0) {
					alert("系统提示：\n\n审核工程量不能小于0！")
					cells[7].children[0].value = "0.000";
				}
			}
		}
		var preCheckId = '';
		function displayRes(consTask, taskId) {
			if (preCheckId != taskId)
				document.getElementById('frConsRes').src = 'AuditConsRes.aspx?consTaskId=' + consTask + '&taskId=' + taskId;
			preCheckId = taskId;
		}

		function sel() {
			var doc = document.frames("frConsRes").document;
			if (doc.getElementById('gvResource') != null) {
				$('#hfldConsTaskResource').val('1');
				var tbRes = doc.getElementById('gvResource');
				var noSelResType = false;
				var noSelResTypeIndex = 0;
				for (i = 1; i < tbRes.rows.length; i++) {
					var selResType = tbRes.rows[i].cells[12].firstChild.value;
					if (jw.trim(selResType) == '') {
						noSelResType = true;
						noSelResTypeIndex = i;
						break;
					}
				}
				if (noSelResType) {
					alert("系统提示：\n\n请选择成本分类！");
					tbRes.rows[noSelResTypeIndex].cells[12].firstChild.focus();
					return false;
				}
				else {
					for (i = 1; i < tbRes.rows.length; i++) {
						var row = tbRes.rows[i];
						var id = row.id;
						var quantity = row.cells[8].firstChild.value;
						var code = row.cells[12].firstChild.value;
						$.ajax({
							type: "GET",
							anayc: false,
							url: "/BudgetManage/Handler/UpdateAccQuantityCBSCode.ashx?" + new Date().getTime() + "&id=" + id + "&quantity=" + quantity + "&code=" + code,
							dataType: "text",
							success: function (data) {

							}
						});
					}
					return true;
				}
			}
			else {
				$('#hfldConsTaskResource').val('0');
			}
		}
	</script>
	<style type="text/css">
		.style1
		{
			width: 80px;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divContent" class="divContent2" style="width: 100%; height: 99%; overflow: hidden;">
		<table style="border: 0px; width: 100%; height: 100%;">
			<tr style="width: 100%;">
				<td style="text-align: right;" class="divFooter">
					
					<asp:Button ID="btnUpdate" Width="80px" Text="保存" OnClick="btnUpdate_Click" runat="server" />
					
				</td>
			</tr>
			<tr>
				<td style="width: 100%; height: 60%; vertical-align: top;">
					<div id="divBudget" class="pagediv" style="border-bottom: solid 0px red;">
						<asp:GridView ID="gvTask" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvTask_RowDataBound" DataKeyNames="ConsTaskId,TaskId" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="25px">
<HeaderTemplate>
										序号
									</HeaderTemplate>

<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
										任务编码
									</HeaderTemplate>

<ItemTemplate>
										<%# Eval("TaskCode") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
										分项工作
									</HeaderTemplate>

<ItemTemplate>
										<%# Eval("TaskName") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<HeaderTemplate>
										工作量
									</HeaderTemplate>

<ItemTemplate>
										<%# Eval("Quantity") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<HeaderTemplate>
										累计审核量
									</HeaderTemplate>

<ItemTemplate>
										<%# Eval("SumAccountingQuantity") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<HeaderTemplate>
										剩余审核量
									</HeaderTemplate>

<ItemTemplate>
										<%# Eval("SurplusQuantity") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<HeaderTemplate>
										上报工程量
									</HeaderTemplate>

<ItemTemplate>
										<%# Eval("CompleteQuantity") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<HeaderTemplate>
										审核工程量
										
									</HeaderTemplate>

<ItemTemplate>
										<asp:TextBox ID="txtAccountingQuantity" Style="text-align: right;
											width: 90px; height: 15px;" CssClass="decimal_input" onblur="theMonenyChange(this.No,'1')" onkeypress="theMonenyChange(this.No,'0')" onkeyup="theMonenyChange(this.No,'0')" Text='<%# System.Convert.ToString(Eval("AccountingQuantity"), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
										工作内容
									</HeaderTemplate>

<ItemTemplate>
										<%# Eval("WorkContent") %>
									</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					</div>
				</td>
			</tr>
			<tr>
				<td>
				</td>
			</tr>
			<tr>
				<td style="width: 100%; height: 33%;">
					<div id="divConsResource">
						
						<iframe id="frConsRes" src="AuditConsRes.aspx" frameborder="0" width="100%" height="100%">
						</iframe>
					</div>
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	<asp:HiddenField ID="hfldReportId" runat="server" />
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	<!-- 保存选择的资源-->
	<asp:HiddenField ID="hfldResourceId" runat="server" />
	
	<asp:HiddenField ID="hfldConsTaskResource" runat="server" />
	<asp:HiddenField ID="hfldResInfo" runat="server" />
	</form>
</body>
</html>
