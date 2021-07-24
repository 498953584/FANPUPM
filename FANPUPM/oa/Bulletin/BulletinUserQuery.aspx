<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulletinUserQuery.aspx.cs" Inherits="oa_Bulletin_BulletinUserQuery" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="com.jwsoft.pm.entpm.action"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>公告列表</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/ecmascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
	<link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript">
	    $(document).ready(function () {
	        replaceEmptyTable('emptyTable', 'gvBulletin');
	        var jwTable = new JustWinTable('gvBulletin');
	        showTooltip('tooltip', 25);
	    });

	    //选择人员
	    function selectUser(id, name) {
	        jw.selectOneUser({ codeinput: 'hfldName', nameinput: 'txtName' });
	    }

	    function openBulletin(id, title) {
	        var url = '/oa/Bulletin/BulletinUserDetail.aspx?id=' + id;
	        var userInfo = { url: url, title: title + "的查看记录" };
	        top.ui.openWin(userInfo);
	    } 
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		<tr>
			<td style="vertical-align: top">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							公告标题
						</td>
						<td>
							<asp:TextBox ID="txtTitle" Width="122px" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							发布日期
						</td>
						<td>
							<asp:TextBox ID="txtStartDate" Width="122px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							至
						</td>
						<td>
							<asp:TextBox ID="txtEndDate" Width="122px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							发布范围
						</td>
						<td>
							<asp:DropDownList ID="droprange" Width="100%" runat="server"><asp:ListItem Text="所有" Value="" /><asp:ListItem Text="集团内部" Value="1" /><asp:ListItem Text="外部" Value="2" /></asp:DropDownList>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							发布人
						</td>
						<td>
							<span class="spanSelect" style="width: 122px;">
								<asp:TextBox ID="txtName" Style="width: 93px; height: 15px; border: none;
									line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
								<img src="../../images/icon.bmp" style="float: right;" alt="选择" id="imgName" onclick="selectUser();" runat="server" />

							</span>
							<input id="hfldName" type="hidden" style="width: 1px" runat="server" />

						</td>
						<td class="descTd" style="white-space: nowrap;">
							流程状态
						</td>
						<td>
							<asp:DropDownList ID="dropWFState" Width="100%" runat="server"><asp:ListItem Value="" Text="所有" /><asp:ListItem Value="-1" Text="未提交" /><asp:ListItem Value="0" Text="审核中" /><asp:ListItem Value="1" Text="已审核" /><asp:ListItem Value="-2" Text="驳回" /><asp:ListItem Value="-3" Text="重报" /></asp:DropDownList>
						</td>
						<td style="white-space: nowrap;">
							<asp:Button ID="brnQuery" Text="查询" OnClick="brnQuery_Click" runat="server" />
						</td>
						<td colspan="3">
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td style="vertical-align: top;">
				<asp:GridView ID="gvBulletin" CssClass="gvdata" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvBulletin_RowDataBound" DataKeyNames="ID" runat="server">
<EmptyDataTemplate>
						<table id="emptyTable">
							<tr class="header">
								<th>
									序号
								</th>
								<th>
									公告标题
								</th>
								<th>
									发布范围
								</th>
								<th>
									分子机构名称
								</th>
								<th>
									发布人
								</th>
								<th>
									发布时间
								</th>
								<th>
									流程状态
								</th>
								<th>
									用户查看记录
								</th>
							</tr>
						</table>
					</EmptyDataTemplate>
<Columns><asp:BoundField HeaderText="序号" DataField="NUM" ItemStyle-Width="25px" /><asp:TemplateField HeaderText="公告标题" HeaderStyle-Width="150px"><ItemTemplate>
								<asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" runat="server">
                        <span class="link tooltip" title='<%# Eval("V_TITLE").ToString() %>' onclick="viewopen('BulletinLock.aspx?rid=<%# Eval("ID") %>&ic=<%# Eval("ID") %>', '公告查看')">
                        <%# StringUtility.GetStr(Eval("V_TITLE").ToString(), 25) %>
                       </span></asp:HyperLink>
							</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="发布范围" DataField="RangName" /><asp:TemplateField HeaderText="分子机构名称" SortExpression="CorpCode"><ItemTemplate>
								<span class="tooltip" title='<%# BulletionAction.ReturnCropName(Eval("CorpCode").ToString()) %>'>
									<%# StringUtility.GetStr(BulletionAction.ReturnCropName(Eval("CorpCode").ToString()), 25) %>
								</span>
							</ItemTemplate><EditItemTemplate>
								<asp:TextBox ID="TextBox1" Text='<%# System.Convert.ToString(Eval("CorpName"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
							</EditItemTemplate></asp:TemplateField><asp:BoundField HeaderText="发布人" DataField="UserName" /><asp:TemplateField HeaderText="发布时间"><ItemTemplate>
								<%# Common2.GetTime(Eval("Date")) %>
							</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
								<%# Common2.GetState(Eval("AuditState").ToString()) %></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="用户查看记录" HeaderStyle-Width="150px"><ItemTemplate>
								<asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" runat="server">
                        <span class="link"  onclick="openBulletin('<%# Eval("ID").ToString() %>','<%# Eval("V_TITLE").ToString() %>')" >
                        用户查看记录
                       </span></asp:HyperLink>
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
	<div id="divFram" title="" style="display: none">
		<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	</form>
</body>
</html>
