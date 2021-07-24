<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Json.aspx.cs" Inherits="Z_Demo_Json" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
	<script type="text/javascript" src="../Script/json2.js"></script>
	<script type="text/javascript">
		var arr = new Array();
		var obj = {};
		obj.id = '1';
		obj.name = "aaa"
		arr.push(obj);
		var obj2 = {};
		obj.id = '2';
		obj.name = "bb"

		var str = JSON.stringify(arr);
		alert(str);


		eval(str);

	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	</div>
	</form>
</body>
</html>
