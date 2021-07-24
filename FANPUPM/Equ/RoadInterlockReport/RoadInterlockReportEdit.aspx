<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoadInterlockReportEdit.aspx.cs" Inherits="Equ_RoadInterlockReport_RoadInterlockReportEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>编辑预制联锁块上报</title>
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
                winclose('RoadInterlockReportEdit', 'RoadInterlockReportList.aspx', false);
            });
        });
        // 选择项目
        function openProjPicker() {
            jw.selectOneProject({ idinput: 'hfldPrjId', nameinput: 'txtPrjName' });
        }
        //选择设备
        function selectEqu(type) {
            var url = '/Equ/Equipment/SelectEquipment.aspx?' + new Date().getTime();
            var taskDivInfo = { width: 800, height: 500, url: url, title: '选择设备' };
            top.ui.callback = function (equInfo) {
                $('#hfldEquId').val(equInfo.id);
                $('#txtEquCode').val(equInfo.code);
            };
            top.ui.openWin(taskDivInfo);
        }
        //选择人员
        function selectUser(type) {
            if (type == 'ownerOperator')
                jw.selectOneUser({ nameinput: 'txtOwnerOperator', codeinput: 'hfldOwnerOperator' });
            else
                jw.selectOneUser({ nameinput: 'txtSubcontractChargeMan', codeinput: 'hfldSubcontractChargeMan' });
        }
        //选择任务
        function selectTask() {
            var prjId = $('#hfldPrjId').val();
            if (prjId == '') {
                top.ui.alert('项目不能为空！');
                return;
            }
            var url = '/Equ/ShipOilWear/BudTaskList.aspx?' + new Date().getTime() + '&prjId=' + prjId;
            var taskDivInfo = { url: url, title: '选择任务', width: 1000, height: 500 };
            top.ui.callback = function (taskInfo) {
                $('#hfldTaskId').val(taskInfo.taskId);
                $('#txtTaskCode').val(taskInfo.taskCode);
            };
            top.ui.openWin(taskDivInfo);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent2" class="divContent2">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    上报日期
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtReportDate" Height="15px" Width="100%" class="{required:true, messages:{required:'上报日期不能为空！'}}" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    设备
                </td>
                <td class="txt mustInput">
                    <span class="spanSelect" style="width: 100%">
                        <input type="text" id="txtEquCode" class="{required:true, messages:{required:'设备不能为空！'}}" readonly="readonly" style="width: 88%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                        <img alt="选择设备" onclick="selectEqu('0');" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldEquId" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    项目
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <input type="text" id="txtPrjName" readonly="readonly" style="width: 88%;
                            height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                        <img alt="选择项目" onclick="openProjPicker();" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldPrjId" runat="server" />
                </td>
                <td class="word">
                    分部分项
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <input type="text" id="txtTaskCode" readonly="readonly" style="width: 88%;
                            height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                        <img alt="选择分项工程" onclick="selectTask();" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldTaskId" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    施工日期
                </td>
                <td>
                    <asp:TextBox ID="txtConstructionDate" Height="15px" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    分包队伍
                </td>
                <td>
                    <asp:TextBox ID="txtSubcontractGroup" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    施工产量
                </td>
                <td>
                    <asp:TextBox ID="txtQty" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    分包现场负责人
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <input type="text" id="txtSubcontractChargeMan" readonly="readonly" style="width: 87%; height: 15px; border: none; line-height: 16px; margin: 1px 2px" runat="server" />

                        <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="img1" onclick="selectUser();" runat="server" />

                        <asp:HiddenField ID="hfldSubcontractChargeMan" runat="server" />
                    </span>
                </td>
            </tr>
            <tr>
                <td class="word">
                    甲方施工员
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <input type="text" id="txtOwnerOperator" readonly="readonly" style="width: 87%;
                            height: 15px; border: none; line-height: 16px; margin: 1px 2px" runat="server" />

                        <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="imgName" onclick="selectUser('ownerOperator');" runat="server" />

                        <asp:HiddenField ID="hfldOwnerOperator" runat="server" />
                    </span>
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
    </form>
</body>
</html>
