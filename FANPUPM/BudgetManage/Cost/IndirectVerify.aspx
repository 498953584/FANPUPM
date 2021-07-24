<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndirectVerify.aspx.cs" Inherits="BudgetManage_Cost_IndirectVerify" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>间接成本预算</title>

    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            setWidthHeight();
            var gvBudget = new JustWinTable('gvBudget');
            setButton(gvBudget, 'btnPass', 'btnCancelVerify', 'btnCancelEReport', 'hfldCheckeds');
            if (document.getElementById('gvBudget')) {
                if (document.getElementById('hfldCheckeds').value != '') {
                    document.getElementById('hfldCheckeds').value = '';
                }
                keyPress();
                TotalBudgets();
            }
            fillSpace(3);
            totalAmount();
        });
        function setWidthHeight() {
            $('#divBudget').height($(this).height() - $('#trBudgetTop').height()-2);
            $('#div_project').height($(this).height() - $('#ddlYear').height()-4);
            $('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 8);
        }
        //合计间接成本预算金额不能大于预算金额
        function keyPress() {
            return;
            var rows = document.getElementById('gvBudget').rows;
            var sum = 0;
            if (arguments[0]) {
                //控制核算
                contrastAmount(rows[parseInt(arguments[0])]);
            }
            for (i = 2; i < rows.length; i++) {
                var accountAmount = rows[i].cells[6].firstChild.value;
                var budgetAmount = rows[i].cells[5].innerText;
                if (parseFloat(accountAmount)) {
                    sum += parseFloat(accountAmount);
                }
            }
            rows[1].cells[6].firstChild.value = getFormat(sum);
        }
        function TotalBudgets() {
            var rows = document.getElementById('gvBudget').rows;
            var sum = 0;
            for (i = 2; i < rows.length; i++) {
                var budget = rows[i].cells[5].innerText;
                if (parseFloat(budget)) {
                    sum += parseFloat(budget);
                }
            }
            rows[1].cells[5].innerText = getFormat(sum);
        }
        //合计已审核的核算金额
        function totalAmount() {
            if (document.getElementById('gvBudget')) {
                var rows = document.getElementById('gvBudget').rows;
                var sum = 0;
                for (i = 2; i < rows.length; i++) {
                    var state = rows[i].cells[4].firstChild.state;
                    if (state == '2') {
                        var Amount = rows[i].cells[6].firstChild.value;
                        if (parseFloat(Amount)) {
                            sum += parseFloat(Amount);
                        }
                    }
                }
                rows[1].cells[6].firstChild.value = getFormat(sum);
            }
        }
        //格式化三位小数
        function getFormat(num) {
            var numFormat;
            if (num.toFixed) {
                numFormat = num.toFixed(3);
            } else {
                var f3 = Math.pow(10, 3);
                numFormat = Math.round(num * f3) / f3;
            }
            return numFormat;
        }
        //复选框选择
        function setButton(jwTable, pass, cancelVerify, cancelEReport, hdChkId) {
            if (!jwTable.table) return;
            if (jwTable.table.firstChild.childNodes.length == 1) {
                //table中没有数据
                return;
            }
            jwTable.registClickTrListener(function() {
                disabledAll();
                checkSingle(this.id);
                if (hdChkId != '') {
                    document.getElementById(hdChkId).value = this.id;
                }
            });
            jwTable.registClickSingleCHKListener(function() {
                var checkedChk = jwTable.getCheckedChk();
                if (checkedChk.length == 0) {
                    disabledAll();
                    if (hdChkId != '') {
                        document.getElementById(hdChkId).value = '';
                    }
                }
                else if (checkedChk.length == 1) {
                    disabledAll();
                    var ckId = jwTable.getCheckedChkIdJson(checkedChk);
                    checkSingle(ckId);
                    if (hdChkId != '') {
                        document.getElementById(hdChkId).value = ckId;
                    }
                }
                else {
                    disabledAll();
                    if (hdChkId != '') {
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);
                    }
                    // 多选
                    checkMultiple(document.getElementById(hdChkId).value);
                }
            });
            jwTable.registClickAllCHKLitener(function() {
                if (this.checked) {
                    disabledAll();
                    if (hdChkId != '') {
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
                    }
                    //多选
                    checkMultiple(document.getElementById(hdChkId).value);
                }
                else {
                    disabledAll();
                    if (hdChkId != '') {
                        document.getElementById(hdChkId).value = '';
                    }
                }
            });
        };
        //禁用按钮
        function disabledAll() {
            document.getElementById('btnPass').setAttribute('disabled', 'disabled');
            document.getElementById("btnCancelEReport").setAttribute('disabled', 'disabled');
            document.getElementById("btnCancelVerify").setAttribute('disabled', 'disabled');
        }
        //激活按钮
        function activateAll(state) {
            if (state == '1' || state == '4') {
                document.getElementById('btnPass').removeAttribute('disabled');
                document.getElementById('btnCancelEReport').removeAttribute('disabled');
            }
            else if (state == '2') {
                document.getElementById('btnCancelVerify').removeAttribute('disabled');
            }
        }
        //单选
        function checkSingle(id) {
            if (document.getElementById(id).childNodes[4].firstChild) {
                var state = document.getElementById(id).childNodes[4].firstChild.state;
                activateAll(state);
            }
        }
        //多选判断
        function checkMultiple(ids) {
            var flagPass = 0;
            var flagEReport = 0;
            var array = ids.substr(1, ids.length - 2).replace(/"/g, '').split(',');
            for (i = 0; i < array.length; i++) {
                var id = array[i];
                if (document.getElementById(id).childNodes[4].firstChild) {
                    var state = document.getElementById(id).childNodes[4].firstChild.state;
                    if (state == '1' || state == '4') {
                        flagEReport++;
                    }
                    else if (state == '2') {
                        flagPass++;
                    }
                }
            }
            if (flagEReport > 0 && flagPass == 0) {
                document.getElementById('btnPass').removeAttribute('disabled');
                document.getElementById('btnCancelEReport').removeAttribute('disabled');
            }
            else if (flagEReport == 0 && flagPass > 0) {
                document.getElementById('btnCancelVerify').removeAttribute('disabled');
            }
            else {
                disabledAll();
            }
        }
        //输入时控制核算
        function contrastAmount(row) {
            if (row != null) {
                var budgetAmount = row.childNodes[5].innerText;
                var accountAmount = row.childNodes[6].firstChild.value;
                if (parseFloat(accountAmount) > parseFloat(budgetAmount)) {
                    row.childNodes[6].firstChild.value = budgetAmount;
                    alert('系统提示：\n核算金额不能大于预算金额！');
                }
                else if (parseFloat(accountAmount) < 0) {
                    alert('系统提示：\n核算金额不能小于0！');
                    row.childNodes[6].firstChild.value = '0';
                }
            }
        }
        //填充其他成本空白字符
        function fillSpace(index) {
            if (document.getElementById('gvBudget')) {
                var rows = document.getElementById('gvBudget').rows;
                if (rows) {
                    for (i = 2; i < rows.length; i++) {
                        rows[i].cells[index].innerText = '         ' + rows[i].cells[index].innerText;
                    }
                }
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
        <table >
            <tr>
                <td style="width: 100%; height: 100%;">
                    <table style="width: 100%; height: 100%;">
                        <tr>
                            <td id="td_Left" style="width: 195px; vertical-align: top; height: 100%;">
                                <div style="">
                                    <asp:DropDownList ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                </div>
                                <div id="div_project" class="pagediv" style="width: 195px; height: 100%;">
                                    <asp:TreeView ID="tvBudget" Font-Size="12px" ShowLines="true" OnSelectedNodeChanged="TreeView_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                                </div>
                            </td>
                            <td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
                                <table class="tab">
                                    <tr>
                                        <td class="divFooter" style="text-align: left;">
                                            <asp:Button ID="btnPass" Text="通 过" Enabled="false" OnClick="btnPass_Click" runat="server" />
                                            <asp:Button ID="btnCancelVerify" Text="取消审核" Enabled="false" Width="75px" OnClick="btnCancelVerify_Click" runat="server" />
                                            <asp:Button ID="btnCancelEReport" Text="取消上报" Enabled="false" Width="75px" OnClick="btnCancelEReport_Click" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; height: 100%;">
                                            <div id="divBudget" class="pagediv">
                                                <asp:GridView ID="gvBudget" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvBudget_RowDataBound" DataKeyNames="Id,State,BudgetAmount" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                                <asp:CheckBox ID="cbBoxAll" runat="server" />
                                                            </HeaderTemplate><ItemTemplate>
                                                                <asp:CheckBox ID="cbBox" runat="server" />
                                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                                <%# (Container.DataItemIndex + 1).ToString() %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="CBS编码" DataField="CBSCode" HeaderStyle-Width="100px" /><asp:TemplateField HeaderText="名称"><ItemTemplate>
                                                                <%# Eval("CostAcc.Name") %>
                                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="上报状态">
<ItemTemplate>
                                                                <%# Common2.GetIndirectState(Eval("State").ToString()) %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="预算金额" DataField="BudgetAmount" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px" /><asp:TemplateField HeaderStyle-Width="100px" HeaderText="核准金额"><ItemTemplate>
                                                                <asp:TextBox ID="txtAccountAmount" CssClass="decimal_input" Style="text-align: right;" onkeypress="keyPress()" onkeyup="keyPress()" onblur="keyPress(this.no)" no='<%# System.Convert.ToString((Container.DataItemIndex + 1).ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' Text='<%# System.Convert.ToString(decimal.Parse(Eval("AccountAmount").ToString()).ToString("0.000"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    
    <asp:HiddenField ID="hfldCheckeds" runat="server" />
    </form>
</body>
</html>
