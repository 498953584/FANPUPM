<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConPayMeasureList.aspx.cs" Inherits="ContractManage_PayoutBalance_ConPayMeasureList" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title><link type="text/css" rel="Stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

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
                    top.ui.callback({ id: measureId });
                }
            });
        });
        function btnSaveReport() {
            var measureId = $('#hfldMeasureIDs').val();
            if (typeof top.ui.callback == 'function') {
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
                                       <asp:GridView ID="gvConract" Width="100%" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="ConsReportId,State,PrjId" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
										<asp:CheckBox ID="cbAllBox" runat="server" />
									</HeaderTemplate>

<ItemTemplate>
										<asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("ConsReportId")) %>' runat="server" />
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="25px">
<HeaderTemplate>
										序号
									</HeaderTemplate>

<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="200px">
<HeaderTemplate>
										上报日期
									</HeaderTemplate>

<ItemTemplate>
										<%# Convert.ToDateTime(Eval("InputDate")).ToString("yyyy-MM-dd") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="200px">
<HeaderTemplate>
										记录人
									</HeaderTemplate>

<ItemTemplate>
										<%# Eval("InputUser") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
										安全工作纪录
									</HeaderTemplate>

<ItemTemplate>
										<asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# Convert.ToString(StringUtility.StripTagsCharArray(Eval("WorkCard").ToString())) %>' runat="server"><%# StringUtility.GetStr(StringUtility.StripTagsCharArray(Eval("WorkCard").ToString()), 25) %></asp:HyperLink>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
										流程状态
									</HeaderTemplate>

<ItemTemplate>
										<%# Common2.GetState(Eval("FlowState").ToString()) %>
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
    </form>
</body>
</html>
