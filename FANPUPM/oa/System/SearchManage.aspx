<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchManage.aspx.cs" Inherits="oa_System_SearchManage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head id="Head1" runat="server"><title>制度查询</title><meta http-equiv="Expires" content="0" />
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
                <td width="25%" height="20"  valign="top">
                    <table width="100%" height="100%"   cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td height="20" valign="top">
                                <asp:DropDownList ID="drpbm" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="drpbm_SelectedIndexChanged" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <iewc:TreeView ID="tv" AutoSelect="true" SelectExpands="true" ShowToolTip="false" ExpandLevel="1" SelectedStyle="color:red;background-color:#ffffff;" runat="server"></iewc:TreeView>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="75%">
                    <iframe id="rFrame" name="rFrame" src="Search_right.aspx?cid=0" frameborder="0" width="100%" height="100%" runat="server"></iframe>
                </td>
            </tr>
        </table>
		
	</form>
	</body>
</HTML>
