<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsFrame.aspx.cs" Inherits="WEB_NewsFrame" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>新闻管理</title></head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
    <table class="table-none" id="Table1" height="100%" cellSpacing="1" cellPadding="1" width="100%"
				border="1">
	<tr>
	<td width="180px" valign="top">
	<div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
        <JWControl:FreeTree ID="trNewsType" ExpandLevel="3" SelectedStyle="color:red;background-color:#ffffff;" runat="server"></JWControl:FreeTree>
        </div>
    </td>
	<td><iframe id="frmPage" name="frmPage" src="WebManagerList.aspx?c_xwlxdm=%&c_xwlxmc=" frameborder="0" width="100%" height="100%" runat="server"></iframe></td>
	</tr>
	</table>
    </div>
    </form>
</body>
</html>
