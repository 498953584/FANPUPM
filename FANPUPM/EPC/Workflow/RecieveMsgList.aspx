<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecieveMsgList.aspx.cs" Inherits="ModuleSet_Workflow_RecieveMsgList" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>被告知信息</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var jwTable = new JustWinTable('GVRecieveMsg');
			replaceEmptyTable('emptyTable', 'GVRecieveMsg');
		});

		function setHeight() {
			var height = document.getElementById("td-msg").clientHeight; ;
			document.getElementById("divMsg").style.height = height;
		}

		function ClickRow(recordid, BusinessCode, BusinessClass) {
			document.getElementById('HdnBusinessCode').value = BusinessCode;
			document.getElementById('HdnBusinessClass').value = BusinessClass;
			document.getElementById('HdnRecoreId').value = recordid;
			document.getElementById('btnViewWF').disabled = false;
			document.getElementById('btnWFPrint').disabled = false;
		}

		//多人选择
		function pickMutiPerson() {
			jw.selectMultiUser({ nameinput: 'txttolder', codeinput: 'hidtolder' });
		}

		//发起人选择
		function PickSingOrgioner() {
			jw.selectOneUser({ nameinput: 'txtOrganizer', codeinput: 'hidorganizer' });
		}

		//单人选择
		function pickSinglePerson() {
			jw.selectOneUser({ nameinput: 'txtsend', codeinput: 'hidsend' });
		}

		///查看审核
		function OpenViewWF() {
			var BusinessCode = document.getElementById('HdnBusinessCode').value;
			var BusinessClass = document.getElementById('HdnBusinessClass').value;
			var rid = document.getElementById('HdnRecoreId').value;
			var url = "/EPC/Workflow/AuditViewWF.aspx?ic=" + rid + '&bc=' + BusinessCode + '&bcl=' + BusinessClass;
			window.open(url, '', "left=100,top=50,width=600,height=460,status=0,toolbar=0,menubar=0,location=0,scrollbars=1,resizable=1");
		}
		//查看审核记录
		function OpenPrintWF() {
			var BusinessCode = document.getElementById('HdnBusinessCode').value;
			var BusinessClass = document.getElementById('HdnBusinessClass').value;
			var rid = document.getElementById('HdnRecoreId').value;
			var url = "/EPC/Workflow/AuditViewPrint.aspx?ic=" + rid + '&bc=' + BusinessCode + '&bcl=' + BusinessClass;
			window.open(url, '', "left=100,top=50,width=800,height=760,status=0,toolbar=0,menubar=0,location=0,scrollbars=1,resizable=1");
		}
		//查看
		function OpenLock(strurl, rid) {
			var url = strurl + "?ic=" + rid;
			window.open(url, '', "left=100,top=50,width=600,height=260,status=0,toolbar=0,menubar=0,location=0,scrollbars=1,resizable=1");
		}
	</script>
