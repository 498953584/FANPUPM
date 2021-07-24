<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnsureStorage.aspx.cs" Inherits="StockManage_Storage_EnsureStorage" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>确认入库</title>
	<style type="text/css">
		#btnEnsure
		{
			background: #fff url(/images1/btnBack.jpg);
			text-align: center;
			vertical-align: middle;
			width: 75px;
			height: 20px;
			border-style: none;
		}
	</style>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../script/Config2.js"></script>
	<script type="text/javascript" src="../script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var storageTable = new JustWinTable('gvwStorage');

			storageTable.registClickTrListener(function () {
				$('#hfldStorageCode').val(this.id);
				$('#btnEnsure').removeAttr('disabled');
				if ($(this).attr('isfirst') == 'True') {
					$('#hfldIsFirst').val('F');
				} else {
					$('#hfldIsFirst').val('S');
				}
			});
		});

		//选择人员
		function selectUser() {
			jw.selectOneUser({ nameinput: 'txtPeople' });
		}

		function selectTrea() {
			jw.selectTreasury({ nameinput: 'txtTrea' });
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		<tr>
			<td>
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
							入库编号
						</td>
						<td>
							<asp:TextBox ID="txtStorage" Width="120px" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							仓库名称
						</td>
						<td>
							<asp:TextBox ID="txtTrea" CssClass="select_input" Width="120px" imgclick="selectTrea" runat="server"></asp:TextBox>
						</td>
						<td>
						</td>
					</tr>
					<tr style="height: 30px;">
						<td class="descTd">
							录入人
						</td>
						<td>
							<asp:TextBox ID="txtPeople" CssClass="txtreadonly select_input" Style="width: 120px;" imgclick="selectUser" runat="server"></asp:TextBox>
						</td>
						<td style="text-align: left">
							<asp:Button ID="btnSearch" Text="查询" Style="cursor: pointer;" OnClick="btnSearch_Click" runat="server" />
						</td>
						<td>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td style="height: 96%; width: 100%;">
				<table class="tab" style="vertical-align: top;">
					<tr style="vertical-align: top;">
						<td class="divFooter" style="text-align: left;">
							<asp:Button ID="btnEnsure" Text="确认入库" CssClass="btn4c" disabled="disabled" OnClick="btnEnsure_Click" runat="server" />
						</td>
					</tr>
					<tr style="vertical-align: top; height: 100%">
						<td>
							<div class="">
								<asp:GridView ID="gvwStorage" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" AllowPaging="true" OnRowDataBound="gvwStorage_RowDataBound" OnPageIndexChanging="gvwStorage_PageIndexChanging" DataKeyNames="scode,isfirst" runat="server"><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="入库编号"><ItemTemplate>
												<span class="al" onclick="viewopen('StorageView.aspx?&ic=<%# Eval("sid") %>')">
													<%# Eval("scode") %>
												</span>
											</ItemTemplate></asp:TemplateField><asp:BoundField DataField="tname" HeaderText="仓库名称" /><asp:BoundField DataField="person" HeaderText="录入人" /><asp:BoundField DataField="intime" HeaderText="录入时间" /><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
												<%# Common2.GetState(Eval("flowstate").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="甲供"><ItemTemplate>
												<%# (Eval("isfirst").ToString() == "True") ? "是" : "否" %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
												<%# GetAnnx(System.Convert.ToString(Eval("sid"))) %>
											</ItemTemplate></asp:TemplateField><asp:BoundField DataField="explain" HeaderText="说明" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	
	<asp:HiddenField ID="hfldStorageCode" runat="server" />
	
	<asp:HiddenField ID="hfldIsFirst" runat="server" />
	<div id="divFram" title="" style="display: none">
		<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	</form>
</body>
</html>
