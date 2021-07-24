<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddBalanceItem.aspx.cs" Inherits="ContractManage_IncometBalance_AddBalanceItem" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <link type="text/css" rel="Stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Val.validate('form1');
        });

        //计算综合单价/小计
        function setValue() {
            //修改数量或综合单价，计算出小计并赋值给小计
            //数量
            var quantity = 0;
            if (Trim($('#txtQty').val()) != '') {
                quantity = parseFloat($('#txtQty').val().replace(/\,/g, ''));
            }
            //综合单价
            var unitPrice = 0;
            if (Trim($('#txtUnitPrice').val()) != '') {
                unitPrice = parseFloat($('#txtUnitPrice').val().replace(/\,/g, ''));
            }
            //小计
            var total = quantity * unitPrice;
            if (isNaN(total)) {
                total = 0;
            }
            $('#txtTotal').val(_formatDecimal(total));
        }

        function btnSave() {
            if (typeof top.ui.callback == 'function') {
                top.ui.callback();
            }
            top.ui.closeWin({ parentName: '_AddBalanceItem' });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divContent2">
        <table cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    扣费类型
                </td>
                <td>
                    <asp:DropDownList ID="dropType" Width="100%" runat="server"><asp:ListItem Value="1" Text="管理扣项" /><asp:ListItem Value="2" Text="结算增减项" /><asp:ListItem Value="3" Text="代扣代缴税金" /></asp:DropDownList>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td class="word">
                    扣费名称
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtName" CssClass="{required:true, messages:{required:'扣费名称必须填写！'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    工程量
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtQty" CssClass="easyui-numberbox {required:true, messages:{required:'工程量必须填写！'}}" data-options="min:-99999999999,max:99999999999,precision:3,groupSeparator:','" onBlur="setValue();" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    单价
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtUnitPrice" onBlur="setValue();" CssClass="easyui-numberbox {required:true, messages:{required:'单价必须填写！'}}" data-options="min:-99999999999,max:99999999999,precision:3,groupSeparator:','" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    小计
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtTotal" ReadOnly="true" CssClass="easyui-numberbox" data-options="min:-99999999999,max:99999999999,precision:3,groupSeparator:','" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" id="td1">
                    附件
                </td>
                <td colspan="3" style="text-align: left; padding-right: 0px;" class="txt">
                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="fileBalanceItem" Class="ContractBudBalance" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="txtNode" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
                </td>
            </tr>
        </table>
    </div>
    
    <asp:HiddenField ID="hfldBalanceID" runat="server" />
    
    <asp:HiddenField ID="hfldBalanceItemId" runat="server" />
    
    <asp:HiddenField ID="hfldconBalanceItem" runat="server" />
    </form>
</body>
</html>
