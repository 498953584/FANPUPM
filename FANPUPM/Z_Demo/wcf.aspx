<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wcf.aspx.cs" Inherits="Z_Demo_wcf" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
	<script type="text/javascript" src="../Script/jquery.js"></script>
	<script type="text/javascript" src="../Script/jw.js"></script>
	<script type="text/javascript" src="../Script/json2.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {

			$('#btnMail').click(function () {
				$.ajax({
					url: "../mailservice.svc/mails/0ad168f9-4ede-47f3-b7be-b501ea041ba7",
					contenttype: 'application/json',
					success: function (data) {
						for (var i in data) {
							alert(i + '\n' + data[i]);
						}
					},
					error: function (ex) { alert('err'); }
				});
			});

			$('#btnDelMail').click(function () {
				$.ajax({
					url: '../MailService.svc/Mails/123',
					type: 'DELETE',
					contentType: 'application/json',
					success: function (data) {
						alert(data);
					}
				});
			});


			$('#btnPutMail').click(function () {
				$.ajax({
					url: '../MailService.svc/Mails',
					type: 'PUT',
					data: '{"id": "1bb6b1a4-1d36-460c-a66f-b01d5947f309", "name": "namenamesdfsdf---"}',
					dataType: 'json',
					contentType: 'application/json',
					success: function (data) {
						alert(data);
					}
				});
			});


			$('#btnPc').click(function () {
				$.ajax({
					url: "../PCPettyCashService.svc/PCPettyCash/124e797f-86c5-4a14-b5ac-41deb9ea29d9",
					contentType: 'application/json',
					success: function (data) {
						for (var i in data)
							alert(data[i]);
					},
					error: function (ex) { alert('err'); }
				});
			});

			$('#btnYhmc').click(function () {
				$.ajax({
					url: "../PTYhmcService.svc/PTYhmc/00000000",
					contentType: 'textapplication/json',
					success: function (data) {
						for (var i in data)
							alert(data[i]);
					},
					error: function (ex) { alert('err'); }
				});
			});

			$('#btnPtmk').click(function () {
				$.ajax({
					url: '../PTMKService.svc/GetAll/06000012/0',
					contenttype: 'application/json',
					success: function (data) {
						alert(JSON.stringify(data));
					},
					error: function (ex) { alert('err'); }
				});
			});

			$('#project').click(function () {
				$.ajax({
					url: '../PTPrjInfoService.svc/Project/BC6455D7-759D-4A77-A551-04B09E44D9BF',
					contenttype: 'application/json',
					success: function (data) {
						alert(JSON.stringify(data));
					},
					error: function (ex) { JSON.stringify(ex); }
				});
			});

			$('#trea').click(function () {
				$.ajax({
					url: '../SmTreasuryService.svc/Treasury/0ae8d83f-de9c-4a1f-9ab7-89cb259d17c1',
					contenttype: 'application/json',
					success: function (data) {
						alert(JSON.stringify(data));
					},
					error: function (ex) { JSON.stringify(ex); }
				});
			});

			// 查询该用户属于哪几个项目(根据项目小组成员查找)
			$('#btnUserProject').click(function () {
				$.ajax({
					url: 'http://jw01:8010/PTPrjInfoService.svc/UserProject/0542',
					contenttype: 'application/json',
					dataType: "jsonp",
					success: function (data) {
						if (data) {
							alert(data.length)									// 返回数据的个数
							alert(data[0].PrjCode + '\n' + data[0].PrjName)		// 第一个项目的项目编号和名称
						}
					},
					error: function (ex) { JSON.stringify(ex); }
				});
			});
		});
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<input type="button" value="mail" id="btnMail" />
		<input type="button" value="delMail" id="btnDelMail">
		<input type="button" value="putMail" id="btnPutMail">
		<input type="button" value="pc" id="btnPc" />
		<input type="button" value="yhmc" id="btnYhmc" />
		<input type="button" value="ptmk" id="btnPtmk" />
		<input type="button" value="project" id="project" />
		<input type="button" value="trea" id="trea" />
		<input type="button" value="UserProject" id="btnUserProject" />
	</div>
	</form>
</body>
</html>
