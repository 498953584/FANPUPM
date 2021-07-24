<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="MultiSelectTask.aspx.cs" Inherits="StockManage_UserControl_MultiSelectTask" MaintainScrollPositionOnPostBack="true" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>任务选择</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jquery.treetable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.blockUI.js"></script>
    <script type="text/javascript" src="../../Script/jquery-budgetpait.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnBindSelectedResource').hide();
            replaceEmptyTable('emptyBudget', 'gvBudget');
            //上面的GVW
            var IsLeafNode = true;
            $('#gvBudget').treetable(true, 'SelectTaskByRpt');
            $('#gvBudget').delegate(':checkbox:gt(0)', 'click', checkSingle);
            $('#gvBudget').delegate(':checkbox:eq(0)', 'click', checkAll);
            setControlButton('hfldCheckedIds', null, null, null, null, null, 'btnSave');

            if ($('#hfldResource').val()) {
                $('#gvBudget tr:gt(0)').each(function () {
                    if ($('#hfldResource').val().indexOf(this.id) >= 0) {
                        $(this).find('input[type=checkbox]').attr('checked', true);
                    }
                });
            }

            $('#btnCancel').click(function () {
                $(parent.document).find('.ui-icon-closethick').each(function () {
                    this.click();
                });
            });

            $('#btnSave').click(function () {
                var isLeaf;
                var checkedIds = $('#hfldCheckedIds').val();
                var tasks = new Array();
                if (checkedIds.indexOf('[') > -1) {
                    var reg = new RegExp('"', 'g');
                    tasks = checkedIds.substring(1, checkedIds.length - 1).replace(reg, '').split(',');
                } else {
                    tasks.push(checkedIds);
                }
                var leafTask = new Array();
                var leafName = new Array();
                for (i = 0; i < tasks.length; i++) {
                    $.ajax({
                        type: 'GET',
                        async: false,
                        url: '/BudgetManage/Handler/IsLeafNode.ashx?' + new Date().getTime() + '&Type=Task&Id=' + tasks[i],
                        success: function (data) {
                            isLeaf = data;
                        }
                    });
                    if (isLeaf == 1) {
                        leafTask.push(tasks[i]);
                        leafName.push(getTaskCode(tasks[i]));
                    }
                }
                $(parent.document).find('.ui-icon-closethick').each(function () {
                    this.click();
                });
                //$(parent.document).find('input[id$=hfldResourceId]').val(array1dToJson(leafTask));
                //$(parent.document).find('input[id$=hfldType]').val('task');
                var TaskId = array1dToJson(leafTask) || '';
                var TaskName = array1dToJson(leafName) || '';
                var type = 'task' || '';
                if (getRequestParam('ButtonId') != '0') {
                    //parent.document.getElementById(getRequestParam('ButtonId')).click();
                    var winNo = jw.getRequestParam('winNo');
                    top.ui.closeWin({ winNo: winNo });
                    if (typeof top.ui.callback == 'function') {
                        top.ui.callback({ TaskIds: TaskId, TaskNames: leafName, type: type, btnBindResTask: getRequestParam('ButtonId') });
                        top.ui.callback = null;
                    }
                }
            });

        });

        //根据任务Id获得任务编号
        function getTaskCode(taskId) {
            var taskInfo;
            $.ajax({
                type: 'GET',
                async: false,
                url: '/BudgetManage/Handler/GetTaskInfo.ashx?' + new Date().getTime() + '&taskId=' + taskId,
                success: function (data) {
                    taskInfo = data;
                }
            });
            return taskInfo.TaskName;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%;">
        <div style="width: 100%; height: 375px; overflow: auto; float: left;">
            <asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwResource_RowDataBound" DataKeyNames="TaskId,SubCount,OrderNumber,TaskName" runat="server">
<EmptyDataTemplate>
                    <table id="emptyBudget" class="gvdata">
                        <tr class="header">
                            <th scope="col" style="width: 20px;">
                                <input type="checkbox" />
                            </th>
                            <th scope="col" style="width: 25px;">
                                序号
                            </th>
                            <th scope="col">
                                名称
                            </th>
                            <th scope="col">
                                编码
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
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" />
                        </HeaderTemplate><ItemTemplate>
                            <asp:CheckBox ID="chkSingle" runat="server" />
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                            <%# Eval("No") %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="清单编码"><ItemTemplate>
                            <%# Eval("Taskcode") %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
                            <span style="vertical-align: middle;">
                                <%# StringUtility.GetStr(Eval("TaskName").ToString(), 8) %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型"><ItemTemplate>
                            <%# Eval("TypeName") %>
                        </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="单位" DataField="Unit" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
        </div>
        <div style="width: 100%; height: 25px; float: left; text-align: right; vertical-align: middle">
            <br />
            <input type="button" id="btnSave" disabled="disabled" value="保存" />
            <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
        </div>
    </div>
    <asp:HiddenField ID="hfldResource" runat="server" />
    <asp:HiddenField ID="hfldType" runat="server" />
    <asp:HiddenField ID="hfldCheckedIds" runat="server" />
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    </form>
</body>
</html>
