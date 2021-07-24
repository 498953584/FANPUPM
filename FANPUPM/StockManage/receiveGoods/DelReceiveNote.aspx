<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DelReceiveNote.aspx.cs" Inherits="StockManage_receiveGoods_DelReceiveNote" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>发货通知单列表</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../script/Config2.js"></script>
	<script type="text/javascript" src="../script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/jwJson.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var purchasePlanTable = new JustWinTable('gvPurchaseplan');
			function rowQuery() {
				var url = "ViewReceiveNote.aspx?t=1&ic=" + document.getElementById("hfldPurchaseChecked").value;
				viewopen(url, 820, 500);
			}
			replaceEmptyTable('emptyPurchaseplan', 'gvPurchaseplan');
		});

		//选择人员
		function selectUser() {
			jw.selectOneUser({ nameinput: 'txtPeople', codeinput: 'hdnPeople' });
		}
  
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		<tr style="height: 20px;">
			<td class="divHeader">
				<asp:Label ID="lblProject" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td style="width: 100%; vertical-align: top;">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							起始时间
						</td>
						<td>
							<asp:TextBox ID="txtStartTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							结束时间
						</td>
						<td>
							<asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							收货通知单
						</td>
						<td>
							<asp:TextBox ID="txtrnCode" Height="15px" Width="120px" runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							录 &nbsp; 入 人
						</td>
						<td>
							<input type="text" style="width: 120px;" id="txtPeople" class="select_input" imgclick="selectUser" runat="server" />

							<asp:HiddenField ID="hdnPeople" runat="server" />
						</td>
						<td>
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
							<asp:Button ID="btnDel" Text="删  除" OnClick="btnDel_Click" runat="server" />
							
						</td>
					</tr>
					<tr>
						<td>
							<div class="">
								<asp:GridView ID="gvPurchaseplan" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvPurchaseplan_PageIndexChanging" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="rnId,snId" runat="server">
<EmptyDataTemplate>
										<table id="emptyPurchaseplan" class="rowa" style="width: 100%; text-align: center;">
											<tr class="header">
												<th scope="col" style="width: 20px;">
													<asp:CheckBox ID="cbAllBox" runat="server" />
												</th>
												<th scope="col" style="width: 25px;">
													序号
												</th>
												<th scope="col">
													收货通知单
												</th>
												<th scope="col">
													录入人
												</th>
												<th scope="col">
													录入时间
												</th>
											</tr>
										</table>
									</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
												<asp:CheckBox ID="cbAllBox" runat="server" />
											</HeaderTemplate><ItemTemplate>
												<asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("rnId")) %>' runat="server" />
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="收货通知单"><ItemTemplate>
												<span class="al" onclick="viewopen('ViewReceiveNote.aspx?ic=<%# Eval("rnId") %>',820,500)">
													<%# Eval("rnCode") %>
												</span>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入人"><ItemTemplate>
												<asp:Label ID="labUser" Text='<%# Convert.ToString(Eval("rnUser")) %>' runat="server"></asp:Label>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入时间"><ItemTemplate>
												<%# Common2.GetTime(Eval("rnTime").ToString()) %>
											</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
	<div id="divFramPrj" title="" style="display: none">
		<iframe id="ifFramPrj" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	</form>
</body>
</html>
