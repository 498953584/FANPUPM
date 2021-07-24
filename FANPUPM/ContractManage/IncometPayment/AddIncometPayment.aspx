<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddIncometPayment.aspx.cs" Inherits="ContractManage_IncometPayment_AddIncometPayment" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>收入合同收款</title><link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if (getRequestParam('t') == '1') {
                setAllInputDisabled();
            }
            hideFundPlan();
            addEvent(document.getElementById('txtCllectionPrice'), 'blur', changeAllowCollectMoney);
            changeAllowCollectMoney();

            // 根据收入合同生成合同收款编码
            var id = getRequestParam('id');
            if (id == '') {
                var contractID = getRequestParam('ContractID');
                $.ajax({
                    type: 'GET',
                    async: false,
                    url: '/ContractManage/Handler/GetIncometPaySKCode.ashx?contractID=' + contractID + "&reftime=" + (new Date()).toString(),
                    success: function (data) {
                        $('#txtCllectionCode').val(data);
                    }
                });
            }

        });

        function hideFundPlan() {
            if (document.getElementById('hldfIsFundPlan').value == '0') {
                document.getElementById('tr1').style.display = 'none';
                document.getElementById('tr2').style.display = 'none';
                document.getElementById('tr3').style.display = 'none';
                document.getElementById('tr4').style.display = 'none';
            } else {
                if (document.getElementById('hldfIsExamineApprove').value == '0') {
                    //document.getElementById('tr3').style.display = 'none';
                }
            }
        }

        // 选择资金计划
        function openFundPlan() {
            var url = '';
            if ($('#hldfIsExamineApprove').val() == "0") {
                url = "/ContractManage/IncometPayment/FundPlanList.aspx?ContractId=" +
                    $('#hldfContractId').val() + "&isExamineApprove=" + $('#hldfIsExamineApprove').val();
                top.ui.callback = function (obj) {
                    $('#txtMonthDate').val(obj.date);
                    $('#hlfdFundPlanUID').val(obj.id);
                    $('#txtPlanMoney').val(obj.planMoney);
                    $('#txtCollectedMoney').val(obj.collectedMoney);
                    $('#txtFundPlanRemark').val(obj.remark);
                    $('#lblAllowCollectMoney').val(obj.allowCollectMoney); //text(obj.allowCollectMoney);
                    if (parseFloat($('#txtCllectionPrice').val()) < parseFloat(obj.allowCollectMoney)) {
                        $('#txtCllectionPrice').attr('title', '收款金额小于计划未完成金额').css('color', 'Red');
                    } else {
                        $('#txtCllectionPrice').attr('title', '').css('color', 'black');
                    }
                }
            } else {
                url = "/ContractManage/IncometPayment/FundPlanListApprove.aspx?ContractId=" +
                $('#hldfContractId').val() + "&isExamineApprove=" + $('#hldfIsExamineApprove').val();
                top.ui.callback = function (obj) {
                    $('#txtMonthDate').val(obj.date);
                    $('#hlfdFundPlanUID').val(obj.id);
                    $('#txtPlanMoney').val(obj.planMoney);
                    $('#txtCollectedMoney').val(obj.collectedMoney);
                    $('#txtFundPlanRemark').val(obj.remark);
                    $('#lblAllowCollectMoney').val(obj.allowCollectMoney); //.text(obj.allowCollectMoney);
                    if (parseFloat(obj.allowCollectMoney) < 0) {
                        $('#lblAllowCollectMoney').val(obj.allowCollectMoney).css('color', 'Red'); //text(obj.allowCollectMoney)
                        $('#lblMessage').text('计划已用金额已大于计划金额').css('display', 'inline');
                    } else {
                        $('#lblAllowCollectMoney').val(obj.allowCollectMoney).css('color', 'black'); //text(obj.allowCollectMoney).
                        $('#lblMessage').css('display', 'none');
                    }
                }
            }


            top.ui.openWin({ title: '选择资金计划', url: url });
        }

        function changeAllowCollectMoney() {
            var cllectionPrice = document.getElementById('txtCllectionPrice').value;
            var allowCollectMoney = document.getElementById('lblAllowCollectMoney').value;
            if (parseFloat(cllectionPrice) < parseFloat(allowCollectMoney)) {
                document.getElementById('txtCllectionPrice').style.color = 'red';
                document.getElementById('txtCllectionPrice').title = '收款金额小于计划未完成金额';
            } else {
                document.getElementById('txtCllectionPrice').style.color = 'black';
                document.getElementById('txtCllectionPrice').title = '';
            }
        }

        function inputValidate() {
            if ($('#txtCllectionCode').val() == "") {
                top.ui.alert("收款编号必须输入！");
                return false;
            }
            if ($('#txtCllectionUser').val() == "") {
                top.ui.alert("收款人必须输入！");
                return false;
            }
            if ($('#txtCllectionPrice').val() == "") {
                top.ui.alert("收款金额必须输入！");
                return false;
            }
            if ($('#txtCllectionTime').val() == "") {
                top.ui.alert("收款日期必须输入！");
                return false;
            }
            return true;
        }

    </script>
    <style type="text/css">
        #FileLink1_But_UpFile
        {
            width: auto;
        }
        #FileLink1_Btn_Upload
        {
            width: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader2">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="divContent2">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td colspan="4" class="groupInfo">
                    合同基本信息
                </td>
            </tr>
            <tr>
                <td class="word">
                    合同编号
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtContractCode" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    合同名称
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtContractName" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    合同金额
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtContractPrice" CssClass="decimal_input" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    签订时间
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtSignedTime" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    项目编号
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtPrjCode" ReadOnly="true" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    项目名称
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtPrjName" ReadOnly="true" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr id="tr1" runat="server"><td colspan="4" class="groupInfo" runat="server">
                    资金计划
                </td></tr>
            <tr id="tr2" runat="server"><td class="word" runat="server">
                    计划月份
                </td><td class="elemTd" style="width: 40%;" runat="server">
                    <input type="text" id="txtMonthDate" readonly="readonly" class="select_input" imgclick="openFundPlan" style="width: 100%;" runat="server" />

                    <asp:HiddenField ID="hlfdFundPlanUID" runat="server" />
                </td><td class="word" runat="server">
                    计划金额
                </td><td class="elemTd readonly" runat="server">
                    <input type="text" id="txtPlanMoney" class="decimal_input" height="15px" value="0.000" readonly="readonly" runat="server" />

                </td></tr>
            <tr id="tr3" runat="server"><td class="word" runat="server">
                    计划已完成金额
                </td><td class="elemTd readonly" runat="server">
                    <input type="text" id="txtCollectedMoney" class="decimal_input" height="15px" value="0.000" readonly="readonly" runat="server" />

                </td><td class="word" runat="server">
                    计划未完成金额
                </td><td class="elemTd readonly" runat="server">
                    <input type="text" id="lblAllowCollectMoney" class="decimal_input" value="0.000" readonly="readonly" style="height: 15px; line-height: 16px;" runat="server" />

                </td></tr>
            <tr id="tr4">
                <td class="word">
                    备注
                </td>
                <td colspan="3">
                    <textarea id="txtFundPlanRemark" class="readonly" readonly="readonly" style="height: 50px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="groupInfo">
                    收款信息
                </td>
            </tr>
            <tr>
                <td class="word">
                    收款累计
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtSumCllection" CssClass="decimal_input" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    差额
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtDiffAmount" CssClass="decimal_input" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    收款编号
                </td>
                <td class="txt mustInput">
                    <asp:TextBox Height="15px" Width="100%" ID="txtCllectionCode" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    收款人
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtCllectionUser" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    收款金额
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtCllectionPrice" Height="15px" Width="100%" Text="0.000" CssClass="easyui-numberbox" data-options="min:-99999999999,max:99999999999,precision:3,groupSeparator:','" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    收款日期
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtCllectionTime" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="word">
                    备注
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" id="td1">
                    附件
                </td>
                <td colspan="3" style="text-align: left; padding-right: 0px;" class="txt">
                    <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    录入人
                </td>
                <td>
                    <asp:TextBox ID="txtInputPerson" class="readonly" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    录入时间
                </td>
                <td class="txt readonly">
                    <asp:TextBox Width="100%" ReadOnly="true" ID="txtInputDate" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClientClick="return inputValidate();" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="winclose('AddIncometPayment','ShowPaymentList.aspx',false)" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdGuid" runat="server" />
    <asp:HiddenField ID="hdCode" runat="server" />
    <asp:HiddenField ID="hldfContractId" runat="server" />
    <asp:HiddenField ID="hldfIsFundPlan" runat="server" />
    <asp:HiddenField ID="hldfIsExamineApprove" runat="server" />
    </form>
</body>
</html>
