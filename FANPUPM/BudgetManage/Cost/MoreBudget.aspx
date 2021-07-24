<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoreBudget.aspx.cs" Inherits="BudgetManage_Cost_MoreBudget" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>间接成本月度预算</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript">
        $(document).ready(function () {
            var gvBudget = new JustWinTable('divBudget');
            setButton(gvBudget, 'btnDel', 'hfldCheckedIds');
            var accountAmount = getRequestParam('amount');
            document.getElementById('txtAccount').value = accountAmount;
            $('#divBudget tr').find('.month').change(function () {
                setSingleMonth();
            });
            $('#divBudget tr').find('.year').change(function () {
                setSingleMonth();
            });
            keyPress();
            $('#spanRepeat').hide();
            //            setSingleMonth();
            
        });

        function setButton(jwTable, btnDel, hdChkId) {
            if (!jwTable.table) return;

            // wdd  updata 2014/03/27
            if ($('#gvBudget tr').length == 1) {
                //table中没有数据
                return;
            }
            jwTable.registClickTrListener(function () {
                document.getElementById(btnDel).removeAttribute('disabled');
                if (hdChkId != '') {
                    document.getElementById(hdChkId).value = this.id;
                }
            });
            jwTable.registClickSingleCHKListener(function () {
                var checkedChk = jwTable.getCheckedChk();
                if (checkedChk.length == 0) {
                    document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                }
                else {
                    if (hdChkId != '') {
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);
                    }
                    document.getElementById(btnDel).removeAttribute('disabled');
                }
            });
            jwTable.registClickAllCHKLitener(function () {
                if (this.checked) {
                    if (hdChkId != '') {
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
                    }
                    document.getElementById(btnDel).removeAttribute('disabled');
                }
                else {
                    if (hdChkId != '') {
                        document.getElementById(hdChkId).value = '';
                    }
                    document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                }
            });
        };

        //合计间接成本预算金额
        function keyPress() {
            var total = 0.0;
            $('#gvBudget .curr_month_amount').each(function () {
                var amount = $(this).val().replace(/\,/g, '');
                total += parseFloat(amount);     
            });
            $('#txtCalculate').val(total);
            setBalance(total);
        }

        //设置余额
        function setBalance(sum) {
            var accountAmount = document.getElementById('txtAccount').value;
            var amount = accountAmount.replace(/\,/g, '');
            //alert(accountAmount)
            var balance = parseFloat(amount) - parseFloat(sum);
            document.getElementById('txtBalance').value = _formatDecimal(balance);
            //alert(document.getElementById('txtBalance').value)
            if (balance < 0) {
 
                document.getElementById('btnSave').setAttribute("disabled", "disabled");
                document.getElementById('txtBalance').style.color = 'red';
            }
            else {
                if (compareDate() == true) {
                    document.getElementById('btnSave').setAttribute("disabled", "disabled");
                } else {
                    document.getElementById('btnSave').removeAttribute("disabled");
                }
                document.getElementById('txtBalance').style.color = 'black';
                var lastid = $('#gvBudget tr:last').attr('id');
                if (lastid) setSingleMonth(lastid, false);
            }
        }

        //限定日期不允许重复
        function setSingleMonth() {
            if ($('#gvBudget')[0] != undefined) {
                var rows = document.getElementById('gvBudget').rows;
                if (rows.length > 1) {
                    var currentId = arguments[0] != null ? arguments[0] : document.getElementById('hfldCheckedIds').value;
                    var currentRow = document.getElementById(currentId);
                    var currentYear = $(currentRow).find('.year').val();
                    var currentMonth = $(currentRow).find('.month').val();
                    var isSame = false;
                    for (i = 1; i < rows.length; i++) {
                        var id = rows[i].id;
                        if (id != currentId) {
                            var year = $(rows[i]).find('.year').val();
                            var month = $(rows[i]).find('.month').val();
                            if (year + month == currentYear + currentMonth) {
                                isSame = true;
                            }
                        }
                    }
                    if (isSame) {
                        document.getElementById('btnSave').setAttribute("disabled", "disabled");
                        if (arguments[1] != false) {
                            $('#spanRepeat').show();
                            top.ui.alert('日期不能重复，请重新选择');
                        }
                    } else {
                        if (compareDate() == true) {
                            document.getElementById('btnSave').setAttribute("disabled", "disabled");
                            $('#spanRepeat').show();
                            top.ui.alert('日期不能重复，请重新选择');
                        } else {
                            $('#spanRepeat').hide();
                            //判断月度预算金额之和是否超出预算金额
                            var sum = document.getElementById('txtCalculate').value;
                            var accountAmount = document.getElementById('txtAccount').value;
                            var amount = accountAmount.replace(/\,/g, '');
                            var balance = parseFloat(amount) - parseFloat(sum);
                            if (balance < 0) {
                                document.getElementById('btnSave').setAttribute("disabled", "disabled");
                                document.getElementById('txtBalance').style.color = 'red';
                            } else {
                                document.getElementById('btnSave').removeAttribute("disabled");
                            }
                        }
                    }
                }
            }
        }
        //比较日期，判断有重复日期
        function compareDate() {
            var isRepeat = false;
            if ($('#gvBudget')[0] != undefined) {
                var rows = document.getElementById('gvBudget').rows;
                var dateArr = new Array();
                for (var i = 1; i < rows.length; i++) {
                    var date = rows[i].childNodes[5].children[0].value + rows[i].childNodes[5].children[1].value;
                    // 判断浏览器
                    if (navigator.appName != 'Microsoft Internet Explorer') {
                        date = rows[i].childNodes[6].children[0].value + rows[i].childNodes[6].children[1].value;
                    }
                    dateArr.push(date);
                }
                for (var i = 1; i < rows.length; i++) {
                    var currentDate = rows[i].childNodes[5].children[0].value + rows[i].childNodes[5].children[1].value;
                    // 判断浏览器
                    if (navigator.appName != 'Microsoft Internet Explorer') {
                        currentDate = rows[i].childNodes[6].children[0].value + rows[i].childNodes[6].children[1].value;
                    }
                    var count = 0;
                    for (var j = 0; j < dateArr.length; j++) {
                        if (dateArr[j] == currentDate) {
                            count++;
                        }
                        if (count > 1) {
                            isRepeat = true;
                            break;
                        }
                    }
                }
            }
            return isRepeat;
        }
        //控制金额不能为负数
        function decimalControl(row) {
            var value = row.cells[4].children[0].value;
            if (parseFloat(value)) {
                if (parseFloat(value) < 0) {
                    row.cells[4].children[0].value = '0';
                    top.ui.alert('本月金额不能小于0！');
                }
            }

        }
 
    </script>
    <style type="text/css">
        #tblCalculate
        {
            width: 100%;
        }
        #tblCalculate tr td
        {
            width: 33%;
            border: solid 1px #CADEED;
            text-align: center;
        }
        #tblCalculate tr td input
        {
            border-style: none;
            text-align: center;
        }
    </style>
