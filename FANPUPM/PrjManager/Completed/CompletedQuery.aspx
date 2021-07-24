<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompletedQuery.aspx.cs" Inherits="PrjManager_Completed_CompletedQuery" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/ecmascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<style type="text/css">
		#queryTable tr td
		{
			white-space: nowrap;
		}
	</style>
	<script type="text/javascript">
		$(document).ready(function () {
			replaceEmptyTable('emptyProject', 'gvwPrjInfo');
			var jwTable = new JustWinTable('gvwPrjInfo');
			formateTable('gvwPrjInfo', 3);

			showTooltip('tooltip', 25);

			jwTable.registClickTrListener(function () {
				document.getElementById('hfldCheckedIds').value = this.id;
				document.getElementById('btnQuery').removeAttribute('disabled');
			});
			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				var btnQuery = document.getElementById('btnQuery');
				if (checkedChk.length == "1") {
					var tr1 = getFirstParentElement(checkedChk[0].parentNode, 'TR');
					document.getElementById('hfldCheckedIds').value = tr1.id;
					btnQuery.removeAttribute('disabled');
				}
				else {
					btnQuery.setAttribute('disabled', 'disabled');
				}
			});

			jwTable.registClickAllCHKLitener(function () {
				document.getElementById('btnQuery').setAttribute('disabled', 'disabled');
			});
		});

		function Query() {
			var hfldPrjId = document.getElementById('hfldCheckedIds');
			var url = "/PrjManager/Completed/PrjCompletedQuery.aspx?ic=" + hfldPrjId.value;
			viewopen(url, '查看项目竣工验收单');
		}

		// 选择人员
		function selectUser(id, name) {
			jw.selectOneUser({ codeinput: id, nameinput: name });
		}

		// 甲方名称
		function pickCorp(param) {
			jw.selectOneCorp({ idinput: 'hfldOwner', nameinput: 'txtOwner' });
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table width="100%" cellpadding="0" cellspacing="0">
		<tr>
			<td>
				<table class="queryTable" id="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							项目编号
						</td>
						<td>
							<asp:TextBox ID="txtPrjCode" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							项目名称
						</td>
						<td>
							<asp:TextBox ID="txtPrjName" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							项目经理
						</td>
						<td>
							<span class="spanSelect" style="width: 122px;">
								<asp:TextBox ID="txtPrjManager" Style="width: 97px; height: 15px;
									border: none; line-height: 16px; margin: margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
								<img src="../../images/icon.bmp" style="float: right; height: 18px;" alt="选择" id="img2"
									onclick="selectUser('hfldPrjManager','txtPrjManager');" />
							</span>
							
							<input id="hfldPrjManager" type="hidden" style="width: 1px" runat="server" />

						</td>
						<td class="descTd">
							项目类型
						</td>
						<td>
							<asp:DropDownList ID="dropPrjKindClass" Width="100%" runat="server"></asp:DropDownList>
						</td>
						<td class="descTd">
							流程状态
						</td>
						<td>
							<asp:DropDownList ID="dropWFState" Width="100px" runat="server"><asp:ListItem Text="所有" Value="" Selected="true" /><asp:ListItem Text="未提交" Value="-1" /><asp:ListItem Text="审核中" Value="0" /><asp:ListItem Text="已审核" Value="1" /><asp:ListItem Text="驳回" Value="-2" /><asp:ListItem Text="重报" Value="-3" /></asp:DropDownList>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							立项申请日期
						</td>
						<td>
							<asp:TextBox ID="txtStartTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							至
						</td>
						<td>
							<asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							建设单位
						</td>
						<td>
							<span class="spanSelect" style="width: 122px;">
								<asp:TextBox ID="txtOwner" Style="width: 97px; height: 15px; border: none; line-height: 16px;
									margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
								<img alt="选择乙方" onclick="pickCorp();" src="../../images/icon.bmp" style="float: right;" />
							</span>
							
							<asp:HiddenField ID="hfldOwner" runat="server" />
						</td>
						<td class="descTd">
							项目状态
						</td>
						<td>
							<asp:DropDownList ID="dropPrjState" Width="100%" runat="server"></asp:DropDownList>
						</td>
						<td colspan="2">
							<asp:Button ID="brnQuery" Text="查询" OnClick="brnQuery_Click" runat="server" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td class="divFooter" style="text-align: left; padding-left: 2px;">
				<input type="button" id="btnQuery" value="查看" disabled="disabled" onclick="Query()" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:GridView ID="gvwPrjInfo" CssClass="gvdata" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="gvwPrjInfo_RowDataBound" DataKeyNames="PrjGuid,TypeCode,Primit" runat="server">
<EmptyDataTemplate>
						<table class="gvdata" cellspacing="0" id="emptyProject" rules="all" border="1" style="width: 100%;
							border-collapse: collapse;">
							<tr>
								<th class="header">
									序号
								</th>
								<th class="header">
									项目状态
								</th>
								<th class="header">
									项目编号
								</th>
								<th class="header">
									项目名称
								</th>
								<th class="header">
									项目经理
								</th>
								<th class="header">
									建设单位
								</th>
								<th class="header">
									项目类型
								</th>
								<th class="header">
									流程状态
								</th>
								<th class="header">
									立项申请日期
								</th>
							</tr>
						</table>
					</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
								<asp:CheckBox ID="cbAllBox" runat="server" />
							</HeaderTemplate><ItemTemplate>
								<asp:CheckBox ID="cbBox" runat="server" />
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
								<%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
							</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="项目状态" DataField="PrjStateName" /><asp:BoundField DataField="PrjCode" HeaderText="项目编号" /><asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="150px"><ItemTemplate>
								<asp:HyperLink ID="hlinkShowName" CssClass="tooltip" Style="text-decoration: none;
									color: Black;" ToolTip='<%# System.Convert.ToString(Eval("PrjName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server">
                        <span class="link" onclick="viewopen('/PrjManager/Completed/PrjCompletedQuery.aspx?&ic=<%# Eval("PrjGuid") %>','650','500')">
                        <%# StringUtility.GetStr(Eval("PrjName").ToString(), 25) %>
                       </span></asp:HyperLink>
							</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="项目经理" DataField="PrjMangerName" /><asp:TemplateField HeaderText="建设单位"><ItemTemplate>
								<%# Eval("Owner") %></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目类型"><ItemTemplate>
								<%# Eval("PrjKindName") %></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
								<%# Common2.GetState(Eval("CompletedFlowState").ToString()) %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="立项申请日期"><ItemTemplate>
								<%# Common2.GetTime(Eval("InputDate")) %>
							</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
				</webdiyer:AspNetPager>
			</td>
		</tr>
	</table>
	
	<div id="divFramPerson" title="选择人员" style="display: none">
		<iframe id="IframePerson" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<asp:HiddenField ID="hdReturnVal" runat="server" />
	<asp:HiddenField ID="hfldCheckedIds" runat="server" />
	</form>
</body>
</html>
