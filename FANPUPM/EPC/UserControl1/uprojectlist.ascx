<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uprojectlist.ascx.cs" Inherits="UProjectList" %>

<table cellspacing="0" cellpadding="0" height="100%" border="0">
    <tr>
        <td height="24">
            <asp:DropDownList ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" runat="server"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td valign="top">
            <asp:TreeView ID="tvProject" ShowLines="true" selectexpands="True" showtooltip="False" expandlevel="1" Height="100%" Width="200px" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
        </td>
    </tr>
</table>
