<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjTrv.aspx.cs" Inherits="PrjTrv" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wucprjtree_ascx" Src="~/EPC/UserControl1/wucprjtree.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>WUCPrjTree</title><link href="/StockManage/skins/sm4.css" rel="stylesheet" type="text/css" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />

		<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
		<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<body onload="selrow3('WUCPrjTree2_tvProjectt1')">
		<form id="Form1" method="post" style="overflow-x:auto; overflow-y:auto;" runat="server">
	         <table class="tab" style="border-collapse: collapse; top:inherit;" cellpadding="0" border="0"
            cellspacing="0">
              <tr style="height:100%; width:230px;">
              <td style="width: 230px; overflow:auto" valign="top">
			<MyUserControl:epc_usercontrol1_wucprjtree_ascx ID="WUCPrjTree2" runat="server" />
		   </td>
		  
		   </tr>
		   </table>
		</form>
	</body>
</HTML>
