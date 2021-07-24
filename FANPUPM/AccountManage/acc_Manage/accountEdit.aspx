<%@ Page Language="C#" AutoEventWireup="true" CodeFile="accountEdit.aspx.cs" Inherits="AccountManage_acc_Manage_accountEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script src="../../StockManage/script/Config2.js" type="text/javascript"></script>

    <script src="../../StockManage/script/JustWinTable.js" type="text/javascript"></script>

    <script src="../../SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
    
<script type ="text/javascript" language ="javascript" >
    //员工选择
    function openPeopleSelect() {

        var url = "Consignee.aspx";
        var human = new Array();
        window.showModalDialog(url, human, "dialogHeight:475px;dialogWidth:480px;center:1;help:0;status:0;");

    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div style =" width :100%;">
        <table width ="100%">
           <tr>
                <td colspan="4" class="divHeader">
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="资金账号设置" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="divContent2" style =" margin-bottom :-20px;">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            
            <tr>
                <td>
                    账户编号
                </td>
                <td>
                    <asp:TextBox ID="txtaccountNum" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td>
                    别名
                </td>
                <td>
                    <asp:TextBox ID="txtacountName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    账号权限
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtauthorizer" TextMode="MultiLine" Columns="3" runat="server"></asp:TextBox> 
                    <asp:Button ID="btnSelect" Text="员工选择" Width="80px" OnClientClick="openPeopleSelect()" OnClick="btnSelect_Click" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtremark" TextMode="MultiLine" Columns="3" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div style =" width :100%;">
        <table width ="100%">
            <tr>
                <td style="height: 100%; text-align: right;" colspan="2" class="divFooter">
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <asp:Button ID="btnCancel" Text="取消" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
