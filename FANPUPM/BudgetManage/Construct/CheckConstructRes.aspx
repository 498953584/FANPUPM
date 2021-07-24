<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckConstructRes.aspx.cs" Inherits="BudgetManage_Construct_CheckConstructRes" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>施工报量</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="../../Script/lightbox/jquery.lightbox-0.5.css" media="screen" />

	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<script src="/Script/jquery.js" type="text/javascript"></script>
	<script type="text/javascript" src="../../Script/Budget/BudgetPait.js"></script>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/lightbox/jquery.lightbox-0.5.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$('#btn_Search')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
			var gvConstruct = new JustWinTable('gvConstruct');
			setWidthAndHeight();
			showTooltip('tooltip', 9);
			clickTree('tvBudget', 'hfldPrjId');
			// 页面加载时从cookie取滚动条位置信息，然后附值给滚动条
			//            $('#div_project').scrollTop(getCookie('scrollTop'));
		});

		//        //页面刷新前保存滚动条位置信息到cookie
		//        window.onbeforeunload = function () {
		//            var scrollPos;
		//            scrollPos = document.getElementById('div_project').scrollTop;
		//            document.cookie = "scrollTop=" + scrollPos;
		//        }
		//设置div高度和宽度
		function setWidthAndHeight() {
			$('#divBudget').height($(this).height() - $('#divTop').height() - $('#divTop2').height() - 2);
			$('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 8);
			$('#div_project').height($(this).height() - $('#ddlYear').height() - 4);
		}
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
								<div id="div1" class="divContent2" style="width: 100%; height: 99%; overflow: hidden;">
									<table style="width: 100%; height: 88%;">
										<tr id="divTop">
											<td>
												<table class="queryTable" cellpadding="3px" cellspacing="0px">
													<tr>
														<td class="descTd">
															任务编码
														</td>
														<td>
															<asp:TextBox ID="txtTaskCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
														</td>
														<td class="descTd">
															任务名称
														</td>
														<td>
															<asp:TextBox ID="txtTaskName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
														</td>
													</tr>
													<tr>
														<td class="descTd">
															资源编号
														</td>
														<td>
															<asp:TextBox ID="txtReourceCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
														</td>
														<td class="descTd">
															资源名称
														</td>
														<td>
															<asp:TextBox ID="txtReourceName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
														</td>
														<td>
															<asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr id="divTop2">
											<td class="divFooter" style="text-align: left; width: 100%;">
												<asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
											</td>
										</tr>
										<tr>
											<td style="width: 100%; height: 100%;">
												<div id="divBudget" class="pagediv" style="border-bottom: solid 0px red;">
													<asp:GridView ID="gvConstruct" Width="100%" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvConstruct_RowDataBound" DataKeyNames="TaskCode" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="50px">
<HeaderTemplate>
																	序号
																</HeaderTemplate>

<ItemTemplate>
																	<%# Eval("Num") %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="200px">
<HeaderTemplate>
																	任务编码
																</HeaderTemplate>

<ItemTemplate>
																	<%# Eval("TaskCode") %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="200px">
<HeaderTemplate>
																	任务名称
																</HeaderTemplate>

<ItemTemplate>
																	<asp:HyperLink ID="hlinkShowTaskName" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(Eval("TaskName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(Eval("TaskName").ToString(), 9) %></asp:HyperLink>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
																	资源编号
																</HeaderTemplate>

<ItemTemplate>
																	<%# Eval("ResourceCode") %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
																	资源名称
																</HeaderTemplate>

<ItemTemplate>
																	<asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(Eval("ResourceName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(Eval("ResourceName").ToString(), 9) %></asp:HyperLink>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="100px">
<HeaderTemplate>
																	单位
																</HeaderTemplate>

<ItemTemplate>
																	<%# Eval("UnitName") %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="100px">
<HeaderTemplate>
																	规格
																</HeaderTemplate>

<ItemTemplate>
																	<asp:HyperLink ID="hlinkShowSpecification" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(Eval("Specification").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(Eval("Specification").ToString(), 9) %></asp:HyperLink>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="100px">
<HeaderTemplate>
																	单价
																</HeaderTemplate>

<ItemTemplate>
																	<%# Eval("UnitPrice") %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="100px">
<HeaderTemplate>
																	数量
																</HeaderTemplate>

<ItemTemplate>
																	<%# Eval("AccountingQuantity") %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="100px">
<HeaderTemplate>
																	小计
																</HeaderTemplate>

<ItemTemplate>
																	<%# Eval("Total") %>
																</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
													<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
													</webdiyer:AspNetPager>
												</div>
											</td>
										</tr>
									</table>
								</div>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	<asp:HiddenField ID="hfldYear" runat="server" />
	</form>
</body>
</html>
