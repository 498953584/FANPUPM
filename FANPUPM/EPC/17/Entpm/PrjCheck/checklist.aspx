<%@ Page Language="C#" AutoEventWireup="true" CodeFile="checklist.aspx.cs" Inherits="CheckList" %>
<%@ Import Namespace="cn.justwin.BLL"%>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>CheckList-项目检查- 检查</title><link type="text/css" href="/Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
	<script type="text/javascript" src="/StockManage/script/Config2S.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
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
	<script type="text/javascript" language="javascript">
		$(function () {

			var purchasePlanTable = new JustWinTable('gvItemInpect');
			if (getRequestParam("Type").toLowerCase() == "Edit".toLowerCase()) {
				setButton(purchasePlanTable, 'Button_del', 'Button_edit', 'Button_view', 'HiddenField_ID')
				$("#headTable").hide();
			}
			if (getRequestParam("Type").toLowerCase() == "rectify".toLowerCase()) {
				$("#Textbox_zgnr").removeAttr("readonly");
			}
			judgeTr();
			// 页面公用, 需判断类型
			var type = getRequestParam('Type');
			if (type != 'ShenHe') {
				$('input[id^=WF]').css('display', 'none');
				$('#btnStartWF').css('display', 'none'); //提交审核
				$('#CancelBt').css('display', 'none'); 		//撤回
			} else {
				setWfButtonState2(purchasePlanTable, 'WF1');
			}
			//如果只有一页的数据,隐藏页码
			hideFirstPageNo();
			jw.tooltip();
			$("#gvItemInpect tr").each(function () {
				var _markTem = $(this).attr("mark");
				if (_markTem != "undefined" && _markTem != "" && _markTem != null) {
					if (getRequestParam("Type") == "Edit") {
						if (_markTem == "1") {
							$(this).find('td').eq(1).find("img").attr("src", "/images/over.gif");
						} else
							if (_markTem == "2") {
								$(this).find('td').eq(1).find("img").attr("src", "/images/Edit.gif");
							} else
								if (_markTem == "3") {
									$(this).find('td').eq(1).find("img").attr("src", "/images/Process.gif");
								}
						$(".queryTable").hide();
					} else {
						if (_markTem == "1") {
							$(this).find('td').eq(0).find("img").attr("src", "/images/over.gif");
						} else
							if (_markTem == "2") {
								$(this).find('td').eq(0).find("img").attr("src", "/images/Edit.gif");
							} else
								if (_markTem == "3") {
									$(this).find('td').eq(0).find("img").attr("src", "/images/Process.gif");
								}
					}
				}
			});

			$("#gvItemInpect tr").each(function () {
				$(this).click(function () {
					var _markTem = $(this).attr("mark");
					if (_markTem == "3") {
						$("#Button_del").attr("disabled", "disabled");
					}
				});
			});
		});

		function newView(pk, ProjectName, n) {
			var url = "/EPC/17/Entpm/PrjCheck/CheckManage.aspx?Type=View&pk=" + pk + "&PrjName=" + PrjName + "";
			top.ui._checklist = window;
			switch (n) {
				case "1":
					toolbox_oncommand(url, "审核项目检查");
					break;
				case "2":
					toolbox_oncommand(url, "审核项目整改");
					break;
				case "3":
					toolbox_oncommand(url, "审核项目验证");
					break;
				default:
					toolbox_oncommand(url, "审核项目查看");
					break;
			}

		}
		function judgeTr() {
			$("#gvItemInpect tr").each(function () {
				if ($(this).attr("class") == "header") {
				} else {
					$(this).live('click', function () {
						$("#Button_view").removeAttr("disabled");
						//FlowState：-1:未提交，0：审核中，1：已审核，-2：驳回，-3：重报。
						var flowstate = $(this).attr('flowState');
						//mark:  1：已归档，3：作为归档资料，2：未作为归档资料。
						var mark = $(this).attr('mark');
						if (getRequestParam('Type').toLowerCase() == 'Edit'.toLowerCase()) {
							if (mark == '3') {
								if (flowstate == '1') {
									$('#btnFiles').removeAttr('disabled');
									$('#Button_del').attr('disabled', 'disabled');
									$('#Button_edit').attr('disabled', 'disabled');
								} else if (flowstate == '-1' || flowstate == '-3') {
									$('#btnFiles').attr('disabled', 'disabled');
									$('#Button_del').removeAttr('disabled');
									$('#Button_edit').removeAttr('disabled');
								} else {
									$('#btnFiles').attr('disabled', 'disabled');
									$('#Button_del').attr('disabled', 'disabled');
									$('#Button_edit').attr('disabled', 'disabled');
								}
							} else {
								$('#btnFiles').attr('disabled', 'disabled');
							}
							if (mark == '1') {
								$('#Button_edit').attr('disabled', 'disabled');
								$('#Button_del').attr('disabled', 'disabled');
							}
						}
						else if (getRequestParam('Type').toLowerCase() == 'Rectify'.toLowerCase()) {
							if (flowstate == '-1' || flowstate == '-3') {
								$('#Button_edit').removeAttr('disabled');
							} else {
								$('#Button_edit').attr('disabled', 'disabled');
							}
							if (mark == '1') {
								$('#Button_edit').attr('disabled', 'disabled');
							}
						} else if (getRequestParam('Type').toLowerCase() == 'Certify'.toLowerCase()) {
							if (flowstate == '-1' || flowstate == '-3') {
								$('#Button_edit').removeAttr('disabled');
							} else {
								$('#Button_edit').attr('disabled', 'disabled');
							}
							if (mark == '1') {
								$('#Button_edit').attr('disabled', 'disabled');
							}
						}
						$("#HiddenField_ID").val($(this).attr("id"));
					});
				}
			});
		}
	</script>
