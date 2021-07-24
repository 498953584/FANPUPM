<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquDiscardEdit.aspx.cs" Inherits="Equ_EquipmentDiscard_EquDiscardEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>设备报废</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js" charset="gb2312"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Val.validate('form1', 'btnSave');
            $('#btnCancel').click(btnCancelClick);
            $('#txtApplicant').attr('readonly', 'readonly');
            $('#txtApplyDate').attr('readonly', 'readonly');
        })
        function btnCancelClick() {
            winclose('EquDiscardEdit', 'EquDiscardList.aspx', false);
        }
        //选择设备
        function selectEqu() {
            var url = '/Equ/Equipment/SelectEquipment.aspx?' + new Date().getTime();
            top.ui.callback = function (equInfo) {
                $('#hfldEquId').val(equInfo.id);
                $('#txtEquCode').val(equInfo.code);
                $('#txtSpecification').val(equInfo.specification);
                $('#txtPurchasePrice').val(_formatDecimal(equInfo.PurchasePrice));
                $('#txtPurchaseDate').val(equInfo.PurchaseDate);
            };
            top.ui.openWin({ url: url, title: '选择设备', width: 800, height: 500 });
        }
        // 选择人员 
        function selectUser(codeInput, nameInput) {
            jw.selectOneUser({ codeinput: codeInput, nameinput: nameInput });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    设备
                </td>
                <td class="txt mustInput">
                    <span class="spanSelect" style="width: 100%">
                        <input type="text" id="txtEquCode" class="{required:true, messages:{required:'设备编号不能为空！'}}" readonly="readonly" style="width: 88%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                        <img alt="选择报废设备" onclick="selectEqu();" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldEquId" runat="server" />
                </td>
                <td class="word">
                    规格
                </td>
                <td>
                    <input type="text" id="txtSpecification" class="readonly" readonly="readonly" style="width: 100%; height: 15px;" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    资产原值
                </td>
                <td>
                    <input type="text" id="txtPurchasePrice" value="0.000" class="readonly" readonly="readonly" style="width: 100%; height: 15px;" runat="server" />

                </td>
                <td class="word">
                    购置日期
                </td>
                <td>
                    <input type="text" id="txtPurchaseDate" class="readonly" readonly="readonly" style="width: 100%; height: 15px;" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    已提折旧
                </td>
                <td>
                    <asp:TextBox ID="txtAlreadyDepreciations" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    资产净值
                </td>
                <td>
                    <asp:TextBox ID="txtNetWorth" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    报废原因
                </td>
                <td>
                    <asp:TextBox ID="txtReason" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    申请人
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtApplicant" data-options="validType:'validQueryChars[50]'" Style="width: 90%; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择申请人" onclick="selectUser('hfldApplicant','txtApplicant');" src="../../../images/icon.bmp"
                            style="float: right;" />
                        <asp:HiddenField ID="hfldApplicant" runat="server" />
                    </span>
                </td>
            </tr>
            <tr>
                <td class="word">
                    申请日期
                </td>
                <td>
                    <asp:TextBox ID="txtApplyDate" Height="15px" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNote" TextMode="MultiLine" Style="width: 100%;
                        height: 50px;" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <asp:Button ID="btnCancel" Text="取消" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
