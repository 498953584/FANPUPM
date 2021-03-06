<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InstitutionList.aspx.cs" Inherits="Enterprise_InstitutionList" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>企业制度</title>
	<style type="text/css">
		span
		{
			text-decoration: underline;
			color: Blue;
			cursor: pointer;
		}
	</style>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var jTable = new JustWinTable("GVInsList");
			setWfButtonState2(jTable, "WF1");
		});
		function openView(inscode) {
			var uri = "InstitutionView.aspx?ic=" + inscode;
			var fth = "width=640px,height=400px,location=no,menubar=no,toolsbar=no,status=no,resizable=yes";
			window.open(uri, '', fth);
		}
		function openEdit(op) {
			var uri = "/oa/System/Institution/InstitutionListEdit.aspx?ic=" + document.getElementById("HdnInsCode").value + "&op=" + op;
			var title = op == 'Add' ? '新增制度' : '编辑制度';
			top.ui._InstitutionList = window;
			toolbox_oncommand(uri, title);
			//            var fth = "width=720px,height=500px,location=no,menubar=no,toolsbar=no,status=no";
			//            window.open(uri, '', fth);
		}
		function rowClick(insCode, status) {
		    if ($("#HdnLeveCode").val() != '管理员') {
		        
		        document.getElementById("BtnDelete").disabled = true;//显示删除按钮
		    }
		    else {
		        document.getElementById("BtnDelete").disabled = false; //隐藏删除按钮
            }
			if (status == "-1" || status == "-3") {
				document.getElementById("btnEdit").disabled = false;
			}
			if (status == "1" || status == "-2") {
				document.getElementById("btnEdit").disabled = true;
			}
			if (status == "0") {
				document.getElementById("btnEdit").disabled = true;
			}
			document.getElementById("HdnInsCode").value = insCode;
		}

		function viewInstitutions(insCode) {
			parent.desktop.viewInstitutions = window;
			var url = "/oa/System/Institution/InstitutionView.aspx?ic=" + insCode;
			toolbox_oncommand(url, "制度查看");
		}
        
    </script>
</head>
<body class="body_clear">
	<form id="form1" runat="server">
	<table class="table-normal" style="width: 100%; height: 613px;" border="1">
		<tr>
			<td style="width: 160px; vertical-align: top;">
				<div style="width: 100%; height: 100%; overflow: auto; margin-top: 15px;">
					<asp:TreeView ID="TVInsClass" ExpandDepth="1" ForeColor="Black" ShowLines="true" AutoGenerateDataBindings="false" OnSelectedNodeChanged="TVInsClass_SelectedNodeChanged" runat="server"><HoverNodeStyle ForeColor="Red" /><SelectedNodeStyle ForeColor="Red" /><NodeStyle ForeColor="Black" /></asp:TreeView>
				</div>
			</td>
			<td style="vertical-align: top;">
				<table class="table-normal" style="width: 100%; vertical-align: top;" border="1"
					cellspacing="0">
					<tr>
						<td class="td-head">
							企业制度
						</td>
					</tr>
					<tr>
						<td class="td-search" style="height: 16px">
							搜索：<asp:TextBox ID="txtcontent" Height="14px" Width="100px" runat="server"></asp:TextBox>
							<asp:DropDownList ID="ddl_Search" Width="80px" runat="server"><asp:ListItem Value="InsName" Text="名称" /><asp:ListItem Value="UniqueCode" Text="编号" /><asp:ListItem Value="IssuePerson" Text="签发人" /></asp:DropDownList>
							<asp:Button ID="BtnSearch" Text="查 询" CssClass="button-normal" OnClick="BtnSearch_Click" runat="server" />
						</td>
					</tr>
					<tr>
						<td class="td-toolsbar">
							<input id="btnAdd" type="button" value="新 增" onclick="openEdit('Add');" />
							<input type="button" id="btnEdit" value=" 编 辑" disabled="disabled" onclick="openEdit('Edit');" />
							<asp:Button ID="BtnDelete" Text="删 除" Enabled="false" OnClick="BtnDelete_Click" runat="server" />
							<!--<asp:Button ID="BtnWF_Purview" Text="提交审核" Enabled="false" OnClick="BtnWF_Purview_Click" runat="server" />
                            <input id="btnViewPurview" type="button" value="查看审核" onclick="OpenViewWF('069','001')" disabled="disabled" />-->
							<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="069" BusiClass="001" runat="server" />
						</td>
					</tr>
					<tr>
						<td style="vertical-align: top;">
							<div style="overflow: auto; min-width: 900px;">
								<asp:GridView ID="GVInsList" CssClass="grid" Width="100%" AllowPaging="true" AutoGenerateColumns="false" PageSize="25" OnPageIndexChanging="GVInsList_PageIndexChanging" OnRowDataBound="GVInsList_RowDataBound" DataKeyNames="InsCode" runat="server">
<EmptyDataTemplate>
										<table class="grid" cellspacing="0" rules="all" border="0" cellpadding="0" style="border-collapse: collapse;">
											<tr class="grid_head">
												<th style="width: 25px">
													序号
												</th>
												<th>
													编号
												</th>
												<th>
													名称
												</th>
												<th>
													签发人
												</th>
												<th>
													签发日期
												</th>
												<th>
													流程状态
												</th>
												<th>
													发布时间
												</th>
											</tr>
										</table>
									</EmptyDataTemplate>
<Columns><asp:BoundField HeaderText="序号" /><asp:TemplateField HeaderText="编号" ItemStyle-Width="200px"><ItemTemplate>
												<div style="width: 98%; word-break: break-all;">
													<%# Eval("UniqueCode").ToString() %></div>
											</ItemTemplate></asp:TemplateField><asp:BoundField DataField="InsName" HeaderText="名称" /><asp:BoundField DataField="IssuePerson" HeaderText="签发人" /><asp:BoundField DataField="IssueDate" HeaderText="签发日期" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:TemplateField HeaderText="流程状态">
<ItemTemplate>
												<%# Common2.GetState(Eval("status").ToString()) %>
											</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="writedate" HeaderText="发布时间" /></Columns><RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<input type="hidden" id="HdnLeveCode" style="width: 1px" runat="server" />

	<input type="hidden" id="HdnInsCode" style="width: 1px" runat="server" />

	</form>
</body>
</html>
