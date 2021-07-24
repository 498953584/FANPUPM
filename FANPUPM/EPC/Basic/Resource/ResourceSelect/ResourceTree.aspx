<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceTree.aspx.cs" Inherits="EPC_Basic_Resource_ResourceSelect_ResourceTree" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title></title></head>
<body style="margin:0px 0px 0px;" scroll="no">
    <form id="form1" runat="server">
    <table class="tab" style="width:100%;height:100%">
    <tr>
    <td>
    <div class="pagediv">
        <asp:TreeView ID="TVResTree" ExpandDepth="2" ForeColor="Black" ShowLines="true" runat="server"><SelectedNodeStyle CssClass="selectTree" /></asp:TreeView>
    </div>
    </td>
    </tr>
    </table>
        <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
