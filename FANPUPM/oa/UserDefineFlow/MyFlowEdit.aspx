<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyFlowEdit.aspx.cs" Inherits="oa_UserDefineFlow_MyFlowEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>申请事项维护</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script src="/StockManage/script/Config2.js" type="text/javascript"></script>
	<script type="text/javascript">
		window.name = "win";
		$(function () {
			//alert(top.ui.tabSuccess);
			//alert(window.location.href);
		});
		function UpFile() {
			var RecordCode = document.getElementById('HdnRecordCode').value; //编号
			var url = "../../../commonWindow/Annex/AnnexList.aspx?mid=88&rc=" + RecordCode + "&at=0";
			var ref = window.showModalDialog(url, window, 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
			return true;
		}
		function setHeight() {
			var height = document.getElementById("td-frmpage").clientHeight;
			document.getElementById("frmPage").style.height = height - 24;

		}
		function successed() {
			var url = "/oa/UserDefineFlow/MyFlow.aspx?type=" + $('#hfdNodeValue').val();
			top.ui.tabSuccess({ url: url, parentName: '_userdefineflow' });
		}
	</script>
</head>
<body onload="setHeight();">
	<form id="Formx" runat="server">
	<table class="tableAudit" cellspacing="0" cellpadding="0" width="100%" border="1">
		<tr>
			<td class="divHeader" colspan="2" height="22px">
				申请事项
			</td>
		</tr>
		<tr>
			<td class="td-label" width="20%" style="height: 22px">
				申请标题
			</td>
			<td class="td-content">
				<asp:TextBox ID="txtLayoutName" CssClass="text-input" Style="width: 120px" MaxLength="50" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="td-label" width="20%" style="height: 22px">
				附件
			</td>
			<td class="td-content">
				<MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="附件" Class="Audit" runat="server" />
			</td>
		</tr>
		<tr>
			<td class="td-label" width="20%" style="height: 100px">
				审核事项
			</td>
			<td class="td-content">
				<asp:TextBox ID="txtRemark" CssClass="text-input" MaxLength="250" Style="width: 99%" TextMode="MultiLine" Rows="6" runat="server"></asp:TextBox>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtRemark" Display="None" ErrorMessage="审核事项必填" runat="server"></asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<td colspan="2" style="text-align: right">
				<asp:Button ID="btnAdd" Text="保  存" OnClick="btnAdd_Click" runat="server" />&nbsp;
				<input id="BtnClose" onclick="top.ui.closeTab();" type="button" value="关  闭" />
				<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
				<input id="HdnRecordCode" type="hidden" style="width: 20px" runat="server" />

				<asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
			</td>
		</tr>
		<tr>
			<td colspan="2" id="td-frmpage">
				<table style="width: 100%; height: 100%">
					<tr>
						<td class="divHeader" height="22px">
							审核流程
						</td>
					</tr>
					<tr>
						<td style="height: 100%">
							<iframe id="frmPage" name="frmPage" src="/EPC/Workflow/WorkFlowChartView.aspx?tid=<%=Request["tid"] %>"
								frameborder="0" width="100%"></iframe>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hfdNodeValue" runat="server" />
	</form>
</body>
</html>
