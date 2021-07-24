<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadifyDemo.aspx.cs" Inherits="UploadifyDemo" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link type="text/css" rel="Stylesheet" href="/UserControl/FileUpload/uploadify/uploadify.css" />

	<style type="text/css">
		.uploadify-button
		{
			background-color: transparent;
			border: 0;
			padding: 0;
		}
		
		.uploadify:hover .uploadify-button
		{
			background-color: transparent;
			border: 0;
			padding: 0;
		}
		
	</style>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/UserControl/FileUpload/uploadify/jquery.uploadify.min.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			// 上传路径
			var folder = $('#hfldFolder').val();

			$(function () {
				$("#uploadify").uploadify({
					buttonImage: '/UserControl/FileUpload/uploadify/browser2.png',
					swf: '/UserControl/FileUpload/uploadify/uploadify.swf',
					uploader: '/UserControl/FileUpload/FileUpload.ashx?folder=' + folder,
//					height: 20,
//					width: 70//,
					onUploadSuccess: uploadComplete
				});

//				$('#uploadify-button').html('');

			});

			// 删除完成事件
			function uploadComplete(file) {
				alert(file.name);
//				var $tr = $('<tr></tr>');
//				var $tdName = $('<td style="width: 60%;">' + file.name + '</td>');
//				var $tdSize = $('<td style="width: 25%;">' + file.size / 1048576 + 'M</td>');
//				var $tdDel = $('<td></td>');

//				var $img = $('<img class="deleteAnnex" style="cursor:pointer" src="/images/cancel_2.png" alt="删除" />')
//				$img.click(function () {
//					deleteAnnex(this, folder);
//				});
//				$tdDel.append($img);

//				$tr.append($tdName);
//				$tr.append($tdSize);
//				$tr.append($tdDel);
//				$('#annexTab').append($tr);
			}

			// 删除附件
			function deleteAnnex(img) {
				var $tr = $(img).parent().parent();
				var annexName = $tr.find('td').first().html();

				$.get('/UserControl/FileUpload/DeleteFile.ashx?' + new Date().getTime(), { File: folder + '/' + annexName }, function (data) {
					if (data == '1') {
						$tr.remove();
					}
					else {
						alert('系统提示：\n\n删除失败！');
					}
				});
			}
		});



	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<table id="annexTab" class="gvdata" style="width: 500px;" runat="server"></table>
		<input type="file" name="uploadify" id="uploadify" />
	</div>
	<asp:HiddenField ID="hfldFolder" runat="server" />
	</form>
</body>
</html>
