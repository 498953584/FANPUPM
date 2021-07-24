<%@ Page Language="C#" AutoEventWireup="true" CodeFile="expertprojectquery.aspx.cs" Inherits="ExpertProjectQuery" %>
<%@ Import Namespace="cn.justwin.BLL"%>

<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server"><title>ExportProjectQuery</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var Levels = document.getElementById("HdnLevels").value;
			if (Levels != "") {
				document.getElementById("trhide").style.display = "none";
			}
			var tbl = new JustWinTable('DGrdExpert');
			setButton(tbl, 'BtnDel', 'BtnUpd', 'BtnView', 'hfldConstruct')
			setWfButtonState2(tbl, 'WF_1');
			//如果只有一页的数据,隐藏页码
			hideFirstPageNo();

			tbl.registClickTrListener(function () {
				$('#btnFiles').attr('disabled', false);
			});
		});

		function deleteConstruct() {
			if (!document.getElementById('hfldConstruct')) return false;
			if (!confirm("确定要删除吗？")) {
				return false;
			}
		}

		function openview(ic, type) {
			var Id = document.getElementById("hfldConstruct").value;
			if (type == 0) {
				var url = "/Epc/17/ppm/ScienceInnovate/expertprojectview.aspx?ic=" + ic;
			}
			if (type == 1) {
				var url = "/Epc/17/ppm/ScienceInnovate/expertprojectview.aspx?id=" + Id;
			}
			parent.parent.desktop.flowclass = window;
			toolbox_oncommand(url, "查看专项方案");
			//viewopen(url);
		}

		function openEdit(opType) {
			var projectCode = document.getElementById("HdnPrjCode").value;
			var prjName = document.getElementById("HdnPrjName").value;
			var Id = document.getElementById("hfldConstruct").value;
			var type = document.getElementById("HdnType").value;

			var url;
			var title;
			switch (opType) {
				case 'Add':
					url = "/EPC/17/Ppm/ScienceInnovate/ExpertProjectEdit.aspx?Type=Add&pc=" + projectCode + "&Id=" + Id + "&pn=" + escape(prjName);
					title = "专项方案新增";
					break;
				case 'Upd':
					url = "/EPC/17/Ppm/ScienceInnovate/ExpertProjectEdit.aspx?Type=Upd&Id=" + Id + "&pc=" + projectCode + "&pn=" + prjName;
					title = "专项方案修改";
					break;
			}

			top.ui._expertprojectquery = window;
			toolbox_oncommand(url, title);
		}
	</script>
	<style type="text/css">
		.dgheader
		{
			background-color: #EEF2F5;
			white-space: nowrap;
			overflow: hidden;
			font-weight: normal;
			text-align: center;
			border-color: #CADEED;
			color: #727FAA;
			padding-left: 6px;
			padding-right: 6px;
		}
	</style>
</head>
<body>
	<form id="Form1" method="post" runat="server">
	<table width="100%" border="0" cellpadding="0" cellspacing="0">
		<tr id="header">
			<td>
				专项方案
			</td>
		</tr>
		<tr>
			<td style="vertical-align: top;">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd" style="white-space: nowrap;">
							编制起始日期
						</td>
						<td>
							<asp:TextBox ID="txtStartTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd" style="white-space: nowrap;">
							编制结束日期
						</td>
						<td>
							<asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
						</td>
						<td class="descTd" style="white-space: nowrap;">
							方案名称
						</td>
						<td>
							<asp:TextBox ID="txtUnit" Width="120px" runat="server"></asp:TextBox>
						</td>
						<td>
							<asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
						</td>
						<td>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr id="trhide" style="width: 100%" runat="server"><td class="divFooter" style="text-align: left; width: 100%" runat="server">
				<asp:Button ID="BtnAdd" Text="新 增" runat="server" />
				<asp:Button ID="BtnUpd" Text="编 辑" Enabled="false" OnClick="BtnUpd_Click" runat="server" />
				<asp:Button ID="BtnDel" Text="删 除" Enabled="false" OnClick="BtnDel_Click" runat="server" />
				<asp:Button ID="BtnView" Text="查看" Enabled="false" runat="server" />
				<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="050" BusiClass="001" runat="server" />
				<asp:Button ID="btnFiles" Enabled="false" Text="归档" OnClick="btnFiles_Click" runat="server" />
				<input id="HdnPrjCode" style="width: 64px; height: 22px" type="hidden" size="5" name="Hidden1" runat="server" />

				<input id="HdnPrjName" style="width: 56px; height: 22px" type="hidden" size="4" name="Hidden2" runat="server" />

				<input id="HdnType" style="width: 88px; height: 22px" type="hidden" size="9" name="Hidden1" runat="server" />

				<input id="HdnLevels" style="width: 88px; height: 22px" type="hidden" size="9" name="Hidden1" runat="server" />

			</td></tr>
		<tr>
			<td valign="top" height="100%">
				<asp:DataGrid ID="DGrdExpert" AutoGenerateColumns="false" Width="100%" CssClass="gvdata" DataKeyField="MainId" AllowPaging="true" PageSize="25" OnPageIndexChanged="DGrdExpert_PageIndexChanged" runat="server"><ItemStyle CssClass="rowa"></ItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><Columns><asp:TemplateColumn HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
<HeaderTemplate>
								<asp:CheckBox ID="chkAll" runat="server" />
							</HeaderTemplate>

<ItemTemplate>
								<asp:Label ID="lblid" Visible="false" Text='<%# Convert.ToString(Eval("MainId")) %>' runat="server"></asp:Label>
								<asp:CheckBox ID="chk" runat="server" />
							</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn>
<HeaderTemplate>
								<span>
									
								</span>
							</HeaderTemplate>

<ItemTemplate>
								<img alt="" src="" id="imgPNG" style="width: 12px; height: 12px;" runat="server" />

							</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
								<%# Container.ItemIndex + 1 %>
							</ItemTemplate>

<EditItemTemplate>
								<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
							</EditItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="V_BMMC" HeaderText="填报单位"></asp:BoundColumn><asp:BoundColumn DataField="PrjName" HeaderText="工程名称"></asp:BoundColumn><asp:TemplateColumn HeaderText="方案名称">
<ItemTemplate>
								<span class="al" onclick="openview('<%# Eval("FlowGuid") %>',0);">
									<%# Eval("SchemeName") %>
								</span>
							</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="Weavemc" HeaderText="编制人"></asp:BoundColumn><asp:BoundColumn DataField="WeaveDate" HeaderText="编制日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="fillmc" HeaderText="填报人"></asp:BoundColumn><asp:BoundColumn DataField="FillDate" HeaderText="填报日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:TemplateColumn HeaderText="附件" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
								<%# GetAnnx(1722, Convert.ToString(Eval("FlowGuid"))) %>
							</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="mark" HeaderText="归档状态" Visible="false"></asp:BoundColumn><asp:TemplateColumn HeaderText="流程状态">
<ItemTemplate>
								<%# Common2.GetState(Eval("AuditState").ToString()) %>
							</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="FlowGuid" Visible="false"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hfldConstruct" runat="server" />
	</form>
</body>
</html>
