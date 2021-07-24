<%@ Page Language="C#" AutoEventWireup="true" CodeFile="contactcorplist.aspx.cs" Inherits="ContactCorpList" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>单位信息</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../web_client/tree.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$('#Txt_CorpSearch').hide();
			$('#btnActiv').hide();
			$('#BtnAudit').hide();
			//如果只有一页的数据,隐藏页码
			hideFirstPageNo();

			var cti = jw.getRequestParam('cti');
			// 合格供应商、合格分包商、内部分包商
			if (cti == '5' || cti == '10' || cti == '19') {
				$("#btnStartWF").show();
				$("#CancelBt").show();

			}
			else {
				$("#btnStartWF").hide();
				$("#CancelBt").hide();
			}

			if (jw.getRequestParam('type') == '3') {
				// 失效单位管理
				$('#btnActiv').show();
			}

			var ic = jw.getRequestParam('ic');
			if (ic != '' && cti == '') {
				// 审核页面
				$('#hiderD').hide();
				$('.td-toolsbar').hide();
			}
		});

		function doClickRow(corpID, corpName, FlowGuid, AuditState, UserCode, CTI) {
			document.getElementById('hdnRecordID').value = FlowGuid;
			document.getElementById('HdnCorpID').value = corpID;
			document.getElementById('HdnCorpName').value = corpName;
			document.getElementById('btnActiv').disabled = false;

			/***********************************
			*流程审核
			********************************/
			if (document.getElementById('WF1_FID') != null)
				document.getElementById('WF1_FID').value = FlowGuid;
			if (CTI == 4) {
				document.getElementById('WF1_BusinessCode').value = "042";
			}
			else if (CTI == 5) {
				document.getElementById('WF1_BusinessCode').value = "044";
			}
			else if (CTI == 10 || CTI == 19 || CTI == 20) {
				document.getElementById('WF1_BusinessCode').value = "045";
			}
			if (document.getElementById('WF1_BusinessClass') != null)
				document.getElementById('WF1_BusinessClass').value = "001";
			if (document.getElementById('WF1_BusinessClass').value == "" || document.getElementById('WF1_BusinessCode').value == "") {
				document.getElementById('btnStartWF').style.display = "none";     //
				document.getElementById("CancelBt").style.display = "none";
			}
			document.getElementById("WF1_hidcontent").value = "供应商信息" + corpName + "审核";          //查看审核记录是显示审核内容在这里设置                
			if (AuditState == "-1")  //未提交状态
			{
				if (document.getElementById('btnStartWF') != null)
					document.getElementById('btnStartWF').disabled = false;     //可提交 
				if (document.getElementById('WF1_btnViewWF') != null)
					document.getElementById('WF1_btnViewWF').disabled = true;   //不可查看流程状态
				if (document.getElementById('WF1_btnWFPrint') != null)
					document.getElementById('WF1_btnWFPrint').disabled = true;  //不可查看审核记录
			}
			if (AuditState == "-3")   //重报状态
			{
				if (document.getElementById('btnStartWF') != null)
					document.getElementById('btnStartWF').disabled = false;      //可提交
				if (document.getElementById('WF1_btnViewWF') != null)
					document.getElementById('WF1_btnViewWF').disabled = false;   //可查看流程状态
				if (document.getElementById('WF1_btnWFPrint') != null)
					document.getElementById('WF1_btnWFPrint').disabled = false;  //可查看审核记录   
			}
			if (AuditState == "1" || AuditState == "0" || AuditState == "-2")   //以通过或者审核中驳回时
			{
				if (document.getElementById('btnStartWF') != null)
					document.getElementById('btnStartWF').disabled = true;
				if (document.getElementById('WF1_btnViewWF') != null)
					document.getElementById('WF1_btnViewWF').disabled = false;
				if (document.getElementById('WF1_btnWFPrint') != null)
					document.getElementById('WF1_btnWFPrint').disabled = false;
			}
			if (AuditState == "0")//状态为0时才能撤销
			{
				document.getElementById("CancelBt").disabled = false;
			}
			else {
				document.getElementById("CancelBt").disabled = true;
			}
			if (AuditState == "1" || AuditState == "0" || AuditState == "-3" || AuditState == "-2")   //彻底删除的可用性
			{
				if (document.getElementById('BtnWFDel') != null)
					document.getElementById('BtnWFDel').disabled = false;
			}
			else {
				if (document.getElementById('BtnWFDel') != null)
					document.getElementById('BtnWFDel').disabled = true;
			}
			if ((AuditState == "-1")) {
				document.getElementById('BtnMod').disabled = false;
				document.getElementById('BtnAudit').disabled = false;
				document.getElementById('BtnDel').disabled = false;
			}
			else {
				document.getElementById('BtnMod').disabled = true;
				document.getElementById('BtnDel').disabled = true;
				document.getElementById('BtnAudit').disabled = true;
			}
			if ('<%=this.isAudit %>' == "Audit") {
				var url = "/ModuleSet/Workflow/FlowList.aspx?ic=" + FlowGuid + "&pt=1";
				document.getElementById("FraFlow").src = url;
			}
		}

		function openContractEdit(op) {
			var corpID = 0;
			var title = '新增单位';
			if (op == 2) {
				// 编辑
				corpID = document.getElementById('HdnCorpID').value;
				title = '编辑单位';
			}
			var corpType = document.getElementById('HdnTypeID').value;
			var url = "/EPC/Basic/ContactCorpEdit.aspx?t=" + op + "&ci=" + corpID + "&cti=" + corpType;
			top.ui._contractcorplist = window;
			toolbox_oncommand(url, title);
		}

		function confirmCorp() {
			corpID = document.getElementById('HdnCorpID').value;
			corpName = document.getElementById('HdnCorpName').value;
			doDblClickRow(corpID, corpName);
		}

		function doDblClickRow(corpID, corpName) {
			var corp = parent.window.dialogArguments;
			corp[0] = corpID;
			corp[1] = corpName;
			parent.window.returnvalue = corp;
			window.close();
		}

		// 单位审核
		function OpenAudit() {
			var rid = document.getElementById('HdnCorpID').value;
			var url = "/HR/Leave/ApplicationReq.aspx?ic=" + rid + "&mid=DeptAtu";
			return window.showModalDialog(url, window, "dialogHeight:230px;dialogWidth:390px;center:1;help:0;status:0;");
		}
	</script>
