<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MatterManage.aspx.cs" Inherits="oa_WorkManage_StorageManage_MatterManage" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title></title><meta http-equiv="Expires" content="0" />
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
                <td width="15%" valign="top">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <iewc:TreeView ID="TVBookClass" ExpandLevel="1" SelectedStyle="color:red;background-color:#ffffff;" runat="server"></iewc:TreeView>
                    </div>
                </td>
                <td width="85%">
                    <iframe id="frmMatter" name="frmMatter" src="MatterBill.aspx?uc=&cc=" frameborder="0" width="100%" height="100%" runat="server"></iframe>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
