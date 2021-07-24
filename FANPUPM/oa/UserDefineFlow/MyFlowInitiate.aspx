<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyFlowInitiate.aspx.cs" Inherits="oa_UserDefineFlow_MyFlowInitiate" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>我发起的流程</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var jwTable = new JustWinTable('GVInitiate');
			replaceEmptyTable('emptyTable', 'GVInitiate');
		});

		function setHeight() {
			var height = document.getElementById("td-flow").clientHeight;
			document.getElementById("divgv").style.height = height - 10;
		}

		function ClickRow(recordid, BusinessCode, BusinessClass) {
			document.getElementById('HdnRecoreId').value = recordid;
			document.getElementById('btnViewWF').disabled = false;
			document.getElementById('btnWFPrint').disabled = false;
			document.getElementById('HdnBusinessCode').value = BusinessCode;
			document.getElementById('HdnBusinessClass').value = BusinessClass;
		}

		//多人选择
		function pickMutiPerson() {
			jw.selectMultiUser({ nameinput: 'txttolder', codeinput: 'hidtolder' });
		}
		// 单人选择
		function pickSinglePerson() {
			jw.selectOneUser({ nameinput: 'txtsend', codeinput: 'hidsend' });
		}

		// 查看审核
		function OpenViewWF() {
			var BusinessCode = document.getElementById('HdnBusinessCode').value;
			var BusinessClass = document.getElementById('HdnBusinessClass').value;
			var rid = document.getElementById('HdnRecoreId').value;
			var url = "/EPC/Workflow/AuditViewWF.aspx?ic=" + rid + '&bc=' + BusinessCode + '&bcl=' + BusinessClass;
			window.open(url, '', "width=600,height=460,status=0,toolbar=0,menubar=0,location=0,scrollbars=1,resizable=1");
		}
		// 查看审核记录
		function OpenPrintWF() {
			var BusinessCode = document.getElementById('HdnBusinessCode').value;
			var BusinessClass = document.getElementById('HdnBusinessClass').value;
			var rid = document.getElementById('HdnRecoreId').value;
			var url = "/EPC/Workflow/AuditViewPrint.aspx?ic=" + rid + '&bc=' + BusinessCode + '&bcl=' + BusinessClass;
			window.open(url, '', "width=800,height=760,status=0,toolbar=0,menubar=0,location=0,scrollbars=1,resizable=1");
		}
		//查看
		function OpenLock(strurl, rid) {
			var url = strurl + "?ic=" + rid;
			window.open(url, '', "width=600,height=260,status=0,toolbar=0,menubar=0,location=0,scrollbars=1,resizable=1");
		}
		//查看
		function OpenLook(strurl) {
			return window.open(strurl, '_blank', "left=100,top=50,width=600,height=400,status=0,toolbar=0,menubar=0,location=0,scrollbars=1,resizable=1", false);
		}
		function OpenRecieve() {
			if (document.getElementById("hiddisplay").value == "1") {
				document.getElementById("btndisplay").style.display = "none";
				document.getElementById("btnMoreSearch").style.display = "";
				document.getElementById("trmore").style.display = "";
			}
			else {
				document.getElementById("btndisplay").style.display = "none";
				document.getElementById("btnSearch").style.display = "none";
				document.getElementById("btnMoreSearch").style.display = "";
				document.getElementById("trmore").style.display = "";
			}

			var rid = document.getElementById('HdnRecoreId').value;
			var url = "RecieveEdit.aspx?ic=" + rid;
			return window.open(url, '_blank', "left=100,top=50,width=600,height=400,status=0,toolbar=0,menubar=0,location=0,scrollbars=1,resizable=1", false);
		}

		function setMoreDisplay() {
			document.getElementById("hiddisplay").value = "1";
			document.getElementById("btnSingle").style.display = "";
			document.getElementById("btndisplay").style.display = "none";
			document.getElementById("btnSearch").style.display = "none";
			document.getElementById("btnMoreSearch").style.display = "";
			document.getElementById("trmore1").style.display = "";
			document.getElementById("trmore2").style.display = "";
		}
		function setDisplay() {
			document.getElementById("hiddisplay").value = "0";
			document.getElementById("btndisplay").style.display = "";
			document.getElementById("btnSingle").style.display = "none";
			document.getElementById("btnSearch").style.display = "";
			document.getElementById("btnMoreSearch").style.display = "none";
			document.getElementById("trmore1").style.display = "none";
			document.getElementById("trmore2").style.display = "none";
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table style="width: 100%">
		<tr>
			<td>
				<table cellpadding="0" cellspacing="0" width="100%" border="0">
					<tr>
						<td>
							<table class="queryTable" cellpadding="3px" cellspacing="0px">
								<tr>
									<td class="descTd">
										流程类别
									</td>
									<td>
										<asp:DropDownList ID="DDLBusinessClass" Width="150px" runat="server"></asp:DropDownList>
									</td>
									<td class="descTd">
										模板名称
									</td>
									<td>
										<asp:TextBox ID="txttemplate" Width="148px" runat="server"></asp:TextBox>
									</td>
									<td class="descTd">
										发起时间
									</td>
									<td>
										<asp:TextBox ID="DBDate" Width="150px" onclick="WdatePicker()" runat="server"></asp:TextBox>
									</td>
								</tr>
								<tr>
									<td class="descTd">
										流程状态
									</td>
									<td>
										<asp:DropDownList ID="ddsing" Width="150px" runat="server"><asp:ListItem Value="" Text="所有" /><asp:ListItem Value="0" Text="审核中" /><asp:ListItem Value="1" Text="已审核" /><asp:ListItem Value="-2" Text="驳回" /><asp:ListItem Value="-3" Text="重报" /></asp:DropDownList>
									</td>
									<td class="descTd">
										告知人
									</td>
									<td>
										<span class="spanSelect" style="width: 148px; float: left">
											<input type="text" id="txtsend" readonly="readonly" style="width: 123px;
												height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

											<img alt="选择人员" onclick="pickSinglePerson();" src="../../images/icon.bmp" style="float: right" />
										</span>
										<input type="hidden" id="hidsend" runat="server" />

									</td>
									<td class="descTd">
										被告知人
									</td>
									<td>
										<span class="spanSelect" style="width: 152px; float: left">
											<input type="text" id="txttolder" readonly="readonly" style="width: 127px;
												height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

											<img alt="选择人员" onclick="pickMutiPerson();" src="../../images/icon.bmp" style="float: right" />
										</span>
										<input type="hidden" id="hidtolder" runat="server" />

									</td>
									<td colspan="4">
										<asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td style="height: 22px; text-align: left" class="divFooter">
				
				
				<asp:Button ID="btnViewWF" Enabled="false" Text="查看审核" Width="80px" runat="server" />
				<asp:Button ID="btnWFPrint" Text="查看审核记录" Enabled="false" Width="100px" runat="server" />&nbsp;
				<asp:HiddenField ID="HdnRecoreId" runat="server" />
				<asp:HiddenField ID="HdnBusinessCode" runat="server" />
				<asp:HiddenField ID="HdnBusinessClass" runat="server" />
			</td>
		</tr>
		<tr>
			<td id="td-flow" style="vertical-align: top; width: 100%">
				<div id="divgv">
					<asp:GridView ID="GVInitiate" AllowPaging="false" AutoGenerateColumns="false" CssClass="gvdata" Width="100%" OnRowDataBound="GVInitiate_RowDataBound" OnPageIndexChanging="GVInitiate_PageIndexChanging" DataKeyNames="ID" runat="server">
<EmptyDataTemplate>
							<table id="emptyTable">
								<tr class="header">
									<th scope="col">
										序号
									</th>
									<th scope="col">
										模板名称
									</th>
									<th scope="col">
										流程类别
									</th>
									<th scope="col">
										参与审核人
									</th>
									<th scope="col">
										审核内容
									</th>
									<th scope="col">
										流程状态
									</th>
									<th scope="col">
										告知人
									</th>
									<th scope="col">
										被告知人
									</th>
									<th scope="col">
										发起时间
									</th>
								</tr>
							</table>
						</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
									<%# Container.DataItemIndex + 1 + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="模版名称" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
									<span class="tooltip" title='<%# Eval("TemplateName").ToString() %>'>
										<%# StringUtility.GetStr(Eval("TemplateName"), 20) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程类别" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
									<span class="tooltip" title='<%# Eval("BusinessClassname").ToString() %>'>
										<%# StringUtility.GetStr(Eval("BusinessClassname"), 20) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:BoundField DataField="OperatorPerson" HeaderText="参与审核人" ReadOnly="true" /><asp:TemplateField HeaderText="审核内容">
<ItemTemplate>
									<asp:HyperLink ID="HLConter" CssClass="firstpage" Style="color: Blue;" runat="server">[HLConter]</asp:HyperLink>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="流程状态">
<ItemTemplate>
									<%# Common2.GetState(Eval("FlowState").ToString()) %>
								</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="ToldName" HeaderText="告知人" ReadOnly="true" /><asp:TemplateField HeaderText="被告知人" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
									<span class="tooltip" title='<%# Eval("TellName").ToString() %>'>
										<%# StringUtility.GetStr(Eval("TellName"), 32) %>
									</span>
								</ItemTemplate></asp:TemplateField><asp:BoundField DataField="StartTime" HtmlEncode="false" HeaderText="发起时间" SortExpression="StartTime" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
					</webdiyer:AspNetPager>
				</div>
			</td>
		</tr>
	</table>
	</form>
</body>
</html>
