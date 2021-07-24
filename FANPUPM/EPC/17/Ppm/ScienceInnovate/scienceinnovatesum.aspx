<%@ Page Language="C#" AutoEventWireup="true" CodeFile="scienceinnovatesum.aspx.cs" Inherits="ScienceInnovateSum" %>
<%@ Import Namespace="cn.justwin.BLL"%>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>工程总结资料</title>
	<base target="_self" />
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2S.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var type = document.getElementById("HdnType").value;
			if (type == "List") {
				document.getElementById("trhide").style.display = "none";
			}
			var purchasePlanTable = new JustWinTable('dgList');
			setButton(purchasePlanTable, 'btnDel', 'btnEdit', 'button', 'hfldConstruct')
			setWfButtonState2(purchasePlanTable, 'WF_1');
			//如果只有一页的数据,隐藏页码
			hideFirstPageNo();
		});

		function rowQuery(prjcode, id) {
			var path = "/EPC/17/Ppm/ScienceInnovate/ScienceInnovateSumEdit.aspx?Type=view&PrjCode=" + prjcode + "&id=" + id;
			parent.parent.desktop.AddIncometBalance = window;
			toolbox_oncommand(path, "查看技术总结");
		}

		function CheckData() {
			if (document.getElementById("hfldConstruct").value.length == 0) {
				alert("请选择记录！");
				return false;
			}
			else {
				return true;
			}

		}

		function doEdit(target) {
			var url = "/EPC/17/Ppm/ScienceInnovate/ScienceInnovateSumEdit.aspx?PrjCode=" + document.getElementById("hidPrjCode").value;
			var title;
			if (target == "edit") {
				url = "/EPC/17/Ppm/ScienceInnovate/ScienceInnovateSumEdit.aspx?PrjCode=" + document.getElementById("hidPrjCode").value;
				url += "&Id=" + document.getElementById("hfldConstruct").value;
				title = "编辑技术总结";
			}
			else if (target == "view") {
				url = "/EPC/17/Ppm/ScienceInnovate/ScienceInnovateSumEdit.aspx?Type=view&PrjCode=" + document.getElementById("hidPrjCode").value;
				Options = "dialogHeight:400px;dialogWidth:400px;center:1;help:0;status:0;";
				url += "&Id=" + document.getElementById("hfldConstruct").value;
				title = "查看技术总结";
			}
			else if (target == "new") {
				url = "/EPC/17/Ppm/ScienceInnovate/ScienceInnovateSumEdit.aspx?PrjCode=" + document.getElementById("hidPrjCode").value;
				title = "新增技术总结";
			}
			url += "&radmon=" + Math.random();
			top.ui._scienceinnovatesum = window; //不可少
			toolbox_oncommand(url, title); //引用js
		}

		function ShowAnnext() {
			if (CheckData())
				window.showModalDialog("/CommonWindow/Annex/AnnexList.aspx?mid=1754&rc=" + document.getElementById("hfldConstruct").value + "&at=0&type=1", "", 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');

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
	<table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
		<tr>
			<td style="vertical-align: top;" width="100%">
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
							填报单位
						</td>
						<td>
							<asp:TextBox ID="txtUnit" Width="120px" runat="server"></asp:TextBox>
						</td>
						<td class="descTd" style="white-space: nowrap;">
							施工技术总结名称
						</td>
						<td>
							<asp:TextBox ID="txtName" Width="120px" runat="server"></asp:TextBox>
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
		<tr id="trhide" style="width: 100%;" runat="server"><td class="divFooter" style="text-align: left; width: 100%" runat="server">
				<input id="hidID" type="hidden" runat="server" />

				<input id="hidPrjCode" type="hidden" name="Hidden1" runat="server" />

				<asp:Button ID="btnAdd" Text="新  增" runat="server" />
				<asp:Button ID="btnEdit" Text="编  辑" Enabled="false" runat="server" />
				<asp:Button ID="btnDel" Text="删  除" Enabled="false" OnClick="btnDel_Click" runat="server" />
				<asp:Button ID="button" Text="查 看" Enabled="false" OnClick="button_Click" runat="server" />
				
				<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="040" BusiClass="001" runat="server" />
				<asp:Button ID="btnFiles" Enabled="false" Text="归档" OnClick="btnFiles_Click" runat="server" />
			</td></tr>
		<tr>
			<td valign="top" height="100%" width="100%">
				<asp:DataGrid ID="dgList" DataKeyField="id" AutoGenerateColumns="false" Width="100%" CssClass="gvdata" AllowPaging="true" PageSize="25" OnPageIndexChanged="dgList_PageIndexChanged" runat="server"><ItemStyle CssClass="rowa"></ItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><Columns><asp:TemplateColumn HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
<HeaderTemplate>
								<asp:CheckBox ID="chkAll" runat="server" />
							</HeaderTemplate>

<ItemTemplate>
								<asp:Label ID="lblid" Visible="false" Text='<%# Convert.ToString(Eval("id")) %>' runat="server"></asp:Label>
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
</asp:TemplateColumn><asp:BoundColumn DataField="bm" HeaderText="填报单位"></asp:BoundColumn><asp:BoundColumn HeaderText="工程名称"></asp:BoundColumn><asp:TemplateColumn HeaderText="施工技术总结名称"><ItemTemplate>
								<span class="al" onclick="rowQuery('<%# Eval("prjid") %>','<%# Eval("id") %>');">
									<%# Eval("summaryName") %>
								</span>
							</ItemTemplate></asp:TemplateColumn><asp:BoundColumn DataField="Reporter" HeaderText="编制人"></asp:BoundColumn><asp:BoundColumn DataField="ReporterDate" HeaderText="编制日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="Comment" HeaderText="备注"></asp:BoundColumn><asp:TemplateColumn HeaderText="附件" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
								<%# GetAnnx(1754, Convert.ToString(Eval("WfGuid"))) %>
							</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="mark" HeaderText="归档状态" Visible="false"></asp:BoundColumn><asp:TemplateColumn HeaderText="流程状态">
<ItemTemplate>
								<%# Common2.GetState(Eval("CheckState").ToString()) %>
							</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="wfguid" Visible="false"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid>
				<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hfldConstruct" runat="server" />
	<input id="HdnType" style="width: 88px; height: 22px" type="hidden" size="9" name="Hidden1" runat="server" />

	</form>
</body>
</html>
