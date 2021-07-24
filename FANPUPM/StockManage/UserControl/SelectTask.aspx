<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="SelectTask.aspx.cs" Inherits="StockManage_UserControl_SelectTask" MaintainScrollPositionOnPostBack="true" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>任务选择</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../script/Config2.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    
    <script type="text/javascript" src="../../Script/jquery.treetable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.blockUI.js"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnBindSelectedResource').hide();
            $('#btnSaveOK').hide();
            //上面的GVW
            $('#gvBudget').treetable(false, 'SelectTask');
            replaceEmptyTable('emptyBudget', 'gvBudget');
            $('#gvBudget tr:gt(0)').live('click', function () {
                var isLeaf;
                var $tr = $('<TR id=' + this.id + ' code=' + this.code + '>' + this.innerHTML + '</TR>');
                $.ajax({
                    type: 'GET',
                    async: false,
                    url: '/BudgetManage/Handler/IsLeafNode.ashx?' + new Date().getTime() + '&Type=Task&Id=' + this.id,
                    success: function (data) {
                        isLeaf = data;
                    }
                });
                if (isLeaf == 1) {
                    $('#btnSave').removeAttr('disabled');
                    $('#hfldResource').val(this.id);
                } else {
                    $('#btnSave').attr('disabled', 'disabled');
                }
            });
            if ($('#hfldResource').val()) {
                $('#gvBudget tr:gt(0)').each(function () {
                    if ($('#hfldResource').val().indexOf(this.id) >= 0) {
                        $(this).find('input[type=checkbox]').attr('checked', true);
                    }
                });
            }

            $('#btnCancel').click(function () {
                if (getRequestParam('type') == '2') {
                    // 新界面
                    top.ui.closeWin();
                }
                else {
                    $(parent.document).find('.ui-icon-closethick').each(function () {
                        this.click();
                    });
                }
            });


            $('#btnSave').click(function () {
                // 新界面
                if (getRequestParam('type') == '2') {
                    if (getRequestParam('resId') == '') {
                        // 资源为空选择资源
                        var resName = getRequestParam('resName');
                        var url = '/StockManage/UserControl/SelectResourceTemp.aspx?type=2&TypeCode=0&resourceName=' + encodeURI(resName);
                        top.ui._SelectTask = window;
                        var _callback = top.ui.callback;
                        top.ui.callback = function (resId) {
                            $('#hfldResourceIds').val(resId);
                            top.ui.callback = _callback;
                            $('#btnSaveOK').click();
                        }
                        top.ui.openWin({ title: '选择资源', url: url, width: 1010, height: 499, winNo: 2 });
                    }
                    else {
                        $('#btnSaveOK').click();
                    }
                }
            });
            // 真正保存方法
            $('#btnSaveOK').click(function () {
                if (getRequestParam('type') == '2') {
                    if (typeof top.ui.callback == 'function') {
                        var taskId = $('#hfldResource').val();
                        var resId = $('#hfldResourceIds').val();
                        top.ui.callback(taskId, resId);
                        top.ui.callback = null;
                    }
                    top.ui.closeWin();
                }
                else {
                    $(parent.document).find('.ui-icon-closethick').each(function () {
                        this.click();
                    });
                    $(parent.document).find('input[id$=hfldResourceId]').val(document.getElementById('hfldResource').value);
                    $(parent.document).find('input[id$=hfldType]').val('task');
                    if (getRequestParam('ButtonId') != '0') {
                        parent.document.getElementById(getRequestParam('ButtonId')).click();
                    }
                }
            });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%;">
        <div style="width: 100%; height: 375px; overflow: auto; float: left;">
            <asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwResource_RowDataBound" DataKeyNames="TaskId,SubCount,OrderNumber" runat="server">
<EmptyDataTemplate>
                    <table id="emptyBudget" class="gvdata">
                        <tr class="header">
                            
                            <th scope="col" style="width: 25px;">
                                序号
                            </th>
                            <th scope="col">
                                清单编码
                            </th>
                            <th scope="col">
                                项目名称
                            </th>
                            <th scope="col">
                                项目特征描述
                            </th>
                            <th scope="col">
                                类型
                            </th>
                            <th scope="col">
                                单位
                            </th>
                        </tr>
                    </table>
                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                            <%# Eval("No") %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="清单编码"><ItemTemplate>
                            <%# StringUtility.GetStr(Eval("TaskCode").ToString(), 8) %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
                            <%# StringUtility.GetStr(Eval("TaskName").ToString(), 8) %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目特征描述" HeaderStyle-Width="100px"><ItemTemplate>
                            <span class="tooltip" title='<%# Eval("FeatureDescription").ToString() %>'>
                                <%# StringUtility.GetStr(Eval("FeatureDescription"), 30) %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型"><ItemTemplate>
                            <%# Eval("TypeName") %>
                        </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="单位" DataField="Unit" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
        </div>
        <div style="width: 100%; height: 25px; float: left; text-align: right; vertical-align: middle">
            <br />
            <input type="button" id="btnSave" disabled="disabled" value="保存" />
            <input type="button" id="btnCancel" value="取消" />
            <input type="button" id="btnSaveOK" disabled="disabled" value="保存" />
        </div>
    </div>
    <asp:HiddenField ID="hfldResource" runat="server" />
    <asp:HiddenField ID="hfldType" runat="server" />
    <asp:HiddenField ID="hfldResourceIds" runat="server" />
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    </form>
</body>
</html>
