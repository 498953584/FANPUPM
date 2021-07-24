<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PickCorpRole.aspx.cs" Inherits="EPC_Workflow_PickCorpRole" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <base target="_self" />
    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript">
        //给父页面设置值
        function setVal() {
            var human = window.dialogArguments;
            human[0] = document.getElementById('hfldCorpCode').value;
            //human[1] = document.getElementById('hfldCorpName').value;

            window.returnvalue = human;
            window.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table class="tab">
        <tr>
            <td valign="top" style="height: 420px">
                <div class="pagediv">
                    <asp:TreeView ID="TvBranch" Width="100%" ShowLines="true" ShowCheckBoxes="All" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                <input id="btnCancel" type="button" value="取 消" class="button-normal" onclick="window.close()" />
            </td>
        </tr>
    </table>
    
    <asp:HiddenField ID="hfldCorpCode" runat="server" />
    
    <asp:HiddenField ID="hfldCorpName" runat="server" />
    </form>
</body>
</html>
