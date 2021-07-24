<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FundPlanList.aspx.cs" Inherits="ContractManage_PayoutPayment_FundPlanList" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            var table = new JustWinTable('gvwFundPlan');
            replaceEmptyTable('emptyFundPlan', 'gvwFundPlan');
            $('#btnSave').click(function () {
                var monthData = {};
                monthData.month = $('#hfldPlanDate').val();
                monthData.fundPlanUID = $('#hfldUID').val();
                monthData.planMoney = $('#hfldPlanMoney').val();
                monthData.usedMoney = $('#hfldUseMoney').val();
                monthData.remark = $('#hfldRemark').val();
                monthData.usableMoney = $('#hfldUsableMoney').val();
                if (typeof top.ui.callback == 'function') {
                    top.ui.callback(monthData);
                    top.ui.callback = null;
                }
                top.ui.closeWin();
            });
            $('#btnCancel').click(function () {
                top.ui.closeWin();
            });
        });

        function setHd(UID, PlanDate, PlanMoney, UseMoney, UsableMoney, Remark) {
            document.getElementById("hfldUID").value = UID;
            document.getElementById("hfldPlanDate").value = PlanDate;
            document.getElementById("hfldPlanMoney").value = PlanMoney;
            document.getElementById("hfldUseMoney").value = UseMoney;
            document.getElementById("hfldUsableMoney").value = UsableMoney;
            document.getElementById("hfldRemark").value = Remark;
            document.getElementById("btnSave").disabled = false;
        }

        function save(UID, PlanDate, PlanMoney, UseMoney, UsableMoney, Remark) {
            parent.document.getElementById('txtMonthDate').value = PlanDate;
            parent.document.getElementById('hlfdFundPlanUID').value = UID;
            parent.document.getElementById('txtPlanMoney').value = PlanMoney;
            parent.document.getElementById('txtUsedMoney').value = UseMoney;
            parent.document.getElementById('txtRemark').value = Remark;
            if (parseFloat(UsableMoney) < 0) {
                parent.document.getElementById('lblUsableMoney').value = UsableMoney;
                parent.document.getElementById('lblUsableMoney').style.color = "red";
                //                parent.document.getElementById('lblMessage').value = "(计划已用金额已大于计划金额)";
                //                parent.document.getElementById('lblMessage').style.display = "inline";
            } else {
                parent.document.getElementById('lblUsableMoney').value = UsableMoney;
                parent.document.getElementById('lblUsableMoney').style.color = "black";
                //parent.document.getElementById('lblMessage').style.display = "none";
            }
            $(parent.document).find('.ui-icon-closethick').each(function () {
                this.click();
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="pagediv">
        <table class="tab" style="height: 85%;">
            <tr>
                <td valign="top" align="center" style="border: solid 0px red; height: 100%; padding-top: 10px;">
                    <div id="pagediv" style="width: 100%; border: solid 0px red; text-align: left;" align="center">
                        <asp:GridView ID="gvwFundPlan" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" OnRowDataBound="gvwFundPlan_RowDataBound" DataKeyNames="UID,PlanDate,PlanMoney,UsedMoney,UsableMoney,Remark" runat="server">
<EmptyDataTemplate>
                                <table id="emptyFundPlan" class="gvdata">
                                    <tr class="header">
                                        <th scope="col" style="width: 25px;">
                                            序号
                                        </th>
                                        <th scope="col">
                                            计划月份
                                        </th>
                                        <th scope="col">
                                            计划金额
                                        </th>
                                        <th scope="col">
                                            计划已用金额
                                        </th>
                                        <th scope="col">
                                            计划可用金额
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计划月份" HeaderStyle-Width="80px">
<ItemTemplate>
                                        <%# Convert.ToDateTime(Eval("PlanDate")).ToString("yyyy年MM月") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="计划金额" HeaderStyle-Width="50px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# Convert.ToDecimal(Eval("PlanMoney")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="计划已用金额" HeaderStyle-Width="50px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# Convert.ToDecimal(Eval("UsedMoney")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="计划可用金额" HeaderStyle-Width="50px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# Convert.ToDouble(Eval("UsableMoney")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                        <%# Eval("Remark") %>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
        <div style="text-align: right">
            <input id="btnSave" type="button" class="button-normal" value="确 定" disabled="disabled" />
            <input id="btnCancel" type="button" value="取 消" class="button-normal" />
        </div>
        <asp:HiddenField ID="hfldUID" runat="server" />
        <asp:HiddenField ID="hfldPlanDate" runat="server" />
        <asp:HiddenField ID="hfldPlanMoney" runat="server" />
        <asp:HiddenField ID="hfldUseMoney" runat="server" />
        <asp:HiddenField ID="hfldUsableMoney" runat="server" />
        <asp:HiddenField ID="hfldRemark" runat="server" />
    </div>
    </form>
</body>
</html>
