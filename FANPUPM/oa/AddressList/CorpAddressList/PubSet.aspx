<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PubSet.aspx.cs" Inherits="oa_AddressList_CorpAddressList_PubSet" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <base target="_self">
   <script type="text/javascript" language="javascript">
    <!--
       window.name = "win";
       -->
    </script>
    <script type="text/javascript" src="../../../Script/jquery.js"></script>

    <script src="../../../StockManage/script/Config2.js" type="text/javascript"></script>

    <script src="../../../StockManage/script/JustWinTable.js" type="text/javascript"></script>

    <script src="../../../SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../../Script/jw.js"></script>
    <style type="text/css">
        .style1
        {
            width: 90px;
        }
    </style>
</head>

<body>
    <form id="form1" target="win" runat="server">
    <div class="divContent2" style =" margin-bottom :-20px;">
        <asp:HiddenField ID="hidBmdm" runat="server" />
        <table style="width:100%;">
            <tr>
                <td colspan="2" class="divHeader">
                    公共信息</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;&nbsp;
                    地 址：</td>
                <td>
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txtaddress" Width="300px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;&nbsp;
                    邮 编：</td>
                <td>
                 &nbsp;&nbsp;
                    <asp:TextBox ID="txtyb" Width="300px" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="style1">
                    &nbsp;&nbsp;
                    传 真：</td>
                <td>
                 &nbsp;&nbsp;
                    <asp:TextBox ID="txtfx" Width="300px" runat="server"></asp:TextBox>
                 </td>
            </tr>
             <tr>
                <td colspan="2" class="divFooter" style="height: 100%; text-align: right;" >
                    <asp:Button ID="btnSave" Text="确 定" OnClick="btnSave_Click" runat="server" />
                    &nbsp;
                    <input id="btnclose" type="button" value="取 消"  onclick="top.ui.closeWin();" />
                 </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
