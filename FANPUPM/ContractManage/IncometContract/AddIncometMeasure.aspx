<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddIncometMeasure.aspx.cs" Inherits="ContractManage_IncometContract_AddIncometMeasure" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnBindTask').css('display', 'none');
            // 设置所有的输入框不能用
            if (getRequestParam('t') == 1) {
                setAllInputDisabled();
                $('#txtNode').attr('disabled', 'disabled');
            }

            var jwTable = new JustWinTable('gvwConsTask');
            jwTable.registClickTrListener(function () {
                $('#hfldConsTaskId').val(this.id);
                $('#hfldAmount').val(this.getAttribute('Amount'));
                $('#hfldLastAmount').val(this.getAttribute('LastAmount'));
                $('#btnDel').removeAttr('disabled');
                if (getRequestParam('t') == 1) {
                    $('#btnDel').attr('disabled', 'disabled');
                }
            });

            // 注册单选事件
            jwTable.registClickSingleCHKListener(function () {
                var chkArr = jwTable.getCheckedChk();
                if (chkArr.length > 0) {
                    $('#hfldConsTaskId').val(jwTable.getCheckedChkIdJson(chkArr));
                    $('#btnDel').removeAttr('disabled');
                } else {
                    $('#btnDel').attr('disabled', 'disabled');
                }
                if (getRequestParam('t') == 1) {
                    $('#btnDel').attr('disabled', 'disabled');
                }
            });

            // 注册全选事件
            jwTable.registClickAllCHKLitener(function () {
                if ($(this).attr('checked') == 'checked') {
                    $('#hfldConsTaskId').val(jwTable.getCheckedChkIdJson());
                    $('#btnDel').removeAttr('disabled');
                } else {
                    $('#btnDel').attr('disabled', 'disabled');
                }
                if (getRequestParam('t') == 1) {
                    $('#btnDel').attr('disabled', 'disabled');
                }
            });

            $('#gvwConsTask tr').live('click', function () {
                if (getRequestParam('t') == 1) {
                    $('#btnDel').attr('disabled', 'disabled');
                }
            });
            // 计算完成百分比
            calcCompletePercent();
            // 完成量添加Keyup事件
            $('#gvwConsTask input[id$=txtCompleteAmount]').keyup(completeAmountKeyupEvent);
            // 完成百分比添加Keyup事件
            $('#gvwConsTask input[id$=txtCompletePercent]').keyup(completePercentKeyupEvent);
        });

        // 添加任务节点
        function addTask() {
            var prjId = $('#hfldPrjGuid').val();
            var contractId = $('#hfldContractId').val();
            var url = '/ContractManage/IncometContract/MultiSelectConTask.aspx?' + (new Date()).getMilliseconds() + '&prjId=' + prjId + '&contractId=' + contractId;
            top.ui.callback = function (idJson) {
                $('#hfldTaskId').val(idJson);
                $('#btnBindTask').click();
            }
            top.ui.openWin({ title: '选择任务', url: url, width: 820, height: 500 });
        }

        // 计算完成百分比
        function calcCompletePercent() {
            $('#gvwConsTask tr[id]').each(function () {
                var amount = $(this).find('input[id$=txtCompleteAmount]').val().replace(/\,/g, '');   // 完成量
                var total = $(this).find('.total').text().replace(/\,/g, '');                         // 小计
                var percent = getCompletePercent(amount, total);                                    // 完成百分比
                if (isNaN(percent)) {
                    percent = 0;
                }
                $(this).find('input[id$=txtCompletePercent]').val(percent * 100);
            });
        }

        // 计算完成百分比
        function getCompletePercent(amount, total) {
            if (!amount || !total) { return 0.0; }
            if (isNaN(amount) && isNaN(total)) { return 0.0; }
            if (parseInt(total) == 0) { return 0.0; }

            return amount / total;
        }

        // 完成量的Keyup事件
        function completeAmountKeyupEvent() {
            var $tr = $(this).parent().parent();
            var amount = $(this).val().replace(/\,/g, '');
            var total = $tr.find('.total').text().replace(/\,/g, '');
            var oldAmount = $('#hfldAmount').val();
            var oldLastAmount = $('#hfldLastAmount').val();
            var type = $('#hfldType').val();
            var lastAmount = $tr.find('.lastAmount').text().replace(/\,/g, '');
            var percent = getCompletePercent(amount, total);
            if (isNaN(percent)) {
                percent = 0;
            }
            if (!isNaN(parseFloat(amount))) {
                $tr.find('.lastAmount').text(parseFloat(oldLastAmount) + parseFloat(oldAmount) - parseFloat(amount));
            }
            var percentFormat = _formatDecimal((percent * 100) + '');
            $tr.find('input[id$=txtCompletePercent]').val(percentFormat);
            if (parseFloat(amount) > parseFloat(oldLastAmount) && type == "add") {
                alert("完成量超出剩余工作量,请重新填报");
                $tr.find('input[id$=txtCompleteAmount]').val(parseFloat(oldAmount));
                $tr.find('input[id$=txtCompletePercent]').val(_formatDecimal(getCompletePercent(oldAmount, total) * 100 + ''));
                $tr.find('.lastAmount').text(parseFloat(oldLastAmount));
                return;
            }
            if (parseFloat(amount) > (parseFloat(oldAmount) + parseFloat(oldLastAmount)) && type == "edit") {
                alert("完成量超出剩余工作量,请重新填报");
                $tr.find('input[id$=txtCompleteAmount]').val(parseFloat(oldAmount));
                $tr.find('input[id$=txtCompletePercent]').val(_formatDecimal(getCompletePercent(oldAmount, total) * 100 + ''));
                $tr.find('.lastAmount').text(parseFloat(oldLastAmount));
                return;
            }
        }

        // 完成百分比的Keyup事件
        function completePercentKeyupEvent() {
            var $tr = $(this).parent().parent();
            var total = $tr.find('.total').text().replace(/\,/g, '');
            var percent = $(this).val().replace(/\,/g, '');
            var amount = (percent * total) / 100;
            var oldAmount = $('#hfldAmount').val();
            var type=$('#hfldType').val();
            var oldLastAmount = $('#hfldLastAmount').val();
            if (isNaN(amount)) {
                amount = 0;
            }
            if (!isNaN(parseFloat(amount))) {
                $tr.find('.lastAmount').text(parseFloat(oldLastAmount) + parseFloat(oldAmount) - parseFloat(amount));
            }
            $tr.find('input[id$=txtCompleteAmount]').val(_formatDecimal(amount));
            var lastAmount = $tr.find('.lastAmount').text().replace(/\,/g, '');
            if (parseFloat(amount) > parseFloat(oldLastAmount) && type == "add") {
                alert("完成量超出剩余工作量,请重新填报");
                $tr.find('input[id$=txtCompleteAmount]').val(parseFloat(oldAmount));
                $tr.find('input[id$=txtCompletePercent]').val(_formatDecimal(getCompletePercent(oldAmount, total) * 100 + ''));
                $tr.find('.lastAmount').text(parseFloat(oldLastAmount));
                return;
            }
            if (parseFloat(amount) > (parseFloat(oldAmount) + parseFloat(oldLastAmount)) && type == "edit") {
                alert("完成量超出剩余工作量,请重新填报");
                $tr.find('input[id$=txtCompleteAmount]').val(parseFloat(oldAmount));
                $tr.find('input[id$=txtCompletePercent]').val(_formatDecimal(getCompletePercent(oldAmount, total) * 100 + ''));
                $tr.find('.lastAmount').text(parseFloat(oldLastAmount));
                return;
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden">
        <table style="border: 0px; width: 100%; height: 100%;">
            <tr id="divTop">
                <td style="text-align: left;" class="divFooter">
                    <span>项目名称：</span>
                    <asp:Label ID="lblPrjName" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp; <span>
                        合同名称：</span>
                    <asp:Label ID="lblContractName" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                    <span>上报日期：</span>
                    <asp:TextBox ID="txtDate" Width="80px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td style="text-align: right;" class="divFooter">
                    <input id="btnSelectTask" type="button" value="选择任务" style="width: 80px;
                        margin-right: 10px;" onclick="addTask();" runat="server" />

                    <asp:Button Width="80px" ID="btnDel" Text="删除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗?')" OnClick="btnDelete_Click" runat="server" />
                    <asp:Button ID="btnOk" Width="80px" Text="保存" OnClick="btnOk_Click" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 100%; height: 70%; vertical-align: top;">
                    <div class="pagediv" style="overflow: auto;">
                        <asp:GridView ID="gvwConsTask" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwConsTask_RowDataBound" DataKeyNames="ConsTaskId,Amount" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" />
                                    </HeaderTemplate><ItemTemplate>
                                        <asp:CheckBox ID="chkSingle" runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="任务编码" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px"><ItemTemplate>
                                        <span>
                                            <%# Eval("ContractTask.TaskCode") %>
                                        </span>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="分项工作 " ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                        <span>
                                            <%# Eval("ContractTask.TaskName") %>
                                        </span>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="50px">
<ItemTemplate>
                                        <span>
                                            <%# Eval("ContractTask.Unit") %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="综合单价" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="60px">
<ItemTemplate>
                                        <span class="decimal_input unit-price">
                                            <%# Eval("ContractTask.UnitPrice") %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="工程量" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="60px">
<ItemTemplate>
                                        <span class="decimal_input qty">
                                            <%# Eval("ContractTask.Quantity") %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="小计" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="60px"><ItemTemplate>
                                        <span class="decimal_input total">
                                            <%# Eval("ContractTask.Total") %></span>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="剩余工作量" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                        <asp:Label ID="lblLastAmount" Style="text-align: right; width: 90%;" CssClass="decimal_input lastAmount" Text='<%# System.Convert.ToString(System.Convert.ToDecimal(Eval("ContractTask.Total")) - base.GetSumTotal(Eval("TaskId").ToString()), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申报量(元)" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px"><ItemTemplate>
                                        <asp:TextBox ID="txtCompleteAmount" Style="text-align: right;" CssClass="decimal_input" Text='<%# System.Convert.ToString(Eval("Amount"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="120px"><HeaderTemplate>
                                        <span title="完成量 / 总工作量">完成百分比</span>
                                    </HeaderTemplate><ItemTemplate>
                                        <asp:TextBox ID="txtCompletePercent" Style="text-align: right; width: 90%;" CssClass="decimal_input" runat="server"></asp:TextBox>%
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="核准量(元)" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px"><ItemTemplate>
                                        <asp:TextBox ID="txtApproveAmount" Style="text-align: right;" CssClass="decimal_input" Text='<%# System.Convert.ToString(Eval("ApproveAmount"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="形象进度" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                        <asp:TextBox ID="txtNote" Text='<%# System.Convert.ToString(Eval("Note"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="vertical-align: bottom;">
                    <table cellspacing="0" cellpadding="0" border="1px" style="width: 100%; border-bottom: 0px;
                        border-right: 0px;">
                        <tr>
                            <td class="td-label">
                                <span>技术质量</span><br />
                                <span>安全工作纪录:</span>
                            </td>
                            <td class="td-content">
                                <textarea id="txtNode" class="wysiwyg" cols="80" rows="150" style="width: 100%;" runat="server"></textarea>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:Button ID="btnBindTask" OnClick="btnBindTask_Click" runat="server" />
    <!-- 存储选择的任务节点-->
    <asp:HiddenField ID="hfldTaskId" runat="server" />
    <!-- 存储选择的施工报量分项ID-->
    <asp:HiddenField ID="hfldConsTaskId" runat="server" />
    
    <asp:HiddenField ID="hfldContractId" runat="server" />
    
    <asp:HiddenField ID="hfldPrjGuid" runat="server" />
    
    <asp:HiddenField ID="hfldRptId" runat="server" />
    <asp:HiddenField ID="hfldAmount" runat="server" />
    <asp:HiddenField ID="hfldLastAmount" runat="server" />
    <asp:HiddenField ID="hfldType" runat="server" />
    </form>
</body>
</html>
