<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestRun.aspx.cs" Inherits="Z_Demo_TestRun" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
	<script type="text/javascript" src="../Script/jquery.js"></script>
	<script type="text/javascript">
		//		$.ajax({
		//			type: "POST",
		//			url: "/Handler/Run.ashx?assembly=WebUtil&className=cn.justwin.Web.StringHelper&method=E",
		//			success: function (data) {
		//				alert(data);
		//			}
		//		});
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<asp:Button ID="Button1" Text="Button" OnClick="Button1_Click" runat="server" />
	</div>
	</form>
</body>
</html>
