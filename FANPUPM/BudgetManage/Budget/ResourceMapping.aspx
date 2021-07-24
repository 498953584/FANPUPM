<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceMapping.aspx.cs" Inherits="BudgetManage_Budget_ResourceMapping" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_usercontrol_selectrestask_ascx" Src="~/StockManage/UserControl/SelectResTask.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>资源映射</title>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script src="/Script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnBindResource').hide();
            $('#btnBindResTask').hide();
            var gvBudget = new JustWinTable('gvBudget');
            setButton(gvBudget, 'btnDel', 'btnResMapping', 'hfldIsLocked', 'hfldPurchaseChecked');
            addEvent(document.getElementById('btnResMapping'), 'click', mapping);
            setWidthHeight();
            //			clickTree('tvBudget', 'hfldPrjId');
        });

        function setWidthHeight() {
            $('#divBudget').height($(this).height() - $('#trBudgetTop').height() - 2);
            //			$('#div_project').height($(this).height() - $('#ddlYear').height() - 4);
            $('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 8);

        }

        // 得到当前所有选中的CHK的TaskId的Json字符串
        JustWinTable.prototype.getCheckedChkTaskIdJson = function (checkedChk) {
            //若参数为空，返回所有CHK
            checkedChk = checkedChk || this.getAllChk();
            var str = '["';
            for (var i = 0; i < checkedChk.length; i++) {
                var td = getFirstParentElement(checkedChk[i], 'TR');
                if (checkedChk.length == 1) {
                    return td.taskId;
                }
                str += td.taskId + '","';
            }
            return str.substring(0, str.length - 2) + ']';
        }

        //得到当前所有选中的CHK的resourceId的Json字符串
        JustWinTable.prototype.getCheckedChkResourceIdJson = function (checkedChk) {
            //若参数为空，返回所有CHK
            checkedChk = checkedChk || this.getAllChk();
            var str = '["';
            for (var i = 0; i < checkedChk.length; i++) {
                var td = getFirstParentElement(checkedChk[i], 'TR');
                if (checkedChk.length == 1) {
                    return td.resourceId;
                }
                str += td.resourceId + '","';
            }
            return str.substring(0, str.length - 2) + ']';
        }
        //得到当前所有选中的CHK的resourceName的Json字符串
        JustWinTable.prototype.getCheckedChkResourceNameJson = function (checkedChk) {
            //若参数为空，返回所有CHK
            checkedChk = checkedChk || this.getAllChk();
            var str = '["';
            for (var i = 0; i < checkedChk.length; i++) {
                var td = getFirstParentElement(checkedChk[i], 'TR');
                if (checkedChk.length == 1) {
                    return td.resourceName;
                }
                str += td.resourceName + '","';
            }
            return str.substring(0, str.length - 2) + ']';
        }
        //资源映射
        function mapping() {
            var id = $('#hfldPurchaseChecked').val();
            var tr = $('#' + id);
            $('#hfldTaskId').val(tr.attr('taskId'));
            $('#hfldResourceIds').val(tr.attr('resourceId'));
            $('#hfldResourceNames').val(tr.attr('resourceName'));
            if ($('#hfldIsWBSRelevance').val() == '1') {
                //如果资源与WBS挂钩时，导入失败的资源没有找到对应的节点时,先对应节点
                if ($("#hfldTaskId").val() == '') {
                    var url = '/StockManage/UserControl/SelectTask.aspx?type=2&prjId=' + $('#hfldPrjId').val() + '&resId=' + $('#hfldResourceIds').val() + '&resName=' + $('#hfldResourceNames').val();
                    top.ui._ResourceMapping = window;
                    top.ui.callback = function (taskId, resId) {
                        $('#hfldTaskId').val(taskId);
                        if ($('#hfldResourceIds').val() == '') {
                            $('#hfldResourceIds').val(resId);
                            $('#hfldType').val('resource');
                        }
                        else {
                            $('#hfldType').val('task');
                        }
                        $('#btnBindResTask').click();
                    }
                    top.ui.openWin({ title: '选择任务', url: url, width: 600, height: 460 });
                    return;
                }
                if ($("#hfldResourceIds").val() == '') {
                    var url = '/StockManage/UserControl/SelectResourceTemp.aspx?type=2&TypeCode=0&resourceName=' + encodeURI($('#hfldResourceNames').val());
                    top.ui._ResourceMapping = window;
                    top.ui.callback = function (resId, resName) {
                        $('#hfldResourceIds').val(resId);
                        $('#hfldType').val('resource');
                        $('#btnBindResTask').click();
                    }
                    top.ui.openWin({ title: '选择资源', url: url, width: 1010, height: 499 });
                    return;
                }
            } else {
                //如果资源不与WBS挂钩时，导入失败的资源不需要对应节点，直接选择资源
                if (document.getElementById('hfldResourceIds').value == '') {
                    var url = '/StockManage/UserControl/SelectResourceTemp.aspx?type=2&TypeCode=0&resourceName=' + encodeURI($('#hfldResourceNames').val());
                    top.ui._ResourceMapping = window;
                    top.ui.callback = function (resId, resName) {
                        $('#hfldResourceIds').val(resId);
                        $('#hfldType').val('resource');
                        $('#btnBindResTask').click();
                    }
                    top.ui.openWin({ title: '选择资源', url: url, width: 1010, height: 499 });
                    return;
                }
            }
        }
        function add() {
            if ($('#hfldIsWBSRelevance').val() == '1') {
                if (document.getElementById('hfldTaskId').value == "" && document.getElementById('hfldResourceIds').value == "") {
                    $('#hfldTaskId').val($('#hfldSelectedTaskId').val());
                } else {
                    $('#btnBindResource').click();
                }
            } else {
                $('#btnBindResource').click();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader" style="display: none;">
        <table class="tableHeader">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="资源映射" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
        <table>
            <tr>
                <td style="width: 100%; height: 100%;">
                    <table style="width: 100%; height: 100%;">
                        <tr>
                            <td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
                                <table border="0" class="tab">
                                    <tr id="trBudgetTop">
                                        <td class="divFooter" style="text-align: left;">
                                            <input type="button" style="width: 50px;" id="btnResMapping" disabled="true" value="映射" runat="server" />

                                            <asp:Button ID="btnDel" Text="删除" OnClientClick="return confirm('确定要删除吗？');" OnClick="btnDel_Click" runat="server" />
                                            <MyUserControl:stockmanage_usercontrol_selectrestask_ascx ID="SelectResource1" Text="增加资源" Width="75.0" ButtonId="btnBindResTask" TypeCode="0" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 100%; vertical-align: top;">
                                            <div id="divBudget" class="pagediv">
                                                <asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="Id,ResourceId,ResourceName,BudTask" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                                <asp:CheckBox ID="cbAllBox" runat="server" />
                                                            </HeaderTemplate><ItemTemplate>
                                                                <asp:CheckBox ID="cbBox" runat="server" />
                                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                                                <span style="cursor: default;">
                                                                    <%# (this.aspNetPageBudget.CurrentPageIndex - 1) * NBasePage.pagesize + Container.DataItemIndex + 1 %>
                                                                </span>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="任务名称"><ItemTemplate>
                                                                <span style="vertical-align: middle; cursor: default;">
                                                                    <%# Eval("BudTask.Name") %>
                                                                </span>
                                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源编号" HeaderStyle-Width="100px"><ItemTemplate>
                                                                <span style="cursor: default;">
                                                                    <%# Eval("ResourceCode") %>
                                                                </span>
                                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源名称"><ItemTemplate>
                                                                <span style="cursor: default;">
                                                                    <%# Eval("ResourceName") %>
                                                                </span>
                                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程量" HeaderStyle-Width="100px">
<ItemTemplate>
                                                                <span style="cursor: default;">
                                                                    <%# System.Convert.ToDecimal(Eval("Quantity")).ToString("0.000") %>
                                                                </span>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="综合单价" HeaderStyle-Width="90px">
<ItemTemplate>
                                                                <span style="cursor: default;">
                                                                    <%# System.Convert.ToDecimal(Eval("UnitPrice")).ToString("0.000") %>
                                                                </span>
                                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="小计" HeaderStyle-Width="100px">
<ItemTemplate>
                                                                <span style="cursor: default;">
                                                                    <%# Eval("Amount") %>
                                                                </span>
                                                            </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                                <webdiyer:AspNetPager ID="aspNetPageBudget" FirstPageText="首页" LastPageText="末页" NextPageText="下一页" PrevPageText="上一页" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到:" EnableTheming="true" ShowPageIndexBox="Always" HorizontalAlign="Right" OnPageChanged="aspNetPageBudget_PageChanged" runat="server">
                                                </webdiyer:AspNetPager>
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
    
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    <asp:HiddenField ID="hfldTaskId" runat="server" />
    <asp:HiddenField ID="hfldResourceIds" runat="server" />
    <asp:HiddenField ID="hfldResourceNames" runat="server" />
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    <asp:HiddenField ID="hfldYear" runat="server" />
    <asp:HiddenField ID="hfldIsLocked" runat="server" />
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    <asp:HiddenField ID="hfldType" runat="server" />
    <asp:HiddenField ID="hfldSelectedTaskId" runat="server" />
    <input type="button" id="btnBindResTask" onclick="add();" />
    <asp:Button ID="btnBindResource" OnClick="btnBindResource_Click" runat="server" />
    </form>
</body>
</html>
