<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudContractTaskTaskEdit.aspx.cs" Inherits="BudgetManage_Budget_BudContractTaskTaskEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>合同预算节点新增/编辑</title>
    <style type="text/css">
        .tableFooter2
        {
            width: 580px;
        }
        
        .tableFooter2 tr
        {
            vertical-align: middle;
            height: 32px;
        }
        .descTd
        {
            width: 75px;
            padding-right: 5px;
        }
        .elemTd
        {
            padding-left: 5px;
        }
    </style>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Val.validate('form1', 'btnSave');
            $('#btnSave').bind('click', isValideData);
            if ($('#hfldState').val() == 'edit') {
                $('#txtTaskCode').attr('readOnly', 'readOnly');
            }
            limitText();
			//验证工期输入的为整数
            $('#txtConstructionPeriod').blur(function () {
                intValidator();
            });
            // 不需要自动计算结束时间，要考虑到非工作日
            // $('#txtConstructionPeriod').keyup(calcEndDate);
        });

        //管理时间
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

        // 计算结束日期
        function calcEndDate() {
            try {
                var sd = jw.strToDate($('#txtStartDate').val());
                var cp = $('#txtConstructionPeriod').val();
                if (isNaN(cp)) return;
                var ed = jw.addDay(sd, cp);
                $('#txtEndDate').val(jw.dateToStr(ed));
            } catch(err) {
            }
        }
       
        //保存节点
        function saveTask() {
            var quantity = $('#txtQuantity').val();
            if (quantity == '') {
                top.ui.alert('工程量必须输入');
            }
            else {
                //            parent.sendTaskData();
            }
        }
        function isValideData() {
            if (!$('#btnSave').get(0).retVal)
                return false;
            else
                return true;
        }
        //关闭
        function closeDialog() {
            $(parent.document).find('.ui-icon-closethick').each(function () {
                resetData();
                this.click();
            });
                    }
        //清空数据
        function resetData() {
            $("input[type=text]").each(function () {
                $('#' + this.id).val('');
                $('#ddlTaskType option:nth-child(1)').attr('selected', 'selected');
            })
        }
        //求小计
        function getSum() {
            var sum = 0;
            var quantity = $("#txtQuantity").val().replace(/\,/g, '');
            var unitPrice = $("#txtUnitPrice").val().replace(/\,/g, '');
            sum = parseFloat(quantity) * parseFloat(unitPrice);
            $("#txtTotal").val(sum);
            if ($("#txtTotal").val() == "NaN") {
                $("#txtTotal").val(parseFloat(0));
            }
        }

        //当成绩过大是，禁止保存
        function computTotal(txt) {
            var quantity = $("#txtQuantity").val().replace(/\,/g, '');
            var unitPrice = $("#txtUnitPrice").val().replace(/\,/g, '');
            if (!isNaN(quantity) && !isNaN(unitPrice)) {
                var total = quantity * unitPrice;
                if (total > 1500000000) {
                    top.ui.alert('该分部分项的乘积过大!');
                    txt.value = 0.000;
                    $("#txtTotal").val(0.000);
                }
            }
        }

		// 验证整数
        function intValidator() {
            var value = $('#txtConstructionPeriod').val();
            var reg = new RegExp('^[1-9]*[1-9][0-9]*$');
            if (!reg.test(value)) {
                $('#txtConstructionPeriod').val('0');
            }
        }

        //限制字符
        function limitText() {
            taskCode = judgeTest($('#txtTaskCode')[0]);
            taskName = judgeTest($('#txtTaskName')[0]);
            unit = judgeTest($('#txtUnit')[0]);
            note = judgeNote();
            if (taskCode == true && taskName == true && unit == true && note == true) {
                $('#btnSave').removeAttr('disabled');
            } else {
                top.ui.alert('输入项的内容中存在非有效字符或不能超过100个字！');
                $('#btnSave').attr('disabled', 'disabled');
            }
        }

        //判断备注是否存在特殊字符
        function judgeNote()
        { 
            var isOK = true;
            var note= $('#txtNote').val();
            if(!!/[<>?/'~`%\$\^《》？‘·￥]/.test(note)){
                isOK =  false;
            }
            return isOK;
        }

        //判断任务节点信息是否符合条件
        function judgeTest(txt) {
            var isOK = true;
            var txtValue = txt.value;
            if(!!/[<>?/'~`%\$\^《》？‘·￥]/.test(txtValue)){
                isOK =  false;
            }
            if (txtValue.length > 100) {
                isOK =  false;
            }
           return isOK;
        }
        function closeDial(action,info){        
           if (typeof top.ui.callback == 'function') {
                top.ui.callback(action,info);
                top.ui.callback == null;
            }
            top.ui.winSuccess({parentName:'_BudContractTaskTaskEdit'});
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="editBudget" style="height: 120px; vertical-align: bottom; width: 100%;">
        <table class="tableFooter2" border="1" cellpadding="1px" cellspacing="1px">
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
            <tr>
                <td class="descTd">
                    类型
                </td>
                <td class="elemTd mustInput" align="left">
                    <asp:DropDownList Enabled="false" Width="180px" ID="ddlTaskType" runat="server"></asp:DropDownList>
                </td>
                <td class="descTd">
                    单位
                </td>
                <td class="elemTd mustInput" align="left">
                    <asp:TextBox ID="txtUnit" Width="180px" onblur="limitText()" CssClass="{required:true, messages:{required:'单位必须输入'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    工程量
                </td>
                <td class="elemTd mustInput" align="left">
                    <asp:TextBox ID="txtQuantity" Text="0" Width="180px" onkeyup="getSum()" CssClass="decimal_input" Style="text-align: right;" onblur="computTotal(this)" runat="server"></asp:TextBox>
                </td>
                <td class="descTd">
                    综合单价
                </td>
                <td class="elemTd mustInput" align="left">
                    <asp:TextBox ID="txtUnitPrice" Width="180px" onkeyup="getSum()" CssClass="decimal_input" Text="0" Style="text-align: right;" onblur="computTotal(this)" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    主材
                </td>
                <td class="elemTd mustInput" align="left">
                    <asp:TextBox ID="txtMainMaterial" Text="0" Width="180px" CssClass="decimal_input" Style="text-align: right;" runat="server"></asp:TextBox>
                </td>
                <td class="descTd">
                    辅材
                </td>
                <td class="elemTd mustInput" align="left">
                    <asp:TextBox ID="txtSubMaterial" Text="0" Width="180px" CssClass="decimal_input" Style="text-align: right;" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    人工
                </td>
                <td class="elemTd mustInput" align="left">
                    <asp:TextBox ID="txtLabor" Text="0" Width="180px" CssClass="decimal_input" Style="text-align: right;" runat="server"></asp:TextBox>
                </td>
                <td class="descTd">
                    合价
                </td>
                <td class="elemTd mustInput" align="left">
                    <asp:TextBox ID="txtTotal" Text="0" Width="180px" class="readonly decimal_input" Style="text-align: right;" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
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
                <td class="descTd">
                    工期(天)
                </td>
                <td class="elemTd" align="left">
                    <asp:TextBox ID="txtConstructionPeriod" Width="180px" MaxLength="6" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    项目特征描述
                </td>
                <td class="elemTd" align="left" colspan="3">
                    <asp:TextBox ID="txtDescription" Width="470px" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    备注
                </td>
                <td align="left" class="elemTd" colspan="3">
                    <asp:TextBox ID="txtNote" Width="470px" onblur="limitText()" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div style="text-align: right; vertical-align: bottom; margin-top: 10px;">
            <asp:Button ID="btnSave" Text="保存" OnClientClick="isValideData();" OnClick="btnSave_Click" runat="server" />
            <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
        </div>
    </div>
    <asp:HiddenField ID="hfldState" runat="server" />
    </form>
</body>
</html>
