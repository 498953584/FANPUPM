<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TemplateTypeEdit.aspx.cs" Inherits="BudgetManage_Template_TemplateTypeEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>模板类型</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript">
        window.onload = function() {
            if (getRequestParam('t') == '1') {
                setAllInputDisabled();
            }
            document.getElementById('btnCancel').onclick = function() {
            winclose('TemplateTypeEdit', 'TemplateTypeList.aspx', false)
            }
            Val.validate('form1');
        }
          
    </script>

    <style type="text/css">
        #FileLink1_But_UpFile
        {
            width: auto;
        }
        #FileLink1_Btn_Upload
        {
            width: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader">
        <table class="tableHeader" style="display: none;">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="divContent2">
        <table class="tableContent2" >
            <tr>
                <td class="word">
                    类型名称
                </td>
                <td class="elemTd">
                   <asp:TextBox ID="txtTypeName" MaxLength="1000" BackColor="#FEFEF4" Height="15px" CssClass="{required:true, messages:{required:'类型名称必须输入'}}" runat="server"></asp:TextBox>
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
    <asp:HiddenField ID="hdGuid" runat="server" />
    </form>
</body>
</html>
