<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="EditBulletin.aspx.cs" Inherits="EditBulletin" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>公告维护</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

	<script src="/StockManage/script/Config2.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/Kindeditor/kindeditor-min.js"></script>
	<script type="text/javascript" src="/Script/Kindeditor/lang/zh_CN.js"></script>
	<script type="text/javascript" src="/Script/Kindeditor/plugins/code/prettify.js"></script>
	<script type="text/javascript" src="/UserControl/FileUpload/uploadify/jquery.uploadify.min.js"></script>
	<script type="text/javascript" language="javascript">
		var editor1;
		KindEditor.ready(function (K) {
			editor1 = K.create('#txtContent', {
				cssPath: '/Script/Kindeditor/plugins/code/prettify.css',
				uploadJson: '/Script/Kindeditor/upload_json.ashx',
				fileManagerJson: '/Script/Kindeditor/file_manager_json.ashx',
				//allowFileManager : true,
				afterCreate: function () {
					var self = this;
					K.ctrl(document, 13, function () {
						self.sync();
						K('form[name=example]')[0].submit();
					});
					K.ctrl(self.edit.doc, 13, function () {
						self.sync();
						K('form[name=example]')[0].submit();
					});
				},
				items: [
                    'source', '|', 'undo', 'redo', '|',
                    'preview', 'print', 'code', 'cut', 'copy', 'paste', 'plainpaste', 'wordpaste', '|',
                    'justifyleft', 'justifycenter', 'justifyright', 'justifyfull', 'insertorderedlist', 'insertunorderedlist', 'indent', 'outdent', 'subscript',
                    'superscript', 'clearhtml', 'quickformat', 'selectall', '|',
                    'fullscreen', 'formatblock', 'fontname', 'fontsize', '|',
                    'forecolor', 'hilitecolor', 'bold', 'italic', 'underline', 'strikethrough', 'lineheight', 'removeformat', '|',
                    'image', 'table', 'hr', 'emoticons', 'baidumap', 'pagebreak',
                    'anchor', 'link', 'unlink',
                    ]

			});
			prettyPrint();
			// 自适应高度
			var height = K.IE ? editor1.edit.doc.body.scrollHeight : editor1.edit.doc.body.offsetHeight + 20;
			if (height < 400) {
				height = 400;
			}
			editor1.edit.setHeight(height);
		});
		window.name = "win";
		method = top.ui.callback;
		function selChange() {
			var opts = document.all("RBLtBound");
			if (opts[1].checked) {
				document.getElementById('bm').style.display = "";
			}
			else if (opts[2].checked) {
				document.getElementById('bm').style.display = "none";
			}
		}
		function selDept(userCode) {
			var dept = document.getElementById('hdnDept').value;
			var url = "/oa/common/selDept.aspx?yhdm=" + userCode + "&dept=" + dept;
			var depInfo = { url: url, title: '选择部门', width: 350, height: 500, winNo: 2 };
			top.ui.callback = setValue;
			top.ui.openWin(depInfo);
		}
		function setValue(user) {
			document.getElementById('hdnDept').value = user.code;
			document.getElementById('tbDept').value = user.name;
			top.ui.callback = method;
		}
		function UpFile(t) {
			//t=2 为公告管理
			var RecordCode = document.getElementById('hdnRecordId').value; //编号
			var url = "../../CommonWindow/Annex/AnnexList.aspx?mid=" + t + "&rc=" + RecordCode + "&at=0&type=2";
			window.showModalDialog(url, window, 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
		}
		function closeWin() {
			top.ui.show('保存成功!');
			top.ui.closeWin();
			if (top.ui.callback != null) {
				top.ui.callback();
				top.ui.callback = null;
			}
		}
		function cancel() {
			winclose('EditBulletin', 'BulletinManage.aspx', false);
		}

	</script>
</head>
<body class="body_popup" scroll="auto">
	<form id="form1" target="win" runat="server">
	<table id="TableMain" cellspacing="0" cellpadding="0" width="100%" border="1" align="center"
		class="table-normal">
		<tr>
			<td colspan="2" class="td-head">
				公告维护
			</td>
		</tr>
		<tr>
			<td align="right" class="td-label">
				范围：
			</td>
			<td>
				<asp:RadioButtonList ID="RBLtBound" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0" onclick="javascript:selChange();" runat="server"><asp:ListItem Value="1" Selected="true" Text="内部公告" /><asp:ListItem Value="2" Text="外部公告" /></asp:RadioButtonList>
				<div id="bm">
					&nbsp;<input type="hidden" id="hdnDept" name="hdnDept" style="width: 98px" runat="server" />

				</div>
			</td>
		</tr>
		<tr>
			<td align="right" class="td-label">
				针对部门：
			</td>
			<td>
				<asp:TextBox ID="tbDept" Width="352px" Enabled="false" runat="server"></asp:TextBox><input id="btnSelDept" type="button" class="button-normal" value="选择部门" runat="server" />

			</td>
		</tr>
		<tr>
			<td align="right" class="td-label">
				标题：
			</td>
			<td>
				<asp:TextBox ID="TBoxTitle" Width="350px" MaxLength="100" runat="server"></asp:TextBox><input id="HdnBulletinID" style="width: 1px; height: 1px" type="hidden" name="HdnBulletinID" runat="server" />

			</td>
		</tr>
		<tr>
			<td align="right" class="td-label">
				失效日期：
			</td>
			<td>
				<asp:TextBox ID="dbExpriseDate" Width="180px" onclick="WdatePicker()" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td align="right" class="td-label">
				附件：
			</td>
			<td>
				<MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="附件" FileType="*.*" Class="Bulletin" runat="server" />
			</td>
		</tr>
		<tr style="display: none">
			<td align="right" class="td-label">
				URL地址：
			</td>
			<td>
				<asp:TextBox ID="tbUrl" Width="350px" MaxLength="100" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td align="right" class="td-label">
				内容：
			</td>
			<td>
				<textarea id="txtContent" cols="100" rows="50" style="width: 100%; height: 100%;
					visibility: hidden;" runat="server"></textarea>
			</td>
		</tr>
		<tr>
			<td colspan="2" align="right">
				<input type="hidden" id="hdnRecordId" name="hdnRecordId" style="width: 10px" runat="server" />

				<asp:Button ID="BtnAdd" Width="60px" CssClass="button-normal" Text="确 定" OnClick="BtnAdd_Click" runat="server" />&nbsp;
				<input type="button" id="btnCancel" class="button-normal" value="取消" onclick="cancel();" />
			</td>
		</tr>
	</table>
	<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
	<asp:HiddenField ID="hfldCallBack" runat="server" />
	</form>
</body>
</html>
