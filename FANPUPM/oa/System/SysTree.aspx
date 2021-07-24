<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysTree.aspx.cs" Inherits="oa_System_SysTree" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>制度查询</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<body>
		<form id="Form1" method="post" runat="server">
			
			
		<table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">
                       <asp:DropDownList ID="drpbm" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="drpbm_SelectedIndexChanged" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="top" height="100%">
                   <iewc:TreeView ID="tv" AutoSelect="true" SelectExpands="true" ShowToolTip="false" ExpandLevel="1" runat="server"></iewc:TreeView>
                </td>
            </tr>
        </table>
		</form>
	</body>
</HTML>
