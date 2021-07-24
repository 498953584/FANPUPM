<%@ Page Language="C#" AutoEventWireup="true" CodeFile="config.aspx.cs" Inherits="Fund_config" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>参数设置</title>
    <style type="text/css">
        .desTd {
            
            text-align: left;
            padding-left:31%;
        }
        
        .desTd2{
             
            text-align: left;
            padding-left:33%;
        }
        td{
            padding-right:5px;
        }
        .group{
            text-align: right; vertical-align: top; padding-top: 10px;
        }
        .childTd{
            padding-left:30px;
        }
    </style>
    <script type="text/javascript" src="../Script/jquery.js"></script>

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="../StockManage/script/ValidateInput.js"></script>

    <script type="text/javascript">
        addEvent(window, 'load', function() {
        hidePlanEndDate()
        hideFundWarn();
        });
        
       

        function hideFundWarn() {
            if (document.getElementById('hldfIsFundWarn').value == '0') {
                document.getElementById('tr1').style.display = 'none';
                document.getElementById('tr2').style.display = 'none';
                document.getElementById('tr3').style.display = 'none';
               
            }

        }
        function hidePlanEndDate() {
            document.getElementById('tr4').style.display = 'none';
        }

        function szJC() {
            var a = document.getElementById("txtBeginDate").value;
            if (a <1 || a > 28) {
                alert("只能输入1-28之间的数字");
            }
        }
        
    </script>

