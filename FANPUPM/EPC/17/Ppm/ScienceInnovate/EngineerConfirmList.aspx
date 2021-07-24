<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EngineerConfirmList.aspx.cs" Inherits="EPC_17_Ppm_ScienceInnovate_EngineerConfirmList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server"><title>工程确认单和工程洽商单</title>
	<script src="../../../../Script/jquery.js" type="text/javascript"></script>
	<script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
	<script type="text/javascript" src="../../../../StockManage/script/Config2.js"></script>
	<script type="text/ecmascript" src="/StockManage/script/Config2S.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../../../Script/wf.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var purchasePlanTable = new JustWinTable('DGrdStandard');
			setButton(purchasePlanTable, 'BtnDel', 'BtnUpd', 'BtnView', 'HdnId');
			setWfButtonState2(purchasePlanTable, 'WF1');
			if ($('#hfldIsAllowAudit').val() == 'false') {
				$('#btnStartWF').attr('style', 'display:none');
				$('#CancelBt').attr('style', 'display:none');
				$('#WF1_btnViewWF').attr('style', 'display:none');
				$('#WF1_btnWFPrint').attr('style', 'display:none');
				$('#WF1_BtnWFDel').attr('style', 'display:none');
			}
			//如果只有一页的数据,隐藏页码
			hideFirstPageNo();
		});
		function clickRow(Id, Type) {
			document.getElementById("HdnId").value = Id;
			if (Type != "View") {
				document.getElementById("BtnUpd").disabled = false;
				document.getElementById("BtnDel").disabled = false;
			}
			document.getElementById("BtnView").disabled = false;
		}

		function FileUp() {
			var hnClassID = "26" + window.document.getElementById('HdnId').value;
			var ClassID = "FD1104BD-A7F4-45EA-9FEB-0265AF013D00";
			var pc = document.getElementById('HdnPrjCode').value;
			var hnProjectCode = "1";
			url = "../FileManage/ProjectFileUpAdd.aspx?FilesID=" + hnClassID + "&ProjectCode=" + hnProjectCode + "&ClassID=" + ClassID + "&PC=" + pc + "&Science=1&ModuleId=26";
			url += "&SpecialID=0";
			return window.showModalDialog(url, window, 'dialogHeight:220px;dialogWidth:380px;center:1;help:0;status:0;');
		}

		function rowQueryA(path) {
			parent.parent.desktop.AddIncometBalance = window;
			toolbox_oncommand(path, "查看收入合同变更");
		}
		function rowQuery(path, _title) {
			parent.parent.desktop.AddIncometBalance = window;
			toolbox_oncommand(path, _title);
		}

		function openEdit(opType) {
			var projectCode = document.getElementById("HdnPrjCode").value;
			var bigsort = document.getElementById("HdnBigSort").value;
			var Id = document.getElementById("HdnId").value;
			var url;
			var title;
			var new_title = document.getElementById("HiddenField1").value;
			switch (opType) {
				case 'Add':
					url = "/EPC/17/Ppm/ScienceInnovate/MeasureDataEdit.aspx?Type=Add&pc=" + projectCode + "&Id=" + Id + "&bs=" + bigsort + "&sm=0&pn=_EngineerConfirmList";
					title = "新增" + new_title;
					break;
				case 'Upd':
					url = "/EPC/17/Ppm/ScienceInnovate/MeasureDataEdit.aspx?Type=Upd&Id=" + Id + "&pc=" + projectCode + "&bs=" + bigsort + "&sm=0&pn=_EngineerConfirmList";
					title = "编辑" + new_title;
					break;
				case 'View':
					url = "/EPC/17/Ppm/ScienceInnovate/MeasureDataEdit.aspx?Type=View&Id=" + Id + "&pc=" + projectCode + "&bs=" + bigsort + "&sm=0&pn=_EngineerConfirmList";
					title = "查看" + new_title;
					break;

			}
			top.ui._EngineerConfirmList = window; //不可少
			toolbox_oncommand(url, title); //引用js
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
	<asp:HiddenField ID="HiddenField1" runat="server" />
	<font face="宋体">
		<table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server"><tr runat="server"><td class="divHeader" style="height: 28px;" runat="server">
					<asp:Label ID="LblTitle" Width="184px" runat="server"></asp:Label>
				</td></tr><tr runat="server"><td style="vertical-align: top;" runat="server">
					<table class="queryTable" cellpadding="3px" cellspacing="0px">
						<tr>
							<td class="descTd">
								资料编号
							</td>
							<td>
								<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
							</td>
							<td class="descTd">
								资料名称
							</td>
							<td>
								<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
							</td>
							<td>
								<asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
							</td>
						</tr>
					</table>
				</td></tr><tr runat="server"><td class="divFooter" style="text-align: left; width: 100%" runat="server">
					<asp:Button ID="BtnAdd" Text="新增" runat="server" />
					<asp:Button ID="BtnUpd" Text="编辑" Enabled="false" runat="server" />
					<asp:Button ID="BtnDel" Text="删除" Enabled="false" OnClick="BtnDel_Click" runat="server" />
					<asp:Button ID="BtnView" Text="查看" Enabled="false" runat="server" />
					<asp:Button ID="btnFiles" Enabled="false" Text="归档" OnClick="btnFiles_Click" runat="server" />
					<input id="HdnId" style="width: 48px; height: 22px" type="hidden" size="2" name="HdnId" runat="server" />

					<input id="HdnPrjCode" style="width: 64px; height: 22px" type="hidden" size="5" name="Hidden1" runat="server" />

					<input id="HdnSmallSort" style="width: 56px; height: 22px" type="hidden" size="4" name="Hidden2" runat="server" />

					<JWControl:JavaScriptControl ID="Js" runat="server"></JWControl:JavaScriptControl>
					<input id="HdnBigSort" style="width: 72px; height: 22px" type="hidden" size="6" name="Hidden1" runat="server" />

					<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="118" BusiClass="001" runat="server" />
				</td></tr><tr runat="server"><td runat="server">
					<table style="width: 100%; height: 50%; margin-top: 10px;" border="0">
						<asp:DataGrid ID="DGrdStandard" Width="100%" DataKeyField="ID" CssClass="gvdata" AutoGenerateColumns="false" AllowPaging="true" PageSize="25" OnPageIndexChanged="DGrdStandard_PageIndexChanged" runat="server"><ItemStyle CssClass="rowa"></ItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><Columns><asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="20px"><HeaderTemplate>
										<input id="chkAll" type="checkbox" /></HeaderTemplate><ItemTemplate>
										<asp:Label ID="Label3" Text='<%# Convert.ToString(Eval("ID")) %>' runat="server"></asp:Label>
										<asp:CheckBox ID="chk" runat="server" />
										<asp:HiddenField ID="hfldPrjCode" Value='<%# Convert.ToString(Eval("PrjCode")) %>' runat="server" />
										<asp:HiddenField ID="hfldGuid" Value='<%# Convert.ToString(Eval("TechGuid")) %>' runat="server" />
									</ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="20px"><HeaderTemplate>
										
									</HeaderTemplate><ItemTemplate>
										<asp:Image ID="Image1" Width="12px" Height="12px" runat="server" />
									</ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="序号" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="25px"><ItemTemplate>
										<%# Container.ItemIndex + 1 %>
									</ItemTemplate></asp:TemplateColumn><asp:BoundColumn DataField="SerialNumber" HeaderText="资料编号"></asp:BoundColumn><asp:TemplateColumn HeaderText="资料名称"><ItemTemplate>
										<asp:Label ID="Label1" CssClass="link" Text='<%# Convert.ToString(Eval("ItemName")) %>' runat="server"></asp:Label>
									</ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="变更编号"><ItemTemplate>
										<%# GetConModifyName(int.Parse(Eval("Id").ToString())) %>
									</ItemTemplate></asp:TemplateColumn><asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn><asp:TemplateColumn HeaderText="附件" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px"><ItemTemplate>
										<%# GetAnnx(Convert.ToString(Eval("TechGuid"))) %>
									</ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="流程状态" HeaderStyle-Width="30px"><ItemTemplate>
										<%# Common2.GetState(Eval("FlowState").ToString()) %>
									</ItemTemplate></asp:TemplateColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid>
					</table>
				</td></tr></table>
	</font>
	<div>
	</div>
	<asp:HiddenField ID="hfldIsAllowAudit" Value="true" runat="server" />
	</form>
</body>
</html>
<script language="javascript" type="text/javascript">
	var img_src = "EPC/QuaitySafety/";
	$(function () {
		if (request("Type").toUpperCase() == "VIEW") {
			$("#ba").find("th").eq(0).hide();
			$("#td_Btn").hide();
			$(".divFooter").hide();
		}
		if ($("#DGrdStandard tr").size() == 2) {
			if ($(".GD-Pager") != null) {
				$(".GD-Pager").hide();
				$("#DGrdStandard tr").eq(0).find("td").eq(0).find("input").attr("disabled", "disabled");
			}
		}
		$("#DGrdStandard tr").each(function () {
			var _markTem = $(this).attr("mark");
			if (_markTem != "undefined" && _markTem != "" && _markTem != null) {
				if (request("Type").toUpperCase() == "VIEW") {
					if (_markTem == "1") {
						$(this).find('td').eq(0).find("img").attr("src", "/images/over.gif");
					} else
						if (_markTem == "2") {
							$(this).find('td').eq(0).find("img").attr("src", "/images/Edit.gif");
						} else
							if (_markTem == "3") {
								$(this).find('td').eq(0).find("img").attr("src", "/images/Process.gif");
							}
				} else {
					if (_markTem == "1") {
						$(this).find('td').eq(1).find("img").attr("src", "/images/over.gif");
					} else
						if (_markTem == "2") {
							$(this).find('td').eq(1).find("img").attr("src", "/images/Edit.gif");
						} else
							if (_markTem == "3") {
								$(this).find('td').eq(1).find("img").attr("src", "/images/Process.gif");
							}
				}
			}
		});
	});
	function clickRow(sc) {
	}
	function request(paras) {
		var url = location.href;
		var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
		var paraObj = {}
		for (i = 0; j = paraString[i]; i++) {
			paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
		}
		var returnValue = paraObj[paras.toLowerCase()];
		if (typeof (returnValue) == "undefined") {
			return "";
		} else {
			return returnValue;
		}
	}
</script>
