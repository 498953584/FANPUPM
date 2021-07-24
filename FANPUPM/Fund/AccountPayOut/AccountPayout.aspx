<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountPayout.aspx.cs" Inherits="Fund_AccountPayOut_AccountPayOut" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="com.jwsoft.pm.entpm"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>支出记账</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="/UserControl/FileUpload/GetFiles.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<link href="../../StyleCss/PM4.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript">
		addEvent(window, 'load', function () {
			replaceEmptyTable('emptyAccount', 'gvwAccount');
			var accTable = new JustWinTable('gvwAccount');
			if (document.getElementById("hdnZHID").value == "") {
				document.getElementById("btnAdd").setAttribute('disabled', 'disabled');
				document.getElementById("btnUpdate").setAttribute('disabled', 'disabled');
				document.getElementById("btnQuery").setAttribute('disabled', 'disabled');
			}
			else {
				setButton(accTable, 'btnDelete', 'btnUpdate', 'btnQuery', 'HdnAccountID');
				setWfButtonState2(accTable, 'WF1');
			}
			registerAddEvent();
			registerDeleteEvent();
			registerUpdateEvent();
			registerQueryEvent();
			displayLookAdjuncts();
		});

		function registerAddEvent() {
			var btnAdd = document.getElementById('btnAdd');
			addEvent(btnAdd, 'click', function () {
				var ZHID = document.getElementById('hdnZHID').value;
				var sub = document.getElementById('hdnSub').value;
				parent.parent.desktop.ModifyEdit = window;
				top.ui._accountpayout = window;
				var url = "/Fund/AccountPayout/AccountPayoutEdit.aspx?Action=Add&ZHID=" + ZHID + "&Sub=" + document.getElementById("hdnSub").value; //+"&PrjName="+escape(PrjName);
				toolbox_oncommand(url, "新增入账单");
			});
		}

		function registerDeleteEvent() {
			var btnDelete = document.getElementById('btnDelete');
			btnDelete.onclick = function () {
				if (!confirm("确定要删除吗")) {
					return false;
				}
			}
		}

		function registerUpdateEvent() {
			var btnUpdate = document.getElementById('btnUpdate');
			addEvent(btnUpdate, 'click', function () {
				var ZHID = document.getElementById('hdnZHID').value;
				var AccountID = document.getElementById('HdnAccountID').value;
				var sub = document.getElementById('hdnSub').value;
				top.ui._accountpayout = window;
				var url = "/Fund/AccountPayout/AccountPayoutEdit.aspx?Action=Update&AccountID=" + AccountID + "&ZHID=" + ZHID + "&Sub=" + sub;
				toolbox_oncommand(url, "编辑入账单");

			});
		}

		function registerQueryEvent() {
			var btnQuery = document.getElementById('btnQuery');
			addEvent(btnQuery, 'click', function () {
				var AccountID = document.getElementById('HdnAccountID').value;
				var url = "/Fund/AccountPayout/AccountPayoutView.aspx?see=see&ic=" + AccountID;
				viewopen(url);
			});
		}
		//查看附件
		function queryAdjunct(id) {
			var path = $('#hfldAdjunctPath').val() + '/' + id;
			parent.window.showFiles(path, 'divOpenAdjunct');
		}
		//附件显示
		function displayLookAdjuncts() {
			//            var tab = document.getElementById('gvwAccount');
			var rows = $("#gvwAccount").find("tr");
			if (rows.length > 0) {
				for (i = 1; i < rows.length; i++) {
					var id = rows.eq(i).attr("id");
					var path = $('#hfldAdjunctPath').val() + '/' + id;
					var showCount = getFilesCount(path);
					if (showCount == 0)
						rows.eq(i).find("td").eq(9).text("");
					//                        tab.rows[i].cells[9].innerText = '';
				}
			}
		}
		function getFilesCount(path) {
			var count = 0;
			$.ajax({
				type: "GET",
				url: "/UserControl/FileUpload/GetFiles.ashx?" + new Date().getTime() + '&Path=' + path,
				async: false,
				dataType: "JSON",
				success: function (data) {
					count = data.length;
				}
			});
			return count;
		}
        
	</script>
</head>
<body class="body_clear" scroll="no">
	<form id="form1" method="post" runat="server">
	<table cellspacing="0" cellpadding="0" height="100%" width="100%" border="1" align="center"
		class="tab">
		<tr style="vertical-align: top;">
			<td class="divFooter" style="text-align: left;" height="26px">
				
				<input type="button" id="btnAdd" value="新增" />
				<input type="button" id="btnUpdate" value="编辑" disabled="disabled" />
				<asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
				<input type="button" id="btnQuery" disabled="disabled" value="查看" />
				<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiClass="001" runat="server" />
			</td>
		</tr>
		<tr>
			<td height="90%" valign="top">
				<div style="overflow: auto; width: 100%; height: 90%">
					<asp:GridView ID="gvwAccount" CssClass="gvdata grid" AllowPaging="true" AutoGenerateColumns="false" OnPageIndexChanging="gvwAccount_PageIndexChanging" OnRowDataBound="gvwAccount_RowDataBound" DataKeyNames="PayoutGuid" runat="server">
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
										入账单号
									</th>
									
									<th scope="col">
										所属单据
									</th>
									<th scope="col">
										入账金额
									</th>
									<th scope="col">
										经手人
									</th>
									<th scope="col">
										入账人
									</th>
									<th scope="col">
										入账日期
									</th>
									<th scope="col">
										流程状态
									</th>
									<th scope="col">
										附件
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
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="入账单号">
<ItemTemplate>
									<span class="link" onclick="viewopen('/Fund/AccountPayout/AccountPayoutView.aspx?see=see&ic=<%# Eval("PayoutGuid") %>')">
										<%# Eval("PayoutCode") %>
									</span>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合同支付单号"><ItemTemplate>
									<asp:Label ID="lblCode" Text='<%# Convert.ToString(Eval("PaymentCode")) %>' runat="server"></asp:Label>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="费用名称"><ItemTemplate>
									<asp:Label ID="lblName" Text='<%# Convert.ToString(Eval("BudName")) %>' runat="server"></asp:Label>
								</ItemTemplate></asp:TemplateField><asp:BoundField DataField="PayOutMoney" HeaderText="入账金额" /><asp:BoundField DataField="Handler" HeaderText="经手人" /><asp:TemplateField HeaderText="入账人">
<ItemTemplate>
									<%# PageHelper.QueryUser(this, Eval("PayOutPeople").ToString()) %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="入账日期">
<ItemTemplate>
									<%# Common2.GetTime(Eval("PayOutTime").ToString()) %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="流程状态">
<ItemTemplate>
									<%# Common2.GetState(Eval("FloeState").ToString()) %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="附件">
<ItemTemplate>
									<span class="link" onclick="queryAdjunct('<%# Eval("PayoutGuid") %>')">
										<img src='/images1/icon_att0b3dfa.gif' style='cursor: pointer; border-style: none' />
									</span>
								</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				</div>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hdnZHID" runat="server" />
	<asp:HiddenField ID="hfldProjectName" runat="server" />
	<input type="hidden" id="hdnSub" runat="server" />

	<asp:HiddenField ID="HdnAccountID" runat="server" />
	<asp:HiddenField ID="hfldAdjunctPath" runat="server" />
	</form>
</body>
</html>
