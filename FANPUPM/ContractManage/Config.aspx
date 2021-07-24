<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Config.aspx.cs" Inherits="ContractManage_Config" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>参数设置</title>
    <style type="text/css">
        .desTd
        {
            text-align: left;
            padding-left: 31%;
        }
        
        .desTd2
        {
            text-align: left;
            padding-left: 33%;
        }
        td
        {
            padding-right: 5px;
        }
        .group
        {
            text-align: right;
            vertical-align: top;
            padding-top: 10px;
        }
        .childTd
        {
            padding-left: 30px;
        }
    </style>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../StockManage/script/ValidateInput.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        //判断输入的值是否是数值类型
        function funsavebefore() {
            if (isNaN($('#txtPayoutAlarmDays').val())) {
                alert('请输入数值类型！');
                return false;
            }
            if (isNaN($('#txtIncomeAlarmDays').val())) {
                alert('请输入数值类型！');
                return false;
            }
            if (isNaN($('#txtHighBalanceAlarmLimit').val())) {
                alert('请输入数值类型！');
                return false;
            }
            if (isNaN($('#txtMidBalanceAlarmLowerLimit').val())) {
                alert('请输入数值类型！');
                return false;
            }
            if (isNaN($('#txtMidBalanceAlarmUpperLimit').val())) {
                alert('请输入数值类型！');
                return false;
            }
            if (isNaN($('#txtLowBalanceAlarmLowerLimit').val())) {
                alert('请输入数值类型！');
                return false;
            }
            if (isNaN($('#txtLowBalanceAlarmUpperLimit').val())) {
                alert('请输入数值类型！');
                return false;
            }
            if (isNaN($('#txtHighPayAlarmLimit').val())) {
                alert('请输入数值类型！');
                return false;
            }
            if (isNaN($('#txtMidPayAlarmLowerLimit').val())) {
                alert('请输入数值类型！');
                return false;
            }
            if (isNaN($('#txtMidPayAlarmUpperLimit').val())) {
                alert('请输入数值类型！');
                return false;
            }
            if (isNaN($('#txtLowPayAlarmLowerLimit').val())) {
                alert('请输入数值类型！');
                return false;
            }
            if (isNaN($('#txtLowPayAlarmUpperLimit').val())) {
                alert('请输入数值类型！');
                return false;
            }
        }
    </script>
</head>
<body style="overflow: hidden">
    <form id="form1" runat="server">
    <div style="height: 95%;" id="divContent2">
        <table border="1" style="border-collapse: collapse; width: 100%; margin: 0; padding: 0px;
            border-color: rgb(240,240,240);">
            <tr>
                <td colspan="2" class="divHeader">
                    参数设置
                </td>
            </tr>
            <tr>
                <td style="width: 38%;" class="group">
                    提醒
                </td>
                <td style="width: 62%">
                    <table cellpadding="3px" cellspacing="0px" class="unlineTable">
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkPayoutPay" Text="付款提醒：" AutoPostBack="true" OnCheckedChanged="chkPayoutPay_CheckedChanged" runat="server" />
                                小于等于
                                <asp:TextBox ID="txtPayoutAlarmDays" Width="50px" MaxLength="2" runat="server"></asp:TextBox>日历天
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkIncomePay" Text="回款提醒：" AutoPostBack="true" OnCheckedChanged="chkIncomePay_CheckedChanged" runat="server" />
                                小于等于
                                <asp:TextBox ID="txtIncomeAlarmDays" Width="50px" MaxLength="2" runat="server"></asp:TextBox>日历天
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="group">
                    合同结算预警
                </td>
                <td>
                    <table cellpadding="3px" cellspacing="0px" class="unlineTable">
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkIsBalanceAlarm" Text="合同结算预警：" AutoPostBack="true" OnCheckedChanged="chkIsBalanceAlarm_CheckedChanged" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="childTd">
                                高&nbsp;&nbsp; &nbsp;&nbsp; 大于<asp:TextBox ID="txtHighBalanceAlarmLimit" Width="50px" MaxLength="2" runat="server"></asp:TextBox>%
                            </td>
                        </tr>
                        <tr>
                            <td class="childTd">
                                中&nbsp;&nbsp; &nbsp;&nbsp;在...之间
                                <asp:TextBox ID="txtMidBalanceAlarmLowerLimit" Width="50px" MaxLength="2" runat="server"></asp:TextBox>%
                                <asp:TextBox ID="txtMidBalanceAlarmUpperLimit" Width="50px" MaxLength="2" runat="server"></asp:TextBox>%
                            </td>
                        </tr>
                        <tr>
                            <td class="childTd">
                                低&nbsp;&nbsp; &nbsp;&nbsp;在...之间
                                <asp:TextBox ID="txtLowBalanceAlarmLowerLimit" Width="50px" MaxLength="2" runat="server"></asp:TextBox>%
                                <asp:TextBox ID="txtLowBalanceAlarmUpperLimit" Width="50px" MaxLength="2" runat="server"></asp:TextBox>%
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="group">
                    合同支付预警
                </td>
                <td>
                    <table cellpadding="3px" cellspacing="0px" class="unlineTable">
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkIsPaymentAlarm" Text="合同支付预警：" AutoPostBack="true" OnCheckedChanged="chkIsPaymentAlarm_CheckedChanged" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="childTd">
                                高&nbsp;&nbsp; &nbsp;&nbsp; 大于<asp:TextBox ID="txtHighPayAlarmLimit" Width="50px" MaxLength="2" runat="server"></asp:TextBox>%
                            </td>
                        </tr>
                        <tr>
                            <td class="childTd">
                                中&nbsp;&nbsp;&nbsp;&nbsp; 在...之间
                                <asp:TextBox ID="txtMidPayAlarmLowerLimit" Width="50px" MaxLength="2" runat="server"></asp:TextBox>%
                                <asp:TextBox ID="txtMidPayAlarmUpperLimit" Width="50px" MaxLength="2" runat="server"></asp:TextBox>%
                            </td>
                        </tr>
                        <tr>
                            <td class="childTd">
                                低&nbsp;&nbsp;&nbsp;&nbsp; 在...之间
                                <asp:TextBox ID="txtLowPayAlarmLowerLimit" Width="50px" MaxLength="2" runat="server"></asp:TextBox>%
                                <asp:TextBox ID="txtLowPayAlarmUpperLimit" Width="50px" MaxLength="2" runat="server"></asp:TextBox>%
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div style="height: 5%;" id="divFooter2">
        <table class="tableFooter2" width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="divFooter" style="text-align: right">
                    <asp:Button ID="butSave" Text="保存" ValidationGroup="save" OnClientClick="return funsavebefore();" OnClick="butSave_Click" runat="server" />
                    <asp:Button ID="butCancel" Text="取消" OnClick="butCancel_Click" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
