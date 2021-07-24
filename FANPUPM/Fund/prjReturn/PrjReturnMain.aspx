<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjReturnMain.aspx.cs" Inherits="Fund_prjReturn_PrjReturnMain" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>入账信息管理</title><link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/StockManage/script/tab.js"></script>
	<script type="text/javascript" src="/Script/DecimalInput.js"></script>
	<script src="../../Web_Client/Tree.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			// 添加验证
			$("#btnSearch")[0].onclick = function () {
				if (!$('#form1').form('validate')) {
					return false;
				}
			}
			replaceEmptyTable('emptyAccount', 'gvwAccount');
			var contractTable = new JustWinTable('gvwAccount');

			//trFrame 为存放Frame的TR
			//必需将整个Table的高度设置为100%，且第二个TR的高度为1px
			var trWidth = document.getElementById('trFrame').offsetHeight;
			document.getElementById('framAccount').style.height = (trWidth - 3) + 'px';
		});


		function ClickRow(accountid, state) {
			document.getElementById("hfldAccount").value = accountid;
			document.getElementById("hdfReturnState").value = state;
			setf();
		}
		//切换下窗口关联页面
		function setf() {
			url = "../prjReturn/prjReturnList.aspx";
			document.getElementById("framAccount").src = url + "?LoanID=" + document.getElementById("hfldAccount").value + "&returnState=" + document.getElementById("hdfReturnState").value; //+"&prjname="+document.getElementById("hdnprjname").value;

		}
		//选择项目
		function openProjPicker() {
			jw.selectOneProject({ idinput: 'hdnProjectCode', nameinput: 'txtProject' });
		}

		//选择人员
		function selectUser() {
			jw.selectOneUser({ codeinput: 'hfdPeople', nameinput: 'txtPeople' });
		}

		//选择账户
		function openAccoun() {
			var url = '/Common/DivSelectFundAccoun.aspx';
			top.ui.callback = function (o) {
				$('#hdfAccoun').val(o.id);
				$('#txtAccoun').val(o.name);
			}
			top.ui.openWin({ title: '选择账户', url: url });
		}
          
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;
		height: 100%; vertical-align: top;">
		<tr id="header">
			<td colspan="2">
				<asp:Label ID="lblTitle" Text="" runat="server"></asp:Label>
			</td>
		</tr>
		<tr style="height: 1px;">
			<td style="vertical-align: top;">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							借款编号
						</td>
						<td>
							<asp:TextBox ID="txtCode" Width="120px" CssClass="easyui-validatebox" data-options="required:false, validType:'validChars[25]'" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							借款金额
						</td>
						<td>
							<asp:TextBox ID="txtMoney" Width="120px" CssClass="decimal_input   {required:true,number:true, messages:{required:'借款金额必须输入',number:' 借款金额格式错误'}}" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							约定归还日期
						</td>
						<td>
							<asp:TextBox ID="txtBeginTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							-
						</td>
						<td>
							<asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							借款人
						</td>
						<td>
							<span class="spanSelect" style="width: 122px;">
								<input id="txtPeople" readonly="readonly" style="width: 97px; height: 15px; border: none;
									line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

								<img alt="选择借款人" onclick="selectUser();" src="../../images/icon.bmp" style="float: right;" />
								<asp:HiddenField ID="hfdPeople" runat="server" />
							</span>
						</td>
						<td class="descTd">
							项目
						</td>
						<td>
							<span class="spanSelect" style="width: 122px">
								<input id="txtProject" readonly="readonly" style="width: 97px; height: 15px;
									border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

								<img alt="选择项目" onclick="openProjPicker();" src="../../images/icon.bmp" style="float: right;" />
							</span>
							<asp:HiddenField ID="hdnProjectCode" runat="server" />
						</td>
						<td class="descTd">
							账户名称
						</td>
						<td>
							<span class="spanSelect" style="width: 122px">
								<input id="txtAccoun" readonly="readonly" style="width: 97px; height: 15px;
									border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

								<img alt="选择账户" onclick="openAccoun();" src="../../images/icon.bmp" style="float: right;" />
							</span>
							<asp:HiddenField ID="hdfAccoun" runat="server" />
						</td>
						<td>
						</td>
						<td>
							<asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
						</td>
					</tr>
				</table>
			</td>
			<td align="right" style="vertical-align: bottom;">
				<asp:Label ID="Label2" Text="单位：元" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</td>
		</tr>
		<tr>
			<td style="height: 160px; width: 100%;" colspan="2">
				<table class="tab">
					<tr>
						<td style="height: 100%; vertical-align: top;">
							<div class="pagediv">
								<asp:GridView ID="gvwAccount" CssClass="gvdata" Width="100%" AllowPaging="true" PageSize="5" AutoGenerateColumns="false" OnRowDataBound="gvwAccount_RowDataBound" OnPageIndexChanging="gvwAccount_PageIndexChanging" DataKeyNames="LoanID,ReturnState" runat="server">
