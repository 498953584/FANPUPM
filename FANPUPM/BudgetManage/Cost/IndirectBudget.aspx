<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndirectBudget.aspx.cs" Inherits="BudgetManage_Cost_IndirectBudget" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>间接成本预算</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/wf.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript">
        var cbsCodeIndex = 1;
        var accountIndex = 3;
        $(document).ready(function () {
            setWidthHeight();
            var gvBudget = new JustWinTable('gvBudget');
            clickTree('tvBudget', 'hfldPrjId');
        });
        function setWidthHeight() {
            $('#divBudget').height($(this).height() - $('#trBudgetTop').height() - 2);
            $('#div_project').height($(this).height() - $('#ddlYear').height() - 4);
            $('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 8);
        }
        //合计间接成本预算金额
        function keyPress() {
            var total = 0.0;
            $('#gvBudget .budget').each(function () {
                var amount = parseFloat($(this).val().replace(/\,/g, ''));
                total += amount;
            });
        
            if (!isNaN(total)) {
                $('#gvBudget .total_budget').val(_formatDecimal(total));
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

        //控制上报（未上报和取消上报 才能上报）
        function checkMultiple(ids) {
            var allow = true;
            var array = ids.substr(1, ids.length - 2).replace(/"/g, '').split(',');
            for (i = 0; i < array.length; i++) {
                var id = array[i];
                if (document.getElementById(id).childNodes[accountIndex].firstChild) {
                    var state = document.getElementById(id).childNodes[accountIndex].firstChild.state;
                    if (state == '1' || state == '2' || state == '4') {
                        allow = false;
                    }
                }
            }
            if (allow) {
                document.getElementById('btnEReport').removeAttribute('disabled');
            }
            else {
                document.getElementById('btnEReport').setAttribute('disabled', 'disabled');
            }
        }

        // 当成绩过大是，禁止保存
        function computTotal(txt) {
            var total = parseFloat($('#gvBudget .total_budget').val().replace(/\,/g, ''));

            if (total > 9999999999) {
                top.ui.alert('间接成本累计值过大!');
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
        <table>
            <tr>
                <td style="width: 100%; height: 100%;">
                    <table style="width: 100%; height: 100%;">
                        <tr>
                            
                            <td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
                                <table class="tab">
                                    <tr id="trBudgetTop">
                                        <td class="divFooter" style="text-align: left;">
                                            <asp:Button ID="btnSave" Style="cursor: pointer;" Text="保存" OnClick="btnSave_Click" runat="server" />
                                            <input type="button" id="btnView" value="查看" />
                                            <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="119" BusiClass="001" runat="server" />
                                            <span style="margin-left: 20px;">(<asp:Label ID="lblWFState" runat="server"></asp:Label>)</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; height: 100%;">
                                            <div id="divBudget" class="pagediv">
                                                <asp:GridView ID="gvBudget" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvBudget_RowDataBound" DataKeyNames="CBSCode,State" runat="server"><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                                <%# (Container.DataItemIndex + 1).ToString() %>
                                                            </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="CBS编码" DataField="CBSCode" ItemStyle-CssClass="cbs_code" ItemStyle-Width="100px" /><asp:TemplateField HeaderText="名称"><ItemTemplate>
                                                                <%# Eval("CostAcc.Name") %>
                                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="100px" HeaderText="预算金额"><ItemTemplate>
                                                                <asp:TextBox ID="txtBudgetAmount" Style="text-align: right;" onkeyup="keyPress()" onblur="computTotal(this)" no='<%# System.Convert.ToString((Container.DataItemIndex + 1).ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' Text='<%# System.Convert.ToString(decimal.Parse(Eval("AccountAmount").ToString()).ToString("0.000"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
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
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    <asp:HiddenField ID="hfldInputUser" runat="server" />
    
    <asp:HiddenField ID="hfldGuid" runat="server" />
    
    <asp:HiddenField ID="hfldFlowState" runat="server" />
    </form>
</body>
<script type="text/javascript">
    $(document).ready(function () {
        $('#btnStartWF').live('click', function () {
            ajaxSaveData();
        });
        $('#btnView').bind('click', function () {
            ajaxSaveData();
            var id = $('#hfldGuid').val();
            var url = '/BudgetManage/Cost/IndirectView.aspx?ic=' + id + '&businessCode=' + $('#WF1_BusinessCode').val();
            viewopen(url, '间接成本预算查看');
        });
    });

    //保存数据
    function ajaxSaveData() {
        var prjId = $('#hfldPrjId').val();
        var inputUser = $('#hfldInputUser').val();
        var guid = $('#hfldGuid').val();
        var url = '/BudgetManage/Handler/SaveIndirect.ashx';
        var cbs = getCBSJson();
        var flowState = $('#hfldFlowState').val();
        var data = '{"prjId":"' + prjId + '","inputUser":"' + inputUser + '","guid":"' + guid + '","type":"1","CBS":' + cbs + ',"flowState":"' + flowState + '"}';
        $.ajax({
            type: "POST",
            url: url,
            data: data,
            async: false,
            success: function () {
            },
            error: function (error) {
                alert(error);
            }
        });
    }
    //获取输入的预算金额JSON数据
    function getCBSJson() {
        var dataJson = '';
        var rows = $('#gvBudget tr');

        for (i = 2; i < rows.length; i++) {
            var code = jw.trim($(rows[i]).find('.cbs_code').text());
            var amount = $(rows[i]).find('.budget').val().replace(/\,/g, '');
            dataJson += '{"code":"' + code + '","amount":"' + amount + '"},';
        }
        dataJson = '[' + dataJson.substring(0, dataJson.length - 1) + ']';
        return dataJson;
    }
</script>
</html>
