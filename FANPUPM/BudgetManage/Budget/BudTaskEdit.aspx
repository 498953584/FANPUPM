<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudTaskEdit.aspx.cs" Inherits="BudgetManage_Budget_BudTaskEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>WBS节点新增/编辑</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Val.validate('form1', 'btnSave');
            $('#btnSave').bind('click', isValideData);

            if ($('#hfldState').val() == 'edit') {
                $('#txtEndDate').attr('readOnly', 'readOnly');
            }
            if ($('#hfldIsWBSRelevance').val() != '0') {
                // 资源与WBS挂钩
                if ($('#trCompute').length > 0) {
                    $('#txtUnitPrice').removeClass();
                    $('#txtTotal').removeClass();
                    $('#trCompute').remove();
                }
                $('#txtQuantity').removeAttr('onkeyup');
                $('#txtQuantity').removeAttr('onblur');
            }
            limitText();
            //验证工期输入的为整数
            $('#txtConstructionPeriod').blur(function () {
                intValidator();
            });
            $('#txtStartDate').attr('readonly', 'readonly');
            $('#txtEndDate').attr('readonly', 'readonly');
        });

        // 验证整数
        function intValidator() {
            var value = $('#txtConstructionPeriod').val();
            var reg = new RegExp('^[1-9]*[1-9][0-9]*$');
            if (!reg.test(value)) {
                $('#txtConstructionPeriod').val('0');
            }
        }

        // 管理时间
        function controlDate(para) {
            var startDates = document.getElementById('txtStartDate').value;

            var startDateList = startDates.split('-');
            var startDate = new Date(startDateList[0], startDateList[1] - 1, startDateList[2]);
            var endDates = document.getElementById('txtEndDate').value;

            if (startDates != "" && endDates != "") {
                if (startDate == 'NaN') {
                    top.ui.alert('输入时间格式不正确！');
                    para.value = "";
                    return;
                }
                var endDatesList = endDates.split('-');
                var endDate = new Date(endDatesList[0], endDatesList[1] - 1, endDatesList[2]);
                if (endDate == 'NaN') {
                    top.ui.alert('输入时间格式不正确！');
                    para.value = "";
                    return;
                }
                if (endDate - startDate < 0) {
                    para.value = "";
                    top.ui.alert('结束时间不能小于开始时间！请重新选择时间！')
                }
            }
            // 计算工期
            if (startDate && endDate) {
                var sd = new Date(startDate).getTime();
                var ed = new Date(endDate).getTime();
                var cp = (ed - sd) / (1000 * 3600 * 24) + 1;
                $('#txtConstructionPeriod').val(cp);
            }
        }

        // 保存节点
        function saveTask() {
            var quantity = $('#txtQuantity').val();
            if (quantity == '') {
                top.ui.alert('工程量必须输入');
            }
            else {
                if ($('#hfldIsWBSRelevance').val() == '0') {
                    if ($('#txtUnitPrice').val() == '') {
                        top.ui.alert('综合单价必须输入');
                    }
                }
            }
        }

        function isValideData() {
            if (!$('#btnSave').get(0).retVal)
                return false;
            else
                return true;
        }

        // 关闭
        function closeDialog() {
            $(parent.document).find('.ui-icon-closethick').each(function () {
                resetData();
                this.click();
            });
        }

        // 清空数据
        function resetData() {
            $("input[type=text]").each(function () {
                $('#' + this.id).val('');
                $('#ddlTaskType option:nth-child(1)').attr('selected', 'selected');
            })
        }

        // 求小计
        function getSum() {
            var sum = 0.00;
            var quantity = $("#txtQuantity").val().replace(/\,/g, '')
            var unitPrice = $("#txtUnitPrice").val().replace(/\,/g, '')
            sum = parseFloat(quantity) * parseFloat(unitPrice);
            $("#txtTotal").val(parseFloat(sum));
            if ($("#txtTotal").val() == "NaN") {
                $("#txtTotal").val(parseFloat(0.00));
            }
        }

        // 当成绩过大是，禁止保存
        function computTotal(txt) {
            var quantityStr = $("#txtQuantity").val();
            var unitPriceStr = $("#txtUnitPrice").val();
            var quantity = quantityStr.replace(/\,/g, '');
            var unitPrice = unitPriceStr.replace(/\,/g, '');
            if (!isNaN(quantity) && !isNaN(unitPrice)) {
                var total = quantity * unitPrice;
                if (total > 9999999999999) {
                    top.ui.alert('该节点的乘积过大!');
                    txt.value = 0.000;
                    $("#txtTotal").val(0.000);
                }
            }
        }

        // 限制字符
        function limitText() {
            taskCode = judgeTest($('#txtTaskCode')[0]);
            taskName = judgeTest($('#txtTaskName')[0]);
            unit = judgeTest($('#txtUnit')[0]);
            note = judgeNote();
            if (taskCode == true && taskName == true && unit == true && note == true) {
                $('#btnSave').removeAttr('disabled');
            } else {
                alert("系统提示：\n输入项的内容中存在非有效字符或不能超过100个字！");
                $('#btnSave').attr('disabled', 'disabled');
            }
        }

        // 判断备注是否存在特殊字符
        function judgeNote() {
            var isOK = true;
            var note = $('#txtNote').val();
            if (!!/[<>?'~`%\$\^《》？‘·￥]/.test(note)) {
                isOK = false;
            }
            return isOK;
        }
        // 判断任务节点信息是否符合条件
        function judgeTest(txt) {
            var isOK = true;
            var txtValue = txt.value;
            if (!!/[<>?'~`%\$\^《》？‘·￥]/.test(txtValue)) {
                isOK = false;
            }
            if (txtValue.length > 100) {
                isOK = false;
            }
            return isOK;
        }
        function closeDial(code, name, unit, quantity, note, ddlTaskType, startDate, endDate, unitPrice, total, consPeriod, id, description) {
            if (typeof top.ui.callback == 'function') {
                top.ui.callback(code, name, unit, quantity, note, ddlTaskType, startDate, endDate, unitPrice, total, consPeriod, id, description);
                top.ui.callback == null;
            }
            var url = '/BudgetManage/Template/BudTemplateList.aspx?tempId=' + $('#hfldTempId').val();
            top.ui.winSuccess({ parentName: '_BudTaskEdit', url: url });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="editBudget" style="height: 150px; vertical-align: bottom; width: 100%;">
        <table class="tableFooter2" border="1" cellpadding="8px" cellspacing="5" style="width: 100%;
            height: 99%;">
            <tr>
                <td class="descTd">
                    清单编码
                </td>
                <td class="elemTd mustInput" align="left">
                    <asp:TextBox ToolTip="清单编码不可重复" ID="txtTaskCode" Width="180px" onblur="limitText()" CssClass="{required:true, messages:{required:'清单编码必须输入'}}" runat="server"></asp:TextBox>
                    <img title="清单编码不可重复" style="cursor: pointer;" src="/images/help.jpg" />
                </td>
                <td class="descTd">
                    项目名称
                </td>
                <td class="elemTd mustInput" align="left">
                    <asp:TextBox ID="txtTaskName" Width="180px" onblur="limitText()" CssClass="{required:true, messages:{required:'项目名称必须输入'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr id="trDate">
                <td class="descTd">
                    开始时间
                </td>
                <td class="elemTd" align="left">
                    <asp:TextBox ID="txtStartDate" Width="180px" onclick="WdatePicker()" onblur="controlDate(this);" runat="server"></asp:TextBox>
                </td>
                <td class="descTd">
                    结束时间
                </td>
                <td class="elemTd" align="left">
                    <asp:TextBox ID="txtEndDate" Width="180px" onclick="WdatePicker()" onblur="controlDate(this);" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="descTd" id="tdLblConsPeriod">
                    工期(天)
                </td>
                <td class="elemTd" align="left" id="tdTxtConsPeriod">
                    <asp:TextBox ID="txtConstructionPeriod" Width="180px" MaxLength="6" runat="server"></asp:TextBox>
                </td>
                <%# Eval("Name") %>
                <td class="descTd">
                    单位
                </td>
                <td class="elemTd mustInput" align="left" id='tdUnit'>
                    <asp:TextBox ID="txtUnit" Width="180px" onblur="limitText()" CssClass="{required:true, messages:{required:'单位必须输入'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    类型
                </td>
                <td class="elemTd readonly" align="left">
                    <asp:DropDownList Enabled="false" Width="180px" ID="ddlTaskType" runat="server"></asp:DropDownList>
                </td>
                <td class="descTd">
                    工程量
                </td>
                <td class="elemTd mustInput" align="left">
                    <asp:TextBox ID="txtQuantity" Text="0" Width="180px" onkeyup="getSum()" CssClass="decimal_input" Style="text-align: right;" onblur="computTotal(this)" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr id="trCompute">
                <td class="descTd">
                    综合单价
                </td>
                <td class="elemTd mustInput" align="left">
                    <asp:TextBox ID="txtUnitPrice" Width="180px" onkeyup="getSum()" CssClass="decimal_input" onblur="computTotal(this)" Text="0" Style="text-align: right;" runat="server"></asp:TextBox>
                </td>
                <td class="descTd">
                    合计
                </td>
                <td class="elemTd readonly" align="left">
                    <input type="text" id="txtTotal" value="0.00" cssclass="decimal_input" readonly="readonly" style="text-align: right; width: 180px;" runat="server" />

                </td>
            </tr>
            <tr>
                <td>
                    项目特征描述
                </td>
                <td align="left" colspan="3">
                    <asp:TextBox ID="txtDescription" Width="96%" onblur="limitText()" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    备注
                </td>
                <td align="left" colspan="3">
                    <asp:TextBox ID="txtNote" Width="96%" onblur="limitText()" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div style="text-align: right; vertical-align: bottom; margin-top: 5px;">
            <asp:Button ID="btnSave" Text="保存" OnClientClick="isValideData();" OnClick="btnSave_Click" runat="server" />
            <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
        </div>
    </div>
    <asp:HiddenField ID="hfldState" runat="server" />
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    
    <asp:HiddenField ID="hfldTempId" runat="server" />
    </form>
</body>
</html>
