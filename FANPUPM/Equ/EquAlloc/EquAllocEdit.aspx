<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquAllocEdit.aspx.cs" Inherits="Equ_EquAlloc_EquAllocEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/default/easyui.css" />
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
            setDisabled();
        })
        // 设置readonly
        function setDisabled() {
            $('#txtDeployPlan').attr('readonly', 'readonly');
            $('#txtEquipmentCode').attr('readonly', 'readonly');
            $('#txtShipEquChargeMan').attr('readonly', 'readonly');
            $('#txtCalloutDep').attr('readonly', 'readonly');
            $('#txtCallouEquAdmin').attr('readonly', 'readonly');
            $('#txtCallouEquChargeMan').attr('readonly', 'readonly');
            $('#txtCallinDep').attr('readonly', 'readonly');
            $('#txtCallinEquAdmin').attr('readonly', 'readonly');
            $('#txtCallinEquChargeMan').attr('readonly', 'readonly');
            $('#txtReceiver').attr('readonly', 'readonly');
        }
        // 取消
        function btnCancelClick() {
            winclose('EquAllocEdit', 'EquAllocList.aspx', false);
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
        //选择设备
        function selectEqu() {
            var url = '/Equ/Equipment/SelectEquipment.aspx?' + new Date().getTime();
            top.ui.callback = function (equInfo) {
                $('#hfldEquipmentId').val(equInfo.id);
                $('#txtEquName').val(equInfo.name);
                $('#txtEquipmentCode').val(equInfo.code);
                $('#txtSpecification').val(equInfo.specification);
                $('#txtPurchasePrice').val(_formatDecimal(equInfo.PurchasePrice));
                $('#txtPurchaseDate').val(equInfo.PurchaseDate);
            };
            top.ui.openWin({ url: url, title: '选择设备', width: 800, height: 500 });
        }
        // 选择部门
        function selectDepartment(nameInput, idInput) {
            jw.selectOneDep({ nameinput: nameInput, idinput: idInput });
        }
        // 选择人员
        function selectUser(id, name) {
            jw.selectOneUser({ nameinput: name, codeinput: id });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
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
                <td class="word">
                    设备编号
                </td>
                <td class="txt mustInput">
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtEquipmentCode" data-options="validType:'validQueryChars[50]'" CssClass="{required:true, messages:{required:'设备编号不能为空'}}" Style="width: 88%;
                            height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择设备" onclick="selectEqu();" src="../../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldEquipmentId" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    设备名称
                </td>
                <td>
                    <input type="text" id="txtEquName" class="readonly" readonly="readonly" style="width: 100%;" runat="server" />

                </td>
                <td class="word">
                    规格
                </td>
                <td>
                    <input type="text" id="txtSpecification" class="readonly" readonly="readonly" style="width: 100%;" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    资产原值
                </td>
                <td>
                    <input type="text" id="txtPurchasePrice" cssclass="decimal_input" class="readonly" readonly="readonly" style="width: 100%;" runat="server" />

                </td>
                <td class="word">
                    购置日期
                </td>
                <td>
                    <input type="text" id="txtPurchaseDate" class="readonly" readonly="readonly" style="width: 100%;" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    船机设备部负责人
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtShipEquChargeMan" data-options="validType:'validQueryChars[50]'" Style="width: 93%; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择负责人" onclick="selectUser('hfldShipEquChargeMan','txtShipEquChargeMan');"
                            src="../../../images/icon.bmp" style="float: right;" />
                        <asp:HiddenField ID="hfldShipEquChargeMan" runat="server" />
                    </span>
                </td>
                <td class="word">
                    调出部门
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtCalloutDep" data-options="validType:'validQueryChars[50]'" Style="width: 88%; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择调出部门" onclick="selectDepartment('txtCalloutDep','hfldCalloutDepId')"
                            src="../../../images/icon.bmp" style="float: right;" />
                        <asp:HiddenField ID="hfldCalloutDepId" runat="server" />
                    </span>
                </td>
            </tr>
            <tr>
                <td class="word">
                    调出部门设备管理员
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtCallouEquAdmin" data-options="validType:'validQueryChars[50]'" Style="width: 93%; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择管理员" onclick="selectUser('hfldCallouEquAdmin','txtCallouEquAdmin');"
                            src="../../../images/icon.bmp" style="float: right;" />
                        <asp:HiddenField ID="hfldCallouEquAdmin" runat="server" />
                    </span>
                </td>
                <td class="word">
                    调出部门负责人
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtCalloutEquChargeMan" data-options="validType:'validQueryChars[50]'" Style="width: 93%; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择负责人" onclick="selectUser('hfldCalloutEquChargeMan','txtCalloutEquChargeMan');"
                            src="../../../images/icon.bmp" style="float: right;" />
                        <asp:HiddenField ID="hfldCalloutEquChargeMan" runat="server" />
                    </span>
                </td>
            </tr>
            <tr>
                <td class="word">
                    调入部门
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtCallinDep" data-options="validType:'validQueryChars[50]'" Style="width: 88%; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择调入部门" onclick="selectDepartment('txtCallinDep','hfldCallinDepId')" src="../../../images/icon.bmp"
                            style="float: right;" />
                        <asp:HiddenField ID="hfldCallinDepId" runat="server" />
                    </span>
                </td>
                <td class="word">
                    调入部门设备管理员
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtCallinEquAdmin" data-options="validType:'validQueryChars[50]'" Style="width: 93%; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择管理员" onclick="selectUser('hfldCallinEquAdmin','txtCallinEquAdmin');"
                            src="../../../images/icon.bmp" style="float: right;" />
                        <asp:HiddenField ID="hfldCallinEquAdmin" runat="server" />
                    </span>
                </td>
            </tr>
            <tr>
                <td class="word">
                    调入部门负责人
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtCallinEquChargeMan" data-options="validType:'validQueryChars[50]'" Style="width: 93%; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择负责人" onclick="selectUser('hfldCallinEquChargeMan','txtCallinEquChargeMan');"
                            src="../../../images/icon.bmp" style="float: right;" />
                        <asp:HiddenField ID="hfldCallinEquChargeMan" runat="server" />
                    </span>
                </td>
                <td class="word">
                    设备状态
                </td>
                <td>
                    <asp:DropDownList ID="dropState" Style="width: 100%;" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="word">
                    接收人
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtReceiver" data-options="validType:'validQueryChars[50]'" Style="width: 93%; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择接收人" onclick="selectUser('hfldReceiver','txtReceiver');" src="../../../images/icon.bmp"
                            style="float: right;" />
                        <asp:HiddenField ID="hfldReceiver" runat="server" />
                    </span>
                </td>
                <td class="word">
                    调拨日期
                </td>
                <td>
                    <input type="text" id="txtAllocDate" style="width: 100%;" readonly="readonly" onclick="WdatePicker()" runat="server" />

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
    <asp:HiddenField ID="hfldAllocId" runat="server" />
    </form>
</body>
</html>