</head>
<body class="body_frame" scroll="no">
	<form id="Form1" method="post" runat="server">
	<table class="table-none" height="100%" cellspacing="0" cellpadding="0" width="100%"
		border="0">
		<tr>
			<td class="td-title" height="20">
				单 位 信 息
			</td>
		</tr>
		<tr id="hiderD">
			<td align="left" height="24">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td>
							单位名称<asp:TextBox ID="txtCorpName" Width="120px" runat="server"></asp:TextBox>
							单位性质<asp:DropDownList ID="DDL_CorpKind" Width="120px" runat="server"></asp:DropDownList>
							单位联系人<asp:TextBox ID="txtLinkMan" Width="120px" runat="server"></asp:TextBox>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td class="td-toolsbar" style="text-align: left; margin-left: 0px; padding-left: 0px;"
				height="20">
				<input id="HdnCorpID" style="width: 10px" type="hidden" name="HdnCorpID" runat="server" />

				<input id="HdnTypeID" style="width: 10px" type="hidden" name="HdnTypeID" runat="server" />

				<input id="HdnCorpName" style="width: 10px" type="hidden" name="HdnCorpName" runat="server" />

				<input id="hdnRecordID" style="width: 10px" type="hidden" name="hdnRecordID" runat="server" />

				<asp:TextBox ID="Txt_CorpSearch" Width="0px" runat="server"></asp:TextBox>
				<asp:Button ID="BtnAdd" Text="新  增" Width="0px" OnClick="BtnAdd_Click" runat="server" />
				<asp:Button ID="BtnMod" Text="编  辑" Enabled="false" Width="0px" OnClick="BtnMod_Click" runat="server" />
				<asp:Button ID="BtnDel" Text="删  除" Enabled="false" Width="0px" OnClick="BtnDel_Click" runat="server" />
				<asp:Button ID="btnpzdel" Text="彻底删除" OnClick="btnpzdel_Click" runat="server" />
				<input id="BtnSearch" type="button" name="BtnSearch" visible="false" OnServerClick="BtnSearch_ServerClick" runat="server" />

				<asp:Button ID="btnExp" CssClass="button-normal" Text="导 出" OnClick="btnExp_Click1" runat="server" />
				<asp:Button ID="btnActiv" Text="激  活" Width="0px" Enabled="false" OnClick="btnActiv_Click" runat="server" />
				<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="044" BusiClass="001" runat="server" />
				<asp:Button ID="BtnAudit" Text="审  核" Width="0px" OnClick="BtnAudit_Click" runat="server" />
				<asp:Button ID="BtnClose" Text="关  闭" Visible="false" runat="server" />
			</td>
		</tr>
		<tr>
			<td valign="top">
				<div>
					<asp:DataGrid ID="DgrdList" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="15" CssClass="grid" DataKeyField="CorpID" OnPageIndexChanged="DgrdList_PageIndexChanged" runat="server"><AlternatingItemStyle Wrap="false" CssClass="grid_row"></AlternatingItemStyle><PagerStyle Mode="NumericPages" CssClass="GD-Pager"></PagerStyle><ItemStyle Wrap="false" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head" VerticalAlign="Middle"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
									<%# Container.ItemIndex + 1 + this.DgrdList.PageSize * this.DgrdList.CurrentPageIndex %>
								</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="CorpName" HeaderText="单位名称"></asp:BoundColumn><asp:TemplateColumn HeaderText="推荐供应商">
<ItemTemplate>
									<%# (Eval("IsHot").ToString().Trim() == "Y") ? "是" : "否" %>
								</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="CorpKind" HeaderText="单位性质"></asp:BoundColumn><asp:BoundColumn DataField="WorkType" HeaderText="经营类别"></asp:BoundColumn><asp:BoundColumn DataField="Speciality" HeaderText="产品概况"></asp:BoundColumn><asp:BoundColumn DataField="LinkMan" HeaderText="联系人"></asp:BoundColumn><asp:BoundColumn DataField="Telephone" HeaderText="联系电话"></asp:BoundColumn><asp:BoundColumn HeaderText="流程状态" DataField="AuditState" Visible="true"></asp:BoundColumn></Columns></asp:DataGrid></div>
			</td>
		</tr>
		<tr id="TrFraFlow" height="30%">
			<td valign="top" colspan="2">
				<iframe id="FraFlow" name="FraFlow" src="" frameborder="no" width="100%" height="100%">
				</iframe>
			</td>
		</tr>
	</table>
	<script>
		if ('<%=this.isAudit %>' == 'DEL') {
			document.getElementById('hiderD').style.display = 'none';
		}
		if ('<%=this.isAudit %>' != 'Audit') {
			document.getElementById("TrFraFlow").style.display = 'none';
		}
		else {
			if ('<%=CorpTypeID %>' == '7') {
				document.getElementById('TrFraFlow').style.display = 'block';
			}
			else {
				document.getElementById('TrFraFlow').style.display = 'none';
			}
		}
	</script>
	<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
	</form>
</body>
</html>
