<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wucprjtree.aspx.cs" Inherits="WUCPrjTree1" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wucprjtree_ascx" Src="~/EPC/UserControl1/wucprjtree.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>WUCPrjTree</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<link href="/StockManage/skins/sm4.css" rel="stylesheet" type="text/css" />
<link href="/StyleCss/Common.css" rel="stylesheet" type="text/css" />

		<script language="javascript" >
		function selrow(obj)
		{
		 <%if (base.Request["Type"] != null && base.Request["Type"] != "ShenHe")
{%>
                 try
			    {
			     document.getElementById(obj).click();
                }
                catch(e){}
		<%}%>
		}
		</script>
	</head>
	<body onload="selrow('WUCPrjTree3_tvProjectt1')" style="margin:0px;">
		<form id="Form1" method="post" runat="server">
		<table cellpadding="0" cellspacing="0" border="0" width="100%" height="100%">
		    <tr>
		        <td><!--<DIV class="gridBox" align=left style="margin:0px;"><P align="left"></P></DIV>-->
			        <MyUserControl:epc_usercontrol1_wucprjtree_ascx ID="WUCPrjTree3" runat="server" />
		        </td>
		    </tr>
		</table>
		</form>
	</body>
</HTML>
