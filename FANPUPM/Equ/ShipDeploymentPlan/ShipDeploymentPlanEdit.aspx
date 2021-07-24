<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShipDeploymentPlanEdit.aspx.cs" Inherits="Equ_ShipDeploymentPlan_ShipDeploymentPlanEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>船机调配计划</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="../../../Script/jw.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../StockManage/script/ValidateInput.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Val.validate('form1', 'btnSave');
            jw.tooltip();
            //取消按钮事件
            $('#btnCancel').click(btnCancelClick);
            setDisabled();
        })
        function btnCancelClick() {
            winclose('ShipDeploymentPlanEdit', 'ShipDeploymentPlanList.aspx', false);
        }
        //选择设备
        function selectEquipmentCode() {
            var url = '/Equ/Equipment/SelectEquipment.aspx?' + new Date().getTime();
            top.ui.callback = function (equInfo) {
                $('#hfldEquipmentId').val(equInfo.id);
                $('#txtEquipmentCode').val(equInfo.code);
                $('#txtSpecification').val(equInfo.specification);
            };
            top.ui.openWin({ title: '选择设备', url: url, width: 1000, height: 500 });
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
        //选择部门
        function selectDepartment(nameInput, idInput) {
            jw.selectOneDep({ nameinput: nameInput, idinput: idInput });
        }
        //设置readonly
        function setDisabled() {
            $('#txtProject').attr('readonly', 'readonly');
            $('#txtTaskId').attr('readonly', 'readonly');
            $('#txtEquipmentCode').attr('readonly', 'readonly');
            $('#txtStartDate').attr('readonly', 'readonly');
            $('#txtEndDate').attr('readonly', 'readonly');
            $('#txtOutDepartment').attr('readonly', 'readonly');
            $('#txtInDepartment').attr('readonly', 'readonly');
            $('#txtApplyDate').attr('readonly', 'readonly');
            $('#txtArriveDate').attr('readonly', 'readonly');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    计划编号
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtPlanCode" Width="100%" CssClass="{required:true, messages:{required:'计划编号不能为空'}}" runat="server"></asp:TextBox>
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
                    分项工程
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtTaskId" data-options="validType:'validQueryChars[50]'" Style="width: 88%; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择分项工程" onclick="selectTask();" src="../../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldTaskId" runat="server" />
                </td>
                <td class="word">
                    挖深
                </td>
                <td>
                    <input type="text" id="txtSump" style="width: 100%" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    台班
                </td>
                <td>
                    <input type="text" id="txtMachineTeam" style="width: 100%" runat="server" />

                </td>
                <td class="word">
                    设备
                </td>
                <td class="txt mustInput">
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtEquipmentCode" CssClass="{required:true, messages:{required:'设备不能为空'}}" data-options="validType:'validQueryChars[50]'" Style="width: 97px; height: 15px;
                            border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择设备" onclick="selectEquipmentCode();" src="../../../images/icon.bmp" style="float: right;" />
                        <asp:HiddenField ID="hfldEquipmentId" runat="server" />
                    </span>
                </td>
            </tr>
            <tr>
                <td class="word">
                    规格型号
                </td>
                <td>
                    <input type="text" id="txtSpecification" style="width: 100%" runat="server" />

                </td>
                <td class="word">
                    本月预算工程量
                </td>
                <td>
                    <asp:TextBox ID="txtBudQuantity" CssClass="decimal_input" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    本月预算油耗
                </td>
                <td>
                    <asp:TextBox ID="txtBudOilWear" CssClass="decimal_input" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    预计进场时间
                </td>
                <td>
                    <asp:TextBox ID="txtStartDate" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    预计进场地点
                </td>
                <td>
                    <asp:TextBox ID="txtStartPlace" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    预计出场时间
                </td>
                <td>
                    <asp:TextBox ID="txtEndDate" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    预计出场地点
                </td>
                <td>
                    <asp:TextBox ID="txtEndPlace" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    调出部门
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtOutDepartment" data-options="validType:'validQueryChars[50]'" Style="width: 88%; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择调出部门" onclick="selectDepartment('txtOutDepartment','hfldOutDepartment')"
                            src="../../../images/icon.bmp" style="float: right;" />
                        <asp:HiddenField ID="hfldOutDepartment" runat="server" />
                    </span>
                </td>
            </tr>
            <tr>
                <td class="word">
                    调入部门
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtInDepartment" data-options="validType:'validQueryChars[50]'" Style="width: 88%; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择调入部门" onclick="selectDepartment('txtInDepartment','hfldInDepartment');"
                            src="../../../images/icon.bmp" style="float: right;" />
                        <asp:HiddenField ID="hfldInDepartment" runat="server" />
                    </span>
                </td>
                <td class="word">
                    申请提出日期
                </td>
                <td>
                    <asp:TextBox ID="txtApplyDate" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    最迟到位日期
                </td>
                <td>
                    <asp:TextBox ID="txtArriveDate" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    设备保养状态
                </td>
                <td>
                    <asp:TextBox ID="txtMaintenanceState" Width="100%" runat="server"></asp:TextBox>
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
