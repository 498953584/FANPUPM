<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewPersonRecordFrame.aspx.cs" Inherits="oa_WorkManage_NewPersonRecordFrame" %>

<html>
<head runat="server"><title></title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <script language="javascript" type="text/javascript">
    <!--
	-->
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td valign="top" width="20%">
                    <asp:TreeView ID="TVDept" ShowLines="true" runat="server"><SelectedNodeStyle ForeColor="Red" /></asp:TreeView>
                </td>
                <td height="100%" valign="top">
                    <iframe id="frmInStoreroom" name="frmInStoreroom" frameborder="0" width="100%" height="100%" src="NewPersonRecord.aspx?cc=" runat="server"></iframe>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