<EmptyDataTemplate>
										<table id="emptyAccount" class="gvdata">
											<tr class="header">
												<th scope="col" style="width: 20px;">
													<input id="chk1" type="checkbox" />
												</th>
												<th scope="col" style="width: 25px;">
													序号
												</th>
												<th scope="col">
													借款编号
												</th>
												<th scope="col">
													借款金额
												</th>
												<th scope="col">
													借款利率
												</th>
												<th scope="col">
													借款用途
												</th>
												<th scope="col">
													借款人
												</th>
												<th scope="col">
													约定归还日期
												</th>
												<th scope="col">
													项目
												</th>
												<th scope="col">
													项目编号
												</th>
												<th scope="col">
													账户名称
												</th>
												<th scope="col">
													账户编号
												</th>
											</tr>
										</table>
									</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
												<asp:CheckBox ID="chkAll" runat="server" />
											</HeaderTemplate>

<ItemTemplate>
												<asp:CheckBox ID="chk" runat="server" />
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="借款编号">
<ItemTemplate>
												<span class="link" onclick="toolbox_oncommand('/Fund/PrjLoan/PrjLoanView.aspx?ic=<%# Eval("loanid") %>', '借款信息查看')">
													<%# Eval("LoanCode") %>
												</span>
											</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="借款金额" DataField="LoanFund" ItemStyle-CssClass="decimal_input" /><asp:TemplateField HeaderText="还款情况">
<ItemTemplate>
												<asp:Label ID="labRetState" Text='<%# Convert.ToString((DataBinder.Eval(Container.DataItem, "ReturnState").ToString() == "0") ? "未还清" : "已还清") %>' runat="server"></asp:Label>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="借款利率">
<ItemTemplate>
												<%# DataBinder.Eval(Container.DataItem, "rate") %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="借款用途">
<ItemTemplate>
												<asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" ToolTip='<%# Convert.ToString(Eval("LoanCause").ToString()) %>' runat="server"><%# Convert.ToString(StringUtility.GetStr(Eval("LoanCause").ToString(), 10)) %></asp:HyperLink>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="约定归还日期">
<ItemTemplate>
												<%# DataBinder.Eval(Container.DataItem, "PlanReturnDate", "{0:yyyy-MM-dd}") %>
											</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="借款人" DataField="v_xm" /><asp:BoundField HeaderText="借款日期" DataField="LoanDate" DataFormatString="{0:yyyy-MM-dd}" /><asp:TemplateField HeaderText="项目">
<ItemTemplate>
												<asp:HyperLink ID="hlinkprjName" Style="text-decoration: none; color: Black;" ToolTip='<%# Convert.ToString(Eval("prjName").ToString()) %>' runat="server"><%# Convert.ToString(StringUtility.GetStr(Eval("prjName").ToString(), 10)) %></asp:HyperLink>
											</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="账户编号" DataField="accountNum" /><asp:TemplateField HeaderText="账户名称">
<ItemTemplate>
												<asp:HyperLink ID="hlinkAcconName" Style="text-decoration: none; color: Black;" ToolTip='<%# Convert.ToString(Eval("acountName").ToString()) %>' runat="server"><%# Convert.ToString(StringUtility.GetStr(Eval("acountName").ToString(), 10)) %></asp:HyperLink>
											</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr id="trFrame">
			<td style="width: 100%; vertical-align: top;" colspan="2">
				<iframe id="framAccount" frameborder="0" width="100%" runat="server"></iframe>
			</td>
		</tr>
	</table>
	<div id="divFram" title="" style="display: none">
		<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<asp:HiddenField ID="hfldAccount" runat="server" />
	<asp:HiddenField ID="hdframsrc" runat="server" />
	<asp:HiddenField ID="hdnType" runat="server" />
	<asp:HiddenField ID="hdnprjname" runat="server" />
	<asp:HiddenField ID="hdfReturnState" runat="server" />
	</form>
</body>
</html>
