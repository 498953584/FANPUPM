<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="InstitutionListEdit.aspx.cs" Inherits="Enterprise_InstitutionListEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>企业制度</title>
	<script type="text/javascript" src="../../../Script/My97DatePicker/WdatePicker.js"></script>
	<link type="text/css" rel="Stylesheet" href="/UserControl/FileUpload/uploadify/uploadify.css" />
<link rel="stylesheet" href="/Script/Kindeditor/themes/default/default.css" />
<link rel="stylesheet" href="/Script/Kindeditor/plugins/code/prettify.css" />
<link type="text/css" rel="Stylesheet" href="/UserControl/FileUpload/uploadify/uploadify.css" />
<link rel="stylesheet" href="/Script/Kindeditor/themes/default/default.css" />
<link rel="stylesheet" href="/Script/Kindeditor/plugins/code/prettify.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

	<script src="/StockManage/script/Config2.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/Kindeditor/kindeditor-min.js"></script>
	<script type="text/javascript" src="/Script/Kindeditor/lang/zh_CN.js"></script>
	<script type="text/javascript" src="/Script/Kindeditor/plugins/code/prettify.js"></script>
	<script type="text/javascript" src="/UserControl/FileUpload/uploadify/jquery.uploadify.min.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			try {
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
					if (height < 430) {
						height = 430;
					}
					editor1.edit.setHeight(height);
				});
			} catch (err) {
				alert(err);
			}
		});

		function select() {
			$('#TRCopy').show();
			$('#img').hide();
		}
		function ValidTXT() {
			if (document.getElementById("txtInsName").value == "" || document.getElementById("txtInsName").value.length == 0) {
				alert('制度名称不能为空!');
				return false;
			}
			if (document.getElementById("txtInsCode").value == "" || document.getElementById("txtInsCode").value.length == 0) {
				alert('编码不能为空!');
				return false;
			}
			if (document.getElementById("txtClassName").value == "" || document.getElementById("txtClassName").value.length == 0) {
				alert('请选择分类！');
				return false;
			}
		}
		function upLoadFile() {
			var rc = document.getElementById("HdnInsCode").value;
			var uri = "/CommonWindow/Annex/AnnexList.aspx?mid=138&rc=" + rc + "&at=0";
			var fth = "dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;resizable:0;";
			return window.showModalDialog(uri, '', fth);
		}
		function searchClass() {
			var uri = "InsClassTree.aspx";
			var fth = "width=250px,height=350px,location=no,menubar=no,toolsbar=no,status=no";
			window.open(uri, '', fth);
		}
		
	</script>
</head>
<body class="body_clear" scroll="auto">
	<form id="form1" runat="server">
	<table class="table-normal" style="width: 100%" cellspacing="0" border="1">
		<tr>
			<td class="td-label" style="width: 100px">
				名称：
			</td>
			<td>
				<asp:TextBox ID="txtInsName" Width="99%" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="td-label">
				编号：
			</td>
			<td>
				<asp:TextBox ID="txtInsCode" Width="99%" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="td-label">
				分类名：
			</td>
			<td>
				<asp:TextBox ID="txtClassName" runat="server"></asp:TextBox>
				<input type="button" style="width: 40px;" id="btnOpen" value="选择" onclick="searchClass();"
					class="button-normal" />
				<input type="hidden" id="HdnClassCode" style="width: 1px;" runat="server" />

			</td>
		</tr>
		<tr>
			<td class="td-label">
				签发人：
			</td>
			<td>
				<asp:TextBox ID="txtIssuPerson" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="td-label">
				签发日期：
			</td>
			<td>
				<asp:TextBox ID="txtIssuDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="td-label">
				内容：
			</td>
			<td>
				<textarea id="txtContent" cols="100" rows="50" style="width: 100%; height: 100%;
					visibility: hidden;" runat="server"></textarea>
			</td>
		</tr>
		<tr>
			<td class="td-label">
				附件：
			</td>
			<td>
				<MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="附件" FileType="*.*" Class="Institution" runat="server" />
			</td>
		</tr>
		<tr>
			<td class="td-submit" colspan="2">
				<div style="margin-right: 15px;">
					<asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />
					<input type="button" id="btnClose" value="关 闭" onclick="winclose('InstitutionListEdit', 'InstitutionList.aspx', false)" />
				</div>
			</td>
		</tr>
	</table>
	<input type="hidden" id="HdnInsCode" style="width: 1px" runat="server" />

	</form>
</body>
</html>