</head>
<body>
	<script type="text/javascript">     
		var PrjName = '<%=Request.QueryString["PrjName"] %>';
		function ShowInsertWindow(pn)
		{	
				//alert(pn);
			<%if (base.Request["PrjCode"] == null)
{%>
			window.alert('请选择项目！')
			<%}
else
{%>			
			var url = "/EPC/17/Entpm/PrjCheck/CheckManage.aspx?prjcode=<%=Request["PrjCode"].ToString() %>&PrjName="+pn+"&Levels=<%=Request["Levels"].ToString() %>&Type=<%=Request["Type"].ToString() %>";
			top.ui._checklist = window;
            toolbox_oncommand(url,"新增项目检查");//引用js
			<%}%>		
		}
		function ShowEditWindow()
		{
		var pk=$("#HiddenField_ID").val();
			if(pk==null&&pk!="")
			{
				window.alert('请选择记录！');
			}
			else
			{   
				var url ='/EPC/17/Entpm/PrjCheck/CheckManage.aspx?pk='+pk+'&Levels=<%=(base.Request["Levels"] == null) ? "" : base.Request["Levels"].ToString() %>&Type=<%=Request["Type"].ToString() %>&PrjName='+PrjName;
				top.ui._checklist = window;
                toolbox_oncommand(url,"编辑项目检查");//引用js
			}
		}
		function Print()
		{
			var pk=$("#HiddenField_ID").val();
			if(pk==null&&pk!="")
			{
				window.alert('请选择记录！');
			}
			else
			{
				var url ="/ModuleSet/common/common/print.asp?Floder=PrjCheck&Report=ProjectChkReport&sql=select * from Prj_V_ProjectCheckView where id='+pk";
				top.ui._checklist = window;
                toolbox_oncommand(url,"打印项目检查");//引用js
			}
		}
		function ShowInfo(event)
		{
			var pk=$("#HiddenField_ID").val();
			if(pk==null&&pk!="")
			{
				window.alert('请选择记录！');
			}
			else
			{
			    var url="/EPC/17/Entpm/PrjCheck/CheckManage.aspx?Type=View&pk="+pk+"&PrjName="+PrjName+"";
			    top.ui._checklist = window;
                toolbox_oncommand(url,"查看项目检查");//引用js
			}
		}
		function ShowSpWindow()
		{
			var pk=$("#HiddenField_ID").val();
			if(pk==null&&pk!="")
			{
				window.alert('请选择记录！');
			}
			else
			{
				var url="/EPC/17/Entpm/PrjCheck/CheckSp.aspx?pk="+pk+"";
				top.ui._checklist = window;
                toolbox_oncommand(url,"审核项目检查");//引用js
			}
		}
	</script>
	<form id="Form1" method="post" runat="server">
	<asp:HiddenField ID="HiddenField_ID" runat="server" />
	<table width="100%" id="headTable">
		<tr>
			<td class="divHeader" align="center">
				<asp:Literal ID="Literal1" runat="server"></asp:Literal>
			</td>
		</tr>
	</table>
	<table style="vertical-align: top;" cellspacing="0" cellpadding="0" width="100%"
		border="0">
		<tr style="height: 25px" id="type_TF_show" runat="server"><td runat="server">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							<asp:TextBox ID="TextBox_pk" Width="0px" BorderColor="White" runat="server"></asp:TextBox>
							<asp:Literal ID="Literal2" Text="检查单位:" runat="server"></asp:Literal>
							<asp:TextBox Width="120px" ID="TextBox_jcdw" runat="server"></asp:TextBox>
							<asp:Literal ID="Literal3" Text="受检单位:" runat="server"></asp:Literal>
							<asp:TextBox Width="120px" ID="TextBox_sjdw" runat="server"></asp:TextBox>
							<asp:Button ID="btn_Search" Text="查询" OnClick="Button_query_Click" runat="server" />
						</td>
					</tr>
				</table>
			</td></tr>
		<tr>
			<td class="divFooter" style="text-align: left;">
				<asp:Button ID="Button_add" Text="新 增" runat="server" /><asp:Button ID="Button_edit" Text="编  辑" Enabled="false" runat="server" />
				<asp:Button ID="Button_del" Text="删  除" Enabled="false" OnClick="Button_del_Click" runat="server" /><asp:Button ID="Button_sp" Text="审  核" Enabled="false" runat="server" />
				<input id="Button_view" type="button" value="查  看" onclick="ShowInfo();" disabled="disabled">
				<asp:Button ID="btnFiles" Enabled="false" Text="归档" OnClick="btnFiles_Click" runat="server" />
				<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="112" BusiClass="001" runat="server" />
			</td>
		</tr>
		<tr>
			<td valign="top">
				<div style="width: 100%; height: 100%">
					<asp:GridView ID="gvItemInpect" Width="100%" AutoGenerateColumns="false" AllowPaging="true" CssClass="gvdata" OnRowDataBound="gvItemInpect_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="ID,FlowState,UID" runat="server"><Columns><asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="20px"><HeaderTemplate>
									<asp:CheckBox ID="chkAll" runat="server" />
								</HeaderTemplate><ItemTemplate>
									<asp:HiddenField ID="HiddenField1_ID" Value='<%# Convert.ToString(Eval("ID")) %>' runat="server" />
									<asp:CheckBox ID="chk" runat="server" />
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" HeaderStyle-Width="20px">
<ItemTemplate>
									<asp:Image ID="Image1" Width="12px" Height="12px" runat="server" />
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="25px"><ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="检查单位" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
									<span class="link tooltip" title='<%# Eval("ExamineUnit").ToString() %>' onclick="newView('<%# Eval("ID") %>','<%=GetPrjName() %>',<%=CheckList._page_N %>)">
										<%# StringUtility.GetStr(Eval("ExamineUnit").ToString(), 10) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="检查内容" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
									<span class="tooltip" title='<%# Eval("AcceptCheckContent").ToString() %>'>
										<%# StringUtility.GetStr(Eval("AcceptCheckContent").ToString(), 10) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="受检单位" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
									<span class="tooltip" title='<%# Eval("AcceptCheckUnit").ToString() %>'>
										<%# StringUtility.GetStr(Eval("AcceptCheckUnit").ToString(), 10) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="检查依据" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
									<span class="tooltip" title='<%# Eval("AcceptCheckGist").ToString() %>'>
										<%# StringUtility.GetStr(Eval("AcceptCheckGist").ToString(), 10) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:BoundField DataField="ItemInspectSortName" HeaderText="检查类别" HeaderStyle-HorizontalAlign="Center" /><asp:BoundField DataField="AcceptCheckAnswerForPerson" HeaderText="负责人" HeaderStyle-HorizontalAlign="Center" /><asp:BoundField DataField="AcceptCheckDate" HeaderStyle-HorizontalAlign="Center" HeaderText="检查日期" DataFormatString="{0:D}" /><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
									<%# Common2.GetState(Eval("FlowState").ToString()) %>
								</ItemTemplate></asp:TemplateField><asp:BoundField Visible="false" DataField="ExamineApproveResult" ReadOnly="true" HeaderText="审核结果" HeaderStyle-HorizontalAlign="Center" /><asp:TemplateField HeaderText="附件" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
									<%# GetAnnx(Eval("ID").ToString(), Eval("CheckResult").ToString(), Eval("IsRectified").ToString()) %>
								</ItemTemplate></asp:TemplateField></Columns><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><RowStyle CssClass="rowa"></RowStyle><PagerStyle CssClass="GD-Pager"></PagerStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				</div>
			</td>
		</tr>
	</table>
	<JWControl:JavaScriptControl ID="JavaScriptControl1" runat="server"></JWControl:JavaScriptControl>
	</form>
</body>
</html>
