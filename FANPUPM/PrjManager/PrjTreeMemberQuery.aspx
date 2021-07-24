<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="PrjTreeMemberQuery.aspx.cs" Inherits="PrjManager_PrjTreeMemberQuery" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>项目小组成员查询</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../UserControl/FileUpload/GetFiles.js"></script>
	<script type="text/ecmascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../Script/jw.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 添加验证
			$('#btnQueryInfo')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
			replaceEmptyTable('emptyPrjMembers', 'gvwPrjMembers');
			var table = new JustWinTable('gvwPrjMembers');
			setWidthHeight();
			displayLookAdjuncts();
			showTooltip('tooltip', 25);
		});

		function setWidthHeight() {
			$('#div_project').height($(this).height() - 2);
			$('#tbMemberInfo').width($(this).width() - $('#div_project').width() - 8);
			$('#dvData').width($(this).width() - $('#div_project').width() - 8);
			$('#dvData').height($(this).height() - $('#divQueryTable').height() - 8);
		}

		// 查看附件
		function queryAdjunct(id) {
			var path = $('#hfldAdjunctPath').val() + '/' + id;
			showFiles(path, 'divOpenAdjunct');
		}

		// 是否显示附件显示
		function displayLookAdjuncts() {
			$('#gvwPrjMembers TR').each(function (i) {
				var id = $(this).attr('id');
				var imgLink = "<SPAN class=link onclick=\"queryAdjunct('" + id + "')\"><IMG style='BORDER-BOTTOM-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-TOP-STYLE: none; BORDER-LEFT-STYLE: none; CURSOR: pointer' src='/images1/icon_att0b3dfa.gif'> </SPAN>";
				var path = $('#hfldAdjunctPath').val() + '/' + id;
				var showCount = getFilesCount(path);
				if (showCount == 0)
					$(this).find('TD').eq(8).html('');
				else
					$(this).find('TD').eq(8).html(imgLink);

			});
		}

		// 获得附件个数
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
<body>
	<form id="form1" runat="server">
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
		<table>
			<tr>
				<td>
					<table>
						<tr>
							
							<td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
								<table id="tbMemberInfo" width="100%" cellpadding="0" cellspacing="0">
									<tr>
										<td>
											<table id="divQueryTable" class="queryTable" cellpadding="3px" cellspacing="0px">
												<tr>
													<td class="descTd">
														姓名
													</td>
													<td>
														<asp:TextBox ID="txtName" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
													</td>
													<td class="descTd">
														岗位
													</td>
													<td>
														<asp:TextBox ID="txtPost" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
													</td>
													<td class="descTd">
														学历及专业
													</td>
													<td>
														<asp:TextBox ID="txtEducationalBackground" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
													</td>
												</tr>
												<tr>
													<td class="descTd">
														职称
													</td>
													<td>
														<asp:TextBox ID="txtTechnical" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
													</td>
													<td colspan="2">
														<asp:Button ID="btnQueryInfo" Text="查询" OnClick="btnQueryInfo_Click" runat="server" />
													</td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td>
											<div id="dvData" style="overflow: auto;">
												<asp:GridView ID="gvwPrjMembers" AutoGenerateColumns="false" ShowHeader="true" CssClass="gvdata" OnRowDataBound="gvwPrjMembers_RowDataBound" DataKeyNames="PrjMemberId" runat="server">
<EmptyDataTemplate>
														<table class="gvdata" cellspacing="0" id="emptyPrjMembers" rules="all" border="1"
															style="width: 100%; border-collapse: collapse;">
															<tr>
																<th class="header">
																	序号
																</th>
																<th class="header">
																	成员姓名
																</th>
																<th class="header">
																	岗位
																</th>
																<th class="header">
																	学历及专业
																</th>
																<th class="header">
																	项目编号
																</th>
																<th class="header">
																	职称
																</th>
																<th class="header">
																	资格证书
																</th>
																<th class="header">
																	上岗培训情况
																</th>
																<th class="header">
																	以往工作表现
																</th>
																<th class="header">
																	附件
																</th>
															</tr>
														</table>
													</EmptyDataTemplate>
<Columns><asp:BoundField HeaderText="序号" DataField="No" HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="姓名" DataField="MemberName" /><asp:TemplateField HeaderText="岗位"><ItemTemplate>
																<a class="tooltip" style="text-decoration: none; color: Black;" title='<%# Eval("Post") %>'>
																	<%# StringUtility.GetStr(Eval("Post").ToString(), 25) %>
																</a>
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="学历及专业"><ItemTemplate>
																<a class="tooltip" style="text-decoration: none; color: Black;" title='<%# Eval("EducationalBackground") %>'>
																	<%# StringUtility.GetStr(Eval("EducationalBackground").ToString(), 25) %>
																</a>
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="职称"><ItemTemplate>
																<a class="tooltip" style="text-decoration: none; color: Black;" title='<%# Eval("Technical") %>'>
																	<%# StringUtility.GetStr(Eval("Technical").ToString(), 25) %>
																</a>
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资格证书"><ItemTemplate>
																<a class="tooltip" style="text-decoration: none; color: Black;" title='<%# Eval("PostAndCompetency") %>'>
																	<%# StringUtility.GetStr(Eval("PostAndCompetency").ToString(), 25) %>
																</a>
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="上岗培训情况"><ItemTemplate>
																<a class="tooltip" style="text-decoration: none; color: Black;" title='<%# Eval("PastPerformance") %>'>
																	<%# StringUtility.GetStr(Eval("TrainingInformation").ToString(), 25) %>
																</a>
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="以往工作表现"><ItemTemplate>
																<a class="tooltip" style="text-decoration: none; color: Black;" title='<%# Eval("PastPerformance") %>'>
																	<%# StringUtility.GetStr(Eval("PastPerformance").ToString(), 25) %>
																</a>
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" HeaderStyle-Width="50px">
<ItemTemplate>
															</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
												<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
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
	<div id="divOpenAdjunct" title="查看附件" style="display: none;">
		<table id="annexTable" class="gvdata" style="width: 100%;" runat="server"><tr class="header" runat="server"><td style="width: 60%" runat="server">
						名称
					</td><td style="width: 30%" runat="server">
						大小
					</td><td runat="server">
					</td></tr></table>
	</div>
	<asp:HiddenField ID="hfldAdjunctPath" runat="server" />
	</form>
</body>
</html>
