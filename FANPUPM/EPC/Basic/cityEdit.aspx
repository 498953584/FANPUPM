<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cityEdit.aspx.cs" Inherits="EPC_Basic_cityEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnCancel').click(function () {
                winclose('cityEdit', 'CityList.aspx', false)
            });
            Val.validate('form1');
            // 城市编码只能是字母
            $('#txtCityCode').keyup(function () {
                $('#txtCityCode').val($('#txtCityCode').val().replace(/[^\a-\z\A-\Z]/g, ''));
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div class="divContent2">
        <table class="tableContent2" >
            <tr>
                <td class="word">
                    城市名称
                </td>
                <td class="elemTd">
                    <asp:TextBox ID="txtCityName" BackColor="#FEFEF4" Height="15px" CssClass="{required:true, messages:{required:'城市名称必须输入!'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    城市编码
                </td>
                <td class="elemTd">
                    <asp:TextBox ID="txtCityCode" BackColor="#FEFEF4" Height="15px" CssClass="{required:true, messages:{required:'城市编码必须输入!'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClientClick="return valForm()" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hlfdProvinceID" runat="server" />
    <asp:HiddenField ID="hlfdCityID" runat="server" />
    </form>
</body>
</html>
