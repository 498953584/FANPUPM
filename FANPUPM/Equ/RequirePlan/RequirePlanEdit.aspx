<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequirePlanEdit.aspx.cs" Inherits="Equ_RequirePlan_RequirePlanEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Val.validate('form1', 'btnSave');
        });
        // 选择项目
        function openProjPicker() {
            jw.selectOneProject({ idinput: 'hfldPrjId', nameinput: 'txtPrjName', winNo: 2 });
        }
        //选择任务
        function selectTask() {
            var prjId = $('#hfldPrjId').val();
            if (prjId == '') {
                top.ui.alert('项目不能为空！');
                return;
            }
            var url = '/Equ/ShipOilWear/BudTaskList.aspx?' + new Date().getTime() + '&prjId=' + prjId;
            var taskDivInfo = { url: url, title: '选择任务', width: 1000, height: 500, winNo: 2 };
            var _callback = top.ui.callback;
            top.ui.callback = function (taskInfo) {
                $('#hfldTaskId').val(taskInfo.taskId);
                $('#txtTaskCode').val(taskInfo.taskCode);
                top.ui.callback = _callback;
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
                    编号
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtCode" Width="100%" CssClass="{required:true, messages:{required:'编号不能为空！'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    项目
                </td>
                <td class="txt mustInput">
                    <span class="spanSelect" style="width: 100%">
                        <input type="text" id="txtPrjName" class="{required:true, messages:{required:'项目不能为空！'}}" readonly="readonly" style="width: 88%; height: 15px; border: none; line-height: 16px;
                            margin: 1px 0px 1px 2px;" runat="server" />

                        <img alt="选择项目" onclick="openProjPicker();" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldPrjId" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    分项工程
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <input type="text" id="txtTaskCode" class="readonly" readonly="readonly" style="width: 88%; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                        <img alt="选择分项工程" onclick="selectTask();" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldTaskId" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
