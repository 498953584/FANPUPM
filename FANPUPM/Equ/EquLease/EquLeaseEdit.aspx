<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquLeaseEdit.aspx.cs" Inherits="Equ_EquLease_EquLeaseEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>设备租赁</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/default/easyui.css" />
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
        // 取消
        function btnCancelClick() {
            winclose('EquLeaseEdit', 'EquLeaseList.aspx', false);
        }
        // 设置readonly
        function setDisabled() {
            $('#txtDeployPlan').attr('readonly', 'readonly');
            $('#txtEquipmentCode').attr('readonly', 'readonly');
            $('#txtACorpName').attr('readonly', 'readonly');
            $('#txtBCorpName').attr('readonly', 'readonly');
            $('#txtStartDate').attr('readonly', 'readonly');
            $('#txtEndDate').attr('readonly', 'readonly');
            $('#txtChargeMan').attr('readonly', 'readonly');
        }
        // 选择设备调配计划
        function selectEquDeployPlan() {
            var url = '/Equ/EquDeployPlan/SelectEquDeployPlan.aspx?' + new Date().getDate();
            top.ui.callback = function (deployPlanInfo) {
                $('#hfldDeployPlanId').val(deployPlanInfo.id);
                $('#txtDeployPlan').val(deployPlanInfo.code);
            };
            top.ui.openWin({ title: '选择设备调配计划', url: url, width: 1000, height: 500 });
        }
        // 选择设备
        function selectEquipmentCode() {
            var url = '/Equ/Equipment/SelectEquipment.aspx?' + new Date().getTime();
            top.ui.callback = function (equInfo) {
                $('#hfldEquipmentId').val(equInfo.id);
                $('#txtEquipmentCode').val(equInfo.code);
            };
            top.ui.openWin({ title: '选择设备', url: url, width: 1000, height: 500 });
        }
        // 选择部门
        function selectDepartment(nameInput, idInput) {
            jw.selectOneDep({ nameinput: nameInput, idinput: idInput });
        }
        // 选择合同
        function selectContract() {
            var type = $('select#dropLeaseType option:selected').val();
            if (type == '承租') {
                jw.selectOutCon({
                    func: function (c) {
                        $('#hfldContractId').val(c.id);
                        $('#txtContract').val(c.name);
                    }
                });
            }
            if (type == '出租') {
                jw.selectInCon({
                    func: function (c) {
                        $('#hfldContractId').val(c.id);
                        $('#txtContract').val(c.name);
                    }
                });
            }

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
                    租赁单号
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtCode" Width="100%" CssClass="{required:true, messages:{required:'租赁单号不能为空'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    设备调配计划单号
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtDeployPlan" data-options="validType:'validQueryChars[50]'" Style="width: 88%; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择设备调配计划" onclick="selectEquDeployPlan();" src="../../../images/icon.bmp"
                            style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldDeployPlanId" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    设备
                </td>
                <td class="txt mustInput">
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtEquipmentCode" data-options="validType:'validQueryChars[50]'" CssClass="{required:true, messages:{required:'设备不能为空'}}" Style="width: 88%; height: 15px;
                            border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择设备" onclick="selectEquipmentCode();" src="../../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldEquipmentId" runat="server" />
                </td>
                <td class="word">
                    租赁方式
                </td>
                <td>
                    <asp:DropDownList ID="dropLeaseType" Width="100%" runat="server"><asp:ListItem Text="承租" /><asp:ListItem Text="出租" /></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="word">
                    租用单位
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtACorpName" data-options="validType:'validQueryChars[50]'" Style="width: 88%; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择租用单位" onclick="selectDepartment('txtACorpName','hfldACorpId')" src="../../../images/icon.bmp"
                            style="float: right;" />
                        <asp:HiddenField ID="hfldACorpId" runat="server" />
                    </span>
                </td>
                <td class="word">
                    出租单位
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtBCorpName" data-options="validType:'validQueryChars[50]'" Style="width: 88%; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择出租单位" onclick="selectDepartment('txtBCorpName','hfldBCorpId')" src="../../../images/icon.bmp"
                            style="float: right;" />
                        <asp:HiddenField ID="hfldBCorpId" runat="server" />
                    </span>
                </td>
            </tr>
            <tr>
                <td class="word">
                    起租日期
                </td>
                <td>
                    <asp:TextBox ID="txtStartDate" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    停租日期
                </td>
                <td>
                    <asp:TextBox ID="txtEndDate" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    租用时长
                </td>
                <td>
                    <asp:TextBox ID="txtDuration" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    租用合同
                </td>
                <td>
                    <span id="spanContract" class="spanSelect" style="width: 100%;">
                        <input id="txtContract" type="text" style="width: 90%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 0px 1px 2px" readonly="readonly" runat="server" />

                        <img src="../../images/icon.bmp" style="float: right;" alt="选择租用合同" onclick="selectContract();" />
                    </span>
                    <asp:HiddenField ID="hfldContractId" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    租用原因
                </td>
                <td>
                    <asp:TextBox ID="txtReason" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    租用费用（元）
                </td>
                <td>
                    <asp:TextBox ID="txtCost" CssClass="decimal_input" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    责任人
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtChargeMan" data-options="validType:'validQueryChars[50]'" Style="width: 97px; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择责任人" onclick="selectUser('hfldChargeMan','txtChargeMan');" src="../../../images/icon.bmp"
                            style="float: right;" />
                        <asp:HiddenField ID="hfldChargeMan" runat="server" />
                    </span>
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNote" Width="100%" TextMode="MultiLine" runat="server"></asp:TextBox>
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
