<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelMultiProgressTask.aspx.cs" Inherits="StockManage_UserControl_SelMultiProgressTask" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>选择进度计划任务</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="../script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript" src="../../Script/jquery.treetable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.blockUI.js"></script>
    <script type="text/javascript" src="../../Script/jquery-budgetpait.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnBindSelectedResource').hide();
            replaceEmptyTable('emptyBudget', 'gvBudget');
            //上面的GVW
            var IsLeafNode = true;
            $('#gvBudget').treetable(true, 'ProgressTask');
            $('#gvBudget').delegate(':checkbox:gt(0)', 'click', checkSingle);
            $('#gvBudget').delegate(':checkbox:eq(0)', 'click', checkAll);
            setControlButton('hfldCheckedIds', undefined, undefined, undefined, undefined, undefined, 'btnSave');
            if ($('#hfldResource').val()) {
                $('#gvBudget tr:gt(0)').each(function () {
                    if ($('#hfldResource').val().indexOf(this.id) >= 0) {
                        $(this).find('input[type=checkbox]').attr('checked', true);
                    }
                });
            }

//            //取消
//            $('#btnCancel').click(function () {
//                $(parent.document).find('.ui-icon-closethick').each(function () {
//                    top.ui.closeWin();
//                    //this.click();
//                });
//            });

            //保存
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
                for (i = 0; i < tasks.length; i++) {
                    var subCount = $('#' + tasks[i]).attr('subCount');
                    if (subCount == '0') {
                        leafTask.push(tasks[i]);
                    }
                }
                $(parent.document).find('.ui-icon-closethick').each(function () {
                    this.click();
                });
                $(parent.document).find('input[id$=hfldTaskId]').val(array1dToJson(leafTask));
                $(parent.document).find('input[id$=hfldType]').val('task');
                //alert($(parent.document).find('input[id$=hfldTaskId]').val());
                if (getRequestParam('ButtonId') != '0') {
                    var TaskId = array1dToJson(leafTask) || '';
                    var type = 'task' || '';
                    var winNo = jw.getRequestParam('winNo');
                    top.ui.closeWin({ winNo: winNo });
                    if (typeof top.ui.callback == 'function') {
                        top.ui.callback({ TaskIds: TaskId, type: type, btnBindResTask: getRequestParam('ButtonId') });
                        top.ui.callback = null;
                    }
                    //  parent.document.getElementById(getRequestParam('ButtonId')).click();

                }
            });

        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%;">
        <div style="width: 100%; height: 375px; overflow: auto; float: left;">
            <asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwResource_RowDataBound" DataKeyNames="UID_,subCount,layer" runat="server">
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
                                任务名称
                            </th>
                            <th scope="col">
                                工期
                            </th>
                            <th scope="col">
                                开始日期
                            </th>
                            <th scope="col">
                                结束日期
                            </th>
                            <th scope="col">
                                实际开始日期
                            </th>
                            <th scope="col">
                                实际结束日期
                            </th>
                            <th scope="col">
                                完成百分比
                            </th>
                        </tr>
                    </table>
                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
                            <asp:CheckBox ID="cbAllBox" runat="server" />
                        </HeaderTemplate>

<ItemTemplate>
                            <asp:CheckBox ID="cbBox" runat="server" />
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                            <%# Eval("No") %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="任务名称"><ItemTemplate>
                            <span style="vertical-align: middle;">
                                <%# StringUtility.GetStr(Eval("NAME_").ToString(), 8) %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="工期" DataField="DURATION_" ItemStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="开始日期" DataField="START_" DataFormatString="{0:yyyy-MM-dd}" /><asp:BoundField HeaderText="结束日期" DataField="FINISH_" DataFormatString="{0:yyyy-MM-dd}" /><asp:BoundField HeaderText="实际开始日期" DataField="ACTUALSTART_" DataFormatString="{0:yyyy-MM-dd}" /><asp:BoundField HeaderText="实际结束日期" DataField="ACTUALFINISH_" DataFormatString="{0:yyyy-MM-dd}" /><asp:TemplateField HeaderText="完成百分比">
<ItemTemplate>
                            <%# Eval("PERCENTCOMPLETE_").ToString() + "%" %>
                        </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
    </form>
</body>
</html>
