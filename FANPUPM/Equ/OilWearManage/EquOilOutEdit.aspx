<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquOilOutEdit.aspx.cs" Inherits="Equ_OilWearManage_EquOilOutEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>出库单</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="../../../Script/jw.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../StockManage/script/ValidateInput.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Val.validate('form1', 'btnSave');
            $('#btnCancel').click(btnCancelClick);
            setDisabled();
        })
        function btnCancelClick() {
            winclose('EquOilOutEdit', 'EquOilOut.aspx', false);
        }
        // 设置readonly
        function setDisabled() {
            $('#txtEquipmentCode').attr('readonly', 'readonly');
            $('#txtSignInUser').attr('readonly', 'readonly');
            $('#txtStoreKeeper').attr('readonly', 'readonly');
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
        // 选择人员 
        function selectUser(codeInput, nameInput) {
            jw.selectOneUser({ codeinput: codeInput, nameinput: nameInput });
        }
        // 选择入库单
        function selectEnterCode() {
            var url = '/Equ/OilWearManage/SelectEnterCode.aspx?' + new Date().getTime();
            top.ui.callback = function (oilEnterInfo) {
                $('#hfldEnterId').val(oilEnterInfo.id);
                $('#txtEnterCode').val(oilEnterInfo.code);
            };
            top.ui.openWin({ title: '选择入库单', url: url, width: 500, height: 500 });
        }
        //选择项目
        function openProjPicker() {
            jw.selectOneProject({ idinput: 'hfldProjectId', nameinput: 'txtProject' });
        }
        //选择分项工程
        function selectTask() {
            var prjId = $('#hfldProjectId').val();
            if (prjId == '') {
                top.ui.alert('项目不能为空！');
                return;
            }
            var url = '/Equ/ShipOilWear/BudTaskList.aspx?' + new Date().getTime() + '&prjId=' + prjId;
            top.ui.callback = function taskInfo(taskInfo) {
                $('#hfldTaskId').val(taskInfo.taskId);
                $('#txtTaskId').val(taskInfo.taskCode);
            }
            top.ui.openWin({ title: '选择任务', url: url, width: 1000, height: 500 });
        }
        // 选择收入合同
        function selectContract() {
            jw.selectInCon({
                func: function (c) {
                    $('#hfldHireContractId').val(c.id);
                    $('#txtHireContract').val(c.name);
                }
            });
        }
        //选择支出合同
        function selectOutContract() {
            jw.selectOutCon({
                func: function (con) {
                    $('#hfldAsupplyContractId').val(con.id);
                    $('#txtAsupplyContract').val(con.name);
                }
            });
        }
        // 设置readonly
        function setDisabled() {
            $('#txtEnterCode').attr('readonly', 'readonly');
            $('#txtProject').attr('readonly', 'readonly');
            $('#txtTaskId').attr('readonly', 'readonly');
            $('#txtSignInUser').attr('readonly', 'readonly');
            $('#txtOutDate').attr('readonly', 'readonly');
            $('#txtStoreKeeper').attr('readonly', 'readonly');
            $('#txtEquipmentCode').attr('readonly', 'readonly');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    出库单号
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtOutCode" Width="100%" CssClass="{required:true, messages:{required:'出库单号不能为空'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    入库单号
                </td>
                <td class="txt mustInput">
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtEnterCode" CssClass="{required:true, messages:{required:'入库单号不能为空'}}" data-options="validType:'validQueryChars[50]'" Style="width: 88%;
                            height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择入库单" onclick="selectEnterCode();" src="../../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldEnterId" runat="server" />
                </td>
            </tr>
            <tr>
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
                <td class="word">
                    分项工程
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtTaskId" data-options="validType:'validQueryChars[50]'" Style="width: 97px; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择分项工程" onclick="selectTask();" src="../../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldTaskId" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    出库日期
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtOutDate" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'出库日期不能为空'}}" Width="100%" runat="server"></asp:TextBox>
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
                    租用类型
                </td>
                <td>
                    <asp:DropDownList ID="dropHireType" Width="100%" runat="server"><asp:ListItem Text="自用" /><asp:ListItem Text="外租" /></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="word">
                    外租合同
                </td>
                <td>
                    <span id="spanHireContract" class="spanSelect" style="width: 100%;">
                        <input type="text" style="width: 90%; height: 15px; border: none; line-height: 16px;
                            margin: 1px 0px 1px 2px" readonly="readonly" id="txtHireContract" runat="server" />

                        <img src="../../images/icon.bmp" style="float: right;" alt="选择外租合同" onclick="selectContract();" />
                    </span>
                    <input id="hfldHireContractId" type="hidden" runat="server" />

                </td>
                <td class="word">
                    是否甲供
                </td>
                <td>
                    <asp:CheckBox ID="chkIsAsupply" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    甲供合同
                </td>
                <td>
                    <span id="spanAsupplyContract" class="spanSelect" style="width: 100%;">
                        <input type="text" style="width: 90%; height: 15px; border: none; line-height: 16px;
                            margin: 1px 0px 1px 2px" readonly="readonly" id="txtAsupplyContract" runat="server" />

                        <img src="../../images/icon.bmp" style="float: right;" alt="选择甲供合同" onclick="selectOutContract();" />
                    </span>
                    <input id="hfldAsupplyContractId" type="hidden" runat="server" />

                </td>
                <td class="word">
                    结算方式
                </td>
                <td>
                    <asp:DropDownList ID="dropBalanceMode" DataValueField="NoteID" DataTextField="CodeName" Width="100%" runat="server"><asp:ListItem /></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="word">
                    单价（元/L）
                </td>
                <td>
                    <asp:TextBox ID="txtUnitPrice" CssClass="decimal_input" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    数量（L）
                </td>
                <td>
                    <asp:TextBox ID="txtQuantity" CssClass="decimal_input" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    库管员
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtStoreKeeper" data-options="validType:'validQueryChars[50]'" Style="width: 97px; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择库管员" onclick="selectUser('hfldStoreKeeper','txtStoreKeeper');" src="../../../images/icon.bmp"
                            style="float: right;" />
                        <asp:HiddenField ID="hfldStoreKeeper" runat="server" />
                    </span>
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
