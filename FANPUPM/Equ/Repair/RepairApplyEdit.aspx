<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RepairApplyEdit.aspx.cs" Inherits="Equ_Repair_RepairApplyEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Val.validate('form1', 'btnSave');
            //取消按钮事件
            $('#btnCancel').click(function () {
                winclose('RepairApplyEdit', 'RepairApplyList.aspx?equipmentType=' + $('#hfldEquipmentType').val() + '', false);
            });
        });
        //选择设备
        function selectEqu() {
            var url = '/Equ/Equipment/SelectEquipment.aspx?' + new Date().getTime();
            var equDivInfo = { url: url, title: '选择设备', width: 800, height: 500 };
            top.ui.callback = function (equInfo) {
                $('#hfldEquId').val(equInfo.id);
                $('#txtEquCode').val(equInfo.code);
                $('#txtEquName').val(equInfo.name);
                $('#txtSpecification').val(equInfo.specification);
                $('#txtPurchasePrice').val(_formatDecimal(equInfo.PurchasePrice));
            };
            top.ui.openWin(equDivInfo);
        }
        //选择维修保养计划
        function selectRepairPlan() {
            var url = '/Equ/Repair/SelectRepairPlan.aspx?' + new Date().getTime() + "&equipmentType=" + $('#hfldEquipmentType').val();
            var planDivInfo = { url: url, title: '选择维修保养计划', width: 800, height: 500 };
            top.ui.callback = function (planInfo) {
                $('#txtRepairPlanCode').val(planInfo.code);
                $('#hfldRepairPlanId').val(planInfo.id);
                $('#hfldEquId').val(planInfo.equId);
                $('#txtEquCode').val(planInfo.equCode);
                $('#txtEquName').val(planInfo.equName);
                $('#txtSpecification').val(planInfo.specification);
                //维修方式
                var selectedIndexRepairType = 0;
                $('select#ddlRepairType option ').each(function (index, domEle) {
                    if (domEle.value == planInfo.repairType) {
                        selectedIndexRepairType = index;
                        return false;
                    }
                });
                $('select#ddlRepairType')[0].selectedIndex = selectedIndexRepairType;
                //维保标识
                var selectedIndexRmFlag = 0;
                $('select#ddlRMFlag option ').each(function (index, domEle) {
                    if (domEle.value == planInfo.rmFlag) {
                        selectedIndexRmFlag = index;
                        return false;
                    }
                });
                $('select#ddlRMFlag')[0].selectedIndex = selectedIndexRmFlag;
            };
            top.ui.openWin(planDivInfo);
        }
        //选择申请部门
        function selectDerpt(param) {
            jw.selectOneDep({ nameinput: 'txtDepartment', idinput: 'hfldDepartment' });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent2" class="divContent2">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    维修申请编号
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtCode" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    维修保养计划
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <input type="text" id="txtRepairPlanCode" readonly="readonly" style="width: 88%;
                            height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                        <img alt="选择维修保养计划" onclick="selectRepairPlan();" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldRepairPlanId" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    设备编号
                </td>
                <td class="txt mustInput">
                    <span class="spanSelect" style="width: 100%">
                        <input type="text" id="txtEquCode" class="{required:true, messages:{required:'设备编号不能为空！'}}" readonly="readonly" style="width: 88%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                        <img alt="选择维修设备" onclick="selectEqu();" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldEquId" runat="server" />
                </td>
                <td class="word">
                    设备名称
                </td>
                <td>
                    <input type="text" id="txtEquName" class="readonly" readonly="readonly" width="100%" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    规格
                </td>
                <td>
                    <input type="text" id="txtSpecification" class="readonly" readonly="readonly" width="100%" runat="server" />

                </td>
                <td class="word">
                    资产原值
                </td>
                <td>
                    <input type="text" id="txtPurchasePrice" value="0.000" class="readonly" readonly="readonly" style="width: 100%; height: 15px;" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    已提折旧金额
                </td>
                <td>
                    <asp:TextBox ID="txtDepreciationAmount" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    施工区域
                </td>
                <td>
                    <asp:TextBox ID="txtConstructionPlace" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    上次维修日期
                </td>
                <td>
                    <asp:TextBox ID="txtLastRepairDate" Height="15px" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    上次维修内容
                </td>
                <td>
                    <asp:TextBox ID="txtLastRepairContent" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    上次维修费用
                </td>
                <td>
                    <asp:TextBox ID="txtLastRepairCost" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    本次预计维修费用
                </td>
                <td>
                    <asp:TextBox ID="txtBudRepairCost" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    本次预计开始日期
                </td>
                <td>
                    <asp:TextBox ID="txtBudRepairStartDate" Height="15px" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    本次预计结束日期
                </td>
                <td>
                    <asp:TextBox ID="txtBudRepairEndDate" Height="15px" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    本次预计维修内容
                </td>
                <td>
                    <asp:TextBox ID="txtBudRepareContent" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    维修原因说明
                </td>
                <td>
                    <asp:TextBox ID="txtRepairReason" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    申请部门
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <input type="text" id="txtDepartment" readonly="readonly" style="width: 88%;
                            height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                        <img alt="选择部门" onclick="selectDerpt();" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldDepartment" runat="server" />
                </td>
                <td class="word">
                    申请日期
                </td>
                <td>
                    <asp:TextBox ID="txtApplyDate" Height="15px" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    维修方式
                </td>
                <td>
                    <asp:DropDownList ID="ddlRepairType" Width="100%" runat="server"><asp:ListItem Value="0" Text="自行维修" /><asp:ListItem Value="1" Text="委外维修" /></asp:DropDownList>
                </td>
                <td class="word">
                    维保标识
                </td>
                <td>
                    <asp:DropDownList ID="ddlRMFlag" Width="100%" runat="server"><asp:ListItem Value="R" Text="维修" /><asp:ListItem Value="M" Text="保养" /></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="word">
                    分部分项工程
                </td>
                <td>
                    <asp:TextBox ID="txtTaskName" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNotes" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfldEquipmentType" runat="server" />
    </form>
</body>
</html>
