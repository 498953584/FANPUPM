<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryVersion.aspx.cs" Inherits="ProgressManagement_Modify_QueryVersion" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>计划版本查询</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var jwTable = new JustWinTable('gvwPlans');
			setButton(jwTable);
			setWidthHeight();
			showTooltip("tooltip", 25);
			$('#btnLook').bind('click', function () {
				var id = $('#hfldChecked').val();
				$('#' + id).find('a').click();
			});
			replaceEmptyTable('tblEmpty', 'gvwPlans');
		});

		function setWidthHeight() {
			$('#divBudget').height($(this).height());
			$('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 7);
			$('#div_project').height($(this).height() - 20);
		}

		//查看计划
		function lookApply(verId, verName, state) {
			var url = '/ProgressManagement/Modify/ApplyView.aspx?ic=' + verId + '&state=' + state;
			var titleText = verName + '-查看计划';
			toolbox_oncommand(url, titleText);
		}

		//复选框选择
		function setButton(jwTable) {
			if (!jwTable.table) return;
			if (jwTable.table.firstChild.childNodes.length == 1) {
				//table中没有数据
				return;
			}

			jwTable.registClickTrListener(function () {
				$('#btnLook').removeAttr('disabled');
				$('#hfldChecked').val(this.id);
			});
		};
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
		<table>
			<tr>
				<td style="width: 100%; height: 100%;">
					<table style="width: 100%; height: 100%;">
						<tr>
							<td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
								<table style="width: auto;" cellpadding="3" cellspacing="3">
									<tr>
										<td>
											计划名称
										</td>
										<td>
											<asp:TextBox ID="txtName" runat="server"></asp:TextBox>
										</td>
										<td>
											版本号
										</td>
										<td>
											<asp:TextBox ID="txtVersion" runat="server"></asp:TextBox>
										</td>
									</tr>
									<tr>
										<td>
											申请人
										</td>
										<td>
											<asp:TextBox ID="txtModifyUserName" runat="server"></asp:TextBox>
										</td>
										<td>
											<asp:CheckBox ID="chkLatest" Text="执行版本" runat="server" />
										</td>
										<td>
											<asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
										</td>
									</tr>
								</table>
								<table class="tab">
									<tr class="divFooter" style="text-align: left;">
										<td>
											<input type="button" id="btnLook" value="查看进度" style="width: auto" disabled="disabled" />
										</td>
									</tr>
									<tr>
										<td style="vertical-align: top;" colspan="3">
											<div id="divBudget" class="pagediv" style="overflow: hidden;">
												<asp:GridView ID="gvwPlans" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwPlans_RowDataBound" DataKeyNames="ProgressVersionId" runat="server">
<EmptyDataTemplate>
														<table id="tblEmpty">
															<tr class="header">
																<td style="width: 20px;">
																	<input type="checkbox" />
																</td>
																<th scope="col" style="width: 20px;">
																	序号 </td>
																	<th scope="col">
																		计划名称
																	</th>
																	<th scope="col">
																		版本号
																	</th>
																	<th scope="col">
																		执行
																	</th>
																	<th scope="col">
																		主计划
																	</th>
																	<th scope="col">
																		原计划名称
																	</th>
																	<th scope="col">
																		原版本号
																	</th>
																	<th scope="col">
																		申请人
																	</th>
																	<th scope="col">
																		录入日期
																	</th>
															</tr>
														</table>
													</EmptyDataTemplate>
<Columns><asp:BoundField HeaderText="序号" DataField="No" HeaderStyle-Width="20px" /><asp:TemplateField HeaderText="计划名称" HeaderStyle-Width="320px"><ItemTemplate>
																<a class="link tooltip" title='<%# Eval("VersionName") %>' onclick="lookApply('<%# Eval("ProgressVersionId") %>','<%# StringUtility.GetStr(Eval("VersionName"), 10) %>','<%# Eval("MFlowState") %>')">
																	<%# StringUtility.GetStr(Eval("VersionName"), 25) %></a>
															</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="版本号" DataField="VersionCode" /><asp:BoundField HeaderText="执行" DataField="Latest" HeaderStyle-Width="30px" /><asp:BoundField HeaderText="主计划" DataField="Main" HeaderStyle-Width="30px" /><asp:TemplateField HeaderText="原计划名称" HeaderStyle-Width="320px"><ItemTemplate>
																<a class="link tooltip" title='<%# Eval("PVersionName") %>' onclick="lookApply('<%# Eval("ParentVerId") %>','<%# StringUtility.GetStr(Eval("PVersionName"), 10) %>','')">
																	<%# StringUtility.GetStr(Eval("PVersionName"), 25) %></a>
															</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="原版本号" DataField="PVersionCode" /><asp:BoundField HeaderText="申请人" DataField="UserName" /><asp:BoundField HeaderText="录入日期" HeaderStyle-Width="80px" DataField="InputDate" DataFormatString="{0:yyyy-MM-dd}" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
												<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
												</webdiyer:AspNetPager>
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
	<asp:HiddenField ID="hfldChecked" runat="server" />
	</form>
</body>
</html>
