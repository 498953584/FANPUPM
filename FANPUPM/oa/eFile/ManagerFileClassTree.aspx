<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagerFileClassTree.aspx.cs" Inherits="oa_eFile_ManagerFileClassTree" %>
<%@ Register TagPrefix="MyUserControl" TagName="oa_booksmanage_ucbooksort_ascx" Src="~/oa/BooksManage/UCBookSort.ascx" %>




<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>档案管理</title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self"/>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
         <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">            
            <tr>
                <td valign="top">
                <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                    <asp:TreeView ID="TreeView1" ShowLines="true" runat="server"><SelectedNodeStyle ForeColor="Red" /></asp:TreeView>                 
                   </div>
                </td>
                
            </tr>
        </table>      
    </div>
    </form>
</body>
</html>
