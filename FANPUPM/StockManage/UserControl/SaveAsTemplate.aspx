<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SaveAsTemplate.aspx.cs" Inherits="StockManage_UserControl_SaveAsTemplate" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
     <script type="text/javascript" src="/StockManage/script/Validation.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript">
        window.onload = function() {
//            if (getRequestParam('t') == '1') {
//                setAllInputDisabled();
//            }
            Val.validate('form1');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style=" text-align:center; height:80%;">
        <table>
            <tr>
                <td class="word">
                    类型名称
                </td>
                <td class="txt">
                   <asp:TextBox ID="txtTypeName" BackColor="#FEFEF4" Height="15px" CssClass="{required:true, messages:{required:'类型名称必须输入'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div style="text-align: right;">
        <asp:Button ID="btnSave" Text="保存" OnClientClick="return valForm()" OnClick="btnSave_Click" runat="server" />
        <input type="button" id="btnCancel" value="取消" onclick="window.close();"/>
    </div>
    </form>
</body>
</html>
