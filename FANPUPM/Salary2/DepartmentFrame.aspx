<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepartmentFrame.aspx.cs" Inherits="Salary2_DepartmentFrame" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>部门</title></head>
<body>
    <form id="form1" runat="server">
    <table class="tab" border="1" style="border-collapse: collapse;" cellpadding="0"
        cellspacing="0">
        <tr style="height: 100%;">
            <td style="width: 200px; height: 99%" valign="top">
                <div class="pagediv" style="heigth: 100%; width: 195px;">
                    <div id="div_project">
                        <asp:TreeView ID="tvDepartment" Font-Size="12px" ShowLines="true" OnSelectedNodeChanged="tvDepartment_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                    </div>
                </div>
            </td>
            <td valign="top" style="height: 99%; padding-top: 0; padding-left: 0; padding-bottom:0; margin-top: 0;
                margin-left: 0; margin-bottom:0; margin: 0">
                <div class="pagediv" style=" height:100%;">
                    <iframe id="InfoList" name="InfoList" frameborder="0" src="#" style="width: 100%;
                        height: 99%; margin-top: 0; margin-left: 0; margin-bottom:0; padding-top: 0; padding-left: 0; padding-bottom:0;" runat="server">
                    </iframe>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
