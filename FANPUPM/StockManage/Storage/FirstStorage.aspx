<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FirstStorage.aspx.cs" Inherits="StockManage_Storage_FirstStorage" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>甲供入库</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="../script/Config2.js"></script>
	<script type="text/javascript" src="../script/JustWinTable.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			addEvent(document.getElementById('btnAdd'), 'click', addFirstStorage);
			addEvent(document.getElementById('btnUpdate'), 'click', updateStorage);
			addEvent(document.getElementById('btnDelete'), 'click', deleteStorage);
			addEvent(document.getElementById('btnQuery'), 'click', queryStorage);

			var storageTable = new JustWinTable('gvwStorage');
			setButton(storageTable, 'btnDelete', 'btnUpdate', 'btnQuery', 'hfldStorageCode');
			setWfButtonState2(storageTable, 'WF1');
		});

		function addFirstStorage() {
			parent.desktop.FirstStorageEdit = window;
			var url = '/StockManage/Storage/FirstStorageEdit.aspx?Action=Add';
			toolbox_oncommand(url, "新增甲供入库");
		}

		function updateStorage() {
			parent.desktop.FirstStorageEdit = window;
			var url = '/StockManage/Storage/FirstStorageEdit.aspx?Action=Update&StorageCode=' + encodeURI(document.getElementById('hfldStorageCode').value);
			toolbox_oncommand(url, "新增甲供入库");
		}

		function deleteStorage() {
			if (!confirm("确定要删除吗？")) {
				return false;
			}
		}

		function queryStorage() {
			viewopen('StorageView.aspx?ic=' + this.guid, 820, 500);
		}

		function openPerson() {
			winopen("/CommonWindow/PickSinglePerson.aspx?sm");
		}

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
							<asp:TextBox ID="txtTrea" CssClass="select_input" imgclick="selectTrea" runat="server"></asp:TextBox>
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
							<input type="button" id="btnAdd" value="新增" />
							<input type="button" id="btnUpdate" value="编辑" disabled="disabled" />
							<asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
							<input type="button" id="btnQuery" disabled="disabled" value="查看" />
							<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="074" BusiClass="001" runat="server" />
						</td>
					</tr>
					<tr style="height: 100%; vertical-align: top">
						<td>
							<div class="">
								<asp:GridView ID="gvwStorage" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" OnRowDataBound="gvwStorage_RowDataBound" DataKeyNames="scode,sid,project" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
												<asp:CheckBox ID="chkSelectAll" runat="server" />
											</HeaderTemplate><ItemTemplate>
												<asp:CheckBox ID="chkSelectSingle" runat="server" />
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="入库编号"><ItemTemplate>
												<span class="al" onclick="viewopen('/StockManage/Storage/StorageView.aspx?ic=<%# Eval("sid") %>',820,500)">
													<%# Eval("scode") %>
												</span>
											</ItemTemplate></asp:TemplateField><asp:BoundField DataField="tname" HeaderText="仓库名称" /><asp:BoundField DataField="person" HeaderText="录入人" /><asp:BoundField DataField="trustee" HeaderText="移交人" ItemStyle-Width="70px" /><asp:BoundField DataField="supervisor" HeaderText="监理" ItemStyle-Width="70px" /><asp:BoundField DataField="intime" HeaderText="录入时间" /><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
												<%# Common2.GetState(Eval("flowstate").ToString()) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px"><ItemTemplate>
												<%# GetAnnx(System.Convert.ToString(Eval("sid"))) %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="说明"><ItemTemplate>
												<%# StringUtility.GetStr(Eval("explain").ToString()) %>
											</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hfldStorageCode" runat="server" />
	<div id="divFram" title="" style="display: none">
		<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	</form>
</body>
</html>
