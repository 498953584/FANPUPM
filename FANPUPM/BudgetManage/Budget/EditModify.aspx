<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditModify.aspx.cs" Inherits="BudgetManage_Budget_EditModify" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>变更详情</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnBindRes').hide();
            $('#divInTask').hide();
            Val.validate('form1', 'btnSave');
            setTask();
            if ($('#hfldIsWBSRelevance').val() == '1') {
                $('#btnResOut').show();
                $('#btnResIn').show();
                $('#btnResOut').click(function () {
                    openRes('out');
                });
                $('#btnResIn').click(function () {
                    openRes('in');
                });
                $('#btnResListOut').hide();
                $('#btnResListIn').hide();
            } else {
                $('#btnResOut').hide();
                $('#btnResIn').hide();
                $('#btnResListOut').show();
                $('#btnResListIn').show();
                $('#btnResListOut').click(function () {
                    openRes('out');
                });
                $('#btnResListIn').click(function () {
                    openRes('in');
                });
            }

            clickWBSType($('#hfldSpType').val());
        });
        //点击清单外/清单内
        function clickWBSType(type) {
            if (type == "out") {
                $('#spOutWBS').attr('class', 'xxkd');
                $('#spInWBS').attr('class', 'xxk');
                $('#divOutTask').show();
                $('#divInTask').hide();
                $('#hfldSpType').val('out');
            } else {
                $('#spOutWBS').attr('class', 'xxk');
                $('#spInWBS').attr('class', 'xxkd');
                $('#divOutTask').hide();
                $('#divInTask').show();
                $('#hfldSpType').val('in');
            }
        }
        //设置任务节点
        function setTask() {
            var jwOutTask = new JustWinTable('gvOutTask');
            var jwInTask = new JustWinTable('gvInTask');
            replaceEmptyTable('emptyOutTask', 'gvOutTask');
            replaceEmptyTable('emptyInTask', 'gvInTask');
            setButton(jwOutTask, 'btnDelOut', '', 'btnResOut', 'hfldCheckedTaskIdOut');
            setButton(jwInTask, 'btnDelIn', '', 'btnResIn', 'hfldCheckedTaskIdIn');
        }

        // 选择节点
        function openTaskList(spType, tem_input) {
            var tr = $(tem_input).parent().parent().parent();
            //            var urlStr = '/BudgetManage/Budget/BudTaskList.aspx?' + new Date().getTime() + '&method=returnTask&prjId=' + $('#hfldPrjId').val();
            //            urlStr += '&spType=' + spType;
            var url = "/ContractManage/PayoutContract/PayoutTarget.aspx?prjId=" + jw.getRequestParam('prjId');
            var taskInfo = { width: 900, height: 500, url: url, title: '选择任务' };
            top.ui.callback = function (taskId) {
                taskId = taskId.taskId
                //资源与WBS不挂钩时，可以在清单内清单外，同时引用同一个任务节点；挂钩时，不可以同时被引用
                if ($('#hfldIsWBSRelevance').val() == '1') {
                    //本单据内判断选择的节点是否在清单内/清单外都存在
                    if (inCheckTask(spType, taskId) == 1) {
                        top.ui.alert('该节点不能在清单外清单内同时引用!');
                        return;
                    }
                    //本单据外判断选择的节点是否在清单内/清单外都存在
                    var taskObj = outCheckTask(spType, taskId);

                    var isExist = outCheckTask(spType, taskId);
                    if (isExist != '0') {
                        if (spType == 'out') {
                            top.ui.alert('该节点已经被其他变更中的清单内引用，不能在此清单外同时被引用!');
                        } else {
                            top.ui.alert('该节点已经被其他变更中的清单外引用，不能在此清单内同时被引用!');
                        }
                        return;
                    }
                }
                //获取节点信息
                var para = "&prjId=" + $('#hfldPrjId').val() + '&spType=' + spType + '&taskId=' + taskId + '&type=getTaskInfo';
                var data = eval(getTaskInfo(para));
                var taskName = data[0].taskCode;
                //赋值
                $(tem_input).prev().val(taskName);
                $(tem_input).parent().prev().val(taskId);
                if (spType == 'out') {
                    //清单外
                    var typeName = data[0].typeName;
                    var orderNumber = data[0].orderNumber;
                    var txtType;
                    var hfldOrderNumber;
                    tr.find('input[flag=TaskType]').each(function () {
                        txtType = this;         //存储类型名称的文本框
                        hfldOrderNumber = $(this).prev();       //存储orderNumber的隐藏表单域
                    });
                    $(txtType).val(typeName);
                    $(hfldOrderNumber).val(orderNumber);
                } else {
                    //清单内
                    var unit = data[0].unit;
                    var txtUnit;
                    tr.find('input[flag=Unit]').each(function () {
                        txtUnit = this;         //存储单位的文本框
                    });
                    $(txtUnit).val(unit);
                }
            };
            top.ui.openWin(taskInfo);
        }
        //选择资源
        function openRes(spType) {
            var title = '选择资源'
            parent.desktop.BudModifyResList = window;
            var urlStr = '/BudgetManage/Budget/BudModifyResList.aspx?' + new Date().getTime() + '&spType=' + spType;
            urlStr += '&modifyId=' + $('#hfldModifyId').val();
            if ($('#hfldIsWBSRelevance').val() == '1') {
                if (spType == 'out') {
                    urlStr = urlStr + '&modifyTaskId=' + $('#hfldCheckedTaskIdOut').val();
                } else {
                    urlStr = urlStr + '&modifyTaskId=' + $('#hfldCheckedTaskIdIn').val();
                }
            }
            urlStr = urlStr + '&modifyPage=0'
            toolbox_oncommand(urlStr, title);
        }
        //获得选中的任务节点信息
        function getTaskInfo(para) {
            var data = '';
            $.ajax({
                type: "GET",
                async: false,
                url: '/BudgetManage/Handler/ModifyTask.ashx?' + new Date().getTime() + para,
                success: function (datas) {
                    if (datas != '') {
                        data = datas;
                    }
                }
            });
            return data;
        }
        //本单据内判断选择的节点是否在清单内/清单外都存在
        function inCheckTask(spType, taskId) {
            var isExist = 0;
            if (spType == 'out') {
                $('#gvInTask').find('input[flag=TaskCode]').each(function () {
                    var hfldInTaskId = $(this).parent().prev().val();
                    if (hfldInTaskId != '' && hfldInTaskId == taskId) {
                        isExist = 1;
                    }
                });
            } else {
                $('#gvOutTask').find('input[flag=TaskCode]').each(function () {
                    var hfldOutTaskId = $(this).parent().prev().val();
                    if (hfldOutTaskId != '' && hfldOutTaskId == taskId) {
                        isExist = 1;
                    }
                });
            }
            return isExist;
        }
        //本单据外判断选择的节点是否在清单内/清单外都存在
        function outCheckTask(spType, taskId) {
            var para = "&prjId=" + $('#hfldPrjId').val() + '&spType=' + spType + '&taskId=' + taskId + '&type=isExist&modifyId=' + $('#hfldModifyId').val();
            return getTaskInfo(para);
        }
        //计算综合单价/小计
        function setValue(id) {
            //数量
            var quantity = 0;
            $('#' + id).find('input[flag=Quantity]').each(function () {
                if (Trim($(this).val()) != '') {
                    quantity = parseFloat(Trim($(this).val().replace(/\,/g, '')));
                }
            });
            if ($('#hfldIsWBSRelevance').val() == '1') {
                //当资源与WBS挂钩的时候，修改数量，计算出综合单价并赋值给综合单价
                //小计
                var total = 0;
                $('#' + id).find('input[flag=Total]').each(function () {
                    if (Trim($(this).val()) != '') {
                        total = parseFloat(Trim($(this).val().replace(/\,/g, '')));
                    }
                });
                //综合单价
                var unitPrice = 0;
                if (quantity != 0) {
                    unitPrice = total / quantity;
                }
                if (isNaN(unitPrice)) {
                    unitPrice = 0;
                }
                $('#' + id).find('input[flag=UnitPrice]').each(function () {
                    $(this).val(_formatDecimal(unitPrice.toString()));
                });
            } else {
                //当资源与WBS不挂钩的时候，修改数量或综合单价，计算出小计并赋值给小计
                //综合单价
                var unitPrice = 0;
                $('#' + id).find('input[flag=UnitPrice]').each(function () {
                    if (Trim($(this).val()) != '') {
                        unitPrice = parseFloat(Trim($(this).val().replace(/\,/g, '')));
                    }
                });
                //小计
                var total = quantity * unitPrice;
                if (isNaN(total)) {
                    total = 0;
                }
                $('#' + id).find('input[flag=Total]').each(function () {
                    var oldTotal = 0;
                    oldTotal = parseFloat($(this).val().replace(/\,/g, '')); //获得当前文本框原小计
                    $(this).val(_formatDecimal(total.toString())); 		//给当前文本框赋上重新计算的小计
                    var oldBudAmount = parseFloat($('#txtBudAmount').val().replace(/\,/g, '')); 	//获得原预算成本
                    var budAmount = oldBudAmount - oldTotal + total; 		//现有的预算成本=原预算成本-重新计算的小计+当前文本框原小计
                    $('#txtBudAmount').val(_formatDecimal(budAmount)); 		//预算成本文本框赋上现有的预算成本
                });

            }
        }
        //编码是否重复
        function IsExistRepeatCode(txtCode) {
            var currentCode = Trim($(txtCode).val());
            var countRepeat = 0;
            $('#gvOutTask').find('input[flag=ModifyTaskCode]').each(function () {
                if (Trim($(this).val()) != '') {
                    if (Trim($(this).val()) == currentCode) {
                        countRepeat++;
                    }
                }
            });
            if (countRepeat > 1) {
                top.ui.alert('清单外的编码出现重复!');
                $(txtCode).val('');
                return;
            }
            var para = '&prjId=' + $('#hfldPrjId').val() + '&spType=out&modifyTaskCode=' + currentCode + '&type=codeIsExistModify&modifyId=' + $('#hfldModifyId').val();
            var isExist = getTaskInfo(para);
            if (isExist != '0') {
                top.ui.alert('此清单外的编码已经在其他的预算变更单中保存，请重新编辑!');
                $(txtCode).val('');
                return;
            }
            para = '&prjId=' + $('#hfldPrjId').val() + '&spType=out&modifyTaskCode=' + currentCode + '&type=codeIsExistBud&modifyId=' + $('#hfldModifyId').val();
            isExist = getTaskInfo(para);
            if (isExist != '0') {
                top.ui.alert('此清单外的编码已经在原预算中保存，请重新编辑!');
                $(txtCode).val('');
                return;
            }
        }
        // 管理时间
        function controlDate(date) {
            var tr = $(date).parent().parent();
            var startDate;
            var endDate;
            //读取开始时间
            tr.find('input[flag=startDate]').each(function () {
                startDate = this.value;
            });
            //读取结束时间
            tr.find('input[flag=endDate]').each(function () {
                endDate = this.value;
            });
            if (startDate != "" && endDate != "") {
                //判断结束日期不能小于开始时间
                var startDateList = startDate.split('-');
                var start = new Date(startDateList[0], startDateList[1] - 1, startDateList[2]);
                var endDateList = endDate.split('-');
                var end = new Date(endDateList[0], endDateList[1] - 1, endDateList[2]);
                if (end - start < 0) {
                    date.value = "";
                    alert('系统提示：\n结束时间不能小于开始时间！请重新选择时间！')
                }
                // 计算工期
                if (start && end) {
                    var sd = new Date(start).getTime();
                    var ed = new Date(end).getTime();
                    var cp = (ed - sd) / (1000 * 3600 * 24) + 1;
                    tr.find('input[flag=ConstructionPeriod]').each(function () {
                        this.value = cp;
                    });
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divContent2" id="divModifyInfo">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word" style="white-space: nowrap;">
                    附件
                </td>
                <td colspan="3" style="padding-right: 0px;">
                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="uploadModify" Class="BudModify" Name="附件" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    编号
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtModifyCode" Height="15px" Width="100%" CssClass="{required:true, messages:{required:'编号必须输入'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    变更内容
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtModifyContent" Height="15px" Width="100%" CssClass="{required:true, messages:{required:'变更内容必须输入'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    变更文件编号
                </td>
                <td>
                    <asp:TextBox ID="txtModifyFileCode" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    预算成本
                </td>
                <td>
                    <input type="text" id="txtBudAmount" readonly="readonly" class="readonly" value="0.00" style="height: 15px; width: 100%;" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    报审价
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtReportAmount" Height="15px" Width="100%" CssClass="decimal_input" Text="0.000" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    核准价
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtApprovalAmount" Height="15px" Width="100%" CssClass="decimal_input" Text="0.000" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    核准日期
                </td>
                <td>
                    <asp:TextBox ID="txtApprovalDate" Height="15px" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
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
            <tr>
                <td colspan="4">
                    <div id="div">
                        <span id="spOutWBS" style="margin-left: 0px;" class="xxkd" onclick="clickWBSType('out')" runat="server">
                            清单外</span> <span id="spInWBS" class="xxk" onclick="clickWBSType('in')" runat="server">
                                清单内</span>
                    </div>
                </td>
            </tr>
        </table>
        <div id="divOutTask">
            <table class="tableContent2" cellpadding="5px" cellspacing="0">
                <tr>
                    <td>
                        <asp:Button Text="新增" ID="btnAddOut" OnClick="btnAddOut_Click" runat="server" />
                        <asp:Button Text="删除" ID="btnDelOut" OnClientClick="return confirm('您确定要删除吗？')" disabled="disabled" OnClick="btnDelOut_Click" runat="server" />
                        <input type="button" id="btnResOut" value="资源配置" style="width: 70px;" disabled='disabled' />
                        <input type="button" id="btnResListOut" value="资源列表" style="width: 70px;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvOutTask" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvOutTask_RowDataBound" DataKeyNames="ModifyTaskId" runat="server">
<EmptyDataTemplate>
                                <table id="emptyOutTask" class="tab" width="100%">
                                    <tr class="header">
                                        <td style="width: 20px">
                                            <asp:CheckBox ID="chkAll" runat="server" />
                                        </td>
                                        <td style="width: 25px;">
                                            序号
                                        </td>
                                        <td align="center">
                                            上级任务
                                        </td>
                                        <td align="center">
                                            清单编码
                                        </td>
                                        <td align="center">
                                            项目名称
                                        </td>
                                        <td align="center">
                                            项目特征描述
                                        </td>
                                        <td align="center">
                                            单位
                                        </td>
                                        <td align="center" style="width: 50px;">
                                            数量
                                        </td>
                                        <td align="center" style="width: 50px;">
                                            综合单价
                                        </td>
                                        <td align="center" style="width: 50px;">
                                            合价
                                        </td>
                                        <td align="center">
                                            开始时间
                                        </td>
                                        <td align="center">
                                            结束时间
                                        </td>
                                        <td align="center">
                                            工期
                                        </td>
                                        <td align="center">
                                            类型
                                        </td>
                                        <td align="center">
                                            备注
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                        <asp:CheckBox ID="cbAllBox" runat="server" />
                                    </HeaderTemplate><ItemTemplate>
                                        <asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField ID="hfldOutModifyTaskId" Value='<%# System.Convert.ToString(Eval("ModifyTaskId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="上级任务" HeaderStyle-Width="90px"><ItemTemplate>
                                        <asp:HiddenField ID="hfldOutTaskId" Value='<%# System.Convert.ToString(Eval("TaskId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                        <span class="spanSelect mustInput" style="width: 90px">
                                            <input type="text" id="txtOutTaskName" class="readonly {required:true, messages:{required:'清单外的上级任务节点必须输入'}}"
                                                flag="TaskCode" readonly="readonly" value='<%# GetTaskCode(Eval("TaskId").ToString()) %>'
                                                style="width: 65px; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" />
                                            <img alt="选择任务" onclick="openTaskList('out',this);" src="../../images/icon.bmp" style="float: right;" />
                                        </span>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="清单编码"><ItemTemplate>
                                        <asp:TextBox ID="txtOutTaskCode" flag="ModifyTaskCode" CssClass="{required:true, messages:{required:'清单外的编码必须输入'}}" onBlur="IsExistRepeatCode(this);" Style="background-color: #FFFDEA;" Text='<%# System.Convert.ToString(Eval("ModifyTaskCode"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
                                        <asp:TextBox ID="txtOutTaskContent" CssClass="{required:true, messages:{required:'清单外的名称必须输入'}}" Style="background-color: #FFFDEA;" Text='<%# System.Convert.ToString(Eval("ModifyTaskContent"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目特征描述"><ItemTemplate>
                                        <asp:TextBox ID="txtOutDescription" Style="background-color: #FFFDEA;" Text='<%# System.Convert.ToString(Eval("FeatureDescription"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位"><ItemTemplate>
                                        <asp:TextBox ID="txtOutUnit" CssClass="{required:true, messages:{required:'清单外的单价必须输入'}}" Style="background-color: #FFFDEA;" Text='<%# System.Convert.ToString(Eval("Unit"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量"><ItemTemplate>
                                        <asp:TextBox ID="txtOutQuantity" flag="Quantity" CssClass="decimal_input" Style="text-align: right;" Text='<%# System.Convert.ToString(Eval("Quantity"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="综合单价"><ItemTemplate>
                                        <asp:TextBox ID="txtOutUnitPrice" flag="UnitPrice" CssClass="decimal_input" Style="text-align: right;" Text='<%# System.Convert.ToString(Eval("UnitPrice"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合价"><ItemTemplate>
                                        <asp:TextBox ID="txtOutTotal" flag="Total" CssClass="decimal_input" Style="text-align: right;" Text='<%# System.Convert.ToString(Eval("Total"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="65px"><ItemTemplate>
                                        <asp:TextBox ID="txtOutStartDate" flag="startDate" onclick="WdatePicker();" onblur="controlDate(this);" Text='<%# System.Convert.ToString(Common2.GetTime(Eval("StartDate")), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="65px"><ItemTemplate>
                                        <asp:TextBox ID="txtOutEndDate" flag="endDate" onclick="WdatePicker();" onblur="controlDate(this);" Text='<%# System.Convert.ToString(Common2.GetTime(Eval("EndDate")), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工期" HeaderStyle-Width="65px"><ItemTemplate>
                                        <asp:TextBox ID="txtOutConstructionPeriod" flag="ConstructionPeriod" MaxLength="6" onblur="intValidator(this)" Text='<%# System.Convert.ToString(Eval("ConstructionPeriod"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型"><ItemTemplate>
                                        <asp:HiddenField ID="hfldOutTaskType" Value='<%# System.Convert.ToString(Eval("OrderNumber"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                        <input type="text" id="txtOutType" class="readonly" flag="TaskType" readonly="readonly"
                                            value='<%# GetTypeName(Eval("OrderNumber").ToString()) %>' />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                        <asp:TextBox ID="txtOutNote" Text='<%# System.Convert.ToString(Eval("Note"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divInTask">
            <table class="tableContent2" cellpadding="5px" cellspacing="0">
                <tr>
                    <td>
                        <asp:Button Text="新增" ID="btnAddIn" OnClick="btnAddIn_Click" runat="server" />
                        <asp:Button Text="删除" ID="btnDelIn" OnClientClick="return confirm('您确定要删除吗？')" disabled="disabled" OnClick="btnDelIn_Click" runat="server" />
                        <input type="button" id="btnResIn" value="资源配置" style="width: 70px;" disabled='disabled' />
                        <input type="button" id="btnResListIn" value="资源列表" style="width: 70px;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvInTask" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvInTask_RowDataBound" DataKeyNames="ModifyTaskId" runat="server">
<EmptyDataTemplate>
                                <table id="emptyInTask" class="tab" width="100%">
                                    <tr class="header">
                                        <td style="width: 20px">
                                            <asp:CheckBox ID="chkAll" runat="server" />
                                        </td>
                                        <td style="width: 25px;">
                                            序号
                                        </td>
                                        <td align="center">
                                            变更任务
                                        </td>
                                        <td align="center">
                                            变更内容
                                        </td>
                                        <td align="center">
                                            单位
                                        </td>
                                        <td align="center" style="width: 50px;">
                                            变更数量
                                        </td>
                                        <td align="center" style="width: 50px;">
                                            综合单价
                                        </td>
                                        <td align="center" style="width: 50px;">
                                            合价
                                        </td>
                                        <td align="center">
                                            备注
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                        <asp:CheckBox ID="cbAllBox" runat="server" />
                                    </HeaderTemplate><ItemTemplate>
                                        <asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField ID="hfldInModifyTaskId" Value='<%# System.Convert.ToString(Eval("ModifyTaskId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="变更任务" HeaderStyle-Width="122px"><ItemTemplate>
                                        <asp:HiddenField ID="hfldInTaskId" Value='<%# System.Convert.ToString(Eval("TaskId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                        <span class="spanSelect mustInput" style="width: 122px">
                                            <asp:TextBox ID="txtInTaskName" CssClass="readonly {required:true, messages:{required:'清单内任务节点必须输入'}}" flag="TaskCode" ReadOnly="true" Style="width: 97px; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" Text='<%# System.Convert.ToString(base.GetTaskCode(Eval("TaskId").ToString()), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                            <img alt="选择任务" onclick="openTaskList('in',this);" src="../../images/icon.bmp" style="float: right;" />
                                        </span>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="变更内容"><ItemTemplate>
                                        <asp:TextBox ID="txtInTaskContent" CssClass="{required:true, messages:{required:'清单内的变更内容必须输入'}}" Style="background-color: #FFFDEA;" Text='<%# System.Convert.ToString(Eval("ModifyTaskContent"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目特征描述"><ItemTemplate>
                                        <asp:TextBox ID="txtInDescription" Style="background-color: #FFFDEA;" Text='<%# System.Convert.ToString(Eval("FeatureDescription"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位"><ItemTemplate>
                                        <asp:TextBox ID="txtInUnit" flag="Unit" CssClass="{required:true, messages:{required:'清单内的单位必须输入'}}" Style="background-color: #FFFDEA;" Text='<%# System.Convert.ToString(Eval("Unit"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量"><ItemTemplate>
                                        <asp:TextBox ID="txtInQuantity" flag="Quantity" CssClass="decimal_input" Style="text-align: right;" Text='<%# System.Convert.ToString(Eval("Quantity"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="综合单价"><ItemTemplate>
                                        <asp:TextBox ID="txtInUnitPrice" flag="UnitPrice" CssClass="decimal_input" Style="text-align: right;" Text='<%# System.Convert.ToString(Eval("UnitPrice"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合价"><ItemTemplate>
                                        <asp:TextBox ID="txtInTotal" flag="Total" CssClass="decimal_input" Style="text-align: right;" Text='<%# System.Convert.ToString(Eval("Total"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                        <asp:HiddenField ID="hfldInTaskType" Value='<%# System.Convert.ToString(Eval("OrderNumber"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                        <asp:TextBox ID="txtInNote" Text='<%# System.Convert.ToString(Eval("Note"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
                    <asp:Button ID="btnCancel" Text="取消" OnClick="btnCancel_Click" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    
    <asp:Button ID="btnBindRes" OnClick="btnBindRes_Click" runat="server" />
    
    <asp:HiddenField ID="hfldModifyId" runat="server" />
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    
    <asp:HiddenField ID="hfldCheckedTaskIds" runat="server" />
    
    <asp:HiddenField ID="hfldCheckedTaskIdOut" runat="server" />
    
    <asp:HiddenField ID="hfldCheckedTaskIdIn" runat="server" />
    
    <asp:HiddenField ID="hfldSpType" Value="out" runat="server" />
    </form>
</body>
</html>
