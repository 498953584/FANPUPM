<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthPlanView.aspx.cs" Inherits="Fund_MonthPlan_MonthPlanView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_querydatectrl_ascx" Src="~/UserControl/QueryDateCtrl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>资金计划编制</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
	<script type="text/javascript" src="/Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/UserControl/FileUpload/GetFiles.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript">
		$(function () {
			var contractTable = new JustWinTable('gvwWebLineList');
			$("#showaudit1_chkAudit").click(function () {
				var tem = $("#showaudit1_chkAudit").attr("checked");
				if (tem) {
					$("#showaudit1_table").removeClass("noprint");
				} else {
					$("#showaudit1_table").addClass("noprint");
				}
			});
			sumAll();
			// displayLookAdjuncts();
		});
		function sumAll() {
			var _thisTr_Last = $("#gvwWebLineList").find("tr").last();
			$(_thisTr_Last).find("td").first().attr("colspan", "6");
			$(_thisTr_Last).find("td").first().after("<td align=\"right\"></td><td  align=\"right\"></td><td  align=\"right\"></td>");
			var coutTr = $("#gvwWebLineList").find("tr").size();
			var mycars = new Array(coutTr);
			var _EcS = 0.00;
			var _BcS = 0.00;
			$("#gvwWebLineList tr").each(function (i) {
			    var _thisTr = this;
			    var temcc = 0;
			    if ($(_thisTr).find("td").eq(6).html() != "" && $(_thisTr).find("td").eq(6).html() != null) {
			        temcc = $(_thisTr).find("td").eq(6).html();
			        _EcS += parseFloat(temcc);
			    }
			    if ($(_thisTr).find("td").eq(7).html() != "" && $(_thisTr).find("td").eq(7).html() != null) {
			        _BcS += parseFloat($(_thisTr).find("td").eq(7).html().replace(','));
			    }
			});
			$(_thisTr_Last).find("td:eq(1)").empty();
			$(_thisTr_Last).find("td:eq(1)").append(roundAmount(_EcS));
			$(_thisTr_Last).find("td:eq(2)").empty();
			$(_thisTr_Last).find("td:eq(2)").append(roundAmount(_BcS));
			if (_EcS > 0 && _BcS > 0) {
				$(_thisTr_Last).find("td:eq(3)").empty();
				$(_thisTr_Last).find("td:eq(3)").append(roundAmount((_BcS / _EcS) * 100) + "%");
			} else {
				$(_thisTr_Last).find("td:eq(3)").append("0.00%");
			}
		}
		function roundAmount(n) {
			var s = " " + Math.round(n * 100) / 100;
			var i = s.indexOf('. ');
			if (i < 0) {
				return s + ".000 ";
			}
			var t = s.substring(0, i + 1) + s.substring(i + 1, i + 3);
			if (i + 2 == s.length) {
				t += "0 "
				return t
			}
		}
		//查看附件
		function queryAdjunct(id) {
			var path = $('#hfldAdjunctPath').val() + id;
			showFiles(path, 'divOpenAdjunct');
		}

		//附件显示
		function displayLookAdjuncts() {
			var tab = document.getElementById('gvwWebLineList');
			if (tab.rows.length > 0) {
				for (i = 1; i < tab.rows.length; i++) {
					var id = tab.rows[i].id;
					var path = $('#hfldAdjunctPath').val() + id;
					var showCount = getFilesCount(path);
					if (showCount == 0)
						if (tab.rows[i].cells[11] != null) {
							tab.rows[i].cells[11].innerText = '';
						}
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
	<style type="text/css" media="print">
		.noprint
		{
			display: none;
		}
		table tr
		{
			border-color: Black;
			background-color: Black;
		}
		.fontsize
		{
			font-size: 13px;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		<tr id="trtoolbar2" runat="server"><td class="divFooter" style="text-align: right" runat="server">
				<asp:Button ID="leadingout" CssClass="noprint" Text="导出" Visible="false" OnClick="leadingout_Click" runat="server" />
				<input type="button" id="btnDy" class="noprint" onclick="winPrint()" value="打印" />
				<asp:Button ID="btnClose" CssClass="noprint" Text="关闭" Visible="false" OnClick="btnClose_Click" runat="server" />
			</td></tr>
		<tr id="header">
			<td>
				<asp:Label ID="lblTitle" Text="" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style: none;
					margin-left: 1px;" class="viewTable">
					<tr>
						<td colspan="3">
							项目：
							<asp:Literal ID="ltprjname" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td colspan="3">
							附件 ：<asp:Literal ID="Literal1" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td colspan="3">
							备注 ：<asp:Literal ID="Literal2" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td>
							流程状态：
							<asp:Literal ID="ltFlowState" runat="server"></asp:Literal>
						</td>
						<td>
							填 报 人：
							<asp:Literal ID="ltadduser" runat="server"></asp:Literal>
						</td>
						<td>
							填报日期：
							<asp:Literal ID="ltadddate" runat="server"></asp:Literal>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td style="vertical-align: top;">
				<table class="tab" style="vertical-align: top;">
					<tr>
						<td>
							
							<div style="width: 100%; height: 100%; border: solid 0px red; margin: 0px; padding: 0px;">
								<asp:GridView ID="gvwWebLineList" CssClass="gvdata" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvwModelList_RowDataBound" DataKeyNames="UID" runat="server">
<EmptyDataTemplate>
										<table id="emptyContractType" class="gvdata" width="100%">
											<tr class="header">
												<th scope="col" style="width: 25px;">
													序号
												</th>
												<th scope="col" style="width: 80px;">
													合同
												</th>
												<th scope="col" style="width: 80px;">
													乙方
												</th>
												<th scope="col" style="width: 80px;">
													合同总额
												</th>
												<th scope="col" style="width: 80px;">
													合同已结算额
												</th>
												<th scope="col" style="width: 80px;">
													合同已付总额
												</th>
												<th scope="col" style="width: 80px;">
													上期计划金额
												</th>
												<th scope="col" style="width: 80px;">
													上期实际发生金额
												</th>
												<th scope="col" style="width: 80px;">
													上期计划执行完成情况
												</th>
												<th scope="col" style="width: 80px;">
													本期计划金额
												</th>
												<th scope="col">
													备注
												</th>
												<th scope="col" style="width: 20px">
													附件
												</th>
											</tr>
										</table>
									</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
												<asp:HiddenField  ID="hidenUID" runat="server" />
												<asp:HiddenField  ID="hidenMonthPlanID" runat="server" />
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合同" HeaderStyle-Width="180px" ItemStyle-Width="170px">
<ItemTemplate>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="乙方" HeaderStyle-Width="110px">
<ItemTemplate>
												<%# GetParty(Eval("BName").ToString()) %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合同总额" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
												<%# DataBinder.Eval(Container.DataItem, "contMoney", "{0:F3}").Replace(",","") %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合同已结算额" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
												<%# DataBinder.Eval(Container.DataItem, "BalanceMoney", "{0:n}").Replace(",","") %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合同已付总额" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
												<%# DataBinder.Eval(Container.DataItem, "PaymentMoney", "{0:n}").Replace(",","") %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上期计划金额" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
												  
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上期实际发生金额" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
												<%# DataBinder.Eval(Container.DataItem, "ActuallyMoney", "{0:n}").Replace(",","") %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上期计划执行完成情况" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
												<asp:HiddenField  ID="hindePlansubjectText" runat="server" />
												<%# Eval("Plansubject") %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="本期计划金额" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
												<%# Eval("PlanMoney") %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注" FooterStyle-Wrap="true">
<ItemTemplate>
												<%# Eval("ReMark") %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"><ItemTemplate>
											</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							</div>
						</td>
					</tr>
				</table>
				<table style="width: 100%; height: 50%; margin-top: 10px;" border="0">
					<tr id="trAudit" style="vertical-align: top;">
						<td>
							<MyUserControl:stockmanage_common_showaudit_ascx ID="showaudit1" BusiClass="001" runat="server" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<div id="divOpenAdjunct" title="查看附件" style="display: none;">
		<table id="annexTable" class="gvdata" style="width: 100%;" runat="server"><tr class="header" runat="server"><td style="width: 60%" runat="server">
						名称
					</td><td style="width: 30%" runat="server">
						大小
					</td><td runat="server">
					</td></tr></table>
	</div>
	<asp:HiddenField ID="mpid" runat="server" />
	<asp:HiddenField ID="hfldAdjunctPath" runat="server" />
	</form>
</body>
</html>