</head>
<body id="body" style="overflow: hidden">
    <form id="form1" runat="server">
    <div class="divFooter" id="divFooter" style="text-align: left; vertical-align: middle;
        height: 20px;">
        <asp:Button ID="btnAdd" Text="添 加" OnClick="btnAdd_Click" runat="server" />
        <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
    </div>
    <div id="divBudget" class="" style="height: 370px; overflow: auto">
        <asp:GridView ID="gvBudget" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvBudget_RowDataBound" DataKeyNames="Id,Year,Month" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
                        <asp:CheckBox ID="cbBoxAll" runat="server" />
                    </HeaderTemplate>

<ItemTemplate>
                        <asp:CheckBox ID="cbBox" runat="server" />
                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="50px">
<ItemTemplate>
                        <%# (Container.DataItemIndex + 1).ToString() %>
                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="CBS编码"><ItemTemplate>
                        <%# Eval("IndirectBudget.CostAcc.Code") %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="名称"><ItemTemplate>
                        <%# Eval("IndirectBudget.CostAcc.Name") %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="本月金额" HeaderStyle-Width="100px">
<ItemTemplate>
                        <asp:TextBox ID="txtAmount" CssClass="decimal_input curr_month_amount" data-options="min:0,max:9999999999,precision:3,groupSeparator:','" Style="text-align: right;" onkeyup="keyPress()" onblur="keyPress(this.no)" no='<%# System.Convert.ToString((Container.DataItemIndex + 1).ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' Text='<%# System.Convert.ToString(decimal.Parse(Eval("Amount").ToString()).ToString("0.000"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="日期" HeaderStyle-Width="100px">
<ItemTemplate>
                        <asp:DropDownList ID="ddlYear" DataValueField="Year" runat="server"></asp:DropDownList>
                        年&nbsp;
                        <asp:DropDownList ID="ddlMonth" DataValueField="Month" runat="server"></asp:DropDownList>
                        月
                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
    </div>
    <div style="overflow: hidden; margin-top: 5px">
        <table id="tblCalculate" cellspacing="0">
            <tr>
                <td>
                    核准金额
                </td>
                <td>
                    月度预算合计
                </td>
                <td>
                    余额
                </td>
            </tr>
            <tr>
                <td>
                    <input type="text" id="txtAccount" value="0.000" class="decimal_input"/>
                </td>
                <td>
                    <input type="text" id="txtCalculate" value="0.000" class="decimal_input"/>
                </td>
                <td>
                    <input type="text" id="txtBalance" value="0.000"  class="decimal_input"/>
                </td>
            </tr>
        </table>
    </div>
    <div style="text-align: right; margin-top: 2px">
        <span id="spanRepeat" style="color: Red">月度预算日期不能重复!</span>
        <asp:Button ID="btnSave" Text="保 存" OnClick="btnSave_Click" runat="server" />
        <input type="button" id="btnClose" value="取 消" onclick="top.ui.closeWin();" />
    </div>
    
    <asp:HiddenField ID="hfldCheckedIds" runat="server" />
    </form>
</body>
</html>
