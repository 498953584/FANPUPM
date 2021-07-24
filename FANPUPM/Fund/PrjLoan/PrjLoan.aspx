<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjLoan.aspx.cs" Inherits="PrjLoan" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>资金管理-项目借款</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="/UserControl/FileUpload/GetFiles.js"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			setWidthHeight();
			var jwTable = new JustWinTable('gvBudget');
			setButton(jwTable, 'btnDel', 'btnEdit', 'btnQuery', 'hdnLoanID');
			if (!jwTable.table) return;
			setWfButtonState2(jwTable, 'WF1');
			deleteClick();
			replaceEmptyTable('emptyContractType', 'gvBudget');
			displayLookAdjuncts();
		});
		//删除前提示
		function deleteClick() {
			var btnDelete = document.getElementById('btnDel');
			btnDelete.onclick = function () {
				if (!window.confirm('系统提示：\n\n确定要删除吗？')) {
					return false;
				}
			}
		}
		function setWidthHeight() {
			$('#divBudget').height($(this).height() - 25);
			$('#div_project').height($(this).height() - 20);
		}
		//点击行赋值
		function clickRows(temAID, temtr) {
			if (temAID != "") {
				$("#hdnLoanID").val(temAID);
			}
		}
		//编辑
		function btnEdit_onclick() {
			top.ui._prjloan = window;
			var titleText = '编辑项目借款';
			var accid = $("#hdnLoanID").val();
			var _PrjId = $("#hfldaccountId").val();
			var prjname = $("#hfPrjName").val();
			if (accid != "") {
				top.ui._prjloan = window;
				url = "/Fund/PrjLoan/PrjLoanEdit.aspx?Action=edit&ic=" + accid + "&accountId=" + _PrjId + "&PrjCode=" + _PrjId + "&PrjName=" + escape(prjname);
				toolbox_oncommand(url, titleText);
			}
		}
		//查看
		function btnQuery_onclick() {
			var titleText = '查看借款信息';
			var accid = $("#hdnLoanID").val();
			var _PrjId = $("#hfldaccountId").val();
			var prjname = $("#hfPrjName").val();
			if (accid != "") {
				url = "/Fund/PrjLoan/PrjLoanView.aspx?Action=query&ic=" + accid + "&accountId=" + _PrjId + "&PrjCode=" + _PrjId + "&PrjName=" + escape(prjname);
				toolbox_oncommand(url, titleText);
			}
		}

		function shoView(ID) {
			parent.parent.desktop.flowclass = window;
			var titleText = '查看借款信息';
			var accid = $("#hdnLoanID").val();
			var _PrjId = $("#hfldaccountId").val();
			var prjname = $("#hfPrjName").val();
			if (accid != "") {
				url = "/Fund/PrjLoan/PrjLoanView.aspx?Action=query&ic=" + ID + "&PrjCode=" + _PrjId + "&PrjName=" + escape(prjname);
				toolbox_oncommand(url, titleText);
			}
		}
		//新增
		function btnAdd_onclick() {
			parent.parent.desktop.flowclass = window;
			var titleText = '新增项目借款';
			var _PrjId = $("#hfldaccountId").val();
			var prjname = $("#hfPrjName").val();
			if (_PrjId != "") {
				top.ui._prjloan = window;
				url = "/Fund/PrjLoan/PrjLoanEdit.aspx?Action=add&accountid=" + _PrjId + "&PrjName=" + escape(prjname);
				toolbox_oncommand(url, titleText);
			}
		}

		//查看附件
		function queryAdjunct(id) {
			var path = $('#hfldAdjunctPath').val() + id;

			//window.parent.showFiles(path, 'divOpenAdjunct');
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
			var rows = $("#gvBudget").find("tr");
			if (rows.length > 0) {
				for (i = 1; i < rows.length; i++) {
					var id = rows.eq(i).attr("id");
					var path = $('#hfldAdjunctPath').val() + '/' + id;
					var showCount = getFilesCount(path);
					if (showCount == 0) {
						rows.eq(i).find("td").eq(12).text("");
					}
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
					count = data.length;
				}
			});
			return count;
		}  
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divOpenAdjunct">
	</div>
	<asp:HiddenField ID="hfldAdjunctPath" runat="server" />
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
		<table style="width: 100%; height: 100%;">
			<tr>
				<td style="width: 100%; height: 100%;">
					<table style="width: 100%; height: 100%;">
						<tr>
							<td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
								<table class="tab" style="vertical-align: top;">
									<tr>
										<td class="divFooter" style="text-align: left;">
											<input type="button" value="新增" id="btnAdd" onclick="return btnAdd_onclick()" runat="server" />

											<input id="btnEdit" type="button" disabled="disabled" value="编辑" onclick="return btnEdit_onclick()" />
											<asp:Button ID="btnDel" Text="删除" Enabled="false" OnClick="btnDel_Click" runat="server" />
											<input type="button" id="btnQuery" disabled="disabled" value="查看" onclick="return btnQuery_onclick()" />
											<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiClass="001" BusiCode="095" runat="server" />
										</td>
									</tr>
									<tr>
										<td style="vertical-align: top; height: 100%;">
											<div id="divBudget" style="overflow: auto;">
												<div id="divDiaries" style="overflow: auto;">
													<asp:GridView ID="gvBudget" CssClass="gvdata" AutoGenerateColumns="false" AllowPaging="true" OnRowDataBound="gvBudget_RowDataBound" OnPageIndexChanging="gvBudget_PageIndexChanging" DataKeyNames="LoanID" runat="server">
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
																		项目
																	</th>
																	<th scope="col">
																		借款日期
																	</th>
																	<th scope="col">
																		借款人
																	</th>
																	<th scope="col">
																		借款用途
																	</th>
																	<th scope="col">
																		借款金额
																	</th>
																	<th scope="col">
																		借款利率
																	</th>
																	<th scope="col">
																		约定归还日期
																	</th>
																	<th scope="col">
																		流程状态
																	</th>
																	<th scope="col">
																		备注
																	</th>
																	<th scope="col">
																		附件
																	</th>
																</tr>
															</table>
														</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
																	<asp:CheckBox ID="chkAll" runat="server" />
																</HeaderTemplate><ItemTemplate>
																	<asp:CheckBox ID="chk" runat="server" />
																</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
																	<%# Container.DataItemIndex + 1 %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目"><ItemTemplate>
																	<%# Eval("PrjName") %>
																</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="借款单号"><ItemTemplate>
																	<asp:Label ID="labLoanCode" CssClass="link" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LoanCode")) %>' runat="server"></asp:Label>
																</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="借款用途"><ItemTemplate>
																	<span class="tooltip" title="<%# Eval("LoanCause") %>">
																		<%# StringUtility.GetStr(Eval("LoanCause").ToString(), 25) %>
																	</span>
																</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="借款日期" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
																	<%# DataBinder.Eval(Container.DataItem, "LoanDate", "{0:yyyy-MM-dd}") %>
																</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="借款人"><ItemTemplate>
																	<%# Eval("LoanManName") %>
																</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="借款金额" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
																	<%# DataBinder.Eval(Container.DataItem, "LoanFund", "{0:F3}") %>
																</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="借款利率" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
																	<%# Math.Round(Convert.ToDecimal(Eval("LoanRate")) * 100m, 2).ToString() %>%
																</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="约定归还日期" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
																	<%# DataBinder.Eval(Container.DataItem, "PlanReturnDate", "{0:yyyy-MM-dd}") %>
																</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态" HeaderStyle-Width="80px">
<ItemTemplate>
																	<%# Common2.GetState(Eval("FlowState").ToString()) %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
																	<span class="tooltip" title="<%# Eval("Remark") %>">
																		<%# StringUtility.GetStr(Eval("Remark").ToString(), 25) %>
																	</span>
																</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px"><ItemTemplate>
																	<span class="link" style="display: block; width: 10px;" onclick="queryAdjunct('<%# Eval("LoanID") %>')">
																		<img src='/images1/icon_att0b3dfa.gif' style='cursor: pointer; border-style: none' />
																	</span>
																</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
	<!-- 当前项目的GUID -->
	<asp:HiddenField ID="hfldaccountId" runat="server" />
	<!-- 选择当前对象的ID -->
	<asp:HiddenField ID="hdnLoanID" runat="server" />
	<!-- 当前项目 -->
	<asp:HiddenField ID="hfPrjName" runat="server" />
	</form>
</body>
</html>
