<%@ Page Language="C#" AutoEventWireup="true" CodeFile="prjReturnList.aspx.cs" Inherits="Fund_prjReturn_prjReturnList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>资金管理-项目还款</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="/UserControl/FileUpload/GetFiles.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var ReturnTable = new JustWinTable('gvBudget');
			if (document.getElementById("hfldLoanID").value == "") {
				document.getElementById("btnEdit").setAttribute('disabled', 'disabled');
				document.getElementById("btnQuery").setAttribute('disabled', 'disabled');
				document.getElementById("btnDel").setAttribute('disabled', 'disabled');
			}
			else {
				setButton(ReturnTable, 'btnDel', 'btnEdit', 'btnQuery', 'hfldPurchaseChecked');

			}
			setWfButtonState2(ReturnTable, 'WF1');
			addEvent(document.getElementById('btnEdit'), 'click', rowEdit);
			addEvent(document.getElementById('btnQuery'), 'click', rowQuery);
			addEvent(document.getElementById('btnAdd'), 'click', rowAdd);
		});

		function rowAdd() {
			top.ui._prjreturnlist = window;
			var url = "/Fund/prjReturn/prjReturnEdit.aspx?LoanID=" + document.getElementById("hfldLoanID").value;
			toolbox_oncommand(url, "新增还款单");
		}
		function rowEdit() {
			top.ui._prjreturnlist = window;
			var url = "/Fund/prjReturn/prjReturnEdit.aspx?LoanID=" + document.getElementById("hfldLoanID").value + "&id=" + document.getElementById("hfldPurchaseChecked").value;
			toolbox_oncommand(url, "编辑还款单");
		}
		function rowQuery() {
			var url = "/Fund/prjReturn/prjReturnView.aspx?ic=" + document.getElementById("hfldPurchaseChecked").value;
			viewopen(url);
		}

      
       
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: auto;">
		<table style="width: 100%; height: 100%;">
			<tr>
				<td style="width: 100%; height: 100%;">
					<table style="width: 100%; height: 100%;">
						<tr>
							<td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
								<table class="tab" style="vertical-align: top;">
									<tr>
										<td class="divFooter" style="text-align: left;">
											<input type="button" value="新增" id="btnAdd" disabled="true" runat="server" />

											<input id="btnEdit" type="button" value="编辑" disabled="disabled" />
											<asp:Button ID="btnDel" Text="删除" disabled="disabled" OnClick="btnDel_Click" runat="server" />
											<input type="button" id="btnQuery" value="查看" disabled="disabled" />
											<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="102" BusiClass="001" runat="server" />
										</td>
									</tr>
									<tr>
										<td style="vertical-align: top; height: 100%;">
											<div id="divBudget" style="overflow: hidden;">
												<div id="divDiaries" style="overflow: auto;">
													<asp:GridView ID="gvBudget" CssClass="gvdata" AutoGenerateColumns="false" AllowPaging="true" PageSize="15" OnPageIndexChanging="gvBudget_PageIndexChanging" OnRowDataBound="gvBudget_RowDataBound" DataKeyNames="FR_id" runat="server">
<EmptyDataTemplate>
															<table id="emptyContractType" class="gvdata" width="100%" border="0">
																<tr class="header">
																	<th scope="col">
																		<input id="Checkbox1" type="checkbox" />
																	</th>
																	<th scope="col">
																		序号
																	</th>
																	<th scope="col">
																		还款编号
																	</th>
																	<th scope="col">
																		归还本金
																	</th>
																	<th scope="col">
																		归还利息
																	</th>
																	<th scope="col">
																		其他扣款
																	</th>
																	<th scope="col">
																		还款人
																	</th>
																	<th scope="col">
																		还款时间
																	</th>
																	<th scope="col">
																		流程状态
																	</th>
																	<th scope="col">
																		备注
																	</th>
																</tr>
															</table>
														</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
<HeaderTemplate>
																	<asp:CheckBox ID="chkAll" runat="server" />
																</HeaderTemplate>

<ItemTemplate>
																	<asp:CheckBox ID="chk" ToolTip='<%# Convert.ToString(Eval("FR_id")) %>' runat="server" />
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
																	<%# Container.DataItemIndex + 1 %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="还款编号">
<ItemTemplate>
																	<span class="link" onclick="viewopen('/Fund/prjReturn/prjReturnView.aspx?ic=<%# Eval("FR_id") %>', '还款信息查看')">
																		<%# Eval("FR_Code") %>
																	</span>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="归还本金">
<ItemTemplate>
																	<%# DataBinder.Eval(Container.DataItem, "FR_Money", "{0:F3}") %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="归还利息" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
																	<%# Eval("FR_interest") %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="其他扣款" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
																	<%# DataBinder.Eval(Container.DataItem, "FR_deduct", "{0:F3}") %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="还款人">
<ItemTemplate>
																	<%# Eval("runPersonnel") %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="还款时间">
<ItemTemplate>
																	<%# DataBinder.Eval(Container.DataItem, "FR_data", "{0:yyyy-MM-dd}") %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="流程状态">
<ItemTemplate>
																	<%# Common2.GetState(Eval("FR_flowState").ToString()) %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注">
<ItemTemplate>
																	
																	<asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" ToolTip='<%# Convert.ToString(Eval("FR_remark").ToString()) %>' runat="server"><%# Convert.ToString(StringUtility.GetStr(Eval("FR_remark").ToString(), 10)) %></asp:HyperLink>
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
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hfldLoanID" runat="server" />
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	</form>
</body>
</html>
