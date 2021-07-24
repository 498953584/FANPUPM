<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoadDumpReportEdit.aspx.cs" Inherits="Equ_RoadDumpReport_RoadDumpReportEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>编辑自卸车上报</title>
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
                winclose('RoadDumpReportEdit', 'RoadDumpReportList.aspx', false);
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
                if (type == "0") {
                    //设备
                    $('#hfldEquId').val(equInfo.id);
                    $('#txtEquCode').val(equInfo.code);
                } else {
                    //抛石船
                    $('#hfldStoneDumper').val(equInfo.id);
                    $('#txtStoneDumper').val(equInfo.code);
                }
            };
            top.ui.openWin(taskDivInfo);
        }
        //选择过磅员
        function selectUser() {
            jw.selectOneUser({ nameinput: 'txtWeighbridgeUser', codeinput: 'hfldWeighbridgeUser' });
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
                    地磅房
                </td>
                <td>
                    <asp:TextBox ID="txtWeighbridgeRoom" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    流水号
                </td>
                <td>
                    <asp:TextBox ID="txtSn" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    车号
                </td>
                <td>
                    <asp:TextBox ID="txtTruckNo" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    货名
                </td>
                <td>
                    <asp:TextBox ID="txtCargoNo" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    毛重
                </td>
                <td>
                    <asp:TextBox ID="txtGrossWeigh" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    空重
                </td>
                <td>
                    <asp:TextBox ID="txtBareWeigh" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    净重
                </td>
                <td>
                    <asp:TextBox ID="txtNetWeigh" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    方数
                </td>
                <td>
                    <asp:TextBox ID="txtCubeQty" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    车数
                </td>
                <td>
                    <asp:TextBox ID="txtTruckQty" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    过磅员
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <input type="text" id="txtWeighbridgeUser" readonly="readonly" style="width: 87%;
                            height: 15px; border: none; line-height: 16px; margin: 1px 2px" runat="server" />

                        <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="imgName" onclick="selectUser();" runat="server" />

                        <asp:HiddenField ID="hfldWeighbridgeUser" runat="server" />
                    </span>
                </td>
                <td class="word">
                    毛重日期
                </td>
                <td>
                    <asp:TextBox ID="txtGrossWeighDate" Height="15px" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    毛重时间
                </td>
                <td>
                    <asp:TextBox ID="txtGrossWeighTime" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    抛弃地点
                </td>
                <td>
                    <asp:TextBox ID="txtDiscardPlace" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    抛石船
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <input type="text" id="txtStoneDumper" readonly="readonly" style="width: 88%;
                            height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                        <img alt="选择设备" onclick="selectEqu('1');" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldStoneDumper" runat="server" />
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
