<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemInfoMain.aspx.cs" Inherits="oa_System_SystemInfoMain" %>
<%@ Register TagPrefix="MyUserControl" TagName="oa_booksmanage_ucbooksort_ascx" Src="~/oa/BooksManage/UCBookSort.ascx" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>制度管理</title>
    <script language=javascript>
    function frmop()
    {
          <%if (base.Request["audit"] == null)
{%>
			window.document.getElementById("frmSysList").src='SystemInfoList.aspx?rt=2&isFirst=1&ctc=002'
			<%}
else
{%>
			 window.document.getElementById("frmSysList").src='SystemInfoList.aspx?rt=2&isFirst=1&audit=&ctc=002';
			<%}%>
    }
    </script>
</head>
<body class="body_clear" scroll="no" onload="frmop()">
    <form id="form1" runat="server">
    <div>
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="1" id="tablex" class="table-normal">
            <tr>
                <td width="25%" valign="top">  
                    <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                    <asp:TreeView ID="TreeView1" ShowLines="true" runat="server"><SelectedNodeStyle ForeColor="Red" /></asp:TreeView>                 
                   </div>
                           
                </td>
                <td width="75%">
                    <iframe id="frmSysList" name="frmSysList" src="SystemInfoList.aspx?rt=2&isFirst=1&audit=&ctc=002" frameborder="0" width="100%" height="100%" runat="server"></iframe>
                </td>
            </tr>
        </table> 
    </div>
    </form>
</body>
</html>
