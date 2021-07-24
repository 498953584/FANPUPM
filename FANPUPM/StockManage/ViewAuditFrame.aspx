<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewAuditFrame.aspx.cs" Inherits="StockManage_ViewAuditFrame" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title></title>
    <style type="text/css">
    .stytable
    {		
	    border:solid 1px #000000;
	    border-collapse:collapse;
	    empty-cells:show;	
    }
    .stytable td
    {
        border-top: 0px solid #000000; border-bottom: 1px solid #000000;border-right:1px solid #000000;height: 22px;
    }
  
    </style>
    
</head>
<body class="body_clear">
    <form id="form1" runat="server">
  
    <div style="height:22px;width:100%;text-align:right;font-size:12px;" id="dvprint" >   
     <table cellpadding="0" cellspacing="0" border="1" class="stytable" width="98%" id="printTable" runat="server"></table>
    </div>
    </form>
</body>
</html>
