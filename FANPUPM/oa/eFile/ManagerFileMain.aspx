<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagerFileMain.aspx.cs" Inherits="oa_eFile_ManagerFileMain" %>




<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>档案管理</title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self"/>
</head>
<body class="body_clear" scroll="no">  
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="1" id="tablex" class="table-normal">
            <tr>
                <td width="25%" valign="top">  
                <iframe id="frmFileClass" name="frmFileClass" frameborder="0" width="100%" height="100%" runat="server"></iframe>                  
                </td>
                <td width="75%">
                    <iframe id="frmFileList" name="frmFileList" src="ProjectFileManage.aspx" frameborder="0" width="100%" height="100%" runat="server"></iframe>
                </td>
            </tr>
        </table>        
    </form>
</body>
</html>
