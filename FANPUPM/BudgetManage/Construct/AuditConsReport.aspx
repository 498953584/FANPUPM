<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditConsReport.aspx.cs" Inherits="BudgetManage_Construct_AuditConsReport" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>施工报量审核</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script src="/Script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Script/Budget/BudgetPait.js"></script>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            var gvConstruct = new JustWinTable('gvConstruct');
            setButton2(gvConstruct, 'btnCancelAudit', 'btnCancelReport', 'btnTemImport', 'hfldPurchaseChecked');
            setWidthAndHeight();
            showTooltip('tooltip', 25);
            var arr = getCookie('scrollTop');
            document.getElementById('div_project').scrollTop = parseInt(arr);
            clickTree('tvBudget', 'hfldPrjId');
        });



        //页面刷新前保存滚动条位置信息到cookie
        window.onbeforeunload = function () {
            var scrollPos;
            scrollPos = document.getElementById('div_project').scrollTop;
            setCookie('scrollTop', scrollPos);
        }
        //设置div高度和宽度
        function setWidthAndHeight() {
            $('#divBudget').height($(this).height() - $('#divTop').height() - 2);
            $('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 8);
            $('#div_project').height($(this).height() - $('#ddlYear').height() - 4);
        }
        function setButton2(jwTable, btnDel, btnUpdate, btnQuery, hdChkId) {

            if (!jwTable.table) return;
            if (jwTable.table.firstChild.childNodes.length == 1) {
                //table中没有数据
                return;
            }
            jwTable.registClickTrListener(function () {
                if (hdChkId != '')
                    document.getElementById(hdChkId).value = this.id;
                if (this.guid) {
                    document.getElementById(btnQuery).guid = this.guid;
                }
                var checkedChk = jwTable.getCheckedChk();
                if (this.state == 1 || this.state == 4) {
                    document.getElementById('btnAudit').removeAttribute('disabled');
                    document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                    document.getElementById(btnUpdate).removeAttribute('disabled');
                } else if (this.state == 2) {
                    document.getElementById('btnAudit').setAttribute('disabled', 'disabled');
                    document.getElementById(btnDel).removeAttribute('disabled');
                    document.getElementById(btnUpdate).setAttribute('disabled', 'disabled');
                }
            });
            jwTable.registClickSingleCHKListener(function () {
                var checkedChk = jwTable.getCheckedChk();
                if (checkedChk.length == 0) {
                    document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                    document.getElementById(btnUpdate).setAttribute('disabled', 'disabled');
                    document.getElementById('btnAudit').setAttribute('disabled', 'disabled');
                } else if (checkedChk.length == 1) {
                    var tr = checkedChk[0].parentNode.parentNode.parentNode;
                    var state = tr.state;
                    if (state == 1 || state == 4) {
                        document.getElementById('btnAudit').removeAttribute('disabled');
                        document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                        document.getElementById(btnUpdate).removeAttribute('disabled');
                    } else if (state == 2) {
                        document.getElementById('btnAudit').setAttribute('disabled', 'disabled');
                        document.getElementById(btnDel).removeAttribute('disabled');
                        document.getElementById(btnUpdate).setAttribute('disabled', 'disabled');
                    }
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);
                }
                else {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);
                    var checkedChks = jwTable.getCheckedChk();
                    setTableState(checkedChk);
                }
            });
            jwTable.registClickAllCHKLitener(function () {
                document.getElementById(btnUpdate).setAttribute('disabled', 'disabled');
                if (this.checked) {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
                    var checkedChks = jwTable.getAllChk();
                    setTableState(checkedChks);
                }
                else {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = '';
                    document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                    document.getElementById(btnUpdate).setAttribute('disabled', 'disabled');
                    document.getElementById('btnAudit').setAttribute('disabled', 'disabled');
                }
                if (jwTable.table.rows.length == 2 && this.checked == true) {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
                }
            });
        };

        function setTableState(checkedChk) {
            var Edit = true;
            var cancelReport = 0;
            var cancelAudit = 0;
            document.getElementById('btnAudit').setAttribute('disabled', 'disabled');
            for (var i = 0; i < checkedChk.length; i++) {
                var tr = checkedChk[i].parentNode.parentNode.parentNode;
                state = tr.state;
                if (state == 1 || state == 4) {
                    cancelReport++;
                } else if (state == 2) {
                    cancelAudit++;
                }
            }
            if (cancelReport > 0 && cancelAudit == 0) {
                document.getElementById('btnCancelAudit').setAttribute('disabled', 'disabled');
                document.getElementById('btnCancelReport').removeAttribute('disabled');
            } else if (cancelAudit > 0 && cancelReport == 0) {
                document.getElementById('btnCancelAudit').removeAttribute('disabled');
                document.getElementById('btnCancelReport').setAttribute('disabled', 'disabled');
            } else {
                document.getElementById('btnCancelAudit').setAttribute('disabled', 'disabled');
                document.getElementById('btnCancelReport').setAttribute('disabled', 'disabled');
            }
        }


        function add() {
            parent.desktop.ConsTaskRes = window;
            var url = '/BudgetManage/Construct/AuditConsTask.aspx?&prjId=' + document.getElementById("hfldPrjId").value + '&conId=' + document.getElementById("hfldPurchaseChecked").value + '&year=' + document.getElementById("hfldYear").value;
            toolbox_oncommand(url, "施工量报审核");
        }

        //添加行进行显示资源信息
        var prevId;
        function showInfo(id) {
            var tab_Resource = null;
            $.ajax({
                type: 'GET',
                async: false,
                url: '/BudgetManage/Handler/ReturnConsRep.ashx?' + new Date().getTime() + '&consId=' + id + '&type=check',
                success: function (data) {
                    tab_Resource = data;
                }
            });
            var isDisplay = $('#' + id + '1').get(0);
            if (isDisplay == undefined) {
                $('#' + id).after('<tr id="' + id + '1"><td align="center" colspan="12" style="border:solid 1px #CADEED;">' + tab_Resource + '</td></tr>');
                if (prevId != undefined && prevId != id) {
                    $('#' + prevId + '1').remove();
                }
                prevId = id;
            }
            else {
                $('#' + id + '1').remove();
                isDisplay = undefined;
            }
        }

        //显示取消审核原因
        function displayCancelAuditReason() {
            document.getElementById('txtCancelAuditReason').value = '';
            $('#divCancelAuditReason').dialog({
                open: function () {
                    $(this).parent().appendTo("form:first");
                },
                width: 400,
                height: 170,
                modal: true,
                buttons: {
                    "确定": function () {
                        saveSignInfo();
                    }
                }
            });
        }

        //显示取消上报原因
        function displayCancelReportReason() {
            document.getElementById('txtCancelReportReason').value = '';
            $('#divCancelReportReason').dialog({
                open: function () {
                    $(this).parent().appendTo("form:first");
                },
                width: 400,
                height: 170,
                modal: true,
                buttons: {
                    "确定": function () {
                        saveCancelReportReason();
                    }
                }
            });
        }

        function saveSignInfo() {
            document.getElementById('btnSaveCancelAuditReason').click();
        }
        function saveCancelReportReason() {
            document.getElementById('btnSaveCancelReportReason').click();
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
                            <td id="td_Left" style="width: 194px; vertical-align: top; height: 100%;">
                                <div>
                                    <asp:DropDownList ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                </div>
                                <div id="div_project" class="pagediv" style="width: 194px; height: 100%;">
                                    <asp:TreeView ID="tvBudget" ShowLines="true" OnSelectedNodeChanged="TreeView_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                                </div>
                            </td>
                            <td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
                                <div id="div1" class="divContent2" style="width: 100%; height: 99%; overflow: hidden;">
                                    <table style="width: 100%; height: 88%;">
                                        <tr id="divTop">
                                            <td class="divFooter" style="text-align: left; width: 100%;">
                                                <input type="button" id="btnAudit" value="审核" style="width: 80px;" onclick="add();"
                                                    disabled="disabled" />
                                                
                                                <input type="button" id="btnCancelAudit" value="取消审核" style="width: 80px" disabled="disabled"
                                                    onclick="displayCancelAuditReason();" />
                                                
                                                <input type="button" id="btnCancelReport" value="取消上报" style="width: 80px" disabled="disabled"
                                                    onclick="displayCancelReportReason();" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 100%;">
                                                <div id="divBudget" class="pagediv" style="border-bottom: solid 0px red;">
                                                    <asp:GridView ID="gvConstruct" Width="100%" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvConstruct_RowDataBound" DataKeyNames="id,state" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
                                                                    <asp:CheckBox ID="cbAllBox" runat="server" />
                                                                </HeaderTemplate>

<ItemTemplate>
                                                                    <asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Eval("Id"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="50px">
<HeaderTemplate>
                                                                    序号
                                                                </HeaderTemplate>

<ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="200px">
<HeaderTemplate>
                                                                    上报时间
                                                                </HeaderTemplate>

<ItemTemplate>
                                                                    <%# System.Convert.ToDateTime(Eval("InputDate")).ToString("yyyy-MM-dd") %>
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
                                                                    <asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(StringUtility.StripTagsCharArray(Eval("WorkCard").ToString()), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(StringUtility.StripTagsCharArray(Eval("WorkCard").ToString()), 25) %></asp:HyperLink>
                                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="100px">
<HeaderTemplate>
                                                                    流程状态
                                                                </HeaderTemplate>

<ItemTemplate>
                                                                    <%# Common2.GetIndirectState(Eval("state").ToString()) %>
                                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
                                                                    取消审核原因
                                                                </HeaderTemplate>

<ItemTemplate>
                                                                    <asp:HyperLink ID="hlinkCancelAuditReason" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(Eval("CancelAuditReason"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(((Eval("CancelAuditReason") == null) ? "" : Eval("CancelAuditReason")).ToString(), 25) %></asp:HyperLink>
                                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
                                                                    取消上报原因
                                                                </HeaderTemplate>

<ItemTemplate>
                                                                    <asp:HyperLink ID="hlinkCancelReportReason" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(Eval("CancelReportReason"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(((Eval("CancelReportReason") == null) ? "" : Eval("CancelReportReason")).ToString(), 25) %></asp:HyperLink>
                                                                </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <asp:HiddenField ID="hfldPrjId" runat="server" />
                                <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
                                <asp:HiddenField ID="hfldYear" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    
    <div id="divCancelAuditReason" class="divContent2" title="取消审核原因" style="display: none;
        text-align: center;">
        <table style="width: 100%; margin: auto;" cellpadding="3px" cellspacing="0px">
            <tr>
                <td class="descTd" style="width: 20%;">
                    取消<br />
                    审核原因
                </td>
                <td>
                    <asp:TextBox ID="txtCancelAuditReason" Height="60px" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button ID="btnSaveCancelAuditReason" Text="确定" Style="display: none;" OnClick="btnSaveCancelAuditReason_Click" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    
    <div id="divCancelReportReason" class="divContent2" title="取消上报原因" style="display: none;
        text-align: center;">
        <table style="width: 100%; margin: auto;" cellpadding="3px" cellspacing="0px">
            <tr>
                <td class="descTd" style="width: 20%;">
                    取消<br />
                    上报原因
                </td>
                <td>
                    <asp:TextBox ID="txtCancelReportReason" Height="60px" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button ID="btnSaveCancelReportReason" Text="确定" Style="display: none;" OnClick="btnSaveCancelReportReason_Click" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
