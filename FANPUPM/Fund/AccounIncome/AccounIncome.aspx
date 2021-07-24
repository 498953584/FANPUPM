<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccounIncome.aspx.cs" Inherits="Fund_AccounIncome_AccounIncome" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>收入记账</title>
    <script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="/UserControl/FileUpload/GetFiles.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
	<link href="../../StyleCss/PM4.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript">
		addEvent(window, 'load', function () {
			replaceEmptyTable('emptyAccount', 'gvwAccount');
			var accTable = new JustWinTable('gvwAccount');
			if (document.getElementById("hfldProjectGuid").value == "") {
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
				var ZHID = document.getElementById('hfldProjectGuid').value;
				top.ui._accountincome = window;
				var url = "/Fund/AccounIncome/AccounIncomeEdit.aspx?Action=Add&ZHID=" + ZHID;
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
				var ZHID = document.getElementById('hfldProjectGuid').value;
				var AccountID = document.getElementById('HdnAccountID').value;
				top.ui._accountincome = window;
				var url = "/Fund/AccounIncome/AccounIncomeEdit.aspx?Action=Update&AccountID=" + AccountID + "&ZHID=" + ZHID;
				toolbox_oncommand(url, "编辑入账单");

			});
		}

		function registerQueryEvent() {
			var btnQuery = document.getElementById('btnQuery');
			addEvent(btnQuery, 'click', function () {
				var AccountID = document.getElementById('HdnAccountID').value;
				var url = "/Fund/AccounIncome/AccounIncomeView.aspx?ic=" + AccountID;
				viewopen(url);
			});
		}
		/*
		//查看附件
		function queryAdjunct(id) {
		var path = $('#hfldAdjunctPath').val() + '/' + id;
		parent.window.showFiles(path, 'divOpenAdjunct');
		}
		*/
		//查看附件
		function queryAdjunct(id) {
			var path = $('#hfldAdjunctPath').val() + id;
			showFiles(path, 'divOpenAdjunct');
		}
		function showFiles(folder, divId) {
			$('#' + divId + ' table tr:gt(0)').remove();
			$.getJSON('/UserControl/FileUpload/GetFiles.ashx?' + new Date().getTime(), { Path: folder }, function (data) {
				$('#' + divId + ' table').empty();
				var content;
				//table 头
				var $thName = "<td class=tdStyle style=' width:280px; height:20px;border:1px solid rgb(202,222,222);text-align:center;background-color:rgb(238,242,242)'>文件名称</td>";
				var $thLength = "<td  style=' width:140px; height:20px;border:1px solid rgb(202,222,222);text-align:center;background-color:rgb(238,242,242)' >文件大小</td>"
				var $trTitle = "<tr class style='width:420px;border:1px solid rgb(202,204,204)；'>" + $thName + $thLength + "</tr>";
				content = $trTitle;
				$.each(data, function (index, annex) {
					var style;
					var $a = "<a target='_blank' href=" + jw.downloadUrl + "?path=" + folder + '/' + annex.Name + " style='cursor:pointer' class='linkAnnex'>" + decodeURIComponent(annex.Name) + "</a>";
					if (index % 2 == 0) {
						style = 'rowa';
					} else {
						style = 'rowb';
					}
					var $tdName = "<td class=tdStyle style=' width:280px; height:20px;border:1px solid rgb(202,222,222);text-align:center;'>" + $a + "</td>";
					var $tdLength = "<td  style=' width:140px; height:20px;border:1px solid rgb(202,222,222);text-align:center;' >" + annex.Length + "</td>"
					var $tr = "<tr class=" + style + " style='width:420px;border:1px solid rgb(202,222,222)'>" + $tdName + $tdLength + "</tr>";
					content += $tr;
				});
				var table = $("<div style='width:420px;height:200px;padding:0px;margin:0px auto;text-align:center;'><table id=" + divId + " style=' text-align:center; border-collapse:collapse; width:418px; height:auto; padding:0px; margin:1px; border:1px solid rgb(202,222,222);border-bottom:0px;'>" + content + "</table></div>");
				$(table).dialog({ width: 434, height: 300, modal: true, title: '附件' });
			});
		}
		//附件显示 
		function displayLookAdjuncts() {
			var rows = $("#gvwAccount").find("tr");
			if (rows.length > 0) {
				for (i = 1; i < rows.length; i++) {
					var id = rows.eq(i).attr("id");
					var path = $('#hfldAdjunctPath').val() + '/' + id;
					var showCount = getFilesCount(path);
					if (showCount == 0)
						rows.eq(i).find("td").eq(13).text("");
				}
			}
		}
		function getFilesCount(path) {
			var count = 1;
			$.ajax({
				type: "GET",
				url: "/UserControl/FileUpload/GetFiles.ashx?" + new Date().getTime() + '&Path=' + path,
				async: false,
				dataType: "JSON",
				success: function (data) {
					//if (data == '[]')
					//	count = 0;
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
				<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="093" BusiClass="001" runat="server" />
			</td>
		</tr>
		<tr>
			<td height="90%" valign="top">
				<div style="overflow: auto; width: 100%; height: 90%">
					<asp:GridView ID="gvwAccount" CssClass="gvdata" AllowPaging="true" AutoGenerateColumns="false" OnPageIndexChanging="gvwAccount_PageIndexChanging" OnRowDataBound="gvwAccount_RowDataBound" DataKeyNames="InUid" runat="server">
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
										所属计划
									</th>
									<th scope="col">
										依据合同
									</th>
									<th scope="col">
										入账类型
									</th>
									<th scope="col">
										回款日期
									</th>
									<th scope="col">
										回款金额
									</th>
									<th scope="col">
										回款入账金额
									</th>
									<th scope="col">
										入账人
									</th>
									<th scope="col">
										入账时间
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
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="入账单号"><ItemTemplate>
									<span class="link" onclick="viewopen('/Fund/AccounIncome/AccounIncomeView.aspx?ic=<%# Eval("InUid") %>')">
										<%# Eval("InCode") %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="所属合同" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
									<span class="tooltip" title='<%# Eval("ContractName").ToString() %>'>
										<%# StringUtility.GetStr(Eval("ContractName").ToString(), 20) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="所属合同收款" DataField="cllectionCode" /><asp:BoundField HeaderText="所属计划" DataField="PlanName" /><asp:BoundField DataField="Subject" HeaderText="入账类型" ItemStyle-HorizontalAlign="Center" /><asp:TemplateField HeaderText="回款日期">
<ItemTemplate>
									<%# Common2.GetTime(Eval("GetDate").ToString()) %>
								</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="GetMoney" HeaderText="回款金额" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="InMoney" HeaderText="回款入账金额" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="People" HeaderText="入账人" /><asp:TemplateField HeaderText="入账时间">
<ItemTemplate>
									<%# Common2.GetTime(Eval("InDate").ToString()) %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="流程状态">
<ItemTemplate>
									<%# Common2.GetState(Eval("FlowState").ToString()) %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="附件">
<ItemTemplate>
									<span class="link" onclick="queryAdjunct('<%# Eval("InUid") %>')">
										<img src='/images1/icon_att0b3dfa.gif' style='cursor: pointer; border-style: none' />
									</span>
								</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				</div>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hfldProjectGuid" runat="server" />
	<asp:HiddenField ID="HdnAccountID" runat="server" />
	<asp:HiddenField ID="hfldAdjunctPath" runat="server" />
	</form>
</body>
</html>
