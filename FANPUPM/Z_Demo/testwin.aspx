<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testwin.aspx.cs" Inherits="Z_Demo_testwin" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
	<script type="text/javascript" src="jquery-1.8.2.js"></script>
	<script type="text/javascript">
		//		function btn1_click() {
		//			top._closeWin();
		//			//			alert(top.callback)
		//			top.callback(document.getElementById('txt1').value);

		//		}
		//		window.onload = function () {
		//			//document.getElementById('txt2').setAttribute('type', 'password');
		//			document.getElementById('rad').setAttribute('disabled', 'disabled');
		//		}

		$(document).ready(function () {
			$('#btnOk').click(function () {
				$('#btnOk').attr('disabled', true);
				 __doPostBack('btnOk', '');
			});
		});
		
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<input type="text" id="txt1" />
		<input type="button" id="btn1" value="btn1" onclick="btn1_click();" />
		<input type="text" id="txt2" value="4444444444444" />
		<input type="radio" value="111" id="rad" />
		<asp:Button ID="btnOk" Text="Button" OnClick="btnOk_Click" runat="server" />
	</div>
	<asp:ImageButton ID="ImageButton1" OnClick="ImageButton1_Click" runat="server" />
	</form>
</body>
</html>
