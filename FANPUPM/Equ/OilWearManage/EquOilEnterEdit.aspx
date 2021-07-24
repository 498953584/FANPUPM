<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquOilEnterEdit.aspx.cs" Inherits="Equ_OilWearManage_EquOilEnterEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>入库单</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="../../../Script/jw.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../StockManage/script/ValidateInput.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Val.validate('form1', 'btnSave');
            $('#btnCancel').click(btnCancelClick);
            setDisabled();
        })
        function btnCancelClick() {
            winclose('EquOilEnterEdit', 'EquOilEnter.aspx', false);
        }
        // 选择项目
        function openProjPicker() {
            jw.selectOneProject({ idinput: 'hfldProjectId', nameinput: 'txtProject' });
        }
        // 选择人员 
        function selectUser(codeInput, nameInput) {
            jw.selectOneUser({ codeinput: codeInput, nameinput: nameInput });
        }
        // 设置readonly
        function setDisabled() {
            $('#txtProject').attr('readonly', 'readonly');
            $('#txtSignInUser').attr('readonly', 'readonly');
            $('#txtEnterDate').attr('readonly', 'readonly');
        }
        // 选择合同
        function selectOutContract() {
            jw.selectOutCon({
                func: function (con) {
                    $('#hfldOutContractId').val(con.id);
                    $('#txtOutContract').val(con.name);
                }
            });
        }
        // 选择采购单
        function selectPuechase() {
            var contractId = $('#hfldOutContractId').val()
            if (contractId == '') {
                top.ui.alert('合同不能为空！');
                return;
            }
            var url = '/Equ/OilWearManage/SelectPurchase.aspx?' + new Date().getTime() + '&contract=' + contractId;
            top.ui.callback = function (purchaseInfo) {
                $('#hfldPurchaseId').val(purchaseInfo.pid);
                $('#txtPurchaseCode').val(purchaseInfo.pcode);
            };
            top.ui.openWin({ title: '选择采购单', url: url, width: 820, height: 500 });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    入库单号
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtEnterCode" Width="100%" CssClass="{required:true, messages:{required:'入库单号不能为空'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    项目
                </td>
                <td class="txt mustInput">
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtProject" CssClass="{required:true, messages:{required:'项目不能为空'}}" data-options="validType:'validQueryChars[50]'" Style="width: 88%; height: 15px;
                            border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择项目" onclick="openProjPicker();" src="../../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldProjectId" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    合同
                </td>
                <td>
                    <span id="spanOutContract" class="spanSelect" style="width: 100%;">
                        <input type="text" style="width: 90%; height: 15px; border: none; line-height: 16px;
                            margin: 1px 0px 1px 2px" readonly="readonly" id="txtOutContract" runat="server" />

                        <img src="../../images/icon.bmp" style="float: right;" alt="选择合同" onclick="selectOutContract();" />
                    </span>
                    <input id="hfldOutContractId" type="hidden" runat="server" />

                </td>
                <td class="word">
                    采购单
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%;">
                        <input type="text" style="width: 90%; height: 15px; border: none; line-height: 16px;
                            margin: 1px 0px 1px 2px" readonly="readonly" id="txtPurchaseCode" runat="server" />

                        <img src="../../images/icon.bmp" style="float: right;" alt="选择采购单" onclick="selectPuechase();" />
                    </span>
                    <input id="hfldPurchaseId" type="hidden" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    入库日期
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtEnterDate" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'入库日期不能为空'}}" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    签收人
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtSignInUser" data-options="validType:'validQueryChars[50]'" Style="width: 97px; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择签收人" onclick="selectUser('hfldSignInUser','txtSignInUser');" src="../../../images/icon.bmp"
                            style="float: right;" />
                        <asp:HiddenField ID="hfldSignInUser" runat="server" />
                    </span>
                </td>
            </tr>
            <tr>
                <td class="word">
                    仓管员
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtStoreKeeper" data-options="validType:'validQueryChars[50]'" Style="width: 97px; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择仓管员" onclick="selectUser('hfldStoreKeeper','txtStoreKeeper');" src="../../../images/icon.bmp"
                            style="float: right;" />
                        <asp:HiddenField ID="hfldStoreKeeper" runat="server" />
                    </span>
                </td>
                <td class="word">
                    单价（元/L）
                </td>
                <td>
                    <asp:TextBox ID="txtUnitPrice" CssClass="decimal_input" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    数量（L）
                </td>
                <td>
                    <asp:TextBox ID="txtQuantity" CssClass="decimal_input" Width="100%" runat="server"></asp:TextBox>
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
