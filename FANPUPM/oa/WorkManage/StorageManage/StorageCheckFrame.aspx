<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageCheckFrame.aspx.cs" Inherits="oa_WorkManage_StorageManage_StorageCheckFrame" %>
<%@ Register TagPrefix="MyUserControl" TagName="mywucstorage" Src="~/oa/WorkManage/StorageManage/WUCStorage.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title></title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td width="20%" valign="top">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                        <MyUserControl:mywucstorage ID="WUCStorage" runat="server" />
                    </div>
                </td>
                <td width="80%">
                    <iframe id="frmMatter" name="frmMatter" src="StorageCheck.aspx?dd=" frameborder="0" width="100%" height="100%" runat="server"></iframe>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
