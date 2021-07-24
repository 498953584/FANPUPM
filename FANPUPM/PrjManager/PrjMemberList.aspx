<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="PrjMemberList.aspx.cs" Inherits="PrjManager_PrjMemberList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>项目小组成员</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../Script/jquery.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../UserControl/FileUpload/GetFiles.js"></script>
	<script type="text/ecmascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../Script/jw.js"></script>
	<script type="text/javascript" src="../Script/wf.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			replaceEmptyTable('emptyPrjMembers', 'gvwPrjMembers');
			var table = new JustWinTable('gvwPrjMembers');
			setButton(table, 'btnDel', 'btnSetAdjunct', 'hfldEmptyBtn', 'hfldCheckedIds');
			displayLookAdjuncts();
			showTooltip('tooltip', 25);
		});

		//设置小组成员
		function openSetPrjMemberTabPage() {
			top.ui._SetPrjMember = window;
			var id = getRequestParam('id');
			var url = '/PrjManager/SetPrjMember.aspx?' + new Date().getTime() + '&id=' + id;
			top.ui.openWin({ title: '设置项目小组成员', url: url, width: 650, height: 500, winNo: 1 });
		}

		//上传附件
		function addAdjunct() {
			top.ui._SetAdjunct = window;
			var url = '/PrjManager/SetAdjunct.aspx?' + new Date().getTime() + '&PrjMemberId=' + $('#hfldCheckedIds').val();
			top.ui.openWin({ title: '编辑信息', url: url, width: 610, height: 470, winNo: 1 });
		}

		//设置上传附件路径
		function setFilelUpPath() {
			var recordCode = $('#hfldCheckedIds').val();
			updateRecordCode('FileUploadAdjunct', recordCode);
		}

		//查看附件
		function queryAdjunct(id) {
			var path = $('#hfldAdjunctPath').val() + '/' + id;
			showFiles(path, 'divOpenAdjunct');
		}
		//是否显示附件显示
		function displayLookAdjuncts() {
			var tab = document.getElementById('gvwPrjMembers');
			if (tab.rows.length > 0) {
				for (i = 1; i < tab.rows.length; i++) {
					var id = tab.rows[i].id;
					var imgLink = "<SPAN class=link onclick=\"queryAdjunct('" + id + "')\"><IMG style='BORDER-BOTTOM-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-TOP-STYLE: none; BORDER-LEFT-STYLE: none; CURSOR: pointer' src='/images1/icon_att0b3dfa.gif'> </SPAN>";
					var path = $('#hfldAdjunctPath').val() + '/' + id;
					var showCount = getFilesCount(path);
				    if (showCount == 0)
						tab.rows[i].cells[9].innerText = '';
					else
						tab.rows[i].cells[9].innerHTML = imgLink;
				}
			}
		}
        //获得附件个数
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
      //关闭
		function closeTab() {
			winclose('PrjMemberList', 'PrjMember.aspx', true);
		}
