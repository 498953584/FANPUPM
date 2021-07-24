<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zgsSysManage.aspx.cs" Inherits="oa_System_zgsSysManage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head id="Head1" runat="server"><title>子公司制度管理</title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self"/>
    <script language="javascript" type="text/javascript">
    
    </script>
</head>
	<body class="body_clear" scroll="no">
	<form id="formx" runat="server">
        <table width="100%" height="100%"   cellpadding="0" cellspacing="0" border="1" id="tablex" class="table-normal">
            <tr>
                <td width="25%" valign="top" style="height: 369px">
                 <iewc:TreeView ID="tv" AutoSelect="true" SelectExpands="true" ShowToolTip="false" ExpandLevel="1" runat="server"></iewc:TreeView>
                </td>
                <td width="75%" style="height: 369px">
                    <iframe id="rFrame" name="rFrame" src="zgsgl_right.aspx?cid=0" frameborder="0" width="100%" height="100%" runat="server"></iframe>
                </td>
            </tr>
        </table>
		
	</form>
	</body>
</HTML>