</head>
<body style="margin: 0 0 0 0">
	<form id="form1" runat="server">
	<table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center">
		<tr>
			<td style="vertical-align: top">
				<table cellpadding="0" cellspacing="0" width="100%" border="0">
					<tr>
						<td>
							<table class="queryTable" cellpadding="3px" cellspacing="0px">
								<tr>
									<td class="descTd">
										流程类别
									</td>
									<td>
										<asp:DropDownList ID="DDLBusinessClass" Width="160px" runat="server"></asp:DropDownList>
									</td>
									<td class="descTd">
										模板名称
									</td>
									<td>
										<asp:TextBox ID="txtTemplateName" runat="server"></asp:TextBox>
									</td>
									<td class="descTd">
										发起人
									</td>
									<td style="padding-right: 1px">
										<span class="spanSelect" style="width: 160px; float: left">
											<input type="text" id="txtOrganizer" readonly="readonly" style="width: 135px;
												height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

											<img alt="选择人员" onclick="PickSingOrgioner();" src="../../images/icon.bmp" style="float: right" />
										</span>
										<input type="hidden" id="hidOrganizer" runat="server" />

									</td>
									<td class="descTd">
										被告知人
									</td>
									<td>
										<span class="spanSelect" style="width: 158px; float: left">
											<input type="text" id="txttolder" readonly="readonly" style="width: 133px;
												height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

											<img alt="选择人员" onclick="pickMutiPerson();" src="../../images/icon.bmp" style="float: right" />
										</span>
										<input type="hidden" id="hidtolder" runat="server" />

									</td>
								</tr>
								<tr>
									<td class="descTd">
										告知人
									</td>
									<td>
										<span class="spanSelect" style="width: 158px; float: left">
											<input type="text" id="txtsend" readonly="readonly" style="width: 133px;
												height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

											<img alt="选择人员" onclick="pickSinglePerson();" src="../../images/icon.bmp" style="float: right" />
										</span>
										<input type="hidden" id="hidsend" runat="server" />

									</td>
									<td class="descTd">
										告知内容
									</td>
									<td>
										<asp:TextBox ID="txtContent" runat="server"></asp:TextBox>
									</td>
									<td class="descTd">
										告知时间
									</td>
									<td>
										<asp:TextBox ID="txtRecieveDate" Width="160px" onclick="WdatePicker()" runat="server"></asp:TextBox>
									</td>
									<td colspan="2">
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
			<td style="vertical-align: top; text-align: left;" class="divFooter">
				<asp:Button ID="btnViewWF" Enabled="false" Text="查看审核" Width="80px" runat="server" />
				<asp:Button ID="btnWFPrint" Text="查看审核记录" Enabled="false" Width="100px" runat="server" />&nbsp;
				<asp:HiddenField ID="HdnRecoreId" runat="server" />
				<asp:HiddenField ID="HdnBusinessClass" runat="server" />
				<asp:HiddenField ID="HdnBusinessCode" runat="server" />
				<asp:HiddenField ID="hiddisplay" runat="server" />
			</td>
		</tr>
		<tr>
			<td style="vertical-align: top; width: 100%" id="td-msg">
				<div id="divMsg">
					<asp:GridView ID="GVRecieveMsg" AllowPaging="false" AutoGenerateColumns="false" CssClass="gvdata" Width="100%" OnRowDataBound="GVRecieveMsg_RowDataBound" OnPageIndexChanging="GVRecieveMsg_PageIndexChanging" DataKeyNames="InstanceCode" runat="server">
<EmptyDataTemplate>
							<table id="emptyTable" border="1" cellspacing="0" rules="all" style="width: 100%;
								margin-left: 0; margin-right: 0">
								<tr class="header">
									<th align="center" scope="col" style="width: 40px">
										序号
									</th>
									<th align="center" scope="col">
										告知人
									</th>
									<th align="center" scope="col">
										被告知人
									</th>
									<th align="center" scope="col">
										告知内容
									</th>
									<th align="center" scope="col">
										流程类型
									</th>
									<th align="center" nowrap="nowrap" scope="col" style="width: 70px">
										模板名称
									</th>
									<th align="center" nowrap="nowrap" scope="col" style="width: 70px">
										审核内容
									</th>
									<th align="center" nowrap="nowrap" scope="col">
										发起人
									</th>
									<th align="center" nowrap="nowrap" scope="col">
										参与审核人
									</th>
									<th align="center" scope="col" style="width: 300px">
										告知时间
									</th>
								</tr>
							</table>
						</EmptyDataTemplate>
<RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
									<%# Eval("Num") %>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="告知人" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30px"><ItemTemplate>
									<%# GetUserName(Eval("RecieveUserCode").ToString()) %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="被告知人" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px"><ItemTemplate>
									<%# Eval("ToldUserNames").ToString() %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="告知内容" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
									<%# Eval("LookUrl").ToString() %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程类型" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px"><ItemTemplate>
									<%# Eval("BusinessClassName") %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="模板名称" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px"><ItemTemplate>
									<%# Eval("TemplateName") %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText=" 审核内容">
<ItemTemplate>
									<asp:HyperLink ID="HLConter" CssClass="firstpage" Style="color: Blue;" runat="server">
									[HLConter]
									</asp:HyperLink>
								</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="发起人" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30px"><ItemTemplate>
									<%# GetUserName(Eval("Organiger").ToString()) %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="参与审核人" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px"><ItemTemplate>
									<%# Eval("OperatNames") %>
								</ItemTemplate></asp:TemplateField><asp:BoundField DataField="RecieveDate" HeaderText="告知时间" /></Columns><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
					</webdiyer:AspNetPager>
				</div>
			</td>
		</tr>
	</table>
	</form>
</body>
</html>
