<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RepairPlanEdit.aspx.cs" Inherits="Equ_Repair_RepairPlanEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>编辑维修计划</title>
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
                winclose('RepairPlanEdit', 'RepairPlanList.aspx?equipmentType=' + $('#hfldEquipmentType').val() + '', false);
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
                    维修计划编号
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtCode" Width="100%" runat="server"></asp:TextBox>
                </td>
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
            </tr>
            <tr>
                <td class="word">
                    设备名称
                </td>
                <td>
                    <input type="text" id="txtEquName" class="readonly" readonly="readonly" width="100%" runat="server" />

                </td>
                <td class="word">
                    规格
                </td>
                <td>
                    <input type="text" id="txtSpecification" class="readonly" readonly="readonly" width="100%" runat="server" />

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
                    计划维修内容
                </td>
                <td>
                    <asp:TextBox ID="txtRepairContent" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    计划开始日期
                </td>
                <td>
                    <asp:TextBox ID="txtRepairStartDate" Height="15px" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    计划结束日期
                </td>
                <td>
                    <asp:TextBox ID="txtRepairEndDate" Height="15px" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    计划维修费用
                </td>
                <td>
                    <asp:TextBox ID="txtCost" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    维修方式
                </td>
                <td>
                    <asp:DropDownList ID="ddlRepairType" Width="100%" runat="server"><asp:ListItem Value="0" Text="自行维修" /><asp:ListItem Value="1" Text="委外维修" /></asp:DropDownList>
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
                    维保标识
                </td>
                <td>
                    <asp:DropDownList ID="ddlRMFlag" Width="100%" runat="server"><asp:ListItem Value="R" Text="维修" /><asp:ListItem Value="M" Text="保养" /></asp:DropDownList>
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
