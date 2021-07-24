<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PostWeaveRoleSelect.aspx.cs" Inherits="HR_Organization_PostWeaveRoleSelect" %>

<html>
<head runat="server"><title>岗位编制信息</title><meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    <!--
	-->
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" method="post" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex">
            <tr>
                <td valign="top" width="50%">
                   <div class="gridBox">
                    <asp:TreeView ID="TVDept" ShowLines="true" ExpandDepth="1" runat="server"></asp:TreeView></div>
                </td>
                <td valign="top">
                    <iframe id="frmBooks" name="frmBooks" src="PostWeaveRoleSelectData.aspx?cc=" frameborder="0" width="100%" height="100%" runat="server"></iframe>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