</script>
</head>
<body style="">
	<form id="form1" runat="server">
	<table id="tabMember" width="100%" cellpadding="0" cellspacing="0">
		<tr>
			<td style="height: 5%;">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							姓名
						</td>
						<td>
							<asp:TextBox ID="txtName" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							岗位
						</td>
						<td>
							<asp:TextBox ID="txtPost" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							学历及专业
						</td>
						<td>
							<asp:TextBox ID="txtEducationalBackground" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							职称
						</td>
						<td>
							<asp:TextBox ID="txtTechnical" runat="server"></asp:TextBox>
						</td>
						<td colspan="2">
							<asp:Button ID="btnQueryInfo" Text="查询" OnClick="btnQueryInfo_Click" runat="server" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td class="divFooter" style="text-align: left; padding-left: 2px; height: 3%;">
				<input type="button" id="btnMember" value="新增" onclick="openSetPrjMemberTabPage()"
					style="width: auto;" />
				<asp:Button ID="btnDel" Text="删除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗？');" OnClick="btnDel_Click" runat="server" />
				<input type="button" id="btnSetAdjunct" value="编辑信息" style="width: auto;" disabled="disabled"
					onclick="addAdjunct()" />
			</td>
		</tr>
		<tr>
			<td style="height: 5%;">
				<div class="" id="divBudget">
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
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
									<asp:CheckBox ID="cbAllBox" runat="server" />
								</HeaderTemplate><ItemTemplate>
									<asp:CheckBox ID="cbBox" runat="server" />
								</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="序号" DataField="No" HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="姓名" DataField="MemberName" /><asp:TemplateField HeaderText="岗位"><ItemTemplate>
									<a class="tooltip" style="text-decoration: none; color: Black;" title='<%# Eval("Post") %>'>
										<%# StringUtility.GetStr(Eval("Post").ToString(), 25) %>
									</a>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="学历及专业"><ItemTemplate>
									<a class="tooltip" style="text-decoration: none; color: Black;" title='<%# Eval("EducationalBackground") %>'>
										<%# StringUtility.GetStr(Eval("EducationalBackground").ToString(), 25) %>
									</a>
								</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="职称" DataField="Technical" /><asp:TemplateField HeaderText="资格证书"><ItemTemplate>
									<a class="tooltip" style="text-decoration: none; color: Black;" title='<%# Eval("PostAndCompetency") %>'>
										<%# StringUtility.GetStr(Eval("PostAndCompetency").ToString(), 25) %>
									</a>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="上岗培训情况"><ItemTemplate>
									<a class="tooltip" style="text-decoration: none; color: Black;" title='<%# Eval("TrainingInformation") %>'>
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
	<div id="divSetAdjunct" title="编辑信息" onclick="addAdjunct()" style="display: none;">
		<table class="tableContent2" cellpadding="5" cellspacing="5">
			<tr>
				<td class="word">
					姓名
				</td>
				<td>
					<asp:TextBox ID="txtMName" Enabled="false" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					岗位
				</td>
				<td>
					<asp:TextBox ID="txtMPost" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					职称
				</td>
				<td>
					<asp:TextBox ID="txtMTechnical" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					学历及专业
				</td>
				<td>
					<asp:TextBox ID="txtMEducationalBackground" TextMode="MultiLine" Height="50px" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					资格证书
				</td>
				<td>
					<asp:TextBox ID="txtMPostAndCompetency" TextMode="MultiLine" Height="50px" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					上岗培训情况
				</td>
				<td>
					<asp:TextBox ID="txtMTrainingInformation" TextMode="MultiLine" Height="50px" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					以往工作表现
				</td>
				<td>
					<asp:TextBox ID="txtMPastPerformance" TextMode="MultiLine" Height="50px" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					相关附件
				</td>
				<td style="white-space: nowrap;">
					<MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUploadAdjunct" Name="附件" Class="ProjectFile" runat="server" />
				</td>
			</tr>
		</table>
		<asp:Button ID="btnUpdatePastPerformance" Text="更新工作表现" Style="display: none;" OnClick="btnUpdatePastPerformance_Click" runat="server" />
	</div>
	<div id="divOpenAdjunct" title="查看附件" style="display: none;">
		<table id="annexTable" class="gvdata" style="width: 100%;" runat="server"><tr class="header" runat="server"><td style="width: 60%" runat="server">
						名称
					</td><td style="width: 30%" runat="server">
						大小
					</td><td runat="server">
					</td></tr></table>
	</div>
	<div id="divFramRole" title="" style="display: none">
		<iframe id="iframeRole" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<asp:HiddenField ID="hfldCheckedIds" runat="server" />
	<asp:HiddenField ID="hfldEmptyBtn" runat="server" />
	<asp:HiddenField ID="hfldAdjunctPath" runat="server" />
	<!-- 放到此处解决与上传附件的用户控件样式冲突的问题-->
	<link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/default/easyui.css" />
	<link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/icon.css" />
	</form>
</body>
</html>
