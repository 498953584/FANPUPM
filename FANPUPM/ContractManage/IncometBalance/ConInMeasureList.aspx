<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConInMeasureList.aspx.cs" Inherits="ContractManage_IncometBalance_ConInMeasureList" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link type="text/css" rel="Stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/wf.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvConract');
            replaceEmptyTable('emptyTable', 'gvConract');

            jwTable.registClickTrListener(function () {
                $('#btnSave').removeAttr('disabled');
                $('#hfldMeasureIDs').val($(this).attr('id'));
            });
            jwTable.registClickSingleCHKListener(function () {
                var checks = jwTable.getCheckedChk();
                if (checks.length == 0) {
                    $('#btnSave').attr('disabled', 'disabled');
                }
                else {
                    $('#btnSave').removeAttr('disabled');
                    $('#hfldMeasureIDs').val(jwTable.getCheckedChkIdJson(checks));
                }
            });
            //全选事件
            jwTable.registClickAllCHKLitener(function () {
                if ($(this).attr('checked')) {
                    $('#btnSave').removeAttr('disabled');
                    var checkCHK = jwTable.getAllChk();
                    $('#hfldMeasureIDs').val(jwTable.getCheckedChkIdJson(checkCHK));
                } else {
                    $('#btnSave').attr('disabled', 'disabled');
                    $('#hfldMeasureIDs').val('');
                }
            });
            jwTable.registDbClickListener(function () {
                var measureId = $(this).attr('id');
                if (typeof top.ui.callback == 'function') {
                    top.ui.callback({id: measureId });
                }
            });
        });
        function btnSaveReport() {
            var measureId = $('#hfldMeasureIDs').val();
            if (typeof top.ui.callback =='function') {
                top.ui.callback({ id: measureId });
                top.ui.callback = null;
            }
            top.ui.closeWin();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%;">
        <div style="width: 100%; height: 430px; overflow: auto; float: left;">
            <table class="tab">
                <tr>
                    <td style="width: 100%; vertical-align: top;">
                        <table border="0" class="tab">
                            <tr>
                                <td style="height: 100%; vertical-align: top;">
                                    <div class="pagediv">
                                        <asp:GridView ID="gvConract" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="RptId,ContractId,BalanceId,PrjId,FlowState" runat="server">
<EmptyDataTemplate>
                                                <table id="emptyTable">
                                                    <tr>
                                                        <th scope="col" style="width: 25px">
                                                        </th>
                                                        <th scope="col" style="width: 25px">
                                                            序号
                                                        </th>
                                                        <th scope="col">
                                                            记录人
                                                        </th>
                                                        <th scope="col">
                                                            上报日期
                                                        </th>
                                                        <th scope="col">
                                                            安全工作记录
                                                        </th>
                                                        <th scope="col" style="width: 50px">
                                                            流程状态
                                                        </th>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                        <asp:CheckBox ID="cbAllBox" runat="server" />
                                                    </HeaderTemplate><ItemTemplate>
                                                        <asp:CheckBox ID="cbBox" runat="server" />
                                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="记录人">
<ItemTemplate>
                                                        <%# Eval("InputUser") %>
                                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上报日期">
<ItemTemplate>
                                                        <%# Common2.GetTime(Eval("InputDate")) %>
                                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="安全工作记录"><ItemTemplate>
                                                         <span class="tooltip" title='<%# Eval("Note").ToString() %>'>
                                                                  <%# StringUtility.GetStr(Eval("Note").ToString(), 20) %>
                                                         </span>
                                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态">
<ItemTemplate>
                                                        <%# Common2.GetState(Eval("FlowState").ToString()) %>
                                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="添加状态">
<ItemTemplate>
                                                        <%# (Eval("Type").ToString() == "0") ? "手动录入" : "变更录入" %>
                                                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divFooter2" class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td>
                        <input type="button" id="btnSave" disabled="disabled" value="保存" onclick="btnSaveReport();" />
                        <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:HiddenField ID="hfldContractID" runat="server" />
    <asp:HiddenField ID="hfldMeasureIDs" runat="server" />
    <asp:HiddenField ID="hfldBalanceId" runat="server" />
    </form>
</body>
</html>
