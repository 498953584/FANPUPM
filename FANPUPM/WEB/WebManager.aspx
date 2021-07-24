<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="WebManager.aspx.cs" Inherits="WebManager" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>企业门户管理</title><link type="text/css" rel="Stylesheet" href="../../UserControl/FileUpload/uploadify/uploadify.css" />
<link rel="stylesheet" href="/Script/Kindeditor/themes/default/default.css" />
<link rel="stylesheet" href="/Script/Kindeditor/plugins/code/prettify.css" />
<link type="text/css" rel="Stylesheet" href="../../UserControl/FileUpload/uploadify/uploadify.css" />

	<script src="/StockManage/script/Config2.js" type="text/javascript"></script>
	<link rel="stylesheet" href="/Script/Kindeditor/themes/default/default.css" />
<link rel="stylesheet" href="/Script/Kindeditor/plugins/code/prettify.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../../Script/Kindeditor/kindeditor-min.js"></script>
	<script type="text/javascript" src="/Script/Kindeditor/lang/zh_CN.js"></script>
	<script type="text/javascript" src="/Script/Kindeditor/plugins/code/prettify.js"></script>
	<script type="text/javascript" src="/UserControl/FileUpload/uploadify/jquery.uploadify.min.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/JavaScript">

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
			if (height < 500) {
				height = 500;
			}
			editor1.edit.setHeight(height);
		});
	</script>
</head>
<body ms_positioning="FlowLayout">
	<form id="Form1" method="post" runat="server">
	<table class="table-none" id="Table1" cellspacing="1" cellpadding="1" width="100%"
		border="1" style="border: 1px solid rgb(204,204,204); height: 100%;">
		<tr style="width: 100%; height: 5%;">
			<td colspan="2" align="center" class="td-title">
				<asp:Label ID="LbNewsName" runat="server"></asp:Label>管理
			</td>
		</tr>
		<tr style="width: 100%; height: 5%;">
			<td>
				<asp:Label ID="LbNewsName1" runat="server"></asp:Label>标题:
			</td>
			<td width="80%">
				<asp:TextBox ID="TbxNewsBt" Width="80%" CssClass="text-normal" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr style="width: 100%; height: 5%;">
			<td>
				添加附件
			</td>
			<td width="80%">
				<MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="附件" FileType="*.*" Class="News" runat="server" />
			</td>
		</tr>
		<tr style="width: 100%; height: 80%;">
			<td colspan="2" valign="top" align="center" style="width: 100%; height: 100%; padding: 0px;
				margin: 0px;">
				<textarea id="txtContent" style="width: 100%; visibility: hidden; padding: 0px; margin: 0px;" runat="server"></textarea>
			</td>
		</tr>
		<tr style="width: 100%; height: 5%;">
			<td align="center" colspan="2" class="td-submit">
				<asp:Button ID="Btn_save" CssClass="button-normal" Text="保 存" OnClick="Btn_save_Click" runat="server" />&nbsp;
				<input class="button-normal" id="Btn_close" onclick="winclose('WebManager', 'WebManagerList.aspx', false)"
					type="button" value="取 消">&nbsp;
			</td>
		</tr>
		<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
	</table>
	<asp:HiddenField ID="hdnNewID" runat="server" />
	<asp:HiddenField ID="hdnRecorde" runat="server" />
	</form>
</body>
</html>
