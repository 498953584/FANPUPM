<%@ Page Language="C#" AutoEventWireup="true" CodeFile="affairview.aspx.cs" Inherits="EPC_QuaitySafety_affairview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/Watermark/jquery_Watermark.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var mark = document.getElementById("hdnmark").value;
            if (mark == 1) {
                GetWaterMarked(window, '/images/yinzh.jpg', 'this');
            }
        }
    </script>
</head>
<body scroll="no" style="height: 100%;">
    <form id="form1" runat="server">
    <div class="divContent2">
        <table width="100%">
            <tr>
                <td class="divHeader" style="width: 100%">
                    <%=EPC_QuaitySafety_affairview._showAffairTitle.Substring(0, 4) %>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdnmark" runat="server" />
        <table border="0" cellspacing="0" cellpadding="0" width="100%" class="viewTable">
            <tr>
                <td class="descTd" style="white-space: nowrap;">
                    附件
                </td>
                <td class="elemTd" colspan="3">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="descTd" style="white-space: nowrap;">
                    <%=EPC_QuaitySafety_affairview._showAffairTitle %>
                </td>
                <td class="elemTd">
                    <asp:Literal ID="litName" runat="server"></asp:Literal>
                </td>
                <td class="descTd" style="white-space: nowrap;">
                    发生日期
                </td>
                <td class="elemTd">
                    <asp:Literal ID="litDate" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="descTd" style="white-space: nowrap;">
                    作为归档资料
                </td>
                <td class="elemTd">
                    <asp:Literal ID="litGD" runat="server"></asp:Literal>
                </td>
                <td class="descTd" style="white-space: nowrap;">
                    归档类别
                </td>
                <td class="elemTd" style="white-space: nowrap;">
                    <JWControl:DropDownTree ID="DDTClass" Visible="false" runat="server"></JWControl:DropDownTree>
                    <asp:Literal ID="litType" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="descTd" style="white-space: nowrap;">
                    内容
                </td>
                <td class="elemTd" colspan="3">
                    <asp:Literal ID="litContent" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="descTd" style="white-space: nowrap;">
                    备注
                </td>
                <td class="elemTd" colspan="3">
                    <asp:Literal ID="litRemark" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
