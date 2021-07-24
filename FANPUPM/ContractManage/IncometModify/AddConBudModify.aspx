<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddConBudModify.aspx.cs" Inherits="ContractManage_IncometModify_AddConBudModify" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnSave').attr('disabled', 'disabled');
            $('#hfldModifyId').val(getRequestParam('modifyId'));
            var modifyType = $('#ModifyType').val();
            $('#hfldModifyType').val(modifyType);
            if ($('#ModifyType').val()) {
                $('#btnSave').removeAttr('disabled');
                if (modifyType == '0') {
                    $('#lbltaskName').text('上级任务');
                    $('#lblModifyTaskName').text('节点名称');
                    $('#lblTaskCode').text('清单编号');
                } else {
                    $('#lbltaskName').text('变更任务');
                    $('#lblModifyTaskName').text('变更内容');
                    $('#lblTaskCode').text('节点名称');
                }
            }
            select_Change();
            Val.validate('form1');
        });

        // 选择节点
        function openTaskList(tem_input) {
            var spType = 'in';
            if ($('#hfldModifyType').val() == '0') spType = 'out';
            var _callback;
            var tr = $(tem_input).parent().parent().parent();
            var urlStr = '/ContractManage/IncometContract/IncometBudgetList.aspx?' + new Date().getTime() + '&method=returnTask&prjId=' + $('#hfldPrjId').val() + '&contractId=' + $('#hfldContractId').val();
            urlStr += '&spType=' + spType;
            var taskInfo = { width: 1000, height: 500, url: urlStr, title: '选择任务', winNo: 2 };
            if (typeof top.ui.callback == 'function') {
                _callback = top.ui.callback;
            }
            top.ui.callback = function (taskId) {
                // 获取节点信息
                var para = "&prjId=" + $('#hfldPrjGuid').val() + '&spType=' + $('#ModifyType').val() + '&taskId=' + taskId + '&type=getTaskInfo';
                var obj = getTaskInfo(para)
                var task = JSON.parse(obj);
                // 赋值
                $(tem_input).prev().val(task.TaskCode);
                $('#TaskId').val(taskId);
                var modifyType = $('#ModifyType').val();
                if (modifyType == '0') {
                    $('#txtUnit').val(task.Unit);
                    $('#txtUnitPrice').val(task.UnitPrice);
                } else {
                    $('#txtTaskCode').val(task.TaskName);
                    $('#txtUnit').val(task.Unit);
                    $('#txtStartDate').val(jw.jsonToDate(task.StartDate));
                    $('#txtEndDate').val(jw.jsonToDate(task.EndDate));
                    $('#txtUnitPrice').val(task.UnitPrice);
                    $('#txtConstructPeriod').val(task.ConstructionPeriod);
                }
                top.ui.callback = _callback;
            };
            top.ui.openWin(taskInfo);
        }

        //获得选中的任务节点信息
        function getTaskInfo(para) {
            var data = '';
            $.ajax({
                type: "GET",
                async: false,
                url: '/ContractManage/IncometModify/ConModifyTask.ashx?' + new Date().getTime() + para,
                success: function (datas) {
                    if (datas != '') {
                        data = datas;
                    }
                }
            });
            return data;
        }
        //变更类型发生变化
        function select_Change() {
            var opt = $('#ModifyType').val();
            $('#hfldModifyType').val(opt);
            if (opt == '0') {
                $('#lbltaskName').text('上级任务');
                $('#lblModifyTaskName').text('节点名称');
                $('#lblTaskCode').text('清单编号');
                $('#txtTaskCode').parent().removeClass('readonly');
                $('#txtTaskCode').removeAttr('disabled');
                $('#txtUnitPrice').parent().removeClass('readonly');
                $('#txtUnitPrice').removeAttr('readonly');
                $('#txtStartDate').parent().removeClass('readonly');
                $('#txtStartDate').removeAttr('disabled');
                $('#txtEndDate').parent().removeClass('readonly');
                $('#txtEndDate').removeAttr('disabled');
                $('#txtConstructPeriod').parent().removeClass('readonly');
                $('#txtConstructPeriod').removeAttr('disabled');
            } else {
                $('#lbltaskName').text('变更任务');
                $('#lblModifyTaskName').text('变更内容');
                $('#lblTaskCode').text('节点名称');
                $('#txtTaskCode').parent().addClass('readonly'); //.attr('disabled', 'disabled');
                $('#txtTaskCode').attr('disabled', 'disabled');
                $('#txtUnitPrice').parent().addClass('readonly');
                $('#txtUnitPrice').attr('readonly', 'readonly');
                //$('#txtStartDate').parent().addClass('readonly');
                //$('#txtStartDate').attr('disabled', 'disabled');
                //$('#txtEndDate').parent().addClass('readonly');
                //$('#txtEndDate').attr('disabled', 'disabled');
                //$('#txtConstructPeriod').parent().addClass('readonly');
                //$('#txtConstructPeriod').attr('disabled', 'disabled');
            }
        }
        //清空文本框的数据
        function CleanInputData() {
            $('#txtTaskName').val('');
            $('#txtTaskCode').val('');
            $('#txtUnitPrice').val('');
            $('#txtStartDate').val('');
            $('#txtEndDate').val('');
            $('#txtUnit').val('');
            $('#txtConstructPeriod').val('');
            $('#txtNode').val('');
            $('#txtTotal').val('');
            $('#txtModifyTaskName').val('');
            $('#txtQuantity').val('');
        }

        //计算综合单价/小计
        function setValue() {
            //修改数量或综合单价，计算出小计并赋值给小计
            //数量
            var quantity = 0;
            if (Trim($('#txtQuantity').val()) != '') {
                quantity = parseFloat($('#txtQuantity').val().replace(/\,/g, ''));
            }
            //综合单价
            var unitPrice = 0;
            if (Trim($('#txtUnitPrice').val()) != '') {
                unitPrice = parseFloat($('#txtUnitPrice').val().replace(/\,/g, ''));
            }
            //小计
            var total = quantity * unitPrice;
            if (isNaN(total)) {
                total = 0;
            }
            $('#txtTotal').val(_formatDecimal(total));
        }

        // 管理时间
        function controlDate() {
            var startDate;
            var endDate;
            startDate = $('#txtStartDate').val();
            endDate = $('#txtEndDate').val();
            if (startDate != "" && endDate != "") {
                //判断结束日期不能小于开始时间
                var startDateList = startDate.split('-');
                var start = new Date(startDateList[0], startDateList[1] - 1, startDateList[2]);
                var endDateList = endDate.split('-');
                var end = new Date(endDateList[0], endDateList[1] - 1, endDateList[2]);
                if (end - start < 0) {
                    top.ui.alert('系统提示：\n结束时间不能小于开始时间！请重新选择时间！')
                    $('#btnSave').attr('disabled', true);
                }
                else {
                    $('#btnSave').attr('disabled', false);
                }
                // 计算工期
                if (start && end) {
                    var sd = new Date(start).getTime();
                    var ed = new Date(end).getTime();
                    var cp = (ed - sd) / (1000 * 3600 * 24) + 1;
                    if (isNaN(cp)) {
                        cp = 0;
                    }
                    $('#txtConstructPeriod').val(cp);
                }
            }
        }

        // 验证整数
        function intValidator(id) {
            var value = $(id).val();
            var reg = new RegExp('^[1-9]*[1-9][0-9]*$');
            if (!reg.test(value)) {
                $(id).val('0');
            }
        }

        //判断编码是否重复
        function IsExistRepeatCode(txtCode) {
            var currentCode = Trim($(txtCode).val());
            var para = '&prjId=' + $('#hfldPrjId').val() + '&spType=out&modifyTaskCode=' + currentCode + '&type=codeIsExistModify&modifyId=' + $('#hfldMidifyId').val();
            var isExist = getTaskInfomation(para);
            if (isExist != '0') {
                top.ui.alert('此清单外的编码已经在其他的预算变更单中保存，请重新编辑!');
                $(txtCode).val('');
                return;
            }
            para = '&prjId=' + $('#hfldPrjId').val() + '&spType=out&modifyTaskCode=' + currentCode + '&type=codeIsExistBud&modifyId=' + $('#hfldMidifyId').val();
            isExist = getTaskInfomation(para);
            if (isExist != '0') {
                top.ui.alert('此清单外的编码已经在原预算中保存，请重新编辑!');
                $(txtCode).val('');
                return;
            }
        }

        //获得选中的任务节点信息
        function getTaskInfomation(para) {
            var data = '';
            $.ajax({
                type: "GET",
                async: false,
                url: '/BudgetManage/Handler/ConModifyTask.ashx?' + new Date().getTime() + para,
                success: function (datas) {
                    if (datas != '') {
                        data = datas;
                    }
                }
            });
            return data;
        }

        function btnSave() {
            if (typeof top.ui.callback == 'function') {
                top.ui.callback();
            }
            top.ui.closeWin({ parentName: '_AddConBudModify' });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divContent2">
        <div class="divContent2" id="outBud" runat="server">
            <table class="tableContent2" cellpadding="5px" cellspacing="0">
                <tr>
                    <td class="word">
                        变更类型
                    </td>
                    <td>
                        <select id="ModifyType" onchange="CleanInputData();select_Change();" style="width: 122px;" runat="server"><option Value="0">清单外</option><option Value="1">清单内</option></select>
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td class="word">
                        <asp:Label ID="lbltaskName" runat="server"></asp:Label>
                    </td>
                    <td class="txt">
                        <asp:HiddenField ID="TaskId" runat="server" />
                        <asp:HiddenField ID="ParentId" Value='<%# Convert.ToString(Eval("ParentId").ToString()) %>' runat="server" />
                        <span class="spanSelect mustInput" style="width: 122px">
                            <input type="text" id="txtTaskName" class="readonly  {required:true, messages:{required:'任务节点必须输入'}}" style="width: 97px; height: 15px;
                                border: none; line-height: 16px; margin: 1px 0px 1px 2px;" value='<%# Convert.ToString(base.GetTaskCode(Eval("TaskId").ToString())) %>' runat="server" />

                            <img alt="选择任务" onclick="openTaskList(this);" src="../../images/icon.bmp" style="float: right;" />
                        </span>
                        <asp:HiddenField ID="orderNumber" runat="server" />
                    </td>
                    <td class="word">
                        <label id="lblTaskCode" runat="server">
                        </label>
                    </td>
                    <td class="txt mustInput">
                        <asp:TextBox ID="txtTaskCode" CssClass="{required:true, messages:{required:'清单编号必须输入'}}" onBlur="IsExistRepeatCode(this);" Width="100%" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word">
                        <label id="lblModifyTaskName" runat="server">
                        </label>
                    </td>
                    <td class="txt mustInput">
                        <asp:TextBox ID="txtModifyTaskName" CssClass="{required:true, messages:{required:'变更内容必须输入'}}" Width="100%" runat="server"></asp:TextBox>
                    </td>
                    <td class="word">
                        单位
                    </td>
                    <td class="txt mustInput">
                        <asp:TextBox ID="txtUnit" Width="100%" CssClass="{required:true, messages:{required:'变更单位必须输入'}}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word">
                        工程量
                    </td>
                    <td>
                        <input id="txtQuantity" class="decimal_input" onblur="setValue();" runat="server" />

                    </td>
                    <td class="word">
                        单价
                    </td>
                    <td class="txt">
                        <input id="txtUnitPrice" onblur="setValue();" class="decimal_input" runat="server" />

                    </td>
                </tr>
                <tr>
                    <td class="word readonly">
                        小计
                    </td>
                    <td class="txt readonly">
                        <input id="txtTotal" readonly="readonly" class="decimal_input" runat="server" />

                    </td>
                    <td class="word">
                        开始时间
                    </td>
                    <td class="txt">
                        <input type="text" id="txtStartDate" onclick="WdatePicker();" onblur="controlDate();" runat="server" />

                    </td>
                </tr>
                <tr>
                    <td class="word">
                        结束时间
                    </td>
                    <td class="txt">
                        <asp:TextBox onclick="WdatePicker();" Width="100%" OnBlur="controlDate();" ID="txtEndDate" runat="server"></asp:TextBox>
                    </td>
                    <td class="word">
                        工期
                    </td>
                    <td>
                        <asp:TextBox ID="txtConstructPeriod" OnBlur="intValidator(this)" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word">
                        备注
                    </td>
                    <td colspan="3" class="txt">
                        <asp:TextBox ID="txtNode" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word" id="td1">
                        附件
                    </td>
                    <td colspan="3" style="text-align: left; padding-right: 0px;" class="txt">
                        <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="fileBud" Class="ContractBudChange" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
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
    
    <asp:HiddenField ID="hfldAction" runat="server" />
    
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    
    <asp:HiddenField ID="hfldContractId" runat="server" />
    
    <asp:HiddenField ID="hfldBudModifyTaskId" runat="server" />
    
    <asp:HiddenField ID="hfldModifyId" runat="server" />
    
    <asp:HiddenField ID="hfldModifyType" runat="server" />
    
    <asp:HiddenField ID="hfldModifyTask" runat="server" />
    </form>
</body>
</html>
