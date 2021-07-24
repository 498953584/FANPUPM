<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MailFrame.aspx.cs" Inherits="oa_MailAdmin_MailFrame" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>无标题页</title></head>
<body class="body_frame" scroll=no>
    <form id="form1" runat="server">
    <div>
    <table id="table1" width="100%" height="100%" cellpadding=0 cellspacing=0 border="1" class="table-normal">
    <tr>
    <td width="120px" valign="top">

        <asp:TreeView ID="TreeView1" ShowLines="true" runat="server"><Nodes /><SelectedNodeStyle ForeColor="Red" /><NodeStyle ForeColor="Black" /></asp:TreeView>
	</td>
    <td valign="top"><iframe id="iframeMail" name="iframeMail" width="100%" height="100%" frameborder="0"  src="NewMail.aspx"></iframe></td>
    </tr></table>
    </div>
    </form>
</body>
</html>
