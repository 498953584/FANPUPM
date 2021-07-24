<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectByNoteList.aspx.cs" Inherits="StockManage_SmPurchaseplan_SelectByNoteList" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>从申请单中选择物资</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$('#btnSearch')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
			var gvNeed = new JustWinTable('gvNeed');
			showTooltip('tooltip', 5);
			if (getRequestParam("Type") == 'check') {
				document.getElementById('btnSave').style.display = 'none';
				document.getElementById('btnCancel').style.display = 'none';
			}
			//            document.getElementById('hdwscode').value = window.parent.document.getElementById('hdwscodeP').value;
//			alert()
			if (document.getElementById('hdc').value == "1") {
				document.getElementById('btnShow').click();
			}

			gvNeed.registClickTrListener(trClickEvent);
			$('#gvNeed a').click(function () {
				$("#hdPcode").val("1");
			});
			$('#gvNeed input').click(function () {
				$("#hdPcode").val("0");
			});
		});

		function trClickEvent() {
			var chk;
			var inputs = this.getElementsByTagName('INPUT');
			for (var i = 0; i < inputs.length; i++) {
				if (inputs[i].getAttribute('type') == 'checkbox') {
					chk = inputs[i];
				}
			}
			if (chk) {
				chk.click();
			}
		}

		function SetPId(pid) {
			parent.parent.document.getElementById("hdwzId").value = pid;
		}

		// 保存
		function saveEvent() {
			if (typeof top.ui.callback == 'function') {
				top.ui.callback({ id: $('#hdVal').val(), code: $("#hdlfWPcodes").val() })
				top.ui.callback = null;
			}
			top.ui.closeWin();

			return;
			var hdwzId = parent.parent.document.getElementById("hdwzId").value;
			if (hdwzId == "")
				hdwzId += document.getElementById("hdVal").value;
			else
				hdwzId += "," + document.getElementById("hdVal").value;
			$(parent.parent.document).find('.ui-icon-closethick')[0].click();
			parent.parent.document.getElementById("hdwzId").value = hdwzId;
			parent.parent.document.getElementById("hdlfWantplanCodes").value = $("#hdlfWPcodes").val();
			parent.parent.document.getElementById("btnShowGv").click();
		}
        
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="550px" style="border: solid 0px red;
		overflow: scroll;">
		<tr style="display: none;">
			<td class="headerrow" style="height: 20px;">
				<asp:Label ID="lblTitle" Text="" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr style="height: 25px;">
						<td class="descTd">
							起始时间
						</td>
						<td>
							<asp:TextBox ID="txtStartTime" Width="110px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							结束时间
						</td>
						<td>
							<asp:TextBox ID="txtEndTime" Width="110px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr style="height: 25px;">
						<td class="descTd">
							计划编号
						</td>
						<td>
							<asp:TextBox ID="txtSwcode" Width="110px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
						</td>
						<td colspan="2">
							<asp:Button ID="btnSearch" Text="查询" Style="cursor: pointer;" OnClick="btnSearch_Click" runat="server" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr style="width: 100%; height: 150px; vertical-align: top;">
			<td>
				<div class="pagediv" style="height: 160px;">
					<asp:GridView ID="gvNeed" Width="100%" AutoGenerateColumns="false" AllowPaging="true" CssClass="gvdata" OnPageIndexChanging="gvNeed_PageIndexChanging" runat="server"><Columns><asp:TemplateField>
<HeaderTemplate>
									<asp:CheckBox ID="chkAll" AutoPostBack="true" OnCheckedChanged="chkBox_CheckedChanged" runat="server" />
								</HeaderTemplate>

<ItemTemplate>
									<asp:CheckBox ID="chkBox" AutoPostBack="true" ToolTip='<%# Convert.ToString(Eval("swcode")) %>' OnCheckedChanged="chkBox_CheckedChanged" runat="server" />
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="计划编号" ItemStyle-Width="50px"><ItemTemplate>
									<%# Eval("swcode") %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="是否受理" ItemStyle-Width="50px"><ItemTemplate>
									<%# Common2.GetAcceptState(Eval("acceptstate").ToString()) %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入时间" ItemStyle-Width="50px"><ItemTemplate>
									<%# GetTime(Eval("intime").ToString()) %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入人" ItemStyle-Width="50px"><ItemTemplate>
									<%# Eval("person") %>
								</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				</div>
			</td>
		</tr>
		<tr style="width: 100%; height: 47.2%; vertical-align: top;">
			<td style="vertical-align: top;">
				<div class="pagediv" style="height: 200px;">
					<asp:GridView ID="gvNeedNote" Width="100%" AutoGenerateColumns="false" CssClass="gvdata" OnPageIndexChanging="gvNeedNote_PageIndexChanging" runat="server"><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="物资编号"><ItemTemplate>
									<%# Eval("scode") %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资名称"><ItemTemplate>
									<span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
										<%# StringUtility.GetStr(Eval("ResourceName").ToString(), 5) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格"><ItemTemplate>
									<span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
										<%# StringUtility.GetStr(Eval("Specification").ToString(), 5) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="型号"><ItemTemplate>
									<span class="tooltip" title='<%# Eval("ModelNumber").ToString() %>'>
										<%# StringUtility.GetStr(Eval("ModelNumber").ToString(), 5) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="品牌"><ItemTemplate>
									<span class="tooltip" title='<%# Eval("brand").ToString() %>'>
										<%# StringUtility.GetStr(Eval("brand").ToString(), 5) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="技术参数"><ItemTemplate>
									<span class="tooltip" title='<%# Eval("TechnicalParameter").ToString() %>'>
										<%# StringUtility.GetStr(Eval("TechnicalParameter").ToString(), 5) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位">
<ItemTemplate>
									<%# Eval("Name") %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="数量"><ItemTemplate>
									<%# Eval("number") %>
								</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				</div>
			</td>
		</tr>
		<tr>
			<td class="bottonrow1" style="height: 20px; text-align: right;">
				<input type="button" id="btnSave" onclick="saveEvent()" value="确定" />
				<input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
				<asp:Button ID="btnShow" Style="display: none;" Text="显示" OnClick="btnShow_Click" runat="server" />
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hdVal" runat="server" />
	<asp:HiddenField ID="hdwscode" runat="server" />
	
	<asp:HiddenField ID="hdlfWPcodes" runat="server" />
	<asp:HiddenField ID="hdc" Value="0" runat="server" />
	<asp:HiddenField ID="hdPcode" Value="0" runat="server" />
	</form>
</body>
</html>
