<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentEdit.aspx.cs" Inherits="ContractManage_PayoutPayment_PaymentEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>支出合同付款</title>
    <style type="text/css">
        .groupInfo
        {
            text-align: left;
            padding-left: 50px;
            font-weight: bold;
        }
        
        .tableState
        {
            width: 100%;
            text-align: center; /*table-layout: fixed;*/
            border-collapse: separate;
            border-spacing: 0px 0px;
            border-right: solid 1px;
            border-bottom: solid 1px;
            border-color: #CADEED;
        }
        
        
        .tableState td
        {
            white-space: nowrap; /*overflow: hidden;*/
            padding-left: 1px;
            border-left: solid 1px;
            border-top: solid 1px;
            font-weight: normal;
            border-color: #CADEED;
        }
        .gvdata
        {
        }
    </style>
    <link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            registerCancelEvent();
            if (getRequestParam('Action') == 'Query' || !getRequestParam('Action')) {
                setAllInputDisabled();
            }
            Val.validate('form1');
            setAuditState();
            hideFundPlan();
            addEvent(document.getElementById('txtPaymentMoney'), 'blur', changeUsableMoney);
            changeUsableMoney();

            // 控制指标
            var gvBudget = new JustWinTable('gvBudget');
            setButton(gvBudget, '', '', '', 'hdCheckedIds');

            // 生成支付合同支付编号
            var Action = getRequestParam('Action');
            if (Action == 'Add') {
                var contractID = getRequestParam('ContractId');
                $.ajax({
                    type: 'GET',
                    async: false,
                    url: '/ContractManage/Handler/GetPayOutFKCode.ashx?contractID=' + contractID + "&reftime=" + (new Date()).toString(),
                    success: function (data) {
                        $('#txtPaymentCode').val(data);
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
            }
        }

        function registerCancelEvent() {
            var btnCancel = document.getElementById('btnCancel');
            addEvent(btnCancel, 'click', function () {
                winclose('PaymentEdit', 'PayoutPaymentEdit.aspx', false)
            })
        }
        //选择资金计划
        function openFundPlan() {
            top.ui._PaymentEdit = window;
            var url = '/ContractManage/PayoutPayment/FundPlanList.aspx?ContractId=' + $('#hldfContractId').val();
            top.ui.callback = function (monthData) {
                $('#txtMonthDate').val(monthData.month);
                $('#hlfdFundPlanUID').val(monthData.fundPlanUID);
                $('#txtPlanMoney').val(monthData.planMoney);
                $('#txtUsedMoney').val(monthData.usedMoney);
                $('#txtRemark').val(monthData.remark);
                $('#lblUsableMoney').val(monthData.usableMoney);
                if (monthData.usableMoney < 0) {
                    $('#lblUsableMoney').css('color', 'red');
                }
                else {
                    $('#lblUsableMoney').css('color', 'black');
                }
            }
            top.ui.openWin({ title: '选择资金计划', url: url, width: 600, height: 300 });
        }

        function changeUsableMoney() {
            var paymentMoney = document.getElementById('txtPaymentMoney').value;
            var usableMoney = document.getElementById('lblUsableMoney').value;
            if (parseFloat(paymentMoney) > parseFloat(usableMoney)) {
                document.getElementById('txtPaymentMoney').style.color = "red";
                document.getElementById('txtPaymentMoney').title = '本次支付金额已超过计划可用金额';
                if (parseFloat(usableMoney) < 0) {
                    document.getElementById('lblUsableMoney').style.color = "red";
                } else {
                    document.getElementById('lblUsableMoney').style.color = "black";
                }
            } else {
                if (parseFloat(usableMoney) < 0) {
                    document.getElementById('lblUsableMoney').style.color = "red";
                } else {
                    document.getElementById('lblUsableMoney').style.color = "black";
                }
                document.getElementById('txtPaymentMoney').style.color = "black";
                document.getElementById('txtPaymentMoney').title = '';
            }
        }

        function limitTextLenth(txtId) {
            var txtValue = txtId.value;
            if (txtValue.length > 200) {
                txtId.value = txtValue.substring(0, 200);
                alert("系统提示：\n\n输入的字数不能大于200个字！");
            }
        }

        //添加控制指标
        function addTarget() {
            top.ui._PaymentEdit = window;
            var url = '/ContractManage/PayoutPayment/ConPayoutTarget.aspx?contractId=' + $('#hldfContractId').val();
            top.ui.callback = function (targetIds) {
                $('#hfldTargetCheckeds').val(targetIds);
                $('#btnBindTarget').click();
            }
            top.ui.openWin({ title: '添加控制指标', url: url, width: 800, height: 485 });
        }
        //删除控制指标
        function delTarget() {
            var delIds = $('#hdCheckedIds').val();
            if (delIds.length > 0) {
                document.getElementById('btnDelTarget').click();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader2">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="支出合同付款" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="divContent" class="divContent2">
        <table id="tableContent" class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td colspan="4" class="groupInfo">
                    合同基本信息
                </td>
            </tr>
            <tr>
                <td class="word">
                    合同编号
                </td>
                <td class="elemTd readonly">
                    <asp:TextBox ID="txtContractCode" ReadOnly="true" Height="15px" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    合同名称
                </td>
                <td class="elemTd readonly">
                    <asp:TextBox ID="txtContractName" Height="15px" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    合同最终金额
                </td>
                <td class="elemTd readonly">
                    <asp:TextBox ID="txtContractMoney" Height="15px" CssClass="decimal_input" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    签订时间
                </td>
                <td class="elemTd readonly">
                    <asp:TextBox ID="txtSignedDate" Height="15px" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr id="tr1" runat="server"><td colspan="4" class="groupInfo" runat="server">
                    资金计划
                </td></tr>
            <tr id="tr2" runat="server"><td class="word" runat="server">
                    计划月份
                </td><td class="elemTd" style="width: 40%;" runat="server">
                    <span class="spanSelect" style="width: 100%;">
                        <input type="text" id="txtMonthDate" readonly="readonly" style="width: 90%;
                            height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                        <img alt="选择项目" onclick="openFundPlan();" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hlfdFundPlanUID" runat="server" />
                </td><td class="word" runat="server">
                    计划金额
                </td><td class="elemTd readonly" runat="server">
                    <input type="text" id="txtPlanMoney" class="decimal_input" height="15px" value="0.000" readonly="readonly" runat="server" />

                </td></tr>
            <tr id="tr3" runat="server"><td class="word" runat="server">
                    计划已用金额
                </td><td class="elemTd readonly" runat="server">
                    <input type="text" id="txtUsedMoney" class="decimal_input" height="15px" value="0.000" readonly="readonly" runat="server" />

                </td><td class="word" runat="server">
                    计划可用金额
                </td><td class="elemTd readonly" runat="server">
                    <input type="text" id="lblUsableMoney" class="decimal_input" value="0.000" readonly="readonly" style="height: 15px; line-height: 16px;" runat="server" />

                </td></tr>
            <tr id="tr4">
                <td class="word">
                    备注
                </td>
                <td colspan="3">
                    <textarea id="txtRemark" class="readonly" readonly="readonly" style="height: 50px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="groupInfo">
                    付款单信息
                </td>
            </tr>
            <tr>
                <td class="word">
                    结算累计
                </td>
                <td class="elemTd readonly">
                    <asp:TextBox ID="txtBalancedSum" CssClass="decimal_input" Height="15px" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    付款累计
                </td>
                <td class="elemTd readonly">
                    <asp:TextBox ID="txtPaySum" CssClass="decimal_input" Height="15px" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    差额
                </td>
                <td class="elemTd readonly">
                    <asp:TextBox ID="txtDiff" CssClass="decimal_input" Height="15px" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    <asp:CheckBox ID="chkContainPending" AutoPostBack="true" OnCheckedChanged="chkContainPendint_CheckedChanged" runat="server" />
                </td>
                <td class="elemTd">
                    包含待审数据
                </td>
            </tr>
            <tr>
                <td class="word">
                    支付编号
                </td>
                <td class="elemTd mustInput">
                    <asp:TextBox ID="txtPaymentCode" CssClass="{required:true, messages:{required:'支付编号必须输入'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    申请人
                </td>
                <td class="elemTd mustInput">
                    <asp:TextBox ID="txtPaymentPerson" CssClass="{required:true, messages:{required:'申请人必须输入'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    本次支付金额
                </td>
                <td class="elemTd mustInput">
                    <asp:TextBox ID="txtPaymentMoney" Text="0.000" CssClass="decimal_input {required:true,number:true, messages:{required:'支付金额必须输入',number:'支付金额格式错误'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    支付方式
                </td>
                <td>
                    <asp:RadioButtonList ID="RblPayType" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server"><asp:ListItem Value="0" Text="现金" /><asp:ListItem Value="1" Text="支票" /><asp:ListItem Value="2" Text="转账" /></asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="word">
                    要求支付日期
                </td>
                <td class="elemTd mustInput">
                    <asp:TextBox ID="txtPaymentDate" onclick="WdatePicker();" CssClass="{required:true, messages:{required:'支付日期必须输入'}}" runat="server"></asp:TextBox>
                    
                </td>
                <td class="word" style="display: none;">
                    <div style="display: none;">
                        大写金额
                    </div>
                </td>
                <td class="elemTd mustInput" style="display: none;">
                    <div style="display: none;">
                        <asp:TextBox ID="txtCapitalNumber" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td class="word">
                    收款单位
                </td>
                <td>
                    <asp:TextBox ID="txtBeneficiary" onkeyup="limitTextLenth(this); " runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    开户行
                </td>
                <td>
                    <asp:TextBox ID="txtBank" onkeyup="limitTextLenth(this);" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    账户
                </td>
                <td>
                    <asp:TextBox ID="txtAccount" onkeyup="limitTextLenth(this);" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNotes" TextMode="MultiLine" Height="50px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    附件
                </td>
                <td colspan="3" style="padding-right: 0px;">
                    <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    录入人
                </td>
                <td class="elemTd mustInput readonly">
                    <asp:TextBox ID="txtInputPerson" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    录入时间
                </td>
                <td class="elemTd mustInput readonly">
                    <asp:TextBox ReadOnly="true" ID="txtInputDate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div id='divTarget'>
                        <table class="tableContent2" cellpadding="5px" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td align="left">
                                    <input type="button" value="添加控制指标" style="width: 90px;" onclick="addTarget()" />
                                    <input type="button" value="删除控制指标" style="width: 90px;" onclick="delTarget()" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" Width="100%" OnRowDataBound="gvBudget_RowDataBound" DataKeyNames="TargetId" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                    <asp:CheckBox ID="cbAllBox" runat="server" />
                                                </HeaderTemplate><ItemTemplate>
                                                    <asp:CheckBox ID="chkBox" ToolTip='<%# Convert.ToString(Container.DataItemIndex) %>' runat="server" />
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="名称">
<ItemTemplate>
                                                    <%# Eval("TaskName") %>
                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="总金额" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                    <%# Convert.ToDecimal(Eval("BudTotal")).ToString("0.000") %>
                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合同签订金额" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                    <%# Convert.ToDecimal(Eval("SignAmount")).ToString("0.000") %>
                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="已支付金额" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                    <%# Convert.ToDecimal(Eval("PaymentedAmount")).ToString("0.000") %>
                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="本次支付金额"><ItemTemplate>
                                                    <input type="text" id="txtTheMoney" class="decimal_input" style="text-align: right;" value='<%# Convert.ToString(Convert.ToDecimal(Eval("CurrentPaymentAmount")).ToString("0.000")) %>' runat="server" />

                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="已支付金额<br/>/合同签订金额">
<ItemTemplate>
                                                    <%# Convert.ToDecimal(Eval("Rate")).ToString("P2") %>
                                                </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr id="trSate" runat="server"><td colspan="4" style="text-align: center; padding-left: 30px" runat="server">
                    <table class="tableState" cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td colspan="5">
                                指标对比表
                            </td>
                        </tr>
                        <tr>
                            <td>
                                状态
                            </td>
                            <td>
                                已结算金额
                            </td>
                            <td>
                                已支付金额
                            </td>
                            <td>
                                本次支付金额
                            </td>
                            <td>
                                差额比例
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblState" CssClass="alarm" Text="" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblBalancedAmount" CssClass="alarm" Text="" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPaymentedAmount" CssClass="alarm" Text="" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPaymentAmount" CssClass="alarm" Text="" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblRate" CssClass="alarm" Text="" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" style="text-align: left">
                                <div style="width: 12%; float: left">
                                    预警级别：
                                </div>
                                <div style="width: 88%; float: left">
                                    高：红色字体，表示已超；中：紫色字体；低：蓝色字体；
                                    <br />
                                    普通：黑色字体，表示未达到预警阀值，正常。
                                </div>
                            </td>
                        </tr>
                    </table>
                </td></tr>
        </table>
    </div>
    <div id="divConTarget" title="添加控制指标" style="display: none">
        <iframe id="ifConTarget" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <div id="divFooter" class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" />
                </td>
            </tr>
        </table>
    </div>
    <input type="button" id="btnBindTarget" style="display: none;" OnServerClick="btnBindTarget_Click" runat="server" />

    <asp:Button ID="btnDelTarget" Style="display: none;" OnClick="btnDelTarget_Click" runat="server" />
    
    <asp:HiddenField ID="hdCheckedIds" runat="server" />
    <asp:HiddenField ID="hldfContractId" runat="server" />
    <asp:HiddenField ID="hfldPaymentId" runat="server" />
    <asp:HiddenField ID="hldfIsFundPlan" runat="server" />
    <asp:HiddenField ID="hfldTargetCheckeds" runat="server" />
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    </form>
</body>
</html>
