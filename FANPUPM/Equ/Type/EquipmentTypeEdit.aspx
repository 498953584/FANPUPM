<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipmentTypeEdit.aspx.cs" Inherits="Equ_Type_EquipmentTypeEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>设备分类设置</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Val.validate('form1');
            $('#btnSave').click(check);
        })
        function check() {
            var code = $('#txtEquipmentName').val();
            if (code.toString().length > 20) {
                top.ui.alert('设备类别名称不大于20个字');
                return false;
            }
        }   
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divContent" style="height: 190px;">
        <table width="98%" cellpadding="5px" cellspacing="0">
            <tr>
                <td style="text-align: right; width: 80px;">
                    设备类别编号
                </td>
                <td align="left">
                    <asp:TextBox ID="txtEquipmentNo" BackColor="#FEFEF4" Height="15px" Width="40px" CssClass="{required:true, messages:{required:'设备类别编号必须输入'}}" runat="server"></asp:TextBox><span style="color: Red; font-weight: bold;">注:同级编号不可重复</span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 60px;">
                    设备类别名称
                </td>
                <td>
                    <asp:TextBox ID="txtEquipmentName" BackColor="#FEFEF4" Height="15px" Width="100%" CssClass="{required:true, messages:{required:'设备类别名称必须输入'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div style="text-align: right; padding-right: 10px;">
        <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
        <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin()" />
    </div>
    <asp:HiddenField ID="hdGuid" runat="server" />
    <asp:HiddenField ID="hfldParentId" runat="server" />
    </form>
</body>
</html>
