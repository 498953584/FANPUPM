<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectByNote.aspx.cs" Inherits="StockManage_SmPurchaseplan_SelectByNote" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_uprojectlist_ascx" Src="~/EPC/UserControl1/uprojectlist.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>从申请单中选择物资</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../script/Config2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if (getRequestParam("Type") == 'check') {
                document.getElementById('InfoList').src = 'SelectByNoteList.aspx?Type=check';
            } else {
                document.getElementById('InfoList').src = 'SelectByNoteList.aspx';
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="table-none" width="100%" border="1" style="border-collapse: collapse;
        height: 100%;">
        <tr>
            <td style="width: 200px; border: solid 0px red;" valign="top">
                <div style="border: solid 0px red; width: 200px; height: 430px; overflow: auto; padding-top: 0px;
                    margin-top: 0px;">
                    <MyUserControl:epc_usercontrol1_uprojectlist_ascx ID="uproject" runat="server" />
                </div>
            </td>
            <td valign="top">
                <iframe id="InfoList" name="InfoList" frameborder="0" width="100%;" scrolling="no" height="440" runat="server"></iframe>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdwscodeP" runat="server" />
    <asp:HiddenField ID="hdType" runat="server" />
    </form>
</body>
</html>
