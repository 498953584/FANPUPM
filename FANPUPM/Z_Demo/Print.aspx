<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print.aspx.cs" Inherits="Z_Demo_Print" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link rel="Stylesheet" type="text/css" href="/Script/Print/css/print-preview.css" media="screen" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/Print/jquery.print-preview.js" charset="utf-8"></script>
	<script type="text/javascript">
		$(function () {
			$('#aside').prepend('<a class="print-preview">Print this page</a>');
			//$('a.print-preview').printPreview();
			$('#btnPrint').printPreview();

		});
	</script>
</head>
<body>
	<input type="button"  value="print" id="btnPrint"/>
	
	<div id="content" class="container_12 clearfix">
		
		<div id="aside" class="grid_3 push_1">
			aside
		</div>
	</div>
</body>
</html>