</head>
<body style="overflow:auto;">
    <form id="form1" runat="server">
        <table border="1" style="border-collapse: collapse; width: 100%; margin: 0; padding: 0px;">
            <tr>
                <td colspan="2" class="divHeader">
                    参数设置</td>
            </tr>
            <tr>
                <td style="width: 38%;" class="group">
                   下月计划编制设置
                </td>
                <td style="width: 62%">
                    <table cellpadding="3px" cellspacing="0px" class="unlineTable">
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkisBeginDate" Text="开始日期设置：" AutoPostBack="true" OnCheckedChanged="chkisBeginDate_CheckedChanged" runat="server" />
                                开始于
                                <asp:TextBox ID="txtBeginDate" Width="50px" ToolTip="只能输入1-28之间的数字" onBlur="szJC()" runat="server"></asp:TextBox>号
                            </td>
                        </tr>
                        <tr id="tr4">
                            <td>
                                <asp:CheckBox ID="chkisEndDate" Text="结束日期设置：" AutoPostBack="true" OnCheckedChanged="chkisEndDate_CheckedChanged" runat="server" />
                                结束于
                                <asp:RadioButton ID="rdbisEndNowMonthT" Text="当月" GroupName="Month" AutoPostBack="true" OnCheckedChanged="rdbisEndNowMonthT_CheckedChanged" runat="server" />
                                <asp:RadioButton ID="rdbisEndNowMonthN" Text="下月" GroupName="Month" AutoPostBack="true" OnCheckedChanged="rdbisEndNowMonthN_CheckedChanged" runat="server" />
                                <asp:TextBox ID="txtEndDate" Width="50px" runat="server"></asp:TextBox>号
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id ="tr1">
                <td class="group">
                    资金支出计划完成情况(超出计划比例)
                </td>
                <td>
                    <table cellpadding="3px" cellspacing="0px" class="unlineTable">
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkisPayAccount" Text="资金支出计划预警：" AutoPostBack="true" OnCheckedChanged="chkisPayAccount_CheckedChanged" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="childTd">
                                <asp:CheckBox ID="chkisHightPayAccountRank" Text="高" AutoPostBack="true" OnCheckedChanged="chkisHightPayAccountRank_CheckedChanged" runat="server" />&nbsp;&nbsp; &nbsp;&nbsp;
                                大于<asp:TextBox ID="txtHightPayAccountRank" Width="50px" runat="server"></asp:TextBox>%
                            </td>
                        </tr>
                        <tr>
                            <td class="childTd">
                                <asp:CheckBox ID="chkisMidPayAccountRank" Text="中 " AutoPostBack="true" OnCheckedChanged="chkisMidPayAccountRank_CheckedChanged" runat="server" />&nbsp;&nbsp; &nbsp;&nbsp;
                                在...之间
                                <asp:TextBox ID="txtBeginMidPayAccountRank" Width="50px" runat="server"></asp:TextBox>%
                                <asp:TextBox ID="txtEndMidPayAccountRank" Width="50px" runat="server"></asp:TextBox>%
                            </td>
                        </tr>
                        <tr>
                            <td class="childTd">
                                <asp:CheckBox ID="chkisLowPayAccountRank" Text="低" AutoPostBack="true" OnCheckedChanged="chkisLowPayAccountRank_CheckedChanged" runat="server" />&nbsp;&nbsp; &nbsp;&nbsp;
                                在...之间
                                <asp:TextBox ID="txtBeginLowPayAccountRank" Width="50px" runat="server"></asp:TextBox>%
                                <asp:TextBox ID="txtEndLowPayAccountRank" Width="50px" runat="server"></asp:TextBox>%
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="tr2">
                <td class="group">
                    资金收入计划完成情况(低于计划比例)
                </td>
                <td>
                    <table cellpadding="3px" cellspacing="0px" class="unlineTable">
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkisIcAccount" Text="资金收入计划完成情况预警：" AutoPostBack="true" OnCheckedChanged="chkisIcAccount_CheckedChanged" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkisHightIcAccountRank" Text="高" AutoPostBack="true" OnCheckedChanged="chkisHightIcAccountRank_CheckedChanged" runat="server" />&nbsp;&nbsp; &nbsp;&nbsp;
                                大于<asp:TextBox ID="txtHightIcAccountRank" Width="50px" runat="server"></asp:TextBox>%
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkisMidIcAccountRank" Text="中" AutoPostBack="true" OnCheckedChanged="chkisMidIcAccountRank_CheckedChanged" runat="server" />&nbsp;&nbsp;
                                &nbsp;&nbsp; 在...之间
                                <asp:TextBox ID="txtBeginMidIcAccountRank" Width="50px" runat="server"></asp:TextBox>%
                                <asp:TextBox ID="txtEndMidIcAccountRank" Width="50px" runat="server"></asp:TextBox>%
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkisLowIcAccountRank" Text="低" AutoPostBack="true" OnCheckedChanged="chkisLowIcAccountRank_CheckedChanged" runat="server" />&nbsp;&nbsp;
                                &nbsp;&nbsp; 在...之间
                                <asp:TextBox ID="txtBeginLowIcAccountRank" Width="50px" runat="server"></asp:TextBox>%
                                <asp:TextBox ID="txtEndLowIcAccountRank" Width="50px" runat="server"></asp:TextBox>%
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="tr3">
                <td class="group">
                    过期未归还借款(未归还比例)
                </td>
                <td>
                    <table cellpadding="3px" cellspacing="0px" class="unlineTable">
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkisLoanAccount" Text="过期未归还借款预警：" AutoPostBack="true" OnCheckedChanged="chkisLoanAccount_CheckedChanged" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkisHightLoanAccountRank" Text="高" AutoPostBack="true" OnCheckedChanged="chkisHightLoanAccountRank_CheckedChanged" runat="server" />&nbsp;&nbsp; &nbsp;&nbsp;
                                大于<asp:TextBox ID="txtHightLoanAccountRank" Width="50px" runat="server"></asp:TextBox>%
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkisMidLoanAccountRank" Text="中" AutoPostBack="true" OnCheckedChanged="chkisMidLoanAccountRank_CheckedChanged" runat="server" />&nbsp;&nbsp;
                                &nbsp;&nbsp; 在...之间
                                <asp:TextBox ID="txtBeginMidLoanAccountRank" Width="50px" runat="server"></asp:TextBox>%
                                <asp:TextBox ID="txtEndMidLoanAccountRank" Width="50px" runat="server"></asp:TextBox>%
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkisLowLoanAccountRank" Text="低" AutoPostBack="true" OnCheckedChanged="chkisLowLoanAccountRank_CheckedChanged" runat="server" />&nbsp;&nbsp;
                                &nbsp;&nbsp; 在...之间
                                <asp:TextBox ID="txtBeginLowLoanAccountRank" Width="50px" runat="server"></asp:TextBox>%
                                <asp:TextBox ID="txtEndLowLoanAccountRank" Width="50px" runat="server"></asp:TextBox>%
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="divFooter" style="text-align: right">
                    <asp:Button ID="butSave" Text="保存" ValidationGroup="save" OnClick="butSave_Click" runat="server" />
                    <asp:Button ID="butCancel" Text="取消" OnClick="butCancel_Click" runat="server" />
                    <asp:HiddenField ID="hldfIsFundWarn" runat="server" />
                    </td>
            </tr>
        </table>
    </form>
</body>
</html>
