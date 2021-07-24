<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FundPlanList.aspx.cs" Inherits="ContractManage_PayoutPayment_FundPlanList" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var table = new JustWinTable('gvwFundPlan');
            replaceEmptyTable('emptyFundPlan', 'gvwFundPlan');
        });

        function setHd(UID, PlanDate, PlanMoney, CollectedMoney, AllowCollectMoney, Remark) {
            document.getElementById("hfldUID").value = UID;
            document.getElementById("hfldPlanDate").value = PlanDate;
            document.getElementById("hfldPlanMoney").value = PlanMoney;
            document.getElementById("hfldCollectedMoney").value = CollectedMoney;
            document.getElementById("hfldAllowCollectMoney").value = AllowCollectMoney;
            document.getElementById("hfldRemark").value = Remark;
            document.getElementById("btnSave").disabled = false;
        }

        function save(UID, PlanDate, PlanMoney, CollectedMoney, AllowCollectMoney, Remark) {
            saveEvent();
        }

        function saveEvent() {
            if (typeof top.ui.callback == 'function') {
                var obj = {};
                obj.date = $('#hfldPlanDate').val();
                obj.id = $('#hfldUID').val();
                obj.planMoney = $('#hfldPlanMoney').val();
                obj.collectedMoney = $('#hfldCollectedMoney').val();
                obj.remark = $('#hfldRemark').val();
                obj.allowCollectMoney = $('#hfldAllowCollectMoney').val();
                top.ui.callback(obj);
                top.ui.callback = null;
            }
            top.ui.closeWin();
            //            parent.document.getElementById('txtMonthDate').value = document.getElementById("hfldPlanDate").value;
            //            parent.document.getElementById('hlfdFundPlanUID').value = document.getElementById("hfldUID").value;
            //            parent.document.getElementById('txtPlanMoney').value = document.getElementById("hfldPlanMoney").value;
            //            parent.document.getElementById('txtCollectedMoney').value = document.getElementById("hfldCollectedMoney").value;
            //            parent.document.getElementById('txtFundPlanRemark').value = document.getElementById("hfldRemark").value;
            //            var AllowCollectMoney = document.getElementById("hfldAllowCollectMoney").value;
            //            var CllectionPrice = parent.document.getElementById('txtCllectionPrice').value;
            //            parent.document.getElementById('lblAllowCollectMoney').value = AllowCollectMoney;
            //            if (parseFloat(CllectionPrice) < parseFloat(AllowCollectMoney)) {
            //                parent.document.getElementById('txtCllectionPrice').style.color = 'red';
            //                parent.document.getElementById('txtCllectionPrice').title = '收款金额小于计划未完成金额';
            //            } else {
            //                parent.document.getElementById('txtCllectionPrice').style.color = 'black';
            //                parent.document.getElementById('txtCllectionPrice').title = '';
            //            }
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
                                            已完成金额
                                        </th>
                                        <th scope="col">
                                            未完成金额
                                        </th>
                                        <th scope="col">
                                            备注
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计划月份" HeaderStyle-Width="100px">
<ItemTemplate>
                                        <%# Convert.ToDateTime(Eval("PlanDate")).ToString("yyyy年MM月") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="计划金额" HeaderStyle-Width="100px">
<ItemTemplate>
                                        <%# Convert.ToDecimal(Eval("PlanMoney")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="计划已用金额" HeaderStyle-Width="50px">
<ItemTemplate>
                                        <%# Convert.ToDecimal(Eval("UsedMoney")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="计划可用金额" HeaderStyle-Width="50px">
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
            <input id="btnSave" type="button" class="button-normal" value="确 定" disabled="disabled"
                onclick="saveEvent();" />
            <input id="btnCancel" type="button" value="取 消" class="button-normal" onclick="top.ui.closeWin();" />
        </div>
        <asp:HiddenField ID="hfldUID" runat="server" />
        <asp:HiddenField ID="hfldPlanDate" runat="server" />
        <asp:HiddenField ID="hfldPlanMoney" runat="server" />
        <asp:HiddenField ID="hfldCollectedMoney" runat="server" />
        <asp:HiddenField ID="hfldAllowCollectMoney" runat="server" />
        <asp:HiddenField ID="hfldRemark" runat="server" />
    </div>
    </form>
</body>
</html>
